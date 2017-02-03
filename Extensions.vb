Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports Archiver.Entities

Module Extensions
    <Extension> Public Function GetSizeReadableForm(byteCount As Integer) As String
        Static Suffix As String() = {"b", "kb", "mb", "gb"}
        If byteCount = 0 Then Return "0" + Suffix(0)
        Dim place As Integer = Convert.ToInt32(Math.Floor(Math.Log(Math.Abs(byteCount), 1024)))
        Return (Math.Sign(byteCount) * Math.Round(Math.Abs(byteCount) / Math.Pow(1024, place), 1)).ToString() + Suffix(place)
    End Function
    <Extension> Public Function MatchGuid(e As Entity, match As String, ByRef Result As List(Of Entity)) As Boolean
        If (e.Guid Like match AndAlso Not Result.Contains(e)) Then
            Result.Add(e)
        End If
        Select Case e.Type
            Case EntityType.Entrypoint
                For Each ent As Entity In CType(e, Entrypoint).Content
                    If (ent.Guid Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.MatchGuid(match, Result)
                Next
            Case EntityType.Directory
                For Each ent As Entity In CType(e, Directory).Content
                    If (ent.Guid Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.MatchGuid(match, Result)
                Next
            Case EntityType.File
                If (e.Guid Like match AndAlso Not Result.Contains(e)) Then
                    Result.Add(e)
                End If
        End Select
        Return Result.Any
    End Function
    <Extension> Public Function MatchName(e As Entity, match As String, ByRef Result As List(Of Entity)) As Boolean
        If (e.Name Like match AndAlso Not Result.Contains(e)) Then
            Result.Add(e)
        End If
        Select Case e.Type
            Case EntityType.Entrypoint
                For Each ent As Entity In CType(e, Entrypoint).Content
                    If (ent.Name Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.MatchName(match, Result)
                Next
            Case EntityType.Directory
                For Each ent As Entity In CType(e, Directory).Content
                    If (ent.Name Like match AndAlso Not Result.Contains(ent)) Then
                        Result.Add(ent)
                    End If
                    ent.MatchName(match, Result)
                Next
            Case EntityType.File
                If (e.Name Like match AndAlso Not Result.Contains(e)) Then
                    Result.Add(e)
                End If
        End Select
        Return Result.Any
    End Function
    <Extension> Public Function TryResolvePath(e As Entity, pathName As String, ByRef result As Entity) As Boolean
        If (pathName.Length > 0) Then
            Dim entities() As String = pathName.NormalizePath.Split("\"c)
            Dim found As Entity = Nothing, nextitem As Entity = Nothing, i As Integer = 0
            found = e
            Do While i <= entities.Count - 1
                If (i = 0 AndAlso found.Name.IsEqualToAndIgnoreCasing(entities(i))) Then
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
    <Extension> Public Function NormalizePath(ByRef path As String) As String
        If (path.StartsWith("\")) Then path = path.Substring(1, path.Length - 1)
        If (path.EndsWith("\")) Then path = path.Substring(0, path.Length - 1)
        Return path
    End Function
    <Extension> Public Function GetPath(e As Entity) As String
        Dim current As Entity = e
        Dim path As New List(Of String)
        While True
            If (current IsNot Nothing) Then
                Select Case current.Type
                    Case EntityType.Entrypoint
                        path.Add(CType(current, Entrypoint).Name)
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
    <Extension> Public Function ContainsEntityWithName(e As Entity, name As String, ByRef result As Entity) As Boolean
        If (e.Type = EntityType.Entrypoint) Then
            For Each ent As Entity In CType(e, Entrypoint).Content
                If (ent.Name.IsEqualToAndIgnoreCasing(name)) Then
                    result = ent
                    Return True
                End If
            Next
        ElseIf (e.Type = EntityType.Directory) Then
            For Each ent As Entity In CType(e, Directory).Content
                If (ent.Name.IsEqualToAndIgnoreCasing(name)) Then
                    result = ent
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    <Extension>
    Public Function IsEqualToAndIgnoreCasing(str As String, match As String) As Boolean
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
