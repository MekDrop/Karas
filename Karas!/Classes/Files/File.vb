Public Class File

    Public Shared Function GetTruePath(ByVal File As String) As String

        ChDir(My.Application.Info.DirectoryPath)

        Dim cd As String = System.IO.Directory.GetCurrentDirectory()

        Do While Not System.IO.Directory.GetCurrentDirectory() = "\"

            If (New System.IO.FileInfo(File)).Exists Then
                Dim rez As String = System.IO.Directory.GetCurrentDirectory() + "\" + File
                System.IO.Directory.SetCurrentDirectory(cd)
                Return rez
            End If

            System.IO.Directory.SetCurrentDirectory("..")

        Loop

        System.IO.Directory.SetCurrentDirectory(cd)
        Return ""

    End Function

End Class
