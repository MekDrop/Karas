<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintDialog))
        Me.ppcPreview = New System.Windows.Forms.PrintPreviewControl
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.tscZoom = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.tslPrinter = New System.Windows.Forms.ToolStripLabel
        Me.tscPrinters = New System.Windows.Forms.ToolStripComboBox
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ppcPreview
        '
        Me.ppcPreview.AutoZoom = False
        Me.ppcPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ppcPreview.Location = New System.Drawing.Point(0, 0)
        Me.ppcPreview.Name = "ppcPreview"
        Me.ppcPreview.Size = New System.Drawing.Size(460, 248)
        Me.ppcPreview.TabIndex = 0
        Me.ppcPreview.UseAntiAlias = True
        Me.ppcPreview.Zoom = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.tscZoom, Me.ToolStripSeparator1, Me.tslPrinter, Me.tscPrinters, Me.PrintToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(444, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(37, 22)
        Me.ToolStripLabel1.Text = "Zoom:"
        '
        'tscZoom
        '
        Me.tscZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscZoom.Name = "tscZoom"
        Me.tscZoom.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'tslPrinter
        '
        Me.tslPrinter.Name = "tslPrinter"
        Me.tslPrinter.Size = New System.Drawing.Size(43, 22)
        Me.tslPrinter.Text = "Printer:"
        '
        'tscPrinters
        '
        Me.tscPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscPrinters.Name = "tscPrinters"
        Me.tscPrinters.Size = New System.Drawing.Size(200, 25)
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.ppcPreview)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(460, 248)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(460, 273)
        Me.ToolStripContainer1.TabIndex = 2
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'frmPrintDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(460, 273)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Name = "frmPrintDialog"
        Me.Text = "Print Dialog"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ppcPreview As System.Windows.Forms.PrintPreviewControl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tscZoom As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tslPrinter As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tscPrinters As System.Windows.Forms.ToolStripComboBox
End Class
