Public Class frmEditShapeMove

    Private rez As frmEditShapeMove.Results

    Public Delegate Sub Results(ByVal X As Integer, ByVal Y As Integer)

    Sub New(ByRef DrawArea As PictureBox, ByRef rez As Results)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.nudX.Minimum = 0
        Me.nudY.Minimum = 0
        Me.nudX.Maximum = DrawArea.Width - 2
        Me.nudY.Maximum = DrawArea.Height - 2
        Me.rez = rez
        frmMain.ucTabForms.Add(Me, ucTabForms.TabWorkMode.OnDeactivateClose)
    End Sub

    Private Sub btnGerai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGerai.Click
        Me.rez.Invoke(Me.nudX.Value, Me.nudY.Value)
        Me.Close()
    End Sub

End Class