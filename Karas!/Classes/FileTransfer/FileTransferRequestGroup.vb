Public Class FileTransferRequestGroup

    Public Delegate Sub ExecuteWhenDownloadFinishes(ByRef Collection As System.Collections.ObjectModel.Collection(Of DataCacheItem))

    Private GroupData As New System.Collections.Queue()
    Private DownloadBox As ucDownloadBox
    Private EndCallEvent As ExecuteWhenDownloadFinishes
    Private ThatEndCallEvent As ucDownloadBox.ExecuteWhenDownloadFinishes
    Private Rez As New System.Collections.ObjectModel.Collection(Of DataCacheItem)

    Private Structure GroupItem
        Public Type As DataCacheItem.DataType
        Public Category As String
        Public Name As String
        Public Connection As NetworkConnectionItem
        Public CallEvent As ucDownloadBox.ExecuteWhenDownloadFinishes
        Sub New(ByRef Type As DataCacheItem.DataType, ByVal Category As String, ByVal Name As String, ByRef Connection As NetworkConnectionItem, Optional ByRef CallEvent As ucDownloadBox.ExecuteWhenDownloadFinishes = Nothing)
            Me.Type = Type
            Me.Category = Category
            Me.Name = Name
            Me.Connection = Connection
            Me.CallEvent = CallEvent
        End Sub
    End Structure

    Protected Friend Sub New(ByRef DownloadBox As ucDownloadBox, Optional ByRef EndCallEvent As ExecuteWhenDownloadFinishes = Nothing)
        Me.DownloadBox = DownloadBox
        Me.EndCallEvent = EndCallEvent
    End Sub

    Public Sub Add(ByRef Type As DataCacheItem.DataType, ByVal Category As String, ByVal Name As String, ByRef Connection As NetworkConnectionItem, Optional ByRef CallEvent As ucDownloadBox.ExecuteWhenDownloadFinishes = Nothing)
        Me.GroupData.Enqueue(New GroupItem(Type, Category, Name, Connection, CallEvent))
    End Sub

    Public Sub Begin()
        If Me.GroupData.Count < 1 Then Exit Sub
        Dim item As GroupItem = Me.GroupData.Dequeue()
        Me.ThatEndCallEvent = item.CallEvent
        Me.DownloadBox.Request(item.Type, item.Category, item.Name, item.Connection, New ucDownloadBox.ExecuteWhenDownloadFinishes(AddressOf Done))
    End Sub

    Private Sub Done(ByRef Data As DataCacheItem)
        Rez.Add(Data)
        If Not Me.ThatEndCallEvent Is Nothing Then
            Me.ThatEndCallEvent.Invoke(Data)
            Me.ThatEndCallEvent = Nothing
        End If
        If Me.GroupData.Count < 1 Then
            If Not Me.EndCallEvent Is Nothing Then
                Me.EndCallEvent.Invoke(Rez)
            End If
            Exit Sub
        End If
        Dim item As GroupItem = Me.GroupData.Dequeue()
        Me.ThatEndCallEvent = item.CallEvent
        Me.DownloadBox.Request(item.Type, item.Category, item.Name, item.Connection, New ucDownloadBox.ExecuteWhenDownloadFinishes(AddressOf Done))
    End Sub


End Class
