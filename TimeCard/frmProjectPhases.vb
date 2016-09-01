Public Class frmProjectPhases
    Private dbConnection As TimeCardDataAccess
    Private Sub frmProjectPhases_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With DGVPhases
            .AddColumn("recordId", "Record Id", True, "ss")
            .AddColumn("phaseId", "Phase Id", False, "ss")
            .AddColumn("phaseDesc", "Phase Description", False, "ss")
            .FinalizeGridColumns()
        End With
        dbConnection = frmTimeCardMainForm.dbConn
        LoadCustomers()
    End Sub

    Sub LoadCustomers()
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oCust As TimeCardSupport.CustomerDetails
        cboCustomers.Items.Clear()
        cboCustomers.DisplayMember = "displayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT recordId, CustomerId, CustomerName FROM customerMaster"
            dr = cmd.ExecuteReader()
            While dr.Read()
                oCust = New TimeCardSupport.CustomerDetails
                oCust.recordId = dr.GetValue(0)
                oCust.customerId = dr.GetValue(1)
                oCust.customerName = dr.GetValue(2)
                cboCustomers.Items.Add(oCust)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        End If
    End Sub

    Private Sub cboCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCustomers.SelectedIndexChanged
        DGVPhases.DGVData.Rows.Clear()
        If cboCustomers.SelectedIndex >= 0 Then
            LoadCustomerProjects(cboCustomers.SelectedItem)
        End If
    End Sub

    Sub LoadCustomerProjects(oCust As TimeCardSupport.CustomerDetails)
        Dim cmd As IDbCommand
        Dim dr As IDataReader
        Dim oParam As IDataParameter
        Dim oProj As TimeCardSupport.ProjectDetails
        cboProjects.Items.Clear()
        cboProjects.DisplayMember = "DisplayName"
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            cmd.CommandText = "SELECT RecordId, ProjectId, ProjectDesc, ProjectRate FROM customerProjects WHERE ParentId = @ParentId"
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oCust.recordId
            End With
            cmd.Parameters.Add(oParam)
            oParam = Nothing

            dr = cmd.ExecuteReader()
            While dr.Read()
                oProj = New TimeCardSupport.ProjectDetails
                oProj.recordId = dr.GetValue(0)
                oProj.projectId = dr.GetValue(1)
                oProj.projectDescription = dr.GetValue(2)
                oProj.projectRate = dr.GetValue(3)
                cboProjects.Items.Add(oProj)
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
        End If
    End Sub

    Private Sub cboProjects_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProjects.SelectedIndexChanged
        DGVPhases.DGVData.Rows.Clear()
        If cboProjects.SelectedIndex >= 0 Then
            LoadProjectPhases(cboProjects.SelectedItem)
        End If
    End Sub

    Private Sub LoadProjectPhases(oProj As TimeCardSupport.ProjectDetails)
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim dr As IDataReader
        Dim iNewRow As Integer
        Dim oRow As DataGridViewRow
        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            oParam = cmd.CreateParameter()
            With oParam
                .ParameterName = "@ParentId"
                .DbType = DbType.Int32
                .Value = oProj.recordId
            End With
            cmd.Parameters.Add(oParam)
            cmd.CommandText = "SELECT RecordId, PhaseId, PhaseDesc FROM customerProjectPhases WHERE ParentId = @ParentId"
            dr = cmd.ExecuteReader()
            While dr.Read()
                With DGVPhases.DGVData
                    iNewRow = .Rows.Add()
                    oRow = .Rows(iNewRow)
                    If dr.IsDBNull(0) Then
                        oRow.Cells("recordId").Value = Nothing
                    Else
                        oRow.Cells("recordId").Value = dr.GetValue(0)
                    End If

                    If dr.IsDBNull(1) Then
                        oRow.Cells("phaseId").Value = Nothing
                    Else
                        oRow.Cells("phaseId").Value = dr.GetValue(1)
                    End If

                    If dr.IsDBNull(2) Then
                        oRow.Cells("phaseDesc").Value = Nothing
                    Else
                        oRow.Cells("phaseDesc").Value = dr.GetValue(2)
                    End If

                End With
            End While
            dr.Close()
            cmd.Dispose()
            dbConnection.Connection.Close()
            dbConnection.Connection.Dispose()
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        If DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue <> "" Then
            If dbConnection.GetConnection() Then
                cmd = dbConnection.Connection.CreateCommand()
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@recordId"
                    .DbType = DbType.Int32
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
                cmd.CommandText = "DELETE FROM customerProjectPhases WHERE RecordId = @recordId"
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
        Dim cmd As IDbCommand
        Dim oParam As IDataParameter
        Dim sSQL As String
        Dim bNewRec As Boolean
        Dim iRowIndex As Integer
        iRowIndex = e.RowIndex
        If DGVPhases.DGVData.Rows(iRowIndex).Cells("recordId").FormattedValue = "" Then
            sSQL = "INSERT INTO customerProjectPhases(ParentId, PhaseId, PhaseDesc) VALUES(@ParentId, @PhaseId, @PhaseDesc);"
            bNewRec = True
        Else
            sSQL = "UPDATE customerProjectPhases SET PhaseId = @PhaseId, PhaseDesc = @PhaseDesc WHERE RecordId = @RecordId;"
            bNewRec = False
        End If

        If dbConnection.GetConnection() Then
            cmd = dbConnection.Connection.CreateCommand()
            If bNewRec Then
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@ParentId"
                    .DbType = DbType.Int32
                    .Value = cboProjects.SelectedItem.RecordId
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseId"
                    .DbType = DbType.String
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseDesc"
                    .DbType = DbType.String
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseDesc").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing
            Else

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseId"
                    .DbType = DbType.String
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseId").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@PhaseDesc"
                    .DbType = DbType.String
                    .Value = DGVPhases.DGVData.Rows(iRowIndex).Cells("phaseDesc").FormattedValue
                End With
                cmd.Parameters.Add(oParam)
                oParam = Nothing

                oParam = cmd.CreateParameter()
                With oParam
                    .ParameterName = "@RecordId"
                    .DbType = DbType.Int32
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