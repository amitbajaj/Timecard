Public Class frmTimeCardReport
    Private Sub frmTimeCardReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oReport As rptCustomerTimeCard
        Dim dbConn As New TimeCardDataAccess
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet
        dbConn.DatabaseFile = My.Settings.DBFile
        If dbConn.GetConnection() Then
            da = New OleDb.OleDbDataAdapter("SELECT * FROM UserTimeCardReport ORDER BY RecordDate ASC", dbConn.Connection)
            ds = New DataSet("TimeCards")
            da.FillSchema(ds, SchemaType.Source, "TimeCardData")
            da.Fill(ds, "TimeCardData")


            oReport = New rptCustomerTimeCard
            oReport.SetDataSource(ds.Tables("TimeCardData"))
            oReport.Refresh()
            CrystalReportViewer1.ReportSource = oReport
        Else
            MsgBox(dbConn.LastError)
        End If
    End Sub
End Class