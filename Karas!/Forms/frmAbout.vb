Imports Tao.OpenGl
Imports Tao.OpenGl.Gl
Imports Tao.Platform.Windows

Public Class frmAbout

    Dim x As Single = 40
    Dim pause As Single = 0
    Dim lmodechange = 0
    Dim mode As RunMode = RunMode.Normal

    Dim texture As Integer

    Dim Images As System.Collections.ObjectModel.Collection(Of System.Drawing.Bitmap)    

    Enum RunMode As Byte
        Normal = 0
        Pause = 1
    End Enum

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Paleidžiame OpenGL komponentą
        Me.sogArea.InitializeContexts()

        ' Pradinė išvaizda
        glShadeModel(GL_SMOOTH)
        glClearColor(0.0F, 0.0F, 0.0F, 0.5F)
        glClearDepth(1.0F)
        glEnable(GL_DEPTH_TEST)
        glDepthFunc(GL_LEQUAL)
        glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST)

        Dim density As Single = 0.35
        Dim fogColor() As Single = {0.9, 0.9, 1.0, 0.2}
        glEnable(GL_FOG) 'Now we set the fog mode, here i have set it to GL_EXP2, which is quite nice looking.
        glFogi(GL_FOG_MODE, GL_LINEAR) 'Here we set the actual colour of our fog.
        glFogfv(GL_FOG_COLOR, fogColor) 'Now the density.
        glFogf(GL_FOG_DENSITY, density) 'And here I have set it up to look the nicest. In large projects, this may slow  down you application.
        glHint(GL_FOG_HINT, GL_NICEST)
        glFogf(GL_FOG_START, 2.0F)
        glFogf(GL_FOG_END, 10.0F)

        'glEnable(GL_STENCIL_TEST)
        'glStencilFunc(GL_ALWAYS, 1, 1)
        'glStencilOp(GL_KEEP, GL_KEEP, GL_REPLACE)
        'glDisable(GL_DEPTH_TEST)

        glEnable(GL_TEXTURE_2D)
        glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST)

        'glGenTextures(1, texture)
        glBindTexture(GL_TEXTURE_2D, texture)

        glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR)
        glTexParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR)

        glEnable(GL_BLEND)
        'glEnable(GL_LIGHTING)

    End Sub

    Private Sub sogArea_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles sogArea.Paint
        Me.Draw()
    End Sub

    Private Sub Draw()
        '   // Clear Screen And Depth Buffer
        glClear(GL_COLOR_BUFFER_BIT Or GL_DEPTH_BUFFER_BIT Or GL_STENCIL_BUFFER_BIT)
        'Gl.glClear(Gl.GL_COLOR_BUFFER_BIT)
        'glClearColor(0.0F + x / 100, 0.0F, 0.0F, 0.5F)
        '// Reset The Current Modelview Matrix
        glLoadIdentity()

        'Glu.gluLookAt(x, x, x, 0, 0, 0, 0, 1, 0)

        'Gl.glMatrixMode(Gl.GL_PROJECTION);
        'glRotatef(x * 10, 0.0F, 1.0F, 0.0F)
        'glTranslatef(0.0F, 0.0F, -7.0F) ';	// Translate Into The Screen 7.0 Units
        glLoadIdentity() ';					// Reset The Current Modelview Matrix
        'glTranslatef(1.5F, 0.0F, 0.0F + x / 100) ';				// Move Right 1.5 Units And Into The Screen 6.0
        'glRotatef(x * 5, 0.0F, 1.0F, 0.0F) ';	// Rotate The cube around the Y axis
        'glTranslatef(0, 0.0F, -4.0F) ';	// Move 1.5 Left And 6.0 Into The Screen.

        '        glRotatef(-x, 0.0F, 1.0F, 0.0F) ';

        '       glRotatef(-x, 0.0F, 0.1F, 0.0F)

        'glTranslatef(0, 0.0F, -4.0F)

        '        glRotatef(-x, 0.0F, 0.5F, 0.0F)


        'glTranslatef(4 * 2.5 / 2, 0, -4.0F)
        'glRotatef(-x, 0.0F, 1.0F, 0.0F)
        'glTranslatef(0.0, 0.0F, -2.0F)
        'glRotatef(-x, 0.0F, 1.0F, 0.0F)        
        'glTranslatef(0.0, 0.0F, -2.0F)
        'glRotatef(-x, 0.0F, 1.0F, 0.0F)

        'glDisable(GL_FOG)

        'glFogf(GL_FOG_START, x)
        'glFogf(GL_FOG_END, x + 10)
        glDisable(GL_TEXTURE_2D)
        glFogf(GL_FOG_END, 10.0F + Math.Sin(x / 100))

        glPushMatrix()
        glTranslatef(0.0F, 0.0F, -4.0F)        
        glScalef(2, 2, 50)
        '
        Draw3D.ACube()
        glPopMatrix()
        glEnable(GL_TEXTURE_2D)

        'Dim LightAmbient() As Single = {0.5F, 1.0F, 1.0F, 0.0F}
        'Dim LightDiffuse() As Single = {1.0F, 1.0F, 1.0F, 0.5F}
        'Dim LightPosition() As Single = {0.0F, 0.0F, 0.0F, 1.0F}

        'glLightfv(GL_LIGHT1, GL_AMBIENT, LightAmbient) ';		// Setup The Ambient Light
        'glLightfv(GL_LIGHT1, GL_DIFFUSE, LightDiffuse) ';		// Setup The Diffuse Light
        'glLightfv(GL_LIGHT1, GL_POSITION, LightPosition) ';	// Position The Light
        'glEnable(GL_LIGHT1) ';
        'glEnable(GL_FOG)

        glEnable(GL_BLEND)
        glDisable(GL_DEPTH_TEST)
        glColor4f(1, 1, 1, 1.0)

        glTranslatef(0.0, 0, -7.0F + Math.Sin(x / 10) * 4)

        Dim bmp As Bitmap = Nothing
        'drawing.DrawString(x.ToString, New System.Drawing.Font("Arial", 12, FontStyle.Regular), Brushes.BurlyWood, 0, 0)
        Me.DrawAppInfo(bmp)
        'drawing.Flush()

        Dim data As System.Drawing.Imaging.BitmapData = bmp.LockBits(New Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA, data.Width, data.Height, 0, GL_BGRA, GL_UNSIGNED_BYTE, data.Scan0)
        bmp.UnlockBits(data)

        Draw2D.Rectangle(-1, 1, 2, 2)
        glDisable(GL_BLEND)
        glEnable(GL_DEPTH_TEST)

        glFlush()
    End Sub

    Sub DrawAppInfo(ByRef bmp As Bitmap)

        Static Original As Bitmap = Nothing
        Static I As Byte = 0
        Static mode As Boolean = False

        Dim draw As System.Drawing.Graphics = Nothing        

        If Original Is Nothing Then

            Dim T As String
            Dim fnt1 As New System.Drawing.Font("Times New Roman", 12, FontStyle.Bold)

            With My.Application.Info
                T = My.Settings.About.Replace("{version}", .Version.ToString())
                T = T.Replace("{appname}", .ProductName)
            End With

            'Dim W As Single, H As Single
            'Draw2D.Measure(T, fnt1.Name, fnt1.Size, W, H)

            Original = New Bitmap(320, 320) 'CInt(W), CInt(H))
            draw = System.Drawing.Graphics.FromImage(Original)
            draw.SmoothingMode = Drawing2D.SmoothingMode.HighQuality

            draw.DrawImage(New Bitmap(File.GetTruePath("Textures\langeliai.jpg")), 0, 0, Original.Width, Original.Height)

            'draw.ScaleTransform(3.0, 3.0)
            'draw.FillRegion(Brushes.White, New System.Drawing.Region(New System.Drawing.Rectangle(0, 0, Original.Width, Original.Height)))
            draw.DrawString(T, fnt1, Brushes.Blue, 3, 4)
            draw.Flush()

        End If

        'If Me.mode = RunMode.Normal Then

        '        If mode Then
        '        I -= 5
        '       Else
        '      I += 5
        '     End If
        '
        'If I < 2 Then
        'mode = False
        'I = 1
        'End If
        'If I > 180 Then
        '        mode = True
        '       I = 180
        '      End If
        '
        'End If

        bmp = Original
        'bmp = ImageFx.Reflection(Original, Color.White, I)

    End Sub

    Private Sub tmrDraw_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDraw.Tick
        lmodechange = lmodechange + 1

        If mode = RunMode.Pause Then
            pause = pause + 1
            If pause > 20000 Then
                pause = 0
                mode = RunMode.Normal
                lmodechange = 0
            End If
        End If
        If mode = RunMode.Normal Then
            x = x + 1
            If x > 360 * 5 Then
                x = 0
            End If
        End If

        If Math.Sin(x / 10) > 0.99 And mode = RunMode.Normal And lmodechange > 20 Then
            mode = RunMode.Pause
            pause = 0
            lmodechange = 0
        End If

        'Me.Text = x
        Me.sogArea.Draw()


        'glCallList(SCENE);
    End Sub

    Private Sub sogArea_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles sogArea.Resize
        Dim height As Single = Me.sogArea.Height
        If height = 0 Then height = 1
        glViewport(0, 0, Me.sogArea.Width, height)

        glMatrixMode(GL_PROJECTION)
        glLoadIdentity()

        Glu.gluPerspective(45.0F, Me.sogArea.Width / height, 0.1F, 100.0F)

        glMatrixMode(GL_MODELVIEW)
        glLoadIdentity()

    End Sub


    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class