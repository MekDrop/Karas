Imports System.Drawing.Drawing2D

Public Class frmEdit

    Public Map As KarasMap
    'Public Items As New Collections.Generic.List(Of MapItem.iItem)

    Private DrawMode As MapItem.PossibleWorkModes = MapItem.PossibleWorkModes.None
    Private currentItem As MapItem.iItem
    Private tmpImage As Image
    'Private _SelectionEditMode As Boolean = False
    Private _NeedSave As Boolean = False

    Private WithEvents tmrToolsUpdate As New Timers.Timer(500)

    Public FileName As String = ""

    Public Graphics As Graphics

    Private Sub frmEdit_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        'Me.PressKey(e)
    End Sub

    Public ReadOnly Property NeedSave() As Boolean
        Get
            Return Me._NeedSave
        End Get
    End Property

    'Public Structure MapItemWall

    'End Structure

    Private Sub frmEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.pbDraw.BackgroundImage = New Bitmap(400, 600)
        Me.Map.DrawGrid()

        Me.pbDraw.Image = New Bitmap(400, 600)

        Me.Graphics = Graphics.FromImage(Me.pbDraw.Image)

        With tscZoom
            Dim items() = {"25%", "50%", "75%", "100%", "125%", "150%", "200%", "300%", "400%", "500%", "(width)", "(height)", "(auto)"}
            .Items.AddRange(items)
            .SelectedIndex = 3
        End With

        '        Me.SelectionEditMode = False
        Me.Map.WorkMode = KarasMap.Mode.Drawing
        Me.Map.Items.Add(New MapItem.KingBoy(Me.pbDraw.Image, 10, 10))
        Me.UpdateItemsCount()

        Me.tsbCursor_Click(sender, e)        
        'Me.SelectionEditMode = True
        Me.Map.WorkMode = KarasMap.Mode.Selection
        Me._NeedSave = False
        'Me.Zoom(100)
        'Me.pbDraw.BackgroundImage = New Bitmap(File.GetTruePath("Textures\langeliai.jpg"))
    End Sub

    Public Sub Zoom(ByVal Factor As Integer)
        Me.pbDraw.Width = Me.pbDraw.Image.Width * Math.Round(Factor / 100, 5)
        Me.pbDraw.Height = Me.pbDraw.Image.Height * Math.Round(Factor / 100, 5)
    End Sub

    Public Sub UpdateItemsCount()
        Me.tslItemsCount.Text = Me.Map.ItemsCount.ToString
    End Sub

    'Public Property SelectionEditMode() As Boolean
    '   Get
    '      Return Me._SelectionEditMode
    ' End Get
    '        Set(ByVal value As Boolean)
    '           If Me._SelectionEditMode = value Then Exit Property
    '          If Me._SelectionEditMode Then
    ' 'Me.pbDraw.Image = New Bitmap(Me.pbDraw.Width, Me.pbDraw.Height)
    '    'Me.Map.Clear()
    '   'For I As Integer = Me.pbDraw.Controls.Count - 1 To 0 Step -1
    '  'If Me.pbDraw.Controls(I).Visible Then
    ''                Me.AddNotLifeObject(Me.pbDraw.Controls(I), True)
    ''           End If
    ''              Next
    ''             'For Each item As frmEditShape In Me.pbDraw.Controls
    ''            'If item.Visible Then
    ''           'Me.AddNotLifeObject(item, True)
    ''          'End If
    ''         'Next
    ''        Me.pbDraw.Controls.Clear()
    'Me.Map.WorkMode = KarasMap.Mode.Drawing
    ''Me.Map.SetMode(Me.pbDraw, )
    'Else
    ''Me.pbDraw.Image = Nothing
    ' ''For Each item As MapItem.iItem In Me.Items
    ' ''Me.AddLifeObject(item)
    '' 'Next
    ''For I As Integer = Me.Map.ItemsCount - 1 To 0 Step -1
    ' ''If Me.pbDraw.Controls(I).Visible Then
    ''Me.AddLifeObject(Me.Map.Items(I))
    ' ''Me.AddNotLifeObject(Me.pbDraw.Controls(I), True)
    ' ''End If
    ''Next
    'Me.Map.WorkMode = KarasMap.Mode.Selection
    ''Me.Map.SetMode(Me.pbDraw, KarasMap.Mode.Selection)
    ''End If
    ''Me._SelectionEditMode = value
    ''End Set
    'End Property

    'Private Sub AddNotLifeObject(ByRef frm As frmEditShape, Optional ByVal move As Boolean = False)
    'Dim I As Integer = Me.Map.ItemsCount
    '   If move Then
    '      frm.Data.Move(frm.Left, frm.Top)
    ' End If
    '        Me.Map.Items.Add(MapItem.Tools.CloneItem(frm.Data))
    '       Me.Map.Items(I).DrawContent(Me.pbDraw.Image)
    '  End Sub

    ' Private Sub AddLifeObject(ByRef item As MapItem.iItem, Optional ByVal X As Single = 0, Optional ByVal Y As Single = 0, Optional ByVal SelectIt As Boolean = False)
    '    If item Is Nothing Then Exit Sub
    'Dim frm As New frmEditShape(item)
    'Dim i As Integer = Me.pbDraw.Controls.Count
    '   With frm
    '      .Left = X
    '     .Top = Y
    '    .TopLevel = False
    '   .FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '  .BackgroundImageLayout = ImageLayout.None
    '.TransparencyKey = Color.Black
    '            .DropDownMenu = Me.cmsForm
    '           .Width = item.Width
    '          .Height = item.Height
    '         item.DrawContent(frm)
    '        .Update()
    ''.KeyPreview = True
    ''.BackColor = Color.Transparent            
    '       .Region = item.Region
    '      If SelectIt Then .Selected = True
    ''.BackColor = Color.White                        
    '       Me.pbDraw.Controls.Add(frm)
    '      Me.pbDraw.Controls.Item(i).Show()
    'End With
    ''Me.KeyPreview = True
    'End Sub

    Private Sub tsbCursor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbCursor.Click
        Me.SelectItem(Me.tsbCursor)
    End Sub

    Private Sub SelectItem(ByRef Item As ToolStripButton)
        Me.tsbCroper.Checked = Item Is Me.tsbCroper
        Me.tsbCursor.Checked = Item Is Me.tsbCursor
        Me.tsbWall.Checked = Item Is Me.tsbWall
        Me.tsbWeapon.Checked = Item Is Me.tsbWeapon
        If Me.tsbCursor.Checked Then
            Me.Map.WorkMode = KarasMap.Mode.Selection
        Else
            Me.Map.WorkMode = KarasMap.Mode.Drawing
        End If
        'Me.SelectionEditMode = Me.tsbCursor.Checked
        If Me.tsbWeapon.Selected Then
            Me.pbDraw.Cursor = Cursors.Cross
        Else
            Me.pbDraw.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub tsbCroper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tsbCroper.Click
        Me.SelectItem(Me.tsbCroper)
    End Sub

    Private Sub tsbWeapon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbWeapon.Click
        Me.SelectItem(Me.tsbWeapon)
    End Sub

    Private Sub tsbWall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbWall.Click
        Me.SelectItem(Me.tsbWall)
    End Sub

    Public LastMoney As Integer

    Private Sub _UpdateView()
        If Me.pbDraw.InvokeRequired Then
            Me.pbDraw.Invoke(New MapItem.RunFunc.dlgRedrawFunc(AddressOf _UpdateView))
        Else
            Me.pbDraw.Refresh()
            'Me.pbDraw.Update()
            'MsgBox("A")
        End If
    End Sub

    Private Sub pbDraw_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbDraw.MouseDown
        If Me.tsbWall.Checked Then
            Me.currentItem = New MapItem.Wall(1, e.X, e.Y)
        ElseIf Me.tsbWeapon.Checked Then
            If Me.currentItem Is Nothing Then
                Me.currentItem = New MapItem.StandartWeapon()
                MapItem.RunFunc.Redraw = AddressOf Me._UpdateView
            ElseIf Not Me.currentItem.GetType Is GetType(MapItem.StandartWeapon) Then
                Me.currentItem = New MapItem.StandartWeapon()
                MapItem.RunFunc.Redraw = AddressOf Me._UpdateView
            End If
        End If
        If Not Me.currentItem Is Nothing Then
            Me.DrawMode = Me.currentItem.WorkMode
            Me.tmpImage = New Bitmap(Me.pbDraw.Image)
            Me.LastMoney = Val(Me.tslMoneyCount.Text)
            Me.Graphics = Graphics.FromImage(Me.pbDraw.Image)
        End If
    End Sub

    Private Sub UpdateMoney()
        If Me.currentItem Is Nothing Then Exit Sub
        Dim ct As Integer = Me.LastMoney - Me.currentItem.Price
        Me.tslMoneyCount.Text = (ct).ToString
    End Sub

    Private Sub pbDraw_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbDraw.MouseMove
        If Me.DrawMode = MapItem.PossibleWorkModes.OnMouseMove Then
            Me.currentItem.DrawAndAdd(Me.Graphics, e.X, e.Y)
            Me.pbDraw.Refresh()
            Me.UpdateMoney()
        End If
    End Sub

    Private Sub pbDraw_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbDraw.MouseUp
        If Not Me.currentItem Is Nothing Then
            Select Case Me.DrawMode
                Case MapItem.PossibleWorkModes.OnMouseMove
                    Me.DrawMode = MapItem.PossibleWorkModes.None
                    Me.currentItem.Finish(e.X, e.Y)
                    Me.Map.Items.Add(Me.currentItem)
                    Me.pbDraw.Image = Me.tmpImage
                    Me.Graphics = Graphics.FromImage(Me.pbDraw.Image)
                    Me.currentItem.DrawContent(Me.Graphics)
                    Me.tmpImage = Nothing
                    Me.UpdateMoney()
                    Me.currentItem = Nothing
                    Me.UpdateItemsCount()
                    Me._NeedSave = True
                Case MapItem.PossibleWorkModes.OnClick
                    Me.currentItem.DrawAndAdd(Me.Graphics, e.X, e.Y)
                    Me.pbDraw.Refresh()
                    Me.UpdateMoney()
                    If Me.currentItem.Finished Then
                        Me.Map.Items.Add(Me.currentItem)
                        'Me.pbDraw.Image = Me.tmpImage
                        'Me.currentItem.DrawContent(Me.pbDraw.Image)
                        'Me.tmpImage = Nothing
                        Me.currentItem = Nothing
                        Me.UpdateItemsCount()
                        Me._NeedSave = True
                    End If
            End Select
        Else
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Me.pbDraw.Tag = "pbDrawClick"
                Me.cmsForm.Show(Me.pbDraw, e.X, e.Y)
            End If
        End If
    End Sub

    Public Enum SizeMode As SByte
        Normal = 0
        Width = 1
        Height = 2
        Auto = 3
    End Enum

    Public Shadows AutoSizeMode As SizeMode = SizeMode.Normal

    Private Sub pnlContent_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pnlContent.Resize
        Dim T As Single, L As Single
        Select Case Me.AutoSizeMode
            Case SizeMode.Width
                Me.Zoom(100 / Me.pbDraw.Image.Width * (Me.pnlContent.Width - 25))
            Case SizeMode.Height
                Me.Zoom(100 / Me.pbDraw.Image.Height * (Me.pnlContent.Height - 25))
            Case SizeMode.Auto
                If (Me.pnlContent.Height - Me.pbDraw.Image.Height) < (Me.pnlContent.Width - Me.pbDraw.Image.Width) Then
                    Me.Zoom(100 / Me.pbDraw.Image.Height * (Me.pnlContent.Height - 25))
                Else
                    Me.Zoom(100 / Me.pbDraw.Image.Width * (Me.pnlContent.Width - 25))
                End If
        End Select
        T = (Me.pnlContent.Height - Me.pbDraw.Height) / 2 - 25
        L = (Me.pnlContent.Width - Me.pbDraw.Width) / 2 - 25
        If T > 0 Then
            Me.pbDraw.Top = T
        Else
            Me.pbDraw.Top = 0
        End If
        If L > 0 Then
            Me.pbDraw.Left = L
        Else
            Me.pbDraw.Left = 0
        End If
    End Sub

    Private Sub tscZoom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tscZoom.TextChanged
        If Me.tscZoom.Text = "(width)" Then
            Me.AutoSizeMode = SizeMode.Width
        ElseIf Me.tscZoom.Text = "(height)" Then
            Me.AutoSizeMode = SizeMode.Height
        ElseIf Me.tscZoom.Text = "(auto)" Then
            Me.AutoSizeMode = SizeMode.Auto
        Else
            Me.AutoSizeMode = SizeMode.Normal
            Me.Zoom(Val(tscZoom.Text))
        End If
        Me.pnlContent_Resize(sender, e)
    End Sub

    'Public Property SelectedItem() As frmEditShape
    '   Get
    '      Return Me._SelectedItem
    '  End Get
    '  Set(ByVal value As frmEditShape)
    '      If Me._SelectedItem Is value Then Exit Property
    '      If value Is Nothing Then Exit Property
    '     If Not Me._SelectedItem Is Nothing Then
    '        Me._SelectedItem.Selected = False
    '   End If
    '   Me._SelectedItem = value
    'End Set
    'End Property

    'Private _SelectedItem As frmEditShape = Nothing

    Private Sub cmsForm_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosingEventArgs) Handles cmsForm.Closing
        Me.pbDraw.Tag = Nothing
    End Sub

    Private Sub cmsForm_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmsForm.KeyUp
        '    Me.PressKey(e)
    End Sub

    Private Sub cmsForm_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cmsForm.Opening
        If Me.Map.SelectedItem Is Nothing Then
            Me.CutToolStripMenuItem.Enabled = False
            Me.CopyToolStripMenuItem.Enabled = False
            Me.DeleteToolStripMenuItem.Enabled = False
        Else
            Me.CutToolStripMenuItem.Enabled = Me.Map.SelectedItem.Data.EditCommands And Not Me.pbDraw.Tag = "pbDrawClick"
            Me.CopyToolStripMenuItem.Enabled = Me.Map.SelectedItem.Data.EditCommands And Not Me.pbDraw.Tag = "pbDrawClick"
            Me.DeleteToolStripMenuItem.Enabled = Me.Map.SelectedItem.Data.EditCommands And Not Me.pbDraw.Tag = "pbDrawClick"
        End If
        Me.MoveToolStripMenuItem.Enabled = Not Me.Map.SelectedItem Is Nothing And Not Me.pbDraw.Tag = "pbDrawClick"
        Me.PasteToolStripMenuItem.Enabled = Clipboard.ContainsData(MapItem.Tools.ClipboardFormat)
    End Sub

    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        Me.CopyToolStripMenuItem_Click(sender, e)
        Me.DeleteToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        If Me.Map.SelectedItem Is Nothing Or Me.pbDraw.Tag = "pbDrawClick" Then Exit Sub
        If Me.Map.SelectedItem.Data.EditCommands = False Then Exit Sub
        Me.Map.ExecLifeEditCommand(KarasMap.LifeEditOperations.Copy)
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        If Not Clipboard.ContainsData(MapItem.Tools.ClipboardFormat) Then Exit Sub
        Me.tslMoneyCount.Text = Val(Me.tslMoneyCount.Text) - Me.Map.SelectedItem.Data.Price
        Me.tslItemsCount.Text = Me.pbDraw.Controls.Count.ToString
        Me.Map.ExecLifeEditCommand(KarasMap.LifeEditOperations.Paste)
        Me._NeedSave = True
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If Me.Map.SelectedItem Is Nothing Or Me.pbDraw.Tag = "pbDrawClick" Then Exit Sub
        If Me.Map.SelectedItem.Data.EditCommands = False Then Exit Sub
        Me.tslMoneyCount.Text = Val(Me.tslMoneyCount.Text) + Me.Map.SelectedItem.Data.Price
        ''Me.pbDraw.Controls.Remove(Me._SelectedItem)        
        'Me._SelectedItem.Close()
        'Me._SelectedItem = Nothing
        Me._NeedSave = True
        Me.Map.ExecLifeEditCommand(KarasMap.LifeEditOperations.Delete)
        Me.tslItemsCount.Text = Me.pbDraw.Controls.Count.ToString
        'If Me.pbDraw.Controls.Count = 0 Then Exit Sub
        'Dim frx As frmEditShape = Me.pbDraw.Controls.Item(0)
        'frx.Selected = True
    End Sub

    Private Sub MoveShape(ByVal X As Integer, ByVal Y As Integer)
        Me.Map.ExecLifeEditCommand(KarasMap.LifeEditOperations.Move)
        Me._NeedSave = True
    End Sub

    Private Sub MoveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MoveToolStripMenuItem.Click
        If Me.Map.SelectedItem Is Nothing Or Me.pbDraw.Tag = "pbDrawClick" Then Exit Sub
        Dim frx As New frmEditShapeMove(Me.pbDraw, AddressOf MoveShape)
    End Sub

    Public Sub PressKey(ByVal Key As KeyEventArgs)
        Dim Code As System.Windows.Forms.Keys = Key.KeyCode
        If Key.Control Then Code = Code Or Keys.Control
        If Key.Shift Then Code = Code Or Keys.Shift
        If Key.Alt Then Code = Code Or Keys.Alt
        Select Case Code
            Case Me.MoveToolStripMenuItem.ShortcutKeys
                Me.MoveToolStripMenuItem_Click(Me, New System.EventArgs())
            Case Me.PasteToolStripMenuItem.ShortcutKeys
                Me.PasteToolStripMenuItem_Click(Me, New System.EventArgs())
            Case Me.CopyToolStripMenuItem.ShortcutKeys
                Me.CopyToolStripMenuItem_Click(Me, New System.EventArgs())
            Case Me.CutToolStripMenuItem.ShortcutKeys
                Me.CutToolStripMenuItem_Click(Me, New System.EventArgs())
            Case Me.DeleteToolStripMenuItem.ShortcutKeys
                Me.DeleteToolStripMenuItem_Click(Me, New System.EventArgs())
        End Select
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripButton.Click
        If Me._NeedSave Then
            Dim dv As DialogResult = System.Windows.Forms.MessageBox.Show("Šis žemėlapis nebuvo išsaugotas. Ar norite dabar išsaugoti?", "Dėmesio", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If dv = DialogResult.Yes Then Me.SaveToolStripButton_Click(sender, e)
        End If
        Me._NeedSave = False
        Me.tslSaved.Text = "#no#"
        Me.Map.Clear()
        Me.pbDraw.Controls.Clear()
        Me.OnLoad(e)
    End Sub

    Private Sub SaveToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveToolStripButton.Click
        'If Me.FileName = "" Then
        Using dialog As New System.Windows.Forms.SaveFileDialog()
            dialog.Filter = "Karas! Žemėlapiai (*.xml)|*.xml|Visi failai (*.*)|*.*"
            If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.FileName = dialog.FileName
            Else
                Exit Sub
            End If
        End Using
        'End If
        'Dim fm As Boolean = Me.SelectionEditMode
        'Me.SelectionEditMode = False
        'Using FS2 As New IO.FileStream(Me.FileName, IO.FileMode.Create)            
        'Using FS As New IO.StreamWriter(FS2)
        '     FS.Write(MapItem.Tools.XMLSerialize(Me.Items))
        '      FS.Close()            
        '   End Using
        'End Using
        Me.Map.Save(Me.FileName)
        'Me.SelectionEditMode = fm
        Me._NeedSave = False
        Me.tslSaved.Text = Date.Now.ToString
    End Sub

    Private Sub OpenToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenToolStripButton.Click
        If Me._NeedSave Then
            Dim dv As DialogResult = System.Windows.Forms.MessageBox.Show("Šis žemėlapis nebuvo išsaugotas. Ar norite dabar išsaugoti?", "Dėmesio", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If dv = DialogResult.Yes Then Me.SaveToolStripButton_Click(sender, e)
        End If
        Using dialog As New System.Windows.Forms.OpenFileDialog()
            dialog.Filter = "Karas! Žemėlapiai (*.xml)|*.xml|Visi failai (*.*)|*.*"
            If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.FileName = dialog.FileName
            Else
                Exit Sub
            End If
        End Using
        'Dim fm As Boolean = Me.SelectionEditMode
        'Me.SelectionEditMode = False
        'Using FS2 As New IO.FileStream(Me.FileName, IO.FileMode.Create)            
        'Using FS As New IO.StreamWriter(FS2)
        '     FS.Write(MapItem.Tools.XMLSerialize(Me.Items))
        '      FS.Close()            
        '   End Using
        'End Using        
        Me.Map.Load(Me.FileName)
        Me.tslMoneyCount.Text = (10000 - Me.Map.Price).ToString
        Me.tslItemsCount.Text = Me.Map.ItemsCount
        Me.tslSaved.Text = Me.Map.LastSavedDateAndTime.ToString
        'Me.SelectionEditMode = fm

    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Dim sel As KarasMap.Mode = Me.Map.WorkMode
        Me.Map.WorkMode = KarasMap.Mode.Drawing
        'Dim sel As Boolean = Me.SelectionEditMode
        'Me.SelectionEditMode = False
        Dim dlg As New frmPrintDialog(Me.pbDraw)
        'Me.SelectionEditMode = sel
        Me.Map.WorkMode = sel
    End Sub

    Private Sub tmrToolsUpdate_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrToolsUpdate.Elapsed
        If Me.Map.SelectedItem Is Nothing Then
            Me.CutToolStripButton.Enabled = False
            Me.CopyToolStripButton.Enabled = False
        Else
            Me.CutToolStripButton.Enabled = Me.Map.SelectedItem.Data.EditCommands And Not Me.pbDraw.Tag = "pbDrawClick" And Me.tsbCursor.Checked
            Me.CopyToolStripButton.Enabled = Me.Map.SelectedItem.Data.EditCommands And Not Me.pbDraw.Tag = "pbDrawClick" And Me.tsbCursor.Checked
        End If
        Me.PasteToolStripButton.Enabled = Clipboard.ContainsData(MapItem.Tools.ClipboardFormat) And Me.tsbCursor.Checked
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        tmrToolsUpdate.SynchronizingObject = Me.tsToolbar
        tmrToolsUpdate.Start()

        Me.Map = New KarasMap(Me.pbDraw)
    End Sub

    Private Sub CutToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripButton.Click
        Me.CutToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CopyToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripButton.Click
        Me.CopyToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PasteToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripButton.Click
        Me.PasteToolStripMenuItem_Click(sender, e)
    End Sub

End Class