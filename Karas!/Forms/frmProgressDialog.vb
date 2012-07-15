Public Class frmProgressDialog

    Sub New(Optional ByVal Steps As Integer = 1)

        Me.InitializeComponent()

        Me.pbOverall.Minimum = 0
        Me.pbOverall.Maximum = Steps
        Me.pbCurrent.Minimum = 0
        Me.pbCurrent.Value = 0
        Me.pbOverall.Value = 0

        frmMain.ucTabForms.Add(Me, ucTabForms.TabWorkMode.Normal)
    End Sub

    Sub StartFromBegining(Optional ByVal Max As Integer = 100, Optional ByVal Desc As String = "")
        Me.pbCurrent.Maximum = Max
        Me.pbCurrent.Value = 0
        Me.pbCurrent.Value += 1
        Me.lblCurrentAction.Visible = Desc <> ""
        Me.lblCAL.Visible = Me.lblCurrentAction.Visible
        Me.lblCurrentAction.Text = Desc
    End Sub

    Sub StartFromBegining(ByVal Desc As String)
        Me.StartFromBegining(1)
        Me.DoStep(Desc)
    End Sub

    Sub DoStep(Optional ByVal Desc As String = "")
        If (Me.pbCurrent.Value + 1) >= Me.pbCurrent.Maximum Then
            If (Me.pbOverall.Value + 1) >= Me.pbOverall.Maximum Then
                Me.Close()
                Exit Sub
            End If
            Me.pbOverall.Value += 1
            Exit Sub
        End If
        Me.pbCurrent.Value += 1
        Me.lblCurrentAction.Visible = Desc <> ""
        Me.lblCAL.Visible = Me.lblCurrentAction.Visible
        Me.lblCurrentAction.Text = Desc
        My.Application.DoEvents()
    End Sub

End Class