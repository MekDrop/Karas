Public Class DataCacheItem

    Private _Content As Object = Nothing
    Private _CachePath As String = My.Application.Info.DirectoryPath + "/cache"
    Private _FileName As String = ""

    Private _Name As String, _Category As String
    Private _Type As DataType
    Private WithEvents _tmr As New System.Timers.Timer(5000)

    Public Enum DataType As Integer        
        Text = 0
        Image = 1
        DateTime = 2        
        Bytes = 3
    End Enum

    Public Shared Function GetDataType(ByRef Type As System.Type) As DataType
        Select Case Type.FullName
            Case "System.Drawing.Image", "System.Drawing.Bitmap"
                Return DataType.Image
            Case "System.DateTime"
                Return DataType.DateTime
            Case Else
                Return DataType.Text
        End Select
    End Function

    Public Shared Function GetDataType(Of Type)() As DataType
        Return GetDataType(GetType(Type))
    End Function

    Sub New(ByVal Type As DataType, ByVal Category As String, ByVal Name As String, Optional ByVal Content As Object = Nothing)
        Me._CachePath = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Category)
        Me._FileName = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Name)
        Me._Name = Name        
        Me._Category = Category
        If Category = "Avatars" And My.Settings.UserPlayerNickName = Name Then
            Me._Content = My.Settings.UserAvatar
            Me._Type = DataType.Image
        ElseIf Category = "CurrentMaps" And My.Settings.UserPlayerNickName = Name Then            
            Dim FS As New IO.FileStream(My.Settings.UserMap, IO.FileMode.Open)
            Dim br As New IO.BinaryReader(FS)
            Me._CachePath = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Category)
            Me._FileName = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Name)
            Me._Type = DataType.Bytes
            Me._Category = Category
            Me._Name = Name
            Me._Content = br.ReadBytes(FS.Length)
            FS.Close()
        ElseIf Category = "Names" And My.Settings.UserPlayerNickName = Name Then
            Me._Content = My.Settings.UserPlayerNickName
            Me._Type = DataType.Text
        ElseIf Category = "eMails" And My.Settings.UserPlayerNickName = Name Then
            Me._Content = My.Settings.UserEmail
            Me._Type = DataType.Text
        ElseIf Category = "birthdate" And My.Settings.UserPlayerNickName = Name Then
            Me._Content = My.Settings.UserBirthdayDate
            Me._Type = DataType.DateTime
        Else
            Me._Type = Type
            If Content Is Nothing Then
                If System.IO.File.Exists(Me._FileName + "." + Me._Type.ToString) Then
                    Me._Content = Me.ReadFile(Me._Type, Me._Type.ToString)
                End If
            Else
                Me._Content = Content
            End If
        End If        
    End Sub

    Sub New(ByVal FileName As String, ByVal Category As String, ByVal Name As String)
        Dim FS As New IO.FileStream(FileName, IO.FileMode.Open)
        Dim br As New IO.BinaryReader(FS)
        Me._CachePath = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Category)
        Me._FileName = Me._CachePath + "/" + System.Web.HttpUtility.UrlEncodeUnicode(Name)
        Me._Type = DataType.Bytes
        Me._Category = Category
        Me._Name = Name
        Me.Content = br.ReadBytes(FS.Length)
        'MsgBox(Me.DataString)
        FS.Close()        
    End Sub

    Public Property Type() As DataType
        Get
            Return Me._Type
        End Get
        Set(ByVal value As DataType)
            Me._Type = value
        End Set
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return Me._Name
        End Get
    End Property

    Public ReadOnly Property Category() As String
        Get
            Return Me._Category
        End Get
    End Property

    Public Property Content() As Object
        Get
            Return Me._Content
        End Get
        Set(ByVal value As Object)
            Me._Content = value
            If (New System.IO.FileInfo(Me._CachePath)).Exists = False Then
                System.IO.Directory.CreateDirectory(Me._CachePath)
            End If
            Me.Save()
        End Set
    End Property

    Public Sub Save()
        ' MsgBox(Me._Content)
        Me.WriteToFile(Me._Type, Me._Type.ToString, Me._Content)
    End Sub

    Public Function CGet(Of Type)() As Type
        Return Me.Content
    End Function

    Public Sub ClearCache()
        If System.IO.File.Exists(Me._FileName + "." + Me._Type.ToString) Then
            System.IO.File.Delete(Me._FileName + "." + Me._Type.ToString)
        End If
        Me._Content = Nothing
    End Sub

    Private Sub WriteToFile(ByVal Type As DataType, ByVal Ext As String, ByRef Data As Object)
        On Error Resume Next
        If Data Is Nothing Then
            Dim Nfo As New System.IO.FileInfo(Me._FileName + "." + Ext)
            Nfo.Delete()
            Exit Sub
        End If
        'MsgBox(Type)
        Dim File As New System.IO.FileStream(Me._FileName + "." + Ext, IO.FileMode.Create)
        'StreamWriter(Me._FileName + "." + Ext)        
        Select Case Type            
            Case DataType.Text, DataType.DateTime
                Dim bytes() As Byte = System.Text.Encoding.UTF32.GetBytes(Data.ToString)
                File.Write(bytes, 0, bytes.Length)
            Case DataType.Image
                Dim Image As System.Drawing.Image = Data
                Image.Save(File, System.Drawing.Imaging.ImageFormat.Jpeg)
            Case DataType.Bytes
                Dim bytes() As Byte = Me._Content
                File.Write(bytes, 0, bytes.Length)
                'MsgBox(Me._FileName + "." + Ext)
            Case Else
                MsgBox(Type)
        End Select
        '        MsgBox(Me._FileName + "." + Ext)
        File.Close()
    End Sub

    Private Function ReadFile(ByVal Type As DataType, ByVal Ext As String) As Object
        Dim ret As Object = Nothing
        Dim Fname As String = Me._FileName + "." + Ext
        If Not (New System.IO.FileInfo(Fname)).Exists Then Return Nothing
        If (New System.IO.FileInfo(Fname)).Length < 1 Then Return Nothing
        Dim File As New System.IO.FileStream(Fname, IO.FileMode.OpenOrCreate)
        Select Case Type            
            Case DataType.Text
                Dim File2 As New System.IO.StreamReader(File)
                ret = File2.ReadToEnd()
                File.Close()
            Case DataType.Image
                Dim Image As System.Drawing.Image = System.Drawing.Image.FromStream(File)
                ret = Image.Clone
                Image.Dispose()
                File.Close()
            Case DataType.DateTime
                Dim File2 As New System.IO.StreamReader(File)
                Dim dt As DateTime = DateTime.Parse(File2.ReadToEnd())
                ret = dt
                File.Close()
            Case DataType.Bytes
                Dim bn As New IO.BinaryReader(File)
                Me._Content = bn.ReadBytes(File.Length)
        End Select
        Return ret
    End Function

    ReadOnly Property Hash() As String
        Get
            Dim text As String = Me.DataString()
            If text Is Nothing Then text = ""
            Dim bytes() As Byte = System.Text.Encoding.Default.GetBytes(text)
            Return Convert.ToBase64String(System.Security.Cryptography.SHA1.Create().ComputeHash(bytes))
        End Get
    End Property

    ReadOnly Property Size() As Long
        Get
            Return Me.DataString().Length
        End Get
    End Property

    Public ReadOnly Property LastUpdateTime() As Date
        Get
            If System.IO.File.Exists(Me._FileName + "." + Me._Type.ToString) And System.IO.File.Exists(Me._FileName + ".info") Then
                Return System.IO.File.GetLastWriteTime(Me._FileName + "." + Me._Type.ToString)
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Property DataString() As String
        Get            
            Select Case Me._Type
                Case DataType.Text, DataType.DateTime
                    If Me._Content Is Nothing Then Return ""
                    Return System.Web.HttpUtility.UrlEncodeUnicode(Me._Content.ToString)
                Case DataType.Image
                    Dim Image As System.Drawing.Image = Me._Content
                    Dim _Image As New Karas.TextImage(Image)
                    Return _Image.Content
                Case DataType.Bytes
                    Return System.Web.HttpUtility.UrlEncodeUnicode(Convert.ToBase64String(Me._Content))
                Case Else
                    Return ""
            End Select
        End Get
        Set(ByVal value As String)            
            Select Case Me._Type
                Case DataType.Text
                    Me._Content = System.Web.HttpUtility.UrlDecode(value)
                Case DataType.Image
                    Me._Content = (New Karas.TextImage(value)).Image
                Case DataType.DateTime
                    Me._Content = System.DateTime.Parse(System.Web.HttpUtility.UrlDecode(value))
                Case DataType.Bytes
                    Me._Content = Convert.FromBase64String(System.Web.HttpUtility.UrlDecode(value))
                Case Else
                    Me._Content = Nothing
            End Select
            Me.Save()
        End Set
    End Property

    Public Property DataCollection() As System.Collections.Specialized.StringCollection
        Get
            Dim rez As New System.Collections.Specialized.StringCollection
            Dim Data As String = Me.DataString
            Dim length As Double = Data.Length
            Const piece As Double = 1024 * 2
            Dim count As Double = System.Math.Floor(length / piece)
            Dim i As Double, k As Double
            For i = 0 To count
                k = i * piece
                If k + piece > length Then
                    rez.Add(Data.Substring(i * piece))
                Else
                    rez.Add(Data.Substring(i * piece, piece))
                End If                
            Next            
            Return rez
        End Get
        Set(ByVal value As System.Collections.Specialized.StringCollection)
            Dim Rez As String = ""
            For Each item As String In value
                Rez += item
            Next
            Me.DataString = Rez
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return Me._Content.ToString()
    End Function

    Private Sub _tmr_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles _tmr.Elapsed
        Me.WriteToFile(Me._Type, Me._Type.ToString, Me._Content)
        Me._tmr.Stop()
    End Sub
End Class
