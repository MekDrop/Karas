<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditShapeMove
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.nudX = New System.Windows.Forms.NumericUpDown
        Me.nudY = New System.Windows.Forms.NumericUpDown
        Me.btnGerai = New System.Windows.Forms.Button
        CType(Me.nudX, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudY, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(504, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Įrašę laukeliuose reikšmes ir paspaudę ""Gerai"", galite perkelti kontrolę, per X i" & _
            "r Y pikselių į pasirinktą vietą."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "X:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Y:"
        '
        'nudX
        '
        Me.nudX.Location = New System.Drawing.Point(36, 40)
        Me.nudX.Name = "nudX"
        Me.nudX.Size = New System.Drawing.Size(120, 20)
        Me.nudX.TabIndex = 4
        '
        'nudY
        '
        Me.nudY.Location = New System.Drawing.Point(36, 63)
        Me.nudY.Name = "nudY"
        Me.nudY.Size = New System.Drawing.Size(120, 20)
        Me.nudY.TabIndex = 5
        '
        'btnGerai
        '
        Me.btnGerai.Location = New System.Drawing.Point(162, 60)
        Me.btnGerai.Name = "btnGerai"
        Me.btnGerai.Size = New System.Drawing.Size(75, 23)
        Me.btnGerai.TabIndex = 6
        Me.btnGerai.Text = "Gerai"
        Me.btnGerai.UseVisualStyleBackColor = True
        '
        'frmEditShapeMove
        '
        Me.AcceptButton = Me.btnGerai
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 273)
        Me.Controls.Add(Me.btnGerai)
        Me.Controls.Add(Me.nudY)
        Me.Controls.Add(Me.nudX)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmEditShapeMove"
        Me.Text = "Move"
        CType(Me.nudX, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudY, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nudX As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudY As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnGerai As System.Windows.Forms.Button
End Class
