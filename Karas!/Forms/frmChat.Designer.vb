<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChat
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.lvUsers = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmsUserList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowUserInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsmRequestGamePlay1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtInput = New System.Windows.Forms.TextBox()
        Me.ucChat = New Karas.ucConsole()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.cmsUserList.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.SplitContainer2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtInput, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(632, 453)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.ucChat)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.lvUsers)
        Me.SplitContainer2.Size = New System.Drawing.Size(626, 421)
        Me.SplitContainer2.SplitterDistance = 439
        Me.SplitContainer2.TabIndex = 212122
        Me.SplitContainer2.TabStop = False
        '
        'lvUsers
        '
        Me.lvUsers.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvUsers.ContextMenuStrip = Me.cmsUserList
        Me.lvUsers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvUsers.FullRowSelect = True
        Me.lvUsers.GridLines = True
        Me.lvUsers.LabelWrap = False
        Me.lvUsers.Location = New System.Drawing.Point(0, 0)
        Me.lvUsers.MultiSelect = False
        Me.lvUsers.Name = "lvUsers"
        Me.lvUsers.ShowGroups = False
        Me.lvUsers.Size = New System.Drawing.Size(183, 421)
        Me.lvUsers.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvUsers.TabIndex = 0
        Me.lvUsers.UseCompatibleStateImageBehavior = False
        Me.lvUsers.View = System.Windows.Forms.View.List
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Width = 178
        '
        'cmsUserList
        '
        Me.cmsUserList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowUserInfoToolStripMenuItem, Me.ToolStripSeparator1, Me.tsmRequestGamePlay1})
        Me.cmsUserList.Name = "cmsUserList"
        Me.cmsUserList.Size = New System.Drawing.Size(163, 54)
        '
        'ShowUserInfoToolStripMenuItem
        '
        Me.ShowUserInfoToolStripMenuItem.Name = "ShowUserInfoToolStripMenuItem"
        Me.ShowUserInfoToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.ShowUserInfoToolStripMenuItem.Text = "&Show User Info..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(159, 6)
        '
        'tsmRequestGamePlay1
        '
        Me.tsmRequestGamePlay1.Name = "tsmRequestGamePlay1"
        Me.tsmRequestGamePlay1.Size = New System.Drawing.Size(162, 22)
        Me.tsmRequestGamePlay1.Text = "&Request Game"
        '
        'txtInput
        '
        Me.txtInput.AcceptsReturn = True
        Me.txtInput.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.txtInput.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInput.Location = New System.Drawing.Point(3, 430)
        Me.txtInput.Multiline = True
        Me.txtInput.Name = "txtInput"
        Me.txtInput.Size = New System.Drawing.Size(626, 20)
        Me.txtInput.TabIndex = 212123
        '
        'ucChat
        '
        Me.ucChat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ucChat.FocusControl = Nothing
        Me.ucChat.Location = New System.Drawing.Point(0, 0)
        Me.ucChat.Name = "ucChat"
        Me.ucChat.Size = New System.Drawing.Size(439, 421)
        Me.ucChat.TabIndex = 0
        '
        'frmChat
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.ContextMenuStrip = Me.cmsUserList
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "frmChat"
        Me.Text = "Chat"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.cmsUserList.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents txtInput As System.Windows.Forms.TextBox
    Friend WithEvents ucChat As Karas.ucConsole
    Friend WithEvents lvUsers As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Public WithEvents cmsUserList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowUserInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsmRequestGamePlay1 As System.Windows.Forms.ToolStripMenuItem
End Class
