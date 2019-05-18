using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using Processes;

namespace ProcessesClass
{
    class ProcessInfo
    {
        IO_COUNTERS ? lastio = new IO_COUNTERS();
        
        public Process BaseProcess { get; }

        public string ProcessName { get { return BaseProcess.ProcessName; } }

        public int HandleCount
        {
            get
            {
                return BaseProcess.HandleCount;
            }
        }

        public ulong Dread { get; set; }
        public ulong Dwrite { get; set; }
        public ulong ROC { get; set; }
        public ulong WOC { get; set; }

        public double ReadTransferCount
        {
            get
            {
                try
                {
                    WinApi.GetProcessIoCounters(BaseProcess.Handle, out IO_COUNTERS ioc);
                    Dread = ioc.ReadTransferCount - lastio.Value.ReadTransferCount;
                    if(lastio == null)
                    lastio = ioc;
                    return Dread;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public double ReadOperationCount
        {
            get
            {
                try
                {
                    WinApi.GetProcessIoCounters(BaseProcess.Handle, out IO_COUNTERS ioc);
                    ROC = ioc.ReadOperationCount - lastio.Value.ReadOperationCount;
                    if (lastio == null)
                        lastio = ioc;
                    return ROC;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public double WriteOperationCount
        {
            get
            {
                try
                {
                    WinApi.GetProcessIoCounters(BaseProcess.Handle, out IO_COUNTERS ioc);
                    WOC = ioc.WriteOperationCount - lastio.Value.WriteOperationCount;
                    if (lastio == null)
                        lastio = ioc;
                    return WOC;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public double WriteTransferCount
        {
            get
            {
                try
                {
                    WinApi.GetProcessIoCounters(BaseProcess.Handle, out IO_COUNTERS ioc);
                    Dwrite = ioc.WriteTransferCount - lastio.Value.WriteTransferCount;
                    if (lastio == null)
                        lastio = ioc;
                    return Dwrite;
                }
                catch
                {
                    return -1;
                }
            }
        }

        public long Memory
        {
            get {
                if (SizeType == 0)
                {
                    return BaseProcess.WorkingSet64;
                }else if(SizeType == 1)
                {
                    return BaseProcess.WorkingSet64 / 1024;
                }
                return BaseProcess.WorkingSet64;
            }
        }
        
        public int SizeType { get; set; }

        public ProcessInfo(Process process)
        {
            BaseProcess = process;
        }
    }
}
