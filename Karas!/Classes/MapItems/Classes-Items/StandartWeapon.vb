Namespace MapItem

    <Serializable()> _
    Public Class StandartWeapon
        Implements MapItem.iItem

        Public Points(0 To 2) As Point
        Public GunPos() As Point = {New Point(Nothing, Nothing), New Point(Nothing, Nothing)}
        Private Current As Integer = -1
        Private Pen As New Pen(Color.Black, 1)
        Private Pen2 As New Pen(Color.Green, 1)
        Private Pen3 As New Pen(Color.Green, 1)
        Private Pen4 As New Pen(Color.Black, 3)
        Public Fin As Boolean = False
        Private tmpImage As Image
        Private tmpGraphics As Graphics
        Private MaskColor As Color = Color.PapayaWhip
        Private RegionData As Region = Nothing

        Public ReadOnly Property CanShoot() As Boolean Implements iItem.CanShoot
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property CanDie() As Boolean Implements iItem.CanDie
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CanBeDestroyed() As Boolean Implements iItem.CanBeDestroyed
            Get
                Return True
            End Get
        End Property

        Sub VerticalFlip(Optional ByVal Width As Single = 0) Implements iItem.VerticalFlip
            Dim max As Point = PointsTools.Max(Me.Points)
            For i As Integer = 0 To Me.Points.Length - 1
                Me.Points(i).X = Math.Abs(Width - Me.Points(i).X) '- Max.X
            Next

            Me.GunPos(0).X = Math.Abs(Width - Me.GunPos(0).X)
            Me.GunPos(1).X = Math.Abs(Width - Me.GunPos(1).X)
            'Me.CalcGun()
        End Sub

        Public Sub AddPoint(ByVal X As Single, ByVal Y As Single) Implements iItem.AddPoint
            If Me.Fin Then Exit Sub
            Me.Current += 1
            Me.Points(Me.Current) = New Point(X, Y)
            Me.RegionData = Nothing
            If Me.Current = 2 Then Me.Fin = True
        End Sub

        Public Sub DrawAndAdd(ByRef Image As System.Drawing.Image, ByVal X As Single, ByVal Y As Single) Implements iItem.DrawAndAdd
            Me.DrawAndAdd(Graphics.FromImage(Image), X, Y)            
        End Sub

        Sub DrawAndAdd(ByRef Graphics As Graphics, ByVal X As Single, ByVal Y As Single) Implements iItem.DrawAndAdd
            If Me.Fin Then Exit Sub
            Me.AddPoint(X, Y)
            Dim dlg As AnimationFx.FinishFunc = AddressOf Me.FFunc1
            'Me.tmpImage = Image
            Me.tmpGraphics = Graphics
            AnimationFx.DrawCircle(Graphics, New Point(X, Y), Pen, 3, dlg, MapItem.RunFunc.Redraw)
        End Sub

        Private Sub FFunc1()
            Static count As SByte = 0
            Static dlg As AnimationFx.FinishFunc = AddressOf Me.FFunc1
            Select Case count
                Case 1
                    AnimationFx.DrawLine(Me.tmpGraphics, Me.Points(1), Me.Points(0), Pen2, , MapItem.RunFunc.Redraw)
                Case 2
                    AnimationFx.DrawLine(Me.tmpGraphics, Me.Points(2), Me.Points(1), Pen2, dlg, MapItem.RunFunc.Redraw)
                    AnimationFx.DrawLine(Me.tmpGraphics, Me.Points(2), Me.Points(0), Pen2, dlg, MapItem.RunFunc.Redraw)
                Case 4
                    Dim gp As New Drawing2D.GraphicsPath()
                    gp.AddLines(Me.Points)
                    AnimationFx.FillRegion(Me.tmpGraphics, Me.Pen3, New Region(gp), dlg, MapItem.RunFunc.Redraw)
                Case 5

                    'Dim pt As Point = Me.GetHalfPoint
                    'pt.X -= pt.X
                    'pt.Y -= pt.Y
                    'Dim x As Single, y As Single                    
                    'x = Me.Points(0).X - pt1.X
                    'y = Me.Points(0).Y - pt1.Y
                    'Dim pt As New Point(Me.Points(0).X + x, Me.Points(0).Y + y)

       
                    ''deg = deg * 180 / Math.PI
                    ''pt.X -= pt.X * Math.Cos(deg) - pt.Y * Math.Sin(deg)
                    ''pt.Y -= pt.X * Math.Sin(deg) + pt.Y * Math.Cos(deg)
                    'pt.X = Me.Points(1).X - M.Elements(0) + M.Elements(2)
                    'pt.Y = Me.Points(1).Y - M.Elements(1) + M.Elements(3)
                    ''If Points(1).X > Points(0).X And Points(1).X > Points(2).X Then
                    '' pt.X = Me.Points(1).X - M.Elements(0)
                    ''pt.Y = Me.Points(1).Y - M.Elements(1)
                    ''ElseIf Points(1).X < Points(0).X And Points(1).X < Points(2).X Then
                    ''deg = (360 - deg) / 2
                    ''ElseIf Points(1).X >= Points(0).X And Points(1).X <= Points(2).X Then
                    ''pt.X = Me.Points(1).X - M.Elements(0)
                    ''pt.Y = Me.Points(1).Y - M.Elements(1)
                    ''End If     
                    Me.CalcGun()
                    AnimationFx.DrawLine(Me.tmpGraphics, Me.GunPos(0), Me.GunPos(1), Pen4, dlg, MapItem.RunFunc.Redraw)
                    'AnimationFx.DrawLine(Me.tmpImage, Me.Points(1), pk, Pen4, , Me.RedrawFunc)
                Case 6
                    Me.DrawContent(Me.tmpGraphics)
            End Select
            count += 1
        End Sub

        Private Sub CalcGun()
            Dim pk As Point = Me.GetHalfPoint
            Dim deg As Double = MapItem.PointsTools.XYToDegrees(pk, Me.Points(1))
            Dim M As New Drawing2D.Matrix()
            'deg = deg * 180 / Math.PI                    
            If pk.X > Me.Points(1).X Then
                If pk.Y > Me.Points(1).Y Then
                    M.Rotate(deg - 90)
                Else
                    M.Rotate(deg - 270)
                End If
            Else
                If pk.Y > Me.Points(1).Y Then
                    M.Rotate(deg + 135)                    
                Else
                    M.Rotate(deg - 135 - 90)
                End If
            End If
            Dim ph As New Drawing2D.GraphicsPath()
            ph.AddLine(0, 0, 5, 5)
            ph.Transform(M)
            Dim pt As Point = New Point(0, 0)
            Me.GunPos(1).X = Me.Points(1).X + ph.PathData.Points(1).X
            Me.GunPos(1).Y = Me.Points(1).Y + ph.PathData.Points(1).Y
            Me.GunPos(0) = Me.Points(1)

            ph.Dispose()
        End Sub

        Private Function GetHalfPoint() As Point
            Return PointsTools.HalfPoint(Me.Points(0), Me.Points(2))
            'Dim pt As Point = New Point(Me.Points(2).X, Me.Points(2).Y)
            'pt.X += (Me.Points(0).X - Me.Points(2).X) / 2
            'pt.Y += (Me.Points(0).Y - Me.Points(2).Y) / 2
            'Return pt
        End Function

        Public Sub DrawContent(ByRef Graphics As System.Drawing.Graphics) Implements iItem.DrawContent
            If Me.GunPos(0).X = Nothing Or Me.GunPos(1).Y = Nothing Then Me.CalcGun()
            Using gr As Graphics = Graphics
                gr.DrawEllipse(Pen, CSng(Me.Points(0).X - 1.5), CSng(Me.Points(0).Y - 1.5), 3, 3)
                gr.DrawEllipse(Pen, CSng(Me.Points(1).X - 1.5), CSng(Me.Points(1).Y - 1.5), 3, 3)
                gr.DrawEllipse(Pen, CSng(Me.Points(2).X - 1.5), CSng(Me.Points(2).Y - 1.5), 3, 3)
                gr.FillPolygon(New SolidBrush(Pen3.Color), Me.Points)
                gr.DrawLines(Pen2, Me.Points)
                gr.DrawLine(Me.Pen4, Me.GunPos(0), Me.GunPos(1))
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
        End Sub

        Public ReadOnly Property LastY() As Single Implements iItem.LastY
            Get
                If Me.Current = 0 Then Return Nothing
                Return Me.Points(Me.Current - 1).Y
            End Get
        End Property

        Public ReadOnly Property LastX() As Single Implements iItem.LastX
            Get
                If Me.Current = 0 Then Return Nothing
                Return Me.Points(Me.Current - 1).X
            End Get
        End Property

        Public Sub Move(ByVal X As Integer, ByVal Y As Integer) Implements iItem.Move
            If Me.GunPos(0).X = Nothing Or Me.GunPos(1).Y = Nothing Then Me.CalcGun()
            PointsTools.Move(Me.Points, X, Y)
            PointsTools.Move(Me.GunPos, X, Y)
            PointsTools.Move(Me.RegionData, X, Y)
        End Sub

        Public ReadOnly Property Price() As Integer Implements iItem.Price
            Get
                If Me.Finished Then
                    Dim w As Single = Math.Abs(PointsTools.Max(Me.Points).X - PointsTools.Min(Me.Points).X)
                    Dim h As Single = Math.Abs(PointsTools.Max(Me.Points).Y - PointsTools.Min(Me.Points).Y)        
                    Dim c As Single = w * h * 10000
                    Dim I As Integer = 0
                    Do
                        I = I + 1
                        c = c / I
                    Loop Until c < 10000
                    Return Math.Round(c / 2)
                Else
                    Return 0
                End If
            End Get
        End Property

        Public ReadOnly Property Type() As String Implements iItem.Type
            Get
                Return "Ginklas"
            End Get
        End Property

        Public ReadOnly Property Region() As System.Drawing.Region Implements iItem.Region
            Get
                If Me.RegionData Is Nothing Then
                    Dim w As Single = Me.Points(0).X, h As Single = Me.Points(0).Y
                    If w < Me.Points(1).X Then w = Me.Points(1).X
                    If w < Me.Points(2).X Then w = Me.Points(2).X
                    If h < Me.Points(2).Y Then h = Me.Points(2).Y
                    If h < Me.Points(1).Y Then h = Me.Points(1).Y
                    Dim bmp As New Bitmap(w + 10, h + 10, Imaging.PixelFormat.Format32bppArgb)
                    Using gr As Graphics = Graphics.FromImage(bmp)
                        gr.FillRectangle(New SolidBrush(Me.maskColor), 0, 0, bmp.Width, bmp.Height)
                        Me.DrawContent(gr)
                    End Using
                    Me.RegionData = GetRegion(bmp, Me.maskColor)
                End If
                Return Me.RegionData               
            End Get
        End Property

        Public ReadOnly Property Width() As Single Implements iItem.Width
            Get
                Return PointsTools.Max(Me.Points).X + 10
            End Get
        End Property

        Public ReadOnly Property Height() As Single Implements iItem.Height
            Get
                Return PointsTools.Max(Me.Points).Y + 10
            End Get
        End Property


        ReadOnly Property WorkMode() As MapItem.PossibleWorkModes Implements MapItem.iItem.WorkMode
            Get
                Return PossibleWorkModes.OnClick
            End Get
        End Property

        ReadOnly Property Finished() As Boolean Implements iItem.Finished
            Get
                Return Me.Fin
            End Get
        End Property

        ReadOnly Property EditCommands() As Boolean Implements iItem.EditCommands
            Get
                Return True
            End Get
        End Property

    End Class

End Namespace