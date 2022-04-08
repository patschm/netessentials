namespace TochWeerDeConsole;

class Program
{
    static void Main()
    {
        //SynchroneVariant();
        //AsynchronousAPM();
        AsynchonousTPL();
        Console.WriteLine("End!!");
        Console.ReadLine();
    }

    private static void AsynchonousTPL()
    {
       // int res = 0;
        //Task<int> t1 = new Task<int>(() => LongAdd(4, 5));
        //t1.ContinueWith(pt => Console.WriteLine(pt.Result)).ContinueWith(pt => Console.WriteLine("Hoi"));
        //t1.Start();
        //res = t1.Result;
        //Console.WriteLine(res);
        Task.Run<int>(()=>LongAdd(5,6))
            .ContinueWith(pt => Console.WriteLine(pt.Result))
            .ContinueWith(pt => Console.WriteLine("Hoi"));
    }

    private static void AsynchronousAPM()
    {
        Func<int, int, int> del = LongAdd;

        IAsyncResult ar = del.BeginInvoke(3, 4, arr => Console.WriteLine(del.EndInvoke(arr)), null);
        while(!ar.IsCompleted)
        {
            Console.Write(".");
            Task.Delay(100).Wait();    
        }
        int result = del.EndInvoke(ar);
        Console.WriteLine(result);
    }

    private static void SynchroneVariant()
    {
        int result = LongAdd(2, 3);
        Console.WriteLine(result);
    }

    static int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
}
