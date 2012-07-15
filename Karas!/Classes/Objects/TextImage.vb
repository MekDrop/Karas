Public Class TextImage

    Private _Content As String = ""

    Sub New(ByRef Image As System.Drawing.Image)
        Me.Image = Image
    End Sub

    Sub New(ByRef Content As System.String)
        Me.Content = Content
    End Sub

    Public Property Image() As System.Drawing.Image
        Get
            Try
                Dim MS As New System.IO.MemoryStream(System.Convert.FromBase64String(Me._Content))
                Dim Bitmap As System.Drawing.Image = System.Drawing.Image.FromStream(MS)
                Return Bitmap
            Catch ex As Exception
                Return Nothing
            End Try            
        End Get
        Set(ByVal value As System.Drawing.Image)
            If value Is Nothing Then
                Me._Content = ""
                Exit Property
            End If
            Dim bmp As New System.Drawing.Bitmap(value)
            Using MS As New System.IO.MemoryStream
                bmp.Save(MS, System.Drawing.Imaging.ImageFormat.Png)
                Me._Content = System.Convert.ToBase64String(MS.ToArray, Base64FormattingOptions.None)
            End Using
        End Set
    End Property

    Public Property Content() As String
        Get
            Return Me._Content
        End Get
        Set(ByVal value As String)
            Me._Content = value
        End Set
    End Property

End Class
