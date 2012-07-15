Public Interface iNetworkConnection

    Event Connecting(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event Connected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event FailedConnection(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event ResolvedIPAddress(ByRef NetworkConnection As NetworkConnectionItem, ByVal Address As String, ByVal IP As String)
    Event Disconected(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event GotData(ByRef NetworkConnection As NetworkConnectionItem, ByVal Data As String)
    Event AddressNotFound(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event MessageNotSend(ByRef NetworkConnection As NetworkConnectionItem, ByVal Command As String)

    Event ServerStarted(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event ServerCantStart(ByRef NetworkConnection As NetworkConnectionItem, ByVal ConnectionData As String)
    Event NewConnectionStarted(ByRef Connection As NetworkConnectionItem)
    Event ServerStoped(ByRef NetworkConnection As NetworkConnectionItem)

    Event IPUpdated(ByRef NetworkConnection As NetworkConnectionItem)

End Interface
