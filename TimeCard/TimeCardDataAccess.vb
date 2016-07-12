Public Class TimeCardDataAccess
    Private _connection As OleDb.OleDbConnection
    Private _connectString As String
    Private _dbFileName As String
    Private _lastError As String

    Public ReadOnly Property LastError As String
        Get
            Return _lastError
        End Get
    End Property

    Public Property DatabaseFile As String
        Get
            Return _dbFileName
        End Get
        Set(value As String)
            _dbFileName = value
        End Set
    End Property

    Public ReadOnly Property ConnectionString As String
        Get
            Return _connectString
        End Get
    End Property

    Public ReadOnly Property Connection As System.Data.OleDb.OleDbConnection
        Get
            Return _connection
        End Get
    End Property

    Public Function GetConnection() As Boolean
        'Microsoft.Jet.OLEDB.4.0
        'Microsoft.ACE.OLEDB.12.0
        _connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" & _dbFileName & ";"
        If _connection Is Nothing Then
            _connection = New OleDb.OleDbConnection(_connectString)
        Else
            If _connection.State = 1 Then
                GetConnection = True
                Exit Function
            Else
                _connection.Close()
            End If
        End If
        Try
            GetConnection = True
            _connection.Open()
        Catch ex As Exception
            _lastError = ex.Message
            GetConnection = False
        End Try
    End Function
End Class
