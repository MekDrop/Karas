Namespace MapItem

    Public Module Tools

        Public ReadOnly Property ClipboardFormat() As String
            Get
                Return My.Application.Info.ProductName + ":" + GetType(MapItem.iItem).FullName
            End Get
        End Property

        Public Function CloneItem(ByVal x As MapItem.iItem) As MapItem.iItem
            Return x
        End Function

        Public Function XMLSerialize(ByRef x As MapItem.iItem) As String            
            Dim Ser As New Xml.Serialization.XmlSerializer(x.GetType, My.Application.Info.ProductName)
            Dim ms As New IO.StringWriter()
            Ser.Serialize(ms, x)            
            Return ms.ToString
        End Function

        Public Function XMLSerialize(ByRef x As Collections.Generic.List(Of MapItem.iItem)) As String
            Dim wait As New frmProgressDialog(2)
            wait.StartFromBegining(x.Count, "Koduojama...")
            Dim items As New Collections.Specialized.StringCollection()
            For i As Integer = 0 To x.Count - 1
                items.Add(XMLSerialize(x(i)))
                wait.DoStep("Koduojama...")
            Next
            wait.StartFromBegining(3, "Koduojama...")
            Dim Ser As New Xml.Serialization.XmlSerializer(items.GetType, My.Application.Info.ProductName)
            wait.DoStep("Koduojama...")
            Dim ms As New IO.StringWriter()
            wait.DoStep("Koduojama...")
            Ser.Serialize(ms, items)
            wait.DoStep("Koduojama...")
            Return ms.ToString
        End Function

        Public Function XMLUnserialize(ByVal Text As String, Optional ByVal ShowDialog As Boolean = False) As Object
            Dim wait As frmProgressDialog = Nothing
            If ShowDialog Then wait = New frmProgressDialog(5)

            If ShowDialog Then wait.StartFromBegining(7, "Nustatomas duomenų tipas...")
            Dim data() As String = Text.Split(">")
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            Dim Type As String = data(1).Trim
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            Dim i As Long = Type.IndexOf(" ")
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            Dim p As Long = Type.IndexOf(vbTab)
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            If p < i And p > -1 Then i = p
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            Type = Type.Substring(1, i - 1).Trim
            If ShowDialog Then wait.DoStep("Nustatomas duomenų tipas...")
            Dim arx As Object
            Dim tt As System.Type
            Select Case Type
                Case "Wall"
                    tt = GetType(Wall)
                Case "StandartWeapon"
                    tt = GetType(StandartWeapon)
                Case "KingBoy"
                    tt = GetType(KingBoy)
                Case "ArrayOfString"
                    tt = GetType(System.Collections.Specialized.StringCollection)
                Case Else
                    MsgBox(Type)
                    Return Nothing
            End Select

            If ShowDialog Then wait.StartFromBegining("Pasiruošiama duomenų iškodavimui...")

            Dim ser As New Xml.Serialization.XmlSerializer(tt, My.Application.Info.ProductName)

            If ShowDialog Then wait.StartFromBegining("Iškoduojami duomenys...")
            Dim ms As New IO.StringReader(Text)
            arx = ser.Deserialize(ms)

            If Type = "ArrayOfString" Then
                Dim col As New Collections.Generic.List(Of MapItem.iItem)()
                Dim x As Collections.Specialized.StringCollection = arx
                If ShowDialog Then wait.StartFromBegining(x.Count)
                For i = 0 To x.Count - 1
                    col.Add(XMLUnserialize(x(i), False))
                    If ShowDialog Then wait.DoStep("Skaitomi subduomenys...")
                Next
                Return col
            End If
            Return arx
        End Function

    End Module


End Namespace