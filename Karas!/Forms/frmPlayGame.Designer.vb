<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPlayGame
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPlayGame))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbClose = New System.Windows.Forms.ToolStripButton
        Me.ToolStripContainer1 = New System.Windows.Forms.ToolStripContainer
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.lblWaiting = New System.Windows.Forms.Label
        Me.pbDraw = New System.Windows.Forms.PictureBox
        Me.ToolStrip1.SuspendLayout()
        Me.ToolStripContainer1.ContentPanel.SuspendLayout()
        Me.ToolStripContainer1.TopToolStripPanel.SuspendLayout()
        Me.ToolStripContainer1.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        CType(Me.pbDraw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbClose})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(69, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbClose
        '
        Me.tsbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tsbClose.CheckOnClick = True
        Me.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.tsbClose.Image = CType(resources.GetObject("tsbClose.Image"), System.Drawing.Image)
        Me.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbClose.Name = "tsbClose"
        Me.tsbClose.Size = New System.Drawing.Size(59, 22)
        Me.tsbClose.Text = "End Game"
        Me.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'ToolStripContainer1
        '
        '
        'ToolStripContainer1.ContentPanel
        '
        Me.ToolStripContainer1.ContentPanel.Controls.Add(Me.pnlContent)
        Me.ToolStripContainer1.ContentPanel.Size = New System.Drawing.Size(643, 408)
        Me.ToolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStripContainer1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStripContainer1.Name = "ToolStripContainer1"
        Me.ToolStripContainer1.Size = New System.Drawing.Size(643, 458)
        Me.ToolStripContainer1.TabIndex = 3
        Me.ToolStripContainer1.Text = "ToolStripContainer1"
        '
        'ToolStripContainer1.TopToolStripPanel
        '
        Me.ToolStripContainer1.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.BackColor = System.Drawing.Color.White
        Me.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlContent.Controls.Add(Me.lblWaiting)
        Me.pnlContent.Controls.Add(Me.pbDraw)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 0)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(643, 408)
        Me.pnlContent.TabIndex = 2
        '
        'lblWaiting
        '
        Me.lblWaiting.AutoSize = True
        Me.lblWaiting.Font = New System.Drawing.Font("Nina", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(186, Byte))
        Me.lblWaiting.Location = New System.Drawing.Point(165, 176)
        Me.lblWaiting.Name = "lblWaiting"
        Me.lblWaiting.Size = New System.Drawing.Size(320, 42)
        Me.lblWaiting.TabIndex = 2
        Me.lblWaiting.Text = "Waiting other player..."
        Me.lblWaiting.Visible = False
        '
        'pbDraw
        '
        Me.pbDraw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbDraw.Location = New System.Drawing.Point(0, 0)
        Me.pbDraw.Name = "pbDraw"
        Me.pbDraw.Size = New System.Drawing.Size(496, 404)
        Me.pbDraw.TabIndex = 1
        Me.pbDraw.TabStop = False
        '
        'frmPlayGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 458)
        Me.Controls.Add(Me.ToolStripContainer1)
        Me.Name = "frmPlayGame"
        Me.Text = "Play"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ToolStripContainer1.ContentPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.ResumeLayout(False)
        Me.ToolStripContainer1.TopToolStripPanel.PerformLayout()
        Me.ToolStripContainer1.ResumeLayout(False)
        Me.ToolStripContainer1.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        Me.pnlContent.PerformLayout()
        CType(Me.pbDraw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbClose As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripContainer1 As System.Windows.Forms.ToolStripContainer
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents lblWaiting As System.Windows.Forms.Label
    Friend WithEvents pbDraw As System.Windows.Forms.PictureBox
End Class
