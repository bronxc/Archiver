Imports System.Threading
Public Class BackgroundWorker
    Public Shared Sub Run(TaskToExecute As Action)
        Call New Thread(New ThreadStart(Sub() TaskToExecute())) With {.IsBackground = True}.Start()
    End Sub
End Class
