Public Class frmTimeCardMaster
    Private dbConnection As TimeCardDataAccess
    Private Sub frmTimeCardMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        InitializeGrid()
        LoadUsers()
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        With DGVTimeCardMaster
            .Columns.Clear()
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AllowUserToResizeRows = False
            .AllowUserToOrderColumns = False
            .MultiSelect = False
        End With

        With DGVTimeCardMaster.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "Record Id"
            col1.ReadOnly = True
            col1.Width = 80
            col1.Visible = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardNumber"
            col1.HeaderText = "Time Card Number"
            col1.ReadOnly = False
            col1.Width = 120
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardMonth"
            col1.HeaderText = "Month"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "timeCardYear"
            col1.HeaderText = "Year"
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


    Private Sub LoadUsers()
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim iNewItem As Integer
        Dim userMaster As TimeCardSupport.UserDetails
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            sSQL = "SELECT RecordId, UserNumber, UserName FROM UserMaster"
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            DGVTimeCardMaster.Rows.Clear()
            cboUsers.Items.Clear()
            cboUsers.DisplayMember = "displayName"
            While dr.Read()
                userMaster = New TimeCardSupport.UserDetails
                userMaster.recordId = dr.GetInt32(0)
                If dr.IsDBNull(1) Then
                    userMaster.userId = -1
                Else
                    userMaster.userId = dr.GetInt16(1)
                End If

                If dr.IsDBNull(2) Then
                    userMaster.userName = "--"
                Else
                    userMaster.userName = dr.GetString(2)
                End If

                iNewItem = cboUsers.Items.Add(userMaster)

            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub cboUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsers.SelectedIndexChanged
        LoadData(cboUsers.SelectedItem.recordId)
    End Sub

    Private Sub LoadData(iRecordId As Integer)
        Dim rw As DataGridViewRow
        Dim iNewRow As Integer
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        If dbConnection.GetConnection() Then
            sSQL = "SELECT * FROM TimeCardMaster WHERE UserId = " & iRecordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            DGVTimeCardMaster.Rows.Clear()
            While dr.Read()
                iNewRow = DGVTimeCardMaster.Rows.Add()
                rw = DGVTimeCardMaster.Rows(iNewRow)
                rw.Cells("recordId").Value = dr.GetInt32(0)
                If dr.IsDBNull(2) Then
                    rw.Cells("timeCardNumber").Value = Nothing
                Else
                    rw.Cells("timeCardNumber").Value = dr.GetInt32(2)
                End If
                If dr.IsDBNull(3) Then
                    rw.Cells("timeCardMonth").Value = Nothing
                Else
                    rw.Cells("timeCardMonth").Value = dr.GetInt16(3)
                End If
                If dr.IsDBNull(4) Then
                    rw.Cells("timeCardYear").Value = Nothing
                Else
                    rw.Cells("timeCardYear").Value = dr.GetInt16(4)
                End If
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            If DGVTimeCardMaster.Rows.Count = 0 Then
                DGVTimeCardMaster.Rows.Add()
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs)
        Dim iNewItem As Integer
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        If cboUsers.SelectedIndex >= 0 Then
            iNewItem = DGVTimeCardMaster.Rows.Add()
            If dbConnection.GetConnection() Then
                sSQL = "INSERT INTO TimeCardMaster(UserId) VALUES (" & cboUsers.SelectedItem.recordId & ");"
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
                LoadData(cboUsers.SelectedItem.recordId)
            End If
        End If
    End Sub

    Private Sub DGVTimeCardMaster_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardMaster.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVTimeCardMaster.Rows(e.RowIndex)
        If rw.Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO TimeCardMaster (UserId, timeCardNumber, timeCardMonth, timeCardYear) VALUES("
            sSQL = sSQL & cboUsers.SelectedItem.UserId
            If rw.Cells("timeCardNumber").Value IsNot Nothing Then
                sSQL = sSQL & "," & rw.Cells("timeCardNumber").Value
            Else
                sSQL = sSQL & " NULL"
            End If
            If rw.Cells("timeCardMonth").Value IsNot Nothing Then
                sSQL = sSQL & "," & rw.Cells("timeCardMonth").Value
            Else
                sSQL = sSQL & ",NULL"
            End If
            If rw.Cells("timeCardYear").Value IsNot Nothing Then
                sSQL = sSQL & "," & rw.Cells("timeCardYear").Value
            Else
                sSQL = sSQL & ",NULL"
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
            sSQL = "UPDATE TimeCardMaster SET "
            If rw.Cells("timeCardNumber").Value IsNot Nothing Then
                sSQL = sSQL & " timeCardNumber = " & rw.Cells("timeCardNumber").Value
            Else
                sSQL = sSQL & " timeCardNumber = NULL"
            End If
            If rw.Cells("timeCardMonth").Value IsNot Nothing Then
                sSQL = sSQL & ", timeCardMonth = " & rw.Cells("timeCardMonth").Value
            Else
                sSQL = sSQL & ", timeCardMonth = NULL"
            End If
            If rw.Cells("timeCardYear").Value IsNot Nothing Then
                sSQL = sSQL & ", timeCardYear = " & rw.Cells("timeCardYear").Value
            Else
                sSQL = sSQL & ", timeCardYear = NULL"
            End If

            sSQL = sSQL & " WHERE timeCardId = " & rw.Cells("recordId").Value
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim rw As DataGridViewRow
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        If DGVTimeCardMaster.SelectedRows.Count > 0 Then
            If dbConnection.GetConnection() Then
                Try
                    cmd = dbConnection.Connection.CreateCommand()
                    For Each rw In DGVTimeCardMaster.SelectedRows
                        If rw.Cells("recordId").Value IsNot Nothing Then
                            sSQL = "DELETE FROM TimeCardMaster WHERE TimeCardId = " & rw.Cells("recordId").Value
                            cmd.CommandText = sSQL
                            cmd.ExecuteNonQuery()
                            'Debug.Print(sSQL)
                        Else
                            'Debug.Print("New Row which is still not in DB!")
                        End If
                        DGVTimeCardMaster.Rows.Remove(rw)
                    Next
                    cmd.Dispose()
                Catch ex As Exception
                    MsgBox("Error deleting records!" & vbCrLf & e.ToString())
                End Try
                dbConnection.Connection.Close()
                LoadData(cboUsers.SelectedItem.recordId)
            Else
                MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
            End If
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

    Private Sub DGVTimeCardMaster_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardMaster.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardMaster.Columns("addNextRow").Index Then
            DGVTimeCardMaster.Rows.Add()
        End If

        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardMaster.Columns("delCurRow").Index Then
            RemoveRow(e.RowIndex)
        End If
    End Sub

    Private Sub frmTimeCardMaster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVTimeCardMaster.Width = Width - 40
        DGVTimeCardMaster.Height = Height - 90
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

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim rw As DataGridViewRow
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        rw = DGVTimeCardMaster.Rows(iRowIndex)
        If dbConnection.GetConnection() Then
            Try
                cmd = dbConnection.Connection.CreateCommand()
                If rw.Cells("recordId").Value IsNot Nothing Then
                    sSQL = "DELETE FROM TimeCardMaster WHERE TimeCardId = " & rw.Cells("recordId").Value
                    cmd.CommandText = sSQL
                    cmd.ExecuteNonQuery()
                End If
                DGVTimeCardMaster.Rows.Remove(rw)
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

End Class