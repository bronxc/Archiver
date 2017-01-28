Imports System.Threading
Public Class BackgroundWorker
    Public Shared Function Run(TaskToExecute As Action) As Thread
        Dim worker As New Thread(New ThreadStart(Sub() TaskToExecute())) With {.IsBackground = True}
        worker.Start()
        Return worker
    End Function
End Class
