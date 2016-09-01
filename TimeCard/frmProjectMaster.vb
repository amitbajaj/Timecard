Public Class frmProjectMaster
    Private dbConnection As TimeCardDataAccess
    Private iDecimals As Integer = TimeCardSupport.NumberOfDecimals
    Private sNumberFormat As String = TimeCardSupport.NumberFormat

    Private Sub frmProjectMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = frmTimeCardMainForm.dbConn
        InitializeGrid()
        LoadCustomers()
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        DGVProjectMaster.Columns.Clear()
        DGVProjectMaster.AllowUserToAddRows = False
        DGVProjectMaster.AllowUserToDeleteRows = False
        DGVProjectMaster.AllowUserToResizeColumns = True
        DGVProjectMaster.AllowUserToResizeRows = False
        DGVProjectMaster.AllowUserToOrderColumns = False
        With DGVProjectMaster.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "Record Id"
            col1.ReadOnly = True
            col1.Width = 80
            col1.Visible = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "ProjectNumber"
            col1.HeaderText = "Project Number"
            col1.ReadOnly = False
            col1.Width = 120
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "ProjectDesc"
            col1.HeaderText = "Description"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "ProjectRate"
            col1.HeaderText = "Rate"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewButtonColumn()
            col1.name = "addNextRow"
            col1.HeaderText = "A"
            col1.Text = "A"
            col1.UseColumnTextForButtonValue = True
            col1.ReadOnly = True
            col1.Width = 25
            .Add(col1)

            col1 = New DataGridViewButtonColumn()
            col1.Name = "delCurRow"
            col1.HeaderText = "D"
            col1.Text = "D"
            col1.UseColumnTextForButtonValue = True
            col1.ReadOnly = True
            col1.Width = 25
            .Add(col1)


        End With
    End Sub


    Private Sub LoadCustomers()
        Dim sSQL As String
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim iNewItem As Integer
        Dim customerMaster As TimeCardSupport.CustomerDetails
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            sSQL = "SELECT RecordId, CustomerId, CustomerName FROM customerMaster"
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            DGVProjectMaster.Rows.Clear()
            cboCustomers.Items.Clear()
            cboCustomers.DisplayMember = "displayName"
            While dr.Read()
                customerMaster = New TimeCardSupport.CustomerDetails
                customerMaster.recordId = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    customerMaster.customerId = Nothing
                Else
                    customerMaster.customerId = dr.GetString(1)
                End If

                If dr.IsDBNull(2) Then
                    customerMaster.customerName = "--"
                Else
                    customerMaster.customerName = dr.GetString(2)
                End If

                iNewItem = cboCustomers.Items.Add(customerMaster)

            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        LoadData(cboCustomers.SelectedItem.recordId)
    End Sub

    Private Sub LoadData(iRecordId As Integer)
        Dim rw As DataGridViewRow
        Dim iNewRow As Integer
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim sSQL As String
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM customerProjects WHERE ParentId = @RecId"
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@RecId"
                .DbType = DbType.Int32
                .Value = iRecordId
            End With
            cmd.Parameters.Add(oParam)
            dr = cmd.ExecuteReader()
            DGVProjectMaster.Rows.Clear()
            While dr.Read()
                iNewRow = DGVProjectMaster.Rows.Add()
                rw = DGVProjectMaster.Rows(iNewRow)
                rw.Cells("recordId").Value = dr.GetValue(0)
                If dr.IsDBNull(1) Then
                    rw.Cells("ProjectNumber").Value = Nothing
                Else
                    rw.Cells("ProjectNumber").Value = dr.GetValue(1)
                End If
                If dr.IsDBNull(2) Then
                    rw.Cells("ProjectDesc").Value = Nothing
                Else
                    rw.Cells("ProjectDesc").Value = dr.GetValue(2)
                End If
                If dr.IsDBNull(3) Then
                    rw.Cells("ProjectRate").Value = Nothing
                Else
                    rw.Cells("ProjectRate").Value = Math.Round(dr.GetDecimal(3), iDecimals).ToString(sNumberFormat)
                End If
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            If DGVProjectMaster.Rows.Count = 0 Then
                DGVProjectMaster.Rows.Add()
            End If
        End If
    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim rw As DataGridViewRow
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim cmd As IDbCommand
        Try
            rw = DGVProjectMaster.Rows(iRowIndex)
            If rw.Cells("recordId").FormattedValue <> "" Then
                If dbConnection.GetConnection() Then
                    cmd = dbConnection.Connection.CreateCommand()
                    sSQL = "DELETE FROM customerProjects WHERE recordId = @RecordId"
                    cmd.CommandText = sSQL
                    oParam = cmd.CreateParameter()
                    With oParam
                        .ParameterName = "@RecordId"
                        .DbType = DbType.Int32
                        .Value = rw.Cells("recordId").Value
                    End With
                    cmd.Parameters.Add(oParam)
                    cmd.ExecuteNonQuery()
                    DGVProjectMaster.Rows.Remove(rw)
                    cmd.Dispose()
                    dbConnection.Connection.Close()
                Else
                    MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
                End If
            Else
                DGVProjectMaster.Rows.Remove(rw)
            End If

        Catch ex As Exception
            MsgBox("Error deleting records!" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub DGVProjectMaster_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProjectMaster.CellEndEdit
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVProjectMaster.Rows(e.RowIndex)
        If rw.Cells("RecordId").FormattedValue = "" Then
            sSQL = "INSERT INTO customerProjects(ParentId, ProjectId, ProjectDesc, ProjectRate) VALUES"
            sSQL = sSQL & "(@ParentId, @ProjectId, @ProjectDesc, @ProjectRate);"
        Else
            sSQL = "UPDATE customerProjects SET ParentId = @ParentId, ProjectId = @ProjectId, ProjectDesc = @ProjectDesc, ProjectRate = @ProjectRate WHERE RecordId = @RecordId"
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = cboCustomers.SelectedItem.RecordId
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ProjectId"
                .DbType = DbType.String
                If rw.Cells("ProjectNumber").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("ProjectNumber").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ProjectDesc"
                .DbType = DbType.String
                If rw.Cells("ProjectDesc").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("ProjectDesc").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ProjectRate"
                .DbType = DbType.Decimal
                If rw.Cells("ProjectRate").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("ProjectRate").Value
                End If
            End With
            cmd.Parameters.Add(oParam)

            If rw.Cells("RecordId").FormattedValue = "" Then
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
        End If
    End Sub

    Private Sub DGVProjectMaster_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVProjectMaster.CellValidating
        Dim dValue As Double
        If e.ColumnIndex = DGVProjectMaster.Columns("ProjectRate").Index Then
            If e.FormattedValue <> "" Then
                If Not Double.TryParse(e.FormattedValue, dValue) Then
                    MsgBox("Enter a valid rate!")
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub frmProjectMaster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVProjectMaster.Width = Width - 50
        DGVProjectMaster.Height = Height - 100
    End Sub

    Private Sub DGVProjectMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVProjectMaster.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVProjectMaster
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

    Private Sub DGVProjectMaster_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProjectMaster.CellContentClick
        Dim iNewRow As Integer
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = DGVProjectMaster.Columns("addNextRow").Index Then
                iNewRow = DGVProjectMaster.Rows.Add()
                DGVProjectMaster.CurrentCell = DGVProjectMaster.Rows(iNewRow).Cells(1)
            ElseIf e.ColumnIndex = DGVProjectMaster.Columns("delCurRow").Index Then
                RemoveRow(e.RowIndex)
                DGVProjectMaster.CurrentCell = DGVProjectMaster.CurrentRow.Cells(1)
            End If
        End If
    End Sub
End Class