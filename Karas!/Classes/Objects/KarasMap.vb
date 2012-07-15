Public Class KarasMap

    Public Items As New Collections.Generic.List(Of MapItem.iItem)

    Private _FileName As String = Nothing
    Private _ICount As Integer = -1
    Private _Price As Integer

    Public Owner As PictureBox

    Public ReadOnly Property FileName() As String
        Get
            Return Me._FileName
        End Get
    End Property

    Public ReadOnly Property Price() As Integer
        Get
            If Me._ICount <> Me.Items.Count Then
                Me._Price = 0
                For Each Item As MapItem.iItem In Me.Items
                    Me._Price += Item.Price
                Next
            End If
            Return Me._Price
        End Get
    End Property

    Public ReadOnly Property ItemsCount() As Integer
        Get
            Return Me.Items.Count
        End Get
    End Property

    Public Sub Load(ByVal FileName As String, Optional ByVal ShowProgress As Boolean = True)
        Dim m As Mode = Me.WorkMode
        Me.WorkMode = Mode.Drawing
        Using FS2 As New IO.FileStream(FileName, IO.FileMode.Open, IO.FileAccess.Read)
            Using zip As New IO.Compression.GZipStream(FS2, IO.Compression.CompressionMode.Decompress)
                Using FS As New IO.StreamReader(zip)
                    Me.Items = MapItem.Tools.XMLUnserialize(FS.ReadToEnd, ShowProgress)
                    'FS.Write(MapItem.Tools.XMLSerialize(Me.Items))
                    'FS.Close()
                End Using
            End Using
        End Using
        Me._FileName = FileName
        Me.WorkMode = m
    End Sub

    Sub New(ByVal FileName As String)
        Me.Load(FileName)
    End Sub

    Sub New(ByRef Owner As PictureBox)
        Me.Owner = Owner
    End Sub

    Sub New(ByVal FileName As String, ByRef Owner As PictureBox)
        Me.Load(FileName)
        Me.Owner = Owner
    End Sub

    Sub New()
    End Sub

    Public Sub LoadFromDataCache(ByRef Data As DataCacheItem, Optional ByVal ShowProgress As Boolean = False)        
        Me.WorkMode = Mode.Drawing
        Dim Bytes() As Byte = Data.Content
        Using FS2 As New IO.MemoryStream(Bytes)
            Using zip As New IO.Compression.GZipStream(FS2, IO.Compression.CompressionMode.Decompress)
                Using FS As New IO.StreamReader(zip)
                    Me.Items = MapItem.Tools.XMLUnserialize(FS.ReadToEnd, ShowProgress)
                    'FS.Write(MapItem.Tools.XMLSerialize(Me.Items))
                    'FS.Close()
                End Using
            End Using
        End Using
    End Sub

    Public Sub Save(ByVal FileName As String)
        Using FS2 As New IO.FileStream(FileName, IO.FileMode.Create)
            Using zip As New IO.Compression.GZipStream(FS2, IO.Compression.CompressionMode.Compress)
                Using FS As New IO.StreamWriter(zip)
                    FS.Write(MapItem.Tools.XMLSerialize(Me.Items))
                    FS.Close()
                End Using
            End Using
        End Using
        Me._FileName = FileName
    End Sub

    Public Sub Save()
        Me.Save(Me._FileName)
    End Sub

    Public Sub Clear()
        Me.Items.Clear()
    End Sub

    Public ReadOnly Property LastSavedDateAndTime() As Date
        Get
            Dim info As New IO.FileInfo(Me.FileName)
            If Not info.Exists Then Return Nothing
            Return info.LastWriteTime
        End Get
    End Property

    Public Enum Mode As SByte
        Drawing = 1
        Selection = 2
        Play = 3
    End Enum

    Private _Mode As Mode = Mode.Drawing

    Public Sub DrawContents()
        Me.Owner.Image = New Bitmap(Me.Owner.Image.Width, Me.Owner.Image.Height)
        Me.DrawGrid()
        'Using gr As Graphics = System.Drawing.Graphics.FromImage(Me.Owner.Image)
        For I As Integer = Me.ItemsCount - 1 To 0 Step -1
            Me.Items(I).DrawContent(Me.Owner.Image)
        Next
        'End Using
    End Sub

    Public Property WorkMode() As Mode
        Get
            Return Me._Mode
        End Get
        Set(ByVal Mode As Mode)
            If Mode = Me._Mode Then Exit Property
            Select Case Mode
                Case KarasMap.Mode.Selection
                    Me.Owner.Image = Nothing
                    'For Each item As MapItem.iItem In Me.Items
                    'Me.AddLifeObject(item)
                    'Next
                    For I As Integer = Me.ItemsCount - 1 To 0 Step -1
                        'If Me.pbDraw.Controls(I).Visible Then
                        Me.AddLifeObject(Me.Items(I))
                        'Me.AddNotLifeObject(Me.pbDraw.Controls(I), True)
                        'End If
                    Next                
                Case KarasMap.Mode.Drawing
                    Me.Owner.Image = New Bitmap(Me.Owner.Width, Me.Owner.Height)
                    Me.Clear()
                    For I As Integer = Me.Owner.Controls.Count - 1 To 0 Step -1
                        If Me.Owner.Controls(I).Visible Then
                            Me.AddNotLifeObject(Me.Owner.Controls(I), True)
                        End If
                    Next
                    'For Each item As frmEditShape In Me.pbDraw.Controls
                    'If item.Visible Then
                    'Me.AddNotLifeObject(item, True)
                    'End If
                    'Next
                    Me.Owner.Controls.Clear()
                Case KarasMap.Mode.Play
                    Me.Owner.Image = Nothing
                    'For Each item As MapItem.iItem In Me.Items
                    'Me.AddLifeObject(item)
                    'Next
                    For I As Integer = Me.ItemsCount - 1 To 0 Step -1
                        'If Me.pbDraw.Controls(I).Visible Then
                        Me.AddLifeObject2(Me.Items(I))
                        'Me.AddNotLifeObject(Me.pbDraw.Controls(I), True)
                        'End If
                    Next
            End Select
            Me._Mode = Mode
        End Set
    End Property

    Public Function FindObjectsIDForPoint(ByVal Pt As Point) As Integer()
        Dim rez As New Collections.Generic.List(Of Integer)
        For I As Integer = 0 To Me.Items.Count - 1
            If Me.Items(I).Region.IsVisible(Pt) Then rez.Add(I)
        Next        
        Return rez.ToArray
    End Function

    Private Sub AddNotLifeObject(ByRef frm As frmEditShape, Optional ByVal move As Boolean = False)
        Dim I As Integer = Me.Items.Count
        If move Then
            frm.Data.Move(frm.Left, frm.Top)
        End If
        Me.Items.Add(MapItem.Tools.CloneItem(frm.Data))
        Me.Items(I).DrawContent(Me.Owner.Image)
    End Sub

    Private Sub AddLifeObject(ByRef item As MapItem.iItem, Optional ByVal X As Single = 0, Optional ByVal Y As Single = 0, Optional ByVal SelectIt As Boolean = False)
        If item Is Nothing Then Exit Sub
        Dim frm As New frmEditShape(item)
        Dim i As Integer = Me.Owner.Controls.Count
        With frm
            .Left = X
            .Top = Y
            .TopLevel = False
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .BackgroundImageLayout = ImageLayout.None
            .TransparencyKey = Color.Black
            .DropDownMenu = frmEdit.cmsForm
            .Width = item.Width
            .Height = item.Height
            .SelectedEvent = AddressOf SelectThatLifeObject
            item.DrawContent(frm)
            .Update()
            '.KeyPreview = True
            '.BackColor = Color.Transparent            
            .Region = item.Region
            If SelectIt Then .Selected = True
            '.BackColor = Color.White                        
            Me.Owner.Controls.Add(frm)
            Me.Owner.Controls.Item(i).Show()
        End With
        'Me.KeyPreview = True
    End Sub

    Private Delegate Sub dlgAddLifeObject2(ByRef item As MapItem.iItem, ByVal X As Single, ByVal Y As Single, ByVal SelectIt As Boolean)

    Public ShootEvent As frmEditShape.dlgShoot = Nothing

    Private Sub AddLifeObject2(ByRef item As MapItem.iItem, Optional ByVal X As Single = 0, Optional ByVal Y As Single = 0, Optional ByVal SelectIt As Boolean = False)
        If item Is Nothing Then Exit Sub
        Dim frm As New frmEditShape(item)        
        Dim i As Integer = Me.Owner.Controls.Count
        With frm
            .Left = X
            .Top = Y
            .TopLevel = False
            .FormBorderStyle = Windows.Forms.FormBorderStyle.None
            .BackgroundImageLayout = ImageLayout.None
            .TransparencyKey = Color.Black
            .DropDownMenu = frmEdit.cmsForm
            .Width = item.Width
            .Height = item.Height
            '.SelectedEvent = AddressOf SelectThatLifeObject
            .ShootEvent = Me.ShootEvent
            item.DrawContent(frm)
            .Update()
            .PlayMode = True
            '.KeyPreview = True
            '.BackColor = Color.Transparent            
            .Region = item.Region
            If SelectIt Then .Selected = True
            '.BackColor = Color.White                        
            Me.Owner.Controls.Add(frm)
            Me.Owner.Controls.Item(i).Show()
        End With
        'Me.KeyPreview = True
    End Sub

    Private Sub SelectThatLifeObject(ByRef Obj As frmEditShape)
        Me.SelectedItem = Obj
    End Sub

    Public Enum LifeEditOperations As SByte
        Copy = 0
        Cut = 1
        Paste = 2
        Delete = 3
        Move = 4
    End Enum

    Private _SelectedItem As frmEditShape

    Public Property SelectedItem() As frmEditShape
        Get
            Return Me._SelectedItem
        End Get
        Set(ByVal value As frmEditShape)
            Me._SelectedItem = value
        End Set
    End Property

    Public Sub DrawGrid()
        Me.DrawGrid(Me.Owner.BackgroundImage, 100, 100, 3, Color.LightGray)

        Me.DrawGrid(Me.Owner.BackgroundImage, 20, 20, 2, Color.LightGray)
        Me.DrawGrid(Me.Owner.BackgroundImage, 4, 4, 1, Color.LightGray)
    End Sub

    Private Sub DrawGrid(ByRef image As System.Drawing.Image, ByVal Width As Single, ByVal Height As Single, ByVal LineWidth As Single, ByVal Color As Color)
        Me.DrawGrid(image, 0, 0, Width, Height, LineWidth, Color)
    End Sub

    Private Sub DrawGrid(ByRef image As System.Drawing.Image, ByVal Top As Single, ByVal Left As Single, ByVal Width As Single, ByVal Height As Single, ByVal LineWidth As Single, ByVal Color As Color)
        Dim x As Single, y As Single
        Dim pen As New Pen(Color, LineWidth)
        Using gr As Graphics = System.Drawing.Graphics.FromImage(image)
            For x = Top To image.Width Step Width
                gr.DrawLine(pen, x, 0, x, image.Height)
            Next
            For y = Left To image.Height Step Height
                gr.DrawLine(pen, 0, y, image.Width, y)
            Next
        End Using
    End Sub

    Public Sub ExecLifeEditCommand(ByVal Command As LifeEditOperations, ByVal ParamArray params() As Object)
        Select Case Command
            Case LifeEditOperations.Copy
                Clipboard.SetData(MapItem.Tools.ClipboardFormat, MapItem.Tools.XMLSerialize(Me._SelectedItem.Data))
            Case LifeEditOperations.Cut
                Me.ExecLifeEditCommand(LifeEditOperations.Copy)
                Me.ExecLifeEditCommand(LifeEditOperations.Delete)
            Case LifeEditOperations.Paste
                Dim mapItem2 As MapItem.iItem = MapItem.Tools.XMLUnserialize(Clipboard.GetData(MapItem.Tools.ClipboardFormat))
                Me.AddLifeObject(mapItem2, 2, 2, True)
                'Me.SelectedItem = mapItem2
            Case LifeEditOperations.Delete
                Me._SelectedItem.Close()
                Me._SelectedItem = Nothing
                If Me.Owner.Controls.Count = 0 Then Exit Sub
                Dim frx As frmEditShape = Me.Owner.Controls.Item(0)
                frx.Selected = True
            Case LifeEditOperations.Move
                Me.SelectedItem.Left = params(0)
                Me.SelectedItem.Top = params(1)
        End Select
    End Sub

    Public Sub VerticalFlip()
        For Each item As MapItem.iItem In Me.Items
            item.VerticalFlip(Me.Owner.Image.Width)
        Next
    End Sub

    Public Shared Function IsValidMap(ByVal FileName As String) As Boolean

        Try
            Dim info As New IO.FileInfo(FileName)
            If Not info.Exists Then Return False
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

End Class
