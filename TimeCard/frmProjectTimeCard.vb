Public Class frmProjectTimeCard
    Private dbConnection As TimeCardDataAccess
    Private sNumberFormat As String = My.Settings.Item("NumberFormat")
    Private iDecimals As Integer = My.Settings.Item("NumberOfDecimals")
    Private Sub frmProjectTimeCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        lblTotalCost.Text = ""
        lblTotalCost.TextAlign = ContentAlignment.TopRight

        lblRegHrs.Text = ""
        lblRegHrs.TextAlign = ContentAlignment.TopRight

        lblOT1.Text = ""
        lblOT1.TextAlign = ContentAlignment.TopLeft

        lblOT2.Text = ""
        lblOT2.TextAlign = ContentAlignment.TopLeft


        InitializeGrid()
        LoadCustomerMaster()
    End Sub

    Private Sub LoadCustomerMaster()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim cdCustMaster As TimeCardSupport.CustomerDetails
        cboCustomers.Items.Clear()
        cboCustomers.DisplayMember = "DisplayName"
        sSQL = "SELECT RecordId, CustomerId, CustomerName FROM CustomerMaster"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                cdCustMaster = New TimeCardSupport.CustomerDetails
                With cdCustMaster
                    .recordId = dr.GetInt32(0)
                    .customerId = dr.GetString(1)
                    .customerName = dr.GetString(2)
                End With
                cboCustomers.Items.Add(cdCustMaster)
            End While
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As DataGridViewTextBoxColumn
        Dim col2 As DataGridViewCheckBoxColumn
        Dim col3 As DataGridViewButtonColumn
        DGVTimeCardDetails.AllowUserToAddRows = False
        DGVTimeCardDetails.AllowUserToDeleteRows = False
        DGVTimeCardDetails.AllowUserToOrderColumns = False
        DGVTimeCardDetails.AllowUserToResizeRows = False
        DGVTimeCardDetails.MultiSelect = False

        With DGVTimeCardDetails.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "RecordId"
            col1.ReadOnly = True
            col1.Width = 60
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "logDay"
            col1.HeaderText = "Day"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "inTime"
            col1.HeaderText = "In Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "outTime"
            col1.HeaderText = "Out Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col2 = New DataGridViewCheckBoxColumn()
            col2.Name = "isHoliday"
            col2.HeaderText = "Is Holiday"
            col2.ReadOnly = False
            col2.Width = 75
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col2)

            col2 = New DataGridViewCheckBoxColumn()
            col2.Name = "isAbsent"
            col2.HeaderText = "Is Absent"
            col2.ReadOnly = False
            col2.Width = 75
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col2)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "regularHrs"
            col1.HeaderText = "Regular Hours"
            col1.ReadOnly = True
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "OT1"
            col1.HeaderText = "Over Time 1"
            col1.ReadOnly = True
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "OT2"
            col1.HeaderText = "Over Time 2"
            col1.ReadOnly = True
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "totalCost"
            col1.HeaderText = "Total Cost"
            col1.ReadOnly = True
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col1)

            col3 = New DataGridViewButtonColumn()
            col3.Name = "addNextRow"
            col3.HeaderText = "A"
            col3.Text = "A"
            col3.UseColumnTextForButtonValue = True
            col3.ReadOnly = True
            col3.Width = 25
            col3.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col3)

            col3 = New DataGridViewButtonColumn()
            col3.Name = "delCurRow"
            col3.HeaderText = "D"
            col3.Text = "D"
            col3.UseColumnTextForButtonValue = True
            col3.ReadOnly = True
            col3.Width = 25
            col3.SortMode = DataGridViewColumnSortMode.NotSortable
            .Add(col3)

        End With
        DGVTimeCardDetails.AutoResizeColumns()
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Private Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim oProj As TimeCardSupport.ProjectDetails
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM CustomerProjects WHERE CustomerId = " & oCust.recordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProj = New TimeCardSupport.ProjectDetails()
                oProj.recordId = dr.GetInt32(0)
                oProj.projectId = dr.GetString(1)
                oProj.projectDescription = dr.GetString(2)
                oProj.projectRate = dr.GetDouble(3)
                cboProjects.Items.Add(oProj)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub

    Private Sub cboProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjects.SelectedIndexChanged
        cboTimeCards.Items.Clear()
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectJobs(cboProjects.SelectedItem)
        End If
    End Sub

    Private Sub LoadProjectJobs(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim oProjJob As TimeCardSupport.ProjectJobDetails
        cboTimeCards.Items.Clear()
        cboProjJobs.Items.Clear()
        cboProjJobs.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, JobId, JobDesc, JobRate FROM ProjectJobs WHERE projectId = " & oProj.recordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProjJob = New TimeCardSupport.ProjectJobDetails()
                With oProjJob
                    .recordId = dr.GetInt32(0)
                    .JobId = dr.GetString(1)
                    .JobDescription = dr.GetString(2)
                    .JobRate = dr.GetDouble(3)
                End With
                cboProjJobs.Items.Add(oProjJob)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub


    Private Sub LoadCustomerProjectTimeCards(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim tcmTimeCard As TimeCardSupport.TimeCardMasterDetails
        cboTimeCards.Items.Clear()
        cboTimeCards.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT TimeCardId, ProjectId, TimeCardNumber, TimeCardMonth, TimeCardYear FROM ProjectTimeCardMaster WHERE projectId = " & oProj.recordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                tcmTimeCard = New TimeCardSupport.TimeCardMasterDetails()
                tcmTimeCard.RecordId = dr.GetInt32(0)
                tcmTimeCard.UserId = dr.GetInt32(1)
                tcmTimeCard.TimeCardNumber = dr.GetInt32(2)
                tcmTimeCard.TimeCardMonth = dr.GetInt16(3)
                tcmTimeCard.TimeCardYear = dr.GetInt16(4)
                cboTimeCards.Items.Add(tcmTimeCard)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub

    Private Sub cboTimeCards_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTimeCards.SelectedIndexChanged
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        If cboTimeCards.SelectedIndex >= 0 Then
            LoadTimeCardDetails(cboTimeCards.SelectedItem)
        End If
    End Sub

    Private Sub LoadTimeCardDetails(oTimeCard As TimeCardSupport.TimeCardMasterDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim rw As DataGridViewRow
        Dim dReg, dOT1, dOT2, dTotalCost As Double
        Dim oGridTotal As New TimeCardSupport.GridTotals
        Dim oTime As New TimeCardSupport.TimeField
        Dim iFieldCount As Integer
        dReg = 0
        dOT1 = 0
        dOT2 = 0
        dTotalCost = 0
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT TimeCardDataId, TimeCardDay, IsHoliday, IsAbsent, InTime, OutTime, RegHrs, OT1Hrs, OT2Hrs, TotalCost FROM ProjectTimeCardDetailData WHERE TimeCardId = " & oTimeCard.RecordId
            dr = cmd.ExecuteReader()
            While dr.Read()
                iFieldCount = 1
                rw = DGVTimeCardDetails.Rows(DGVTimeCardDetails.Rows.Add())
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("logDay").Value = Nothing
                Else
                    rw.Cells("logDay").Value = dr.GetInt16(iFieldCount)
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("isHoliday").Value = Nothing
                Else
                    rw.Cells("isHoliday").Value = dr.GetBoolean(iFieldCount)
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("isAbsent").Value = Nothing
                Else
                    rw.Cells("isAbsent").Value = dr.GetBoolean(iFieldCount)
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("inTime").Value = Nothing
                Else
                    rw.Cells("inTime").Value = dr.GetDateTime(iFieldCount).TimeOfDay.ToString()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("outTime").Value = Nothing
                Else
                    rw.Cells("outTime").Value = dr.GetDateTime(iFieldCount).TimeOfDay.ToString()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("regularHrs").Value = Nothing
                Else
                    oTime.SetTime(dr.GetDouble(iFieldCount))
                    dReg = dReg + oTime.GetTime()
                    rw.Cells("regularHrs").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("OT1").Value = Nothing
                Else
                    oTime.SetTime(dr.GetDouble(iFieldCount))
                    dOT1 = dOT1 + oTime.GetTime()
                    rw.Cells("OT1").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("OT2").Value = Nothing
                Else
                    oTime.SetTime(dr.GetDouble(iFieldCount))
                    dOT2 = dOT2 + oTime.GetTime()
                    rw.Cells("OT2").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("totalCost").Value = Nothing
                Else
                    rw.Cells("totalCost").Value = dr.GetDouble(iFieldCount).ToString(sNumberFormat)
                    dTotalCost = dTotalCost + dr.GetDouble(8)
                End If
            End While
            oGridTotal.AddValues(dReg, dOT1, dOT2, dTotalCost)
            UpdateGridTotal(oGridTotal)
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            If DGVTimeCardDetails.Rows.Count = 0 Then
                DGVTimeCardDetails.Rows.Add()
                DGVTimeCardDetails.CurrentCell = DGVTimeCardDetails.Rows(0).Cells(0)
            End If
        End If
    End Sub

    Private Sub frmProjectTimeCard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVTimeCardDetails.Width = Width - 40
        DGVTimeCardDetails.Height = Height - 130
        lblTotalCost.Left = Width - lblTotalCost.Width - 40
        lblTotalCost.Top = Height - 70
        lblRegHrs.Top = Height - 70
        lblOT1.Top = Height - 70
        lblOT2.Top = Height - 70
    End Sub

    Sub UpdateGridTotal(oGridTotal As TimeCardSupport.GridTotals)
        lblTotalCost.Text = "Total Cost: " & Math.Round(oGridTotal.TotalCost, iDecimals).ToString(sNumberFormat)
        lblTotalCost.AutoSize = True
        lblTotalCost.Left = Width - lblTotalCost.Width - 40
        lblRegHrs.Text = "Total Regular : " & TimeCardSupport.DisplayDays(oGridTotal.RegularHours)
        lblOT1.Text = "Total OT1 : " & TimeCardSupport.DisplayDays(oGridTotal.OT1Hours)
        lblOT2.Text = "Total OT2 : " & TimeCardSupport.DisplayDays(oGridTotal.OT2Hours)

    End Sub

    Private Sub DGVTimeCardDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVTimeCardDetails.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVTimeCardDetails
                If TimeCardSupport.ValidateCell(cboTimeCards, DGVTimeCardDetails, .CurrentCell.ColumnIndex, .CurrentRow.Index, .CurrentCell.Value) Then
                    If .CurrentCell.ColumnIndex < .ColumnCount - 2 Then
                        .CurrentCell = .Rows.Item(.CurrentCell.RowIndex).Cells.Item(.CurrentCell.ColumnIndex + 1)
                    ElseIf .CurrentCell.RowIndex < .Rows.Count - 1 Then
                        .CurrentCell = .Rows.Item(.CurrentCell.RowIndex + 1).Cells.Item(0)
                    ElseIf .CurrentCell.RowIndex = .Rows.Count - 1 And .CurrentCell.ColumnIndex = .ColumnCount - 2 Then
                        .Rows.Add()
                        .CurrentCell = .Rows.Item(.Rows.Count - 1).Cells(0)
                    End If
                    e.Handled = True
                End If
            End With
        End If

    End Sub

    Private Sub DGVTimeCardDetails_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVTimeCardDetails.CellValidating
        e.Cancel = Not TimeCardSupport.ValidateCell(cboTimeCards, DGVTimeCardDetails, e.ColumnIndex, e.RowIndex, e.FormattedValue.ToString())
    End Sub

    Private Sub DGVTimeCardDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardDetails.CellEndEdit
        SaveData(e.RowIndex)
    End Sub

    Private Sub SaveData(iRow As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        Dim tInTime, tOutTime As Date
        Dim bNewRow As Boolean
        Dim bInTime, bOutTime, bHoliday As Boolean
        Dim oTime As New TimeCardSupport.TimeField
        rw = DGVTimeCardDetails.Rows(iRow)
        bInTime = False
        bOutTime = False
        bHoliday = False
        If rw.Cells("recordId").Value Is Nothing Then
            bNewRow = True
            sSQL = "INSERT INTO ProjectTimeCardDetailData(TimeCardId, TimeCardDay, IsHoliday, IsAbsent, InTime, OutTime, RegHrs, OT1Hrs, OT2Hrs, TotalCost) VALUES("
            sSQL = sSQL & cboTimeCards.SelectedItem.recordId
            sSQL = sSQL & ", "
            If rw.Cells("logDay").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("logDay").Value
            End If
            sSQL = sSQL & ", "
            If rw.Cells("isHoliday").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isHoliday").Value.ToString()
                bHoliday = rw.Cells("isHoliday").Value
            End If
            sSQL = sSQL & ", "
            If rw.Cells("isAbsent").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isAbsent").Value.ToString()
                bHoliday = rw.Cells("isAbsent").Value
            End If

            sSQL = sSQL & ", "
            If Date.TryParse(rw.Cells("inTime").Value, tInTime) Then
                sSQL = sSQL & "'" & tInTime.TimeOfDay.ToString() & "'"
                bInTime = True
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ","
            If Date.TryParse(rw.Cells("outTime").Value, tOutTime) Then
                sSQL = sSQL & "'" & tOutTime.TimeOfDay.ToString() & "'"
                bOutTime = True
            Else
                sSQL = sSQL & "NULL"
            End If
            If bInTime And bOutTime Then
                TimeCardSupport.CalculateCost(cboProjects.SelectedItem.ProjectRate, rw, iDecimals, sNumberFormat, tInTime, tOutTime, bHoliday)
                UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
            End If

            sSQL = sSQL & ", "
            If rw.Cells("regularHrs").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("regularHrs").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", "
            If rw.Cells("OT1").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("OT1").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", "
            If rw.Cells("OT2").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("OT2").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", "
            If rw.Cells("totalCost").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("totalCost").Value
            End If
            sSQL = sSQL & ")"
        Else
            sSQL = "UPDATE ProjectTimeCardDetailData SET "
            sSQL = sSQL & "TimeCardDay = "
            If rw.Cells("logDay").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("logDay").Value
            End If
            sSQL = sSQL & ", isHoliday = "
            If rw.Cells("isHoliday").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isHoliday").Value.ToString()
                bHoliday = rw.Cells("isHoliday").Value
            End If
            sSQL = sSQL & ", isAbsent = "
            If rw.Cells("isAbsent").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isAbsent").Value.ToString()
                bHoliday = rw.Cells("isAbsent").Value
            End If
            sSQL = sSQL & ", inTime = "
            If rw.Cells("inTime").Value IsNot Nothing Then
                If Date.TryParse(rw.Cells("inTime").Value.ToString(), tInTime) Then
                    sSQL = sSQL & "'" & tInTime.TimeOfDay.ToString() & "'"
                    bInTime = True
                Else
                    sSQL = sSQL & "NULL"
                End If
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ", outTime = "
            If rw.Cells("outTime").Value IsNot Nothing Then
                If Date.TryParse(rw.Cells("outTime").Value.ToString(), tOutTime) Then
                    sSQL = sSQL & "'" & tOutTime.TimeOfDay.ToString() & "'"
                    bOutTime = True
                Else
                    sSQL = sSQL & "NULL"
                End If
            Else
                sSQL = sSQL & "NULL"
            End If
            If bInTime And bOutTime Then
                TimeCardSupport.CalculateCost(cboProjects.SelectedItem.ProjectRate, rw, iDecimals, sNumberFormat, tInTime, tOutTime, bHoliday)
                UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
            End If
            sSQL = sSQL & ", RegHrs = "
            If rw.Cells("regularHrs").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("regularHrs").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", OT1Hrs = "
            If rw.Cells("OT1").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("OT1").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", OT2Hrs = "
            If rw.Cells("OT2").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTime.SetTime(rw.Cells("OT2").Value)
                sSQL = sSQL & oTime.GetTime()
            End If
            sSQL = sSQL & ", TotalCost = "
            If rw.Cells("totalCost").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("totalCost").Value
            End If
            sSQL = sSQL & " WHERE TimeCardDataId = " & rw.Cells("recordId").Value
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            cmd.ExecuteNonQuery()
            If bNewRow Then
                cmd.CommandText = "SELECT @@IDENTITY"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub DGVTimeCardDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardDetails.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardDetails.Columns("addNextRow").Index Then
            DGVTimeCardDetails.Rows.Add()
        End If

        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardDetails.Columns("delCurRow").Index Then
            RemoveRow(e.RowIndex)
        End If
    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim sRecordId As String
        If DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value IsNot Nothing Then
            sRecordId = DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
        Else
            sRecordId = ""
        End If
        If sRecordId <> "" Then
            If dbConnection.GetConnection() Then
                sSQL = "DELETE FROM ProjectTimeCardDetailData WHERE TimeCardDataId = " & DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                Try
                    cmd.ExecuteNonQuery()
                    DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
                Catch
                End Try
            End If
        Else
            DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
        End If
        UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
    End Sub

    Private Sub cboProjJobs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjJobs.SelectedIndexChanged
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        If cboProjJobs.SelectedIndex >= 0 Then
            LoadCustomerProjectTimeCards(cboProjJobs.SelectedItem)
        End If
    End Sub
End Class