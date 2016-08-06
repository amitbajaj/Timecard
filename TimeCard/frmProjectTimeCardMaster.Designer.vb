<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectTimeCardMaster
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
        Me.DGVTimeCardMaster = New System.Windows.Forms.DataGridView()
        Me.cboCustomers = New System.Windows.Forms.ComboBox()
        Me.cboProjects = New System.Windows.Forms.ComboBox()
        CType(Me.DGVTimeCardMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVTimeCardMaster
        '
        Me.DGVTimeCardMaster.AllowUserToAddRows = False
        Me.DGVTimeCardMaster.AllowUserToDeleteRows = False
        Me.DGVTimeCardMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimeCardMaster.Location = New System.Drawing.Point(12, 37)
        Me.DGVTimeCardMaster.Name = "DGVTimeCardMaster"
        Me.DGVTimeCardMaster.Size = New System.Drawing.Size(622, 264)
        Me.DGVTimeCardMaster.TabIndex = 3
        '
        'cboCustomers
        '
        Me.cboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(12, 8)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.Size = New System.Drawing.Size(288, 21)
        Me.cboCustomers.TabIndex = 2
        '
        'cboProjects
        '
        Me.cboProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProjects.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboProjects.FormattingEnabled = True
        Me.cboProjects.Location = New System.Drawing.Point(306, 8)
        Me.cboProjects.Name = "cboProjects"
        Me.cboProjects.Size = New System.Drawing.Size(328, 21)
        Me.cboProjects.TabIndex = 4
        '
        'frmProjectTimeCardMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(647, 313)
        Me.Controls.Add(Me.cboProjects)
        Me.Controls.Add(Me.DGVTimeCardMaster)
        Me.Controls.Add(Me.cboCustomers)
        Me.Name = "frmProjectTimeCardMaster"
        Me.Text = "frmProjectTimeCardMaster"
        CType(Me.DGVTimeCardMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVTimeCardMaster As DataGridView
    Friend WithEvents cboCustomers As ComboBox
    Friend WithEvents cboProjects As ComboBox
End Class
