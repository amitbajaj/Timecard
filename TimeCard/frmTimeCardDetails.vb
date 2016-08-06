Public Class frmTimeCardDetails
    Private dbConnection As TimeCardDataAccess
    Private iDecimals As Integer = My.Settings.Item("NumberOfDecimals")
    Private sNumberFormat As String = My.Settings.Item("NumberFormat")

    Private Sub frmTimeCardDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = My.Settings.Item("DBFile")
        lblTotalCost.Text = ""
        lblTotalCost.TextAlign = ContentAlignment.TopRight
        lblRegHrs.Text = ""
        lblRegHrs.TextAlign = ContentAlignment.TopRight

        lblOT1.Text = ""
        lblOT1.TextAlign = ContentAlignment.TopLeft

        lblOT2.Text = ""
        lblOT2.TextAlign = ContentAlignment.TopLeft
        InitializeGrid()
        LoadUserMaster()
    End Sub


    Private Sub LoadUserMaster()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim udUser As TimeCardSupport.UserDetails
        cboUsers.Items.Clear()
        cboUsers.DisplayMember = "DisplayName"
        sSQL = "SELECT RecordId, UserNumber, UserName, Trade, Basic FROM UserMaster"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                udUser = New TimeCardSupport.UserDetails
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
        DGVTimeCardDetails.AllowUserToAddRows = False
        DGVTimeCardDetails.AllowUserToDeleteRows = False
        DGVTimeCardDetails.AllowUserToOrderColumns = False
        DGVTimeCardDetails.AllowUserToResizeRows = False
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

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "logDay"
            col1.HeaderText = "Day"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "inTime"
            col1.HeaderText = "In Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "outTime"
            col1.HeaderText = "Out Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = False
            .Add(col1)

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
        DGVTimeCardDetails.AutoResizeColumns()
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
        UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
    End Sub

    Private Sub cboUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboUsers.SelectedIndexChanged
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        If cboUsers.SelectedIndex >= 0 Then
            LoadTimeCardMaster(cboUsers.SelectedItem)
        End If
    End Sub

    Private Sub LoadTimeCardMaster(udUser As TimeCardSupport.UserDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim sSQL As String
        Dim tcmTimeCard As TimeCardSupport.TimeCardMasterDetails
        cboTimeCards.Items.Clear()
        cboTimeCards.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            sSQL = "SELECT * FROM TimeCardMaster WHERE userId = " & udUser.recordId
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            While dr.Read()
                tcmTimeCard = New TimeCardSupport.TimeCardMasterDetails()
                tcmTimeCard.RecordId = dr.GetInt32(0)
                tcmTimeCard.UserId = dr.GetInt32(1)
                tcmTimeCard.TimeCardNumber = dr.GetInt32(2)
                tcmTimeCard.TimeCardMonth = dr.GetInt16(3)
                tcmTimeCard.TimeCardYear = dr.GetInt16(4)
                cboTimeCards.Items.Add(tcmTimeCard)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
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
        Dim dReg, dOT1, dOT2, dTotalCost As Double
        Dim oGridTotal As New TimeCardSupport.GridTotals
        Dim oTime As New TimeCardSupport.TimeField
        dReg = 0
        dOT1 = 0
        dOT2 = 0
        dTotalCost = 0
        DGVTimeCardDetails.Rows.Clear()
        lblTotalCost.Text = ""
        If dbConnection.GetConnection() Then
            sSQL = "SELECT * FROM TimeCardDetailData WHERE TimeCardId = " & iRecordId & " ORDER BY TimeCardDay"
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            dr = cmd.ExecuteReader()
            If dr.HasRows() Then
                While dr.Read()
                    iNewItem = DGVTimeCardDetails.Rows.Add()
                    rw = DGVTimeCardDetails.Rows(iNewItem)
                    rw.Cells("recordStatus").Value = Nothing
                    rw.Cells("recordId").Value = dr.GetInt32(0)
                    If dr.IsDBNull(2) Then
                        rw.Cells("logDay").Value = Nothing
                    Else
                        rw.Cells("logDay").Value = dr.GetInt16(2)
                    End If
                    If dr.IsDBNull(3) Then
                        rw.Cells("isHoliday").Value = Nothing
                    Else
                        rw.Cells("isHoliday").Value = dr.GetBoolean(3)
                    End If
                    If dr.IsDBNull(4) Then
                        rw.Cells("inTime").Value = Nothing
                    Else
                        rw.Cells("inTime").Value = dr.GetDateTime(4).TimeOfDay.ToString()
                    End If
                    If dr.IsDBNull(5) Then
                        rw.Cells("outTime").Value = Nothing
                    Else
                        rw.Cells("outTime").Value = dr.GetDateTime(5).TimeOfDay.ToString()
                    End If
                    If dr.IsDBNull(6) Then
                        rw.Cells("regularHrs").Value = Nothing
                    Else
                        oTime.SetTime(dr.GetDouble(6))
                        dReg = dReg + oTime.GetTime()
                        rw.Cells("regularHrs").Value = oTime.DisplayTime
                    End If
                    If dr.IsDBNull(7) Then
                        rw.Cells("OT1").Value = Nothing
                    Else
                        oTime.SetTime(dr.GetDouble(7))
                        dOT1 = dOT1 + oTime.GetTime()
                        rw.Cells("OT1").Value = oTime.DisplayTime
                    End If
                    If dr.IsDBNull(8) Then
                        rw.Cells("OT2").Value = Nothing
                    Else
                        oTime.SetTime(dr.GetDouble(8))
                        dOT2 = dOT2 + oTime.GetTime()
                        rw.Cells("OT2").Value = oTime.DisplayTime
                    End If
                    If dr.IsDBNull(9) Then
                        rw.Cells("totalCost").Value = Nothing
                    Else
                        rw.Cells("totalCost").Value = dr.GetDouble(9).ToString(sNumberFormat)
                        dTotalCost = dTotalCost + dr.GetDouble(8)
                    End If
                End While
                oGridTotal.AddValues(dReg, dOT1, dOT2, dTotalCost)
                UpdateGridTotal(oGridTotal)
                DGVTimeCardDetails.CurrentCell = DGVTimeCardDetails.Rows(0).Cells(1)
            Else
                iNewItem = DGVTimeCardDetails.Rows.Add()
                DGVTimeCardDetails.CurrentCell = DGVTimeCardDetails.Rows(iNewItem).Cells(1)
            End If
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Private Sub frmTimeCardDetails_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        DGVTimeCardDetails.Width = Width - 40
        DGVTimeCardDetails.Height = Height - 130
        lblTotalCost.Left = Width - lblTotalCost.Width - 40
        lblTotalCost.Top = Height - 70
        lblRegHrs.Top = Height - 70
        lblOT1.Top = Height - 70
        lblOT2.Top = Height - 70


    End Sub

    Private Sub DGVTimeCardDetails_CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles DGVTimeCardDetails.CellValidating
        e.Cancel = Not TimeCardSupport.ValidateCell(cboTimeCards, DGVTimeCardDetails, e.ColumnIndex, e.RowIndex, e.FormattedValue.ToString())
    End Sub

    Private Sub DGVTimeCardDetails_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVTimeCardDetails.CellEndEdit
        SaveData(e.RowIndex)
    End Sub

    Private Sub SaveData(iRow As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rw As DataGridViewRow
        Dim tInTime, tOutTime As Date
        Dim bNewRow As Boolean
        Dim bInTime, bOutTime, bHoliday As Boolean
        Dim oTimeField As TimeCardSupport.TimeField
        oTimeField = New TimeCardSupport.TimeField
        rw = DGVTimeCardDetails.Rows(iRow)
        bInTime = False
        bOutTime = False
        bHoliday = False
        If rw.Cells("recordId").Value Is Nothing Then
            bNewRow = True
            sSQL = "INSERT INTO TimeCardDetailData(TimeCardId, TimeCardDay, IsHoliday, InTime, OutTime, RegHrs, OT1Hrs, OT2Hrs, TotalCost) VALUES("
            sSQL = sSQL & cboTimeCards.SelectedItem.recordId
            sSQL = sSQL & ", "
            If rw.Cells("logDay").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("logDay").Value
            End If
            sSQL = sSQL & ", "
            If rw.Cells("isHoliday").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isHoliday").Value.ToString()
                bHoliday = rw.Cells("isHoliday").Value
            End If

            sSQL = sSQL & ", "
            If Date.TryParse(rw.Cells("inTime").Value, tInTime) Then
                sSQL = sSQL & "'" & tInTime.TimeOfDay.ToString() & "'"
                bInTime = True
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ","
            If Date.TryParse(rw.Cells("outTime").Value, tOutTime) Then
                sSQL = sSQL & "'" & tOutTime.TimeOfDay.ToString() & "'"
                bOutTime = True
            Else
                sSQL = sSQL & "NULL"
            End If
            If bInTime And bOutTime Then
                TimeCardSupport.CalculateCost(cboUsers.SelectedItem.Basic, rw, iDecimals, sNumberFormat, tInTime, tOutTime, bHoliday)
                UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
            End If

            sSQL = sSQL & ", "
            If rw.Cells("regularHrs").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("regularHrs").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", "

            If rw.Cells("OT1").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("OT1").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", "
            If rw.Cells("OT2").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("OT2").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", "
            If rw.Cells("totalCost").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("totalCost").Value
            End If
            sSQL = sSQL & ")"
        Else
            sSQL = "UPDATE TimeCardDetailData SET "
            sSQL = sSQL & "TimeCardDay = "
            If rw.Cells("logDay").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("logDay").Value
            End If
            sSQL = sSQL & ", isHoliday = "
            If rw.Cells("isHoliday").Value Is Nothing Then
                sSQL = sSQL & "false"
            Else
                sSQL = sSQL & rw.Cells("isHoliday").Value.ToString()
                bHoliday = rw.Cells("isHoliday").Value
            End If
            sSQL = sSQL & ", inTime = "
            If rw.Cells("inTime").Value IsNot Nothing Then
                If Date.TryParse(rw.Cells("inTime").Value.ToString(), tInTime) Then
                    sSQL = sSQL & "'" & tInTime.TimeOfDay.ToString() & "'"
                    bInTime = True
                Else
                    sSQL = sSQL & "NULL"
                End If
            Else
                sSQL = sSQL & "NULL"
            End If
            sSQL = sSQL & ", outTime = "
            If rw.Cells("outTime").Value IsNot Nothing Then
                If Date.TryParse(rw.Cells("outTime").Value.ToString(), tOutTime) Then
                    sSQL = sSQL & "'" & tOutTime.TimeOfDay.ToString() & "'"
                    bOutTime = True
                Else
                    sSQL = sSQL & "NULL"
                End If
            Else
                sSQL = sSQL & "NULL"
            End If
            If bInTime And bOutTime Then
                TimeCardSupport.CalculateCost(cboUsers.SelectedItem.Basic, rw, iDecimals, sNumberFormat, tInTime, tOutTime, bHoliday)
                UpdateGridTotal(TimeCardSupport.GridTotal(DGVTimeCardDetails))
            End If
            sSQL = sSQL & ", RegHrs = "
            If rw.Cells("regularHrs").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("regularHrs").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", OT1Hrs = "
            If rw.Cells("OT1").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("OT1").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", OT2Hrs = "
            If rw.Cells("OT2").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                oTimeField.SetTime(rw.Cells("OT2").Value)
                sSQL = sSQL & oTimeField.GetTime()
            End If
            sSQL = sSQL & ", TotalCost = "
            If rw.Cells("totalCost").Value Is Nothing Then
                sSQL = sSQL & "NULL"
            Else
                sSQL = sSQL & rw.Cells("totalCost").Value
            End If
            sSQL = sSQL & " WHERE TimeCardDataId = " & rw.Cells("recordId").Value
        End If
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = sSQL
            cmd.ExecuteNonQuery()
            If bNewRow Then
                cmd.CommandText = "SELECT @@IDENTITY"
                rw.Cells("recordId").Value = cmd.ExecuteScalar()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox("Error connecting to database!" & vbCrLf & dbConnection.LastError)
        End If
    End Sub

    Sub UpdateGridTotal(oGridTotal As TimeCardSupport.GridTotals)
        lblTotalCost.Text = "Total Cost: " & Math.Round(oGridTotal.TotalCost, iDecimals).ToString(sNumberFormat)
        lblTotalCost.AutoSize = True
        lblTotalCost.Left = Width - lblTotalCost.Width - 40
        lblRegHrs.Text = "Total Regular : " & TimeCardSupport.DisplayDays(oGridTotal.RegularHours)
        lblOT1.Text = "Total OT1 : " & TimeCardSupport.DisplayDays(oGridTotal.OT1Hours)
        lblOT2.Text = "Total OT2 : " & TimeCardSupport.DisplayDays(oGridTotal.OT2Hours)
    End Sub

    Private Sub DGVTimeCardDetails_KeyDown(sender As Object, e As KeyEventArgs) Handles DGVTimeCardDetails.KeyDown
        If e.KeyCode = Asc(vbCr) Then
            With DGVTimeCardDetails
                If TimeCardSupport.ValidateCell(cboTimeCards, DGVTimeCardDetails, .CurrentCell.ColumnIndex, .CurrentRow.Index, .CurrentCell.Value) Then
                    If .CurrentCell.ColumnIndex < .ColumnCount - 2 Then
                        .CurrentCell = .Rows.Item(.CurrentCell.RowIndex).Cells.Item(.CurrentCell.ColumnIndex + 1)
                    ElseIf .CurrentCell.RowIndex < .Rows.Count - 1 Then
                        .CurrentCell = .Rows.Item(.CurrentCell.RowIndex + 1).Cells.Item(1)
                    ElseIf .CurrentCell.RowIndex = .Rows.Count - 1 And .CurrentCell.ColumnIndex = .ColumnCount - 2 Then
                        .Rows.Add()
                        .CurrentCell = .Rows.Item(.Rows.Count - 1).Cells(1)
                    End If
                    e.Handled = True
                End If
            End With
        End If

    End Sub
End Class