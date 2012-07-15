
Namespace My
    
    'This class allows you to handle specific events on the settings class:
    ' The SettingChanging event is raised before a setting's value is changed.
    ' The PropertyChanged event is raised after a setting's value is changed.
    ' The SettingsLoaded event is raised after the setting values are loaded.
    ' The SettingsSaving event is raised before the setting values are saved.
    Partial Public NotInheritable Class MySettings

        'Private _Avatar As System.Drawing.Image = Nothing
        Private _Avatars As New DataCache(DataCacheItem.DataType.Image, "Avatars")

        Public Property UserAvatar() As System.Drawing.Image
            Get
                Return Me._Avatars.Item("user").Content
            End Get
            Set(ByVal value As System.Drawing.Image)
                Me._Avatars.Item("user").Content = value
            End Set
        End Property

        '     Public Sub SaveAvatar(ByVal Image As System.Drawing.Image)

        '   If Image Is Nothing Then
        '_Avatar = Nothing
        'Me.UserAvatar = Nothing
        'Exit Sub
        'End If
        'Dim MS As New System.IO.MemoryStream()
        'Image.Save(MS, System.Drawing.Imaging.ImageFormat.Jpeg)
        'Me.UserAvatar = System.Convert.ToBase64String(MS.GetBuffer())
        '_Avatar = New System.Drawing.Bitmap(Image)
        '      End Sub

        '       Public Function GetAvatar() As System.Drawing.Image
        'If Not _Avatar Is Nothing Then Return _Avatar
        'Try
        '            Dim MS As New System.IO.MemoryStream()
        '           Dim bytes() As Byte = System.Convert.FromBase64String(Me.UserAvatar)
        '          MS.Write(bytes, 0, bytes.Count)
        '         MS.Seek(0, IO.SeekOrigin.Begin)
        '        Dim Image As System.Drawing.Image = System.Drawing.Image.FromStream(MS)
        '       Return Image
        '      Catch ex As Exception
        'Return Nothing
        'End Try

        '        End Function

    End Class
End Namespace
