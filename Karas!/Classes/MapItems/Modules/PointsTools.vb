Namespace MapItem

    Module PointsTools

        Public Sub Move(ByRef Points() As Point, ByVal X As Integer, ByVal Y As Integer)
            Dim I As Integer
            For I = 0 To Points.Count - 1 Step 1
                Move(Points(I), X, Y)                
            Next
        End Sub

        Public Sub Move(ByRef Point As Point, ByVal X As Integer, ByVal Y As Integer)            
            Point.X += X
            Point.Y += Y
        End Sub

        Public Function Min(ByRef Points() As Point) As Point
            Dim w As Single = Points(0).X, h As Single = Points(0).Y
            For I As Integer = 1 To Points.Count - 1
                If w > Points(I).X Then w = Points(I).X
                If h > Points(I).Y Then h = Points(I).Y
            Next
            Return New Point(w, h)
        End Function

        Public Function Max(ByRef Points() As Point) As Point
            Dim w As Single = Points(0).X, h As Single = Points(0).Y
            For I As Integer = 1 To Points.Count - 1
                If w < Points(I).X Then w = Points(I).X
                If h < Points(I).Y Then h = Points(I).Y
            Next
            Return New Point(w, h)
        End Function

        Public Sub Move(ByRef Points As Collections.Generic.List(Of Point), ByVal X As Integer, ByVal Y As Integer)
            Dim I As Integer ', pt As Point
            For I = 0 To Points.Count - 1 Step 1
                Move(Points.Item(I), X, Y)
                'pt = Points.Item(I)
                'pt.X += X
                'pt.Y += Y
                'Points.Item(I) = pt
            Next
        End Sub

        Public Sub Move(ByRef Region As Region, ByVal X As Integer, ByVal Y As Integer)
            Region.Translate(X, Y)
        End Sub

        Public Function Move(ByVal X As Integer, ByVal Y As Integer, ByVal ParamArray Points() As Point) As Point()
            Dim I As Integer
            For I = 0 To Points.Count - 1 Step 1
                Points(I).X += X
                Points(I).Y += Y
            Next
            Return Points
        End Function

        ' http://www.codeproject.com/KB/selection/angle_custom_control.aspx
        Public Function XYToDegrees(ByVal xy As Point, ByVal origin As Point) As Single
            Dim angle As Double = 0

            If xy.Y < origin.Y Then
                If xy.X > origin.X Then
                    angle = CDbl((xy.X - origin.X)) / CDbl((origin.Y - xy.Y))
                    angle = Math.Atan(angle)
                    angle = 90 - angle * 180 / Math.PI
                ElseIf xy.X < origin.X Then
                    angle = CDbl((origin.X - xy.X)) / CDbl((origin.Y - xy.Y))
                    angle = Math.Atan(-angle)
                    angle = 90 - angle * 180 / Math.PI
                End If
            ElseIf xy.Y > origin.Y Then
                If xy.X > origin.X Then
                    angle = CDbl((xy.X - origin.X)) / CDbl((xy.Y - origin.Y))
                    angle = Math.Atan(-angle)
                    angle = 270 - angle * 180 / Math.PI
                ElseIf xy.X < origin.X Then
                    angle = CDbl((origin.X - xy.X)) / CDbl((xy.Y - origin.Y))
                    angle = Math.Atan(angle)
                    angle = 270 - angle * 180 / Math.PI
                End If
            End If

            If angle > 180 Then
                angle -= 360
            End If
            'Optional. Keeps values between -180 and 180
            Return CSng(angle)
        End Function

        ' http://www.codeproject.com/KB/selection/angle_custom_control.aspx
        Public Function DegreesToXY(ByVal degrees As Single, ByVal radius As Single, ByVal origin As Point) As Point
            Dim xy As New Point()
            Dim radians As Double = degrees * Math.PI / 180
            xy.X = CSng(Math.Cos(radians)) * radius + origin.X
            xy.Y = CSng(Math.Sin(-radians)) * radius + origin.Y
            Return xy
        End Function

        Public Function HalfPoint(ByVal P1 As Point, ByVal p2 As Point) As Point
            Dim pt As Point = New Point(p2.X, p2.Y)
            pt.X += (P1.X - p2.X) / 2
            pt.Y += (P1.Y - p2.Y) / 2
            Return pt
        End Function

        Public Function HalfPoint(ByVal ParamArray Points() As Point) As Point
            Dim I As Integer
            Dim Points2 As New Collections.Generic.List(Of Point)
            For I = 0 To Points.Length - 1 Step 2
                If I + 1 = Points.Length Then
                    Points2.Add(HalfPoint(Points(I), Points(0)))
                Else
                    Points2.Add(HalfPoint(Points(I), Points(I + 1)))
                End If
            Next
            If Points2.Count = 1 Then
                Return Points2(0)
            Else
                Return HalfPoint(Points2.ToArray)
            End If
        End Function

        ' source - http://vb-helper.com/howto_net_shape_pic.html
        Public Function GetRegion(ByVal bm As Bitmap, ByVal bg_color As Color) As Region
            Dim new_region As New Region()
            new_region.MakeEmpty()

            Dim rect As New Rectangle
            Dim in_image As Boolean = False
            Dim X As Integer
            Dim b As Boolean

            For Y As Integer = 0 To bm.Height - 1
                X = 0
                Do While (X < bm.Width)
                    b = IsSameColor(bm.GetPixel(X, Y), bg_color)
                    If Not in_image Then
                        If Not b Then
                            in_image = True
                            rect.X = X
                            rect.Y = Y
                            rect.Height = 1
                        End If
                    ElseIf b Then
                        in_image = False
                        rect.Width = (X - rect.X)
                        new_region.Union(rect)
                    End If
                    X = (X + 1)
                Loop

                ' Add the final piece if necessary.
                If in_image Then
                    in_image = False
                    rect.Width = (bm.Width - rect.X)
                    new_region.Union(rect)
                End If
            Next Y

            Return new_region
        End Function

        Public Function IsSameColor(ByRef Color1 As Color, ByRef Color2 As Color) As Boolean
            Return Color1.R = Color2.R And Color1.G = Color2.G And Color1.B = Color2.B And Color1.A = Color2.A
        End Function

    End Module

End Namespace