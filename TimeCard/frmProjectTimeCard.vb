Public Class frmProjectTimeCard
    Private dbConnection As TimeCardDataAccess
    Private sNumberFormat As String = My.Settings.Item("NumberFormat")
    Private iDecimals As Integer = My.Settings.Item("NumberOfDecimals")
    Private Sub frmProjectTimeCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = frmTimeCardMainForm.dbConn
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
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim sSQL As String
        Dim cdCustMaster As TimeCardSupport.CustomerDetails
        cboCustomers.Items.Clear()
        cboCustomers.DisplayMember = "DisplayName"
        sSQL = "SELECT RecordId, CustomerId, CustomerName FROM customerMaster"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                cdCustMaster = New TimeCardSupport.CustomerDetails
                With cdCustMaster
                    .recordId = dr.GetValue(0)
                    .customerId = dr.GetValue(1)
                    .customerName = dr.GetValue(2)
                End With
                cboCustomers.Items.Add(cdCustMaster)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
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
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Private Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim oProj As TimeCardSupport.ProjectDetails
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        cboTimeCards.Items.Clear()
        cboJobs.Items.Clear()
        cboPhases.Items.Clear()
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM customerProjects WHERE ParentId = @ParentId"
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oCust.recordId
            End With
            cmd.Parameters.Add(oParam)
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProj = New TimeCardSupport.ProjectDetails()
                oProj.recordId = dr.GetValue(0)
                oProj.projectId = dr.GetValue(1)
                oProj.projectDescription = dr.GetValue(2)
                oProj.projectRate = dr.GetValue(3)
                cboProjects.Items.Add(oProj)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub

    Private Sub cboProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjects.SelectedIndexChanged
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectPhases(cboProjects.SelectedItem)
        End If
    End Sub

    Private Sub LoadProjectPhases(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim oProjPhase As TimeCardSupport.ProjectPhaseDetails
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        cboTimeCards.Items.Clear()
        cboJobs.Items.Clear()
        cboPhases.Items.Clear()
        cboPhases.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, PhaseId, PhaseDesc FROM customerProjectPhases WHERE ParentId = @ParentId"
            cmd = dbConnection.Connection.CreateCommand()
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oProj.recordId
            End With
            cmd.Parameters.Add(oParam)
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProjPhase = New TimeCardSupport.ProjectPhaseDetails()
                With oProjPhase
                    .RecordId = dr.GetValue(0)
                    .PhaseId = dr.GetValue(1)
                    .PhaseDescription = dr.GetValue(2)
                End With
                cboPhases.Items.Add(oProjPhase)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub


    Private Sub LoadPhaseJobs(oPhase As TimeCardSupport.ProjectPhaseDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim oProjJob As TimeCardSupport.ProjectJobDetails
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        cboTimeCards.Items.Clear()
        cboJobs.Items.Clear()
        cboJobs.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, JobId, JobDesc, JobRate FROM customerProjPhaseJobs WHERE ParentId = @ParentId"
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oPhase.RecordId
            End With
            cmd.Parameters.Add(oParam)
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProjJob = New TimeCardSupport.ProjectJobDetails()
                With oProjJob
                    .recordId = dr.GetValue(0)
                    .JobId = dr.GetValue(1)
                    .JobDescription = dr.GetValue(2)
                    .JobRate = dr.GetValue(3)
                End With
                cboJobs.Items.Add(oProjJob)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub


    Private Sub LoadCustomerProjectTimeCards(oJob As TimeCardSupport.ProjectJobDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim tcmTimeCard As TimeCardSupport.TimeCardMasterDetails
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        cboTimeCards.Items.Clear()
        cboTimeCards.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, TimeCardNumber, TimeCardMonth, TimeCardYear FROM customerProjPhaseJobTimeCard WHERE ParentId = @ParentId"
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oJob.recordId
            End With
            cmd.Parameters.Add(oParam)
            dr = cmd.ExecuteReader()
            While dr.Read()
                tcmTimeCard = New TimeCardSupport.TimeCardMasterDetails()
                tcmTimeCard.RecordId = dr.GetValue(0)
                tcmTimeCard.TimeCardNumber = dr.GetValue(1)
                tcmTimeCard.TimeCardMonth = dr.GetValue(2)
                tcmTimeCard.TimeCardYear = dr.GetValue(3)
                cboTimeCards.Items.Add(tcmTimeCard)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub

    Private Sub cboTimeCards_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTimeCards.SelectedIndexChanged
        If cboTimeCards.SelectedIndex >= 0 Then
            LoadTimeCardDetails(cboTimeCards.SelectedItem)
        End If
    End Sub

    Private Sub LoadTimeCardDetails(oTimeCard As TimeCardSupport.TimeCardMasterDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim rw As DataGridViewRow
        Dim dReg, dOT1, dOT2, dTotalCost As Double
        Dim oGridTotal As New TimeCardSupport.GridTotals
        Dim oTime As New TimeCardSupport.TimeField
        Dim iFieldCount As Integer
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        lblRegHrs.Text = ""
        lblOT1.Text = ""
        lblOT2.Text = ""
        dReg = 0
        dOT1 = 0
        dOT2 = 0
        dTotalCost = 0
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, TimeCardDay, IsHoliday, IsAbsent, InTime, OutTime, RegHrs, OT1Hrs, OT2Hrs, TotalCost FROM customerProjPhaseJobTimeCardData WHERE ParentId = @ParentId ORDER BY TimeCardDay ASC"
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oTimeCard.RecordId
            End With
            cmd.Parameters.Add(oParam)
            dr = cmd.ExecuteReader()
            While dr.Read()
                iFieldCount = 1
                rw = DGVTimeCardDetails.Rows(DGVTimeCardDetails.Rows.Add())
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("logDay").Value = Nothing
                Else
                    rw.Cells("logDay").Value = dr.GetValue(iFieldCount)
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
                    If dbConnection.DBType = TimeCardDataAccess.DatabaseType.AccessDB Then
                        rw.Cells("inTime").Value = dr.GetDateTime(iFieldCount).TimeOfDay.ToString()
                    Else
                        rw.Cells("inTime").Value = dr.GetValue(iFieldCount)
                    End If
                End If
                    iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("outTime").Value = Nothing
                Else
                    If dbConnection.DBType = TimeCardDataAccess.DatabaseType.AccessDB Then
                        rw.Cells("outTime").Value = dr.GetDateTime(iFieldCount).TimeOfDay.ToString()
                    Else
                        rw.Cells("outTime").Value = dr.GetValue(iFieldCount)
                    End If
                End If
                    iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("regularHrs").Value = Nothing
                Else
                    oTime.SetTime(dr.GetValue(iFieldCount))
                    dReg = dReg + oTime.GetTime()
                    rw.Cells("regularHrs").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("OT1").Value = Nothing
                Else
                    oTime.SetTime(dr.GetValue(iFieldCount))
                    dOT1 = dOT1 + oTime.GetTime()
                    rw.Cells("OT1").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("OT2").Value = Nothing
                Else
                    oTime.SetTime(dr.GetValue(iFieldCount))
                    dOT2 = dOT2 + oTime.GetTime()
                    rw.Cells("OT2").Value = oTime.DisplayTime()
                End If
                iFieldCount = iFieldCount + 1
                If dr.IsDBNull(iFieldCount) Then
                    rw.Cells("totalCost").Value = Nothing
                Else
                    rw.Cells("totalCost").Value = dr.GetDecimal(iFieldCount).ToString(sNumberFormat)
                    dTotalCost = dTotalCost + dr.GetDecimal(iFieldCount)
                End If
            End While
            oGridTotal.AddValues(dReg, dOT1, dOT2, dTotalCost)
            UpdateGridTotal(oGridTotal)
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim rw As DataGridViewRow
        Dim tInTime, tOutTime As Date
        Dim bNewRow As Boolean
        Dim bInTime, bOutTime, bHoliday, bAbsent As Boolean
        Dim oTime As New TimeCardSupport.TimeField
        rw = DGVTimeCardDetails.Rows(iRow)
        bInTime = False
        bOutTime = False
        bHoliday = False
        bAbsent = False
        If rw.Cells("recordId").FormattedValue = "" Then
            bNewRow = True
            sSQL = "INSERT INTO customerProjPhaseJobTimeCardData (ParentId, TimeCardDay, IsHoliday, IsAbsent, InTime, OutTime, RegHrs, OT1Hrs, OT2Hrs, TotalCost) VALUES"
            sSQL = sSQL & "(@ParentId, @TimeCardDay, @IsHoliday, @IsAbsent, @InTime, @OutTime, @RegHrs, @OT1Hrs, @OT2Hrs, @TotalCost)"
        Else
            sSQL = "UPDATE customerProjPhaseJobTimeCardData  SET ParentId = @ParentId, TimeCardDay = @TimeCardDay, IsHoliday = @IsHoliday, IsAbsent = @IsAbsent, InTime = @InTime, OutTime = @OutTime, RegHrs = @RegHrs, OT1Hrs = @OT1Hrs, OT2Hrs = @OT2Hrs, TotalCost = @TotalCost WHERE RecordId = @RecordId"
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = cboTimeCards.SelectedItem.RecordId
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@TimeCardDay"
                .DbType = DbType.Int16
                If rw.Cells("logDay").Value Is Nothing Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("logDay").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@IsHoliday"
                .DbType = DbType.Boolean
                If rw.Cells("isHoliday").Value Is Nothing Then
                    .Value = False
                Else
                    .Value = rw.Cells("isHoliday").Value
                    bHoliday = rw.Cells("isHoliday").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@IsAbsent"
                .DbType = DbType.Boolean
                If rw.Cells("isAbsent").Value Is Nothing Then
                    .Value = False
                Else
                    .Value = rw.Cells("isAbsent").Value
                    bAbsent = rw.Cells("isAbsent").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@InTime"
                .DbType = DbType.DateTime
                If Date.TryParse(rw.Cells("inTime").FormattedValue, tInTime) Then
                    .Value = tInTime.TimeOfDay.ToString()
                    bInTime = True
                Else
                    .Value = DBNull.Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@OutTime"
                .DbType = DbType.DateTime
                If Date.TryParse(rw.Cells("outTime").FormattedValue, tOutTime) Then
                    .Value = tOutTime.TimeOfDay.ToString()
                    bOutTime = True
                Else
                    .Value = DBNull.Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            If bInTime And bOutTime Then
                TimeCardSupport.CalculateCost(cboProjects.SelectedItem.ProjectRate, rw, iDecimals, sNumberFormat, tInTime, tOutTime, bHoliday, bAbsent)
                UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
            End If
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@RegHrs"
                .DbType = DbType.Double
                If rw.Cells("regularHrs").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    oTime.SetTime(rw.Cells("regularHrs").Value)
                    .Value = oTime.GetTime()
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@OT1Hrs"
                .DbType = DbType.Double
                If rw.Cells("OT1").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    oTime.SetTime(rw.Cells("OT1").Value)
                    .Value = oTime.GetTime()
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@OT2Hrs"
                .DbType = DbType.Double
                If rw.Cells("OT2").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    oTime.SetTime(rw.Cells("OT2").Value)
                    .Value = oTime.GetTime()
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@TotalCost"
                .DbType = DbType.Double
                If rw.Cells("totalCost").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("totalCost").Value
                End If
            End With
            cmd.Parameters.Add(oParam)
            If bNewRow Then
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@IDENTITY"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            Else
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .DbType = DbType.Int32
                    .Value = rw.Cells("RecordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                cmd.ExecuteNonQuery()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim sRecordId As String
        sRecordId = DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").FormattedValue
        If sRecordId <> "" Then
            If dbConnection.GetConnection() Then
                sSQL = "DELETE FROM customerProjPhaseJobTimeCardData WHERE RecordId = @RecordId"
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .DbType = DbType.Int32
                    .Value = sRecordId
                End With
                cmd.Parameters.Add(oParam)
                Try
                    cmd.ExecuteNonQuery()
                    cmd.Dispose()
                    dbConnection.Connection.Close()
                    dbConnection.Connection.Dispose()
                    DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
                Catch
                End Try
            End If
        Else
            DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
        End If
        UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
        If DGVTimeCardDetails.Rows.Count = 0 Then
            DGVTimeCardDetails.Rows.Add()
        End If
    End Sub

    Private Sub cboProjJobs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobs.SelectedIndexChanged
        If cboJobs.SelectedIndex >= 0 Then
            LoadCustomerProjectTimeCards(cboJobs.SelectedItem)
        End If
    End Sub

    Private Sub cboPhases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPhases.SelectedIndexChanged
        If cboPhases.SelectedIndex >= 0 Then
            LoadPhaseJobs(cboPhases.SelectedItem)
        End If
    End Sub
End Class