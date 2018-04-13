Public Class gameForm
    Dim grid() As Control = {Label1}

    Private Sub box_MouseHover(sender As Object, e As EventArgs)
        MessageBox.Show("wooo")
    End Sub

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each lb As Label In grid
            AddHandler lb.MouseHover, AddressOf box_MouseHover
        Next
    End Sub
End Class
