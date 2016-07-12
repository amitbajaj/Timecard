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
