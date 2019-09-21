using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using ProcessesClass;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Processes
{
    public class Performance : INotifyPropertyChanged
    {
        
        string _processCount;
        int _tickcount;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ProccessCount
        {
            get{ return System.Diagnostics.Process.GetCurrentProcess().Threads.Count.ToString(); }
            set
            {
                _processCount = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public int TickCount
        {
            get{ return Environment.TickCount & Int32.MaxValue; }
            set
            {
                _tickcount = value;
                OnPropertyChanged();
            }
        }

        
    }
}
