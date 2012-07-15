Public Class frmMain

    Public Sub ShowFormAsMDIChild(ByRef Form As Form)

        Static LastForm As Form = Nothing

        If Not LastForm Is Nothing Then
            If LastForm.Name = Form.Name Then Exit Sub
        End If        

        If Not LastForm Is Nothing Then

            If LastForm.Tag = True Then
                LastForm.Visible = False
            Else
                LastForm.Close()
            End If
        End If

        Form.MdiParent = Me
        Form.WindowState = FormWindowState.Maximized
        Form.MinimizeBox = False
        Form.MaximizeBox = False
        Form.ControlBox = False
        Form.Show()
        Form.Visible = True

        LastForm = Form

    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        'Me.ShowFormAsMDIChild(New Karas.frmAbout)
        Me.ucTabForms.Add(Karas.frmAbout, Karas.ucTabForms.TabWorkMode.OnDeactivateClose, BorderStyle.FixedSingle)
    End Sub

    Private Sub frmMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If Me.ucTabForms.SelectedItem Is frmEdit Then
            frmEdit.PressKey(e)
        End If        
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.ucTabForms.Add(Karas.frmEdit, Karas.ucTabForms.TabWorkMode.Normal, BorderStyle.FixedSingle)
        'Me.ucTabForms.Add(Karas.frmEdit)
        Me.ucTabForms.Add(Karas.frmChat)
        'Me.ucTabForms.Add(Karas.frmOptions)
        'frmChat.Tag = True
        'Me.ShowFormAsMDIChild(frmChat)
        'Me.ShowFormAsMDIChild(frmOptions)
        'frmOptions.ShowMeWithSelectedPage(frmOptions.Page.Player)
        'Me.IsMdiContainer = False
        'Me.AddOwnedForm(frmEdit)
        'Dim f As New frmEdit
        'f.TopLevel = False
        'Me.Controls.Add(f)
        'f.Show()
        'f.Top = Me.MainMenuStrip.Top + Me.MainMenuStrip.Height
        'f.Left = 0
        'f.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        'f.d()
        'Me.ShowFormAsMDIChild(frmEdit)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click        

        Me.ucTabForms.Add(Karas.frmChat)
        '        frmChat.Tag = True
        '       Me.ShowFormAsMDIChild(frmChat)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        Me.ucTabForms.Add(Karas.frmOptions, Karas.ucTabForms.TabWorkMode.OnDeactivateClose)
        'Me.ShowFormAsMDIChild(frmOptions)
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Me.ucTabForms.Add(Karas.frmEdit, Karas.ucTabForms.TabWorkMode.Normal, BorderStyle.FixedSingle)
        'Me.ShowFormAsMDIChild(frmEdit)
    End Sub

    Public Sub ShowGame()
        Me.ucTabForms.Add(Karas.frmPlayGame)
    End Sub



End Class
