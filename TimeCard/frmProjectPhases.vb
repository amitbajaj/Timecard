Public Class frmProjectPhases
    Private dbConnection As TimeCardDataAccess
    Private Sub frmProjectPhases_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DGVPhases
            .AddColumn("recordId", "Record Id", True, "ss")
            .AddColumn("phaseId", "Phase Id", False, "ss")
            .AddColumn("phaseDesc", "Phase Description", False, "ss")
            .FinalizeGridColumns()
        End With
        dbConnection = New TimeCardDataAccess()
        dbConnection.DatabaseFile = dbConnection.DefaultDatabaseFile
        LoadCustomers()
    End Sub

    Sub LoadCustomers()
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oCust As TimeCardSupport.CustomerDetails
        cboCustomers.Items.Clear()
        cboCustomers.DisplayMember = "displayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT recordId, CustomerId, CustomerName FROM CustomerMaster"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oCust = New TimeCardSupport.CustomerDetails
                oCust.recordId = dr.GetInt32(0)
                oCust.customerId = dr.GetString(1)
                oCust.customerName = dr.GetString(2)
                cboCustomers.Items.Add(oCust)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        DGVPhases.DGVData.Rows.Clear()
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Dim oParam As OleDb.OleDbParameter
        Dim oProj As TimeCardSupport.ProjectDetails
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM CustomerProjects WHERE ParentId = ?"
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "ParentId"
                .OleDbType = OleDb.OleDbType.Numeric
                .SourceColumn = "ParentId"
                .Direction = ParameterDirection.Input
                .Value = oCust.recordId
            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing


            dr = cmd.ExecuteReader()
            While dr.Read()
                oProj = New TimeCardSupport.ProjectDetails
                oProj.recordId = dr.GetInt32(0)
                oProj.projectId = dr.GetString(1)
                oProj.projectDescription = dr.GetString(2)
                oProj.projectRate = dr.GetDouble(3)
                cboProjects.Items.Add(oProj)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
        End If
    End Sub

    Private Sub cboProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjects.SelectedIndexChanged
        DGVPhases.DGVData.Rows.Clear()
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectPhases(cboProjects.SelectedItem)
        End If
    End Sub

    Private Sub LoadProjectPhases(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As OleDb.OleDbCommand
        Dim oParam As OleDb.OleDbParameter
        Dim dr As OleDb.OleDbDataReader
        Dim iNewRow As Integer
        Dim oRow As DataGridViewRow
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .OleDbType = OleDb.OleDbType.Integer
                .Value = oProj.recordId
            End With
            cmd.Parameters.Add(oParam)
            cmd.CommandText = "SELECT RecordId, PhaseId, PhaseDesc FROM ProjectPhases WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                With DGVPhases.DGVData
                    iNewRow = .Rows.Add()
                    oRow = .Rows(iNewRow)
                    If dr.IsDBNull(0) Then
                        oRow.Cells("recordId").Value = Nothing
                    Else
                        oRow.Cells("recordId").Value = dr.GetInt32(0)
                    End If

                    If dr.IsDBNull(1) Then
                        oRow.Cells("phaseId").Value = Nothing
                    Else
                        oRow.Cells("phaseId").Value = dr.GetString(1)
                    End If

                    If dr.IsDBNull(2) Then
                        oRow.Cells("phaseDesc").Value = Nothing
                    Else
                        oRow.Cells("phaseDesc").Value = dr.GetString(2)
                    End If

                End With
            End While
            If DGVPhases.DGVData.RowCount = 0 Then
                DGVPhases.DGVData.Rows.Add()
            End If
        End If
    End Sub

    Private Sub DGVPhases_RemoveRow(sender As Object, e As EventArgs) Handles DGVPhases.RemoveRow
        RemoveRow(sender.currentRow.Index)
    End Sub

    Private Sub RemoveRow(iRowIndex As Integer)
        Dim bRetVal As Boolean = True
        Dim cmd As OleDb.OleDbCommand
        Dim oParam As OleDb.OleDbParameter
        If DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue <> "" Then
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@recordId"
                    .OleDbType = OleDb.OleDbType.Integer
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
                cmd.CommandText = "DELETE FROM ProjectPhases WHERE RecordId = @recordId"
                cmd.ExecuteNonQuery()
                cmd.Dispose()
                dbConnection.Connection.Close()
                dbConnection.Connection.Dispose()
            Else
                MsgBox(dbConnection.LastError)
                bRetVal = False
            End If
        End If
        If bRetVal Then
            DGVPhases.DGVData.Rows.RemoveAt(iRowIndex)
            If DGVPhases.DGVData.RowCount = 0 Then
                DGVPhases.DGVData.Rows.Add()
            End If
        End If
    End Sub

    Private Sub DGVPhases_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DGVPhases.CellEndEdit
        Dim cmd As OleDb.OleDbCommand
        Dim oParam As OleDb.OleDbParameter
        Dim sSQL As String
        Dim bNewRec As Boolean
        Dim iRowIndex As Integer
        iRowIndex = e.RowIndex
        If DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO ProjectPhases(ParentId, PhaseId, PhaseDesc) VALUES(@ParentId, @PhaseId, @PhaseDesc);"
            bNewRec = True
        Else
            sSQL = "UPDATE ProjectPhases SET PhaseId = @PhaseId, PhaseDesc = @PhaseDesc WHERE RecordId = @RecordId;"
            bNewRec = False
        End If

        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            If bNewRec Then
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@ParentId"
                    .OleDbType = OleDb.OleDbType.Integer
                    .Value = cboProjects.SelectedItem.RecordId
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseId"
                    .OleDbType = OleDb.OleDbType.VarChar
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseDesc"
                    .OleDbType = OleDb.OleDbType.VarChar
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseDesc").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
            Else

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseId"
                    .OleDbType = OleDb.OleDbType.VarChar
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseDesc"
                    .OleDbType = OleDb.OleDbType.VarChar
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseDesc").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .OleDbType = OleDb.OleDbType.Integer
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
            End If
            cmd.CommandText = sSQL
            cmd.ExecuteNonQuery()
            If bNewRec Then
                cmd.CommandText = "SELECT @@Identity"
                DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").Value = cmd.ExecuteScalar()
            End If
            cmd.Dispose()
            dbConnection.Connection.Close()
        Else
            MsgBox(dbConnection.LastError)
        End If
    End Sub

End Class