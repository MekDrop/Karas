Namespace MapItem

    <Serializable()> _
    Public Class Wall        
        Implements MapItem.iItem

        Public Points As System.Collections.Generic.List(Of Point)
        Public LineWith As Single

        Private MinX As Single, MinY As Single
        Private MaxX As Single, MaxY As Single
        Private X As Single, Y As Single
        Private Pen As Pen = New Pen(Color.Green, 10)
        Private tmpPrice As Integer = -1
        Private MaskColor As Color = Color.PapayaWhip
        Private RegionData As Region = Nothing        

        Public ReadOnly Property CanShoot() As Boolean Implements iItem.CanShoot
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CanDie() As Boolean Implements iItem.CanDie
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property CanBeDestroyed() As Boolean Implements iItem.CanBeDestroyed
            Get
                Return False
            End Get
        End Property

        Sub VerticalFlip(Optional ByVal Width As Single = 0) Implements iItem.VerticalFlip
            Dim points() As Point = Me.Points.ToArray.Clone
            Dim max As Point = PointsTools.Max(points)
            Dim min As Point = PointsTools.Max(points)
            Dim pt As Point
            For i As Integer = 0 To Me.Points.Count - 1                
                pt = Me.Points.Item(i)
                pt.X = Math.Abs(Width - Me.Points.Item(i).X) '- min.X
                Me.Points.Item(i) = pt
            Next
        End Sub

        Sub New(ByVal LineWith As Single, ByVal X As Single, ByVal Y As Single)
            Me.LineWith = LineWith
            Me.X = X
            Me.Y = Y
            Me.Points = New System.Collections.Generic.List(Of Point)
            Me.Points.Add(New Point(X, Y))            
            Me.MinX = X
            Me.MinY = Y
            Me.MaxX = X
            Me.MaxY = Y
        End Sub

        Sub DrawAndAdd(ByRef Image As Image, ByVal X As Single, ByVal Y As Single) Implements MapItem.iItem.DrawAndAdd            
            Using gr As Graphics = System.Drawing.Graphics.FromImage(Image)
                Me.DrawAndAdd(Graphics.FromImage(Image), X, Y)
            End Using
        End Sub

        Sub DrawAndAdd(ByRef Graphics As Graphics, ByVal X As Single, ByVal Y As Single) Implements iItem.DrawAndAdd
            Graphics.DrawLine(New Pen(Me.Pen.Color, 10), Me.X, Me.Y, X, Y)
            Graphics.Flush()
            Me.AddPoint(X, Y)
        End Sub

        Public ReadOnly Property Type() As String Implements iItem.Type
            Get
                Return "Siena"
            End Get
        End Property

        Private Sub UpdateMinMax(ByVal X As Single, ByVal Y As Single)
            If Me.MinY > Y Then Me.MinY = Y
            If Me.MinX > X Then Me.MinX = X
            If Me.MaxX < X Then Me.MaxX = X
            If Me.MaxY < Y Then Me.MaxY = Y
            Me.tmpPrice = Math.Round((Math.Abs(MaxY - MinY) * Math.Abs(MaxX - MinX) * Me.Points.Count) / 1000, 2)
        End Sub

        Sub AddPoint(ByVal X As Single, ByVal Y As Single) Implements MapItem.iItem.AddPoint
            If X = Me.X And Y = Me.Y Then Exit Sub
            If X = Me.X Or Y = Me.Y Then Exit Sub
            Me.Points.Add(New Point(X, Y))
            Me.X = X
            Me.Y = Y
            Me.UpdateMinMax(X, Y)
        End Sub

        Sub Finish(ByVal X As Single, ByVal Y As Single) Implements MapItem.iItem.Finish
            If X = Me.X And Y = Me.Y Then Exit Sub
            Me.Points.Add(New Point(X, Y))
            Me.Points.Add(New Point(Me.X, Me.Y))
            Me.UpdateMinMax(X, Y)
        End Sub

        Public ReadOnly Property LastX() As Single Implements MapItem.iItem.LastX
            Get
                Return X
            End Get
        End Property

        Public ReadOnly Property LastY() As Single Implements MapItem.iItem.LastY
            Get
                Return Y
            End Get
        End Property

        Public Sub DrawContent(ByRef Form As Global.System.Windows.Forms.Form) Implements MapItem.iItem.DrawContent
            Dim bmp As New Bitmap(Form.Width, Form.Height)
            Me.DrawContent(System.Drawing.Graphics.FromImage(bmp))
            Form.BackgroundImage = bmp.Clone
        End Sub

        Public Sub DrawContent(ByRef Image As Image) Implements MapItem.iItem.DrawContent
            Me.DrawContent(System.Drawing.Graphics.FromImage(Image))
        End Sub

        Public Sub DrawContent(ByRef Graphics As Graphics) Implements MapItem.iItem.DrawContent
            'Using Graphics
            'Dim gp As New System.Drawing.Drawing2D.GraphicsPath
            'Dim c As New Collections.Generic.List(Of Point)
            'Dim I As Integer
            'For I = 0 To Me.Points.Count - 1 Step 1
            'c.Add(New Point(Me.Points(I).X - 5, Me.Points(I).Y - 5))
            'Next
            'For I = Me.Points.Count - 1 To 0 Step -1
            '                c.Add(New Point(Me.Points(I).X + 5, Me.Points(I).Y + 5))
            '               Next
            If Me.Points.Count < 2 Then Exit Sub
            '             gp.AddPolygon(c.ToArray)
            Dim pt() As Point = Me.Points.ToArray()
            Graphics.DrawLines(Me.Pen, pt)
            Graphics.Flush()
            'Graphics.FillRegion(Brushes.Aqua, New Region(gp))
            'End Using
        End Sub

        Public ReadOnly Property Region() As Region Implements MapItem.iItem.Region
            Get
                'Dim gp As New System.Drawing.Drawing2D.GraphicsPath
                'Dim c As New Collections.Generic.List(Of Point)
                'Dim I As Integer
                'For I = 0 To Me.Points.Count - 1 Step 1
                'c.Add(New Point(Me.Points(I).X - 6, Me.Points(I).Y - 6))
                'Next
                'For I = Me.Points.Count - 1 To 0 Step -1
                '                c.Add(New Point(Me.Points(I).X + 6, Me.Points(I).Y + 6))
                '               Next
                '              gp.AddPolygon(c.ToArray)
                '             Return New Region(gp)
                If Me.RegionData Is Nothing Then
                    Dim min As Point = PointsTools.Min(Me.Points.ToArray), max As Point = PointsTools.Max(Me.Points.ToArray)
                    'MsgBox(w, , h)
                    Dim bmp As New Bitmap(max.X + 10, max.Y + 10, Imaging.PixelFormat.Format32bppArgb)
                    Using gr As Graphics = Graphics.FromImage(bmp)
                        gr.TranslateTransform(-min.X + 10, -min.Y + 10)
                        gr.FillRectangle(New SolidBrush(Me.MaskColor), 0, 0, bmp.Width + min.X + 10, bmp.Height + min.Y + 10)
                        Me.DrawContent(gr)
                    End Using
                    'bmp.Save("C:\test" + Date.Now.Ticks.ToString + ".png", Drawing.Imaging.ImageFormat.Png)
                    Me.RegionData = GetRegion(bmp, Me.MaskColor)
                    Me.RegionData.Translate(min.X - 10, min.Y - 10)
                End If
                Return Me.RegionData
            End Get
        End Property

        Public ReadOnly Property Width() As Single Implements iItem.Width
            Get
                Return PointsTools.Max(Me.Points.ToArray).X + 10
            End Get
        End Property

        Public ReadOnly Property Height() As Single Implements iItem.Height
            Get
                Return PointsTools.Max(Me.Points.ToArray).Y + 10
            End Get
        End Property

        Public ReadOnly Property Price() As Integer Implements MapItem.iItem.Price
            Get
                If Me.tmpPrice < 0 And Me.Points.Count > 0 Then
                    Dim I As Integer
                    For I = 0 To Me.Points.Count - 1 Step 1
                        Me.UpdateMinMax(Me.Points(I).X, Me.Points(I).Y)
                    Next
                ElseIf Me.tmpPrice < 0 And Me.Points.Count = 0 Then
                    Me.tmpPrice = 0
                End If
                Return Me.tmpPrice
            End Get
        End Property

        Public Sub Move(ByVal X As Integer, ByVal Y As Integer) Implements MapItem.iItem.Move
            PointsTools.Move(Me.Points, X, Y)
            '.Translate(X, Y)
            PointsTools.Move(Me.RegionData, X, Y)
        End Sub

        Sub New()

        End Sub

        ReadOnly Property WorkMode() As MapItem.PossibleWorkModes Implements MapItem.iItem.WorkMode
            Get
                Return PossibleWorkModes.OnMouseMove
            End Get
        End Property

        ReadOnly Property Finished() As Boolean Implements iItem.Finished
            Get
                Return False
            End Get
        End Property

        ReadOnly Property EditCommands() As Boolean Implements iItem.EditCommands
            Get
                Return True
            End Get
        End Property

    End Class

End Namespace