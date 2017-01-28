<Serializable>
Public NotInheritable Class Root
    Inherits Entity
    Sub New(Name As String)
        MyBase.New(Name, EntityType.Root)
        Me.Content = New List(Of Entity)
    End Sub
    Public Overrides Function ToString() As String
        Return String.Format("{0} {1} item(s)", Me.Type, Me.Content.Count)
    End Function
    Public Property Content As List(Of Entity)
End Class
