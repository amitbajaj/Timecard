<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimeCard
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.bIsHoliday = New System.Windows.Forms.CheckBox()
        Me.logDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.tOutTime = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tInTime = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnAddRow = New System.Windows.Forms.Button()
        Me.btnLoadData = New System.Windows.Forms.Button()
        Me.btnSaveData = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.lblStartTime = New System.Windows.Forms.Label()
        Me.lblEndTime = New System.Windows.Forms.Label()
        Me.lblRegRate = New System.Windows.Forms.Label()
        Me.lblOT1 = New System.Windows.Forms.Label()
        Me.lblOT2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnParameters = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.bIsHoliday)
        Me.GroupBox1.Controls.Add(Me.logDate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.tOutTime)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.tInTime)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtUserId)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 267)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(594, 94)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Time Details"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(484, 70)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(82, 20)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(484, 46)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 20)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'bIsHoliday
        '
        Me.bIsHoliday.AutoSize = True
        Me.bIsHoliday.Location = New System.Drawing.Point(489, 25)
        Me.bIsHoliday.Name = "bIsHoliday"
        Me.bIsHoliday.Size = New System.Drawing.Size(78, 17)
        Me.bIsHoliday.TabIndex = 8
        Me.bIsHoliday.Text = "Is Holiday?"
        Me.bIsHoliday.UseVisualStyleBackColor = True
        '
        'logDate
        '
        Me.logDate.CustomFormat = ""
        Me.logDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.logDate.Location = New System.Drawing.Point(316, 23)
        Me.logDate.Name = "logDate"
        Me.logDate.Size = New System.Drawing.Size(111, 20)
        Me.logDate.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(255, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Date"
        '
        'tOutTime
        '
        Me.tOutTime.CustomFormat = ""
        Me.tOutTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tOutTime.Location = New System.Drawing.Point(316, 59)
        Me.tOutTime.Name = "tOutTime"
        Me.tOutTime.Size = New System.Drawing.Size(111, 20)
        Me.tOutTime.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(255, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Out Time"
        '
        'tInTime
        '
        Me.tInTime.CustomFormat = ""
        Me.tInTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.tInTime.Location = New System.Drawing.Point(68, 59)
        Me.tInTime.Name = "tInTime"
        Me.tInTime.Size = New System.Drawing.Size(111, 20)
        Me.tInTime.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "In Time"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "User Id"
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(68, 26)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(111, 20)
        Me.txtUserId.TabIndex = 0
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
        Me.btnAddRow.Location = New System.Drawing.Point(876, 252)
        Me.btnAddRow.Name = "btnAddRow"
        Me.btnAddRow.Size = New System.Drawing.Size(75, 23)
        Me.btnAddRow.TabIndex = 5
        Me.btnAddRow.Text = "Add Row"
        Me.btnAddRow.UseVisualStyleBackColor = True
        '
        'btnLoadData
        '
        Me.btnLoadData.Location = New System.Drawing.Point(795, 252)
        Me.btnLoadData.Name = "btnLoadData"
        Me.btnLoadData.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadData.TabIndex = 6
        Me.btnLoadData.Text = "Load Data"
        Me.btnLoadData.UseVisualStyleBackColor = True
        '
        'btnSaveData
        '
        Me.btnSaveData.Location = New System.Drawing.Point(714, 252)
        Me.btnSaveData.Name = "btnSaveData"
        Me.btnSaveData.Size = New System.Drawing.Size(75, 23)
        Me.btnSaveData.TabIndex = 7
        Me.btnSaveData.Text = "Save Data"
        Me.btnSaveData.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(633, 252)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(75, 23)
        Me.btnReport.TabIndex = 8
        Me.btnReport.Text = "Report"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'lblStartTime
        '
        Me.lblStartTime.AutoSize = True
        Me.lblStartTime.Location = New System.Drawing.Point(12, 16)
        Me.lblStartTime.Name = "lblStartTime"
        Me.lblStartTime.Size = New System.Drawing.Size(55, 13)
        Me.lblStartTime.TabIndex = 6
        Me.lblStartTime.Text = "Start Time"
        '
        'lblEndTime
        '
        Me.lblEndTime.AutoSize = True
        Me.lblEndTime.Location = New System.Drawing.Point(231, 16)
        Me.lblEndTime.Name = "lblEndTime"
        Me.lblEndTime.Size = New System.Drawing.Size(52, 13)
        Me.lblEndTime.TabIndex = 8
        Me.lblEndTime.Text = "End Time"
        '
        'lblRegRate
        '
        Me.lblRegRate.AutoSize = True
        Me.lblRegRate.Location = New System.Drawing.Point(12, 41)
        Me.lblRegRate.Name = "lblRegRate"
        Me.lblRegRate.Size = New System.Drawing.Size(70, 13)
        Me.lblRegRate.TabIndex = 11
        Me.lblRegRate.Text = "Regular Rate"
        '
        'lblOT1
        '
        Me.lblOT1.AutoSize = True
        Me.lblOT1.Location = New System.Drawing.Point(12, 67)
        Me.lblOT1.Name = "lblOT1"
        Me.lblOT1.Size = New System.Drawing.Size(54, 13)
        Me.lblOT1.TabIndex = 13
        Me.lblOT1.Text = "OT1 Rate"
        '
        'lblOT2
        '
        Me.lblOT2.AutoSize = True
        Me.lblOT2.Location = New System.Drawing.Point(211, 67)
        Me.lblOT2.Name = "lblOT2"
        Me.lblOT2.Size = New System.Drawing.Size(54, 13)
        Me.lblOT2.TabIndex = 15
        Me.lblOT2.Text = "OT2 Rate"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblOT2)
        Me.GroupBox2.Controls.Add(Me.lblOT1)
        Me.GroupBox2.Controls.Add(Me.lblRegRate)
        Me.GroupBox2.Controls.Add(Me.lblEndTime)
        Me.GroupBox2.Controls.Add(Me.lblStartTime)
        Me.GroupBox2.Location = New System.Drawing.Point(633, 275)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 86)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'btnParameters
        '
        Me.btnParameters.Location = New System.Drawing.Point(957, 252)
        Me.btnParameters.Name = "btnParameters"
        Me.btnParameters.Size = New System.Drawing.Size(75, 23)
        Me.btnParameters.TabIndex = 9
        Me.btnParameters.Text = "Parameters"
        Me.btnParameters.UseVisualStyleBackColor = True
        '
        'frmTimeCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 370)
        Me.Controls.Add(Me.btnParameters)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnSaveData)
        Me.Controls.Add(Me.btnLoadData)
        Me.Controls.Add(Me.btnAddRow)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "frmTimeCard"
        Me.Text = "Time Card"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents tOutTime As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents tInTime As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtUserId As TextBox
    Friend WithEvents logDate As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents bIsHoliday As CheckBox
    Friend WithEvents btnSave As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents btnAddRow As Button
    Friend WithEvents btnLoadData As Button
    Friend WithEvents btnSaveData As Button
    Friend WithEvents btnReport As Button
    Friend WithEvents lblStartTime As Label
    Friend WithEvents lblEndTime As Label
    Friend WithEvents lblRegRate As Label
    Friend WithEvents lblOT1 As Label
    Friend WithEvents lblOT2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnParameters As Button
End Class
