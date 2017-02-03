Namespace Entities
    <Serializable>
    Public MustInherit Class Entity
        Sub New(Name As String, Type As EntityType)
            Me.Name = Name
            Me.Type = Type
            Me.Guid = New Byte(3) {}.Randomize.ToHexString
            Me.Created = DateTime.Now
        End Sub
        Public Function FindByGuid(match As String, ByRef result As List(Of Entity)) As Boolean
            Me.FindGuidLike(match, result)
            Return result.Any
        End Function
        Public Function FindByName(match As String, ByRef result As List(Of Entity)) As Boolean
            Me.FindNameLike(match, result)
            Return result.Any
        End Function
        Public Function TryResolve(path As String, ByRef result As Entity) As Boolean
            Return Me.TryResolvePath(path, result)
        End Function
        Public Function ExtractTo(Directory As String, Optional Overwrite As Boolean = False) As Boolean
            Try
                Dim output As String = String.Format("{0}\{1}", Directory, Me.GetPath)
                If (Me.Type = EntityType.Root) Then
                    Dim folder As New IO.DirectoryInfo(output)
                    If (Not folder.Exists) Then
                        folder.Create()
                    End If
                ElseIf (Me.Type = EntityType.Directory) Then
                    Dim folder As New IO.DirectoryInfo(output)
                    If (Not folder.Exists) Then
                        folder.Create()
                    End If
                ElseIf (Me.Type = EntityType.File) Then
                    Dim file As New IO.FileInfo(output)
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
        Public MustOverride Overrides Function ToString() As String
        Public Property Created As DateTime
        Public Property Type As EntityType
        Public Property Guid As String
        Public Property Name As String
    End Class
End Namespace