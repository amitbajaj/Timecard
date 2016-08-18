<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTimeCardMaster
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
        dbConnection.Dispose()
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.cboUsers = New System.Windows.Forms.ComboBox()
        Me.DGVTimeCardMaster = New System.Windows.Forms.DataGridView()
        CType(Me.DGVTimeCardMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboUsers
        '
        Me.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsers.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(13, 12)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(288, 21)
        Me.cboUsers.TabIndex = 0
        '
        'DGVTimeCardMaster
        '
        Me.DGVTimeCardMaster.AllowUserToAddRows = False
        Me.DGVTimeCardMaster.AllowUserToDeleteRows = False
        Me.DGVTimeCardMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimeCardMaster.Location = New System.Drawing.Point(13, 41)
        Me.DGVTimeCardMaster.Name = "DGVTimeCardMaster"
        Me.DGVTimeCardMaster.Size = New System.Drawing.Size(622, 264)
        Me.DGVTimeCardMaster.TabIndex = 1
        '
        'frmTimeCardMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 339)
        Me.Controls.Add(Me.DGVTimeCardMaster)
        Me.Controls.Add(Me.cboUsers)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimeCardMaster"
        Me.Text = "TimeCard Master"
        CType(Me.DGVTimeCardMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cboUsers As ComboBox
    Friend WithEvents DGVTimeCardMaster As DataGridView
End Class
