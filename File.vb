<Serializable>
Public NotInheritable Class File
    Inherits Entity
    Sub New(Filename As String, Parent As Entity)
        MyBase.New(New IO.FileInfo(Filename).Name, EntityType.File)
        Me.m_parent = Parent
        If (IO.File.Exists(Filename)) Then
            Using br As New IO.BinaryReader(New IO.FileStream(Filename, IO.FileMode.Open, IO.FileAccess.Read), Text.Encoding.UTF8)
                Me.m_content = New Byte(Convert.ToInt32(br.BaseStream.Length)) {}
                br.ReadBytes(Me.Content.Length).CopyTo(Me.Content, 0)
            End Using
            Me.m_checksum = Me.Content.ToCrc32
        Else
            Me.m_content = New Byte() {}
        End If
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0} {1} {2} bytes", Me.Type, Me.Name, Me.Content.Length)
    End Function
    Private m_content As Byte()
    Public ReadOnly Property Content As Byte()
        Get
            Return Me.m_content
        End Get
    End Property
    Private m_checksum As UInt32
    Public ReadOnly Property Length As Integer
        Get
            Return Me.Content.Length
        End Get
    End Property
    Public ReadOnly Property Checksum As UInt32
        Get
            Return Me.m_checksum
        End Get
    End Property
    Private m_parent As Entity
    Public ReadOnly Property Parent As Entity
        Get
            Return Me.m_parent
        End Get
    End Property
End Class
