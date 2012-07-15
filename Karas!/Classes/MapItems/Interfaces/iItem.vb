Namespace MapItem

    Public Interface iItem

        Sub AddPoint(ByVal X As Single, ByVal Y As Single)
        Sub Finish(ByVal X As Single, ByVal Y As Single)

        Sub DrawAndAdd(ByRef Image As Image, ByVal X As Single, ByVal Y As Single)
        Sub DrawAndAdd(ByRef Graphics As Graphics, ByVal X As Single, ByVal Y As Single)
        Sub DrawContent(ByRef Image As Image)
        Sub DrawContent(ByRef Form As Form)
        Sub DrawContent(ByRef Graphics As Graphics)
        Sub Move(ByVal X As Integer, ByVal Y As Integer)

        ReadOnly Property LastX() As Single
        ReadOnly Property LastY() As Single
        ReadOnly Property Price() As Integer
        ReadOnly Property Region() As Region
        ReadOnly Property WorkMode() As MapItem.Enums.PossibleWorkModes
        ReadOnly Property Finished() As Boolean
        ReadOnly Property EditCommands() As Boolean

        ReadOnly Property CanShoot() As Boolean
        ReadOnly Property CanDie() As Boolean
        ReadOnly Property CanBeDestroyed() As Boolean

        ReadOnly Property Width() As Single
        ReadOnly Property Height() As Single

        ReadOnly Property Type() As String

        Sub VerticalFlip(Optional ByVal Width As Single = 0)

    End Interface

End Namespace