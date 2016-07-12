<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTimeCardDetails
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
        Me.DGVTimeCardDetails = New System.Windows.Forms.DataGridView()
        Me.cboUsers = New System.Windows.Forms.ComboBox()
        Me.cboTimeCards = New System.Windows.Forms.ComboBox()
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVTimeCardDetails
        '
        Me.DGVTimeCardDetails.AllowUserToAddRows = False
        Me.DGVTimeCardDetails.AllowUserToDeleteRows = False
        Me.DGVTimeCardDetails.AllowUserToResizeRows = False
        Me.DGVTimeCardDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimeCardDetails.Location = New System.Drawing.Point(12, 49)
        Me.DGVTimeCardDetails.Name = "DGVTimeCardDetails"
        Me.DGVTimeCardDetails.Size = New System.Drawing.Size(1058, 246)
        Me.DGVTimeCardDetails.TabIndex = 1
        '
        'cboUsers
        '
        Me.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(13, 22)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(308, 21)
        Me.cboUsers.TabIndex = 2
        '
        'cboTimeCards
        '
        Me.cboTimeCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeCards.FormattingEnabled = True
        Me.cboTimeCards.Location = New System.Drawing.Point(355, 22)
        Me.cboTimeCards.Name = "cboTimeCards"
        Me.cboTimeCards.Size = New System.Drawing.Size(308, 21)
        Me.cboTimeCards.TabIndex = 3
        '
        'frmTimeCardDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1083, 307)
        Me.Controls.Add(Me.cboTimeCards)
        Me.Controls.Add(Me.cboUsers)
        Me.Controls.Add(Me.DGVTimeCardDetails)
        Me.Name = "frmTimeCardDetails"
        Me.Text = "frmTimeCardDetails"
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DGVTimeCardDetails As DataGridView
    Friend WithEvents cboUsers As ComboBox
    Friend WithEvents cboTimeCards As ComboBox
End Class
