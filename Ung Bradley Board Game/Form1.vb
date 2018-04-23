Public Class gameForm

    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = False
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim shipCellIndex(9) As Integer
    Dim validPlacement As Boolean = False
    Dim grid(7, 7) As Label

    Private Function selectedCells(x As Integer, y As Integer) As Integer()
        Dim returnedCells(shipLengthToPlace - 1, 2)


        If shipRotated Then

        Else

            For k As Integer = 0 To shipLengthToPlace - 1

                Dim selectedCellIndex As Integer

                Integer.TryParse(grid(k, y).Tag, selectedCellIndex)

                If selectfdasfds Then
                    validPlacement = False
                    Exit For
                ElseIf shipCellIndex.Contains(selectedCellIndex) Then
                    validPlacement = False
                Else

                    returnedCells(k
                    MessageBox.Show(returnedCells(0).Tag)
                End If
            Next
        End If

        For Each lb As Label In returnedCells
            MessageBox.Show(lb.Tag)
        Next
        Return returnedCells
    End Function

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        placePhase = True

        'Generates grid
        For k As Integer = 0 To 7
            For i As Integer = 0 To 7
                Dim cellIndex As Integer = i * 8 + k

                Dim cellBase As New Label
                cellBase.Name = "cell" & cellIndex.ToString
                cellBase.BorderStyle = BorderStyle.FixedSingle
                cellBase.Location = New Point(30 + 28 * k, 30 + 28 * i)
                cellBase.Size = New Size(30, 30)
                cellBase.Tag = cellIndex
                Me.Controls.Add(cellBase)

                grid(k, i) = cellBase
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
        Dim cellNum As Integer = cell.Tag

        'Gets x and y coords for the mouseover-ed cell
        Dim xPos As Integer = cellNum Mod 8
        Dim yPos As Integer = (cellNum - xPos + 1) / 8

        Dim cellsToFill(shipLengthToPlace - 1) As Label
        cellsToFill = selectedCells(xPos, yPos)
        MessageBox.Show(cellsToFill(1).Tag)

        For k As Integer = 0 To cellsToFill.Length - 1
            MessageBox.Show(cellsToFill(k).Tag)
        Next
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
