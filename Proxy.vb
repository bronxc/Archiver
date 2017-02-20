Imports System.Reflection
Imports System.Runtime.Remoting.Proxies
Imports System.Runtime.Remoting.Messaging
Public Class Proxy
    Inherits RealProxy
    Public Event BeforeCall(mcallmessage As IMethodCallMessage)
    Public Event AfterCall(mcallmessage As IMethodCallMessage)
    Public Event ExceptionCaught(mcallmessage As IMethodCallMessage, ex As Exception)
    Private Property Target As Object
    Sub New(Target As Object)
        MyBase.New(Target.GetType)
        Me.Target = Target
    End Sub
    Public Overrides Function Invoke(msg As IMessage) As IMessage
        Dim callMessage As IMethodCallMessage = TryCast(msg, IMethodCallMessage)
        If callMessage IsNot Nothing Then Return Me.PerformCall(callMessage)
        Return Nothing
    End Function
    Public Function Wrap(Of T)() As T
        Return DirectCast(Me.GetTransparentProxy, T)
    End Function
    Private Function PerformCall(msg As IMethodCallMessage) As IMessage
        Try
            RaiseEvent BeforeCall(msg)
            Dim result As Object = msg.MethodBase.Invoke(Me.Target, msg.InArgs)
            RaiseEvent AfterCall(msg)
            Return New ReturnMessage(result, Nothing, 0, msg.LogicalCallContext, msg)
        Catch ex As TargetInvocationException
            RaiseEvent ExceptionCaught(msg, ex.InnerException)
            Return New ReturnMessage(ex.InnerException, msg)
        End Try
    End Function
End Class
