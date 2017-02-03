﻿Imports System.IO.Compression
Imports Archiver.Entities
Imports Microsoft.Win32
Imports System.Text

Public Class Provider
    Public Sub Create(Name As String)
        Me.Root = New Root(Name, Me.RetrieveLocalSignatureKey)
    End Sub
    Public Sub Open(filename As String)
        If (IO.File.Exists(filename)) Then
            If (Not New Header.Reader().IntegrityCheck(Of Root)(filename, Constants.Signature, Me.Root)) Then
                Throw New Exception(String.Format("Unable to fetch header from '{0}'", New IO.FileInfo(filename).Name))
            End If
        End If
    End Sub
    Public Sub AddDirectory(Path As String)
        If (Me.Root Is Nothing) Then
            Me.Root = New Root("root", Me.RetrieveLocalSignatureKey)
        End If
        Me.ScanDirectory(New IO.DirectoryInfo(Path), Me.Root)
    End Sub
    Public Sub AddDirectory(Path As String, Parent As Entity)
        If (Parent IsNot Nothing AndAlso Parent.Type = EntityType.Root Or Parent.Type = EntityType.Directory) Then
            Me.ScanDirectory(New IO.DirectoryInfo(Path), Parent)
        End If
    End Sub
    Public Sub AddFile(Filename As String)
        If (IO.File.Exists(Filename)) Then
            If (Me.Root Is Nothing) Then
                Me.Root = New Root("root", Me.RetrieveLocalSignatureKey)
            End If
            Me.CreateEntity(Filename, EntityType.File, Me.Root)
        End If
    End Sub
    Public Sub AddFile(Filename As String, Parent As Entity)
        If (IO.File.Exists(Filename) AndAlso Parent IsNot Nothing) Then
            If (Parent Is Nothing AndAlso Parent.Type = EntityType.Root Or Parent.Type = EntityType.Directory) Then
                Me.CreateEntity(Filename, EntityType.File, Parent)
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        End If
    End Sub
    Public Sub SaveAs(Filename As String, Optional Overwrite As Boolean = False, Optional Level As CompressionLevel = CompressionLevel.Fastest)
        If (IO.File.Exists(Filename) AndAlso Overwrite) Then
            IO.File.Delete(Filename)
        End If
        If (Me.Root IsNot Nothing AndAlso Not New Header.Writer().Build(Me.Root, Constants.Signature, Filename, Level)) Then
            Throw New Exception(String.Format("Unable to create header for '{0}'", New IO.FileInfo(Filename).Name))
        End If
    End Sub
    Public Sub Extract(Output As String, Target As Entity)

        Target.ExtractTo(Output)

        Select Case Target.Type
            Case EntityType.Root
                CType(Target, Root).Content.ForEach(Sub(entity)
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
    Private Sub ScanDirectory(directory As IO.DirectoryInfo, Parent As Entity)
        For Each x As IO.DirectoryInfo In directory.GetDirectories()
            Try
                Me.ScanDirectory(x, Me.CreateEntity(x.Name, EntityType.Directory, Parent))
            Catch ex As UnauthorizedAccessException
                'No access to folder, skip it
            End Try
        Next
        For Each y As IO.FileInfo In directory.GetFiles()
            Me.CreateEntity(y.FullName, EntityType.File, Parent)
        Next
    End Sub
    Private Function CreateEntity(Name As String, Type As EntityType, Parent As Entity) As Entity
        Dim result As Entity = Nothing
        If (Type = EntityType.Directory) Then
            Dim directory As New Directory(Name, Parent)
            If (Parent.Type = EntityType.Root) Then
                CType(Parent, Root).Content.Add(directory)
                result = directory
            ElseIf (Parent.Type = EntityType.Directory) Then
                CType(Parent, Directory).Content.Add(directory)
                result = directory
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        ElseIf (Type = EntityType.File) Then
            Dim file As New File(Name, Parent)
            If (Parent.Type = EntityType.Directory) Then
                CType(Parent, Directory).Content.Add(file)
                result = file
            ElseIf (Parent.Type = EntityType.Root) Then
                CType(Parent, Root).Content.Add(file)
                result = file
            Else
                Throw New Exception(String.Format("Current entity '{0}' does not accept sub items", Parent.Type))
            End If
        End If
        Return result
    End Function
    Private Function RetrieveLocalSignatureKey() As Byte()
        Return Encoding.ASCII.GetBytes(Registry.GetValue(Constants.ID_HKLM, Constants.ID_KEY, "0000").ToString.Replace("-"c, String.Empty))
    End Function
    Public Property Root As Root
End Class
