Imports System.Runtime.InteropServices

Namespace Hashing
    Public Class Checksums
        ''' <summary>
        ''' The longitudinal redundancy check (LRC) or horizontal redundancy check that is applied independently to each of a parallel group of bit streams
        ''' </summary>
        ''' <returns>UInt16</returns>
        Public Shared Function Xor8(buffer() As Byte) As UInt16
            Dim lrc As UInt16 = 0
            For i As Integer = 0 To buffer.Length - 1
                lrc = Convert.ToUInt16((lrc + buffer(i)) And &HFF)
            Next
            Return Convert.ToUInt16((((lrc Xor &HFF) + 1) And &HFF))
        End Function
        ''' <summary>
        ''' The Adler checksum algorithm was invented by Mark Adler in 1995 and is a modification of the Fletcher checksum.
        ''' </summary>
        ''' <returns>UInt16</returns>
        Public Shared Function Adler16(buffer() As Byte) As UInt16
            Dim a As UInt16 = 0, b As UInt16 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt16((a + buffer(i)) Mod &HFFF1)
                b = Convert.ToUInt16((b + a) Mod &HFFF1)
            Next
            Return (b << 8) Or a
        End Function
        ''' <summary>
        ''' The Adler checksum algorithm was invented by Mark Adler in 1995 and is a modification of the Fletcher checksum.
        ''' </summary>
        ''' <returns>UInt32</returns>
        Public Shared Function Adler32(buffer() As Byte) As UInt32
            Dim a As UInt32 = 0, b As UInt32 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt32((a + buffer(i)) Mod &HFFF1)
                b = Convert.ToUInt32((b + a) Mod &HFFF1)
            Next
            Return (b << 16) Or a
        End Function
        ''' <summary>
        ''' The Adler checksum algorithm was invented by Mark Adler in 1995 and is a modification of the Fletcher checksum.
        ''' </summary>
        ''' <returns>UInt64</returns>
        Public Shared Function Adler64(buffer() As Byte) As UInt64
            Dim a As UInt64 = 0, b As UInt64 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt64((a + buffer(i)) Mod &HFFF1)
                b = Convert.ToUInt64((b + a) Mod &HFFF1)
            Next
            Return (b << 32) Or a
        End Function
        ''' <summary>
        ''' The Fletcher checksum is an algorithm for computing a position-dependent checksum devised by John G. Fletcher
        ''' </summary>
        ''' <returns>UInt16</returns>
        Public Shared Function Fletcher16(buffer() As Byte) As UInt16
            Dim a As UInt16 = 0, b As UInt16 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt16((a + buffer(i)) Mod &HFF)
                b = Convert.ToUInt16((b + a) Mod &HFF)
            Next
            Return (b << 8) Or a
        End Function
        ''' <summary>
        ''' The Fletcher checksum is an algorithm for computing a position-dependent checksum devised by John G. Fletcher
        ''' </summary>
        ''' <returns>UInt23</returns>
        Public Shared Function Fletcher32(buffer() As Byte) As UInt32
            Dim a As UInt32 = 0, b As UInt32 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt32((a + buffer(i)) Mod &HFF)
                b = Convert.ToUInt32((b + a) Mod &HFF)
            Next
            Return (b << 16) Or a
        End Function
        ''' <summary>
        ''' The Fletcher checksum is an algorithm for computing a position-dependent checksum devised by John G. Fletcher
        ''' </summary>
        ''' <returns>UInt64</returns>
        Public Shared Function Fletcher64(buffer() As Byte) As UInt64
            Dim a As UInt64 = 0, b As UInt64 = 0
            For i As Integer = 0 To buffer.Length - 1
                a = Convert.ToUInt64((a + buffer(i)) Mod &HFF)
                b = Convert.ToUInt64((b + a) Mod &HFF)
            Next
            Return (b << 32) Or a
        End Function
        ''' <summary>
        ''' The SYSV checksum algorithm is a commonly used, legacy checksum algorithm. 
        ''' </summary>
        ''' <returns>UInt32</returns>
        Public Shared Function SysV32(buffer() As Byte) As UInt32
            Dim a As UInt32 = 0, b As UInt32 = 0
            Do While b <= buffer.Length - 1
                a = Convert.ToUInt32((a + buffer(Convert.ToInt32(b))) Mod &HFFFF)
                b = Convert.ToUInt32(b + 1)
            Loop
            Return (a << 32) Or a
        End Function
        ''' <summary>
        ''' The BSD checksum algorithm is a commonly used, legacy checksum algorithm
        ''' </summary>
        ''' <returns>UInt32</returns>
        Public Shared Function Bsd32(buffer() As Byte) As UInt32
            Dim sum As UInt32 = 0, count As UInt32 = 0
            Do While count <= buffer.Length - 1
                sum = Convert.ToUInt32((sum >> 1) + &H8000 * (sum And 1))
                sum = Convert.ToUInt32((sum + buffer(Convert.ToInt32(count))) And &HFFFF)
                count = Convert.ToUInt32(count + 1)
            Loop
            Return sum
        End Function
    End Class
End Namespace