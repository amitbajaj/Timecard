<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmProjectPhases
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
            dbConnection.Dispose()
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
        Me.cboProjects = New System.Windows.Forms.ComboBox()
        Me.cboCustomers = New System.Windows.Forms.ComboBox()
        Me.DGVPhases = New TimeCard.MyDataGrid()
        Me.SuspendLayout()
        '
        'cboProjects
        '
        Me.cboProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProjects.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboProjects.FormattingEnabled = True
        Me.cboProjects.Location = New System.Drawing.Point(306, 9)
        Me.cboProjects.Name = "cboProjects"
        Me.cboProjects.Size = New System.Drawing.Size(328, 21)
        Me.cboProjects.TabIndex = 6
        '
        'cboCustomers
        '
        Me.cboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(12, 9)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.Size = New System.Drawing.Size(288, 21)
        Me.cboCustomers.TabIndex = 5
        '
        'DGVPhases
        '
        Me.DGVPhases.Location = New System.Drawing.Point(12, 36)
        Me.DGVPhases.Name = "DGVPhases"
        Me.DGVPhases.Size = New System.Drawing.Size(622, 236)
        Me.DGVPhases.TabIndex = 0
        '
        'frmProjectPhases
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 282)
        Me.Controls.Add(Me.cboProjects)
        Me.Controls.Add(Me.cboCustomers)
        Me.Controls.Add(Me.DGVPhases)
        Me.Name = "frmProjectPhases"
        Me.Text = "frmProjectPhases"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVPhases As MyDataGrid
    Friend WithEvents cboProjects As ComboBox
    Friend WithEvents cboCustomers As ComboBox
End Class
