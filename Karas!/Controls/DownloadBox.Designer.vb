<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucDownloadBox
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
        Me.lvItems = New System.Windows.Forms.ListView
        Me.chName = New System.Windows.Forms.ColumnHeader
        Me.chFrom = New System.Windows.Forms.ColumnHeader
        Me.chProgress = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'lvItems
        '
        Me.lvItems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lvItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chName, Me.chFrom, Me.chProgress})
        Me.lvItems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvItems.FullRowSelect = True
        Me.lvItems.LabelWrap = False
        Me.lvItems.Location = New System.Drawing.Point(0, 0)
        Me.lvItems.Name = "lvItems"
        Me.lvItems.OwnerDraw = True
        Me.lvItems.ShowGroups = False
        Me.lvItems.Size = New System.Drawing.Size(252, 146)
        Me.lvItems.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvItems.TabIndex = 0
        Me.lvItems.UseCompatibleStateImageBehavior = False
        Me.lvItems.View = System.Windows.Forms.View.Details
        '
        'chName
        '
        Me.chName.Text = "Name"
        '
        'chFrom
        '
        Me.chFrom.Text = "From"
        '
        'chProgress
        '
        Me.chProgress.Text = "Progress"
        Me.chProgress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ucDownloadBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Controls.Add(Me.lvItems)
        Me.Name = "ucDownloadBox"
        Me.Size = New System.Drawing.Size(252, 146)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lvItems As System.Windows.Forms.ListView
    Friend WithEvents chName As System.Windows.Forms.ColumnHeader
    Friend WithEvents chFrom As System.Windows.Forms.ColumnHeader
    Friend WithEvents chProgress As System.Windows.Forms.ColumnHeader

End Class
