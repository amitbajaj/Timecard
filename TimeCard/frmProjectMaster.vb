Public Class frmProjectMaster
    Private dbConnection As TimeCardDataAccess
    Private iDecimals As Integer = My.Settings.Item("NumberOfDecimals")
    Private sNumberFormat As String = My.Settings.Item("NumberFormat")

    Private Sub frmProjectMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
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
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim iNewItem As Integer
        Dim customerMaster As TimeCardSupport.CustomerDetails
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            sSQL = "SELECT RecordId, CustomerId, CustomerName FROM CustomerMaster"
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
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        If dbConnection.GetConnection() Then
            sSQL = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM CustomerProjects WHERE CustomerId = " & iRecordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            DGVProjectMaster.Rows.Clear()
            While dr.Read()
                iNewRow = DGVProjectMaster.Rows.Add()
                rw = DGVProjectMaster.Rows(iNewRow)
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    rw.Cells("ProjectNumber").Value = Nothing
                Else
                    rw.Cells("ProjectNumber").Value = dr.GetString(1)
                End If
                If dr.IsDBNull(2) Then
                    rw.Cells("ProjectDesc").Value = Nothing
                Else
                    rw.Cells("ProjectDesc").Value = dr.GetString(2)
                End If
                If dr.IsDBNull(3) Then
                    rw.Cells("ProjectRate").Value = Nothing
                Else
                    rw.Cells("ProjectRate").Value = Math.Round(dr.GetDouble(3), iDecimals).ToString(sNumberFormat)
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
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        Try
            rw = DGVProjectMaster.Rows(iRowIndex)
            If rw.Cells("recordId").FormattedValue <> "" Then
                If dbConnection.GetConnection() Then
                    cmd = dbConnection.Connection.CreateCommand()
                    sSQL = "DELETE FROM CustomerProjects WHERE recordId = " & rw.Cells("recordId").Value
                    cmd.CommandText = sSQL
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
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVProjectMaster.Rows(e.RowIndex)
        If rw.Cells("RecordId").FormattedValue = "" Then
            sSQL = "INSERT INTO CustomerProjects(customerId, ProjectId, ProjectDesc, ProjectRate) VALUES("
            sSQL = sSQL & cboCustomers.SelectedItem.RecordId
            If rw.Cells("ProjectNumber").Value IsNot Nothing Then
                sSQL = sSQL & ", '" & rw.Cells("ProjectNumber").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & ",NULL"
            End If

            If rw.Cells("ProjectDesc").Value IsNot Nothing Then
                sSQL = sSQL & ", '" & rw.Cells("ProjectDesc").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & ", NULL"
            End If

            If rw.Cells("ProjectRate").Value IsNot Nothing Then
                sSQL = sSQL & ", " & rw.Cells("ProjectRate").Value
            Else
                sSQL = sSQL & ", NULL"
            End If
            sSQL = sSQL & ");"
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@IDENTITY"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
                cmd.Dispose()
                dbConnection.Connection.Close()
            End If

        Else
            sSQL = "UPDATE CustomerProjects SET "
            If rw.Cells("ProjectNumber").Value IsNot Nothing Then
                sSQL = sSQL & " ProjectId = '" & rw.Cells("ProjectNumber").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & " ProjectId = NULL"
            End If

            If rw.Cells("ProjectDesc").Value IsNot Nothing Then
                sSQL = sSQL & ", ProjectDesc = '" & rw.Cells("ProjectDesc").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & ", ProjectDesc = NULL"
            End If

            If rw.Cells("ProjectRate").Value IsNot Nothing Then
                sSQL = sSQL & ", ProjectRate = " & rw.Cells("ProjectRate").Value
            Else
                sSQL = sSQL & ", ProjectRate = NULL"
            End If

            sSQL = sSQL & " WHERE recordId = " & rw.Cells("recordId").Value
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
            End If

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
End Class