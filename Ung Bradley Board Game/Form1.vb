Public Class gameForm



    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = False
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim filledCells() As Integer
    Dim shipCellIndex() As Integer
    Dim validPlacement As Boolean = False
    Dim grid(63) As Label

    Private Sub fillCells(ByVal x As Integer, y As Integer)



    End Sub

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        placePhase = True

        'Generates grid
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

    Private Sub cell_MouseLeave(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)

        'cell.BackColor = Me.BackColor
    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer
        Integer.TryParse(cell.Tag, cellNum)
        Dim row As Integer = (cellNum / 8) - 1
        Dim column As Integer = cellNum Mod 8
        MessageBox.Show(column.ToString + " " + row.ToString)
        fillCells(column, row)

        If shipRotated Then

        Else

            For k As Integer = column To column + shipLengthToPlace

                Dim cellIndex As Integer = row * 8 + k

                If k > 7 Then
                    validPlacement = False
                    Exit For
                Else

                    If shipCellIndex.Contains(cellIndex) Then
                        validPlacement = False
                    Else

                        grid(cellIndex).BackColor = Color.DarkGray
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub cell_MouseClick(sender As Object, e As EventArgs)
        shipLengthToPlace -= 2

    End Sub

    Private Sub gameForm_SpaceKeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = " " Then
            MessageBox.Show("nice")
        End If
    End Sub
End Class
