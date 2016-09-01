Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports System.Data.OleDb
Public Class TimeCardDataAccess
    Implements IDisposable
    Private disposed As Boolean = False

    Private _connection As IDbConnection
    Private _connectString As String
    Private _dbFileName As String
    Private _defaultDatabaseFile As String
    Private _lastError As String
    Private _server As String
    Private _port As Integer
    Private _trusted As Boolean
    Private _dbName As String
    Private _userId As String
    Private _password As String
    Private _dbtype As DatabaseType
    Public Enum DatabaseType
        AccessDB = 0
        SQLLocalDB = 1
        MySQL = 2
    End Enum

    Public Sub New()
        _dbtype = DatabaseType.AccessDB
        _defaultDatabaseFile = My.Settings.DBFile
        _dbFileName = My.Settings.DBFile
    End Sub

    Public ReadOnly Property DBType As DatabaseType
        Get
            Return _dbtype
        End Get
    End Property

    Private Sub resetConnection()
        If _connection IsNot Nothing Then
            Try
                _connection.Close()
            Catch ex As Exception
            End Try
            _connection.Dispose()
            _connection = Nothing
        End If
    End Sub

    Public Sub setAccessDB(ByVal dbFile As String)
        'Microsoft.Jet.OLEDB.4.0
        'Microsoft.ACE.OLEDB.12.0
        _dbFileName = dbFile
        _connectString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source=" & _dbFileName & ";"
        _dbtype = DatabaseType.AccessDB
        resetConnection()
    End Sub

    Public Sub setSQLLocalDB(ByVal InstanceName As String, DBName As String, ByVal TrustedConnection As Boolean, Optional ByVal UserId As String = "", Optional ByVal Password As String = "")
        _server = "(localdb)\" & InstanceName
        _dbName = DBName
        _trusted = TrustedConnection
        If Not TrustedConnection Then
            _userId = UserId
            _password = Password
        End If
        _connectString = "Server=" & _server & ";Database=" & _dbName & ";"
        If _trusted Then
            _connectString = _connectString & "Trusted_Connection=yes;"
        Else
            _connectString = _connectString & "Uid=" & _userId & ";Pwd=" & _password & ";"
        End If
        _dbtype = DatabaseType.SQLLocalDB
        resetConnection()
    End Sub

    Public Sub setMySQLDB(ByVal Server As String, ByVal Port As Integer, ByVal DatabaseName As String, ByVal UserId As String, ByVal Password As String)
        _server = Server
        _port = Port
        _dbName = DatabaseName
        _userId = UserId
        _password = Password
        _connectString = "Server=" & _server & ";Port=" & _port & ";Database=" & _dbName & ";UID=" & _userId & ";PWD=" & _password
        _dbtype = DatabaseType.MySQL
        resetConnection()
    End Sub

    Public ReadOnly Property DefaultDatabaseFile As String
        Get
            Return _defaultDatabaseFile
        End Get
    End Property

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

    Public ReadOnly Property Connection As IDbConnection
        Get
            Return _connection
        End Get
    End Property

    Public Function GetConnection() As Boolean
        If _connection Is Nothing Then
            Select Case _dbtype
                Case DatabaseType.AccessDB
                    _connection = New OleDbConnection()
                Case DatabaseType.SQLLocalDB
                    _connection = New SqlConnection()
                Case DatabaseType.MySQL
                    _connection = New MySqlConnection()
            End Select

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

    Public Function GetDataAdapter(SQLString As String) As Object
        Dim da As Object
        Select Case _dbtype
            Case TimeCardDataAccess.DatabaseType.AccessDB
                da = New OleDb.OleDbDataAdapter(SQLString, CType(_connection, OleDbConnection))
            Case TimeCardDataAccess.DatabaseType.SQLLocalDB
                da = New SqlClient.SqlDataAdapter(SQLString, CType(_connection, SqlConnection))
            Case TimeCardDataAccess.DatabaseType.MySQL
                da = New MySql.Data.MySqlClient.MySqlDataAdapter(SQLString, CType(_connection, MySqlConnection))
            Case Else
                da = Nothing
        End Select
        GetDataAdapter = da
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
