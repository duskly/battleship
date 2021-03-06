﻿Public Class gameForm

    Dim rand As New Random
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

    'Gameplay variables
    Dim playerTurn As Boolean = False

    Dim compShipCells As New ArrayList()
    Dim compShipsDestroyed As New ArrayList()
    Dim compShipsMissed As New ArrayList()

    Dim playerShipsDestroyed As New ArrayList()
    Dim playerShipsMissed As New ArrayList()

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

    Private Sub gameInit()
        For k As Integer = 0 To 10
            compShipCells.Add(grid(rand.Next(0, 8), rand.Next(0, 8)))
        Next

        startRound()
    End Sub

    Private Sub startRound()
        playerTurn = True
        switchBoard()

    End Sub

    Private Sub endSwitch()
        If shipCellLabels.Count = 0 Then
            MessageBox.Show("You lose.")
            MessageBox.Show("Sorry for rushed coding but you have to open the program again to start a new game :/")
            Application.Exit()
        ElseIf compShipCells.Count = 0
            MessageBox.Show("You win!")
            MessageBox.Show("Sorry for rushed coding but you have to open the program again to start a new game :/")
            Application.Exit()
        End If
    End Sub

    Private Sub playGame(cell As Label)
        If Not compShipsDestroyed.Contains(cell) And Not compShipsMissed.Contains(cell) Then
            If compShipCells.Contains(cell) Then
                compShipsDestroyed.Add(cell)
                compShipCells.Remove(cell)
                cell.BackColor = Color.Red
                MessageBox.Show("You hit!")
                endSwitch()
            Else
                compShipsMissed.Add(cell)
                cell.BackColor = Color.Black
                MessageBox.Show("You missed.")
            End If
            playerTurn = False
            switchBoard()

            shipTurn()
        End If
    End Sub

    Private Sub shipTurn()
        Dim cell As Label
        Dim xFired As Integer
        Dim yFired As Integer
        Do
            xFired = rand.Next(0, 8)
            yFired = rand.Next(0, 8)
            cell = grid(xFired, yFired)

            If Not playerShipsDestroyed.Contains(cell) And Not playerShipsMissed.Contains(cell) Then
                If shipCellLabels.Contains(cell) Then
                    playerShipsDestroyed.Add(cell)
                    shipCellLabels.Remove(cell)
                    cell.BackColor = Color.Red
                    MessageBox.Show("Computer hit!")
                    endSwitch()
                Else
                    playerShipsMissed.Add(cell)
                    cell.BackColor = Color.Black
                    MessageBox.Show("Computer miss.")
                End If
                Exit Do
            End If
        Loop

        startRound()
    End Sub

    Private Sub switchBoard()
        For Each lb As Label In grid
            lb.BackColor = Me.BackColor
        Next

        If playerTurn = True Then
            turnLbl.Text = "Your turn - click to fire a missle"
            boardLbl.Text = "Enemy board"

            For Each lb As Label In compShipsDestroyed
                lb.BackColor = Color.Red
            Next
            For Each lb As Label In compShipsMissed
                lb.BackColor = Color.Black
            Next
        Else
            turnLbl.Text = "Enemy turn"
            boardLbl.Text = "Your board"

            For Each lb As Label In shipCellLabels
                lb.BackColor = Color.DarkBlue
            Next
            For Each lb As Label In playerShipsDestroyed
                lb.BackColor = Color.Red
            Next
            For Each lb As Label In playerShipsMissed
                lb.BackColor = Color.Black
            Next
        End If
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

        MessageBox.Show("Welcome to Fleet Battle! I didn't have enough time to make an official instruction page, much less an actual tutorial, so just have this MessageBox explanation." & vbNewLine & "Basically this is like Battleship but the enemy has a bunch of individual small ships that are only 1x1 while you have one 5x1 and two 3x1 ships." & vbNewLine & vbNewLine & "btw this probably has some bugs (i'm aware of the highlighting bug in the ship placement phase), i didn't have enough time to do any animations, and yes, the randomization is really bad (but the spread of enemy ships balances it out so that's cool), so it is what it is :/")
    End Sub

    Private Sub cell_MouseLeave(sender As Object, e As EventArgs)
        Dim cell = DirectCast(sender, Label)

        If placePhase Then
            'Clear cells once mouse leaves
            Try
                For k As Integer = 0 To shipLengthToPlace - 1
                    If cell.Tag = 0 Or Not grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag = 0 And Not shipCellIndex.Contains(grid(cellsToFill(k, 0), cellsToFill(k, 1)).Tag) Then
                        grid(cellsToFill(k, 0), cellsToFill(k, 1)).BackColor = Me.BackColor
                    End If
                Next
            Catch ex As Exception
            End Try
        End If

    End Sub

    Private Sub cell_MouseEnter(sender As Object, e As EventArgs)

        Dim cell = DirectCast(sender, Label)
        Dim cellNum As Integer = cell.Tag

        justPlaced = False
        'Gets x and y coords for the mouseover-ed cell
        Dim xPos As Integer = cellNum Mod 8
        Dim yPos As Integer = (cellNum - xPos + 1) / 8

        If placePhase Then
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
        Else

        End If
    End Sub

    Private Sub cell_MouseClick(sender As Object, e As MouseEventArgs)
        Dim cell = DirectCast(sender, Label)

        If placePhase Then
            If e.Button = MouseButtons.Left Then
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

                If shipCellIndex.Count = 11 Then
                    placePhase = False
                    gameInit()
                End If
            ElseIf e.Button = MouseButtons.Right Then
                'Rotates ship placement indicator
                shipRotated = Not shipRotated
            End If
        ElseIf playerTurn Then
            playGame(cell)
        End If
    End Sub
End Class