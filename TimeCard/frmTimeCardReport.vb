Public Class frmTimeCardReport
    Private Sub frmTimeCardReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oReport As rptCustomerTimeCard
        Dim dbConn As TimeCardDataAccess
        Dim da As Object
        Dim ds As DataSet
        Dim sSQL As String = "SELECT * FROM UserTimeCardReport ORDER BY RecordDate ASC"
        dbConn = frmTimeCardMainForm.dbConn
        If dbConn.GetConnection() Then
            da = dbConn.GetDataAdapter(sSQL)
            If da IsNot Nothing Then
                ds = New DataSet("TimeCards")
                da.Fill(ds, "TimeCardData")
                oReport = New rptCustomerTimeCard
                oReport.SetDataSource(ds.Tables("TimeCardData"))
                oReport.Refresh()
                CrystalReportViewer1.ReportSource = oReport
            End If
        Else
            MsgBox(dbConn.LastError)
        End If
        Try
            dbConn.Connection.Close()
            dbConn.Connection.Dispose()
        Catch ex As Exception
        End Try
        dbConn = Nothing
    End Sub
End Class