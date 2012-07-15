Public Class ucDownloadBox

    Public WithEvents FileTransfers As New Karas.FileTransferCollection()

    Private Delegate Sub dlUpdateProgress(ByVal ID As String, ByVal Value As Integer)
    Private Delegate Sub dlAddToList(ByVal ID As String, ByVal Category As String, ByVal Name As String, ByVal IP As String)

    Public Delegate Sub ExecuteWhenDownloadFinishes(ByRef Data As Object)
    Private ExecuteWhenDownloadFinishesCollection As New System.Collections.Generic.Dictionary(Of String, ExecuteWhenDownloadFinishes)

    Private ActiveDownloadsCount As Integer = 0
    Private _AutoClear As Boolean = True

    Public Event DownloadStarted(ByVal Category As String, ByVal Name As String, ByRef Connection As NetworkConnectionItem)
    Public Event FirstDownloadStarted()
    Public Event DownloadEnded(ByRef FileTransferItem As FileTransferItem, ByRef Data As DataCacheItem)
    Public Event LastDownloadEnded()

    Private Delegate Sub dlClear()

    Private Sub DownloadBox_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.Width < 400 Then
            Me.chFrom.Width = Me.Width * 0.3
            Me.chProgress.Width = Me.Width * 0.3
        Else
            Me.chFrom.Width = 150
            Me.chProgress.Width = 150
        End If
        Me.chName.Width = Me.Width - Me.chFrom.Width - Me.chProgress.Width - 4
    End Sub

    Public Sub Clear()
        Me.FileTransfers.Clear()
        If Me.lvItems.InvokeRequired Then
            Me.lvItems.Invoke(New dlClear(AddressOf Me.lvItems.Items.Clear))
        Else
            Me.lvItems.Clear()
        End If
    End Sub

    Public Property AutoClear() As Boolean
        Get
            Return Me._AutoClear
        End Get
        Set(ByVal value As Boolean)
            Me._AutoClear = value
        End Set
    End Property

    Public Function CreateRequestGroup(Optional ByRef LastEvent As Karas.FileTransferRequestGroup.ExecuteWhenDownloadFinishes = Nothing) As FileTransferRequestGroup
        Return New FileTransferRequestGroup(Me, LastEvent)
    End Function

    Public Sub Request(ByVal Type As DataCacheItem.DataType, ByVal Category As String, ByVal Name As String, ByRef Connection As NetworkConnectionItem, Optional ByRef ExecuteWhenDownloadFinishes As ExecuteWhenDownloadFinishes = Nothing)
        Dim key As String = Category + "/" + Name
        If Me.FileTransfers.Count < 1 Then
            RaiseEvent FirstDownloadStarted()
        End If
        Me.FileTransfers.Request(Type, Category, Name, Connection)
        If Not ExecuteWhenDownloadFinishes Is Nothing Then
            If Me.ExecuteWhenDownloadFinishesCollection.ContainsKey(key) Then
                Me.ExecuteWhenDownloadFinishesCollection.Item(key) = ExecuteWhenDownloadFinishes.Combine(Me.ExecuteWhenDownloadFinishesCollection.Item(key))
                'Me.ExecuteWhenDownloadFinishesCollection.Item(key) = Me.ExecuteWhenDownloadFinishesCollection.Item(key).Combine(ExecuteWhenDownloadFinishes)
            Else
                Me.ExecuteWhenDownloadFinishesCollection.Add(key, ExecuteWhenDownloadFinishes)
            End If
        End If
        RaiseEvent DownloadStarted(Category, Name, Connection)
        Me.ActiveDownloadsCount += 1
    End Sub

    Private Sub UpdateProgress(ByVal ID As String, ByVal Value As Integer)
        With Me.lvItems.Items(ID).SubItems(2)
            .Text = Value.ToString + "%"
        End With
    End Sub

    Private Sub AddToList(ByVal ID As String, ByVal Category As String, ByVal Name As String, ByVal IP As String)
        Me.lvItems.Items.Add(ID, Category + "/" + Name, 0)
        Me.lvItems.Items(ID).SubItems.Add(IP)
        Me.lvItems.Items(ID).SubItems.Add(0)
    End Sub

    Private Sub FileTransfers_Added(ByRef FileTransferItem As FileTransferItem) Handles FileTransfers.Added
        If Me.lvItems.InvokeRequired Then
            Me.lvItems.Invoke(New dlAddToList(AddressOf AddToList), FileTransferItem.ID, FileTransferItem.ResourceCategory, FileTransferItem.ResourceName, FileTransferItem.Connection.InfoList("remote.address"))
        Else
'            MsgBox(FileTransferItem.ResourceCategory)
            Me.AddToList(FileTransferItem.ID, FileTransferItem.ResourceCategory, FileTransferItem.ResourceName, FileTransferItem.Connection.InfoList("remote.address"))
        End If
    End Sub

    Private Sub FileTransfers_Done(ByRef FileTransferItem As FileTransferItem, ByRef Data As DataCacheItem) Handles FileTransfers.Done
        Dim key As String = FileTransferItem.ResourceCategory + "/" + FileTransferItem.ResourceName
        If Me.ExecuteWhenDownloadFinishesCollection.ContainsKey(key) Then
            Me.ExecuteWhenDownloadFinishesCollection.Item(key).Invoke(Data)
            Me.ExecuteWhenDownloadFinishesCollection.Remove(key)
        End If
        Me.ActiveDownloadsCount -= 1
        RaiseEvent DownloadEnded(FileTransferItem, Data)
        If Me.ActiveDownloadsCount < 1 Then
            RaiseEvent LastDownloadEnded()
            If Me.AutoClear Then Me.Clear()
        End If
    End Sub

    Private Sub FileTransfers_UpdateStatus(ByRef FileTransferItem As FileTransferItem, ByVal Percent As Integer) Handles FileTransfers.UpdateStatus
        If Me.lvItems.InvokeRequired Then
            Me.lvItems.Invoke(New dlUpdateProgress(AddressOf UpdateProgress), FileTransferItem.ID, Percent)
        Else
            Me.UpdateProgress(FileTransferItem.ID, Percent)
        End If
    End Sub

    Private Sub lvItems_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles lvItems.DrawColumnHeader
        e.DrawDefault = True
    End Sub

    Private Sub lvItems_DrawSubItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles lvItems.DrawSubItem
        Using gr As System.Drawing.Graphics = e.Graphics
            If e.ColumnIndex = 2 Then
                Dim rect As New System.Drawing.Rectangle(e.Item.SubItems(e.ColumnIndex).Bounds.X, e.Item.SubItems(e.ColumnIndex).Bounds.Y, e.Item.SubItems(e.ColumnIndex).Bounds.Width, e.Item.SubItems(e.ColumnIndex).Bounds.Height)
                Dim bcolor As Color = Color.FromKnownColor(KnownColor.MenuHighlight)
                Dim colors() As Color = {Color.FromKnownColor(KnownColor.MenuHighlight), Color.FromKnownColor(KnownColor.HighlightText)}
                Dim border As New System.Drawing.Pen(bcolor)
                Dim value As Integer = Val(e.Item.SubItems(e.ColumnIndex).Text)
                Dim fill As System.Drawing.Brush = New Drawing.Drawing2D.LinearGradientBrush(rect, colors(0), colors(1), Drawing2D.LinearGradientMode.Vertical)  ' System.Drawing.Brushes.AliceBlue
                rect.Y += 0
                rect.X += 1
                rect.Width -= 2
                rect.Height -= 0
                value = rect.Width * 0.01 * value
                Debug.WriteLine(value.ToString)
                gr.FillRectangle(fill, rect.X, rect.Y, value, rect.Height)
                gr.DrawRectangle(border, rect)
                e.DrawText(TextFormatFlags.HorizontalCenter)
            Else
                e.DrawDefault = True
            End If
            '            gr.DrawString(e.Item.SubItems(e.ColumnIndex).Text, Me.lvItems.Font,System.Drawing.Brushes.
        End Using
        'e.DrawText(TextFormatFlags.SingleLine)
    End Sub

    Private Sub lvItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvItems.SelectedIndexChanged

    End Sub
End Class
