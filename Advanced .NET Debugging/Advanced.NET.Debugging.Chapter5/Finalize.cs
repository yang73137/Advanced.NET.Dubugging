using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.NET.Debugging.Chapter5
{
    public class Finalize
    {
        public class NativeEvent
        {
            public IntPtr NativeHandle { get; private set; }

            public NativeEvent(string name)
            {
                NativeHandle = CreateEvent(IntPtr.Zero, false, true, name);
            }

            ~NativeEvent()
            {
                if (this.NativeHandle != IntPtr.Zero)
                {
                    CloseHandle(this.NativeHandle);
                    this.NativeHandle = IntPtr.Zero;
                }
            }

            [DllImport("kernel32.dll")]
            private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

            [DllImport("kernel32.dll")]
            private static extern IntPtr CloseHandle(IntPtr lpEvent);
        }

        public void Run()
        {
            NativeEvent nEvent = new NativeEvent("MyNewEvent");

            //
            // 使用 nEvent
            //

            nEvent = null;

            Console.WriteLine("Pressa any key to GC");
            Console.ReadKey();

            GC.Collect();

            Console.WriteLine("Pressa any key to GC");
            Console.ReadKey();

            GC.Collect();

            Console.WriteLine("Pressa any key to exit");
            Console.ReadKey();
        }
    }
}
