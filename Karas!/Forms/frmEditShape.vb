Public Class frmEditShape

    Public Delegate Sub dlgSelected(ByRef Owner As frmEditShape)
    Public Delegate Sub dlgShoot(ByRef Owner As frmEditShape)

    Private X As Single, Y As Single, Drag As Boolean = False

    Private _Selected As Boolean
    Private _Hovered As Boolean
    Private tmpColor As Color

    Public SelectedEvent As dlgSelected = Nothing
    Public ShootEvent As dlgShoot = Nothing

    Public PlayMode As Boolean = False

    Public Property Selected() As Boolean
        Get
            Return Me._Selected
        End Get
        Set(ByVal value As Boolean)
            If value And Not Me.SelectedEvent Is Nothing Then
                Me.SelectedEvent.Invoke(Me)
                'If Me._Hovered Then
                'Me.BackColor = Color.DarkBlue
                'Else
                '   Me.BackColor = Color.Black
                'End If
                '    frmEdit.SelectedItem = Me
                'Else
                'If Me._Hovered Then
                'Me.BackColor = Color.Blue
                'Else
                'Me.BackColor = Color.Transparent
                'End If
                '   frmEdit.SelectedItem = Nothing
            End If
            Me._Selected = value
        End Set
    End Property

    Public WithEvents DropDownMenu As ContextMenuStrip = Nothing
    Public Data As MapItem.iItem

    Public Sub New(ByVal Data As MapItem.iItem)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        'Me.BackColor = Color.Transparent
        Me.Data = Data
        Me.mett.ToolTipTitle = "Info"
    End Sub

    Private Sub frmEditShape_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        If Not Me.PlayMode Then Exit Sub
        If Me.Data.CanShoot And Not Me.ShootEvent Is Nothing Then Me.ShootEvent.Invoke(Me)        
    End Sub

    Private Sub frmEditShape_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If Me.PlayMode Then Exit Sub
        frmEdit.PressKey(e)
    End Sub

    Private Sub frmEditShape_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        If Me.PlayMode Then Exit Sub
        Me.Selected = True
    End Sub

    Private Sub frmEditShape_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        If Me.PlayMode Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Left Then
            X = e.X
            Y = e.Y
            Me.Drag = True
        End If
    End Sub

    Private Sub frmEditShape_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
        If Me.PlayMode Then Exit Sub
        Me._Hovered = True
        Me.Selected = Me._Selected
        Me.mett.Show(vbCrLf & "Kaina: " + Me.Data.Price.ToString + " Lt" & vbCrLf & "Tipas: " & Me.Data.Type, Me)
    End Sub

    Private Sub frmEditShape_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        If Me.PlayMode Then Exit Sub
        Me._Hovered = False
        Me.Selected = Me._Selected
        Me.mett.Hide(Me)
    End Sub

    Private Sub frmEditShape_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If Me.PlayMode Then Exit Sub
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Me.Left += (e.X - X)
            Me.Top += (e.Y - Y)
        End If
    End Sub

    Private dpVisible As Boolean = False

    Private Sub frmEditShape_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        If Me.PlayMode Then Exit Sub
        If (e.Button = Windows.Forms.MouseButtons.Right) And (Not Me.DropDownMenu Is Nothing) Then
            Me.dpVisible = True
            DropDownMenu.Show(Me, e.X, e.Y)
        End If
    End Sub

    
End Class