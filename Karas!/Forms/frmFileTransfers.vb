Public Class frmFileTransfers

    Private Overloads Sub Show()
        frmMain.ucTabForms.Add(Me)
    End Sub

    Private Sub ucDownloadBox_FirstDownloadStarted() Handles ucDownloadBox.FirstDownloadStarted
        Me.Show()
    End Sub

    Private Sub frmFileTransfers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class