
Public Class frmCustomerMaster
    Private dbConnection As TimeCardDataAccess

    Private Sub DGVCategory_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVCustomers.CellEndEdit
        If e.ColumnIndex = DGVCustomers.Columns("CustomerId").Index Or e.ColumnIndex = DGVCustomers.Columns("CustomerName").Index Then
            If DGVCustomers.Rows(e.RowIndex).Cells("RecordId").Value Is Nothing Then
                CreateRecord(DGVCustomers.Rows(e.RowIndex))
            Else
                UpdateRecord(DGVCustomers.Rows(e.RowIndex).Cells("RecordId").Value, DGVCustomers.Rows(e.RowIndex).Cells("CustomerId").Value, DGVCustomers.Rows(e.RowIndex).Cells("CustomerName").Value)
            End If
        End If
    End Sub

    Private Sub DGVCategory_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVCustomers.CellContentClick
        If e.RowIndex > -1 Then
            If e.ColumnIndex = DGVCustomers.Columns("AddRow").Index Then
                DGVCustomers.Rows.Add()
            ElseIf e.ColumnIndex = DGVCustomers.Columns("DeleteRow").Index Then
                If DeleteRecord(DGVCustomers.Rows(e.RowIndex).Cells("RecordId").Value) Then
                    DGVCustomers.Rows.RemoveAt(e.RowIndex)
                    If DGVCustomers.Rows.Count = 0 Then
                        DGVCustomers.Rows.Add()
                    End If
                End If
            End If
        End If
    End Sub

    Private Function DeleteRecord(iRecordId As Integer) As Boolean
        Dim cmd As OleDb.OleDbCommand
        Try
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = "DELETE FROM CustomerMaster WHERE RecordId = " & iRecordId.ToString & ";"
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
                DeleteRecord = True
            Else
                DeleteRecord = False
            End If
        Catch
            DeleteRecord = False
        End Try
    End Function

    Private Sub CreateRecord(rw As DataGridViewRow)
        Dim cmd As OleDb.OleDbCommand
        Dim sCustomerId, sCustomerName As String
        If rw.Cells("CustomerId").Value Is Nothing Then
            sCustomerId = ""
        Else
            sCustomerId = rw.Cells("CustomerId").Value.ToString.Replace("'", "''")
        End If
        If rw.Cells("CustomerName").Value Is Nothing Then
            sCustomerName = ""
        Else
            sCustomerName = rw.Cells("CustomerName").Value.ToString.Replace("'", "''")
        End If
        Try
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = "INSERT INTO CustomerMaster(CustomerId, CustomerName) VALUES('" & sCustomerId & "','" & sCustomerName & "');"
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@Identity"
                rw.Cells("RecordId").Value = cmd.ExecuteScalar().ToString()
                cmd.Dispose()
                dbConnection.Connection.Close()
            End If
        Catch
        End Try
    End Sub

    Private Sub UpdateRecord(iRecordId As Integer, sCustomerId As String, sCustomerName As String)
        Dim cmd As OleDb.OleDbCommand
        Try
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = "UPDATE CustomerMaster SET CustomerId = '" & sCustomerId.ToString.Replace("'", "''") & "', CustomerName='" & sCustomerName.ToString.Replace("'", "''") & "' WHERE RecordId = " & iRecordId.ToString()
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
            End If
        Catch
        End Try
    End Sub

    Private Sub frmCategoryMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        InitializeGrid()
        LoadData()
    End Sub

    Private Sub frmCategoryMaster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVCustomers.Width = Me.Width - 40
        DGVCustomers.Height = Me.Height - 60
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As DataGridViewTextBoxColumn
        Dim col2 As DataGridViewButtonColumn
        With DGVCustomers
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .Columns.Clear()

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "RecordId"
            col1.HeaderText = "Record Id"
            col1.ReadOnly = True
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "CustomerId"
            col1.HeaderText = "Customer Id"
            col1.ReadOnly = False
            col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "CustomerName"
            col1.HeaderText = "Name"
            col1.ReadOnly = False
            'col1.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col1.Width = 150
            col1.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col1)

            col2 = New DataGridViewButtonColumn()
            col2.Name = "AddRow"
            col2.HeaderText = "A"
            col2.Text = "A"
            col2.UseColumnTextForButtonValue = True
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col2)

            col2 = New DataGridViewButtonColumn()
            col2.Name = "DeleteRow"
            col2.HeaderText = "D"
            col2.Text = "D"
            col2.UseColumnTextForButtonValue = True
            col2.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            col2.SortMode = DataGridViewColumnSortMode.NotSortable
            .Columns.Add(col2)
        End With
    End Sub

    Private Sub LoadData()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim rw As DataGridViewRow
        Dim iNewRow As Integer
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, CustomerId, CustomerName FROM CustomerMaster"
            dr = cmd.ExecuteReader()
            DGVCustomers.Rows.Clear()
            While dr.Read()
                iNewRow = DGVCustomers.Rows.Add()
                rw = DGVCustomers.Rows.Item(iNewRow)
                rw.Cells.Item("RecordId").Value = dr.GetInt32(0)
                rw.Cells.Item("CustomerId").Value = dr.GetString(1)
                rw.Cells.Item("CustomerName").Value = dr.GetString(2)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            If DGVCustomers.Rows.Count = 0 Then
                DGVCustomers.Rows.Add()
            End If
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub DGVCustomers_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVCustomers.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVCustomers
                If .CurrentCell.ColumnIndex < .ColumnCount - 2 Then
                    .CurrentCell = .Rows.Item(.CurrentCell.RowIndex).Cells.Item(.CurrentCell.ColumnIndex + 1)
                ElseIf .CurrentCell.ColumnIndex = .ColumnCount - 1 Then
                    If DeleteRecord(.Rows(.CurrentCell.RowIndex).Cells("recordId").Value) Then
                        .Rows.RemoveAt(.CurrentCell.RowIndex)
                    End If
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