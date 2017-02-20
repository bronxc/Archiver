Public Class frmEditor
    Public Property Target As Object
    Private Sub frmEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Me.Target IsNot Nothing) Then
            Me.PropertyEditor.SelectedObject = Me.Target
        End If
    End Sub
End Class