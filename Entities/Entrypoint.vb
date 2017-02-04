Namespace Entities
    <Serializable>
    Public NotInheritable Class Entrypoint
        Inherits Entity
        Sub New(Name As String)
            MyBase.New(Name, EntityType.Entrypoint)
            Me.Content = New List(Of Entity)
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0} item(s)", Me.Content.Count)
        End Function
        Public Property Content As List(Of Entity)
    End Class
End Namespace