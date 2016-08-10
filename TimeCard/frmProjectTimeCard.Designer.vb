<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjectTimeCard
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
        Me.lblTotalCost = New System.Windows.Forms.Label()
        Me.cboTimeCards = New System.Windows.Forms.ComboBox()
        Me.cboCustomers = New System.Windows.Forms.ComboBox()
        Me.DGVTimeCardDetails = New System.Windows.Forms.DataGridView()
        Me.cboProjects = New System.Windows.Forms.ComboBox()
        Me.lblRegHrs = New System.Windows.Forms.Label()
        Me.lblOT1 = New System.Windows.Forms.Label()
        Me.lblOT2 = New System.Windows.Forms.Label()
        Me.cboProjJobs = New System.Windows.Forms.ComboBox()
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTotalCost
        '
        Me.lblTotalCost.AutoSize = True
        Me.lblTotalCost.Location = New System.Drawing.Point(795, 289)
        Me.lblTotalCost.Name = "lblTotalCost"
        Me.lblTotalCost.Size = New System.Drawing.Size(39, 13)
        Me.lblTotalCost.TabIndex = 8
        Me.lblTotalCost.Text = "Label1"
        '
        'cboTimeCards
        '
        Me.cboTimeCards.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTimeCards.FormattingEnabled = True
        Me.cboTimeCards.Location = New System.Drawing.Point(665, 9)
        Me.cboTimeCards.Name = "cboTimeCards"
        Me.cboTimeCards.Size = New System.Drawing.Size(169, 21)
        Me.cboTimeCards.TabIndex = 7
        '
        'cboCustomers
        '
        Me.cboCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomers.FormattingEnabled = True
        Me.cboCustomers.Location = New System.Drawing.Point(12, 9)
        Me.cboCustomers.Name = "cboCustomers"
        Me.cboCustomers.Size = New System.Drawing.Size(196, 21)
        Me.cboCustomers.TabIndex = 6
        '
        'DGVTimeCardDetails
        '
        Me.DGVTimeCardDetails.AllowUserToResizeRows = False
        Me.DGVTimeCardDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimeCardDetails.Location = New System.Drawing.Point(12, 36)
        Me.DGVTimeCardDetails.Name = "DGVTimeCardDetails"
        Me.DGVTimeCardDetails.Size = New System.Drawing.Size(823, 246)
        Me.DGVTimeCardDetails.TabIndex = 5
        '
        'cboProjects
        '
        Me.cboProjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProjects.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboProjects.FormattingEnabled = True
        Me.cboProjects.Location = New System.Drawing.Point(214, 9)
        Me.cboProjects.Name = "cboProjects"
        Me.cboProjects.Size = New System.Drawing.Size(206, 21)
        Me.cboProjects.TabIndex = 9
        '
        'lblRegHrs
        '
        Me.lblRegHrs.AutoSize = True
        Me.lblRegHrs.Location = New System.Drawing.Point(12, 293)
        Me.lblRegHrs.Name = "lblRegHrs"
        Me.lblRegHrs.Size = New System.Drawing.Size(65, 13)
        Me.lblRegHrs.TabIndex = 10
        Me.lblRegHrs.Text = "lblRegHours"
        '
        'lblOT1
        '
        Me.lblOT1.AutoSize = True
        Me.lblOT1.Location = New System.Drawing.Point(211, 293)
        Me.lblOT1.Name = "lblOT1"
        Me.lblOT1.Size = New System.Drawing.Size(38, 13)
        Me.lblOT1.TabIndex = 11
        Me.lblOT1.Text = "lblOT1"
        '
        'lblOT2
        '
        Me.lblOT2.AutoSize = True
        Me.lblOT2.Location = New System.Drawing.Point(428, 293)
        Me.lblOT2.Name = "lblOT2"
        Me.lblOT2.Size = New System.Drawing.Size(38, 13)
        Me.lblOT2.TabIndex = 12
        Me.lblOT2.Text = "lblOT2"
        '
        'cboProjJobs
        '
        Me.cboProjJobs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProjJobs.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cboProjJobs.FormattingEnabled = True
        Me.cboProjJobs.Location = New System.Drawing.Point(426, 9)
        Me.cboProjJobs.Name = "cboProjJobs"
        Me.cboProjJobs.Size = New System.Drawing.Size(233, 21)
        Me.cboProjJobs.TabIndex = 13
        '
        'frmProjectTimeCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(846, 315)
        Me.Controls.Add(Me.cboProjJobs)
        Me.Controls.Add(Me.lblOT2)
        Me.Controls.Add(Me.lblOT1)
        Me.Controls.Add(Me.lblRegHrs)
        Me.Controls.Add(Me.cboProjects)
        Me.Controls.Add(Me.lblTotalCost)
        Me.Controls.Add(Me.cboTimeCards)
        Me.Controls.Add(Me.cboCustomers)
        Me.Controls.Add(Me.DGVTimeCardDetails)
        Me.MaximizeBox = False
        Me.Name = "frmProjectTimeCard"
        Me.Text = "ProjectTimeCard"
        CType(Me.DGVTimeCardDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTotalCost As Label
    Friend WithEvents cboTimeCards As ComboBox
    Friend WithEvents cboCustomers As ComboBox
    Friend WithEvents DGVTimeCardDetails As DataGridView
    Friend WithEvents cboProjects As ComboBox
    Friend WithEvents lblRegHrs As Label
    Friend WithEvents lblOT1 As Label
    Friend WithEvents lblOT2 As Label
    Friend WithEvents cboProjJobs As ComboBox
End Class
