Public Class DelaytedExecution

    Private Shared Works As System.Collections.Queue
    Private Shared WithEvents Timer As System.Timers.Timer

    Public Delegate Sub SubForExecution(ByVal Params() As Object)

    Private Structure Action
        Public SubNameForExecute As DelaytedExecution.SubForExecution
        Public Params() As Object
        Sub New(ByRef SubNameForExecute As DelaytedExecution.SubForExecution, ByVal ParamArray params() As Object)
            Me.SubNameForExecute = SubNameForExecute
            Me.Params = params
        End Sub
    End Structure

    Sub New(Optional ByVal Interval As Integer = 100, Optional ByVal QueueSize As Integer = 100)
        Timer = New System.Timers.Timer(Interval)
        Works = New System.Collections.Queue(QueueSize)
    End Sub

    Private Shared Sub DelaytedExecution_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer.Elapsed
        If Works.Count > 0 Then
            Dim item As Action = Works.Dequeue()
            item.SubNameForExecute.Invoke(item.Params)
        Else
            Timer.Stop()
        End If
    End Sub

    Public Sub Add(ByRef SubForExecute As DelaytedExecution.SubForExecution, ByVal ParamArray params() As Object)
        Works.Enqueue(New Action(SubForExecute, params))
        If Timer.Enabled = False Then
            Timer.Enabled = True
            Timer.Start()
        End If
    End Sub

End Class
