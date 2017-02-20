Imports System.Reflection

Partial Class FrmMain
    Private Function ShowMessage(Message As String) As DialogResult
        Return MessageBox.Show(Message, "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation)
    End Function
    Private Sub DebugProxyBeforeCall(mcallmessage As Runtime.Remoting.Messaging.IMethodCallMessage)
        Me.WriteLog(String.Format("{0}()", mcallmessage.MethodName), Color.DarkGreen)
        Me.WriteLog(Me.GetMethodCallStack(0, 5), Color.DarkGray)
    End Sub
    Private Sub DebugProxyAfterCall(mcallmessage As Runtime.Remoting.Messaging.IMethodCallMessage)
        Me.WriteLog(String.Format("{0}()", mcallmessage.MethodName), Color.CornflowerBlue)
    End Sub
    Private Sub DebugProxyException(mcallmessage As Runtime.Remoting.Messaging.IMethodCallMessage, ex As Exception)
        Me.WriteLog(String.Format("Exception caught at {0}() : {1}", mcallmessage.MethodName, ex.Message), Color.Red)
    End Sub
    Private Function GetMethodCallStack(index As Integer, count As Integer) As String
        Return String.Join(String.Format("{1}{0}{1}", ControlChars.CrLf, ControlChars.Tab), New StackTrace().GetFrames.Select(Function(x) Me.Truncate(x.GetMethod.ToString, 60)).ToList.GetRange(6, 4))
    End Function
    Private Function Truncate(str As String, len As Integer) As String
        Return If(str.Length <= len, str, str.Substring(0, len) + "...")
    End Function
End Class
