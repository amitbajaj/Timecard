
Public Class frmUserMaster
    Dim dbConnection As TimeCardDataAccess

    Private Sub frmUserMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        InitializeGrid()
        LoadData()
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        With DGVUserMaster
            .Columns.Clear()
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToOrderColumns = False
            .AllowUserToResizeRows = False
        End With

        With DGVUserMaster.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "Record Id"
            col1.ReadOnly = True
            col1.Width = 60
            col1.Visible = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "userNumber"
            col1.HeaderText = "User Number"
            col1.ReadOnly = False
            col1.Width = 100
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "userName"
            col1.HeaderText = "User Name"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "trade"
            col1.HeaderText = "Trade"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "basic"
            col1.HeaderText = "Basic Rate"
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

    Private Sub LoadData()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim iRow As Integer
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, UserNumber, UserName, Trade, Basic FROM UserMaster"
            dr = cmd.ExecuteReader()
            DGVUserMaster.Rows.Clear()
            While dr.Read()
                iRow = DGVUserMaster.Rows.Add()
                If dr.IsDBNull(0) Then
                    DGVUserMaster.Rows(iRow).Cells("recordId").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("recordId").Value = dr.GetInt32(0)
                End If

                If dr.IsDBNull(1) Then
                    DGVUserMaster.Rows(iRow).Cells("userNumber").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("userNumber").Value = dr.GetInt16(1)
                End If

                If dr.IsDBNull(2) Then
                    DGVUserMaster.Rows(iRow).Cells("userName").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("userName").Value = dr.GetString(2)
                End If

                If dr.IsDBNull(3) Then
                    DGVUserMaster.Rows(iRow).Cells("trade").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("trade").Value = dr.GetString(3)
                End If

                If dr.IsDBNull(4) Then
                    DGVUserMaster.Rows(iRow).Cells("basic").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("basic").Value = dr.GetDouble(4)
                End If
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error acquiring database connection!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub DGVUserMaster_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUserMaster.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVUserMaster.Rows(e.RowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO UserMaster (userNumber, userName, trade, basic) VALUES ("
            If rw.Cells("userNumber").Value IsNot Nothing Then
                sSQL = sSQL & rw.Cells("userNumber").Value
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ","
            If rw.Cells("userName").Value IsNot Nothing Then
                sSQL = sSQL & "'" & rw.Cells("userName").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ","

            If rw.Cells("trade").Value IsNot Nothing Then
                sSQL = sSQL & "'" & rw.Cells("trade").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ","
            If rw.Cells("basic").Value IsNot Nothing Then
                sSQL = sSQL & rw.Cells("basic").Value
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ")"
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
            sSQL = "UPDATE UserMaster SET "
            If rw.Cells("userNumber").Value IsNot Nothing Then
                sSQL = sSQL & " userNumber = " & rw.Cells("userNumber").Value
            Else
                sSQL = sSQL & " userNumber = NULL"
            End If

            If rw.Cells("userName").Value IsNot Nothing Then
                sSQL = sSQL & ", userName = '" & rw.Cells("userName").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & ", userName = NULL"
            End If

            If rw.Cells("trade").Value IsNot Nothing Then
                sSQL = sSQL & ", trade = '" & rw.Cells("trade").Value.ToString().Replace("'", "''") & "'"
            Else
                sSQL = sSQL & ", trade = NULL"
            End If

            If rw.Cells("basic").Value IsNot Nothing Then
                sSQL = sSQL & ", basic = " & rw.Cells("basic").Value
            Else
                sSQL = sSQL & ", basic = NULL"
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

    Private Sub DGVUserMaster_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUserMaster.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = DGVUserMaster.Columns("addNextRow").Index Then
            DGVUserMaster.Rows.Add()
        End If

        If e.RowIndex >= 0 And e.ColumnIndex = DGVUserMaster.Columns("delCurRow").Index Then
            RemoveRow(e.RowIndex)
        End If
    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim rw As DataGridViewRow
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        rw = DGVUserMaster.Rows(iRowIndex)
        If dbConnection.GetConnection() Then
            Try
                cmd = dbConnection.Connection.CreateCommand()
                If rw.Cells("recordId").Value IsNot Nothing Then
                    sSQL = "DELETE FROM UserMaster WHERE recordId = " & rw.Cells("recordId").Value
                    cmd.CommandText = sSQL
                    cmd.ExecuteNonQuery()
                End If
                DGVUserMaster.Rows.Remove(rw)
                cmd.Dispose()
                dbConnection.Connection.Close()
            Catch ex As Exception
                MsgBox("Error deleting record!" & vbCrLf & ex.Message)
            End Try
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If

    End Sub

    Private Sub DGVUserMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVUserMaster.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVUserMaster
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

    Private Sub frmUserMaster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVUserMaster.Width = Width - 40
        DGVUserMaster.Height = Height - 60
    End Sub
End Class