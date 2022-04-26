'Jessica McArthur
'RCET0265   
'Spring 2020
'EtchASketch 
'https://github.com/jmcarth4/EtchASketch.git

Imports System.Math
Public Class EtchASketch
    Dim currentColor As Color

    'Lets user draw as want on the picture box
    Sub Sketch(startX As Integer, startY As Integer, endX As Integer, endY As Integer)
        Dim g As Graphics = PictureBox1.CreateGraphics
        Dim pen As New Pen(Me.currentColor)

        g.DrawLine(pen, startX, startY, endX, endY)
        g.Dispose()
        pen.Dispose()
    End Sub

    'Lets user pick color of pen 
    Sub PicKPenColor()
        ColorDialog1.ShowDialog()
        Me.currentColor = ColorDialog1.Color
    End Sub

    'Shakes displays when clear button pressed
    Sub Shake()
        Dim offset As Integer = 50

        For i = 0 To 12
            System.Threading.Thread.Sleep(200) 'waits 200ms
            Me.Left += offset
            Me.Top += offset
            offset *= -1 'inverts offset
        Next
    End Sub

    'Functions trace mouse movements of the user over the picture box
    Private Sub GraphicsForm_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        Static oldX, oldY As Integer
        Me.Text = $"({e.X},{e.Y}) Button: {e.Button.ToString()}"

        Select Case e.Button.ToString
            Case "Left"
                Sketch(oldX, oldY, e.X, e.Y)
            Case "Middle"
                PicKPenColor()

        End Select

        oldX = e.X
        oldY = e.Y

    End Sub

    'Draws differnt waveforms
    'Draws sine wave
    Sub DrawSinWave()
        Dim x, y, ymax, oldY, oldX As Integer
        Dim oldColor As Color = Me.currentColor
        Me.currentColor = Color.MediumPurple

        ymax = 145
        x = 45
        oldY = ymax

        For x = 0 To 360 Step 1
            y = CInt(ymax * Sin((x * (PI / 180)) * 1) + ymax)
            Sketch(oldX, oldY, x, y)
            oldX = x
            oldY = y
        Next
        Me.currentColor = oldColor
    End Sub

    'Draws cosine wave
    Sub DrawCosWave()
        Dim x, y, ymax, oldY, oldX As Integer
        Dim oldColor As Color = Me.currentColor
        Me.currentColor = Color.RoyalBlue

        ymax = 145
        x = 45
        oldY = ymax

        For x = 0 To 360 Step 1
            y = CInt(ymax * Cos((x * (PI / 180)) * 1) + ymax)
            Sketch(oldX, oldY, x, y)
            oldX = x
            oldY = y
        Next
        Me.currentColor = oldColor
    End Sub

    'Draws Tangent Wave
    Sub DrawTanWave()
        Dim x, y, ymax, oldY, oldX As Integer
        Dim oldColor As Color = Me.currentColor
        Me.currentColor = Color.Teal

        ymax = 50
        x = 45
        oldY = ymax

        For x = 0 To 360 Step 1
            Try
                y = CInt(ymax * Tan((x * (PI / 180)) * 1) + ymax)
                Sketch(oldX, oldY, x, y)
                oldX = x
                oldY = y
            Catch ex As Exception
            End Try
        Next
        Me.currentColor = oldColor
    End Sub

    'Draws divisions in picture box
    Sub DrawDivisions()
        Dim verticalDivisions As Integer = 10
        Dim horizontalDivisions As Integer = 10
        Dim oldColor As Color = Me.currentColor

        Me.currentColor = Color.LightBlue

        'Draw division lines top to bottom
        For v = 0 To 800 Step PictureBox1.Width \ verticalDivisions
            Sketch(v, 0, v, PictureBox1.Height)
        Next
        'Draw division lines left to right
        For h = 0 To 300 Step PictureBox1.Height \ horizontalDivisions
            Sketch(0, h, PictureBox1.Width, h)
        Next
        Me.currentColor = oldColor
    End Sub

    'Reset page 
    Private Sub Reset()
        Me.Refresh()
    End Sub

    'Buttons, and menus commands  
    Private Sub SelectColorButton_Click(sender As Object, e As EventArgs) _
        Handles SelectColorButton.Click, SelectColorToolStripMenuItem.Click, SelectColorToolStripMenuItem1.Click
        PicKPenColor()
    End Sub

    Private Sub DrawWaveformsButton_Click(sender As Object, e As EventArgs) _
        Handles DrawWaveformsButton.Click
        Reset()
        DrawDivisions()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub

    Private Sub ClearButton_Click(sender As Object, e As EventArgs) _
        Handles ClearButton.Click, ClearToolStripMenuItem1.Click, ClearToolStripMenuItem.Click
        Shake()
        Reset()
    End Sub

    Private Sub ExitButton_Click(sender As Object, e As EventArgs) _
        Handles ExitButton.Click, ExitToolStripMenuItem.Click, ExitToolStripMenuItem1.Click
        Me.Close()
    End Sub

    'Default settings for drawing
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentColor = Color.Black
    End Sub

    'Displays information about program when clicked
    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
        Handles AboutToolStripMenuItem1.Click, AboutToolStripMenuItem.Click
        MsgBox("This is an attempt of an Etch-a-Sketch.
              Use at your own risk")
    End Sub

    'Displays waveforms when selected in menus
    Private Sub DrawWaveformsToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles DrawWaveformsToolStripMenuItem.Click, DrawformsToolStripMenuItem.Click
        Reset()
        DrawDivisions()
        DrawSinWave()
        DrawCosWave()
        DrawTanWave()
    End Sub
End Class
