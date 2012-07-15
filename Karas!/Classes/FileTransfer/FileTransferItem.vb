Public Class FileTransferItem

    Public Event UpdateStatus(ByRef Obj As FileTransferItem, ByVal PercentCompleted As Integer)
    Public Event Canceled(ByRef Obj As FileTransferItem)
    Public Event Done(ByRef Obj As FileTransferItem, ByRef Content As DataCacheItem)
    Public Event Finished(ByRef Obj As FileTransferItem)

    Private _Connection As NetworkConnectionItem

    Private _Mode As Mode
    Private _Buffer As New System.Collections.Specialized.StringCollection()
    Private _Res As DataCacheItem
    Private _ID As String
    Private _PartsCount As Integer = 0
    Private _RPart As Integer = 0
    Private _Type As DataCacheItem.DataType

    Private Shared _Count As Integer = 0

    Public Enum Mode As SByte
        Request = 1
        Send = 2
    End Enum

    Public Enum Command As SByte
        RequestFileInfo = 0 ' (tmpname) (resourcecategory) (resourcename)
        SendFileInfo = 1    ' (tmpname) (hash) (size)
        BeginSend = 2       ' (tmpname) (resourcecategory) (resourcename)
        AcceptBeginSend = 3 ' (tmpname) (pieces_count)        
        RequestPiece = 5    ' (tmpname) (piece_nr)
        SendPiece = 6       ' (tmpname) (hash) (size) (piece)
        RequestType = 7     ' (tmpname)
        SendType = 8        ' (tmpname) (type)
        Finished = 9        ' (tmpname)
    End Enum

    Sub New(ByRef Type As DataCacheItem.DataType, ByVal ResourceCategory As String, ByVal ResourceName As String, ByVal Mode As Mode, ByRef Connection As NetworkConnectionItem)
        Me._Mode = Mode
        Me._Res = New DataCacheItem(Type, ResourceCategory, ResourceName)
        Me._Connection = Connection
        Me._Type = Type
        _Count += 1
        Me._ID = _Count.ToString + "." + Connection.ConnectionID + "." + Connection.InfoList("remote.address").Replace(":", ".")
        Select Case Me._Mode
            Case Mode.Request
                Me.SendCMD(FileTransferItem.Command.RequestFileInfo, ResourceCategory, ResourceName)
        End Select
    End Sub

    Public ReadOnly Property Connection() As NetworkConnectionItem
        Get
            Return Me._Connection
        End Get
    End Property

    Public ReadOnly Property ResourceCategory() As String
        Get
            Return Me._Res.Category
        End Get
    End Property

    Public ReadOnly Property ResourceName() As String
        Get
            Return Me._Res.Name
        End Get
    End Property

    Public Property ID() As String
        Get
            Return Me._ID
        End Get
        Set(ByVal value As String)
            Me._ID = value
        End Set
    End Property

    Private Sub SendCMD(ByVal Command As Command, ByVal ParamArray params() As String)
        Me._Connection.SendCommand("/f " + Convert.ToSByte(Command).ToString + " " + Me._ID + " " + System.String.Join(" ", params))
    End Sub

    Private Function _Hash(ByVal text As String) As String
        If text Is Nothing Then text = ""
        Dim bytes() As Byte = System.Text.Encoding.Default.GetBytes(text)
        Dim rez As String = Convert.ToBase64String(System.Security.Cryptography.SHA1Cng.Create().ComputeHash(bytes), Base64FormattingOptions.None)
        Return rez.Substring(0, rez.Length - 1)
    End Function

    Public Sub Update(ByVal Command As Command, ByVal ParamArray params() As String)
        Select Case Me._Mode
            Case Mode.Send
                Select Case Command
                    Case FileTransferItem.Command.RequestFileInfo
                        Me.SendCMD(FileTransferItem.Command.SendFileInfo, Me._Res.Size, Me._Res.Hash)
                    Case FileTransferItem.Command.BeginSend
                        Me._Buffer = Me._Res.DataCollection
                        'MsgBox(Me._Res.DataCollection.ToString)
                        'Dim data() As String = Me._Res.ToString.Split(vbCrLf)
                        'Me._Buffer.Clear()
                        'For Each dataItem As String In data
                        '                        Me._Buffer.Add(dataItem)
                        '                       Next
                        Me.SendCMD(FileTransferItem.Command.AcceptBeginSend, Me._Buffer.Count)
                    Case FileTransferItem.Command.RequestPiece
                        Dim Index As Integer = System.Convert.ToInt32(params(0))
                        Dim piece As String = Me._Buffer.Item(Index)
                        Dim Hash As String = Me._Hash(piece)
                        Me.SendCMD(FileTransferItem.Command.SendPiece, Hash, piece.Length.ToString, piece)
                    Case FileTransferItem.Command.RequestType
                        Me.SendCMD(FileTransferItem.Command.SendType, Me._Res.Type)
                    Case FileTransferItem.Command.Finished
                        RaiseEvent Finished(Me)
                End Select
            Case Mode.Request
                Select Case Command
                    Case FileTransferItem.Command.SendFileInfo
                        If params(0) = "" Then params(0) = "0"
                        If Me._Res.Hash = params(1) And Me._Res.Size.ToString = params(0) Then
                            Me.SendCMD(FileTransferItem.Command.Finished)
                            RaiseEvent UpdateStatus(Me, 100)
                            RaiseEvent Done(Me, Me._Res)
                            Exit Sub
                        End If
                        Me.SendCMD(FileTransferItem.Command.BeginSend, Me._Res.Category, Me._Res.Name)
                    Case FileTransferItem.Command.AcceptBeginSend
                        Me._PartsCount = Convert.ToInt32(params(0))
                        If Me._PartsCount < 1 Then
                            Me.SendCMD(FileTransferItem.Command.Finished)
                            RaiseEvent UpdateStatus(Me, 100)
                            RaiseEvent Done(Me, Me._Res)
                            Exit Select
                        End If
                        Me._RPart = 0
                        Me.SendCMD(FileTransferItem.Command.RequestPiece, Me._RPart.ToString)
                        Me._Buffer.Clear()
                        RaiseEvent UpdateStatus(Me, 0)
                    Case FileTransferItem.Command.SendPiece
                        Dim Hash1 As String = params(0)
                        Dim Size As Long = System.Convert.ToInt32(params(1))
                        Dim Data As String = params(2)
                        Dim Hash2 As String = Me._Hash(Data)
                        If Hash1 = Hash2 And Size = Data.Length Then
                            RaiseEvent UpdateStatus(Me, 100 / Me._PartsCount * (Me._RPart + 1))
                            Me._Buffer.Add(Data)
                            If Me._RPart = Me._PartsCount - 1 Then
                                RaiseEvent UpdateStatus(Me, 100)
                                Me.SendCMD(FileTransferItem.Command.RequestType)                                
                                Exit Sub
                            Else
                                Me._RPart += 1
                            End If
                        End If
                        'MsgBox(Data)
                        Me.SendCMD(FileTransferItem.Command.RequestPiece, Me._RPart)
                    Case FileTransferItem.Command.SendType
                        Me.SendCMD(FileTransferItem.Command.Finished)
                        Me._Res.Type = params(0)
                        Me._Res.DataCollection = Me._Buffer
                        RaiseEvent Done(Me, Me._Res)
                End Select
        End Select
    End Sub

End Class
