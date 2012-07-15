Imports System.Net.Sockets

Public Class NetworkConnectionItem
    Implements Karas.iNetworkConnection

    Private WithEvents Socket As System.Net.Sockets.Socket

    Public Event Connecting(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.Connecting
    Public Event Connected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.Connected
    Public Event FailedConnection(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.FailedConnection
    Public Event ResolvedIPAddress(ByRef NetworkConnection As NetworkConnectionItem, ByVal Address As String, ByVal IP As String) Implements iNetworkConnection.ResolvedIPAddress
    Public Event Disconected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.Disconected
    Public Event GotData(ByRef NetworkConnection As NetworkConnectionItem, ByVal Data As String) Implements iNetworkConnection.GotData
    Public Event AddressNotFound(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.AddressNotFound
    Public Event MessageNotSend(ByRef NetworkConnection As NetworkConnectionItem, ByVal Command As String) Implements iNetworkConnection.MessageNotSend

    Public Event ServerStarted(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.ServerStarted
    Public Event ServerCantStart(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String) Implements iNetworkConnection.ServerCantStart
    Public Event NewConnectionStarted(ByRef Connection As NetworkConnectionItem) Implements iNetworkConnection.NewConnectionStarted
    Public Event ServerStoped(ByRef NetworkConnection As NetworkConnectionItem) Implements iNetworkConnection.ServerStoped

    Public Event IPUpdated(ByRef NetworkConnection As NetworkConnectionItem) Implements iNetworkConnection.IPUpdated

    Private Shared Shadows _ConnectionIDCount As Integer = 0
    Private _ConnectionID As String

    Private ThreadListenAndAcceptIfNeeded As New System.Threading.Thread(AddressOf AcceptingAndListingIfNeeded)

    Private _IsServer As Boolean = False

    Private _InfoList As New System.Collections.Specialized.NameValueCollection()

    Private DelaytedExecution As New DelaytedExecution

    Public Const BUFFER_SIZE As Integer = 1024 * 10

    Private Enum DelaytedExecutionCommand As SByte
        SendCommand = 0
        ToBeServer = 1
        Connect = 2
        Disconnect = 3
        StopServer = 4
        Sleep = 5
    End Enum

    Private Sub UpdateWithTrueIP(ByVal port As Integer)
        Dim hostname As String = System.Net.Dns.GetHostName()
        Dim addreses() As Net.IPAddress = System.Net.Dns.GetHostAddresses(hostname)
        For Each address As Net.IPAddress In addreses
            If address.IsIPv6LinkLocal Then Continue For
            If address.IsIPv6Multicast Then Continue For
            If address.IsIPv6SiteLocal Then Continue For
            If My.Computer.Network.Ping(address.ToString) Then
                Dim n As New Net.IPEndPoint(address, port)
                Me.UpdateConnectionData(n, Nothing)
                Exit Sub
            End If
        Next
        Me.UpdateConnectionData(Me.Socket.LocalEndPoint, Nothing)
    End Sub

    Private Sub DoActions(ByVal ParamArray params() As Object)
        Dim iCommand As NetworkConnectionItem.DelaytedExecutionCommand = params(0)
        Select Case iCommand
            Case DelaytedExecutionCommand.SendCommand
                Try
                    Me.Socket.Send(System.Text.Encoding.UTF32.GetBytes(params(1).ToString + vbCrLf))
                Catch ex As Exception
                    RaiseEvent MessageNotSend(Me, params(1).ToString)
                    RaiseEvent Disconected(Me, Me.ConnectionData)
                End Try
            Case DelaytedExecutionCommand.ToBeServer
                Dim serverIP As Net.IPAddress = System.Net.IPAddress.Any
                Dim port As Integer = params(1)
                Dim fport As Integer = params(1)
                Dim trueIP As Net.IPEndPoint
                Dim TryNextIfThisPortFails As Boolean = params(2)
                Dim WasError As Boolean = False
                While True
                    Try
                        Me.Socket = New System.Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                        trueIP = New Net.IPEndPoint(serverIP, port)
                        Me.Socket.Bind(trueIP)
                        Me.Socket.Listen(BUFFER_SIZE)
                        Me._IsServer = True                                                
                        ThreadListenAndAcceptIfNeeded.Start()
                        Me.UpdateWithTrueIP(port)
                        RaiseEvent ServerStarted(Me, Me.ConnectionData)
                        TryNextIfThisPortFails = True
                        'n=Me.Socket.LocalEndPoint                        
                        Exit While
                    Catch ex As Exception
                        If TryNextIfThisPortFails Then
                            port += 1
                            If port - fport > 1000 Then
                                WasError = True
                                Exit While
                            End If
                        Else
                            WasError = True
                            Exit While
                        End If
                    End Try
                End While
                If WasError Then
                    RaiseEvent ServerCantStart(Me, serverIP.ToString + ":" + port.ToString)
                End If
            Case DelaytedExecutionCommand.Connect
                Dim IP As String = params(1).ToString

                If Not Me.Socket Is Nothing Then
                    If Me.Socket.Connected Then Me.Disconnect()
                End If

                RaiseEvent Connecting(Me, IP)
                Dim url As System.Uri
                Try
                    url = New System.Uri("musis://" + IP)
                Catch ex As Exception
                    RaiseEvent FailedConnection(Me, IP)
                    Exit Sub
                End Try

                Dim Adress As System.Net.IPAddress
                Try
                    Adress = System.Net.IPAddress.Parse(url.Host)
                Catch ex As Exception
                    Try
                        Adress = System.Net.Dns.GetHostEntry(url.Host).AddressList(0)
                        RaiseEvent ResolvedIPAddress(Me, url.Host, Adress.ToString())
                    Catch ex2 As Exception
                        RaiseEvent AddressNotFound(Me, url.Host)
                        Exit Sub
                    End Try
                End Try

                Me.Socket = New System.Net.Sockets.Socket(Adress.AddressFamily, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)

                Try
                    Me.Socket.Connect(Adress, url.Port)
                Catch ex As Exception
                    RaiseEvent FailedConnection(Me, IP)
                    Exit Sub
                End Try

                If Me.Socket.Connected Then
                    'Me.IsThereListeningConnection = True
                    'Me.ThreadListenConnection.Start()
                    Me.UpdateConnectionData(Me.Socket.LocalEndPoint, Me.Socket.RemoteEndPoint)
                    ThreadListenAndAcceptIfNeeded.Start()
                    RaiseEvent Connected(Me, IP)
                    Exit Sub
                End If

                RaiseEvent FailedConnection(Me, IP)
            Case DelaytedExecutionCommand.Disconnect
                Me.ThreadListenAndAcceptIfNeeded.Abort()
                If Me.Socket.Connected Then
                    Me.Socket.Shutdown(Net.Sockets.SocketShutdown.Both)
                    Me.UpdateConnectionData(Nothing, Nothing)
                    Me.Socket.Disconnect(False)
                    Me.Socket.Close()
                    RaiseEvent Disconected(Me, Me.ConnectionData)
                End If
            Case DelaytedExecutionCommand.StopServer
                Me.UpdateConnectionData(Me.Socket.LocalEndPoint, Me.Socket.RemoteEndPoint)
                Dim tmpUp As Object = Me.ConnectionData(ConnectionDataType.Port)
                'Me.IsThereListeningConnection = False
                ThreadListenAndAcceptIfNeeded.Abort()
                Me._IsServer = False
                RaiseEvent ServerStoped(Me)
            Case DelaytedExecutionCommand.Sleep
                Dim Dtime As Integer = DateTime.Now.Millisecond
                Dim CTime As Integer = Dtime + Convert.ToInt32(params(1))
                Do
                    Dtime = DateTime.Now.Millisecond
                    My.Application.DoEvents()
                Loop Until Math.Abs(Dtime - CTime) < 0
        End Select
    End Sub

    Public Sub Sleep(ByVal Interval As Integer)
        Me.AddAction(DelaytedExecutionCommand.Sleep, Interval)
    End Sub

    Private Sub AddAction(ByVal Command As DelaytedExecutionCommand, Optional ByRef Param As Object = Nothing)
        'Me.DelaytedExecutionQueue.Enqueue(New DelaytedExecutionItem(Command, Param))
        Me.DelaytedExecution.Add(AddressOf DoActions, Command, Param)
    End Sub

    Public Property InfoList(ByVal Name As String) As String
        Get
            Return Me._InfoList(Name)
        End Get
        Set(ByVal value As String)
            Me._InfoList(Name) = value
        End Set
    End Property

    Public ReadOnly Property IsServer() As Boolean
        Get
            Return _IsServer
        End Get
    End Property

    Sub ToBeServer(ByVal Port As Integer, Optional ByRef TryNextIfThisPortFails As Boolean = True)
        Me.DelaytedExecution.Add(AddressOf Me.DoActions, DelaytedExecutionCommand.ToBeServer, Port, TryNextIfThisPortFails)
    End Sub

    Sub Connect(ByVal IP As String)
        AddAction(DelaytedExecutionCommand.Connect, IP)
    End Sub

    Sub Disconnect()
        AddAction(DelaytedExecutionCommand.Disconnect)
    End Sub

    Public Sub SendCommand(ByVal Text As String)
        If Me._IsServer Then Exit Sub
        Me.AddAction(DelaytedExecutionCommand.SendCommand, Text)
    End Sub

    Public Enum ConnectionDataType As SByte
        Address = 0
        Port = 1
        Both = 2
    End Enum

    Private _IP As String = ""
    Private _Port As Integer = -1

    Private Sub UpdateConnectionData(ByVal LocalEndPoint As Net.IPEndPoint, ByVal RemoteEndPoint As Net.IPEndPoint)
        With LocalEndPoint
            Me.InfoList("local.ip") = .Address.ToString
            Me.InfoList("local.port") = .Port.ToString
        End With
        If Me._IsServer = False Then
            With RemoteEndPoint
                Me.InfoList("remote.ip") = .Address.ToString
                Me.InfoList("remote.port") = .Port.ToString
            End With
        End If
        If Me._IsServer Then
            _IP = Me.InfoList("local.ip")
            _Port = Me.InfoList("local.port")
        Else
            _IP = Me.InfoList("remote.ip")
            _Port = Me.InfoList("remote.port")
        End If
        RaiseEvent IPUpdated(Me)
    End Sub

    Public ReadOnly Property ConnectionData(Optional ByVal DataType As ConnectionDataType = ConnectionDataType.Both) As Object
        Get
            Select Case DataType
                Case ConnectionDataType.Address
                    Return _IP
                Case ConnectionDataType.Port
                    Return _Port
                Case Else
                    Return String.Format("{0}:{1}", _IP, _Port)
            End Select
        End Get
    End Property

    Public Sub SetSocket(ByRef Socket As Net.Sockets.Socket)
        Me.Socket = Socket
        Me.UpdateConnectionData(Me.Socket.LocalEndPoint, Me.Socket.RemoteEndPoint)
        Dim tmpUp As Object = Me.ConnectionData(ConnectionDataType.Port)
        ThreadListenAndAcceptIfNeeded.Start()
    End Sub

    Sub StopServer()
        Me.AddAction(DelaytedExecutionCommand.Disconnect)
        Me.AddAction(DelaytedExecutionCommand.StopServer)
    End Sub

    Private Sub AcceptingAndListingIfNeeded()
        Dim clientSocket As Net.Sockets.Socket
        Dim temp As String
        Dim buffer(BUFFER_SIZE) As Byte
        Dim bytes As Integer
        Dim ErrCount As Integer = 0
        Do
            System.Threading.Thread.Sleep(10)
            If Me._IsServer Then
                clientSocket = Me.Socket.Accept()
                If clientSocket.Connected Then
                    Dim NewConnection As New NetworkConnectionItem()
                    NewConnection.SetSocket(clientSocket)
                    RaiseEvent NewConnectionStarted(NewConnection)
                End If
            Else
                If Not Me.Socket.Connected Then Exit Do
            End If
            If Me.Socket.Available > 0 Then
                Array.Clear(buffer, 0, buffer.Length)
                temp = ""
                While (True)
                    Array.Clear(buffer, 0, buffer.Length)
                    bytes = Socket.Receive(buffer, buffer.Length, 0)
                    temp += System.Text.Encoding.UTF32.GetString(buffer, 0, bytes)
                    If (bytes < buffer.Length) Then Exit While
                    My.Application.DoEvents()
                End While
                RaiseEvent GotData(Me, temp)
            Else
                If Me.Socket.Poll(100, System.Net.Sockets.SelectMode.SelectRead) Then
                    ErrCount += 1
                Else
                    ErrCount = 0
                End If
                If ErrCount > 100 Then Exit Do
            End If
        Loop
        RaiseEvent Disconected(Me, Me.ConnectionData)
    End Sub

    Private Sub AcceptingConnections()
        Dim clientSocket As Net.Sockets.Socket

        Do While Me._IsServer
            clientSocket = Me.Socket.Accept()
            If clientSocket.Connected Then
                Dim NewConnection As New NetworkConnectionItem()
                NewConnection.SetSocket(clientSocket)
                RaiseEvent NewConnectionStarted(NewConnection)
            Else
                My.Application.DoEvents()
            End If
        Loop
    End Sub

    Public ReadOnly Property Alive() As Boolean
        Get
            Return Me.Socket.Connected
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()
        Me.ThreadListenAndAcceptIfNeeded.IsBackground = True
        _ConnectionIDCount += 1
        Me._ConnectionID = Conversion.Hex(_ConnectionIDCount)
    End Sub

    Public ReadOnly Property ConnectionID() As String
        Get
            Return Me._ConnectionID
        End Get
    End Property

End Class
