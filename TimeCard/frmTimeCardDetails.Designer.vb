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
        Me.lblTotalCost = New System.Windows.Forms.Label()
        Me.lblOT2 = New System.Windows.Forms.Label()
        Me.lblOT1 = New System.Windows.Forms.Label()
        Me.lblRegHrs = New System.Windows.Forms.Label()
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGVTimeCardDetails
        '
        Me.DGVTimeCardDetails.AllowUserToResizeRows = False
        Me.DGVTimeCardDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimeCardDetails.Location = New System.Drawing.Point(12, 49)
        Me.DGVTimeCardDetails.Name = "DGVTimeCardDetails"
        Me.DGVTimeCardDetails.Size = New System.Drawing.Size(823, 246)
        Me.DGVTimeCardDetails.TabIndex = 1
        '
        'cboUsers
        '
        Me.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsers.FormattingEnabled = True
        Me.cboUsers.Location = New System.Drawing.Point(12, 22)
        Me.cboUsers.Name = "cboUsers"
        Me.cboUsers.Size = New System.Drawing.Size(196, 21)
        Me.cboUsers.TabIndex = 2
        '
        'cboTimeCards
        '
        Me.cboTimeCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeCards.FormattingEnabled = True
        Me.cboTimeCards.Location = New System.Drawing.Point(214, 22)
        Me.cboTimeCards.Name = "cboTimeCards"
        Me.cboTimeCards.Size = New System.Drawing.Size(308, 21)
        Me.cboTimeCards.TabIndex = 3
        '
        'lblTotalCost
        '
        Me.lblTotalCost.AutoSize = True
        Me.lblTotalCost.Location = New System.Drawing.Point(795, 302)
        Me.lblTotalCost.Name = "lblTotalCost"
        Me.lblTotalCost.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalCost.TabIndex = 4
        Me.lblTotalCost.Text = "Label1"
        '
        'lblOT2
        '
        Me.lblOT2.AutoSize = True
        Me.lblOT2.Location = New System.Drawing.Point(428, 314)
        Me.lblOT2.Name = "lblOT2"
        Me.lblOT2.Size = New System.Drawing.Size(38, 13)
        Me.lblOT2.TabIndex = 15
        Me.lblOT2.Text = "lblOT2"
        '
        'lblOT1
        '
        Me.lblOT1.AutoSize = True
        Me.lblOT1.Location = New System.Drawing.Point(211, 314)
        Me.lblOT1.Name = "lblOT1"
        Me.lblOT1.Size = New System.Drawing.Size(38, 13)
        Me.lblOT1.TabIndex = 14
        Me.lblOT1.Text = "lblOT1"
        '
        'lblRegHrs
        '
        Me.lblRegHrs.AutoSize = True
        Me.lblRegHrs.Location = New System.Drawing.Point(12, 314)
        Me.lblRegHrs.Name = "lblRegHrs"
        Me.lblRegHrs.Size = New System.Drawing.Size(65, 13)
        Me.lblRegHrs.TabIndex = 13
        Me.lblRegHrs.Text = "lblRegHours"
        '
        'frmTimeCardDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(849, 336)
        Me.Controls.Add(Me.lblOT2)
        Me.Controls.Add(Me.lblOT1)
        Me.Controls.Add(Me.lblRegHrs)
        Me.Controls.Add(Me.lblTotalCost)
        Me.Controls.Add(Me.cboTimeCards)
        Me.Controls.Add(Me.cboUsers)
        Me.Controls.Add(Me.DGVTimeCardDetails)
        Me.Name = "frmTimeCardDetails"
        Me.Text = "frmTimeCardDetails"
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DGVTimeCardDetails As DataGridView
    Friend WithEvents cboUsers As ComboBox
    Friend WithEvents cboTimeCards As ComboBox
    Friend WithEvents lblTotalCost As Label
    Friend WithEvents lblOT2 As Label
    Friend WithEvents lblOT1 As Label
    Friend WithEvents lblRegHrs As Label
End Class
