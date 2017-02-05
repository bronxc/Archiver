Imports Archiver.Entities
Public Class Provider
    Inherits Catcher
    Public Property Entrypoint As Entrypoint
    Public Event Status(Message As String)
    Public Event ProgressionRead(Position As Int64, Total As Int64)
    Public Event ProgressionWrite(Position As Int64, Total As Int64)
    Public Sub Create(Name As String)
        Me.Entrypoint = New Entrypoint(Name)
    End Sub
    Public Sub Open(filename As String)
        If (IO.File.Exists(filename)) Then
            If (Not New Header.Reader().IntegrityCheck(Of Entrypoint)(filename, Constants.Signature, Me.Entrypoint, AddressOf Me.Decompression)) Then
                Throw New Exception(String.Format("Unable to fetch header from '{0}'", New IO.FileInfo(filename).Name))
            End If
        End If
    End Sub
    Public Sub AddDirectory(Path As String)
        If (Me.Entrypoint Is Nothing) Then Me.Create("Entrypoint")
        Me.ScanDirectory(New IO.DirectoryInfo(Path), Me.Entrypoint)
    End Sub
    Public Sub AddDirectory(Path As String, Parent As Entity)
        If (Parent IsNot Nothing AndAlso Parent.Type = EntityType.Entrypoint Or Parent.Type = EntityType.Directory) Then
            Me.ScanDirectory(New IO.DirectoryInfo(Path), Parent)
        End If
    End Sub
    Public Sub AddFile(Filename As String)
        If (IO.File.Exists(Filename)) Then
            If (Me.Entrypoint Is Nothing) Then Me.Create("Entrypoint")
            Me.CreateEntity(Filename, EntityType.File, Me.Entrypoint)
        End If
    End Sub
    Public Sub AddFile(Filename As String, Parent As Entity)
        If (IO.File.Exists(Filename) AndAlso Parent IsNot Nothing) Then
            If (Parent Is Nothing AndAlso Parent.Type = EntityType.Entrypoint Or Parent.Type = EntityType.Directory) Then
                Me.CreateEntity(Filename, EntityType.File, Parent)
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        End If
    End Sub
    Public Sub SaveAs(Filename As String, Optional Overwrite As Boolean = False)
        If (IO.File.Exists(Filename) AndAlso Overwrite) Then
            IO.File.Delete(Filename)
        End If
        If (Me.Entrypoint IsNot Nothing AndAlso Not New Header.Writer().Build(Me.Entrypoint, Constants.Signature, Filename, AddressOf Me.Compression)) Then
            Throw New Exception(String.Format("Unable to create header for '{0}'", New IO.FileInfo(Filename).Name))
        End If
    End Sub
    Public Sub Extract(Output As String, Target As Entity)

        Target.ExtractTo(Output)

        Select Case Target.Type
            Case EntityType.Entrypoint
                CType(Target, Entrypoint).Content.ForEach(Sub(entity)
                                                              entity.ExtractTo(Output)
                                                              Me.Extract(Output, entity)
                                                          End Sub)
            Case EntityType.Directory
                CType(Target, Directory).Content.ForEach(Sub(entity)
                                                             entity.ExtractTo(Output)
                                                             Me.Extract(Output, entity)
                                                         End Sub)
        End Select
    End Sub
    Private Sub Compression(n As Int64, t As Int64)
        RaiseEvent ProgressionRead(n, t)
        If (n = 0 AndAlso t = 0) Then
            RaiseEvent Status("Ready")
        Else
            RaiseEvent Status(String.Format("Packing... {0}%", Math.Floor((n / t) * 100)))
        End If
    End Sub
    Private Sub Decompression(n As Int64, t As Int64)
        RaiseEvent ProgressionRead(n, t)
        If (n = 0 AndAlso t = 0) Then
            RaiseEvent Status("Ready")
        Else
            RaiseEvent Status(String.Format("Unpacking... {0}%", Math.Floor((n / t) * 100)))
        End If
    End Sub
    Private Sub ScanDirectory(directory As IO.DirectoryInfo, Parent As Entity)
        For Each x As IO.DirectoryInfo In directory.GetDirectories()
            Try
                RaiseEvent Status(String.Format("Looking ... {0}", x.Name))
                Me.ScanDirectory(x, Me.CreateEntity(x.Name, EntityType.Directory, Parent))
            Catch ex As UnauthorizedAccessException
            End Try
        Next
        For Each y As IO.FileInfo In directory.GetFiles()
            Me.CreateEntity(y.FullName, EntityType.File, Parent)
        Next
        RaiseEvent Status("Ready")
    End Sub
    Private Function CreateEntity(Name As String, Type As EntityType, Parent As Entity) As Entity
        Dim result As Entity = Nothing
        If (Type = EntityType.Directory) Then
            Dim directory As New Directory(Name, Parent)
            If (Parent.Type = EntityType.Entrypoint) Then
                CType(Parent, Entrypoint).Content.Add(directory)
                result = directory
            ElseIf (Parent.Type = EntityType.Directory) Then
                CType(Parent, Directory).Content.Add(directory)
                result = directory
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        ElseIf (Type = EntityType.File) Then
            Dim file As Entities.Entity = Nothing
            file = New File(Name, Parent)
            If (Parent.Type = EntityType.Directory) Then
                CType(Parent, Directory).Content.Add(file)
                result = file
            ElseIf (Parent.Type = EntityType.Entrypoint) Then
                CType(Parent, Entrypoint).Content.Add(file)
                result = file
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        End If
        Return result
    End Function
    Public Overrides Sub CatchException(ex As Exception)
        If (IO.File.Exists(Constants.ErrorLog)) Then IO.File.Delete(Constants.ErrorLog)
        Using bw As New IO.BinaryWriter(New IO.FileStream(Constants.ErrorLog, IO.FileMode.CreateNew, IO.FileAccess.Write, IO.FileShare.Write))
            bw.Write(ex.ToString)
            bw.Flush()
        End Using
    End Sub
End Class
