Public Class Constants
    Public Shared Signature As UInt32 = BitConverter.ToUInt32(New Byte() {&HBF, &HBF, &H1, &H0}, 0)
    Public Const ID_KEY As String = "ProductId"
    Public Const ID_HKLM As String = "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion"
End Class
