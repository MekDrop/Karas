Public Class ucTabForms

    Private LastSelected As Integer = -1

    Public Enum TabWorkMode As SByte
        Normal = 0
        OnDeactivateClose = 1        
    End Enum

    Private Structure TabInfo
        Public TabWorkMode As TabWorkMode
        Sub New(ByVal TabWorkMode As TabWorkMode)
            Me.TabWorkMode = TabWorkMode
        End Sub
    End Structure

    Public Property Appearance() As TabAppearance
        Get
            Return Me.tcTab.Appearance
        End Get
        Set(ByVal value As TabAppearance)
            Me.tcTab.Appearance = value
        End Set
    End Property

    Public Property Alignment() As TabAlignment
        Get
            Return Me.tcTab.Alignment
        End Get
        Set(ByVal value As TabAlignment)
            Me.tcTab.Alignment = value
        End Set
    End Property

    Private Delegate Sub dlgAdd(ByRef Form As System.Windows.Forms.Form, ByVal TabWorkMode As TabWorkMode, ByVal BorderStyle As System.Windows.Forms.BorderStyle, ByVal Activate As Boolean)

    Public Sub Add(ByRef Form As System.Windows.Forms.Form, Optional ByVal TabWorkMode As TabWorkMode = ucTabForms.TabWorkMode.Normal, Optional ByVal BorderStyle As System.Windows.Forms.BorderStyle = Windows.Forms.BorderStyle.None, Optional ByVal Activate As Boolean = True)
        If Form.InvokeRequired Then
            Form.Invoke(New dlgAdd(AddressOf Add), Form, TabWorkMode, BorderStyle, Activate)
            Exit Sub
        End If
        If Me.tcTab.TabPages.ContainsKey(Form.Handle.ToString) Then
            If Activate Then Me.tcTab.SelectTab(Me.tcTab.TabPages(Form.Handle.ToString))
            Exit Sub
        End If
        'Me.tcTab.ImageList = Nothing        
        Me.imlList.Images.Add(Form.Handle.ToString, Form.Icon)
        'Me.imlList.Images.Add(Form.Handle.ToString, .ToBitmap)
        'Me.tcTab.ImageList = Me.imlList
        Me.tcTab.TabPages.Add(Form.Handle.ToString, Form.Text)
        With Me.tcTab.TabPages(Form.Handle.ToString)
            Form.TopLevel = False
            .Controls.Add(Form)
            .Padding = System.Windows.Forms.Padding.Empty
            .BackColor = Color.Aqua
            .Tag = New TabInfo(TabWorkMode)
            .ImageKey = Form.Handle.ToString
            .BorderStyle = BorderStyle
        End With
        With Form
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .Show()
            .Dock = DockStyle.Fill
        End With
        If Activate Then Me.tcTab.SelectTab(Me.tcTab.TabPages(Form.Handle.ToString))
        AddHandler Form.TextChanged, AddressOf _HeaderUpdate
        AddHandler Form.FormClosed, AddressOf _FormClose
    End Sub

    Private Sub _HeaderUpdate(ByVal Form As System.Windows.Forms.Form, ByVal e As System.EventArgs)
        If Not Me.tcTab.TabPages.ContainsKey(Form.Handle.ToString) Then Exit Sub
        Me.tcTab.TabPages(Form.Handle.ToString).Text = Form.Text
    End Sub

    Private Sub _FormClose(ByVal Form As System.Windows.Forms.Form, ByVal e As System.EventArgs)
        Me.Remove(Form)
    End Sub

    Private Function GetTabForm(ByVal Index As Integer) As System.Windows.Forms.Form
        Return CType(Me.tcTab.TabPages(Index).Controls(0), System.Windows.Forms.Form)
    End Function

    Public Sub Remove(ByRef Form As System.Windows.Forms.Form)
        If Not Me.tcTab.TabPages.ContainsKey(Form.Handle.ToString) Then Exit Sub
        Dim key As String = Form.Handle.ToString
        Me.imlList.Images.RemoveByKey(key)
        'Form.Close()
        Me.tcTab.TabPages.RemoveByKey(key)
    End Sub

    Private Sub tcTab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tcTab.SelectedIndexChanged
        On Error Resume Next
        If Me.LastSelected > -1 Then
            With CType(Me.tcTab.TabPages(Me.LastSelected).Tag, TabInfo)
                Select Case .TabWorkMode
                    Case TabWorkMode.OnDeactivateClose
                        'Me.tcTab.ImageList = Nothing
                        'Dim frm As System.Windows.Forms.Form = GetTabForm(Me.LastSelected)
                        'Me.imlList.Images.RemoveByKey(frm.Handle.ToString)
                        'frm.Close()
                        'Me.tcTab.TabPages.RemoveAt(Me.LastSelected)
                        Me.Remove(GetTabForm(Me.LastSelected))
                        'Me.tcTab.ImageList = Me.imlList
                End Select
            End With
        End If
        Me.LastSelected = Me.tcTab.SelectedIndex
    End Sub

    Public ReadOnly Property SelectedItem() As Form
        Get
            Return GetTabForm(Me.tcTab.SelectedIndex)
        End Get
    End Property

End Class
