using VuilnisMan;

namespace Vuilnisman;

class Program
{
    static UResource res1 = new UResource();
    static UResource res2 = new UResource();    

    public static void Main()
    {
        using (res1)
        {
            res1.Open();
        }
        
        //res1.Dispose();
        res1 = null;

        // GC.Collect();
        //GC.WaitForPendingFinalizers();

        using (UResource res3 = new UResource())
            res3.Open();
       // res3.Dispose();

        using (UResource res4 = new UResource())
        {
            res4.Open();
        }
    }
}
