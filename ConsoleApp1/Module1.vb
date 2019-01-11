Imports System.Threading.Tasks
Imports System.Threading
'как понять Где начинается callback и что происходит с вызывающим потоком

Module Module1

	Sub Main()
		'
		mySubAsync(10) ' запускается Task -> т.е. параллельная работа -) в консоле!
		mySub(20) 'consoleEND 5 (из параллели пришел callback)
		'т.е. после завершения task_и вызывающий поток прерывается и выполняет callback( часть после await )
		Console.ReadLine()
	End Sub
	Private Sub mySub(ByVal x As Integer)
		'x = x * 10
		For i = 0 To x
			'Thread.Sleep(500)
			Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} ...i={i}")
		Next
	End Sub
	Private Async Sub mySubAsync(ByVal x As Integer)
		mySub(x)
		Await Task.Run(Sub()
						   mySub(x)
					   End Sub)
		Console.WriteLine("END>>>")

	End Sub
End Module
