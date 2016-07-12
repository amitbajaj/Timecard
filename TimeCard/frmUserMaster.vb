Public Class frmUserMaster
    Dim dbConnection As TimeCardDataAccess

    Private Sub DGVUserMaster_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DGVUserMaster.RowsRemoved

    End Sub

    Private Sub frmUserMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess
        dbConnection.DatabaseFile = "C:\DB\TimeCard.mdb"
        InitializeGrid()
        LoadData()
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        DGVUserMaster.Columns.Clear()
        DGVUserMaster.AllowUserToAddRows = False
        DGVUserMaster.AllowUserToDeleteRows = False
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

        End With
    End Sub

    Private Sub LoadData()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim iRow As Integer
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT * FROM UserMaster"
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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim iNewRow As Integer
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        DGVUserMaster.Focus()
        iNewRow = DGVUserMaster.Rows.Add()
        DGVUserMaster.CurrentCell = DGVUserMaster.Rows(iNewRow).Cells(0)
        sSQL = "INSERT INTO UserMaster(userNumber) VALUES(NULL);"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
        LoadData()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim rw As DataGridViewRow
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        If DGVUserMaster.SelectedRows.Count > 0 Then
            If dbConnection.GetConnection() Then
                Try
                    cmd = dbConnection.Connection.CreateCommand()
                    For Each rw In DGVUserMaster.SelectedRows
                        If rw.Cells("recordId").Value IsNot Nothing Then
                            sSQL = "DELETE FROM UserMaster WHERE recordId = " & rw.Cells("recordId").Value
                            cmd.CommandText = sSQL
                            cmd.ExecuteNonQuery()
                            'Debug.Print(sSQL)
                        Else
                            'Debug.Print("New Row which is still not in DB!")
                        End If
                        DGVUserMaster.Rows.Remove(rw)
                    Next
                    cmd.Dispose()
                Catch ex As Exception
                    MsgBox("Error deleting records!" & vbCrLf & e.ToString())
                End Try
                dbConnection.Connection.Close()
                LoadData()
            Else
                MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
            End If
        End If
    End Sub

    Private Sub DGVUserMaster_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUserMaster.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        rw = DGVUserMaster.Rows(e.RowIndex)
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
        End If
    End Sub
End Class