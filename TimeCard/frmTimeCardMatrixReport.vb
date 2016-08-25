Public Class frmTimeCardMatrixReport
    Private Sub frmTimeCardMatrixReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oReport As rptCustomerTimeCardMatrix
        Dim dbConn As New TimeCardDataAccess
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet
        dbConn.DatabaseFile = My.Settings.DBFile
        Try
            If dbConn.GetConnection() Then
                da = New OleDb.OleDbDataAdapter("SELECT * FROM vw_customerProjectTimeCardReport", dbConn.Connection)
                ds = New DataSet("TimeCards")
                da.Fill(ds, "vw_customerProjectTimeCardReport")
                oReport = New rptCustomerTimeCardMatrix
                oReport.SetDataSource(ds.Tables("vw_customerProjectTimeCardReport"))
                oReport.Refresh()
                CrystalReportViewer1.ReportSource = oReport
            Else
                MsgBox(dbConn.LastError)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            dbConn.Connection.Close()
        Catch ex As Exception
        End Try
        dbConn = Nothing
    End Sub
End Class