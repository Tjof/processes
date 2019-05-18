using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Processes
{
    public class WinApi
    {
        [DllImport("kernel32.dll")]
        public static extern bool GetProcessIoCounters(IntPtr ProcessHandle, out IO_COUNTERS IoCounters);
    }

    public struct IO_COUNTERS
    {
        public ulong ReadOperationCount;
        public ulong WriteOperationCount;
        public ulong OtherOperationCount;
        public ulong ReadTransferCount;
        public ulong WriteTransferCount;
        public ulong OtherTransferCount;

        public void Init()
        {
            ReadOperationCount = 0;
            WriteOperationCount = 0;
            OtherOperationCount = 0;
            ReadTransferCount = 0;
            WriteTransferCount = 0;
            OtherTransferCount = 0;
        }
    }
}
