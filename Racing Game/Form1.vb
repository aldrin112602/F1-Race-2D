Imports System.Media
Public Class Form1
    Dim speed As Integer = 10
    Dim carSpeed As Integer = 14
    Dim score As Integer = 0
    Dim lives As Integer = 3
    Dim enemyHit() As Boolean = {False, False, False, False}
    Dim rnd As New Random()
    Private scorePlayer As New SoundPlayer
    Private carExplode As New SoundPlayer
    Dim zoomFactor As Double = 1.5
    Dim playerJump As Boolean = False
    Dim playerJumpSpeed As Integer = 10


    Sub UpdateScore(ByRef score As Integer, ByVal lives As Integer)
        If lives > 0 Then
            score += 1
            PlayScoreSoundFile()
            Label2.Text = score
        End If
    End Sub

    Sub resetForm()
        speed = 10
        carSpeed = 14
        score = 0
        lives = 3
        enemyHit = {False, False, False, False}
        playerJump = False
        playerJumpSpeed = 10
        Enemy1.Top = -1000
        Enemy2.Top = -3000
        Enemy3.Top = -400
        Enemy4.Top = -300
        lifeBar.Top = -5000
        Player.Left = 73
        Player.Top = 375
    End Sub

    Sub PlayScoreSoundFile()
        scorePlayer.SoundLocation = "C:\Users\Gliceria Ditablan\Downloads\point.wav"
        scorePlayer.Play()
    End Sub

    Sub carExplodeSoundFile()
        carExplode.SoundLocation = "C:\Users\Gliceria Ditablan\Downloads\car_explode.wav"
        carExplode.Play()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PictureBox1.Top += speed
        PictureBox2.Top += speed
        PictureBox3.Top += speed
        PictureBox4.Top += speed
        PictureBox5.Top += speed
        PictureBox6.Top += speed
        PictureBox7.Top += speed
        PictureBox8.Top += speed
        PictureBox9.Top += speed

        Enemy1.Top += rnd.Next(carSpeed, carSpeed + 2)
        Enemy2.Top += rnd.Next(carSpeed, carSpeed + 2)
        Enemy3.Top += rnd.Next(carSpeed, carSpeed + 2)
        Enemy4.Top += rnd.Next(carSpeed, carSpeed + 2)

        lifeBar.Top += rnd.Next(20, 40)
        If playerJump Then
            Player.Top -= playerJumpSpeed
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim pictureBoxes() As PictureBox = {PictureBox1, PictureBox2, PictureBox3, PictureBox4, PictureBox5, PictureBox6, PictureBox7, PictureBox8, PictureBox9}
        For Each pb As PictureBox In pictureBoxes
            If pb.Location.Y > 500 Then
                pb.Top = -pb.Size.Height
            End If
        Next


        If lifeBar.Location.Y > 500 Then
            lifeBar.Top = rnd.Next(-3000, 0)
        End If


        If Player.Bounds.IntersectsWith(lifeBar.Bounds) Then
            lives = lives + 1
            Label4.Text = lives
            lifeBar.Top = rnd.Next(-3000, 0)
        End If

        Dim enemiesPictureBox() As PictureBox = {Enemy1, Enemy2, Enemy3, Enemy4}


        For i As Integer = 0 To enemiesPictureBox.Length - 1
            Dim enemy As PictureBox = enemiesPictureBox(i)


            If Player.Bounds.IntersectsWith(enemy.Bounds) And Not enemyHit(i) And Not playerJump Then
                enemyHit(i) = True
                If lives > 0 Then
                    lives = lives - 1
                End If
                Label4.Text = lives
                enemy.Top = enemy.Top - (enemy.Size.Height * 3)
                If Player.Top + Player.Size.Height < 460 Then
                    Player.Top += 30
                End If

                carExplodeSoundFile()


                enemyHit(i) = False
            End If
            If enemy.Location.Y > 500 Then
                enemy.Top = rnd.Next(-2000, -500)
                UpdateScore(score, lives)
                If enemyHit(i) Then
                    lives = lives - 1
                    Label4.Text = lives
                End If
                enemyHit(i) = False

            End If

        Next


        If lives = 0 Then
            resetForm()
            'Me.Hide()
            'Form2.score.Text = score
            'Form2.Show()

        End If




    End Sub

    Sub StopJumpEffect()
        playerJump = False
        Player.Width = 44
        Player.Height = 70
    End Sub


    Sub setTimeout(ByVal func As Action, ByVal milliseconds As Integer)
        Dim timer As New Timer()
        timer.Interval = milliseconds
        AddHandler timer.Tick, Sub(sender, e)
                                   func.Invoke()
                                   timer.Stop()
                               End Sub
        timer.Start()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case 32
                If Not playerJump Then
                    Player.BringToFront()
                    Player.Width = CInt(44 * zoomFactor)
                    Player.Height = CInt(70 * zoomFactor)
                    playerJump = True
                    setTimeout(AddressOf StopJumpEffect, 1500)
                End If
            Case Keys.Up
                If Player.Top > 0 Then
                    Player.Top -= 10
                End If
            Case Keys.Down
                If Player.Top + Player.Size.Height < 460 Then
                    Player.Top += 10
                End If
            Case Keys.Left
                If Player.Left > 0 Then
                    Player.Left -= 10
                End If
            Case Keys.Right
                If Player.Right < 280 Then
                    Player.Left += 10
                End If
        End Select

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Enemy1.Top = -1000
        Enemy2.Top = -3000
        Enemy3.Top = -400
        Enemy4.Top = -300
        lifeBar.Top = -5000
    End Sub

End Class