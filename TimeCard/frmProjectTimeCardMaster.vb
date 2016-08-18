Public Class frmProjectTimeCardMaster
    Private dbConnection As TimeCardDataAccess
    Private Sub frmProjectTimeCardMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        InitializeGrid()
        LoadCustomers()
    End Sub

    Sub InitializeGrid()
        Dim col1 As DataGridViewTextBoxColumn
        Dim col2 As DataGridViewButtonColumn
        With DGVTimeCardMaster
            .Columns.Clear()
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "Record Id"
            col1.ReadOnly = True
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardNumber"
            col1.HeaderText = "TimeCard Number"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardMonth"
            col1.HeaderText = "TimeCard Month"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardYear"
            col1.HeaderText = "TimeCard Year"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col2 = New DataGridViewButtonColumn()
            col2.Name = "addNewRow"
            col2.Text = "A"
            col2.HeaderText = "A"
            col2.UseColumnTextForButtonValue = True
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            .Columns.Add(col2)

            col2 = New DataGridViewButtonColumn()
            col2.Name = "delCurRow"
            col2.Text = "D"
            col2.HeaderText = "D"
            col2.UseColumnTextForButtonValue = True
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            .Columns.Add(col2)

        End With
    End Sub

    Sub LoadCustomers()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oCust As TimeCardSupport.CustomerDetails
        cboCustomers.Items.Clear()
        cboCustomers.DisplayMember = "displayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT recordId, CustomerId, CustomerName FROM CustomerMaster"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oCust = New TimeCardSupport.CustomerDetails
                oCust.recordId = dr.GetInt32(0)
                oCust.customerId = dr.GetString(1)
                oCust.customerName = dr.GetString(2)
                cboCustomers.Items.Add(oCust)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub frmProjectTimeCardMaster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVTimeCardMaster.Width = Width - 40
        DGVTimeCardMaster.Height = Height - 90
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        DGVTimeCardMaster.Rows.Clear()
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oPr As OleDb.OleDbParameter
        Dim oProj As TimeCardSupport.ProjectDetails
        cboJobs.Items.Clear()
        DGVTimeCardMaster.Rows.Clear()
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oPr = cmd.CreateParameter()
            oPr.ParameterName = "@ParentId"
            oPr.OleDbType = OleDb.OleDbType.Integer
            oPr.Value = oCust.recordId
            cmd.Parameters.Add(oPr)
            cmd.CommandText = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM CustomerProjects WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProj = New TimeCardSupport.ProjectDetails
                oProj.recordId = dr.GetInt32(0)
                oProj.projectId = dr.GetString(1)
                oProj.projectDescription = dr.GetString(2)
                oProj.projectRate = dr.GetDouble(3)
                cboProjects.Items.Add(oProj)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub cboProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjects.SelectedIndexChanged
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectPhases(cboProjects.SelectedItem)
        End If
    End Sub

    Sub LoadProjectPhases(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oPr As OleDb.OleDbParameter
        Dim oProjPhase As TimeCardSupport.ProjectPhaseDetails
        DGVTimeCardMaster.Rows.Clear()
        cboJobs.Items.Clear()
        cboPhases.Items.Clear()
        cboPhases.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oPr = cmd.CreateParameter()
            oPr.ParameterName = "@ParentId"
            oPr.OleDbType = OleDb.OleDbType.Integer
            oPr.Value = oProj.recordId
            cmd.Parameters.Add(oPr)
            cmd.CommandText = "SELECT RecordId, PhaseId, PhaseDesc FROM ProjectPhases WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProjPhase = New TimeCardSupport.ProjectPhaseDetails
                oProjPhase.RecordId = dr.GetInt32(0)
                oProjPhase.PhaseId = dr.GetString(1)
                oProjPhase.PhaseDescription = dr.GetString(2)
                cboPhases.Items.Add(oProjPhase)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Sub LoadPhaseJobs(oPhase As TimeCardSupport.ProjectPhaseDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oPr As OleDb.OleDbParameter
        Dim oProjJob As TimeCardSupport.ProjectJobDetails
        DGVTimeCardMaster.Rows.Clear()
        cboJobs.Items.Clear()
        cboJobs.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oPr = cmd.CreateParameter()
            oPr.ParameterName = "@ParentId"
            oPr.OleDbType = OleDb.OleDbType.Integer
            oPr.Value = oPhase.RecordId
            cmd.Parameters.Add(oPr)
            cmd.CommandText = "SELECT RecordId, JobId, JobDesc, JobRate FROM ProjectPhaseJobs WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oProjJob = New TimeCardSupport.ProjectJobDetails
                oProjJob.recordId = dr.GetInt32(0)
                oProjJob.JobId = dr.GetString(1)
                oProjJob.JobDescription = dr.GetString(2)
                oProjJob.JobRate = dr.GetDouble(3)
                cboJobs.Items.Add(oProjJob)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub


    Sub LoadProjectTimeCards(oProjJob As TimeCardSupport.ProjectJobDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oPr As OleDb.OleDbParameter
        Dim rw As DataGridViewRow
        DGVTimeCardMaster.Rows.Clear()
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oPr = cmd.CreateParameter()
            oPr.ParameterName = "@ParentId"
            oPr.OleDbType = OleDb.OleDbType.Integer
            oPr.Value = oProjJob.recordId
            cmd.Parameters.Add(oPr)
            cmd.CommandText = "SELECT RecordId, TimeCardNumber, TimeCardMonth, TimeCardYear FROM ProjectTimeCardMaster WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                rw = DGVTimeCardMaster.Rows(DGVTimeCardMaster.Rows.Add())
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    rw.Cells("timeCardNumber").Value = Nothing
                Else
                    rw.Cells("timeCardNumber").Value = dr.GetInt32(1)
                End If
                If dr.IsDBNull(2) Then
                    rw.Cells("timeCardMonth").Value = Nothing
                Else
                    rw.Cells("timeCardMonth").Value = dr.GetInt16(2)
                End If
                If dr.IsDBNull(3) Then
                    rw.Cells("timeCardYear").Value = Nothing
                Else
                    rw.Cells("timeCardYear").Value = dr.GetInt16(3)
                End If
            End While
            If DGVTimeCardMaster.Rows.Count = 0 Then
                DGVTimeCardMaster.Rows.Add()
            End If
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub DGVTimeCardMaster_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardMaster.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DGVTimeCardMaster.Columns("addNewRow").Index Then
                DGVTimeCardMaster.Rows.Add()
            ElseIf e.ColumnIndex = DGVTimeCardMaster.Columns("delCurRow").Index Then
                RemoveRow(e.RowIndex)
            End If
        End If
    End Sub

    Sub RemoveRow(iRowIndex As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim oPr As OleDb.OleDbParameter
        Dim rw As DataGridViewRow
        rw = DGVTimeCardMaster.Rows(iRowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            DGVTimeCardMaster.Rows.RemoveAt(iRowIndex)
        Else
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                oPr = cmd.CreateParameter()
                oPr.ParameterName = "@RecordId"
                oPr.OleDbType = OleDb.OleDbType.Integer
                oPr.Value = rw.Cells("recordId").FormattedValue
                cmd.CommandText = "DELETE FROM ProjectTimeCardMaster WHERE RecordId = @RecordId"
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
                DGVTimeCardMaster.Rows.RemoveAt(iRowIndex)
            End If
        End If
    End Sub

    Private Sub DGVTimeCardMaster_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardMaster.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim oParam As OleDb.OleDbParameter
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVTimeCardMaster.Rows(e.RowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO ProjectTimeCardMaster(ParentId, TimeCardNumber, TimeCardMonth, TimeCardYear) VALUES(@ParentId, @TimeCardNumber, @TimeCardMonth, @TimeCardYear);"
        Else
            sSQL = "UPDATE ProjectTimeCardMaster SET ParentId = @ParentId, TimeCardNumber = @TimeCardNumber, TimeCardMonth = @TimeCardMonth, TimeCardYear = @TimeCardYear WHERE RecordId = @RecordId;"
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .OleDbType = OleDb.OleDbType.Integer
                .Value = cboJobs.SelectedItem.RecordId
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@TimeCardNumber"
                .OleDbType = OleDb.OleDbType.Integer
                If rw.Cells("timeCardNumber").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("timeCardNumber").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@TimeCardMonth"
                .OleDbType = OleDb.OleDbType.Integer
                If rw.Cells("timeCardMonth").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("timeCardMonth").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@TimeCardYear"
                .OleDbType = OleDb.OleDbType.Integer
                If rw.Cells("timeCardYear").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("timeCardYear").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            cmd.CommandText = sSQL
            If rw.Cells("recordId").FormattedValue = "" Then
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@Identity"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            Else
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .OleDbType = OleDb.OleDbType.Integer
                    .Value = rw.Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                cmd.ExecuteNonQuery()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub DGVTimeCardMaster_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVTimeCardMaster.CellValidating
        Dim bRetVal As Boolean = False
        Dim iMonth, iYear As Integer
        Dim rw As DataGridViewRow
        Dim currentRow As DataGridViewRow = DGVTimeCardMaster.Rows(e.RowIndex)
        If e.FormattedValue IsNot Nothing Then
            If e.FormattedValue <> "" Then
                If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardMaster.Columns("TimeCardMonth").Index Then
                    If Integer.TryParse(e.FormattedValue, iMonth) Then
                        If iMonth > 12 Or iMonth < 0 Then
                            bRetVal = True
                            MsgBox("Enter a valid month!")
                        Else
                            For Each rw In DGVTimeCardMaster.Rows
                                If rw.Cells("TimeCardMonth").Value = iMonth And rw.Index <> e.RowIndex And rw.Cells("TimeCardYear").FormattedValue = currentRow.Cells("TimeCardYear").FormattedValue Then
                                    MsgBox("Duplicate month and year combination!")
                                    bRetVal = True
                                End If
                            Next
                        End If
                    Else
                        bRetVal = True
                        MsgBox("Enter a valid month!")
                    End If
                ElseIf e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardMaster.Columns("timeCardYear").Index Then
                    If Integer.TryParse(e.FormattedValue, iYear) Then
                        For Each rw In DGVTimeCardMaster.Rows
                            If rw.Cells("TimeCardYear").Value = iYear And rw.Index <> e.RowIndex And rw.Cells("TimeCardMonth").FormattedValue = currentRow.Cells("TimeCardMonth").FormattedValue Then
                                MsgBox("Duplicate month and year combination!")
                                bRetVal = True
                            End If
                        Next
                    Else
                        bRetVal = True
                        MsgBox("Enter a valid year!")
                    End If
                End If
            End If
        End If
        e.Cancel = bRetVal
    End Sub

    Private Sub DGVTimeCardMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVTimeCardMaster.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVTimeCardMaster
                If .CurrentCell.ColumnIndex < .ColumnCount - 2 Then
                    .CurrentCell = .Rows.Item(.CurrentCell.RowIndex).Cells.Item(.CurrentCell.ColumnIndex + 1)
                ElseIf .CurrentCell.ColumnIndex = .ColumnCount - 1 Then
                    RemoveRow(.CurrentCell.RowIndex)
                    .CurrentCell = .CurrentRow.Cells(1)
                ElseIf .CurrentCell.RowIndex = .Rows.Count - 1 And .CurrentCell.ColumnIndex = .ColumnCount - 2 Then
                    .Rows.Add()
                    .CurrentCell = .Rows.Item(.Rows.Count - 1).Cells.Item(1)
                ElseIf .CurrentCell.RowIndex < .Rows.Count - 1 Then
                    .CurrentCell = .Rows.Item(.CurrentCell.RowIndex + 1).Cells.Item(1)
                End If
                e.Handled = True
            End With
        End If
    End Sub

    Private Sub cboProjJobs_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboJobs.SelectedIndexChanged
        If cboJobs.SelectedIndex >= 0 Then
            LoadProjectTimeCards(cboJobs.SelectedItem)
        End If
    End Sub

    Private Sub cboPhases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPhases.SelectedIndexChanged
        If cboPhases.SelectedIndex >= 0 Then
            LoadPhaseJobs(cboPhases.SelectedItem)
        End If
    End Sub
End Class