<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class gameForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.turnLbl = New System.Windows.Forms.Label()
        Me.boardLbl = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Location = New System.Drawing.Point(30, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 224)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        Me.Label1.Visible = False
        '
        'turnLbl
        '
        Me.turnLbl.Location = New System.Drawing.Point(30, 258)
        Me.turnLbl.Name = "turnLbl"
        Me.turnLbl.Size = New System.Drawing.Size(224, 23)
        Me.turnLbl.TabIndex = 2
        Me.turnLbl.Text = "Left-click to place ship, right-click to rotate"
        '
        'boardLbl
        '
        Me.boardLbl.Location = New System.Drawing.Point(27, 7)
        Me.boardLbl.Name = "boardLbl"
        Me.boardLbl.Size = New System.Drawing.Size(100, 23)
        Me.boardLbl.TabIndex = 3
        Me.boardLbl.Text = "Your board"
        '
        'gameForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(300, 310)
        Me.Controls.Add(Me.boardLbl)
        Me.Controls.Add(Me.turnLbl)
        Me.Controls.Add(Me.Label1)
        Me.Name = "gameForm"
        Me.Text = "Fleet Battle"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents turnLbl As Label
    Friend WithEvents boardLbl As Label
End Class
