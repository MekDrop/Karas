Public Class frmChat

    Public WithEvents Connections As New Karas.NetworkConnectionCollection()

    Private tmpThread As New System.Threading.Thread(AddressOf Startup)

    'Private Delegate Sub dlDownloadBoxFx(ByVal Grow As Boolean)
    Private DownloadBox As Karas.ucDownloadBox
    Private FileTransfers As Karas.FileTransferCollection

    Public CurrentServerConnection As NetworkConnectionItem

    'Public WithEvents Server As New Karas.NetworkConnection()
    Private Sub Startup()

        Me.ucChat.DebugMode = My.Settings.DebugMode
        Me.ucChat.FocusControl = Me.txtInput

        Me.Connections.CreateServerOnPort(My.Settings.DefaultPort)

        'Me.Server.ToBeServer(5387)
        Dim Rand As New System.Random()

        If My.Settings.Nodes.Count > 0 And My.Settings.ConnectOnStartup Then
            Me.Connections.CreateNewConnectionTo(My.Settings.Nodes(Rand.Next(0, My.Settings.Nodes.Count - 1)))
        End If

        Me.UpdateMySelfNick()
    End Sub

    Public Sub UpdateSettings()
        Me.ucChat.DebugMode = My.Settings.DebugMode
    End Sub

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.tmpThread.Start()
        'frmMain.ucTabForms.Add(Karas.frmFileTransfers, Karas.ucTabForms.TabWorkMode.Normal, BorderStyle.None, False)
        Me.DownloadBox = frmFileTransfers.ucDownloadBox ' Me.ucDownloadBox
        Me.FileTransfers = Me.DownloadBox.FileTransfers        
    End Sub

    Private Sub txtInput_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInput.KeyUp
        If e.Alt Or e.Control Or e.Shift Then Exit Sub
        If e.KeyCode = Keys.Enter Then
            Me.ExecCommand(Me.txtInput.Text)
            Me.txtInput.AutoCompleteCustomSource.Add(Me.txtInput.Text)
            Me.txtInput.Text = ""
        End If
    End Sub

    Public Enum SystemCommands As Byte
        NOP = 0
        RequestVersion = 1
        SendVersion = 2
        RequestRemoteAddress = 3
        SendRemoteAddress = 4
        CheckIfIsKarasGame = 5
        YesItsKarasGame = 6
        RequestUserName = 7
        SendUserName = 8
        RequestUserList = 9
        AddUser = 10
        Disconnect = 11
        DataTransfer = 12

        RequestGame = 13
        CancelRequestGame = 14
        AcceptRequestGame = 15
        RequestMapResourceName = 16
        SendMapResourceName = 17
        NotValidMapForGame = 18
        GotMapForGame = 19

        GameCommand = 20
    End Enum

    Delegate Sub wtUpdateUserInUserList(ByRef Connection As NetworkConnectionItem, ByVal Name As String)

    Private Sub UpdateUserInUserList(ByRef Connection As NetworkConnectionItem, ByVal Name As String)
        If Connection Is Nothing Then Exit Sub

        If Me.lvUsers.InvokeRequired Then
            Me.lvUsers.Invoke(New wtUpdateUserInUserList(AddressOf UpdateUserInUserList), Connection, Name)
            Exit Sub
        End If

        If Me.lvUsers.Items.ContainsKey(Connection.ConnectionID) Then
            If Connection.InfoList("user.name") = Name And Connection.InfoList("update.ul") = "1" Then Exit Sub
            Me.lvUsers.Items.RemoveByKey(Connection.ConnectionID)
        End If
        '+ "|" + Connection.ConnectionData, 0
        Me.lvUsers.Items.Add(Connection.ConnectionID, Name + "|" + Connection.ConnectionData, 0)
        Connection.InfoList("user.name") = Name
        Connection.InfoList("update.ul") = "1"
        Me.lvUsers.Update()
    End Sub

    Private Sub ExecCommand(ByVal CMD As String, Optional ByRef Connection As NetworkConnectionItem = Nothing)
        If CMD.StartsWith("/") Then
            Dim n As Integer = CMD.IndexOf(" ")
            Dim Command As String
            If n > -1 Then
                Command = CMD.Substring(1, n - 1).ToLower
            Else
                Command = CMD
            End If
            Dim Argument As String = ""
            If (Command.Length + 2) <= CMD.Length Then
                Argument = CMD.Substring(Command.Length + 2).Trim()
            End If
            Select Case Command
                Case "node", "n"
                    Dim Url As Uri
                    Try
                        Url = New Uri("karas://" + Argument)
                    Catch ex As Exception
                        Me.ucChat.WriteAsComputer("Klaida: blogai nurodytas adresas.")
                        Exit Sub
                    End Try
                    If Url.Port < 0 Then
                        Argument += ":" + My.Settings.DefaultPort.ToString
                    End If
                    Me.Connections.CreateNewConnectionTo(Argument)
                Case "s", "system", "sys"
                    If Connection Is Nothing Then Exit Sub
                    Dim args() As Object = Argument.Split(" ")
                    Dim ccmd As SystemCommands = System.Convert.ToByte(args(0))
                    Me.ucChat.WriteAsDebugData(CMD.ToString)
                    Select Case ccmd
                        Case SystemCommands.SendVersion
                            Connection.InfoList("app.name") = args(1).ToString
                            Connection.InfoList("app.version") = args(2).ToString
                            Me.ucChat.WriteAsDebugData(Connection.InfoList("app.name") + " " + Connection.InfoList("app.version"))
                        Case SystemCommands.RequestVersion
                            SendSystemCommand(Connection, SystemCommands.SendVersion, My.Application.Info.Version.ToString)
                        Case SystemCommands.SendRemoteAddress
                            Connection.InfoList("remote.address") = args(1).ToString
                            Me.ucChat.WriteAsDebugData(args(1).ToString)
                        Case SystemCommands.RequestRemoteAddress
                            SendSystemCommand(Connection, SystemCommands.SendRemoteAddress, Me.Connections.Item(0).ConnectionData.ToString)
                        Case SystemCommands.CheckIfIsKarasGame
                            UpdateUserInUserList(Connection, args(1).ToString)
                            SendSystemCommand(Connection, SystemCommands.YesItsKarasGame, My.Settings.UserPlayerNickName)
                        Case SystemCommands.RequestUserName
                            SendSystemCommand(Connection, SystemCommands.SendUserName, My.Settings.UserPlayerNickName)
                        Case SystemCommands.SendUserName
                            UpdateUserInUserList(Connection, args(1).ToString)
                            Me.ucChat.WriteAsDebugData(Connection.InfoList("user.name"))
                        Case SystemCommands.YesItsKarasGame
                            'Connection.InfoList("user.name") = args(1).ToString
                            UpdateUserInUserList(Connection, args(1).ToString)
                            Me.ucChat.WriteAsDebugData("User: " + Connection.InfoList("user.name"))
                            SendSystemCommand(Connection, SystemCommands.SendRemoteAddress, Me.Connections.Item(0).ConnectionData.ToString)
                            SendSystemCommand(Connection, SystemCommands.RequestRemoteAddress)
                            SendSystemCommand(Connection, SystemCommands.RequestUserList)
                            For n = 1 To Me.Connections.Count - 1
                                If Connection.InfoList("remote.address") = Me.Connections.Item(n).InfoList("remote.address") Then Continue For
                                SendSystemCommand(Connection, SystemCommands.AddUser, Me.Connections.Item(n).InfoList("remote.address"))
                            Next
                            'Me.ucChat.WriteAsComputer("Debug: senddata")
                            'SendSystemCommand(Connection, SystemCommands.SendUserName, My.Settings.PlayerNickName)
                            'SendSystemCommand(Connection, SystemCommands.YesItsKarasGame, My.Settings.PlayerNickName)
                            'SendSystemCommand(Connection, SystemCommands.SendRemoteAddress, Me.Connections.Item(0).ConnectionData(NetworkConnection.ConnectionDataType.Both, True).ToString)
                        Case SystemCommands.RequestUserList
                            For n = 1 To Me.Connections.Count - 1
                                If Connection.InfoList("remote.address") = Me.Connections.Item(n).InfoList("remote.address") Then Continue For
                                SendSystemCommand(Connection, SystemCommands.AddUser, Me.Connections.Item(n).InfoList("remote.address"))
                            Next
                        Case SystemCommands.AddUser
                            Dim address As String = args(0).ToString
                            'MsgBox(address)
                            For n = 1 To Me.Connections.Count - 1
                                If Me.Connections.Item(n).InfoList("remote.address") = address Then Exit Sub
                            Next
                            Me.Connections.CreateNewConnectionTo(address)
                        Case SystemCommands.Disconnect
                            Connection.Disconnect()
                        Case SystemCommands.RequestGame
                            If KarasMap.IsValidMap(My.Settings.UserMap) Then
                                Dim Text As String = String.Format("{0} siūlo pažaisti {1} žaidimą. Ar sutinki?", Connection.InfoList("user.name").ToString, args(1).ToString)
                                'If System.Windows.Forms.MessageBox.Show(Text, "Klausimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Me.frmPlayGame = New frmPlayGame
                                Me.SendSystemCommand(Connection, SystemCommands.AcceptRequestGame, args(1))
                                'Else
                                '   Me.SendSystemCommand(Connection, SystemCommands.CancelRequestGame, args(1))
                                'End If
                            Else
                                Dim Text As String = String.Format("{0} siūlė pažaisti {1} žaidimą, tačiau tu nesi pasirinkęs žemėlapio, todėl žaidimas buvo atšauktas. :(", Connection.InfoList("user.name").ToString, args(1).ToString)
                                Me.SendSystemCommand(Connection, SystemCommands.NotValidMapForGame, args(1))
                                MessageBox.Show(Text, "Pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If
                        Case SystemCommands.NotValidMapForGame
                            Dim Text As String = String.Format("{0}, deja, negalėjo priimti išūkio {1} žaidimui, nes jis nepasirinkęs vis dar žaidybinio žemėlapio :(", Connection.InfoList("user.name").ToString, args(1).ToString)
                            System.Windows.Forms.MessageBox.Show(Text, "Atsakymas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Case SystemCommands.CancelRequestGame
                            Dim Text As String = String.Format("{0} nesutiko žaisti {1} žaidimą.", Connection.InfoList("user.name").ToString, args(1).ToString)
                            System.Windows.Forms.MessageBox.Show(Text, "Atsakymas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Case SystemCommands.AcceptRequestGame
                            Dim MapName As String = System.Web.HttpUtility.UrlEncode(My.Settings.UserPlayerNickName + "|" + (New IO.FileInfo(My.Settings.UserMap)).Name)
                            Dim k As New DataCacheItem(My.Settings.UserMap, "Maps", MapName)
                            k.Save()
                            Me.SendSystemCommand(Connection, SystemCommands.RequestMapResourceName, MapName)
                        Case SystemCommands.RequestMapResourceName
                            Dim MapName As String = System.Web.HttpUtility.UrlEncode(My.Settings.UserPlayerNickName + "|" + (New IO.FileInfo(My.Settings.UserMap)).Name)
                            Dim k As New DataCacheItem(My.Settings.UserMap, "Maps", MapName)
                            k.Save()
                            Me.SendSystemCommand(Connection, SystemCommands.SendMapResourceName, MapName)
                            Dim RequestGroup As FileTransferRequestGroup = Me.DownloadBox.CreateRequestGroup(AddressOf PlayThatGame)
                            Me.GameConnection = Connection
                            Me.GotOtherPlayerMap = False
                            'RequestGroup.Add(DataCacheItem.DataType.Text, "Maps", args(1).ToString, Connection)
                            RequestGroup.Add(DataCacheItem.DataType.Text, "CurrentMaps", Connection.InfoList("user.name").ToString, Connection)
                            RequestGroup.Begin()
                        Case SystemCommands.SendMapResourceName
                            Dim RequestGroup As FileTransferRequestGroup = Me.DownloadBox.CreateRequestGroup(AddressOf PlayThatGame)
                            Me.GameConnection = Connection
                            Me.GotOtherPlayerMap = False
                            RequestGroup.Add(DataCacheItem.DataType.Text, "CurrentMaps", Connection.InfoList("user.name").ToString, Connection)
                            'RequestGroup.Add(DataCacheItem.DataType.Text, "Maps", args(1).ToString, Connection)
                            RequestGroup.Begin()
                            frmPlayGame.IsMyMove = True
                        Case SystemCommands.GotMapForGame
                            Me.GotOtherPlayerMap = True
                            frmPlayGame.WaitOtherPlayer = False
                        Case SystemCommands.GameCommand
                            Dim cmd2 As frmPlayGame.GameCommands = Convert.ToSByte(args(1))
                            Dim args2 As New Generic.List(Of Object)
                            For i As Integer = 2 To args.Count - 1
                                args2.Add(args(i))
                            Next
                            Me.frmPlayGame.ExecCommand(cmd2, args2.ToArray)
                    End Select
                Case "f", "file"
                    If Connection Is Nothing Then Exit Sub
                    Me.ucChat.WriteAsDebugData("/f " + Argument)
                    'Me.ucDownloadBox.FileTransfers.Update(Argument, Connection)
                    Me.FileTransfers.Update(Argument, Connection)
            End Select
        ElseIf Not Connection Is Nothing Then
            If CMD.Trim.Length > 0 Then
                Dim i As Integer = CMD.IndexOf(" ")
                If i < 1 Then Exit Sub
                Dim name As String = CMD.Substring(0, i).Trim
                Me.ucChat.WriteAsUser(name, CMD.Substring(i + 1).Trim)
                UpdateUserInUserList(Connection, name)
                'Connection.InfoList("user.name") = name
            End If
        Else
            If CMD.Trim.Length > 0 Then
                Me.ucChat.WriteAsUser(My.Settings.UserPlayerNickName, CMD)
                Me.Connections.SendCommand(My.Settings.UserPlayerNickName + " " + CMD)
            End If
        End If
    End Sub

    Private GameConnection As NetworkConnectionItem
    Private GotOtherPlayerMap As Boolean

    Private Delegate Sub dlgPTG2()
    Private frmPlayGame As frmPlayGame

    Private Sub PlayThatGame2()
        If Me.InvokeRequired Then
            Me.Invoke(New dlgPTG2(AddressOf PlayThatGame2))
            Exit Sub
        End If
        frmMain.ucTabForms.Add(Me.frmPlayGame)        
        Me.frmPlayGame.SetConnections(Me.CurrentServerConnection, Me.GameConnection)
        frmPlayGame.Draw()
        frmPlayGame.EndedGame = False
        'frmPlayGame.Init(Me.OtherPlayerMapData)
    End Sub

    Private Sub PlayThatGame(ByRef C As System.Collections.ObjectModel.Collection(Of DataCacheItem))
        Me.SendSystemCommand(Me.GameConnection, SystemCommands.GotMapForGame)
        frmPlayGame.WaitOtherPlayer = Not Me.GotOtherPlayerMap
        Dim pb As New PictureBox
        pb.Image = New Bitmap(400, 600)
        pb.BackgroundImage = New Bitmap(400, 600)
        frmPlayGame.Map(1) = New KarasMap(pb)
        frmPlayGame.Map(1).LoadFromDataCache(C(0))
        frmPlayGame.Map(1).VerticalFlip()
        frmPlayGame.Map(0) = New KarasMap(My.Settings.UserMap, New PictureBox())        
        Me.PlayThatGame2()
        ' frmPlayGame.Init(C(0), Me.Connections.FindByConnectionID(Me.lvUsers.SelectedItems(0).Name), Me.GameConnection)
    End Sub

    Public Sub SendSystemCommand(ByRef Connection As Karas.NetworkConnectionItem, ByVal Command As SystemCommands, Optional ByVal param As Object = Nothing)
        If param Is Nothing Then
            Connection.SendCommand("/s " + Convert.ToByte(Command).ToString)
        Else
            Connection.SendCommand("/s " + Convert.ToByte(Command).ToString + " " + param.ToString)
        End If
    End Sub

    Public Sub SendSystemCommand(ByRef UserName As String, ByVal Command As SystemCommands, Optional ByVal param As Object = Nothing)
        Dim Connection As NetworkConnectionItem = Me.Connections.FindByConnectionID(Me.lvUsers.SelectedItems(0).Name)
        Me.SendSystemCommand(Connection, Command, param)
    End Sub

    Private Sub SendSystemCommand(ByVal Command As SystemCommands, Optional ByVal param As Object = Nothing)
        If param Is Nothing Then
            Me.Connections.SendCommand("/s " + Convert.ToByte(Command).ToString)
        Else
            Me.Connections.SendCommand("/s " + Convert.ToByte(Command).ToString + " " + param.ToString)
        End If
    End Sub

    Private Sub Connection_AddressNotFound(ByRef Connection As NetworkConnectionItem, ByVal IP As String) Handles Connections.AddressNotFound
        Me.ucChat.WriteAsComputer("Adresas " + IP + " nerastas")
    End Sub

    Private Sub Connection_Connected(ByRef Connection As NetworkConnectionItem, ByVal IP As String) Handles Connections.Connected
        Me.ucChat.WriteAsComputer("Prisijungta.")
        Me.ucChat.WriteAsComputer("Iš viso " + (Me.Connections.Count - 1).ToString + " susijungimų.")

        If My.Settings.AutoAddGoodNodes Then
            If Not My.Settings.Nodes.Contains(IP) Then
                My.Settings.Nodes.Add(IP)
            End If
        End If
    End Sub

    Private Sub Connection_Connecting(ByRef Connection As NetworkConnectionItem, ByVal ConnectionData As String) Handles Connections.Connecting
        Me.ucChat.WriteAsComputer("Jungiamasi prie " + ConnectionData + "...")
    End Sub

    Private Sub Connection_Disconected(ByRef Connection As NetworkConnectionItem, ByVal ConnectionData As String) Handles Connections.Disconected
        If Me.lvUsers.InvokeRequired Then
            Me.lvUsers.Invoke(New wtUpdateUserInUserList(AddressOf Connection_Disconected), Connection, ConnectionData)
            Exit Sub
        End If
        If Me.lvUsers.Items.ContainsKey(Connection.ConnectionID) Then
            Me.lvUsers.Items.RemoveByKey(Connection.ConnectionID)
        End If
        Me.ucChat.WriteAsComputer("Atsijungta nuo " + ConnectionData + ".")
    End Sub

    Private Sub Connection_FailedConnection(ByRef Connection As NetworkConnectionItem, ByVal ConnectionData As String) Handles Connections.FailedConnection
        Me.ucChat.WriteAsComputer("Nepasisekė prisijungti.")
        If My.Settings.AutoRemoveBadNodes Then
            If My.Settings.Nodes.Contains(ConnectionData) Then
                My.Settings.Nodes.Remove(ConnectionData)
            End If
        End If
    End Sub

    Private Sub Connection_GotData(ByRef Connection As NetworkConnectionItem, ByVal Data As String) Handles Connections.GotData

        Dim Text() As String = Data.Split(vbCrLf)

        For Each T As String In Text
            T = T.Trim
            If T.Length < 1 Then Continue For
            ExecCommand(T, Connection)
        Next

    End Sub

    Private Sub Connections_IPUpdated(ByRef NetworkConnection As NetworkConnectionItem) Handles Connections.IPUpdated
        If NetworkConnection.ConnectionID = Me.Connections.Item(0).ConnectionID Then
            NetworkConnection.InfoList("update.ul") = ""
            Me.UpdateMySelfNick()
        End If
    End Sub

    Private Sub Connections_MessageNotSend(ByRef NetworkConnection As NetworkConnectionItem, ByVal Command As String) Handles Connections.MessageNotSend
        Me.ucChat.WriteAsComputer("Žinutė nenusiųsta.")
    End Sub

    Private Sub Connections_NewConnectionStarted(ByRef Connection As NetworkConnectionItem) Handles Connections.NewConnectionStarted
        Me.ucChat.WriteAsComputer("Prisijungta iš " + Connection.ConnectionData.ToString + ".")
        Me.Connections.Add(Connection)
        'Connection.Sleep(10)
        Me.SendSystemCommand(Connection, SystemCommands.CheckIfIsKarasGame, My.Settings.UserPlayerNickName)
    End Sub

    Private Sub Connection_ResolvedIPAddress(ByRef Connection As NetworkConnectionItem, ByVal Address As String, ByVal IP As String) Handles Connections.ResolvedIPAddress
        Me.ucChat.WriteAsComputer(Address + " = " + IP)
    End Sub

    Private Delegate Sub slgtxtInput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Private Sub txtInput_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInput.TextChanged
        If Me.txtInput.InvokeRequired Then
            Me.txtInput.Invoke(New slgtxtInput_TextChanged(AddressOf txtInput_TextChanged), sender, e)
            Exit Sub
        End If
        Try
            If txtInput.Lines.Count > 1 Then
                For Each line As String In txtInput.Lines
                    ExecCommand(line)
                Next
                If txtInput.Lines.Count > 0 Then
                    Me.txtInput.Text = txtInput.Lines(txtInput.Lines.Count - 1).Trim
                Else
                    Me.txtInput.Text = ""
                End If
            End If
        Catch ex As Exception

        End Try        
    End Sub

    Private Sub Connections_ServerCantStart(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Handles Connections.ServerCantStart
        Me.ucChat.WriteAsComputer("Serveris negali būti paleistas " + ConnectionData + ".")
    End Sub

    Private Sub Connections_ServerStarted(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Handles Connections.ServerStarted
        Me.ucChat.WriteAsComputer("Serveris paleistas " + ConnectionData + ".")
        'Me.UpdateMySelfNick()
    End Sub

    Private Sub Connections_ServerStoped(ByRef NetworkConnection As NetworkConnectionItem) Handles Connections.ServerStoped
        Me.Connections.CreateServerOnPort(My.Settings.DefaultPort)
    End Sub

    Private Sub frmChat_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If My.Settings.UserPlayerNickName.Trim.Length < 3 Then
            My.Settings.UserPlayerNickName = String.Format("Svečias[{0:T}]", System.DateTime.Now)
        End If
    End Sub

    Public Sub UpdateMySelfNick()
        Me.UpdateUserInUserList(Me.Connections.Item(0), My.Settings.UserPlayerNickName)
    End Sub

    Protected Overrides Sub Finalize()
        Me.SendSystemCommand(SystemCommands.Disconnect)
        MyBase.Finalize()
    End Sub

    Private Sub ShowUserInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowUserInfoToolStripMenuItem.Click
        If Me.lvUsers.SelectedItems(0).Name.ToString = "1" Then
            Me.ShowUserInfo(My.Settings.UserAvatar, My.Settings.UserPlayerNickName, My.Settings.UserBirthdayDate, My.Settings.UserEmail)
        Else
            Dim Connection As NetworkConnectionItem = Me.Connections.FindByConnectionID(Me.lvUsers.SelectedItems(0).Name)
            Dim RequestGroup As FileTransferRequestGroup = Me.DownloadBox.CreateRequestGroup(AddressOf ShowUserInfo2)
            Dim name As String = Connection.InfoList("user.name")
            RequestGroup.Add(DataCacheItem.DataType.Image, "Avatars", name, Connection)
            RequestGroup.Add(DataCacheItem.DataType.Text, "Names", name, Connection)
            RequestGroup.Add(DataCacheItem.DataType.DateTime, "birthdate", name, Connection)
            RequestGroup.Add(DataCacheItem.DataType.Text, "eMails", name, Connection)
            RequestGroup.Begin()
            'Me.ucDownloadBox.FileTransfers.Request(Of System.Drawing.Image)("Avatars", Connection.InfoList("user.name"), Connection)            
        End If
    End Sub

    Private Delegate Sub dlShowUserInfo(ByRef Avatar As System.Drawing.Image, ByVal NickName As String, ByVal BirthDate As System.DateTime, ByVal eMail As String)

    Private Sub ShowUserInfo2(ByRef Collection As System.Collections.ObjectModel.Collection(Of DataCacheItem))
        Me.Invoke(New dlShowUserInfo(AddressOf ShowUserInfo), Collection.Item(0).CGet(Of System.Drawing.Image), Collection.Item(1).CGet(Of System.String), Collection.Item(2).CGet(Of System.DateTime), Collection.Item(3).CGet(Of System.String))
        'ShowUserInfo()
    End Sub

    Public Sub ShowUserInfo(ByRef Avatar As System.Drawing.Image, ByVal NickName As String, ByVal BirthDate As System.DateTime, ByVal eMail As String)
        Dim Age As String = Math.Floor(((Now.Date.Year - BirthDate.Year) * 12 + (Now.Date.Month - BirthDate.Month)) / 12).ToString
        frmUserInfo.ShowDialog(Avatar, "Slapyvadis", NickName, "Amžius", Age, "Elektroninis paštas", eMail)
    End Sub

    Private Sub cmsUserList_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsUserList.Opening
        Dim ItemSelected As Boolean = True
        If Me.lvUsers.SelectedItems.Count = 0 Then
            ItemSelected = False
        ElseIf Not Me.lvUsers.SelectedItems(0).Focused Then
            ItemSelected = False
        End If
        If Not ItemSelected Then
            e.Cancel = True
        End If
        Me.tsmRequestGamePlay1.Enabled = Me.lvUsers.SelectedItems(0).Name.ToString <> "1" And KarasMap.IsValidMap(My.Settings.UserMap)
        'Me.ShowUserInfoToolStripMenuItem.Visible = ItemSelected
    End Sub

    Private Sub tsmRequestGamePlay1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsmRequestGamePlay1.Click
        'Dim Connection As NetworkConnectionItem = Me.Connections.FindByConnectionID(Me.lvUsers.SelectedItems(0).Name)        
        Me.frmPlayGame = New frmPlayGame
        Me.CurrentServerConnection = Me.Connections.FindByConnectionID(Me.lvUsers.SelectedItems(0).Name)
        Me.SendSystemCommand(Me.lvUsers.SelectedItems(0).Name, SystemCommands.RequestGame, "standart")
    End Sub

    Private Sub lvUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvUsers.SelectedIndexChanged

    End Sub
End Class