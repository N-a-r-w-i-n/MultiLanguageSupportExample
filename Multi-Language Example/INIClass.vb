Public Class INIClass

    Private Declare Unicode Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringW" (ByVal lpApplicationName As String, _
ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As System.Text.StringBuilder, ByVal nSize As Int32, ByVal lpFileName As String) As Int32


    Private _iniPath As String
    Public Property iniPath As String
        Get
            Return _iniPath
        End Get
        Set(value As String)
            _iniPath = value
        End Set
    End Property

    Sub New(P As String)
        iniPath = P
    End Sub

    Public Function ReadValueBetween(ByVal Sec As String, Item As String)
        Dim str As New System.Text.StringBuilder(255)
        Dim i As Integer = GetPrivateProfileString(Sec, Item, String.Empty, str, 255, iniPath)
        Return str.ToString()
    End Function
    
End Class
