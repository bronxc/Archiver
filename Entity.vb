<Serializable>
Public MustInherit Class Entity
    Sub New(Name As String, Type As EntityType)
        Me.m_name = Name
        Me.m_type = Type
        Me.m_created = DateTime.Now
        Me.m_guid = New Byte(3) {}.Randomize.ToHexString
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
    Public ReadOnly Property Path As String
        Get
            Return Me.GetPath
        End Get
    End Property
    Private m_type As EntityType
    Public ReadOnly Property Type As EntityType
        Get
            Return Me.m_type
        End Get
    End Property
    Private m_guid As String
    Public ReadOnly Property Guid As String
        Get
            Return Me.m_guid
        End Get
    End Property
    Private m_name As String
    Public ReadOnly Property Name As String
        Get
            Return Me.m_name
        End Get
    End Property
    Private m_created As DateTime
    Public ReadOnly Property Created As DateTime
        Get
            Return Me.m_created
        End Get
    End Property
End Class
