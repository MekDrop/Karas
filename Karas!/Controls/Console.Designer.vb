<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucConsole
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.rtbChat = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'rtbChat
        '
        Me.rtbChat.AutoWordSelection = True
        Me.rtbChat.BackColor = System.Drawing.SystemColors.Window
        Me.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtbChat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbChat.Location = New System.Drawing.Point(0, 0)
        Me.rtbChat.Name = "rtbChat"
        Me.rtbChat.ReadOnly = True
        Me.rtbChat.Size = New System.Drawing.Size(150, 150)
        Me.rtbChat.TabIndex = 1
        Me.rtbChat.Text = ""
        '
        'ucConsole
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.rtbChat)
        Me.Name = "ucConsole"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbChat As System.Windows.Forms.RichTextBox

End Class
