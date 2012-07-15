<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
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
        Me.tpNodes = New System.Windows.Forms.TabPage
        Me.grpNodesOptions = New System.Windows.Forms.GroupBox
        Me.chkAutoAddToNodesList = New System.Windows.Forms.CheckBox
        Me.chkRemoveBadNodes = New System.Windows.Forms.CheckBox
        Me.chkConnectOnStartup = New System.Windows.Forms.CheckBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnAddNode = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.mtbPort = New System.Windows.Forms.MaskedTextBox
        Me.lvItems = New System.Windows.Forms.ListView
        Me.chIP = New System.Windows.Forms.ColumnHeader
        Me.chPort = New System.Windows.Forms.ColumnHeader
        Me.mtbDefaultPort = New System.Windows.Forms.MaskedTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.tpPlayer = New System.Windows.Forms.TabPage
        Me.btnClearAvatar = New System.Windows.Forms.Button
        Me.btnLoadNewPhoto = New System.Windows.Forms.Button
        Me.pbUserPhoto = New System.Windows.Forms.PictureBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtEmail = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpBirthdate = New System.Windows.Forms.DateTimePicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtNickName = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.tcSettings = New System.Windows.Forms.TabControl
        Me.tpOther = New System.Windows.Forms.TabPage
        Me.gpOthersOptions = New System.Windows.Forms.GroupBox
        Me.chkDebugMode = New System.Windows.Forms.CheckBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.pbMap = New System.Windows.Forms.PictureBox
        Me.btnLoadMap = New System.Windows.Forms.Button
        Me.tpNodes.SuspendLayout()
        Me.grpNodesOptions.SuspendLayout()
        Me.tpPlayer.SuspendLayout()
        CType(Me.pbUserPhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tcSettings.SuspendLayout()
        Me.tpOther.SuspendLayout()
        Me.gpOthersOptions.SuspendLayout()
        CType(Me.pbMap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tpNodes
        '
        Me.tpNodes.AutoScroll = True
        Me.tpNodes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpNodes.Controls.Add(Me.grpNodesOptions)
        Me.tpNodes.Controls.Add(Me.Label8)
        Me.tpNodes.Controls.Add(Me.btnAddNode)
        Me.tpNodes.Controls.Add(Me.TextBox1)
        Me.tpNodes.Controls.Add(Me.Label4)
        Me.tpNodes.Controls.Add(Me.mtbPort)
        Me.tpNodes.Controls.Add(Me.lvItems)
        Me.tpNodes.Controls.Add(Me.mtbDefaultPort)
        Me.tpNodes.Controls.Add(Me.Label3)
        Me.tpNodes.Controls.Add(Me.Label2)
        Me.tpNodes.Location = New System.Drawing.Point(4, 25)
        Me.tpNodes.Name = "tpNodes"
        Me.tpNodes.Padding = New System.Windows.Forms.Padding(3)
        Me.tpNodes.Size = New System.Drawing.Size(527, 569)
        Me.tpNodes.TabIndex = 1
        Me.tpNodes.Text = "Jungtys"
        Me.tpNodes.UseVisualStyleBackColor = True
        '
        'grpNodesOptions
        '
        Me.grpNodesOptions.Controls.Add(Me.chkAutoAddToNodesList)
        Me.grpNodesOptions.Controls.Add(Me.chkRemoveBadNodes)
        Me.grpNodesOptions.Controls.Add(Me.chkConnectOnStartup)
        Me.grpNodesOptions.Location = New System.Drawing.Point(133, 195)
        Me.grpNodesOptions.Name = "grpNodesOptions"
        Me.grpNodesOptions.Size = New System.Drawing.Size(338, 78)
        Me.grpNodesOptions.TabIndex = 16
        Me.grpNodesOptions.TabStop = False
        '
        'chkAutoAddToNodesList
        '
        Me.chkAutoAddToNodesList.AutoSize = True
        Me.chkAutoAddToNodesList.Location = New System.Drawing.Point(6, 56)
        Me.chkAutoAddToNodesList.Name = "chkAutoAddToNodesList"
        Me.chkAutoAddToNodesList.Size = New System.Drawing.Size(186, 17)
        Me.chkAutoAddToNodesList.TabIndex = 18
        Me.chkAutoAddToNodesList.Text = "Automatiškai pildyti jungčių sąrašą"
        Me.chkAutoAddToNodesList.UseVisualStyleBackColor = True
        '
        'chkRemoveBadNodes
        '
        Me.chkRemoveBadNodes.AutoSize = True
        Me.chkRemoveBadNodes.Location = New System.Drawing.Point(6, 33)
        Me.chkRemoveBadNodes.Name = "chkRemoveBadNodes"
        Me.chkRemoveBadNodes.Size = New System.Drawing.Size(194, 17)
        Me.chkRemoveBadNodes.TabIndex = 17
        Me.chkRemoveBadNodes.Text = "Automatiškai pašalinti blogas jungtis"
        Me.chkRemoveBadNodes.UseVisualStyleBackColor = True
        '
        'chkConnectOnStartup
        '
        Me.chkConnectOnStartup.AutoSize = True
        Me.chkConnectOnStartup.Location = New System.Drawing.Point(6, 10)
        Me.chkConnectOnStartup.Name = "chkConnectOnStartup"
        Me.chkConnectOnStartup.Size = New System.Drawing.Size(171, 17)
        Me.chkConnectOnStartup.TabIndex = 16
        Me.chkConnectOnStartup.Text = "Automatiškai jungtis startuojant"
        Me.chkConnectOnStartup.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(62, 205)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(65, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Pasirinkimai:"
        '
        'btnAddNode
        '
        Me.btnAddNode.AutoEllipsis = True
        Me.btnAddNode.Location = New System.Drawing.Point(447, 4)
        Me.btnAddNode.Name = "btnAddNode"
        Me.btnAddNode.Size = New System.Drawing.Size(24, 22)
        Me.btnAddNode.TabIndex = 12
        Me.btnAddNode.Text = "+"
        Me.btnAddNode.UseCompatibleTextRendering = True
        Me.btnAddNode.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(133, 6)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(216, 20)
        Me.TextBox1.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(355, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = ":"
        '
        'mtbPort
        '
        Me.mtbPort.HidePromptOnLeave = True
        Me.mtbPort.Location = New System.Drawing.Point(371, 6)
        Me.mtbPort.Mask = "00009"
        Me.mtbPort.Name = "mtbPort"
        Me.mtbPort.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mtbPort.Size = New System.Drawing.Size(70, 20)
        Me.mtbPort.TabIndex = 9
        '
        'lvItems
        '
        Me.lvItems.AllowColumnReorder = True
        Me.lvItems.CheckBoxes = True
        Me.lvItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chIP, Me.chPort})
        Me.lvItems.LabelEdit = True
        Me.lvItems.LabelWrap = False
        Me.lvItems.Location = New System.Drawing.Point(133, 32)
        Me.lvItems.MultiSelect = False
        Me.lvItems.Name = "lvItems"
        Me.lvItems.ShowGroups = False
        Me.lvItems.Size = New System.Drawing.Size(338, 131)
        Me.lvItems.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvItems.TabIndex = 8
        Me.lvItems.UseCompatibleStateImageBehavior = False
        Me.lvItems.View = System.Windows.Forms.View.Details
        '
        'chIP
        '
        Me.chIP.Text = "Adresas"
        Me.chIP.Width = 200
        '
        'chPort
        '
        Me.chPort.Text = "Portas"
        '
        'mtbDefaultPort
        '
        Me.mtbDefaultPort.HidePromptOnLeave = True
        Me.mtbDefaultPort.Location = New System.Drawing.Point(133, 169)
        Me.mtbDefaultPort.Mask = "00009"
        Me.mtbDefaultPort.Name = "mtbDefaultPort"
        Me.mtbDefaultPort.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.mtbDefaultPort.Size = New System.Drawing.Size(67, 20)
        Me.mtbDefaultPort.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 169)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(124, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Portas (pagal nutylėjimą):"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(79, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Adresas:"
        '
        'tpPlayer
        '
        Me.tpPlayer.AutoScroll = True
        Me.tpPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpPlayer.Controls.Add(Me.btnLoadMap)
        Me.tpPlayer.Controls.Add(Me.pbMap)
        Me.tpPlayer.Controls.Add(Me.Label10)
        Me.tpPlayer.Controls.Add(Me.btnClearAvatar)
        Me.tpPlayer.Controls.Add(Me.btnLoadNewPhoto)
        Me.tpPlayer.Controls.Add(Me.pbUserPhoto)
        Me.tpPlayer.Controls.Add(Me.Label7)
        Me.tpPlayer.Controls.Add(Me.txtEmail)
        Me.tpPlayer.Controls.Add(Me.Label6)
        Me.tpPlayer.Controls.Add(Me.dtpBirthdate)
        Me.tpPlayer.Controls.Add(Me.Label5)
        Me.tpPlayer.Controls.Add(Me.txtNickName)
        Me.tpPlayer.Controls.Add(Me.Label1)
        Me.tpPlayer.Location = New System.Drawing.Point(4, 25)
        Me.tpPlayer.Name = "tpPlayer"
        Me.tpPlayer.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPlayer.Size = New System.Drawing.Size(527, 569)
        Me.tpPlayer.TabIndex = 0
        Me.tpPlayer.Text = "Žaidėjas(-a)"
        Me.tpPlayer.UseVisualStyleBackColor = True
        '
        'btnClearAvatar
        '
        Me.btnClearAvatar.Location = New System.Drawing.Point(124, 256)
        Me.btnClearAvatar.Name = "btnClearAvatar"
        Me.btnClearAvatar.Size = New System.Drawing.Size(75, 23)
        Me.btnClearAvatar.TabIndex = 31
        Me.btnClearAvatar.Text = "Išvalyti"
        Me.btnClearAvatar.UseVisualStyleBackColor = True
        '
        'btnLoadNewPhoto
        '
        Me.btnLoadNewPhoto.Location = New System.Drawing.Point(206, 256)
        Me.btnLoadNewPhoto.Name = "btnLoadNewPhoto"
        Me.btnLoadNewPhoto.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadNewPhoto.TabIndex = 30
        Me.btnLoadNewPhoto.Text = "Įkelti"
        Me.btnLoadNewPhoto.UseVisualStyleBackColor = True
        '
        'pbUserPhoto
        '
        Me.pbUserPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbUserPhoto.Location = New System.Drawing.Point(124, 93)
        Me.pbUserPhoto.Name = "pbUserPhoto"
        Me.pbUserPhoto.Size = New System.Drawing.Size(157, 157)
        Me.pbUserPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pbUserPhoto.TabIndex = 29
        Me.pbUserPhoto.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(55, 93)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(63, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Nuotrauka: "
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(124, 67)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(158, 20)
        Me.txtEmail.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Elektroninis paštas: "
        '
        'dtpBirthdate
        '
        Me.dtpBirthdate.Location = New System.Drawing.Point(124, 41)
        Me.dtpBirthdate.Name = "dtpBirthdate"
        Me.dtpBirthdate.ShowUpDown = True
        Me.dtpBirthdate.Size = New System.Drawing.Size(158, 20)
        Me.dtpBirthdate.TabIndex = 25
        Me.dtpBirthdate.Value = New Date(1978, 12, 21, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(47, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Gimimo data: "
        '
        'txtNickName
        '
        Me.txtNickName.Location = New System.Drawing.Point(124, 15)
        Me.txtNickName.Name = "txtNickName"
        Me.txtNickName.Size = New System.Drawing.Size(158, 20)
        Me.txtNickName.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(51, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Slapyvardis: "
        '
        'tcSettings
        '
        Me.tcSettings.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.tcSettings.Controls.Add(Me.tpPlayer)
        Me.tcSettings.Controls.Add(Me.tpNodes)
        Me.tcSettings.Controls.Add(Me.tpOther)
        Me.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcSettings.HotTrack = True
        Me.tcSettings.Location = New System.Drawing.Point(0, 0)
        Me.tcSettings.Name = "tcSettings"
        Me.tcSettings.Padding = New System.Drawing.Point(0, 0)
        Me.tcSettings.SelectedIndex = 0
        Me.tcSettings.Size = New System.Drawing.Size(535, 598)
        Me.tcSettings.TabIndex = 4
        '
        'tpOther
        '
        Me.tpOther.AutoScroll = True
        Me.tpOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tpOther.Controls.Add(Me.gpOthersOptions)
        Me.tpOther.Controls.Add(Me.Label9)
        Me.tpOther.Location = New System.Drawing.Point(4, 25)
        Me.tpOther.Name = "tpOther"
        Me.tpOther.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOther.Size = New System.Drawing.Size(527, 569)
        Me.tpOther.TabIndex = 2
        Me.tpOther.Text = "Kita"
        Me.tpOther.UseVisualStyleBackColor = True
        '
        'gpOthersOptions
        '
        Me.gpOthersOptions.Controls.Add(Me.chkDebugMode)
        Me.gpOthersOptions.Location = New System.Drawing.Point(129, 0)
        Me.gpOthersOptions.Name = "gpOthersOptions"
        Me.gpOthersOptions.Size = New System.Drawing.Size(338, 33)
        Me.gpOthersOptions.TabIndex = 18
        Me.gpOthersOptions.TabStop = False
        '
        'chkDebugMode
        '
        Me.chkDebugMode.AutoSize = True
        Me.chkDebugMode.Location = New System.Drawing.Point(6, 10)
        Me.chkDebugMode.Name = "chkDebugMode"
        Me.chkDebugMode.Size = New System.Drawing.Size(132, 17)
        Me.chkDebugMode.TabIndex = 16
        Me.chkDebugMode.Text = "Programuotojo režimas"
        Me.chkDebugMode.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(58, 10)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Pasirinkimai:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(55, 291)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 13)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "Žėmėlapis:"
        '
        'pbMap
        '
        Me.pbMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pbMap.Location = New System.Drawing.Point(125, 291)
        Me.pbMap.Name = "pbMap"
        Me.pbMap.Size = New System.Drawing.Size(157, 157)
        Me.pbMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbMap.TabIndex = 33
        Me.pbMap.TabStop = False
        '
        'btnLoadMap
        '
        Me.btnLoadMap.Location = New System.Drawing.Point(206, 454)
        Me.btnLoadMap.Name = "btnLoadMap"
        Me.btnLoadMap.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadMap.TabIndex = 34
        Me.btnLoadMap.Text = "Įkelti"
        Me.btnLoadMap.UseVisualStyleBackColor = True
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(535, 598)
        Me.Controls.Add(Me.tcSettings)
        Me.DoubleBuffered = True
        Me.Name = "frmOptions"
        Me.Text = "Options"
        Me.tpNodes.ResumeLayout(False)
        Me.tpNodes.PerformLayout()
        Me.grpNodesOptions.ResumeLayout(False)
        Me.grpNodesOptions.PerformLayout()
        Me.tpPlayer.ResumeLayout(False)
        Me.tpPlayer.PerformLayout()
        CType(Me.pbUserPhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tcSettings.ResumeLayout(False)
        Me.tpOther.ResumeLayout(False)
        Me.tpOther.PerformLayout()
        Me.gpOthersOptions.ResumeLayout(False)
        Me.gpOthersOptions.PerformLayout()
        CType(Me.pbMap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tpNodes As System.Windows.Forms.TabPage
    Friend WithEvents mtbDefaultPort As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tpPlayer As System.Windows.Forms.TabPage
    Friend WithEvents tcSettings As System.Windows.Forms.TabControl
    Friend WithEvents lvItems As System.Windows.Forms.ListView
    Friend WithEvents chIP As System.Windows.Forms.ColumnHeader
    Friend WithEvents chPort As System.Windows.Forms.ColumnHeader
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents mtbPort As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnAddNode As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents grpNodesOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoAddToNodesList As System.Windows.Forms.CheckBox
    Friend WithEvents chkRemoveBadNodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkConnectOnStartup As System.Windows.Forms.CheckBox
    Friend WithEvents tpOther As System.Windows.Forms.TabPage
    Friend WithEvents gpOthersOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkDebugMode As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnClearAvatar As System.Windows.Forms.Button
    Friend WithEvents btnLoadNewPhoto As System.Windows.Forms.Button
    Friend WithEvents pbUserPhoto As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpBirthdate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNickName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnLoadMap As System.Windows.Forms.Button
    Public WithEvents pbMap As System.Windows.Forms.PictureBox
End Class
