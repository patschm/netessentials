using System.Collections;
using System.Collections.Concurrent;

namespace TochWeerDeConsole;

class Program
{
    static void Main()
    {
        //SynchroneVariant();
        //AsynchronousAPM();
        //AsynchonousTPL();
        //AsynchronousNogMooier();
        //AsynchronousErrors();
        //Unstopable();
        //EchtParallelAsync();
        //Restantjes();
        AsyncCollections();
        Console.WriteLine("End!!");
        Console.ReadLine();
    }

    private static void AsyncCollections()
    {
       //List<int> lst = new List<int>();
       ConcurrentBag<int> bag = new ConcurrentBag<int>();
        bag.Add(1);


       
        ConcurrentDictionary<int, string> dictionary = new ConcurrentDictionary<int,string>();
        ConcurrentQueue<int> queue = new ConcurrentQueue<int>();
        ConcurrentStack<int> stack = new ConcurrentStack<int>();

    }

    static object stokje = new object();
    private static void Restantjes()
    {
        int counter = 0;

        Parallel.For(0, 10, idx =>
        {
            // Critical Region        
            lock (stokje)
            {
                int tmp = counter;
                Task.Delay(100).Wait();
                tmp++;
                counter = tmp;
                Console.WriteLine(counter);
            }

        });

        Console.WriteLine(counter);
    }

    private static async Task EchtParallelAsync()
    {
        int resa = 0;
        int resb = 0;

        ManualResetEvent zaklamp = new ManualResetEvent(false);
        ManualResetEvent zaklamp2 = new ManualResetEvent(false);
        //ManualResetEventSlim
        var t1 =Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            resa = 4;
            zaklamp.Set();
            zaklamp.Reset();
        });
        var t2 = Task.Run(() =>
        {
            Task.Delay(2000).Wait();
            resb = 5;
            zaklamp.Set();
        });

        //WaitHandle.WaitAll(new WaitHandle[] { zaklamp, zaklamp2 });
        await Task.WhenAll(t1, t2);
        Console.WriteLine(resa + resb);
    }

    private static void Unstopable()
    {
        Console.WriteLine("Who's gonna stop me?");
        CancellationTokenSource nikko = new CancellationTokenSource();
        CancellationToken bommetje = nikko.Token;
        Task.Run(() => { 
            for(; ; )
            {
                //bommetje.ThrowIfCancellationRequested();
                if (bommetje.IsCancellationRequested) return;
                Console.WriteLine("hoi");
                Task.Delay(1000).Wait();
            }
        }).ContinueWith(pt => Console.WriteLine(pt.Status));

        Task.Delay(2000).Wait();
        nikko.Cancel();
        
    }

    private static async void AsynchronousErrors()
    {
        try
        {
            //Task.Run(() => Ooops()).ContinueWith(pt => { 
            //    if(pt.Exception != null)
            //        Console.WriteLine(pt.Exception.InnerException?.Message);
            // });
            await Task.Run(() => Ooops());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Ooops()
    {
        Console.WriteLine("Proberen");
        Task.Delay(2000).Wait();
        throw new Exception("Ooops");
    }

    private static async void AsynchronousNogMooier()
    {
        Task<int> t1 = new Task<int>(() => LongAdd(4, 5));
        t1.Start();
        int result = await t1;
        Console.WriteLine(result);
        Console.WriteLine("En verder");
        result = await Task.Run(() => LongAdd(14, 15));
        Console.WriteLine(result);
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
