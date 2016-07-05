Public Class frmParametersv2

    Friend Sub LoadValues(sRegHrs As Double, sRegRate As Double, sOT1 As Double, sOT2 As Double)
        txtRegHrs.Text = sRegHrs.ToString()
        txtRegularRate.Text = sRegRate.ToString()
        txtOT1Rate.Text = sOT1.ToString()
        txtOT2Rate.Text = sOT2.ToString()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        Dim dRegRate As Double
        Dim dOT1Rate As Double
        Dim dOT2Rate As Double
        Dim dRegHrs As Double
        If Not Double.TryParse(txtRegHrs.Text, dRegHrs) Then
            dRegHrs = 0
        End If
        If Not Double.TryParse(txtRegularRate.Text, dRegRate) Then
            dRegRate = 0
        End If
        If Not Double.TryParse(txtOT1Rate.Text, dOT1Rate) Then
            dOT1Rate = 0
        End If
        If Not Double.TryParse(txtOT2Rate.Text, dOT2Rate) Then
            dOT2Rate = 0
        End If
        frmTimeCardv2.SaveParameters(dRegHrs, dRegRate, dOT1Rate, dOT2Rate)
        Me.Close()
    End Sub
End Class