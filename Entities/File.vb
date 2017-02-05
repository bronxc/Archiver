Namespace Entities
    <Serializable>
    Public NotInheritable Class File
        Inherits Entity
        Sub New(Filename As String, Parent As Entity)
            MyBase.New(New IO.FileInfo(Filename).Name, EntityType.File)
            Me.Parent = Parent
            If (IO.File.Exists(Filename)) Then
                Using br As New IO.BinaryReader(New IO.FileStream(Filename, IO.FileMode.Open, IO.FileAccess.Read), Text.Encoding.UTF8)
                    Me.Content = New Byte(Convert.ToInt32(br.BaseStream.Length)) {}
                    br.ReadBytes(Me.Content.Length).CopyTo(Me.Content, 0)
                End Using
                Me.Checksum = Hashing.Checksums.Fletcher32(Me.Content)
                Me.Created = New IO.FileInfo(Filename).CreationTime
                Me.Extension = IO.Path.GetExtension(Filename)
            Else
                Me.Content = New Byte() {}
            End If
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} [0x{2}]", Me.Type.ToString, Me.Content.Length.SizeToReadableForm, Me.Checksum.ToString("X2"))
        End Function
        <ComponentModel.Browsable(False)>
        Public Property Content As Byte()
        <ComponentModel.Browsable(False)>
        Public Property Length As Integer
        <ComponentModel.Browsable(False)>
        Public Property Checksum As UInt32
        Public Property Extension As String
        Public Property Parent As Entity
    End Class
End Namespace