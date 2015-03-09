using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Advanced.NET.Debugging.Chapter5
{
    internal class Name
    {
        public string First { get; private set; }

        public string Last { get; private set; }

        public Name(string first, string last)
        {
            First = first;
            Last = last;
        }
    }

    class Program
    {
        public static Name CompleteName = new Name("First", "Last");

        private Thread thread;
        private bool shouldExit;

        static void Main(string[] args)
        {
            Finalize finalize = new Finalize();
            finalize.Run();
        }

        public void Gen()
        {
            Name n1 =  new Name("Mario", "Hewardt");
            Name n2 = new Name("Mario", "Hewardt");

            Console.WriteLine("Allocated objects");

            Console.WriteLine("Press any key to invoke GC");
            Console.ReadKey();

            n1 = null;
            GC.Collect();

            Console.WriteLine("Press any key to invoke GC");
            Console.ReadKey();

            GC.Collect();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        public void Root()
        {
            shouldExit = false;

            Name n1 = CompleteName;

            thread = new Thread(o =>
            {
                Name n2 = (Name) o;
                Console.WriteLine("Thread started {0}, {1}", n2.First, n2.Last);
                while (true)
                {
                    Thread.Sleep(500);
                    if (shouldExit)
                    {
                        break;
                    }
                }
            });

            thread.Start(n1);

            Thread.Sleep(500);

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            shouldExit = true;
        }

    }
}
