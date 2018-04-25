Public Class gameForm

    'Keeps track of every cell and their properties
    Dim grid(7, 7) As Label

    'Global variables for the initial ship placing phase
    Dim placePhase As Boolean = True
    Dim shipRotated As Boolean = False
    Dim ableToPlaceShip As Boolean = False
    Dim shipLengthToPlace As Integer = 5
    Dim shipCellIndex(9) As Integer
    Dim validPlacement As Boolean = False
    Dim cellsToFill As Integer(,)
    Dim availibleCells As Integer = 0

    Private Function selectedCells(x As Integer, y As Integer) As Integer(,)
        shipCellIndex(4) = 45
        validPlacement = True
        Dim returnedCells(0, 0) As Integer


        If shipRotated Then
            For k As Integer = 0 To shipLengthToPlace - 1
                Dim selectedCellIndex As Integer
                Try
                    Integer.TryParse(grid(x, y + k).Tag, selectedCellIndex)
                Catch ex As Exception
                    Exit For
                End Try

                If Not y + k > 7 Or Not shipCellIndex.Contains(selectedCellIndex) Then
                    availibleCells += 1
                Else
                    validPlacement = False

                End If
            Next

            ReDim returnedCells(availibleCells - 1, 1)
            For k As Integer = 0 To availibleCells - 1
                returnedCells(k, 0) = x
                returnedCells(k, 1) = y + k

            Next
        Else
            For k As Integer = 0 To shipLengthToPlace - 1

                Dim selectedCellIndex As Integer
                Try
                    Integer.TryParse(grid(k + x, y).Tag, selectedCellIndex)
                Catch ex As Exception
                    validPlacement = False
                    Exit For
                End Try

                If shipCellIndex.Contains(selectedCellIndex) Then
                    validPlacement = False
                Else
                    availibleCells += 1
                End If

            Next

            ReDim returnedCells(availibleCells - 1, 1)
            For k As Integer = 0 To availibleCells - 1
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

        For Each lb As Label In grid
            lb.BackColor = Me.BackColor
        Next
    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer = cell.Tag

        'Gets x and y coords for the mouseover-ed cell
        Dim xPos As Integer = cellNum Mod 8
        Dim yPos As Integer = (cellNum - xPos + 1) / 8

        cellsToFill = selectedCells(xPos, yPos)


        For k As Integer = 0 To shipLengthToPlace - 1
            If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 Then
                grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Color.DarkBlue
            End If
        Next


    End Sub

    Private Sub cell_MouseClick(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left And placePhase Then
            shipLengthToPlace -= 2

            If shipLengthToPlace = 1 Then
                placePhase = False
            End If
        ElseIf e.Button = MouseButtons.Right And placePhase Then
            shipRotated = Not shipRotated
            messagebox.show(validPlacement )
        End If


    End Sub

    Private Sub gameForm_SpaceKeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = " " Then
            MessageBox.Show("nice")
        End If
    End Sub
End Class
