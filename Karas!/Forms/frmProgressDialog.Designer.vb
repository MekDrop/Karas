<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProgressDialog
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.pbOverall = New System.Windows.Forms.ProgressBar
        Me.Label2 = New System.Windows.Forms.Label
        Me.pbCurrent = New System.Windows.Forms.ProgressBar
        Me.lblCAL = New System.Windows.Forms.Label
        Me.lblCurrentAction = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Viskas:"
        '
        'pbOverall
        '
        Me.pbOverall.Location = New System.Drawing.Point(15, 35)
        Me.pbOverall.Name = "pbOverall"
        Me.pbOverall.Size = New System.Drawing.Size(378, 23)
        Me.pbOverall.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 73)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Dabartinis žingsnis:"
        '
        'pbCurrent
        '
        Me.pbCurrent.Location = New System.Drawing.Point(15, 89)
        Me.pbCurrent.Name = "pbCurrent"
        Me.pbCurrent.Size = New System.Drawing.Size(378, 23)
        Me.pbCurrent.TabIndex = 3
        '
        'lblCAL
        '
        Me.lblCAL.AutoSize = True
        Me.lblCAL.Location = New System.Drawing.Point(12, 129)
        Me.lblCAL.Name = "lblCAL"
        Me.lblCAL.Size = New System.Drawing.Size(108, 13)
        Me.lblCAL.TabIndex = 4
        Me.lblCAL.Text = "Atliekamas veiksmas:"
        '
        'lblCurrentAction
        '
        Me.lblCurrentAction.AutoSize = True
        Me.lblCurrentAction.Location = New System.Drawing.Point(14, 151)
        Me.lblCurrentAction.Name = "lblCurrentAction"
        Me.lblCurrentAction.Size = New System.Drawing.Size(16, 13)
        Me.lblCurrentAction.TabIndex = 5
        Me.lblCurrentAction.Text = "..."
        '
        'frmProgressDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(607, 244)
        Me.Controls.Add(Me.lblCurrentAction)
        Me.Controls.Add(Me.lblCAL)
        Me.Controls.Add(Me.pbCurrent)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pbOverall)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmProgressDialog"
        Me.Text = "Progress"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pbOverall As System.Windows.Forms.ProgressBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pbCurrent As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCAL As System.Windows.Forms.Label
    Friend WithEvents lblCurrentAction As System.Windows.Forms.Label
End Class
