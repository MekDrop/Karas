Public Class ucConsole

    Delegate Sub wtWrite(ByVal Text As String)

    Private _FocusControl As System.Windows.Forms.Control = Nothing
    Public DebugMode As Boolean = False

    Public Property FocusControl() As System.Windows.Forms.Control
        Get
            Return Me._FocusControl
        End Get
        Set(ByVal value As System.Windows.Forms.Control)
            Me._FocusControl = value
        End Set
    End Property

    Private Function getTime() As String        
        Return String.Format("[{0:T}]", System.DateTime.Now)
    End Function

    Private Sub Write(ByVal Text As String)
        Try
            Me.rtbChat.AppendText(Text.Trim + vbCrLf)
            Me.rtbChat.Select(Me.rtbChat.TextLength, 1)
            Me.rtbChat.ScrollToCaret()
            System.Windows.Forms.Application.DoEvents()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub WriteAsComputer(ByVal Text As String)

        Text = Me.getTime + " " + Text

        If Me.rtbChat.InvokeRequired Then
            Me.rtbChat.Invoke(New wtWrite(AddressOf Write), Text)
        Else
            Me.Write(Text)
        End If

    End Sub

    Public Sub WriteAsUser(ByVal Name As String, ByVal Text As String)
        Me.WriteAsComputer(Name + "> " + Text)
    End Sub

    Private Sub rtbChat_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rtbChat.MouseUp

        If Me.rtbChat.SelectionLength > 0 Then
            Clipboard.SetText(Me.rtbChat.SelectedText)
            Me.rtbChat.SelectionLength = 0
        End If        

        If Not Me._FocusControl Is Nothing Then
            Me._FocusControl.Focus()
        End If

    End Sub

    Sub WriteAsDebugData(ByVal Text As String)
        If Me.DebugMode Then Me.WriteAsComputer("Debug: " + Text)
    End Sub

End Class
