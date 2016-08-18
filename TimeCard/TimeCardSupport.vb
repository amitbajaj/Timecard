Public Class TimeCardSupport

    Public Shared NumberOfDecimals As Integer = My.Settings.NumberOfDecimals
    Public Shared NumberFormat As String = My.Settings.NumberFormat
    Public Shared DatabaseFile As String = My.Settings.DBFile

    Public Class GridTotals
        Private _RegHrs As Double
        Private _OT1Hrs As Double
        Private _OT2Hrs As Double
        Private _TotalCost As Double

        Public Sub New()
            _RegHrs = 0
            _OT1Hrs = 0
            _OT2Hrs = 0
            _TotalCost = 0
        End Sub

        Public Sub AddValues(RegularHours As Double, OT1Hours As Double, OT2Hours As Double, TotalCost As Double)
            _RegHrs = _RegHrs + RegularHours
            _OT1Hrs = _OT1Hrs + OT1Hours
            _OT2Hrs = _OT2Hrs + OT2Hours
            _TotalCost = _TotalCost + TotalCost
        End Sub

        Public ReadOnly Property RegularHours As Double
            Get
                Return _RegHrs
            End Get
        End Property

        Public ReadOnly Property OT1Hours As Double
            Get
                Return _OT1Hrs
            End Get
        End Property

        Public ReadOnly Property OT2Hours As Double
            Get
                Return _OT2Hrs
            End Get
        End Property

        Public ReadOnly Property TotalCost As Double
            Get
                Return _TotalCost
            End Get
        End Property

    End Class

    Public Class TimeField
        Private _iHours As Integer
        Private _iMinutes As Integer
        Public Property Hours As Integer
            Set(value As Integer)
                _iHours = value
            End Set
            Get
                Return _iHours
            End Get
        End Property

        Public Property Minutes As Integer
            Set(value As Integer)
                _iMinutes = value
            End Set
            Get
                Return _iMinutes
            End Get
        End Property

        Public Sub SetTime(dTime As Double)
            _iHours = Decimal.Truncate(dTime)
            If _iHours > 0 Then
                _iMinutes = CInt((dTime Mod _iHours) * 60)
            Else
                _iMinutes = CInt(_iHours * 60)
            End If
        End Sub

        Public Sub SetTime(sTime As String)
            Dim aTime() As String
            aTime = sTime.Split(":")
            _iHours = CInt(aTime(0))
            If aTime.Length > 1 Then
                _iMinutes = CInt(aTime(1))
            Else
                _iMinutes = 0
            End If
        End Sub

        Public Function GetTime() As Double
            Return _iHours + (_iMinutes / 60)
        End Function

        Public Function DisplayTime() As String
            Dim sHrs, sMin As String
            If _iHours < 10 Then
                sHrs = "0" & _iHours.ToString()
            Else
                sHrs = _iHours.ToString()
            End If
            If _iMinutes < 10 Then
                sMin = "0" & _iMinutes.ToString()
            Else
                sMin = _iMinutes.ToString()
            End If
            Return sHrs & ":" & sMin
        End Function
    End Class

    Public Class ProjectPhaseDetails
        Private _recordId As Integer
        Private _phaseId As String
        Private _phaseDesc As String

        Public ReadOnly Property DisplayName As String
            Get
                Return _phaseId & " # " & _phaseDesc
            End Get
        End Property

        Public Property RecordId As Integer
            Get
                Return _recordId
            End Get
            Set(value As Integer)
                _recordId = value
            End Set
        End Property

        Public Property PhaseId As String
            Get
                Return _phaseId
            End Get
            Set(value As String)
                _phaseId = value
            End Set
        End Property

        Public Property PhaseDescription As String
            Get
                Return _phaseDesc
            End Get
            Set(value As String)
                _phaseDesc = value
            End Set
        End Property

    End Class

    Public Class ProjectJobDetails
        Private _recordId As Integer
        Private _jobId As String
        Private _jobDesc As String
        Private _jobRate As Double

        Public ReadOnly Property DisplayName As String
            Get
                Return _jobId & " # " & _jobDesc
            End Get
        End Property

        Public Property recordId As Integer
            Get
                Return _recordId
            End Get
            Set(value As Integer)
                _recordId = value
            End Set
        End Property

        Public Property JobId As String
            Get
                Return _jobId
            End Get
            Set(value As String)
                _jobId = value
            End Set
        End Property

        Public Property JobDescription As String
            Get
                Return _jobDesc
            End Get
            Set(value As String)
                _jobDesc = value
            End Set
        End Property

        Public Property JobRate As Double
            Get
                Return _jobRate
            End Get
            Set(value As Double)
                _jobRate = value
            End Set
        End Property
    End Class

    Public Class ProjectDetails
        Private _recordId As Integer
        Private _projectId As String
        Private _projectDesc As String
        Private _projectRate As Double

        Public ReadOnly Property DisplayName As String
            Get
                Return _projectId & " # " & _projectDesc
            End Get
        End Property

        Public Property recordId As Integer
            Get
                Return _recordId
            End Get
            Set(value As Integer)
                _recordId = value
            End Set
        End Property

        Public Property projectId As String
            Get
                Return _projectId
            End Get
            Set(value As String)
                _projectId = value
            End Set

        End Property

        Public Property projectDescription As String
            Get
                Return _projectDesc
            End Get
            Set(value As String)
                _projectDesc = value
            End Set
        End Property

        Public Property projectRate As Double
            Get
                Return _projectRate
            End Get
            Set(value As Double)
                _projectRate = value
            End Set
        End Property
    End Class

    Public Class CustomerDetails
        Private _recordId As Integer
        Private _customerId As String
        Private _customerName As String

        Public ReadOnly Property displayName As String
            Get
                Return _customerId & " # " & _customerName
            End Get
        End Property

        Public Property recordId As Integer
            Set(value As Integer)
                _recordId = value
            End Set
            Get
                Return _recordId
            End Get
        End Property

        Public Property customerId As String
            Get
                Return _customerId
            End Get
            Set(value As String)
                _customerId = value
            End Set
        End Property

        Public Property customerName As String
            Get
                Return _customerName
            End Get
            Set(value As String)
                _customerName = value
            End Set
        End Property

    End Class

    Public Class UserDetails
        Private _recordId As Integer
        Private _userId As Integer
        Private _userName As String
        Private _trade As String
        Private _basic As Double
        Public ReadOnly Property displayName As String
            Get
                Return _userId & " # " & _userName
            End Get
        End Property

        Public Property recordId As Integer
            Get
                Return _recordId
            End Get
            Set(value As Integer)
                _recordId = value
            End Set
        End Property
        Public Property userId As Integer
            Get
                Return _userId
            End Get
            Set(value As Integer)
                _userId = value
            End Set
        End Property
        Public Property userName As String
            Get
                Return _userName
            End Get
            Set(value As String)
                _userName = value
            End Set
        End Property

        Public Property Trade As String
            Get
                Return _trade
            End Get
            Set(value As String)
                _trade = value
            End Set
        End Property

        Public Property Basic As Double
            Get
                Return _basic
            End Get
            Set(value As Double)
                _basic = value
            End Set
        End Property
    End Class

    Public Class TimeCardMasterDetails
        Private _recordId As Integer
        Private _userId As Integer
        Private _timeCardNumber As Integer
        Private _timeCardMonth As Short
        Private _timeCardYear As Short

        Public Property RecordId As Integer
            Get
                Return _recordId
            End Get
            Set(value As Integer)
                _recordId = value
            End Set
        End Property

        Public Property UserId As Integer
            Get
                Return _userId
            End Get
            Set(value As Integer)
                _userId = value
            End Set
        End Property

        Public Property TimeCardNumber As Integer
            Get
                Return _timeCardNumber
            End Get
            Set(value As Integer)
                _timeCardNumber = value
            End Set
        End Property

        Public Property TimeCardMonth As Short
            Get
                Return _timeCardMonth
            End Get
            Set(value As Short)
                _timeCardMonth = value
            End Set
        End Property

        Public Property TimeCardYear As Short
            Get
                Return _timeCardYear
            End Get
            Set(value As Short)
                _timeCardYear = value
            End Set
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return "TimeCard : " & _timeCardNumber & " # Year : " & _timeCardYear & " # Month : " & _timeCardMonth
            End Get
        End Property
    End Class

    Public Class EntityCategory
        Private _categoryId As Integer
        Private _categoryName As String
        Public Property CategoryId As Integer
            Get
                Return _categoryId
            End Get
            Set(value As Integer)
                _categoryId = value
            End Set
        End Property

        Public Property CategoryName As String
            Get
                Return _categoryName
            End Get
            Set(value As String)
                _categoryName = value
            End Set
        End Property

    End Class

    Private Class MonthClass
        Private _monthNumber As Short
        Private _monthName As String
        Public Property MonthName As String
            Get
                Return _monthName
            End Get
            Set(value As String)
                _monthName = value
            End Set
        End Property

        Public Property MonthNumber As Short
            Get
                Return _monthNumber
            End Get
            Set(value As Short)
                _monthNumber = value
            End Set
        End Property

        Public ReadOnly Property DisplayName As String
            Get
                Return _monthNumber & " - " & _monthName
            End Get
        End Property
    End Class

    Public Shared Function ValidateCell(cboTimeCards As ComboBox, DGVTimeCardDetails As DataGridView, iColumnIndex As Integer, iRowIndex As Integer, sValue As String) As Boolean
        Dim dDate As Date
        Dim iDay As Integer
        Dim currentMonth As Integer
        Dim currentYear As Integer
        Dim rw As DataGridViewRow
        If cboTimeCards.SelectedItem IsNot Nothing Then
            currentMonth = cboTimeCards.SelectedItem.TimeCardMonth
            currentYear = cboTimeCards.SelectedItem.TimeCardYear
            ValidateCell = True
            If DGVTimeCardDetails.Columns(iColumnIndex).Name = "logDay" Then
                If sValue <> "" Then
                    If Not Integer.TryParse(sValue, iDay) Then
                        MsgBox("Enter a valid day!")
                        ValidateCell = False
                        Exit Function
                    ElseIf Not ValidateDate(iDay, currentMonth, currentYear) Then
                        MsgBox("Enter a valid day!")
                        ValidateCell = False
                        Exit Function
                    End If
                    For Each rw In DGVTimeCardDetails.Rows
                        If rw.Cells("logDay").Value = iDay And rw.Index <> iRowIndex Then
                            MsgBox("Duplicate day for the month!")
                            ValidateCell = False
                            Exit Function
                        End If
                    Next
                End If
            End If
            If DGVTimeCardDetails.Columns(iColumnIndex).Name = "inTime" Or DGVTimeCardDetails.Columns(iColumnIndex).Name = "outTime" Then
                If sValue <> "" Then
                    If Not Date.TryParse(sValue, dDate) Then
                        MsgBox("Enter a valid time!")
                        ValidateCell = False
                        Exit Function
                    End If
                End If
            End If
        Else
            ValidateCell = True
        End If
    End Function

    Private Shared Function ValidateDate(iDay As Integer, iMonth As Integer, iYear As Integer) As Boolean
        Dim days_in_month(13) As Integer
        ValidateDate = True
        days_in_month = {0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        If isLeapYear(iYear) Then
            days_in_month(2) = 29
        End If
        If iMonth < 1 Or iMonth > 12 Then
            ValidateDate = False
        ElseIf iDay < 1 Or iDay > days_in_month(iMonth) Then
            ValidateDate = False
        End If
    End Function

    Private Shared Function isLeapYear(iYear As Integer) As Boolean
        If iYear Mod 4 <> 0 Then
            isLeapYear = False
        ElseIf iYear Mod 400 = 0 Then
            isLeapYear = True
        ElseIf iYear Mod 100 = 0 Then
            isLeapYear = False
        Else
            isLeapYear = True
        End If
    End Function

    Public Shared Sub CalculateCost(dBasic As Double, rw As DataGridViewRow, iDecimals As Integer, sNumberFormat As String, inTime As Date, outTime As Date, isHoliday As Boolean, isAbsent As Boolean)
        Dim dReg, dOT1, dOT2, dTotal As Double
        Dim oTime As TimeCardSupport.TimeField
        dReg = 0
        dOT1 = 0
        dOT2 = 0
        dBasic = (dBasic / 30) / 8
        If Not isAbsent Then
            dReg = outTime.Subtract(inTime).TotalHours
            If isHoliday Then
                dOT2 = dReg
                dReg = 0
                dOT1 = 0
            ElseIf dReg > 8 Then
                dOT1 = dReg - 8
                dReg = 8
            End If
        End If
        dTotal = (dReg * dBasic) + (dOT1 * dBasic * 1.25) + (dOT2 * dBasic * 1.5)
        oTime = New TimeField
        oTime.SetTime(dReg)
        rw.Cells("regularHrs").Value = oTime.DisplayTime()
        oTime.SetTime(dOT1)
        rw.Cells("OT1").Value = oTime.DisplayTime()
        oTime.SetTime(dOT2)
        rw.Cells("OT2").Value = oTime.DisplayTime()
        rw.Cells("totalCost").Value = Math.Round(dTotal, iDecimals).ToString(sNumberFormat)
    End Sub

    Public Shared Function GridTotal(DGVTimeCardDetails As DataGridView) As GridTotals
        Dim oGridTotal As New GridTotals
        Dim rCalc As DataGridViewRow
        Dim dReg, dOT1, dOT2, dTotal As Double
        Dim oTime As New TimeField
        dReg = 0
        dOT1 = 0
        dOT2 = 0
        dTotal = 0
        For Each rCalc In DGVTimeCardDetails.Rows

            If rCalc.Cells("regularHrs").FormattedValue = "" Then
                dReg = 0
            Else
                oTime.SetTime(0)
                oTime.SetTime(rCalc.Cells("regularHrs").FormattedValue)
                dReg = oTime.GetTime()
            End If
            If rCalc.Cells("OT1").FormattedValue = "" Then
                dOT1 = 0
            Else
                oTime.SetTime(0)
                oTime.SetTime(rCalc.Cells("OT1").FormattedValue)
                dOT1 = oTime.GetTime()
            End If

            If rCalc.Cells("OT2").FormattedValue = "" Then
                dOT2 = 0
            Else
                oTime.SetTime(0)
                oTime.SetTime(rCalc.Cells("OT2").FormattedValue)
                dOT2 = oTime.GetTime()
            End If

            If rCalc.Cells("totalCost").Value IsNot Nothing Then
                If Not Double.TryParse(rCalc.Cells("totalCost").Value, dTotal) Then
                    dTotal = 0
                End If
            End If
            oGridTotal.AddValues(dReg, dOT1, dOT2, dTotal)
        Next
        GridTotal = oGridTotal
    End Function

    Public Shared Function DisplayDays(dValue As Double) As String
        Dim dDays As Double
        Dim iDays As Integer
        Dim iHrs As Integer
        dDays = dValue / 8
        iDays = Decimal.Truncate(dDays)
        iHrs = CInt(dValue Mod 8)
        Return iDays & " days, " & iHrs & " hours"
    End Function

End Class
