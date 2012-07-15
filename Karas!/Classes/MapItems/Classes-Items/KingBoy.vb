Namespace MapItem

    <Serializable()> _
    Public Class KingBoy
        Implements iItem

        Public LeftTopPoint As New Point(0, 0)
        Private KingBoyImage As Image = New Drawing.Imaging.Metafile(File.GetTruePath("Textures\kingboy.emf"))
        Private MaskColor As Color = Color.PapayaWhip
        Private RegionData As Region = Nothing

        Sub VerticalFlip(Optional ByVal Width As Single = 0) Implements iItem.VerticalFlip
            Me.LeftTopPoint.X = Math.Abs(Width - Me.LeftTopPoint.X) - KingBoyImage.Width
        End Sub

        Public ReadOnly Property CanShoot() As Boolean Implements iItem.CanShoot
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CanDie() As Boolean Implements iItem.CanDie
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property CanBeDestroyed() As Boolean Implements iItem.CanBeDestroyed
            Get
                Return False
            End Get
        End Property

        Sub New(ByRef Image As System.Drawing.Image, ByVal X As Single, ByVal Y As Single)
            Me.DrawAndAdd(Image, X, Y)
        End Sub

        Sub New(ByRef Graphics As Graphics, ByVal X As Single, ByVal Y As Single)
            Me.DrawAndAdd(Graphics, X, Y)
        End Sub

        Sub DrawAndAdd(ByRef Graphics As Graphics, ByVal X As Single, ByVal Y As Single) Implements iItem.DrawAndAdd
            Me.AddPoint(X, Y)
            Me.DrawContent(Graphics)
        End Sub

        Public ReadOnly Property Type() As String Implements iItem.Type
            Get
                Return "Karalius"
            End Get
        End Property

        Public Sub AddPoint(ByVal X As Single, ByVal Y As Single) Implements iItem.AddPoint
            Me.LeftTopPoint.X = X
            Me.LeftTopPoint.Y = Y
        End Sub

        Public Sub DrawAndAdd(ByRef Image As System.Drawing.Image, ByVal X As Single, ByVal Y As Single) Implements iItem.DrawAndAdd
            Me.AddPoint(X, Y)
            Me.DrawContent(Image)
        End Sub

        Public Sub DrawContent(ByRef Graphics As System.Drawing.Graphics) Implements iItem.DrawContent
            Using gr As Graphics = Graphics
                gr.DrawImage(Me.KingBoyImage, Me.LeftTopPoint.X, Me.LeftTopPoint.Y)
            End Using
        End Sub

        Public Sub DrawContent(ByRef Image As System.Drawing.Image) Implements iItem.DrawContent
            Me.DrawContent(System.Drawing.Graphics.FromImage(Image))
        End Sub

        Public Sub DrawContent(ByRef Form As System.Windows.Forms.Form) Implements iItem.DrawContent
            Dim bmp As New Bitmap(Form.Width, Form.Height)
            Me.DrawContent(System.Drawing.Graphics.FromImage(bmp))
            Form.BackgroundImage = bmp.Clone
        End Sub

        Public Sub Finish(ByVal X As Single, ByVal Y As Single) Implements iItem.Finish
            Me.AddPoint(X, Y)
        End Sub

        Public ReadOnly Property Finished() As Boolean Implements iItem.Finished
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property LastY() As Single Implements iItem.LastY
            Get
                Return Me.LeftTopPoint.Y
            End Get
        End Property

        Public ReadOnly Property LastX() As Single Implements iItem.LastX
            Get
                Return Me.LeftTopPoint.X
            End Get
        End Property

        Public Sub Move(ByVal X As Integer, ByVal Y As Integer) Implements iItem.Move
            MapItem.PointsTools.Move(Me.LeftTopPoint, X, Y)
            MapItem.PointsTools.Move(Me.RegionData, X, Y)
        End Sub

        Public Sub New()

        End Sub

        Public ReadOnly Property Price() As Integer Implements iItem.Price
            Get
                Return 0
            End Get
        End Property

        Public ReadOnly Property Width() As Single Implements iItem.Width
            Get
                Return KingBoyImage.Width + Me.LeftTopPoint.X
            End Get
        End Property

        Public ReadOnly Property Height() As Single Implements iItem.Height
            Get
                Return KingBoyImage.Height + Me.LeftTopPoint.Y
            End Get
        End Property

        Public ReadOnly Property Region() As System.Drawing.Region Implements iItem.Region
            Get
                If RegionData Is Nothing Then
                    Dim MaskImage As New Bitmap(Me.KingBoyImage.Width, Me.KingBoyImage.Height)
                    Using gr As Drawing.Graphics = Graphics.FromImage(MaskImage)
                        gr.FillRectangle(New SolidBrush(Me.MaskColor), 0, 0, Me.KingBoyImage.Width, Me.KingBoyImage.Height)
                        gr.DrawImage(Me.KingBoyImage, 0, 0)
                    End Using
                    Me.RegionData = GetRegion(MaskImage, Me.MaskColor)
                    MapItem.PointsTools.Move(Me.RegionData, Me.LeftTopPoint.X, Me.LeftTopPoint.Y)
                End If
                Return Me.RegionData
            End Get
        End Property

        Public ReadOnly Property WorkMode() As PossibleWorkModes Implements iItem.WorkMode
            Get
                Return PossibleWorkModes.None
            End Get
        End Property

        ReadOnly Property EditCommands() As Boolean Implements iItem.EditCommands
            Get
                Return False
            End Get
        End Property

    End Class

End Namespace
