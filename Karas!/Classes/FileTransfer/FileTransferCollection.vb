Imports Karas.FileTransferItem

Public Class FileTransferCollection

    Private _Items As New Collections.Specialized.ListDictionary()
    Private _Timeout As New System.TimeSpan(0, 5, 0)
    Private _Thread As New System.Threading.Thread(AddressOf RunningThread)
    Private _Timer As New System.Timers.Timer(5000)

    Public Event Added(ByRef FileTransferItem As FileTransferItem)
    Public Event Canceled(ByRef FileTransferItem As FileTransferItem, ByVal Auto As Boolean)
    Public Event Done(ByRef FileTransferItem As FileTransferItem, ByRef Data As DataCacheItem)
    Public Event UpdateStatus(ByRef FileTransferItem As FileTransferItem, ByVal Percent As Integer)

    Private Structure DictionaryItem
        Dim FileTransferItem As FileTransferItem
        Dim Type As DataCacheItem.DataType
        Dim LastUpdate As System.DateTime
        Sub New(ByRef FileTransferItem As FileTransferItem, ByVal Type As DataCacheItem.DataType, Optional ByVal LastUpdate As System.DateTime = Nothing)
            Me.FileTransferItem = FileTransferItem
            Me.Type = Type
            If LastUpdate = Nothing Then
                Me.LastUpdate = System.DateTime.Now
            Else
                Me.LastUpdate = LastUpdate
            End If
        End Sub
        Sub Update()
            Me.LastUpdate = System.DateTime.Now
        End Sub
    End Structure

    Private Sub RunningThread()
        Me._Timer.Stop()
        Dim Item As DictionaryItem
        For Each Key As String In Me._Items.Keys
            Item = Me._Items.Item(Key)
            If Item.LastUpdate.Subtract(System.DateTime.Now).TotalSeconds > Me._Timeout.TotalSeconds Then
                RaiseEvent Canceled(CType(Me._Items(Key), DictionaryItem).FileTransferItem, True)
                'Me._Items.Remove(Key)
                Exit For
            End If
            System.Threading.Thread.Sleep(100)
        Next
        Me._Timer.Start()
    End Sub

    Public Property Timeout() As System.TimeSpan
        Get
            Return Me._Timeout
        End Get
        Set(ByVal value As System.TimeSpan)
            Me._Timeout = value
        End Set
    End Property

    Public Sub Clear()
        Me._Items.Clear()
    End Sub

    Public Sub Request(ByVal Type As DataCacheItem.DataType, ByVal ResourceCategory As String, ByVal ResourceName As String, ByRef Connection As NetworkConnectionItem)        
        RaiseEvent Added(Me.Add(Type, FileTransferItem.Mode.Request, ResourceCategory, ResourceName, Connection))
    End Sub

    Private Function Add(ByRef Type As DataCacheItem.DataType, ByVal Command As FileTransferItem.Mode, ByVal ResourceCategory As String, ByVal ResourceName As String, ByRef Connection As NetworkConnectionItem, Optional ByVal ID As String = Nothing) As FileTransferItem
        Dim Item As New FileTransferItem(Type, ResourceCategory, ResourceName, Command, Connection)
        Dim dicItem As New DictionaryItem(Item, Type)
        If Not ID Is Nothing Then
            Item.ID = ID
            Me._Items.Add(ID, dicItem)
        Else
            Me._Items.Add(Item.ID, dicItem)
        End If
        AddHandler Item.Done, AddressOf _Done
        AddHandler Item.Canceled, AddressOf _Canceled
        AddHandler Item.UpdateStatus, AddressOf _UpdateStatus
        AddHandler Item.Finished, AddressOf _Finished
        Return Item
    End Function

    Sub _Finished(ByRef Obj As Object)        
        Me._Items.Remove(CType(Obj, FileTransferItem).ID)
    End Sub

    Sub _Done(ByRef Obj As Object, ByRef Data As DataCacheItem)
        RaiseEvent Done(Obj, Data)
        Me._Items.Remove(CType(Obj, FileTransferItem).ID)
    End Sub

    Sub _Canceled(ByRef Obj As Object)
        RaiseEvent Canceled(Obj, False)
        Me._Items.Remove(CType(Obj, FileTransferItem).ID)
    End Sub

    Sub _UpdateStatus(ByRef Obj As Object, ByVal PercentCompleted As Integer)
        RaiseEvent UpdateStatus(Obj, PercentCompleted)
    End Sub

    Private Function GetIt(ByVal ID As String) As DictionaryItem
        Dim DicItem As DictionaryItem = Me._Items.Item(ID)
        Return DicItem
    End Function

    Public Sub Update(ByVal Text As String, ByRef Connection As NetworkConnectionItem)        
        Text += "  "
        Dim data() As String = Text.Split(" ")
        Dim Opt As Command = Convert.ToSByte(data(0))
        Dim tmpName As String = data(1)
        Dim DicItem As DictionaryItem        
        Select Case Opt.ToString
            Case Command.RequestFileInfo.ToString, Command.BeginSend.ToString
                If Not Me._Items.Contains(tmpName) Then
                    Me.Add(DataCacheItem.DataType.Text, FileTransferItem.Mode.Send, data(2), data(3), Connection, tmpName)
                End If
                DicItem = Me._Items.Item(tmpName)
                DicItem.FileTransferItem.Update(Opt)
                DicItem.Update()
            Case Command.RequestPiece.ToString, Command.AcceptBeginSend.ToString, Command.SendType.ToString
                If Not Me._Items.Contains(tmpName) Then Exit Sub
                With GetIt(tmpName)
                    .FileTransferItem.Update(Opt, data(2))
                    .Update()
                End With
            Case Command.SendFileInfo.ToString                
                If Not Me._Items.Contains(tmpName) Then Exit Sub                
                With GetIt(tmpName)
                    .FileTransferItem.Update(Opt, data(2), data(3))
                    .Update()
                End With
            Case Command.SendPiece.ToString
                If Not Me._Items.Contains(tmpName) Then Exit Sub
                With GetIt(tmpName)
                    .FileTransferItem.Update(Opt, data(2), data(3), data(4))
                    .Update()
                End With
            Case Command.RequestType.ToString, Command.Finished.ToString
                If Not Me._Items.Contains(tmpName) Then Exit Sub
                With GetIt(tmpName)
                    .FileTransferItem.Update(Opt)
                    .Update()
                End With
        End Select
    End Sub

    Public Function Item(ByVal Index As Integer) As FileTransferItem
        Return CType(Me._Items.Item(Me._Items.Keys(Index)), DictionaryItem).FileTransferItem
    End Function

    Public Function Item(ByVal ID As String) As FileTransferItem
        Return CType(Me._Items.Item(ID), DictionaryItem).FileTransferItem
    End Function

    Public ReadOnly Property Count() As Integer
        Get
            Return Me._Items.Count
        End Get
    End Property

    Protected Overrides Sub Finalize()
        Me._Items.Clear()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        'Me._Timer.Start()
    End Sub
End Class
