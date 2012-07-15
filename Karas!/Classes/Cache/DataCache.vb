Public Class DataCache

    Private Shared CachePath As String = My.Application.Info.DirectoryPath + "/cache"
    Private Category As String
    Private DataCollection As New System.Collections.Specialized.ListDictionary()
    Private Type As DataCacheItem.DataType

    Sub New(ByVal Type As DataCacheItem.DataType, Optional ByVal Category As String = "Unsorted")
        If (New System.IO.FileInfo(CachePath)).Exists = False Then
            System.IO.Directory.CreateDirectory(CachePath)
        End If
        Me.Category = Category
        Me.Type = Type
    End Sub

    Public Property Item(ByVal Name As String) As DataCacheItem
        Get
            If Me.DataCollection.Contains(Name) = False Then
                Me.DataCollection.Add(Name, New DataCacheItem(Me.Type, Me.Category, Name))
            End If
            Return Me.DataCollection.Item(Name)
        End Get
        Set(ByVal value As DataCacheItem)
            If Me.DataCollection.Contains(Name) = False Then
                Me.DataCollection.Add(Name, New DataCacheItem(Me.Type, Me.Category, Name))
            End If
            Me.DataCollection.Item(Name) = value
        End Set
    End Property

End Class
