Public Class frmTimeCardMaster
    Private dbConnection As TimeCardDataAccess
    Private Class MonthClass
        Private _monthNumber As Short
        Private _monthName As String
        Public Property MonthName As String
            Get
                Return _monthName
            End Get
            Set(value As String)
                _monthName = value
            End Set
        End Property

        Public Property MonthNumber As Short
            Get
                Return _monthNumber
            End Get
            Set(value As Short)
                _monthNumber = value
            End Set
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return _monthNumber & " - " & _monthName
            End Get
        End Property
    End Class


    Private Sub frmTimeCardMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = "C:\db\TimeCard.mdb"
        InitializeGrid()
        LoadUsers()
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        'Dim sMonthName(11) As String
        'Dim iMonthNumber(11) As Short
        'Dim col2 As DataGridViewComboBoxColumn
        'Dim mMonth As MonthClass
        'Dim iMonthCount As Integer
        DGVTimeCardMaster.Columns.Clear()
        DGVTimeCardMaster.AllowUserToAddRows = False
        DGVTimeCardMaster.AllowUserToDeleteRows = False
        'sMonthName(0) = "January"
        'sMonthName(1) = "February"
        'sMonthName(2) = "March"
        'sMonthName(3) = "April"
        'sMonthName(4) = "May"
        'sMonthName(5) = "June"
        'sMonthName(6) = "July"
        'sMonthName(7) = "August"
        'sMonthName(8) = "September"
        'sMonthName(9) = "October"
        'sMonthName(10) = "November"
        'sMonthName(11) = "December"

        'iMonthNumber(0) = 1
        'iMonthNumber(1) = 2
        'iMonthNumber(2) = 3
        'iMonthNumber(3) = 4
        'iMonthNumber(4) = 5
        'iMonthNumber(5) = 6
        'iMonthNumber(6) = 7
        'iMonthNumber(7) = 8
        'iMonthNumber(8) = 9
        'iMonthNumber(9) = 10
        'iMonthNumber(10) = 11
        'iMonthNumber(11) = 12


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

            'col2 = New DataGridViewComboBoxColumn()
            'col2.Name = "timeCardMonth"
            'col2.HeaderText = "Month"
            ''col2.DisplayMember = "DisplayMonth"
            ''col2.ValueMember = "MonthNumber"
            'col2.ReadOnly = False

            'For iMonthCount = 0 To 11
            '    mMonth = New MonthClass
            '    mMonth.MonthNumber = iMonthNumber(iMonthCount)
            '    mMonth.MonthName = sMonthName(iMonthCount)
            '    col2.Items.Add(mMonth.MonthNumber)
            'Next
            '.Add(col2)

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
        End With
        'DGVTimeCardMaster.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader)
    End Sub

    Private Sub LoadUsers()
        Dim sSQL As String
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim iNewItem As Integer
        Dim userMaster As userDetails
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            sSQL = "SELECT * FROM UserMaster"
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            cboUsers.Items.Clear()
            cboUsers.DisplayMember = "displayName"
            While dr.Read()
                userMaster = New userDetails
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
        Debug.Print(cboUsers.SelectedItem.displayName)
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
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
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
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
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
                LoadData(cboUsers.SelectedIndex)
            Else
                MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
            End If
        End If
    End Sub
End Class