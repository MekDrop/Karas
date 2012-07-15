<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserInfo
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
        Me.pbUserPhoto = New System.Windows.Forms.PictureBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.rtbInfo = New System.Windows.Forms.RichTextBox
        CType(Me.pbUserPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbUserPhoto
        '
        Me.pbUserPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbUserPhoto.Location = New System.Drawing.Point(12, 12)
        Me.pbUserPhoto.Name = "pbUserPhoto"
        Me.pbUserPhoto.Size = New System.Drawing.Size(157, 157)
        Me.pbUserPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbUserPhoto.TabIndex = 10
        Me.pbUserPhoto.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnClose.Location = New System.Drawing.Point(176, 176)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 11
        Me.btnClose.Text = "Uždaryti"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'rtbInfo
        '
        Me.rtbInfo.BackColor = System.Drawing.SystemColors.Control
        Me.rtbInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbInfo.Location = New System.Drawing.Point(176, 12)
        Me.rtbInfo.Name = "rtbInfo"
        Me.rtbInfo.Size = New System.Drawing.Size(256, 157)
        Me.rtbInfo.TabIndex = 12
        Me.rtbInfo.Text = ""
        '
        'frmUserInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(444, 211)
        Me.Controls.Add(Me.rtbInfo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.pbUserPhoto)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUserInfo"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About user"
        CType(Me.pbUserPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pbUserPhoto As System.Windows.Forms.PictureBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents rtbInfo As System.Windows.Forms.RichTextBox

End Class
