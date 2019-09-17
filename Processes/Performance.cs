using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processes
{
    class Performance
    {
        public Performance(string qwe)
        {
            ProccessCount = qwe;
        }
        string _processCount;
        public string ProccessCount
        {
            get
            {
                return _processCount;//System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString();
            }
            set
            {
                _processCount = value;
            }
        }

        public int TickCount()
        {
            return Environment.TickCount & Int32.MaxValue;
        }
    }
}
