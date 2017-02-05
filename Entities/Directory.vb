Namespace Entities
    <Serializable>
    Public NotInheritable Class Directory
        Inherits Entity
        Sub New(Name As String, Parent As Entity)
            MyBase.New(Name, EntityType.Directory)
            Me.Parent = Parent
            Me.Content = New List(Of Entity)
        End Sub
        Public Overrides Function ToString() As String
            Return String.Format("{0} {1} item(s)", Me.Type.ToString, Me.Content.Count)
        End Function
        <ComponentModel.Browsable(False)>
        Public Property Content As List(Of Entity)
        Public Property Parent As Entity
    End Class
End Namespace