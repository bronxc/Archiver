Public MustInherit Class Debugger
    Inherits MarshalByRefObject
    Public MustOverride Sub CatchAndLogException(ex As Exception)
    Public Function Run(Of T)(Method As Func(Of T)) As T
        Try
            Return Method()
        Catch ex As Exception
            Me.CatchAndLogException(ex)
        End Try
    End Function
    Public Sub Run(Method As Action)
        Try
            Method()
        Catch ex As Exception
            Me.CatchAndLogException(ex)
        End Try
    End Sub
End Class
