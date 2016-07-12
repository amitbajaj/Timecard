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
