Imports Tao.OpenGl
Imports Tao.OpenGl.Gl

Public Class Draw3D

    Public Shared Sub ACube()
        glBegin(GL_QUADS) '									// Draw A Quad
        glColor3f(0.0F, 1.0F, 0.0F) '						// Set The Color To Blue
        glVertex3f(1.0F, 1.0F, -1.0F) '					// Top Right Of The Quad (Top)
        glVertex3f(-1.0F, 1.0F, -1.0F) '					// Top Left Of The Quad (Top)
        glVertex3f(-1.0F, 1.0F, 1.0F) '					// Bottom Left Of The Quad (Top)
        glVertex3f(1.0F, 1.0F, 1.0F) '					// Bottom Right Of The Quad (Top)
        glColor3f(1.0F, 0.5F, 0.0F) '						// Set The Color To Orange
        glVertex3f(1.0F, -1.0F, 1.0F) '					// Top Right Of The Quad (Bottom)
        glVertex3f(-1.0F, -1.0F, 1.0F) '					// Top Left Of The Quad (Bottom)
        glVertex3f(-1.0F, -1.0F, -1.0F) '					// Bottom Left Of The Quad (Bottom)
        glVertex3f(1.0F, -1.0F, -1.0F) '					// Bottom Right Of The Quad (Bottom)
        'glColor3f(1.0F, 0.0F, 0.0F) '						// Set The Color To Red
        'glVertex3f(1.0F, 1.0F, 1.0F) '					// Top Right Of The Quad (Front)
        'glVertex3f(-1.0F, 1.0F, 1.0F) '					// Top Left Of The Quad (Front)
        'glVertex3f(-1.0F, -1.0F, 1.0F) '					// Bottom Left Of The Quad (Front)
        'glVertex3f(1.0F, -1.0F, 1.0F) '					// Bottom Right Of The Quad (Front)
        glColor3f(1.0F, 1.0F, 0.0F) '						// Set The Color To Yellow
        glVertex3f(1.0F, -1.0F, -1.0F) '					// Top Right Of The Quad (Back)
        glVertex3f(-1.0F, -1.0F, -1.0F) '					// Top Left Of The Quad (Back)
        glVertex3f(-1.0F, 1.0F, -1.0F) '					// Bottom Left Of The Quad (Back)
        glVertex3f(1.0F, 1.0F, -1.0F) '					// Bottom Right Of The Quad (Back)
        glColor3f(0.0F, 0.0F, 1.0F) '						// Set The Color To Blue
        glVertex3f(-1.0F, 1.0F, 1.0F) '					// Top Right Of The Quad (Left)
        glVertex3f(-1.0F, 1.0F, -1.0F) '					// Top Left Of The Quad (Left)
        glVertex3f(-1.0F, -1.0F, -1.0F) '					// Bottom Left Of The Quad (Left)
        glVertex3f(-1.0F, -1.0F, 1.0F) '					// Bottom Right Of The Quad (Left)
        glColor3f(1.0F, 0.0F, 1.0F) '						// Set The Color To Violet
        glVertex3f(1.0F, 1.0F, -1.0F) '					// Top Right Of The Quad (Right)
        glVertex3f(1.0F, 1.0F, 1.0F) '					// Top Left Of The Quad (Right)
        glVertex3f(1.0F, -1.0F, 1.0F) '					// Bottom Left Of The Quad (Right)
        glVertex3f(1.0F, -1.0F, -1.0F) '					// Bottom Right Of The Quad (Right)
        glEnd() '											// Done Drawing The Quad
    End Sub

End Class
