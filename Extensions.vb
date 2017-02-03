Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports Archiver.Entities

Module Extensions

    <Extension>
    Public Sub FindGuidLike(e As Entity, match As String, ByRef Result As List(Of Entity))
        If (e.Guid Like match AndAlso Not Result.Contains(e)) Then
            Result.Add(e)
        End If
        Select Case e.Type
            Case EntityType.Root
                For Each ent As Entity In CType(e, Root).Content
                    If (ent.Guid Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.FindGuidLike(match, Result)
                Next
            Case EntityType.Directory
                For Each ent As Entity In CType(e, Directory).Content
                    If (ent.Guid Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.FindGuidLike(match, Result)
                Next
            Case EntityType.File
                If (e.Guid Like match AndAlso Not Result.Contains(e)) Then
                    Result.Add(e)
                End If
        End Select
    End Sub
    <Extension>
    Public Sub FindNameLike(e As Entity, match As String, ByRef Result As List(Of Entity))
        If (e.Name Like match AndAlso Not Result.Contains(e)) Then
            Result.Add(e)
        End If
        Select Case e.Type
            Case EntityType.Root
                For Each ent As Entity In CType(e, Root).Content
                    If (ent.Name Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.FindNameLike(match, Result)
                Next
            Case EntityType.Directory
                For Each ent As Entity In CType(e, Directory).Content
                    If (ent.Name Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.FindNameLike(match, Result)
                Next
            Case EntityType.File
                If (e.Name Like match AndAlso Not Result.Contains(e)) Then
                    Result.Add(e)
                End If
        End Select
    End Sub
    <Extension>
    Public Function TryResolvePath(e As Entity, path As String, ByRef result As Entity) As Boolean
        If (path.Length > 0) Then
            path = path.NormalizePath
            Dim entities() As String = path.Split("\"c)
            Dim found As Entity = Nothing, nextitem As Entity = Nothing, i As Integer = 0
            found = e
            Do While i <= entities.Count - 1
                If (i = 0 AndAlso found.Name.IsEqualToAndIgnoreCaseSensitive(entities(i))) Then
                    i += 1
                ElseIf (found.ContainsEntityWithName(entities(i), nextitem)) Then
                    i += 1
                    found = nextitem
                Else
                    Return False
                End If
            Loop
            result = found
            Return True
        End If
        Return False
    End Function
    <Extension>
    Public Function NormalizePath(ByRef path As String) As String
        If (path.StartsWith("\")) Then path = path.Substring(1, path.Length - 1)
        If (path.EndsWith("\")) Then path = path.Substring(0, path.Length - 1)
        Return path
    End Function
    <Extension>
    Public Function GetPath(e As Entity) As String
        Dim current As Entity = e
        Dim path As New List(Of String)
        While True
            If (current IsNot Nothing) Then
                Select Case current.Type
                    Case EntityType.Root
                        path.Add(CType(current, Root).Name)
                        Exit While
                    Case EntityType.Directory
                        path.Add(CType(current, Directory).Name)
                        current = CType(current, Directory).Parent
                    Case EntityType.File
                        path.Add(CType(current, File).Name)
                        current = CType(current, File).Parent
                End Select
            End If
        End While
        Return String.Join("\", path.ToArray.Reverse)
    End Function
    <Extension>
    Public Function ContainsEntityWithName(e As Entity, name As String, ByRef result As Entity) As Boolean
        If (e.Type = EntityType.Root) Then
            For Each ent As Entity In CType(e, Root).Content
                If (ent.Name.IsEqualToAndIgnoreCaseSensitive(name)) Then
                    result = ent
                    Return True
                End If
            Next
        ElseIf (e.Type = EntityType.Directory) Then
            For Each ent As Entity In CType(e, Directory).Content
                If (ent.Name.IsEqualToAndIgnoreCaseSensitive(name)) Then
                    result = ent
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    <Extension>
    Public Function IsEqualToAndIgnoreCaseSensitive(str As String, match As String) As Boolean
        Return str.Equals(match, StringComparison.OrdinalIgnoreCase)
    End Function
    <Extension>
    Public Function ToHexString(src() As Byte) As String
        Return String.Join(String.Empty, Array.ConvertAll(src, Function(b) b.ToString("X2").ToLower))
    End Function
    <Extension>
    Public Function Randomize(ByRef src() As Byte) As Byte()
        Using rng As New Security.Cryptography.RNGCryptoServiceProvider()
            rng.GetNonZeroBytes(src)
            Return src
        End Using
    End Function
    <Extension>
    Public Function ToCrc32(src() As Byte) As UInt32
        Dim offset As UInt32 = 0
        Dim crc As UInt32 = &HFFFFFFFFUI
        Dim values() As UInt32 = New UInt32(255) {}
        For x As UInt32 = 0 To Convert.ToUInt32(values.Length - 1)
            offset = x
            For y As Integer = 8 To 1 Step -1
                If (offset And 1) = 1 Then
                    offset = CUInt((offset >> 1) Xor &HEDB88320UI)
                Else
                    offset >>= 1
                End If
            Next
            values(Convert.ToInt32(x)) = offset
        Next
        For i As Integer = 0 To src.Length - 1
            Dim index As Byte = CByte(((crc) And &HFF) Xor src(i))
            crc = CUInt((crc >> 8) Xor values(index))
        Next
        Return Not crc
    End Function
    <Extension>
    Public Function Compress(src() As Byte, Optional CompressionLevel As CompressionLevel = CompressionLevel.Fastest) _
        As Byte()
        Using ms As New IO.MemoryStream
            Using gzs As New DeflateStream(ms, CompressionLevel, True)
                gzs.Write(src, 0, src.Length)
            End Using
            Return ms.ToArray
        End Using
    End Function
    <Extension>
    Public Function Decompress(src() As Byte) As Byte()
        Using gzs As New DeflateStream(New IO.MemoryStream(src), CompressionMode.Decompress)
            Dim length As Integer = 0, buffer As Byte() = New Byte(&H400) {}
            Using ms As New IO.MemoryStream
                Do
                    length = gzs.Read(buffer, 0, &H400)
                    If length > 0 Then
                        ms.Write(buffer, 0, length)
                    End If
                Loop While length > 0
                Return ms.ToArray()
            End Using
        End Using
    End Function
End Module
