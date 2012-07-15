Imports System.Windows.Forms

Public Class frmUserInfo

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Public Overloads Shared Sub ShowDialog(ByRef Avatar As System.Drawing.Image, ByVal ParamArray Info() As String)
        Dim FontHeader As New System.Drawing.Font("Arial", 10, FontStyle.Bold)
        Dim FontNormal As New System.Drawing.Font("Arial", 10, FontStyle.Regular)
        Dim IsHeader As Boolean = True
        Dim m As New frmUserInfo
        m.pbUserPhoto.Image = Avatar
        With m.rtbInfo
            .Text = ""
            For Each Data As String In Info
                If IsHeader Then
                    .SelectionStart = .TextLength
                    .SelectionLength = 0
                    .SelectionFont = FontHeader
                    .SelectionColor = Color.Blue
                    .SelectedText = Data + ": "
                Else
                    .SelectionStart = .TextLength
                    .SelectionLength = 0
                    .SelectionFont = FontNormal
                    .SelectionColor = Color.Red
                    .SelectedText = Data + vbCrLf + vbCrLf
                End If
                IsHeader = Not IsHeader
            Next
            .SelectionLength = 0
            .SelectionStart = 0
            .Text = .Text.Trim()
            .ReadOnly = True
        End With
        frmMain.ucTabForms.Add(m, ucTabForms.TabWorkMode.OnDeactivateClose)
        'Me.Show()
        'MsgBox("A")
    End Sub

    Private Sub frmUserInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
