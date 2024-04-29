Public Class Form2

    
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Timer1.Enabled = False
        Form1.Timer2.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Dispose()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form1.Timer1.Enabled = True
        Form1.Timer2.Enabled = True
        Form1.Show()
    End Sub
End Class