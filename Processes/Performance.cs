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
        
        int _processCount;
        int _tickcount;
        int _processes;
        int _handleCount;
        long _virtualMemorySize;

        public event PropertyChangedEventHandler PropertyChanged;

        public int ProccessCount
        {
            get{ return _processCount; }
            set
            {
                _processCount = value;
                OnPropertyChanged();
            }
        }

        public int Processes
        {
            get
            {
                return _processes;
            }
            set
            {
                _processes = value;
                OnPropertyChanged();
            }
        }

        public int HandleCount
        {
            get
            {
                return _handleCount;
            }
            set
            {
                _handleCount = value;
                OnPropertyChanged();
            }
        }

        public long VirtualMemorySize
        {
            get
            {
                return _virtualMemorySize;
            }
            set
            {
                _virtualMemorySize = value;
                OnPropertyChanged();
            }
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



        void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
