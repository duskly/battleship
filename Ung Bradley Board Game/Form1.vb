Public Class gameForm

    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = False
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim shipCellIndex(9) As Integer
    Dim validPlacement As Boolean = False
    Dim grid(7, 7) As Label
    Dim cellsToFill As Integer(,)
    Dim availibleCellsIndex As Integer()

    Private Function selectedCells(x As Integer, y As Integer) As Integer(,)
        validPlacement = True
        availibleCellsIndex = Nothing
        Dim returnedCells(,) As Integer

        If shipRotated Then

        Else

            For k As Integer = 0 To shipLengthToPlace - 1

                Dim selectedCellIndex As Integer
                Integer.TryParse(grid(k, y).Tag, selectedCellIndex)

                If Not x + k > 7 Or Not shipCellIndex.Contains(selectedCellIndex) Then
                    availibleCellsIndex(k) = selectedCellIndex
                Else
                    validPlacement = False
                End If

                '    returnedCells(k, 0) = x + k
                '    returnedCells(k, 1) = y

            Next

            ReDim returnedCells(availibleCellsIndex.Length, 1)
            For k As Integer = 0 To availibleCellsIndex.Length - 1
                returnedCells(k, 0) = x + k
                returnedCells(k, 1) = y
            Next

        End If

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

        For k As Integer = 0 To shipLengthToPlace - 1
            grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Me.BackColor
        Next
    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer = cell.Tag

        'Gets x and y coords for the mouseover-ed cell
        Dim xPos As Integer = cellNum Mod 8
        Dim yPos As Integer = (cellNum - xPos + 1) / 8

        cellsToFill = selectedCells(xPos, yPos)


        For k As Integer = 0 To cellsToFill.Length - 1
            If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 Then
                grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Color.DarkBlue
            End If
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
