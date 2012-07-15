Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Drawing2D

Public Class ImageFx

    ' http://www.codeproject.com/KB/GDI-plus/Image-Glass-Reflection.aspx
    Public Shared Function Reflection(ByRef _Image As Image, ByRef _BackgroundColor As Color, ByVal _Reflectivity As Integer) As Bitmap

        Dim height As Integer = CInt((_Image.Height + (_Image.Height * (CSng(_Reflectivity) / 255))))
        Dim newImage As New Bitmap(_Image.Width, height, PixelFormat.Format24bppRgb)
        newImage.SetResolution(_Image.HorizontalResolution, _Image.VerticalResolution)

        Using graphics As Graphics = graphics.FromImage(newImage)
            ' Initialize main graphics buffer
            graphics.Clear(_BackgroundColor)
            graphics.DrawImage(_Image, New Point(0, 0))
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic
            Dim destinationRectangle As New Rectangle(0, _Image.Size.Height, _Image.Size.Width, _Image.Size.Height)

            ' Prepare the reflected image
            Dim reflectionHeight As Integer = (_Image.Height * _Reflectivity) / 255
            Dim reflectedImage As Image = New Bitmap(_Image.Width, reflectionHeight)

            ' Draw just the reflection on a second graphics buffer
            Using gReflection As Graphics = graphics.FromImage(reflectedImage)
                gReflection.DrawImage(_Image, New Rectangle(0, 0, reflectedImage.Width, reflectedImage.Height), 0, _Image.Height - reflectedImage.Height, reflectedImage.Width, reflectedImage.Height, _
                GraphicsUnit.Pixel)
            End Using
            reflectedImage.RotateFlip(RotateFlipType.RotateNoneFlipY)
            Dim imageRectangle As New Rectangle(destinationRectangle.X, destinationRectangle.Y, destinationRectangle.Width, (destinationRectangle.Height * _Reflectivity) / 255)

            ' Draw the image on the original graphics
            graphics.DrawImage(reflectedImage, imageRectangle)

            ' Finish the reflection using a gradiend brush
            Dim brush As New LinearGradientBrush(imageRectangle, Color.FromArgb(255 - _Reflectivity, _BackgroundColor), _BackgroundColor, 90, False)
            graphics.FillRectangle(brush, imageRectangle)
        End Using

        Return newImage

    End Function

End Class
