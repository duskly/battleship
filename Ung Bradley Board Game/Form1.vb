Public Class gameForm

    'Keeps track of every cell and their properties
    Dim grid(7, 7) As Label

    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = True
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim shipCellIndex As New ArrayList()
    Dim shipCellLabels As New ArrayList()
    Dim flagCells As New ArrayList()
    Dim validPlacement As Boolean = False
    Dim cellsToFill As Integer(,)
    Dim availibleCells As Integer = 0
    Dim justPlaced As Integer = False

    'Computer variables
    Dim compShipCells As New ArrayList()

    'Gets the cells to be highlighted or filled with "ships"
    Private Function selectedCells(x As Integer, y As Integer) As Integer(,)

        availibleCells = 0
        validPlacement = True
        Dim returnedCells(0, 0) As Integer


        If shipRotated Then
            For k As Integer = 0 To shipLengthToPlace - 1

                Dim selectedCellIndex As Integer
                Try
                    Integer.TryParse(grid(x, y + k).Tag, selectedCellIndex)
                Catch ex As Exception
                    validPlacement = False
                    Exit For
                End Try

                If shipCellIndex.Contains(selectedCellIndex) Then
                    flagCells.Add(selectedCellIndex)
                    validPlacement = False
                Else
                    availibleCells += 1
                End If

            Next

            'Fill returnedCells with coords of each cell
            ReDim returnedCells(availibleCells - 1, 1)
            For k As Integer = 0 To availibleCells - 1
                If Not flagCells.Contains(grid(x, y + k).Tag) Then
                    returnedCells(k, 0) = x
                    returnedCells(k, 1) = y + k
                End If
            Next
        Else
            'Loop to find number of availible cells so ReDim works
            For k As Integer = 0 To shipLengthToPlace - 1

                Dim selectedCellIndex As Integer
                Try
                    Integer.TryParse(grid(k + x, y).Tag, selectedCellIndex)
                Catch ex As Exception
                    validPlacement = False
                    Exit For
                End Try

                If shipCellIndex.Contains(selectedCellIndex) Then
                    flagCells.Add(selectedCellIndex)
                    validPlacement = False
                Else
                    availibleCells += 1
                End If

            Next

            'Fill returnedCells with coords of each cell
            ReDim returnedCells(availibleCells - 1, 1)
            For k As Integer = 0 To availibleCells - 1
                If Not flagCells.Contains(grid(x + k, y).Tag) Then
                    returnedCells(k, 0) = x + k
                    returnedCells(k, 1) = y
                End If
            Next
        End If

        Return returnedCells
    End Function

    Private Sub compInit()
        For k As Integer = 0 To 8
            compShipCells.Add()
        Next
    End Sub

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
        
        'Adds event for each cell
        For Each lb As Label In grid
            AddHandler lb.MouseEnter, AddressOf cell_MouseEnter
            AddHandler lb.MouseLeave, AddressOf cell_MouseLeave
            AddHandler lb.MouseClick, AddressOf cell_MouseClick
        Next
    End Sub

    Private Sub cell_MouseLeave(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)
        'Clear cells once mouse leaves
        Try
            For k As Integer = 0 To shipLengthToPlace - 1
                If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 And Not shipCellIndex.Contains(grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag) Then
                    grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Me.BackColor
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)

        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer = cell.Tag

        justPlaced = False
        'Gets x and y coords for the mouseover-ed cell
        Dim xPos As Integer = cellNum Mod 8
        Dim yPos As Integer = (cellNum - xPos + 1) / 8

        cellsToFill = selectedCells(xPos, yPos)

        'Highlights cells
        Try
            For k As Integer = 0 To shipLengthToPlace - 1
                If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 And Not shipCellIndex.Contains(grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag) Then
                    grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Color.LightBlue
                End If
            Next
        Catch ex As Exception
        End Try

    End Sub

    Private Sub cell_MouseClick(sender As Object, e As MouseEventArgs)
        Dim cell = DirectCast(sender, Label)

        If e.Button = MouseButtons.Left And placePhase Then
            If validPlacement And Not justPlaced Then
                For k As Integer = 0 To shipLengthToPlace - 1
                    If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 Then
                        shipCellIndex.Add(grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag)
                        grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Color.DarkBlue
                        shipCellLabels.Add(grid(cellsToFill(k, 0), cellsToFill(k, 1)))
                    End If
                Next
                shipLengthToPlace = 3

                justPlaced = True
            Else
                MessageBox.Show("Error: invalid placement")
            End If

            If shipCellIndex.Count = 9 Then
                placePhase = False
            End If
        ElseIf e.Button = MouseButtons.Right And placePhase Then
            'Rotates ship placement indicator
            shipRotated = Not shipRotated
        End If
    End Sub
End Class
