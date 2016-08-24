﻿Public Class frmReport
    Private Sub rptViewTimeCard_Load(sender As Object, e As EventArgs) Handles rptViewTimeCard.Load
        Dim oReport As rptTimeCard
        Dim dbConn As New TimeCardDataAccess
        Dim da As OleDb.OleDbDataAdapter
        Dim ds As DataSet
        dbConn.DatabaseFile = My.Settings.DBFile
        If dbConn.GetConnection() Then
            da = New OleDb.OleDbDataAdapter("SELECT * FROM UserTimeCardReport ORDER BY RecordDate ASC", dbConn.Connection)
            ds = New DataSet("TimeCards")
            da.FillSchema(ds, SchemaType.Source, "TimeCardData")
            da.Fill(ds, "TimeCardData")


            oReport = New rptTimeCard
            oReport.SetDataSource(ds.Tables("TimeCardData"))
            oReport.Refresh()
            rptViewTimeCard.ReportSource = oReport
        Else
            MsgBox(dbConn.LastError)
        End If
    End Sub
End Class