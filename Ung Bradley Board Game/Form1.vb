Public Class gameForm

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cellBase As Label

        cellBase.BorderStyle = BorderStyle.FixedSingle


        Dim grid() As Label = {}

        For k As Integer = 0 To 7
            For i As Integer = 0 To 7
                cellBase.Name = "cell" + (k * 8 + i + 1).ToString
                Me.Controls.Add(cellBase)

                cellBase.Size = New System.Drawing.Size(30, 30)
                cellBase.Location = New System.Drawing.Point(30, 30)
                grid(k * 8 + i) = cellBase
            Next
        Next



        For Each lb As Label In grid
            AddHandler lb.MouseEnter, AddressOf cell_MouseEnter
            AddHandler lb.MouseLeave, AddressOf cell_MouseLeave
        Next
    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)

        cell.BackColor = Color.White
    End Sub

    Private Sub cell_MouseLeave(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)

        cell.BackColor = Me.BackColor
    End Sub
End Class
