<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEdit))
        Me.tsDrawingTools = New System.Windows.Forms.ToolStrip
        Me.tsbCursor = New System.Windows.Forms.ToolStripButton
        Me.tsbWeapon = New System.Windows.Forms.ToolStripButton
        Me.tsbCroper = New System.Windows.Forms.ToolStripButton
        Me.tsbWall = New System.Windows.Forms.ToolStripButton
        Me.tscEdit = New System.Windows.Forms.ToolStripContainer
        Me.pnlContent = New System.Windows.Forms.Panel
        Me.pbDraw = New System.Windows.Forms.PictureBox
        Me.tsToolbar = New System.Windows.Forms.ToolStrip
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.CopyToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.PasteToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tslMoneyText = New System.Windows.Forms.ToolStripLabel
        Me.tslMoneyCount = New System.Windows.Forms.ToolStripLabel
        Me.tslMoneyC = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.tslItemsCount = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel
        Me.tslSaved = New System.Windows.Forms.ToolStripLabel
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.tscZoom = New System.Windows.Forms.ToolStripComboBox
        Me.cmsForm = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.MoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.tsDrawingTools.SuspendLayout()
        Me.tscEdit.ContentPanel.SuspendLayout()
        Me.tscEdit.LeftToolStripPanel.SuspendLayout()
        Me.tscEdit.TopToolStripPanel.SuspendLayout()
        Me.tscEdit.SuspendLayout()
        Me.pnlContent.SuspendLayout()
        CType(Me.pbDraw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tsToolbar.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.cmsForm.SuspendLayout()
        Me.SuspendLayout()
        '
        'tsDrawingTools
        '
        Me.tsDrawingTools.Dock = System.Windows.Forms.DockStyle.None
        Me.tsDrawingTools.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbCursor, Me.tsbWeapon, Me.tsbCroper, Me.tsbWall})
        Me.tsDrawingTools.Location = New System.Drawing.Point(0, 3)
        Me.tsDrawingTools.Name = "tsDrawingTools"
        Me.tsDrawingTools.Size = New System.Drawing.Size(38, 126)
        Me.tsDrawingTools.TabIndex = 0
        Me.tsDrawingTools.Text = "ToolStrip1"
        '
        'tsbCursor
        '
        Me.tsbCursor.Checked = True
        Me.tsbCursor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsbCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbCursor.Image = CType(resources.GetObject("tsbCursor.Image"), System.Drawing.Image)
        Me.tsbCursor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbCursor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCursor.Name = "tsbCursor"
        Me.tsbCursor.Size = New System.Drawing.Size(36, 36)
        Me.tsbCursor.Text = "Cursor"
        '
        'tsbWeapon
        '
        Me.tsbWeapon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbWeapon.Image = CType(resources.GetObject("tsbWeapon.Image"), System.Drawing.Image)
        Me.tsbWeapon.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbWeapon.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbWeapon.Name = "tsbWeapon"
        Me.tsbWeapon.Size = New System.Drawing.Size(36, 36)
        Me.tsbWeapon.Text = "Weapon"
        '
        'tsbCroper
        '
        Me.tsbCroper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbCroper.Image = CType(resources.GetObject("tsbCroper.Image"), System.Drawing.Image)
        Me.tsbCroper.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbCroper.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbCroper.Name = "tsbCroper"
        Me.tsbCroper.Size = New System.Drawing.Size(36, 36)
        Me.tsbCroper.Text = "Cut"
        Me.tsbCroper.Visible = False
        '
        'tsbWall
        '
        Me.tsbWall.AutoSize = False
        Me.tsbWall.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbWall.Image = CType(resources.GetObject("tsbWall.Image"), System.Drawing.Image)
        Me.tsbWall.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.tsbWall.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbWall.Name = "tsbWall"
        Me.tsbWall.Size = New System.Drawing.Size(37, 36)
        Me.tsbWall.Text = "Wall"
        '
        'tscEdit
        '
        '
        'tscEdit.ContentPanel
        '
        Me.tscEdit.ContentPanel.Controls.Add(Me.pnlContent)
        Me.tscEdit.ContentPanel.Size = New System.Drawing.Size(755, 497)
        Me.tscEdit.Dock = System.Windows.Forms.DockStyle.Fill
        '
        'tscEdit.LeftToolStripPanel
        '
        Me.tscEdit.LeftToolStripPanel.Controls.Add(Me.tsDrawingTools)
        Me.tscEdit.Location = New System.Drawing.Point(0, 0)
        Me.tscEdit.Name = "tscEdit"
        Me.tscEdit.Size = New System.Drawing.Size(793, 547)
        Me.tscEdit.TabIndex = 1
        Me.tscEdit.Text = "ToolStripContainer1"
        '
        'tscEdit.TopToolStripPanel
        '
        Me.tscEdit.TopToolStripPanel.Controls.Add(Me.tsToolbar)
        Me.tscEdit.TopToolStripPanel.Controls.Add(Me.ToolStrip1)
        '
        'pnlContent
        '
        Me.pnlContent.AutoScroll = True
        Me.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlContent.Controls.Add(Me.pbDraw)
        Me.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlContent.Location = New System.Drawing.Point(0, 0)
        Me.pnlContent.Name = "pnlContent"
        Me.pnlContent.Size = New System.Drawing.Size(755, 497)
        Me.pnlContent.TabIndex = 0
        '
        'pbDraw
        '
        Me.pbDraw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbDraw.Location = New System.Drawing.Point(137, 0)
        Me.pbDraw.Name = "pbDraw"
        Me.pbDraw.Size = New System.Drawing.Size(480, 451)
        Me.pbDraw.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbDraw.TabIndex = 1
        Me.pbDraw.TabStop = False
        '
        'tsToolbar
        '
        Me.tsToolbar.Dock = System.Windows.Forms.DockStyle.None
        Me.tsToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.PrintToolStripButton, Me.toolStripSeparator, Me.CutToolStripButton, Me.CopyToolStripButton, Me.PasteToolStripButton})
        Me.tsToolbar.Location = New System.Drawing.Point(3, 0)
        Me.tsToolbar.Name = "tsToolbar"
        Me.tsToolbar.Size = New System.Drawing.Size(177, 25)
        Me.tsToolbar.TabIndex = 7
        Me.tsToolbar.Text = "ToolStrip2"
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.NewToolStripButton.Text = "&New"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.OpenToolStripButton.Text = "&Open"
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.SaveToolStripButton.Text = "&Save"
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
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.CutToolStripButton.Text = "C&ut"
        '
        'CopyToolStripButton
        '
        Me.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CopyToolStripButton.Image = CType(resources.GetObject("CopyToolStripButton.Image"), System.Drawing.Image)
        Me.CopyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyToolStripButton.Name = "CopyToolStripButton"
        Me.CopyToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.CopyToolStripButton.Text = "&Copy"
        '
        'PasteToolStripButton
        '
        Me.PasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PasteToolStripButton.Image = CType(resources.GetObject("PasteToolStripButton.Image"), System.Drawing.Image)
        Me.PasteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteToolStripButton.Name = "PasteToolStripButton"
        Me.PasteToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.PasteToolStripButton.Text = "&Paste"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tslMoneyText, Me.tslMoneyCount, Me.tslMoneyC, Me.ToolStripSeparator1, Me.ToolStripLabel1, Me.tslItemsCount, Me.ToolStripSeparator2, Me.ToolStripLabel3, Me.tslSaved, Me.ToolStripSeparator3, Me.tscZoom})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(299, 25)
        Me.ToolStrip1.TabIndex = 0
        '
        'tslMoneyText
        '
        Me.tslMoneyText.Name = "tslMoneyText"
        Me.tslMoneyText.Size = New System.Drawing.Size(43, 22)
        Me.tslMoneyText.Text = "Money:"
        '
        'tslMoneyCount
        '
        Me.tslMoneyCount.Name = "tslMoneyCount"
        Me.tslMoneyCount.Size = New System.Drawing.Size(37, 22)
        Me.tslMoneyCount.Text = "10000"
        '
        'tslMoneyC
        '
        Me.tslMoneyC.Name = "tslMoneyC"
        Me.tslMoneyC.Size = New System.Drawing.Size(18, 22)
        Me.tslMoneyC.Text = "LT"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(38, 22)
        Me.ToolStripLabel1.Text = "Items:"
        '
        'tslItemsCount
        '
        Me.tslItemsCount.Name = "tslItemsCount"
        Me.tslItemsCount.Size = New System.Drawing.Size(43, 22)
        Me.tslItemsCount.Text = "000000"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(63, 22)
        Me.ToolStripLabel3.Text = "Last saved:"
        '
        'tslSaved
        '
        Me.tslSaved.Name = "tslSaved"
        Me.tslSaved.Size = New System.Drawing.Size(35, 22)
        Me.tslSaved.Text = "#no#"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        Me.ToolStripSeparator3.Visible = False
        '
        'tscZoom
        '
        Me.tscZoom.AutoToolTip = True
        Me.tscZoom.DropDownHeight = 38
        Me.tscZoom.DropDownWidth = 38
        Me.tscZoom.IntegralHeight = False
        Me.tscZoom.MaxDropDownItems = 10
        Me.tscZoom.Name = "tscZoom"
        Me.tscZoom.Size = New System.Drawing.Size(75, 25)
        Me.tscZoom.ToolTipText = "Zoom"
        Me.tscZoom.Visible = False
        '
        'cmsForm
        '
        Me.cmsForm.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.PasteToolStripMenuItem, Me.ToolStripSeparator4, Me.MoveToolStripMenuItem})
        Me.cmsForm.Name = "cmsForm"
        Me.cmsForm.Size = New System.Drawing.Size(151, 120)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.CutToolStripMenuItem.Text = "&Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.CopyToolStripMenuItem.Text = "&Copy"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.DeleteToolStripMenuItem.Text = "&Delete"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.PasteToolStripMenuItem.Text = "&Paste"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(147, 6)
        '
        'MoveToolStripMenuItem
        '
        Me.MoveToolStripMenuItem.Name = "MoveToolStripMenuItem"
        Me.MoveToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.MoveToolStripMenuItem.Text = "&Move to..."
        '
        'frmEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ClientSize = New System.Drawing.Size(793, 547)
        Me.Controls.Add(Me.tscEdit)
        Me.KeyPreview = True
        Me.Name = "frmEdit"
        Me.Text = "Edit"
        Me.tsDrawingTools.ResumeLayout(False)
        Me.tsDrawingTools.PerformLayout()
        Me.tscEdit.ContentPanel.ResumeLayout(False)
        Me.tscEdit.LeftToolStripPanel.ResumeLayout(False)
        Me.tscEdit.LeftToolStripPanel.PerformLayout()
        Me.tscEdit.TopToolStripPanel.ResumeLayout(False)
        Me.tscEdit.TopToolStripPanel.PerformLayout()
        Me.tscEdit.ResumeLayout(False)
        Me.tscEdit.PerformLayout()
        Me.pnlContent.ResumeLayout(False)
        CType(Me.pbDraw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tsToolbar.ResumeLayout(False)
        Me.tsToolbar.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.cmsForm.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tsDrawingTools As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbCursor As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbWeapon As System.Windows.Forms.ToolStripButton
    Friend WithEvents tscEdit As System.Windows.Forms.ToolStripContainer
    Friend WithEvents tsbCroper As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbWall As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tslMoneyText As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslMoneyCount As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslMoneyC As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslItemsCount As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel3 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tslSaved As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlContent As System.Windows.Forms.Panel
    Friend WithEvents pbDraw As System.Windows.Forms.PictureBox
    Friend WithEvents tscZoom As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents cmsForm As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsToolbar As System.Windows.Forms.ToolStrip
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents CopyToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PasteToolStripButton As System.Windows.Forms.ToolStripButton
End Class
