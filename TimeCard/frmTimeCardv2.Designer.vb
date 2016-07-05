<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTimeCardv2
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.UserId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.InTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OutTime = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IsHoliday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.RegularHrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OT1Hrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.OT2Hrs = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalCost = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddRow = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.btnLoadData = New System.Windows.Forms.Button()
        Me.btnSaveData = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnParameters = New System.Windows.Forms.Button()
        Me.lblRegRate = New System.Windows.Forms.Label()
        Me.lblOT1 = New System.Windows.Forms.Label()
        Me.lblOT2 = New System.Windows.Forms.Label()
        Me.lblRegHrs = New System.Windows.Forms.Label()
        Me.btnCalculate = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.UserId, Me.InTime, Me.OutTime, Me.IsHoliday, Me.RegularHrs, Me.OT1Hrs, Me.OT2Hrs, Me.TotalCost, Me.AddRow})
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1058, 246)
        Me.DataGridView1.TabIndex = 0
        '
        'UserId
        '
        Me.UserId.HeaderText = "User Id"
        Me.UserId.Name = "UserId"
        '
        'InTime
        '
        DataGridViewCellStyle1.Format = "T"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.InTime.DefaultCellStyle = DataGridViewCellStyle1
        Me.InTime.HeaderText = "In Time"
        Me.InTime.Name = "InTime"
        '
        'OutTime
        '
        DataGridViewCellStyle2.Format = "T"
        Me.OutTime.DefaultCellStyle = DataGridViewCellStyle2
        Me.OutTime.HeaderText = "Out Time"
        Me.OutTime.Name = "OutTime"
        '
        'IsHoliday
        '
        Me.IsHoliday.HeaderText = "Is Holiday?"
        Me.IsHoliday.Name = "IsHoliday"
        Me.IsHoliday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.IsHoliday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'RegularHrs
        '
        DataGridViewCellStyle3.Format = "N0"
        DataGridViewCellStyle3.NullValue = "0"
        Me.RegularHrs.DefaultCellStyle = DataGridViewCellStyle3
        Me.RegularHrs.HeaderText = "Regular Hours"
        Me.RegularHrs.Name = "RegularHrs"
        Me.RegularHrs.ReadOnly = True
        '
        'OT1Hrs
        '
        DataGridViewCellStyle4.Format = "N0"
        DataGridViewCellStyle4.NullValue = "0"
        Me.OT1Hrs.DefaultCellStyle = DataGridViewCellStyle4
        Me.OT1Hrs.HeaderText = "Over Time 1"
        Me.OT1Hrs.Name = "OT1Hrs"
        Me.OT1Hrs.ReadOnly = True
        '
        'OT2Hrs
        '
        DataGridViewCellStyle5.Format = "N0"
        DataGridViewCellStyle5.NullValue = "0"
        Me.OT2Hrs.DefaultCellStyle = DataGridViewCellStyle5
        Me.OT2Hrs.HeaderText = "Over Time 2"
        Me.OT2Hrs.Name = "OT2Hrs"
        Me.OT2Hrs.ReadOnly = True
        '
        'TotalCost
        '
        DataGridViewCellStyle6.Format = "N0"
        DataGridViewCellStyle6.NullValue = "0"
        Me.TotalCost.DefaultCellStyle = DataGridViewCellStyle6
        Me.TotalCost.HeaderText = "Total Cost"
        Me.TotalCost.Name = "TotalCost"
        Me.TotalCost.ReadOnly = True
        '
        'AddRow
        '
        Me.AddRow.FillWeight = 80.0!
        Me.AddRow.HeaderText = "Add Row"
        Me.AddRow.Name = "AddRow"
        Me.AddRow.Text = "Add Row"
        Me.AddRow.Width = 75
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(115, 252)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 13)
        Me.lblStatus.TabIndex = 4
        '
        'btnAddRow
        '
        Me.btnAddRow.Location = New System.Drawing.Point(876, 261)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(75, 23)
        Me.btnAddRow.TabIndex = 5
        Me.btnAddRow.Text = "Add Row"
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'btnLoadData
        '
        Me.btnLoadData.Location = New System.Drawing.Point(795, 261)
        Me.btnLoadData.Name = "btnLoadData"
        Me.btnLoadData.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadData.TabIndex = 6
        Me.btnLoadData.Text = "Load Data"
        Me.btnLoadData.UseVisualStyleBackColor = True
        '
        'btnSaveData
        '
        Me.btnSaveData.Location = New System.Drawing.Point(714, 261)
        Me.btnSaveData.Name = "btnSaveData"
        Me.btnSaveData.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveData.TabIndex = 7
        Me.btnSaveData.Text = "Save Data"
        Me.btnSaveData.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(633, 261)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(75, 23)
        Me.btnReport.TabIndex = 8
        Me.btnReport.Text = "Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'btnParameters
        '
        Me.btnParameters.Location = New System.Drawing.Point(957, 261)
        Me.btnParameters.Name = "btnParameters"
        Me.btnParameters.Size = New System.Drawing.Size(75, 23)
        Me.btnParameters.TabIndex = 9
        Me.btnParameters.Text = "Parameters"
        Me.btnParameters.UseVisualStyleBackColor = True
        '
        'lblRegRate
        '
        Me.lblRegRate.AutoSize = True
        Me.lblRegRate.Location = New System.Drawing.Point(5, 275)
        Me.lblRegRate.Name = "lblRegRate"
        Me.lblRegRate.Size = New System.Drawing.Size(70, 13)
        Me.lblRegRate.TabIndex = 11
        Me.lblRegRate.Text = "Regular Rate"
        '
        'lblOT1
        '
        Me.lblOT1.AutoSize = True
        Me.lblOT1.Location = New System.Drawing.Point(179, 252)
        Me.lblOT1.Name = "lblOT1"
        Me.lblOT1.Size = New System.Drawing.Size(54, 13)
        Me.lblOT1.TabIndex = 13
        Me.lblOT1.Text = "OT1 Rate"
        '
        'lblOT2
        '
        Me.lblOT2.AutoSize = True
        Me.lblOT2.Location = New System.Drawing.Point(179, 275)
        Me.lblOT2.Name = "lblOT2"
        Me.lblOT2.Size = New System.Drawing.Size(54, 13)
        Me.lblOT2.TabIndex = 15
        Me.lblOT2.Text = "OT2 Rate"
        '
        'lblRegHrs
        '
        Me.lblRegHrs.AutoSize = True
        Me.lblRegHrs.Location = New System.Drawing.Point(5, 252)
        Me.lblRegHrs.Name = "lblRegHrs"
        Me.lblRegHrs.Size = New System.Drawing.Size(63, 13)
        Me.lblRegHrs.TabIndex = 16
        Me.lblRegHrs.Text = "Regular Hrs"
        '
        'btnCalculate
        '
        Me.btnCalculate.Location = New System.Drawing.Point(472, 261)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(75, 23)
        Me.btnCalculate.TabIndex = 17
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'frmTimeCardv2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 301)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.lblOT2)
        Me.Controls.Add(Me.lblRegHrs)
        Me.Controls.Add(Me.lblOT1)
        Me.Controls.Add(Me.btnParameters)
        Me.Controls.Add(Me.lblRegRate)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnSaveData)
        Me.Controls.Add(Me.btnLoadData)
        Me.Controls.Add(Me.btnAddRow)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmTimeCardv2"
        Me.Text = "Time Card"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents UserId As DataGridViewTextBoxColumn
    Friend WithEvents InTime As DataGridViewTextBoxColumn
    Friend WithEvents OutTime As DataGridViewTextBoxColumn
    Friend WithEvents IsHoliday As DataGridViewCheckBoxColumn
    Friend WithEvents RegularHrs As DataGridViewTextBoxColumn
    Friend WithEvents OT1Hrs As DataGridViewTextBoxColumn
    Friend WithEvents OT2Hrs As DataGridViewTextBoxColumn
    Friend WithEvents TotalCost As DataGridViewTextBoxColumn
    Friend WithEvents AddRow As DataGridViewButtonColumn
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAddRow As Button
    Friend WithEvents btnLoadData As Button
    Friend WithEvents btnSaveData As Button
    Friend WithEvents btnReport As Button
    Friend WithEvents btnParameters As Button
    Friend WithEvents lblRegRate As Label
    Friend WithEvents lblOT1 As Label
    Friend WithEvents lblOT2 As Label
    Friend WithEvents lblRegHrs As Label
    Friend WithEvents btnCalculate As Button
End Class
