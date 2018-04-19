Public Class gameForm



    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = False
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim shipCellIndex(9) As Integer
    Dim validPlacement As Boolean = False
    Dim grid(7, 7) As Label

    Private Function selectedCells(x As Integer, y As Integer) As Label()
        Dim returnedCells(shipLengthToPlace - 1) As Label

        If shipRotated Then

        Else

            For k As Integer = x To x + shipLengthToPlace
                Dim selectedCellIndex As Integer

                Integer.TryParse(grid(k, y).Tag, selectedCellIndex)

                If k > 7 Then
                    validPlacement = False
                    Exit For
                ElseIf shipCellIndex.Contains(selectedcellIndex) Then
                    validPlacement = False
                Else

                    returnedCells(k - x) = grid(k, y)
                    MessageBox.Show(returnedCells(k - x).Name)
                End If
            Next
        End If

        Return returnedCells
    End Function

    Private Sub gameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        placePhase = True

        'Generates grid
        For k As Integer = 0 To 7
            For i As Integer = 0 To 7
                Dim cellIndex As Integer = k * 8 + i

                Dim cellBase As New Label
                cellBase.Name = "cell" & cellIndex.ToString
                cellBase.BorderStyle = BorderStyle.FixedSingle
                cellBase.Location = New Point(30 + 28 * i, 30 + 28 * k)
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
        Dim yPos As Integer = cellNum Mod 8
        Dim xPos As Integer = (cellNum - yPos + 1) / 8

        For Each cellToFill As Label In selectedCells(xPos, yPos)
            cellToFill.BackColor = Color.DarkGray
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
