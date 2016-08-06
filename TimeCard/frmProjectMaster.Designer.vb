<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectMaster
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DGVProjectMaster = New System.Windows.Forms.DataGridView()
        Me.cboCustomers = New System.Windows.Forms.ComboBox()
        CType(Me.DGVProjectMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVProjectMaster
        '
        Me.DGVProjectMaster.AllowUserToAddRows = False
        Me.DGVProjectMaster.AllowUserToDeleteRows = False
        Me.DGVProjectMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVProjectMaster.Location = New System.Drawing.Point(18, 38)
        Me.DGVProjectMaster.Name = "DGVProjectMaster"
        Me.DGVProjectMaster.Size = New System.Drawing.Size(622, 264)
        Me.DGVProjectMaster.TabIndex = 6
        '
        'cboCustomers
        '
        Me.cboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(18, 9)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.Size = New System.Drawing.Size(288, 21)
        Me.cboCustomers.TabIndex = 5
        '
        'frmProjectMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 337)
        Me.Controls.Add(Me.DGVProjectMaster)
        Me.Controls.Add(Me.cboCustomers)
        Me.Name = "frmProjectMaster"
        Me.Text = "frmProjectMaster"
        CType(Me.DGVProjectMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGVProjectMaster As DataGridView
    Friend WithEvents cboCustomers As ComboBox
End Class
