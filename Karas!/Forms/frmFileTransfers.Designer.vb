<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFileTransfers
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim FileTransferCollection1 As Karas.FileTransferCollection = New Karas.FileTransferCollection
        Me.ucDownloadBox = New Karas.ucDownloadBox
        Me.SuspendLayout()
        '
        'ucDownloadBox
        '
        Me.ucDownloadBox.AutoClear = True
        Me.ucDownloadBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ucDownloadBox.Dock = System.Windows.Forms.DockStyle.Fill
        FileTransferCollection1.Timeout = System.TimeSpan.Parse("00:05:00")
        Me.ucDownloadBox.FileTransfers = FileTransferCollection1
        Me.ucDownloadBox.Location = New System.Drawing.Point(0, 0)
        Me.ucDownloadBox.Name = "ucDownloadBox"
        Me.ucDownloadBox.Size = New System.Drawing.Size(292, 273)
        Me.ucDownloadBox.TabIndex = 212125
        '
        'frmFileTransfers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.ucDownloadBox)
        Me.Name = "frmFileTransfers"
        Me.Text = "File Transfers"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents ucDownloadBox As Karas.ucDownloadBox
End Class
