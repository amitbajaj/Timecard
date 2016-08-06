Imports System.Data
Public Class frmTimeCard
    Dim bEditMode As Boolean
    Dim oEditRow As DataGridViewRow
    Dim conn As OleDb.OleDbConnection
    Dim pStartTime As Date
    Dim pEndTime As Date
    Dim pRegularRate As Double
    Dim pOT1Rate As Double
    Dim pOT2Rate As Double
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 And e.ColumnIndex <> DataGridView1.Columns("delCurRow").Index And e.ColumnIndex <> DataGridView1.Columns("delCurRow").Index Then
            DataGridView1.Rows(e.RowIndex).Selected = True
            ReadValues(DataGridView1.Rows(e.RowIndex))
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex >= 0 And e.ColumnIndex = DataGridView1.Columns("addNextRow").Index Then
            DataGridView1.Rows.Add()
            DataGridView1.Rows(DataGridView1.RowCount - 1).Selected = True
            ReadValues(DataGridView1.Rows(DataGridView1.RowCount - 1))
        End If

        If e.RowIndex >= 0 And e.ColumnIndex = DataGridView1.Columns("delCurRow").Index Then
            RemoveRow(e.RowIndex)
        End If

    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim sRecordId As String
        If DataGridView1.Rows.Item(iRowIndex).Cells.Item("recordId").Value IsNot Nothing Then
            sRecordId = DataGridView1.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
        Else
            sRecordId = ""
        End If
        If sRecordId <> "" Then
            If OpenConn() Then
                sSQL = "DELETE FROM TimeCardData WHERE RecordId = " & DataGridView1.Rows.Item(iRowIndex).Cells.Item("recordId").Value.ToString()
                cmd = conn.CreateCommand()
                cmd.CommandText = sSQL
                Try
                    cmd.ExecuteNonQuery()
                    DataGridView1.Rows.RemoveAt(iRowIndex)
                Catch
                End Try
            End If
        Else
            DataGridView1.Rows.RemoveAt(iRowIndex)
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DataGridView1.KeyPress
        If DataGridView1.SelectedCells.Item(0).ColumnIndex < DataGridView1.ColumnCount - 1 Then
            DataGridView1.Rows(DataGridView1.SelectedCells(0).RowIndex).Selected = True
            ReadValues(DataGridView1.Rows(DataGridView1.SelectedCells(0).RowIndex))
        ElseIf DataGridView1.SelectedCells.Item(0).ColumnIndex = DataGridView1.ColumnCount - 1 Then
            DataGridView1.Rows.Add()
            DataGridView1.Rows(DataGridView1.RowCount - 1).Selected = True
            ReadValues(DataGridView1.Rows(DataGridView1.RowCount - 1))
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iColCount As Integer
        LoadParameters()
        For iColCount = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns.RemoveAt(0)
        Next
        Dim col1 As Object
        With DataGridView1.Columns
            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "recordId"
            col1.HeaderText = "RecordId"
            col1.ReadOnly = True
            col1.Width = 60
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "userId"
            col1.HeaderText = "User Id"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "logDate"
            col1.HeaderText = "Date"
            col1.DefaultCellStyle.Format = "D"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "inTime"
            col1.HeaderText = "In Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewTextBoxColumn()
            col1.Name = "outTime"
            col1.HeaderText = "Out Time"
            col1.DefaultCellStyle.Format = "T"
            col1.ReadOnly = True
            .Add(col1)

            col1 = New DataGridViewCheckBoxColumn()
            col1.Name = "isHoliday"
            col1.HeaderText = "Is Holiday"
            col1.ReadOnly = True
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
        DataGridView1.MultiSelect = False
        LoadData()
    End Sub

    Sub ReadValues(ByVal oRow As DataGridViewRow)
        lblStatus.Text = "Edit mode... Please 'Save' or 'Cancel' to enable GRID again"
        oRow.DataGridView.Enabled = False
        oEditRow = oRow
        With oRow.Cells
            txtUserId.Text = .Item("userId").Value
            If IsNothing(.Item("logDate").Value) Then
                logDate.Value = Now()
            Else
                logDate.Value = .Item("logDate").Value
            End If

            bIsHoliday.Checked = .Item("isHoliday").Value

            If IsNothing(.Item("inTime").Value) Then
                tInTime.Value = Now()
            Else
                tInTime.Value = .Item("inTime").Value
            End If

            If IsNothing(.Item("outTime").Value) Then
                tOutTime.Value = Now()
            Else
                tOutTime.Value = .Item("outTime").Value
            End If

        End With
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveDataToGrid()
    End Sub

    Private Sub SaveDataToGrid()
        With oEditRow.Cells
            .Item("userId").Value = txtUserId.Text
            .Item("logDate").Value = logDate.Value
            .Item("inTime").Value = tInTime.Value
            .Item("outTime").Value = tOutTime.Value
            .Item("isHoliday").Value = bIsHoliday.Checked
            CalculateCost(oEditRow)
        End With
        oEditRow.Selected = True
        oEditRow.DataGridView.Enabled = True
        lblStatus.Text = "Grid enabled. Select a row to edit data or add a new row"
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If oEditRow IsNot Nothing Then
            If oEditRow.DataGridView IsNot Nothing Then
                oEditRow.DataGridView.Enabled = True
            End If
            oEditRow = Nothing
        End If
        lblStatus.Text = "Grid enabled. Select a row to edit data or add a new row"
    End Sub

    Private Sub btnAddRow_Click(sender As Object, e As EventArgs) Handles btnAddRow.Click
        DataGridView1.Rows.Add()
    End Sub

    Private Sub CalculateCost(oRow As DataGridViewRow)
        Dim iRegularHours As Double
        Dim iOT1Hours As Double
        Dim iOT2Hours As Double

        Dim dRegRate As Double
        Dim dOT1Rate As Double
        Dim dOT2Rate As Double

        Dim dTotalCost As Double

        Dim tsStartTime As TimeSpan
        Dim tsEndTime As TimeSpan
        Dim tsInTime As TimeSpan
        Dim tsOutTime As TimeSpan

        With oRow.Cells
            tsInTime = CDate(.Item("inTime").Value).TimeOfDay
            tsOutTime = CDate(.Item("outTime").Value).TimeOfDay


            tsStartTime = pStartTime.TimeOfDay
            tsEndTime = pEndTime.TimeOfDay

            If .Item("isHoliday").Value Then
                iRegularHours = 0
                iOT1Hours = 0
                iOT2Hours = tsOutTime.Subtract(tsInTime).TotalHours
            Else
                iOT2Hours = 0
                iRegularHours = 0
                iOT1Hours = 0
                If tsInTime.Subtract(tsStartTime).TotalHours >= 0 Then
                    If tsInTime.Subtract(tsEndTime).TotalHours < 0 Then
                        If tsOutTime.Subtract(tsEndTime).TotalHours > 0 Then
                            iRegularHours = tsEndTime.Subtract(tsInTime).TotalHours
                            iOT1Hours = tsOutTime.Subtract(tsEndTime).TotalHours
                        Else
                            iRegularHours = tsOutTime.Subtract(tsInTime).TotalHours
                        End If
                    Else
                        iOT1Hours = tsOutTime.Subtract(tsInTime).TotalHours
                    End If
                Else
                    If tsOutTime.Subtract(tsStartTime).TotalHours > 0 Then
                        iOT1Hours = tsStartTime.Subtract(tsInTime).TotalHours
                        If tsOutTime.Subtract(tsEndTime).TotalHours > 0 Then
                            iOT1Hours = iOT1Hours + tsEndTime.Subtract(tsOutTime).TotalHours
                            iRegularHours = tsOutTime.Subtract(tsStartTime).TotalHours
                        Else
                            iRegularHours = tsOutTime.Subtract(tsStartTime).TotalHours
                        End If
                    Else
                        iOT1Hours = tsOutTime.Subtract(tsInTime).TotalHours
                    End If
                End If
            End If
            .Item("regularHrs").Value = Math.Round(iRegularHours, 2).ToString()
            .Item("OT1").Value = Math.Round(iOT1Hours, 2).ToString()
            .Item("OT2").Value = Math.Round(iOT2Hours, 2).ToString()
            If Not Double.TryParse(pRegularRate, dRegRate) Then
                dRegRate = 0
            End If
            If Not Double.TryParse(pOT1Rate, dOT1Rate) Then
                dOT1Rate = 0
            End If
            If Not Double.TryParse(pOT2Rate, dOT2Rate) Then
                dOT2Rate = 0
            End If

            dTotalCost = (iRegularHours * dRegRate) + (iOT1Hours * dOT1Rate) + (iOT2Hours * dOT2Rate)
            .Item("totalCost").Value = Math.Round(dTotalCost, 2).ToString()

        End With
    End Sub
    Private Function OpenConn() As Boolean
        Dim sConn As String
        'Microsoft.Jet.OLEDB.4.0
        'Microsoft.ACE.OLEDB.12.0
        Dim sDBFile As String = My.Settings.Item("DBFile")
        sConn = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" & sDBFile & ";Persist Security Info=False;User Id=Admin;"
        If conn Is Nothing Then
            conn = New OleDb.OleDbConnection(sConn)
        Else
            If conn.state = 1 Then
                OpenConn = True
                Exit Function
            Else
                conn.close()
            End If
        End If
        Try
            OpenConn = True
            conn.Open()
        Catch
            OpenConn = False
        End Try
    End Function

    Private Sub LoadData()
        Dim cmd As Data.OleDb.OleDbCommand
        Dim rs As Data.OleDb.OleDbDataReader
        Dim sSQL As String
        Dim iRowCount As Integer

        If bEditMode Then
            MsgBox("Grid Editing is active. Please save the current record....")
            Exit Sub
        End If

        sSQL = "SELECT RecordId, EmployeeName, RecordDate, InTime, OutTime, IsHoliday, RegularHrs, OT1Hrs, OT2Hrs, TotalCost From TimeCardData"
        If OpenConn() Then
            cmd = conn.CreateCommand()
            cmd.CommandText = sSQL
            rs = cmd.ExecuteReader()
            With DataGridView1
                iRowCount = .RowCount
                While iRowCount > 0
                    .Rows.RemoveAt(0)
                    iRowCount = iRowCount - 1
                End While
                While rs.Read()
                    .Rows.Add()
                    With .Rows.Item(.RowCount - 1).Cells
                        .Item("recordId").Value = rs.GetInt32(0)
                        .Item("userId").Value = rs.GetString(1)
                        .Item("logDate").Value = rs.GetDateTime(2)
                        .Item("inTime").Style.Format = "T"
                        .Item("inTime").Value = rs.GetDateTime(3).ToString()
                        .Item("outTime").Value = rs.GetDateTime(4).ToString()
                        .Item("isHoliday").Value = rs.GetBoolean(5)
                        .Item("regularHrs").Value = rs.GetDouble(6).ToString()
                        .Item("OT1").Value = rs.GetDouble(7).ToString()
                        .Item("OT2").Value = rs.GetDouble(8).ToString()
                        .Item("totalCost").Value = rs.GetDouble(9).ToString()
                    End With
                    ReadValues(.Rows.Item(.RowCount - 1))
                    SaveDataToGrid()
                End While
            End With
            rs.Close()
        Else
            MsgBox("Error connecting to database!!")
        End If

    End Sub

    Private Sub SaveData()
        Dim cmd As Data.OleDb.OleDbCommand
        Dim rw As DataGridViewRow
        Dim sSQL As String
        If OpenConn() Then
            cmd = conn.CreateCommand()
            For Each rw In DataGridView1.Rows
                With rw.Cells
                    If .Item("recordId").Value Is Nothing Then
                        sSQL = "INSERT INTO TimeCardData(EmployeeName, RecordDate, InTime, OutTime, IsHoliday, RegularHrs, OT1Hrs, OT2Hrs, TotalCost) VALUES ( "
                        sSQL = sSQL & "'" & .Item("userId").Value & "',"
                        sSQL = sSQL & "'" & .Item("logDate").Value & "',"
                        sSQL = sSQL & "'" & .Item("inTime").Value & "',"
                        sSQL = sSQL & "'" & .Item("outTime").Value & "',"
                        sSQL = sSQL & IIf(.Item("isHoliday").Value, "True", "False") & ","
                        sSQL = sSQL & .Item("regularHrs").Value & ","
                        sSQL = sSQL & .Item("OT1").Value & ","
                        sSQL = sSQL & .Item("OT2").Value & ","
                        sSQL = sSQL & .Item("totalCost").Value & ")"
                    Else
                        sSQL = "UPDATE TimeCardData SET "
                        sSQL = sSQL & "EmployeeName = '" & .Item("userId").Value & "',"
                        sSQL = sSQL & "RecordDate = '" & .Item("logDate").Value & "',"
                        sSQL = sSQL & "InTime = '" & .Item("inTime").Value & "',"
                        sSQL = sSQL & "OutTime = '" & .Item("outTime").Value & "',"
                        sSQL = sSQL & "IsHoliday = " & IIf(.Item("isHoliday").Value, "True", "False") & ","
                        sSQL = sSQL & "RegularHrs = " & .Item("regularHrs").Value & ","
                        sSQL = sSQL & "OT1Hrs = " & .Item("OT1").Value & ","
                        sSQL = sSQL & "OT2Hrs = " & .Item("OT2").Value & ","
                        sSQL = sSQL & "TotalCost = " & .Item("totalCost").Value
                        sSQL = sSQL & " WHERE RecordId = " & .Item("recordId").Value
                    End If
                    cmd.CommandText = sSQL
                    cmd.ExecuteNonQuery()
                End With
            Next
            LoadData()
        Else
            MsgBox("Cannot connect to the database")
        End If
    End Sub


    Private Sub btnLoadData_Click(sender As Object, e As EventArgs) Handles btnLoadData.Click
        LoadData()
    End Sub

    Private Sub btnSaveData_Click(sender As Object, e As EventArgs) Handles btnSaveData.Click
        SaveData()
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim frmReportInstance As New frmReport()
        frmReportInstance.rptViewTimeCard.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        frmReportInstance.WindowState = FormWindowState.Maximized
        frmReportInstance.ShowDialog()
    End Sub

    Private Sub LoadParameters()
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        Dim rs As OleDb.OleDbDataReader
        If OpenConn() Then
            cmd = conn.CreateCommand()
            sSQL = "SELECT * FROM [Parameters]"
            cmd.CommandText = sSQL
            Try
                rs = cmd.ExecuteReader()
                If rs.Read() Then
                    pStartTime = rs.GetDateTime(0)
                    lblStartTime.Text = "Start Time : " & pStartTime.TimeOfDay.ToString()
                    pEndTime = rs.GetDateTime(1)
                    lblEndTime.Text = "Start Time : " & pEndTime.TimeOfDay.ToString()
                    pRegularRate = rs.GetDouble(2)
                    lblRegRate.Text = "Regular Rate : " & pRegularRate.ToString()
                    pOT1Rate = rs.GetDouble(3)
                    lblOT1.Text = "OT1 Rate : " & pOT1Rate.ToString()
                    pOT2Rate = rs.GetDouble(4)
                    lblOT2.Text = "OT2 Rate : " & pOT2Rate.ToString()
                End If
                rs.Close()
            Catch ex As Exception
                MsgBox("Error loading parameters! Aborting!", vbOKOnly)
                Application.Exit()
            End Try
        Else
            MsgBox("Error loading parameters! Aborting!", vbOKOnly)
            Application.Exit()
        End If
    End Sub

    Friend Sub SaveParameters(sStartTime As DateTime, sEndTime As DateTime, sRegRate As Double, sOT1Rate As Double, sOT2Rate As Double)
        Dim cmd As OleDb.OleDbCommand
        Dim sSQL As String
        If OpenConn() Then
            cmd = conn.CreateCommand()
            sSQL = "UPDATE [Parameters] SET "
            sSQL = sSQL & "StartTime = '" & sStartTime.ToString() & "'"
            sSQL = sSQL & ", EndTime = '" & sEndTime.ToString() & "'"
            sSQL = sSQL & ", RegularRate = " & sRegRate.ToString()
            sSQL = sSQL & ", OT1Rate = " & sOT1Rate.ToString()
            sSQL = sSQL & ", OT2Rate = " & sOT2Rate.ToString()
            cmd.CommandText = sSQL
            If cmd.ExecuteNonQuery() >= 1 Then
                pStartTime = sStartTime
                lblStartTime.Text = pStartTime

                pEndTime = sEndTime
                lblEndTime.Text = pEndTime

                pRegularRate = sRegRate
                lblRegRate.Text = pRegularRate

                pOT1Rate = sOT1Rate
                lblOT1.Text = pOT1Rate

                pOT2Rate = sOT2Rate
                lblOT2.Text = pOT2Rate

                MsgBox("Parameters saved!")
            Else
                MsgBox("Error saving parameters!", vbExclamation)
            End If
        Else
            MsgBox("Error saving parameters!")
        End If
    End Sub

    Private Sub btnParameters_Click(sender As Object, e As EventArgs) Handles btnParameters.Click
        Dim frmParams As New frmParameters
        frmParams.LoadValues(pStartTime, pEndTime, pRegularRate, pOT1Rate, pOT2Rate)
        frmParams.StartPosition = FormStartPosition.CenterParent
        frmParams.ShowDialog()
    End Sub
End Class
