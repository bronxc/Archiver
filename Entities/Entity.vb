﻿Namespace Entities
    <Serializable> Public MustInherit Class Entity
        Sub New(Name As String, Type As EntityType)
            Me.Name = Name
            Me.Type = Type
            Me.Guid = New Byte(3) {}.Randomize.ToHexString
            Me.Created = DateTime.Now
        End Sub
        Public Function FindByGuid(match As String, ByRef result As List(Of Entity)) As Boolean
            Return Me.MatchGuid(match, result)
        End Function
        Public Function FindByName(match As String, ByRef result As List(Of Entity)) As Boolean
            Return Me.MatchName(match, result)
        End Function
        Public Function TryResolve(pathName As String, ByRef result As Entity) As Boolean
            Return Me.TryResolvePath(pathName, result)
        End Function
        Public Function Extract(pathName As String, Optional Overwrite As Boolean = False) As Boolean
            Try
                pathName = String.Format("{0}\{1}", pathName, Me.GetPath).Replace("\Entrypoint", String.Empty)
                If (Me.Type = EntityType.Directory) Then
                    Dim folder As New IO.DirectoryInfo(pathName)
                    If (Not folder.Exists) Then
                        folder.Create()
                    End If
                ElseIf (Me.Type = EntityType.File) Then
                    Dim file As New IO.FileInfo(pathName)
                    If (file.Exists AndAlso Overwrite) Then
                        file.Delete()
                    End If
                    Using bw As New IO.BinaryWriter(file.Create)
                        bw.Write(CType(Me, File).Content)
                    End Using
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Sub Delete()
            If (Me.Type = EntityType.Directory) Then Me.Delete(CType(Me, Directory))
            If (Me.Type = EntityType.File) Then Me.Delete(CType(Me, File))
        End Sub
        Protected Friend Sub Delete(this As Directory)
            If (this.Parent.Type = EntityType.Entrypoint) Then
                CType(this.Parent, Entrypoint).Content.Remove(this)
            ElseIf (this.Parent.Type = EntityType.Directory) Then
                CType(this.Parent, Entrypoint).Content.Remove(this)
            End If
        End Sub
        Protected Friend Sub Delete(this As File)
            If (this.Parent.Type = EntityType.Entrypoint) Then
                CType(this.Parent, Entrypoint).Content.Remove(this)
            ElseIf (this.Parent.Type = EntityType.Directory) Then
                CType(this.Parent, Entrypoint).Content.Remove(this)
            End If
        End Sub
        Public MustOverride Overrides Function ToString() As String
        <ComponentModel.Description("Unique generated id for location and reference")>
        Public Property Guid As String
        <ComponentModel.Description("Entity name")>
        Public Property Name As String
        <ComponentModel.Browsable(False)>
        Public Property Type As EntityType
        <ComponentModel.Description("Entity creation time")>
        Public Property Created As DateTime
    End Class
End Namespace