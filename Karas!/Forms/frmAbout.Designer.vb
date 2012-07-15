<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAbout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                ' Sunaikiname OpenGL komponentą
                Me.sogArea.DestroyContexts()
                ' Panaikiname visus kitus komponentus
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
        Me.components = New System.ComponentModel.Container
        Me.sogArea = New Tao.Platform.Windows.SimpleOpenGlControl
        Me.tmrDraw = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'sogArea
        '
        Me.sogArea.AccumBits = CType(0, Byte)
        Me.sogArea.AutoCheckErrors = False
        Me.sogArea.AutoFinish = False
        Me.sogArea.AutoMakeCurrent = True
        Me.sogArea.AutoSwapBuffers = True
        Me.sogArea.BackColor = System.Drawing.Color.Black
        Me.sogArea.ColorBits = CType(32, Byte)
        Me.sogArea.DepthBits = CType(16, Byte)
        Me.sogArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sogArea.Location = New System.Drawing.Point(0, 0)
        Me.sogArea.Name = "sogArea"
        Me.sogArea.Size = New System.Drawing.Size(492, 394)
        Me.sogArea.StencilBits = CType(0, Byte)
        Me.sogArea.TabIndex = 0
        '
        'tmrDraw
        '
        Me.tmrDraw.Enabled = True
        '
        'frmAbout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 394)
        Me.Controls.Add(Me.sogArea)
        Me.Name = "frmAbout"
        Me.Text = "About"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents sogArea As Tao.Platform.Windows.SimpleOpenGlControl

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Friend WithEvents tmrDraw As System.Windows.Forms.Timer
End Class
