Public Class frmPlayGame

    Private Delegate Sub dlgWaitSetValue(ByVal value As Boolean)

    Private GridMap As New KarasMap(pbDraw)

    Public Property WaitOtherPlayer() As Boolean
        Get
            Return Me.lblWaiting.Visible
        End Get
        Set(ByVal value As Boolean)
            If Me.lblWaiting.InvokeRequired Then
                Me.Invoke(New dlgWaitSetValue(AddressOf _WaitSetValue), value)
                'Me.lblWaiting
                Exit Property
            Else
                Me._WaitSetValue(value)
            End If
        End Set
    End Property

    Private Sub _WaitSetValue(ByVal value As Boolean)
        If Me.InvokeRequired Then
            Me.Invoke(New dlgWaitSetValue(AddressOf _WaitSetValue), value)
            Exit Sub
        End If
        Me.lblWaiting.Visible = value
        'TODO: need fix this
        ' Me.Enabled = Not value
    End Sub

    Public Map(0 To 1) As KarasMap
    Public MapCache() As PictureBox = {New PictureBox(), New PictureBox()}
    Public Connections(0 To 1) As NetworkConnectionItem

    Public IsMyMove As Boolean = False

    Sub Draw()
        Me.pbDraw.Width = 800
        Me.pbDraw.Height = 600
        Me.Map(0).Owner.Image = New Bitmap(400, 600)
        Me.Map(0).Owner.BackgroundImage = New Bitmap(400, 600)
        Me.Map(0).Owner = Me.pbDraw
        Me.Map(0).ShootEvent = AddressOf Me.Shoot
        Me.Map(0).WorkMode = KarasMap.Mode.Play
        Me.Map(1).Owner.Image = New Bitmap(400, 600)
        Me.Map(1).Owner.BackgroundImage = New Bitmap(400, 600)        
        'Me.Map(0).DrawContents()
        Me.Map(1).DrawContents()
        Me.pbDraw.Image = New Bitmap(800, 600)
        pbDraw.BackgroundImage = New Bitmap(800, 600)
        Me.GridMap.Owner = pbDraw        
        Me.GridMap.DrawGrid()
        Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
            'gr.DrawImage(Me.Map(0).Owner.Image, 0, 0)
            gr.DrawImage(Me.Map(1).Owner.Image, 400, 0)
        End Using        
    End Sub

    Public Enum GameCommands As SByte
        NOP = 0        
        MoveBegin = 1
        MakeMove = 2
        MoveEnd = 3
        Won = 4
        Loose = 5
        RemoveItem = 6
        CanceledGame = 7
    End Enum

    Public Sub SendGameCommand(ByRef Connection As NetworkConnectionItem, ByVal Command As GameCommands, ByVal ParamArray Params() As Object)
        Dim param As String = ""
        For Each p As Object In Params
            param += p.ToString + " "
        Next
        frmChat.SendSystemCommand(Me.Connections(1), frmChat.SystemCommands.GameCommand, Convert.ToSByte(Command).ToString + " " + Param.ToString)
    End Sub

    Private Delegate Sub dlgExecCommand(ByVal Command As GameCommands, ByVal params() As Object)

    Public EndedGame As Boolean = False

    Public Sub ExecCommand(ByVal Command As GameCommands, ByVal ParamArray params() As Object)
        Static cnt As Integer = 0
        If Me.InvokeRequired Then
            Me.Invoke(New dlgExecCommand(AddressOf ExecCommand), Command, params)
            Exit Sub
        End If
        Select Case Command
            Case GameCommands.MakeMove
                Dim pen As Pen = New Pen(Color.FromArgb(Convert.ToInt32(params(2))), 5)
                Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
                    gr.DrawEllipse(pen, 800 - Convert.ToSingle(params(0)), Convert.ToSingle(params(1)), 2, 2)
                    gr.Flush()
                End Using
                cnt -= 1
                If cnt < 1 Then
                    cnt = 21
                    Me.pbDraw.Refresh()
                End If
                'Me.pbDraw.Refresh()
            Case GameCommands.MoveBegin
                cnt = 21
                'Me.tmrUpdate.Start()
            Case GameCommands.MoveEnd
                'Me.tmrUpdate.Stop()
                Me.pbDraw.Refresh()
                Me.IsMyMove = True

                Me.Draw()
                Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
                    'gr.DrawImage(Me.Map(0).Owner.Image, 0, 0)
                    gr.DrawImage(Me.Map(1).Owner.Image, 400, 0)
                End Using
            Case GameCommands.Loose
                MsgBox("Pralaimėjai :(")
                Me.EndedGame = True
            Case GameCommands.Won
                MsgBox("Laimėjai :)")
                Me.EndedGame = True
            Case GameCommands.CanceledGame
                MsgBox("Žaidimas buvo nutrauktas")
                Me.EndedGame = True
            Case GameCommands.RemoveItem
                Dim cMapID As Integer = Convert.ToInt32(params(0))
                If cMapID = 0 Then
                    MsgBox("Pataikė :(")
                Else
                    MsgBox("Pataikė :)")
                End If
                Me.Map(cMapID).Items.RemoveAt(Convert.ToInt32(params(1)))

                'Me.Draw()
                'Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
                ''gr.DrawImage(Me.Map(0).Owner.Image, 0, 0)
                'gr.DrawImage(Me.Map(1).Owner.Image, 400, 0)
                'End Using
        End Select
        'MsgBox(Command)
    End Sub

    Private Sub Shoot(ByRef dt As MapItem.StandartWeapon)

        If Me.EndedGame Then Exit Sub

        Dim deg As Double = MapItem.PointsTools.XYToDegrees(dt.GunPos(1), dt.GunPos(0))
        Dim np As Point = dt.GunPos(1), onp As Point = dt.GunPos(1)
        Dim rg As New Region()
        Dim ids() As Integer
        Dim cMapID As Integer = 0
        Dim cR As Byte = 0, cB As Byte = 0, cG As Byte = 0, cBM As Boolean = True
        Dim dv As Integer = 0

        'If Part = 0 Then
        ' For i As Integer = 0 To Me.Map(Part).Items.Count
        'If Me.Map(Part).Items(i).Equals(dt) Then
        ' Me.SendGameCommand(Me.Connections(1), GameCommands.MakeMove, i)
        'Exit For
        'End If
        'Next
        'End If
        Me.SendGameCommand(Me.Connections(1), GameCommands.MoveBegin)

        Me.Map(0).WorkMode = KarasMap.Mode.Drawing
        'tmrUpdate.Start()

        Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
            'gr.DrawImage(Me.Map(0).Owner.Image, 0, 0)
            gr.DrawImage(Me.Map(1).Owner.Image, 400, 0)
        End Using

        Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
            Do
                np = MapItem.PointsTools.DegreesToXY(deg + Math.Sin(dv * 5) * 5, 1, onp)
                If np.X > 400 Then
                    cMapID = 1
                    ids = Me.Map(cMapID).FindObjectsIDForPoint(New Point(np.X - 400, np.Y))
                Else
                    cMapID = 0
                    ids = Me.Map(cMapID).FindObjectsIDForPoint(np)
                End If
                If ids.Length > 0 Then
                    If Me.Map(cMapID).Items(ids(0)).CanBeDestroyed Then
                        'Me.Map(cMapID).Items.RemoveAt(ids(0))
                        Me.Map(cMapID).Items.RemoveRange(ids(0), 1)
                        If cMapID = 0 Then                            
                            MsgBox("Pataikė :(")                            
                        Else
                            MsgBox("Pataikė :)")                            
                        End If
                        Me.SendGameCommand(Me.Connections(1), GameCommands.RemoveItem, 1 - cMapID, ids(0))
                        Exit Do
                    ElseIf Me.Map(cMapID).Items(ids(0)).CanDie Then
                        If cMapID = 0 Then
                            Me.SendGameCommand(Me.Connections(1), GameCommands.Won)
                            MsgBox("Pralaimėjai :(")
                        Else
                            Me.SendGameCommand(Me.Connections(1), GameCommands.Loose)
                            MsgBox("Laimėjai :)")
                        End If
                        Me.EndedGame = True
                        Me.IsMyMove = False
                        Exit Do
                    Else
                        deg += 60.6
                        'np = MapItem.PointsTools.DegreesToXY(deg + 45, 1, np)
                        Continue Do
                    End If
                End If
                If np.X < 0 Or np.Y < 0 Or np.Y > Me.pbDraw.Image.Height - 5 Or np.X > Me.pbDraw.Image.Width - 5 Then
                    deg += 45.45
                    Continue Do
                ElseIf onp.X = np.X And onp.Y = np.Y Then
                    deg += 2.759
                    Continue Do
                End If

                If Math.Abs(deg) > 359 Then deg = Math.Abs(deg - 360)

                onp = np
                'gm = 0
                '                bmp.SetPixel(np.X, np.Y, Color.Coral)
                'gr.DrawLine(New Pen(Color.Blue, 5), dt.GunPos(1), np)
                If cBM Then
                    If cR < 180 Then
                        cR += 1
                    ElseIf cG < 180 Then
                        cG += 1
                    ElseIf cB < 180 Then
                        cB += 1
                    Else
                        cBM = False
                    End If
                Else
                    If cR > 0 Then
                        cR -= 1
                    ElseIf cG > 0 Then
                        cG -= 1
                    ElseIf cB > 0 Then
                        cB -= 1
                    Else
                        cBM = True
                    End If
                End If

                gr.DrawEllipse(New Pen(Color.FromArgb(100, cR, cG, cB), 5), np.X, np.Y, 2, 2)
                Me.SendGameCommand(Me.Connections(1), GameCommands.MakeMove, np.X, np.Y, Color.FromArgb(100, cR, cG, cB).ToArgb)
                dv += 1
                If dv > 21 Then
                    gr.Flush()
                    My.Application.DoEvents()
                    Me.pbDraw.Refresh()
                    dv = 0
                End If
            Loop
            gr.Flush()
            Me.pbDraw.Refresh()
        End Using
        'tmrUpdate.Stop()        

        Me.Draw()

        Using gr As Graphics = Graphics.FromImage(Me.pbDraw.Image)
            'gr.DrawImage(Me.Map(0).Owner.Image, 0, 0)
            gr.DrawImage(Me.Map(1).Owner.Image, 400, 0)
        End Using

        Me.SendGameCommand(Me.Connections(1), GameCommands.MoveEnd)

        Dim c As Boolean = False
        For Each im As MapItem.iItem In Me.Map(0).Items
            c = c Or im.CanShoot
        Next
        If c = False Then
            Me.SendGameCommand(Me.Connections(1), GameCommands.Won)
            MsgBox("Pralaimėjai :(")
            Me.EndedGame = True
        Else
            c = False
            For Each im As MapItem.iItem In Me.Map(0).Items
                c = c Or im.CanShoot
            Next
            If c = False Then
                Me.SendGameCommand(Me.Connections(1), GameCommands.Loose)
                MsgBox("Laimėjai :)")
                Me.EndedGame = True
            End If
        End If

        '        If Part = 1 Then dt.Move(-400, 0)                

    End Sub

    Private Sub Shoot(ByRef item As frmEditShape)
        If Not IsMyMove Then Exit Sub
        Dim dt As MapItem.StandartWeapon = item.Data
        Me.Shoot(dt)
      
        'MsgBox("A")
    End Sub

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.        
        Me.pbDraw.Image = New Bitmap(800, 600)
    End Sub

    Sub Init(ByVal OtherPlayerMapData As Karas.DataCacheItem)
        Me.Map(0) = New KarasMap(My.Settings.UserMap, Me.MapCache(0))
        Me.Map(1) = New KarasMap(Me.MapCache(1))
        Me.Map(1).LoadFromDataCache(OtherPlayerMapData)
        Me.Draw()
    End Sub

    Sub SetConnections(ByRef Connection1 As NetworkConnectionItem, ByRef Connection2 As NetworkConnectionItem)
        Me.Connections(0) = Connection1
        Me.Connections(1) = Connection2
    End Sub

    Sub BeginGame()
        'MsgBox("game!")
    End Sub

    Private Sub tsbClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbClose.Click
        If Me.EndedGame = False Then
            Me.SendGameCommand(Me.Connections(1), GameCommands.CanceledGame)
        End If
        Me.Close()
    End Sub

    Private Sub pnlContent_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlContent.Paint

    End Sub

    Private Sub frmPlayGame_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class