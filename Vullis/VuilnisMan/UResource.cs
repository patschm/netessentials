using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VuilnisMan
{
    public class UResource : IDisposable
    {
        private static bool isOpen = false;
        private static FileStream related = null;

        public void Open()
        {
            if (!isOpen)
            {
                Console.WriteLine("Open bestand");
                isOpen = true;
            }
            else
            {
                Console.WriteLine("Helaas! In gebruik");
            }
        }
        public void Close()
        {
            Console.WriteLine("Closing....");
            isOpen=false;
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);  
        }
        private void Dispose(bool fromFinalzier)
        {
            if (!fromFinalzier)
            {
                related?.Dispose();
            }
            Close();
        }
        ~UResource()
        {
            Dispose(true);
        }
    }
}
