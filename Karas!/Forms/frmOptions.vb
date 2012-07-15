Public Class frmOptions

    Public Enum Page As SByte
        None = 2
        Player = 0
        Connection = 1
        Other = 3
    End Enum

    Public Sub ShowMeWithSelectedPage(ByVal Page As Page)        
        Me.tcSettings.SelectedIndex = Page
        frmMain.ShowFormAsMDIChild(Me)
        frmMain.Show()
    End Sub

    Private Sub tcSettings_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcSettings.SelectedIndexChanged
        Me.SaveSettingsView()
        'Me.UpdateSettingsView()
    End Sub

    Public Sub SaveSettingsView()
        'On Error Resume Next

        My.Settings.UserPlayerNickName = Me.txtNickName.Text.Replace(" ", "_")        
        My.Settings.UserEmail = Me.txtEmail.Text

        If Not Me.mtbDefaultPort.Text = "" Then
            My.Settings.DefaultPort = Convert.ToInt16(Me.mtbDefaultPort.Text)
        End If        

        My.Settings.Nodes.Clear()
        For Each node As System.Windows.Forms.ListViewItem In Me.lvItems.Items
            If node.Checked Then My.Settings.Nodes.Add(node.Name)
        Next

        My.Settings.Save()

        'Me.tpPlayer.BackgroundImage = Me.pbUserPhoto.Image
        'My.Settings.SetAvatar(Me.pbUserPhoto.Image)

        frmChat.UpdateMySelfNick()
        frmChat.UpdateSettings()

        Me.btnClearAvatar.Enabled = Not Me.pbUserPhoto.Image Is Nothing

    End Sub

    Public Map As KarasMap

    Public Sub UpdateSettingsView()
        Dim tab As Page = Me.tcSettings.SelectedIndex

        Me.txtNickName.Text = My.Settings.UserPlayerNickName
        Me.dtpBirthdate.Value = My.Settings.UserBirthdayDate
        Me.txtEmail.Text = My.Settings.UserEmail
        Me.pbUserPhoto.Image = My.Settings.UserAvatar()

        Me.btnClearAvatar.Enabled = Not Me.pbUserPhoto.Image Is Nothing

        Dim url As System.Uri
        For Each node As String In My.Settings.Nodes
            If Me.lvItems.Items.ContainsKey(node) Then Continue For
            url = New System.Uri("kazkas://" + node)
            If url.Port < 0 Then Continue For
            With Me.lvItems.Items.Add(node, url.Host, 0)
                .SubItems.Add(url.Port)
                .Checked = True
            End With
        Next
        'Me.lvItems.SelectedItems.Clear()                
        'Me.lvItems.SelectedItems(My.Settings.SelectedNode).Selected = True
        Me.mtbDefaultPort.Text = My.Settings.DefaultPort
        Me.mtbPort.Text = Me.mtbDefaultPort.Text

        Me.chkConnectOnStartup.Checked = My.Settings.ConnectOnStartup
        Me.chkRemoveBadNodes.Checked = My.Settings.AutoRemoveBadNodes
        Me.chkAutoAddToNodesList.Checked = My.Settings.AutoAddGoodNodes

        Me.chkDebugMode.Checked = My.Settings.DebugMode

    End Sub

    Public Sub ShowControlMessage(ByRef Control As System.Windows.Forms.Control, Optional ByVal Message As String = "")
        Static Errors As New System.Collections.Generic.Dictionary(Of System.String, System.Windows.Forms.ErrorProvider)
        If Errors.ContainsKey(Control.Name) Then
            If Message = "" Then
                Errors.Item(Control.Name).Clear()
                Errors.Remove(Control.Name)
                'Errors.Item(Control.Name).Clear()
                'Errors.Item(Control.Name) = 
                'Errors.Item(Control.Name).SetError(Control, Nothing)
            Else
                If Not Errors.Item(Control.Name).GetError(Control) = Message Then
                    Errors.Item(Control.Name).SetError(Control, Message)
                End If
            End If
        Else
            Dim prov As New System.Windows.Forms.ErrorProvider()
            Errors.Add(Control.Name, prov)
            prov.SetError(Control, Message)
        End If        
    End Sub

    Private Sub txtNickName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNickName.TextChanged

        If (Me.txtNickName.Text.IndexOf(" ") > 0) Then
            Me.ShowControlMessage(Me.txtNickName, "Tarpo simbolio naudoti slapyvardyje negalima (išsaugant tarpo simbolis bus pašalintas)")
        ElseIf Me.txtNickName.Text.Trim.Length < 3 Then
            Me.ShowControlMessage(Me.txtNickName, "Slapyvardis turi būti sudarytas bent iš trijų simbolių (priešingu atveju jungiantis, bus sugeneruotas automatiškai ir išsaugotas)")
        Else
            ShowControlMessage(Me.txtNickName)
        End If


    End Sub

    Private Sub TextBox1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter Or Keys.Return
                Me.btnAddNode_Click(sender, New System.EventArgs())
            Case Keys.Tab
                Me.mtbPort.Focus()
            Case Keys.Right
                If Me.TextBox1.SelectionStart = Me.TextBox1.TextLength Then
                    Me.mtbPort.Focus()
                    Me.mtbPort.SelectionStart = 0
                End If
        End Select
    End Sub

    Private Sub mtbPort_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles mtbPort.KeyUp
        Select Case e.KeyCode
            Case Keys.Enter Or Keys.Return
                Me.btnAddNode_Click(sender, New System.EventArgs())
            Case Keys.Tab
                Me.TextBox1.Focus()
            Case Keys.Left                
                If Me.mtbPort.SelectionStart = 0 Then
                    Me.TextBox1.Focus()
                    Me.TextBox1.SelectionStart = Me.TextBox1.TextLength
                End If
        End Select
    End Sub

    Private Sub btnAddNode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNode.Click
        If Me.TextBox1.TextLength > 0 And Me.mtbPort.TextLength > 0 Then
            With Me.lvItems.Items.Add(Me.TextBox1.Text + ":" + Me.mtbPort.Text, Me.TextBox1.Text, 0)
                .SubItems.Add(Me.mtbPort.Text)
                .Checked = True
            End With
            Me.mtbPort.Text = Me.mtbDefaultPort.Text
            Me.TextBox1.Text = ""
            Me.TextBox1.Focus()
        ElseIf Me.TextBox1.TextLength > 0 Then
            Me.mtbPort.Focus()
        ElseIf Me.mtbPort.TextLength > 0 Then
            Me.TextBox1.Focus()
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.dtpBirthdate.MaxDate = DateTime.Now.Subtract(New System.TimeSpan(24 * 30 * 24, 0, 0))
        Me.dtpBirthdate.MinDate = DateTime.Today.Subtract(New System.TimeSpan(24 * 30 * 12 * 100, 0, 0))

        Me.pbMap.BackgroundImage = New Bitmap(320, 480)
        Me.pbMap.Image = New Bitmap(320, 480)
        Me.Map = New KarasMap(Me.pbMap)
        Me.Map.WorkMode = KarasMap.Mode.Drawing
        Me.pbMap.BackColor = Color.White
    End Sub

    Private Sub txtEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
        Static Dim re As New System.Text.RegularExpressions.Regex("^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + _
         "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + _
         ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")

        Dim Text As String = Me.txtEmail.Text
        If re.IsMatch(Text) Then
            Me.ShowControlMessage(Me.txtEmail)
        Else
            Me.ShowControlMessage(Me.txtEmail, "Elektroninis paštas turi būti įvestas formatu: kazkas@domenas.lt")
        End If
    End Sub

    Private Sub btnLoadNewPhoto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadNewPhoto.Click
        Dim FileOpenDialog As New System.Windows.Forms.OpenFileDialog()
        With FileOpenDialog
            .CheckFileExists = True
            .Filter = "Visi paveiksliukai (*.jpg;*.bmp;*.jpeg;*.gif;*.rle;*.dib;*.jpe;*.png;*.emf;*.wmf;*.ico)|*.jpg;*.bmp;*.jpeg;*.gif;*.rle;*.dib;*.jpe;*.png;*.emf;*.wmf;*.ico|JPEG Paveiskliukai (*.jpg;*.jpeg;*.jpe)|*.jpg;*.jpeg;*.jpe|Bitmap paveiskliukai (*.bmp;*.dib;*.rle)|*.bmp;*.dib;*.rle|Windows MetaFile tipo paveiskliukai (*.wmf;*.emf)|*.wmf;*.emf|GIF paveiskliukai (*.gif)|*.gif|PNG paveiskliukai (*.png)|*.png|Piktogramos (*.ico)|*.ico|Visi failai (*.*)|*.*"
            .Multiselect = False
            .ShowReadOnly = False
            .ShowHelp = False
            .Title = "Pasirinkite paveiksliuką"
            .ValidateNames = True
        End With

        If FileOpenDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.pbUserPhoto.Image = System.Drawing.Image.FromFile(FileOpenDialog.FileName)
            If Me.pbUserPhoto.Image.Width > Me.pbUserPhoto.Width Or Me.pbUserPhoto.Image.Height > Me.pbUserPhoto.Height Then
                Dim callbackData As New System.IntPtr
                Dim Image As System.Drawing.Image = Me.pbUserPhoto.Image.GetThumbnailImage(Me.pbUserPhoto.Width, Me.pbUserPhoto.Height, AddressOf _GetThumbnailImageAbort, callbackData)
                Me.pbUserPhoto.Image = Image.Clone
                Me.btnClearAvatar.Enabled = True
                My.Settings.UserAvatar = Me.pbUserPhoto.Image
            End If
        End If
    End Sub

    Private Function _GetThumbnailImageAbort() As Boolean
    End Function

    Private Sub frmOptions_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.SaveSettingsView()
    End Sub

    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.UpdateSettingsView()
        Try
            'MsgBox(My.Settings.UserMap)
            If (New IO.FileInfo(My.Settings.UserMap)).Exists Then
                Me.Map.Load(My.Settings.UserMap, False)
                Me.Map.DrawContents()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnClearAvatar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearAvatar.Click
        Me.pbUserPhoto.Image = Nothing
        Me.btnClearAvatar.Enabled = False
        My.Settings.UserAvatar = Me.pbUserPhoto.Image
    End Sub

    Private Sub chkDebugMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDebugMode.CheckedChanged
        My.Settings.DebugMode = Me.chkDebugMode.Checked
    End Sub

    Private Sub chkConnectOnStartup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkConnectOnStartup.CheckedChanged
        My.Settings.ConnectOnStartup = Me.chkConnectOnStartup.Checked
    End Sub

    Private Sub chkRemoveBadNodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRemoveBadNodes.CheckedChanged
        My.Settings.AutoRemoveBadNodes = Me.chkRemoveBadNodes.Checked
    End Sub

    Private Sub chkAutoAddToNodesList_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoAddToNodesList.CheckedChanged
        My.Settings.AutoAddGoodNodes = Me.chkRemoveBadNodes.Checked
    End Sub

    Private Sub dtpBirthdate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpBirthdate.ValueChanged
        My.Settings.UserBirthdayDate = Me.dtpBirthdate.Value
    End Sub

    Private Sub btnLoadMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadMap.Click
        Dim FileOpenDialog As New System.Windows.Forms.OpenFileDialog()
        With FileOpenDialog
            .CheckFileExists = True
            .Filter = "Karas! žemėlapiai (*.xml)|*.xml|Visi failai (*.*)|*.*"
            .Multiselect = False
            .ShowReadOnly = False
            .ShowHelp = False
            .Title = "Pasirinkite žemėlapį"
            .ValidateNames = True
        End With

        If FileOpenDialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.Map.Load(FileOpenDialog.FileName, True)
            'Me.Map.DrawGrid()
            Try
                Me.Map.DrawContents()
            Catch ex As Exception

            End Try            
            My.Settings.UserMap = FileOpenDialog.FileName
            My.Settings.Save()            
        End If
    End Sub

End Class
