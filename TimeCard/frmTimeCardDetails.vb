Public Class frmTimeCardDetails
    Private pFirstVisibleColumnNumber As Integer
    Private pUserIdColumnNumber As Integer
    Private pLogDateColumnNumber As Integer
    Private pInTimeColumnNumber As Integer
    Private pOutTimeColumnNumber As Integer
    Private dbConnection As TimeCardDataAccess

    Private Sub frmTimeCardDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = "C:\db\TimeCard.mdb"
        InitializeGrid()
        LoadUserMaster()
    End Sub

    Private Sub LoadUserMaster()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim udUser As UserDetails
        cboUsers.Items.Clear()
        cboUsers.DisplayMember = "DisplayName"
        sSQL = "SELECT * FROM UserMaster"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                udUser = New UserDetails
                With udUser
                    .recordId = dr.GetInt32(0)
                    .userId = dr.GetInt16(1)
                    .userName = dr.GetString(2)
                    .Trade = dr.GetString(3)
                    .Basic = dr.GetDouble(4)
                End With
                cboUsers.Items.Add(udUser)
            End While
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub InitializeGrid()
        Dim col1 As Object
        DGVTimeCardDetails.MultiSelect = False
        With DGVTimeCardDetails.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordStatus"
            col1.HeaderText = "Record Status"
            col1.ReadOnly = True
            col1.Width = 60
            col1.Visible = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "RecordId"
            col1.ReadOnly = True
            col1.Width = 60
            .Add(col1)
            pFirstVisibleColumnNumber = 1

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "logDate"
            col1.HeaderText = "Date"
            col1.DefaultCellStyle.Format = "D"
            col1.ReadOnly = False
            .Add(col1)
            pLogDateColumnNumber = 2

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "inTime"
            col1.HeaderText = "In Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            .Add(col1)
            pInTimeColumnNumber = 3

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "outTime"
            col1.HeaderText = "Out Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            .Add(col1)
            pOutTimeColumnNumber = 4

            col1 = New DataGridViewCheckBoxColumn()
            col1.Name = "isHoliday"
            col1.HeaderText = "Is Holiday"
            col1.ReadOnly = False
            col1.width = 75
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "regularHrs"
            col1.HeaderText = "Regular Hours"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "OT1"
            col1.HeaderText = "Over Time 1"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "OT2"
            col1.HeaderText = "Over Time 2"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "totalCost"
            col1.HeaderText = "Total Cost"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewButtonColumn()
            col1.Name = "addNextRow"
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

    Private Sub DGVTimeCardDetails_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardDetails.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardDetails.Columns("addNextRow").Index Then
            DGVTimeCardDetails.Rows.Add()
        End If

        If e.RowIndex >= 0 And e.ColumnIndex = DGVTimeCardDetails.Columns("delCurRow").Index Then
            RemoveRow(e.RowIndex)
        End If
    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim sRecordId As String
        If DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value IsNot Nothing Then
            sRecordId = DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
        Else
            sRecordId = ""
        End If
        If sRecordId <> "" Then
            If dbConnection.GetConnection() Then
                sSQL = "DELETE FROM TimeCardDetailData WHERE TimeCardDataId = " & DGVTimeCardDetails.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
                cmd = dbConnection.Connection.CreateCommand()
                cmd.CommandText = sSQL
                Try
                    cmd.ExecuteNonQuery()
                    DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
                Catch
                End Try
            End If
        Else
            DGVTimeCardDetails.Rows.RemoveAt(iRowIndex)
        End If
    End Sub

    Private Sub cboUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsers.SelectedIndexChanged
        If cboUsers.SelectedIndex >= 0 Then
            LoadTimeCardMaster(cboUsers.SelectedItem)
        End If
    End Sub

    Private Sub LoadTimeCardMaster(udUser As UserDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim tcmTimeCard As TimeCardMasterDetails
        cboTimeCards.Items.Clear()
        cboTimeCards.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT * FROM TimeCardMaster WHERE userId = " & udUser.recordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                tcmTimeCard = New TimeCardMasterDetails()
                tcmTimeCard.RecordId = dr.GetInt32(0)
                tcmTimeCard.UserId = dr.GetInt32(1)
                tcmTimeCard.TimeCardNumber = dr.GetInt32(2)
                tcmTimeCard.TimeCardMonth = dr.GetInt16(3)
                tcmTimeCard.TimeCardYear = dr.GetInt16(4)
                cboTimeCards.Items.Add(tcmTimeCard)
            End While
        Else
            MsgBox("Error connecting to database!" & dbConnection.LastError)
        End If
    End Sub

    Private Sub cboTimeCards_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTimeCards.SelectedIndexChanged
        If cboTimeCards.SelectedIndex >= 0 Then
            LoadTimeCardDetails(cboTimeCards.SelectedItem.recordId)
        End If
    End Sub

    Private Sub LoadTimeCardDetails(iRecordId As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim iNewItem As Integer
        Dim rw As DataGridViewRow
        If dbConnection.GetConnection() Then
            sSQL = "SELECT * FROM TimeCardDetailData WHERE TimeCardId = " & iRecordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                iNewItem = DGVTimeCardDetails.Rows.Add()
                rw = DGVTimeCardDetails.Rows(iNewItem)
                rw.Cells("recordStatus").Value = Nothing
                rw.Cells("TimeCardDataId").Value = dr.GetInt32(0)
            End While
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If

        DGVTimeCardDetails.Rows.Clear()

    End Sub
End Class