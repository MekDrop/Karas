Public Class NetworkConnectionCollection
    Implements Karas.iNetworkConnection


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

    Private NetworkConnections As New System.Collections.ObjectModel.Collection(Of NetworkConnectionItem)

    '   Private ThreadDelaytedExecution As New System.Threading.Thread(AddressOf DelaytedExecution)
    '    Private DelaytedExecutionQueue As New System.Collections.Queue()

    Private Enum DelaytedExecutionCommand As Byte
        SendCommand = 0
        Disconnect = 1
        RemoveFromCollectionList = 2
    End Enum

    Private DelaytedExecution As New DelaytedExecution()


    Private Sub AddToDelaytedQueue(ByRef NetworkConnection As NetworkConnectionItem, ByVal Command As DelaytedExecutionCommand, Optional ByRef Param As Object = Nothing)
        Me.DelaytedExecution.Add(AddressOf _DelaytedExecution, Command, NetworkConnection, Param)
    End Sub

    Private Sub MassToDo(ByVal Command As DelaytedExecutionCommand, Optional ByRef Param As Object = Nothing)
        For Each Connection As Karas.NetworkConnectionItem In Me.NetworkConnections
            Me.AddToDelaytedQueue(Connection, Command, Param)
        Next
    End Sub

    Private Sub _DelaytedExecution(ByVal ParamArray params() As Object)
        Dim command As DelaytedExecutionCommand = params(0)
        Select Case command
            Case DelaytedExecutionCommand.SendCommand
                DirectCast(params(1), NetworkConnectionItem).SendCommand(params(2).ToString)
            Case DelaytedExecutionCommand.Disconnect
                DirectCast(params(1), NetworkConnectionItem).Disconnect()
            Case DelaytedExecutionCommand.RemoveFromCollectionList
                DirectCast(params(1), NetworkConnectionItem).Disconnect()
                Me.NetworkConnections.Remove(DirectCast(params(1), NetworkConnectionItem))
        End Select
    End Sub

    Public Sub SendCommand(ByVal Command As String)
        Me.MassToDo(DelaytedExecutionCommand.SendCommand, Command)
    End Sub

    Public Function CreateNewConnectionTo(ByVal Address As String) As NetworkConnectionItem

        For Each Con As NetworkConnectionItem In Me.NetworkConnections
            If Con.ConnectionData = Address Then
                Return Nothing
            End If
        Next

        Dim Connection As NetworkConnectionItem = Me.Add()
        Connection.Connect(Address)
        Return Connection

    End Function

    Public Function CreateServerOnPort(ByVal Port As Integer) As NetworkConnectionItem
        Dim Connection As NetworkConnectionItem = Me.Add()
        Connection.ToBeServer(Port)
        Return Connection
    End Function

    Public Function Add() As NetworkConnectionItem
        Return Me.Add(New NetworkConnectionItem())
    End Function

    Sub New()
        'Me.ThreadDelaytedExecution.IsBackground = True
        'Me.DelaytedExecutionIsRunning = True
        'Me.ThreadDelaytedExecution.Start()
    End Sub

    Public Function Add(ByRef NetworkConnection As NetworkConnectionItem) As NetworkConnectionItem
        Me.NetworkConnections.Add(NetworkConnection)

        Dim I As Integer = Me.NetworkConnections.IndexOf(NetworkConnection)

        AddHandler Me.NetworkConnections(I).GotData, AddressOf Me._GotData
        AddHandler Me.NetworkConnections(I).FailedConnection, AddressOf Me._FailedConnection
        AddHandler Me.NetworkConnections(I).AddressNotFound, AddressOf Me._AddressNotFound
        AddHandler Me.NetworkConnections(I).Connected, AddressOf Me._Connected
        AddHandler Me.NetworkConnections(I).Connecting, AddressOf Me._Connecting
        AddHandler Me.NetworkConnections(I).Disconected, AddressOf Me._Disconected
        AddHandler Me.NetworkConnections(I).NewConnectionStarted, AddressOf Me._NewConnectionStarted
        AddHandler Me.NetworkConnections(I).ResolvedIPAddress, AddressOf Me._ResolvedIPAddress
        AddHandler Me.NetworkConnections(I).ServerCantStart, AddressOf Me._ServerCantStart
        AddHandler Me.NetworkConnections(I).ServerStarted, AddressOf Me._ServerStarted
        AddHandler Me.NetworkConnections(I).MessageNotSend, AddressOf Me._MessageNotSend
        AddHandler Me.NetworkConnections(I).ServerStoped, AddressOf Me._ServerStoped
        AddHandler Me.NetworkConnections(I).IPUpdated, AddressOf Me._IPUpdated

        Return Me.NetworkConnections(I)
    End Function

    Public Property Item(ByVal Index As Integer) As NetworkConnectionItem
        Get
            On Error Resume Next
            Return Me.NetworkConnections.Item(Index)
        End Get
        Set(ByVal value As NetworkConnectionItem)
            Me.NetworkConnections.Item(Index) = value
        End Set
    End Property

    Private Sub _ServerStoped(ByRef NetworkConnection As NetworkConnectionItem)
        RaiseEvent ServerStoped(NetworkConnection)
        Me.Remove(NetworkConnection)
    End Sub

    Private Sub _IPUpdated(ByRef NetworkConnection As NetworkConnectionItem)
        RaiseEvent IPUpdated(NetworkConnection)        
    End Sub

    Private Sub _MessageNotSend(ByRef NetworkConnection As NetworkConnectionItem, ByVal Command As String)
        RaiseEvent MessageNotSend(NetworkConnection, Command)
    End Sub

    Public ReadOnly Property Count() As Integer
        Get
            Return Me.NetworkConnections.Count
        End Get
    End Property

    Public Sub DisconnectAll()
        Me.MassToDo(DelaytedExecutionCommand.Disconnect)
    End Sub

    Public Sub Remove(ByRef NetworkConnection As NetworkConnectionItem)

        If NetworkConnection.Alive Then
            Me.AddToDelaytedQueue(NetworkConnection, DelaytedExecutionCommand.Disconnect)
            'Me.DelaytedExecution.Add(AddressOf _DelaytedExecution, DelaytedExecutionCommand.Disconnect, NetworkConnection)
            'NetworkConnection.Disconnect()
        End If

        Me.AddToDelaytedQueue(NetworkConnection, DelaytedExecutionCommand.RemoveFromCollectionList)
        'Return Me.NetworkConnections.Remove(NetworkConnection)

    End Sub

    Private Sub _GotData(ByRef NetworkConnection As NetworkConnectionItem, ByVal Text As String)
        RaiseEvent GotData(NetworkConnection, Text)
    End Sub

    Private Sub _Connecting(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent Connecting(NetworkConnection, ConnectionData)
    End Sub

    Private Sub _Connected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent Connected(NetworkConnection, ConnectionData)
    End Sub

    Private Sub _FailedConnection(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent FailedConnection(NetworkConnection, ConnectionData)
        Me.NetworkConnections.Remove(NetworkConnection)
    End Sub

    Private Sub _ResolvedIPAddress(ByRef NetworkConnection As NetworkConnectionItem, ByVal Address As String, ByVal IP As String)
        RaiseEvent ResolvedIPAddress(NetworkConnection, Address, IP)
    End Sub

    Private Sub _Disconected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent Disconected(NetworkConnection, ConnectionData)
        Me.NetworkConnections.Remove(NetworkConnection)
    End Sub

    Private Sub _AddressNotFound(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent AddressNotFound(NetworkConnection, ConnectionData)
        Me.NetworkConnections.Remove(NetworkConnection)
    End Sub

    Private Sub _ServerStarted(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent ServerStarted(NetworkConnection, ConnectionData)
    End Sub

    Private Sub _ServerCantStart(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
        RaiseEvent ServerStarted(NetworkConnection, ConnectionData)
        Me.NetworkConnections.Remove(NetworkConnection)
    End Sub

    Private Sub _NewConnectionStarted(ByRef Connection As NetworkConnectionItem)
        RaiseEvent NewConnectionStarted(Connection)
    End Sub

    Protected Overrides Sub Finalize()
        'Me.ThreadDelaytedExecution.Abort()
        MyBase.Finalize()
    End Sub

    Public Function FindByConnectionID(ByVal ConnectionID As String) As NetworkConnectionItem
        For Each Connection As NetworkConnectionItem In NetworkConnections
            If Connection.ConnectionID = ConnectionID Then Return Connection
        Next
        Return Nothing
    End Function


End Class
