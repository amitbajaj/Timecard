Public Class frmUserMaster
    Dim dbConnection As TimeCardDataAccess

    Private Sub frmUserMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = frmTimeCardMainForm.dbConn
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
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim iRow As Integer
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, UserNumber, UserName, Trade, Basic FROM userMaster"
            dr = cmd.ExecuteReader()
            DGVUserMaster.Rows.Clear()
            While dr.Read()
                iRow = DGVUserMaster.Rows.Add()
                If dr.IsDBNull(0) Then
                    DGVUserMaster.Rows(iRow).Cells("recordId").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("recordId").Value = dr.GetValue(0)
                End If

                If dr.IsDBNull(1) Then
                    DGVUserMaster.Rows(iRow).Cells("userNumber").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("userNumber").Value = dr.GetValue(1)
                End If

                If dr.IsDBNull(2) Then
                    DGVUserMaster.Rows(iRow).Cells("userName").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("userName").Value = dr.GetValue(2)
                End If

                If dr.IsDBNull(3) Then
                    DGVUserMaster.Rows(iRow).Cells("trade").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("trade").Value = dr.GetValue(3)
                End If

                If dr.IsDBNull(4) Then
                    DGVUserMaster.Rows(iRow).Cells("basic").Value = Nothing
                Else
                    DGVUserMaster.Rows(iRow).Cells("basic").Value = dr.GetValue(4)
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVUserMaster.Rows(e.RowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO userMaster (userNumber, userName, trade, basic) VALUES "
            sSQL = sSQL & "(@userNumber, @userName, @trade, @basic)"
        Else
            sSQL = "UPDATE userMaster SET UserNumber = @userNumber, userName = @userName, trade = @trade, basic = @basic WHERE RecordId = @RecordId"
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@userNumber"
                .DbType = DbType.Int32
                If rw.Cells("userNumber").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("userNumber").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@userName"
                .DbType = DbType.String
                If rw.Cells("userName").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("userName").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@trade"
                .DbType = DbType.String
                If rw.Cells("trade").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("trade").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@basic"
                .DbType = DbType.Double
                If rw.Cells("basic").FormattedValue = "" Then
                    .Value = DBNull.Value
                Else
                    .Value = rw.Cells("basic").FormattedValue
                End If
            End With
            cmd.Parameters.Add(oParam)

            If rw.Cells("recordId").FormattedValue = "" Then
                cmd.ExecuteNonQuery()
                cmd.CommandText = "SELECT @@IDENTITY"
                cmd.Parameters.Clear()
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            Else
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .DbType = DbType.Int32
                    .Value = rw.Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                cmd.ExecuteNonQuery()

            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        rw = DGVUserMaster.Rows(iRowIndex)
        If dbConnection.GetConnection() Then
            Try
                cmd = dbConnection.Connection.CreateCommand()
                If rw.Cells("recordId").Value IsNot Nothing Then
                    sSQL = "DELETE FROM userMaster WHERE recordId = @RecordId"
                    cmd.CommandText = sSQL
                    oParam = cmd.CreateParameter()
                    With oParam
                        .ParameterName = "@RecordId"
                        .DbType = DbType.Int32
                        .Value = rw.Cells("recordId").Value
                    End With
                    cmd.Parameters.Add(oParam)
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