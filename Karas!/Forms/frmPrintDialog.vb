Public Class frmPrintDialog

    Private WithEvents PrintDocument As System.Drawing.Printing.PrintDocument
    Private Image As Image

    Private WithEvents tmrPrintersUpdate As New Timers.Timer(5000)
    Private PrintersCount As Integer = -1

    Sub New(ByRef DrawArea As PictureBox)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.ppcPreview.Document = PrintDocument

        Me.Image = New Bitmap(DrawArea.Width + 100, DrawArea.Height + 100)
        '        Me.PrintDocument.DefaultPageSettings.Margins.Left = 0.0001
        '       Me.PrintDocument.DefaultPageSettings.Margins.Top = 0.0001
        '      Me.PrintDocument.DefaultPageSettings.Margins.Bottom = 0.0001
        '     Me.PrintDocument.DefaultPageSettings.Margins.Right = 0.0001
        Me.PrintDocument.OriginAtMargins = True
        Using gr As Graphics = Graphics.FromImage(Me.Image)
            If Not DrawArea.BackgroundImage Is Nothing Then
                gr.DrawImage(DrawArea.BackgroundImage, 0, 0)
            End If
            If Not DrawArea.Image Is Nothing Then
                gr.DrawImage(DrawArea.Image, 0, 0, DrawArea.BackgroundImage.Width, DrawArea.BackgroundImage.Height)
            End If
        End Using

        ' Add any initialization after the InitializeComponent() call.        
        frmMain.ucTabForms.Add(Me, ucTabForms.TabWorkMode.OnDeactivateClose)
    End Sub

    Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument.PrintPage
        Using gr As Graphics = e.Graphics
            gr.DrawImage(Me.Image, 0, 0)
        End Using
    End Sub

    Private Sub frmPrintDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim col As New Collections.Generic.List(Of String)
        Me.tscZoom.Items.Add("1%")
        For i As Integer = 10 To 100 Step 10
            Me.tscZoom.Items.Add(i.ToString + "%")
        Next
        For i As Integer = 200 To 1000 Step 100
            Me.tscZoom.Items.Add(i.ToString + "%")
        Next
        Me.tscZoom.SelectedIndex = Me.tscZoom.Items.IndexOf("100%")
        Me.tmrPrintersUpdate.Start()
        Me.tmrPrintersUpdate_Elapsed(sender, Nothing)
    End Sub

    Private Sub tscZoom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tscZoom.SelectedIndexChanged
        Me.ppcPreview.Zoom = Val(Me.tscZoom.Text) / 100
    End Sub

    Private Sub tscZoom_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tscZoom.TextChanged                
        Me.ppcPreview.Zoom = Val(Me.tscZoom.Text) / 100
    End Sub

    Private Sub PrintToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintToolStripButton.Click
        Me.PrintDocument.PrinterSettings.PrinterName = Me.tscPrinters.Items(Me.tscPrinters.SelectedIndex)
        Me.PrintDocument.Print()
    End Sub

    Private Sub tmrPrintersUpdate_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles tmrPrintersUpdate.Elapsed
        Try
            If System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count = Me.PrintersCount Then Exit Sub            
            For Each Printer As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
                Me.tscPrinters.Items.Add(Printer)
            Next
            If Me.tscPrinters.Items.Count > 0 Then
                Me.tscPrinters.SelectedIndex = 0
            Else
                Me.PrintToolStripButton.Enabled = False
            End If
            Me.PrintersCount = System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count
        Catch ex As Exception
            Me.PrintersCount = -1
        End Try        
    End Sub
End Class