Public Class frmProjectJobs
    Private dbConnection As TimeCardDataAccess
    Private Sub frmProjectJobs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = dbConnection.DefaultDatabaseFile
        InitializeGrid()
        LoadCustomers()
    End Sub

    Sub InitializeGrid()
        Dim col1 As DataGridViewTextBoxColumn
        Dim col2 As DataGridViewButtonColumn
        With DGVProjectJobs
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
            col1.Name = "jobId"
            col1.HeaderText = "Job Id"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "jobDesc"
            col1.HeaderText = "Job Description"
            col1.ReadOnly = False
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "jobRate"
            col1.HeaderText = "Job Rate"
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
            cmd.CommandText = "SELECT recordId, CustomerId, CustomerName FROM customerMaster"
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

    Private Sub frmProjectJobs_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVProjectJobs.Width = Width - 40
        DGVProjectJobs.Height = Height - 90
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        DGVProjectJobs.Rows.Clear()
        cboProjects.Items.Clear()
        cboPhases.Items.Clear()
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oParam As OleDb.OleDbParameter
        Dim oProj As TimeCardSupport.ProjectDetails
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM customerProjects WHERE ParentId = ?"
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "ParentId"
                .OleDbType = OleDb.OleDbType.Numeric
                .Value = oCust.recordId
            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing


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
        DGVProjectJobs.Rows.Clear()
        cboPhases.Items.Clear()
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectPhases(cboProjects.SelectedItem)
        End If
    End Sub

    Sub LoadProjectPhases(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oParam As OleDb.OleDbParameter
        Dim oProjPhase As TimeCardSupport.ProjectPhaseDetails
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .OleDbType = OleDb.OleDbType.Integer
                .Value = oProj.recordId
            End With
            cmd.Parameters.Add(oParam)
            cmd.CommandText = "SELECT RecordId, PhaseId, PhaseDesc FROM customerProjectPhases WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            cboPhases.DisplayMember = "DisplayName"
            While dr.Read()
                oProjPhase = New TimeCardSupport.ProjectPhaseDetails
                oProjPhase.RecordId = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    oProjPhase.PhaseId = "-"
                Else
                    oProjPhase.PhaseId = dr.GetString(1)
                End If

                If dr.IsDBNull(2) Then
                    oProjPhase.PhaseDescription = "-"
                Else
                    oProjPhase.PhaseDescription = dr.GetString(2)
                End If
                cboPhases.Items.Add(oProjPhase)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Sub LoadProjectPhaseJobs(oProj As TimeCardSupport.ProjectPhaseDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oParam As OleDb.OleDbParameter
        Dim rw As DataGridViewRow
        DGVProjectJobs.Rows.Clear()
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, JobId, JobDesc, JobRate FROM customerProjPhaseJobs WHERE ParentId = ?"
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "ParentId"
                .OleDbType = OleDb.OleDbType.Numeric
                .Value = oProj.RecordId
            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing

            dr = cmd.ExecuteReader()
            While dr.Read()
                rw = DGVProjectJobs.Rows(DGVProjectJobs.Rows.Add())
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    rw.Cells("jobId").Value = Nothing
                Else
                    rw.Cells("jobId").Value = dr.GetString(1)
                End If
                If dr.IsDBNull(2) Then
                    rw.Cells("jobDesc").Value = Nothing
                Else
                    rw.Cells("jobDesc").Value = dr.GetString(2)
                End If
                If dr.IsDBNull(3) Then
                    rw.Cells("jobRate").Value = Nothing
                Else
                    rw.Cells("jobRate").Value = dr.GetDouble(3)
                End If
            End While
            If DGVProjectJobs.Rows.Count = 0 Then
                DGVProjectJobs.Rows.Add()
            End If
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub DGVProjectJobs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProjectJobs.CellContentClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DGVProjectJobs.Columns("addNewRow").Index Then
                DGVProjectJobs.Rows.Add()
            ElseIf e.ColumnIndex = DGVProjectJobs.Columns("delCurRow").Index Then
                RemoveRow(e.RowIndex)
            End If
        End If
    End Sub

    Sub RemoveRow(iRowIndex As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim rw As DataGridViewRow
        Dim oParam As OleDb.OleDbParameter
        rw = DGVProjectJobs.Rows(iRowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            DGVProjectJobs.Rows.RemoveAt(iRowIndex)
        Else
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = "DELETE FROM customerProjPhaseJobs WHERE RecordId = @RecordId"
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .OleDbType = OleDb.OleDbType.VarChar
                    .Value = rw.Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing


                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
                DGVProjectJobs.Rows.RemoveAt(iRowIndex)
            End If
        End If
    End Sub

    Private Sub DGVProjectJobs_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProjectJobs.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim oParam As OleDb.OleDbParameter
        Dim sSQL As String
        Dim rw As DataGridViewRow
        Dim bNewRec As Boolean
        rw = DGVProjectJobs.Rows(e.RowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            bNewRec = True
        Else
            bNewRec = False
        End If
        If bNewRec Then
            sSQL = "INSERT INTO customerProjPhaseJobs(ParentId, JobId, JobDesc, JobRate) VALUES(@ParentId,@JobId,@JobDesc,@JobRate)"
        Else
            sSQL = "UPDATE customerProjPhaseJobs SET ParentId = @ParentId, JobId = @JobId, JobDesc = @JobDesc, JobRate = @JobRate WHERE RecordId = @RecId"
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .OleDbType = OleDb.OleDbType.Numeric
                .Value = cboPhases.SelectedItem.recordId
            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@JobId"
                .OleDbType = OleDb.OleDbType.VarChar
                If rw.Cells("jobId").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("jobId").FormattedValue
                End If

            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@JobDesc"
                .OleDbType = OleDb.OleDbType.VarChar
                If rw.Cells("jobDesc").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("jobDesc").FormattedValue
                End If

            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@JobRate"
                .OleDbType = OleDb.OleDbType.Double
                If rw.Cells("jobRate").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("jobRate").FormattedValue
                End If

            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing
            If bNewRec Then
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@Identity"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            Else
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .OleDbType = OleDb.OleDbType.Double
                    .Value = rw.Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
                cmd.ExecuteNonQuery()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub DGVProjectJobs_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVProjectJobs.CellValidating
        Dim bRetVal As Boolean = False
        Dim dRate As Double
        Dim currentRow As DataGridViewRow = DGVProjectJobs.Rows(e.RowIndex)
        If e.FormattedValue IsNot Nothing Then
            If e.FormattedValue <> "" Then
                If e.RowIndex >= 0 And e.ColumnIndex = DGVProjectJobs.Columns("JobRate").Index Then
                    If Not Double.TryParse(e.FormattedValue, dRate) Then
                        bRetVal = True
                        MsgBox("Enter a valid rate value!")
                    End If
                End If
            End If
        End If
        e.Cancel = bRetVal
    End Sub

    Private Sub DGVProjectJobs_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVProjectJobs.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVProjectJobs
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

    Private Sub cboPhases_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPhases.SelectedIndexChanged
        DGVProjectJobs.Rows.Clear()
        If cboPhases.SelectedIndex >= 0 Then
            LoadProjectPhaseJobs(cboPhases.SelectedItem)
        End If
    End Sub
End Class