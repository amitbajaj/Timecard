Public Class TimeCardDataAccess
    Implements IDisposable


    Private _defaultDatabaseFile As String
    Private disposed As Boolean = False
    Private _connection As OleDb.OleDbConnection
    Private _connectString As String
    Private _dbFileName As String
    Private _lastError As String

    Public ReadOnly Property DefaultDatabaseFile As String
        Get
            Return _defaultDatabaseFile
        End Get
    End Property

    Public Sub New()
        _defaultDatabaseFile = My.Settings.DBFile
    End Sub

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
            _connection = New OleDb.OleDbConnection()
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
            _connection.ConnectionString = _connectString
            _connection.Open()
        Catch ex As Exception
            _lastError = ex.Message
            GetConnection = False
        End Try
    End Function

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposed Then
            If _connection IsNot Nothing Then
                Try
                    _connection.Dispose()
                Catch
                End Try
            End If
            disposed = True
            End If
    End Sub
#Region " IDisposable Support "
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
#End Region

End Class
