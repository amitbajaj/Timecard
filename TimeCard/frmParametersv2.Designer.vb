<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmParametersv2
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRegHrs = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtOT2Rate = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtOT1Rate = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtRegularRate = New System.Windows.Forms.TextBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtRegHrs)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtOT2Rate)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtOT1Rate)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtRegularRate)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 70)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Regular Hours"
        '
        'txtRegHrs
        '
        Me.txtRegHrs.Location = New System.Drawing.Point(88, 15)
        Me.txtRegHrs.Name = "txtRegHrs"
        Me.txtRegHrs.Size = New System.Drawing.Size(111, 20)
        Me.txtRegHrs.TabIndex = 16
        Me.txtRegHrs.Text = "8"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(215, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(54, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "OT2 Rate"
        '
        'txtOT2Rate
        '
        Me.txtOT2Rate.Location = New System.Drawing.Point(287, 41)
        Me.txtOT2Rate.Name = "txtOT2Rate"
        Me.txtOT2Rate.Size = New System.Drawing.Size(111, 20)
        Me.txtOT2Rate.TabIndex = 14
        Me.txtOT2Rate.Text = "20"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "OT1 Rate"
        '
        'txtOT1Rate
        '
        Me.txtOT1Rate.Location = New System.Drawing.Point(88, 41)
        Me.txtOT1Rate.Name = "txtOT1Rate"
        Me.txtOT1Rate.Size = New System.Drawing.Size(111, 20)
        Me.txtOT1Rate.TabIndex = 12
        Me.txtOT1Rate.Text = "15"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(215, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Regular Rate"
        '
        'txtRegularRate
        '
        Me.txtRegularRate.Location = New System.Drawing.Point(287, 16)
        Me.txtRegularRate.Name = "txtRegularRate"
        Me.txtRegularRate.Size = New System.Drawing.Size(111, 20)
        Me.txtRegularRate.TabIndex = 10
        Me.txtRegularRate.Text = "10"
        '
        'cmdSave
        '
        Me.cmdSave.Location = New System.Drawing.Point(299, 89)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(116, 23)
        Me.cmdSave.TabIndex = 4
        Me.cmdSave.Text = "Save Parameters"
        Me.cmdSave.UseVisualStyleBackColor = True
        '
        'frmParametersv2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 120)
        Me.Controls.Add(Me.cmdSave)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParametersv2"
        Me.Text = "Parameters"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtOT2Rate As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtOT1Rate As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtRegularRate As TextBox
    Friend WithEvents cmdSave As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRegHrs As TextBox
End Class
