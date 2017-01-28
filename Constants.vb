Public Class Constants
    Public Shared Signature As UInt32 = BitConverter.ToUInt32(New Byte() {&HBF, &HBF, &H1, &H0}, 0)
End Class
