Public Class frmTimeCardMatrixReport
    Private Sub frmTimeCardMatrixReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oReport As rptCustomerTimeCardMatrix
        Dim dbConn As New OleDb.OleDbConnection
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet
        dbConn.ConnectionString = "Provider=SQLNCLI11;Server=(localdb)\TimeCard;Database=TimeCard;Trusted_Connection=yes;"
        Try
            dbConn.Open()
            da = New OleDb.OleDbDataAdapter("SELECT * FROM vw_customerProjectTimeCardReport", dbConn)
            ds = New DataSet("TimeCards")
            da.Fill(ds, "vw_customerProjectTimeCardReport")
            oReport = New rptCustomerTimeCardMatrix
            oReport.SetDataSource(ds.Tables("vw_customerProjectTimeCardReport"))
            oReport.Refresh()
            CrystalReportViewer1.ReportSource = oReport
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            dbConn.Close()
        Catch ex As Exception
        End Try
        dbConn = Nothing
    End Sub
End Class