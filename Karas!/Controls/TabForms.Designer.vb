<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTabForms
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
        Me.components = New System.ComponentModel.Container
        Me.tcTab = New System.Windows.Forms.TabControl
        Me.imlList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'tcTab
        '
        Me.tcTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcTab.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcTab.HotTrack = True
        Me.tcTab.ImageList = Me.imlList
        Me.tcTab.Location = New System.Drawing.Point(0, 0)
        Me.tcTab.Name = "tcTab"
        Me.tcTab.Padding = New System.Drawing.Point(0, 0)
        Me.tcTab.SelectedIndex = 0
        Me.tcTab.Size = New System.Drawing.Size(385, 331)
        Me.tcTab.TabIndex = 0
        '
        'imlList
        '
        Me.imlList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
        Me.imlList.ImageSize = New System.Drawing.Size(16, 16)
        Me.imlList.TransparentColor = System.Drawing.Color.Transparent
        '
        'ucTabForms
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tcTab)
        Me.Name = "ucTabForms"
        Me.Size = New System.Drawing.Size(385, 331)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tcTab As System.Windows.Forms.TabControl
    Friend WithEvents imlList As System.Windows.Forms.ImageList

End Class
