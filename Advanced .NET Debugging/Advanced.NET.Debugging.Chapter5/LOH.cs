using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET.Debugging.Chapter5
{
    public class LOH
    {
        public void Run()
        {
            byte[] b = null;
            Console.WriteLine("Press any key to allocate on LOH");
            Console.ReadKey();

            b = new byte[100000];

            Console.WriteLine("Press any key yo GC");
            Console.ReadKey();

            b = null;
            GC.Collect();

            Console.WriteLine("Press any key yo exit");
            Console.ReadKey();
        }
    }
}
