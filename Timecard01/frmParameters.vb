Public Class frmParameters

    Friend Sub LoadValues(sStartTime As DateTime, sEndTime As DateTime, sRegRate As Double, sOT1 As Double, sOT2 As Double)
        tStartTime.Value = sStartTime
        tEndTime.Value = sEndTime
        txtRegularRate.Text = sRegRate.ToString()
        txtOT1Rate.Text = sOT1.ToString()
        txtOT2Rate.Text = sOT2.ToString()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim dRegRate As Double
        Dim dOT1Rate As Double
        Dim dOT2Rate As Double
        If Not Double.TryParse(txtRegularRate.Text, dRegRate) Then
            dRegRate = 0
        End If
        If Not Double.TryParse(txtOT1Rate.Text, dOT1Rate) Then
            dOT1Rate = 0
        End If
        If Not Double.TryParse(txtOT2Rate.Text, dOT2Rate) Then
            dOT2Rate = 0
        End If
        frmTimeCard.SaveParameters(tStartTime.Value, tEndTime.Value, dRegRate, dOT1Rate, dOT2Rate)
        Me.Close()
    End Sub
End Class