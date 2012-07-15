Public Class AnimationFx

    Private Shared Animations As New Collections.Queue(20)    
    Private Shared WithEvents AnimThread As New Timers.Timer(10)
    Private Shared Processing As Boolean = False

    Public Delegate Sub FinishFunc()

    Private Structure AnimationData
        Public Pen As Pen
        Public WorkingMode As WorkingType
        Public Points() As Point
        Public Value As Integer
        Public Params() As Object
        Public LastUpdate As Long
        Public UpdateInterval As Integer
        Public Graphics As Graphics
        Public Image As Image
        Public UpdateFunc As MapItem.dlgRedrawFunc
        Public FinishFunc As FinishFunc
        Sub New(ByRef Pen As Pen, ByVal WorkingMode As WorkingType, ByRef Graphics As Graphics, ByVal ParamArray Params() As Object)
            Me.Pen = Pen
            Me.WorkingMode = WorkingMode
            'Me.Image = Image
            Me.Graphics = Graphics
            Me.Params = Params
            Me.Value = 0
            Me.LastUpdate = Date.Now.Ticks
            Me.UpdateInterval = 10
        End Sub
        Sub SetParams(ByVal ParamArray Params() As Object)
            Me.Params = Params
        End Sub
        Sub SetPoints(ByVal ParamArray Points() As Point)
            Me.Points = Points
            Select Case Me.WorkingMode
                Case WorkingType.DrawLine
                    Me.SetParams(Me.Points(0).X - Me.Points(1).X, Me.Points(0).Y - Me.Points(1).Y)
                    Me.Params(0) = Me.Params(0) / 100
                    Me.Params(1) = Me.Params(1) / 100
            End Select
        End Sub
        Sub Destroy()
            Me.Finalize()
        End Sub
        Shared Sub Add(ByRef ad As AnimationData)
            Animations.Enqueue(ad)
            If AnimThread.Enabled = False Then
                AnimThread.Start()
                AnimThread.Enabled = True
            End If
        End Sub
    End Structure

    Protected Enum WorkingType As SByte
        DrawEclipse = 1
        DrawLine = 2
        FillRegion = 3
    End Enum

    Private Shared Sub AnimWorkShop() Handles AnimThread.Elapsed
        If Processing Then Exit Sub
        Dim ad As AnimationData
        'Do
        Processing = True
        ad = Animations.Dequeue()
        Select Case ad.WorkingMode
            Case WorkingType.DrawEclipse
                ad.Value = Math.Abs(Fix((-ad.LastUpdate + Date.Now.Ticks) / 1000000 * ad.UpdateInterval))
                If ad.Value > 360 Then ad.Value = 360
                ad.Graphics.DrawArc(ad.Pen, ad.Points(0).X, ad.Points(0).Y, ad.Params(0), ad.Params(1), 0, ad.Value)
                If ad.Value < 360 Then
                    Animations.Enqueue(ad)
                Else
                    ad.Graphics.DrawArc(ad.Pen, ad.Points(0).X, ad.Points(0).Y, ad.Params(0), ad.Params(1), 0, 360)
                    If Not ad.FinishFunc Is Nothing Then ad.FinishFunc.Invoke()
                    ad.Destroy()
                End If
            Case WorkingType.DrawLine
                ad.Value += ad.UpdateInterval
                If ad.Value > 100 Then ad.Value = 100
                ad.Graphics.DrawLine(ad.Pen, ad.Points(0), New Point( _
                                ad.Points(0).X - ad.Params(0) * ad.Value, _
                                ad.Points(0).Y - ad.Params(1) * ad.Value) _
                               )
                If ad.Value < 100 Then
                    Animations.Enqueue(ad)
                Else
                    ad.Graphics.DrawLine(ad.Pen, ad.Points(0), ad.Points(1))
                    If Not ad.FinishFunc Is Nothing Then ad.FinishFunc.Invoke()
                    ad.Destroy()
                End If
            Case WorkingType.FillRegion
                ad.Value += ad.UpdateInterval
                If ad.Value > 255 Then ad.Value = 255
                ad.Graphics.FillRegion(New SolidBrush(Color.FromArgb(ad.Value, ad.Pen.Color.R, ad.Pen.Color.G, ad.Pen.Color.B)), ad.Params(0))
                If ad.Value < 255 Then
                    Animations.Enqueue(ad)
                Else
                    ad.Graphics.FillRegion(New SolidBrush(ad.Pen.Color), ad.Params(0))
                    If Not ad.FinishFunc Is Nothing Then ad.FinishFunc.Invoke()
                    ad.Destroy()                    
                End If
        End Select
        'ad.Graphics.Save()
        Try
            ad.Graphics.Flush()
        Catch ex As Exception

        End Try        
        If Not ad.UpdateFunc Is Nothing Then ad.UpdateFunc.Invoke()
        AnimThread.Enabled = Animations.Count > 0
        'System.Threading.Thread.Sleep(100)
        'Loop While IsRunning
        Processing = False
    End Sub

    Public Shared Sub DrawEclipse(ByRef Graphics As Graphics, ByRef Point As Point, ByRef Pen As Pen, ByVal Width As Single, ByVal Height As Single, Optional ByVal FinishFunc As [Delegate] = Nothing, Optional ByVal UpdateFunc As [Delegate] = Nothing, Optional ByVal Speed As Integer = 100)
        Dim ad As New AnimationData(Pen, WorkingType.DrawEclipse, Graphics)
        Point.X -= Width / 2
        Point.Y -= Height / 2
        ad.SetParams(Width, Height)
        ad.SetPoints(Point)
        ad.UpdateFunc = UpdateFunc
        ad.UpdateInterval = Speed
        ad.FinishFunc = FinishFunc
        AnimationData.Add(ad)
    End Sub

    Public Shared Sub DrawCircle(ByRef Graphics As Graphics, ByRef Point As Point, ByRef Pen As Pen, ByVal r As Single, Optional ByVal FinishFunc As [Delegate] = Nothing, Optional ByVal UpdateFunc As [Delegate] = Nothing, Optional ByVal Speed As Integer = 100)
        DrawEclipse(Graphics, Point, Pen, r, r, FinishFunc, UpdateFunc, Speed)
    End Sub

    Public Shared Sub DrawLine(ByRef Graphics As Graphics, ByRef Point1 As Point, ByRef Point2 As Point, ByRef Pen As Pen, Optional ByVal FinishFunc As [Delegate] = Nothing, Optional ByVal UpdateFunc As [Delegate] = Nothing, Optional ByVal Speed As Integer = 100)
        Dim ad As New AnimationData(Pen, WorkingType.DrawLine, Graphics)
        ad.SetPoints(Point1, Point2)
        ad.UpdateFunc = UpdateFunc
        ad.UpdateInterval = Speed
        ad.FinishFunc = FinishFunc
        AnimationData.Add(ad)
    End Sub

    Public Shared Sub FillRegion(ByRef Graphics As Graphics, ByRef Pen As Pen, ByRef Region As Region, Optional ByVal FinishFunc As [Delegate] = Nothing, Optional ByVal UpdateFunc As [Delegate] = Nothing, Optional ByVal Speed As Integer = 10)
        Dim ad As New AnimationData(Pen, WorkingType.FillRegion, Graphics)
        ad.UpdateFunc = UpdateFunc
        ad.UpdateInterval = Speed
        ad.SetParams(Region)
        ad.FinishFunc = FinishFunc
        AnimationData.Add(ad)
    End Sub

    Private Sub New()
    End Sub

End Class
