Imports Tao.OpenGl
Imports Tao.OpenGl.Gl
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class Draw2D

    Public Shared Sub Rectangle(ByVal X As Single, ByVal Y As Single, ByVal Width As Single, ByVal Height As Single)
        glBegin(GL_QUADS)
        glTexCoord2f(0.0F, 0.0F) : glVertex3f(X, Y, 0.0F)
        glTexCoord2f(1.0F, 0.0F) : glVertex3f(X + Width, Y, 0.0F)
        glTexCoord2f(1.0F, 1.0F) : glVertex3f(X + Width, Y - Height, 0.0F)
        glTexCoord2f(0.0F, 1.0F) : glVertex3f(X, Y - Height, 0.0F)
        glEnd()
    End Sub

    Public Shared Sub Measure(ByVal BannerText As String, _
      ByVal FontName As String, ByVal FontSize As Single, _
      ByRef Width As Single, ByRef Height As Single)

        Dim b As Bitmap
        Dim g As Graphics
        Dim f As New Font(FontName, FontSize)

        ' Compute the string dimensions in the given font

        b = New Bitmap(1, 1, PixelFormat.Format32bppArgb)
        g = Graphics.FromImage(b)
        Dim stringSize As SizeF = g.MeasureString(BannerText, f)
        Width = stringSize.Width
        Height = stringSize.Height
        g.Dispose()
        b.Dispose()

    End Sub

End Class
