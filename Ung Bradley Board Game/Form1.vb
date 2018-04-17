Public Class gameForm

    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = False
    Dim rotation As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        placePhase = True

        'Generates grid, assigns each cell properties
        Dim grid(63) As Label

        For k As Integer = 0 To 7
            For i As Integer = 0 To 7
                Dim cellIndex As Integer = k * 8 + i

                Dim cellBase As New Label
                cellBase.Name = "cell" + (cellIndex + 1).ToString
                cellBase.BorderStyle = BorderStyle.FixedSingle
                cellBase.Location = New Point(30 + 28 * i, 30 + 28 * k)
                cellBase.Size = New Size(30, 30)
                cellBase.Tag = cellIndex
                Me.Controls.Add(cellBase)

                grid(cellIndex) = cellBase
            Next
        Next

        For Each lb As Label In grid
            AddHandler lb.MouseEnter, AddressOf cell_MouseEnter
            AddHandler lb.MouseLeave, AddressOf cell_MouseLeave
            AddHandler lb.MouseClick, AddressOf cell_MouseClick
        Next
    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer
        Integer.TryParse(cell.Tag, cellNum)
        Dim row As Integer = (cellNum / 8) - 1
        Dim column As Integer = cellNum Mod 8


    End Sub

    Private Sub cell_MouseLeave(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)

        cell.BackColor = Me.BackColor
    End Sub

    Private Sub cell_MouseClick(sender As Object, e As EventArgs)

    End Sub

    Private Sub gameForm_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = " " Then
            MessageBox.Show("nice")
        End If
    End Sub
End Class
