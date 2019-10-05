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
using System.Management;
using Microsoft.VisualBasic;

namespace Processes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private object[] _processesList;

        public event PropertyChangedEventHandler PropertyChanged;

        public object[] ProcessesList
        {
            get => _processesList;
            set
            {
                _processesList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProcessesList"));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            new Thread(ProcRefresh).Start();
            new Thread(PerformanceWHAAAAT).Start();

        }

        void ProcRefresh()
        {
            Process[] processes;
            while (true)
            {
                processes = System.Diagnostics.Process.GetProcesses();
                Thread.Sleep(1000);
                ProcessesList = processes.Select(p => new ProcessInfo(p) { SizeType = 1 })
                    .Select(pi => new { pi.ReadTransferCount, pi.ReadOperationCount, pi.BaseProcess.ProcessName, pi.WriteOperationCount, pi.Memory, pi.WriteTransferCount, pi.HandleCount })
                    .ToArray();
            }
        }

        private Performance performance = new Performance();

        public Performance Performance
        {
            get => performance;
            set
            {
                performance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Performance"));
            }
        }

        public static IEnumerable<object> GetResults(string win32ClassName, string property)
        {
            return (from x in new ManagementObjectSearcher("SELECT * FROM " + win32ClassName).Get().OfType<ManagementObject>()
                    select x.GetPropertyValue(property));
        }

        

        void PerformanceWHAAAAT()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var processes = System.Diagnostics.Process.GetProcesses();


                int sumTheard = 0;
                foreach (var Process in processes)
                {
                    int i = 0;
                    i = Process.Threads.Count;
                    sumTheard += i;
                }
                performance.ProccessCount = sumTheard;

                int sumProcesses = 0;
                foreach (var Process in processes)
                {
                    sumProcesses = processes.Length;
                }
                performance.Processes = sumProcesses;

                int sumHandleCount = 0;
                foreach (var Process in processes)
                {
                    int i = 0;
                    i = Process.HandleCount;
                    sumHandleCount += i;
                }
                performance.HandleCount = sumHandleCount;

                long sumVirtualMemorySize = 0;
                foreach (var Process in processes)
                {
                    long i = 0;
                    i = Process.PrivateMemorySize64 / 1024 / 1024;
                    sumVirtualMemorySize += i;
                }
                performance.VirtualMemorySize = sumVirtualMemorySize;
                
                var values = GetResults("Win32_PhysicalMemory", "Capacity");
                ulong? Total = null;
                foreach (var item in values)
                {
                    var casted = item as ulong?;
                    if (casted.HasValue)
                    {
                        if (Total == null) Total = 0;
                        Total += casted.Value;
                    }
                }
                performance.TotalInstalledBytes = Total / 1024 /1024;

                PerformanceCounter ramFree = new PerformanceCounter("Memory", "Available MBytes");
                performance.MemoryAvailable = ramFree.NextValue();

                PerformanceCounter CacheBytes = new PerformanceCounter("Memory", "Cache Bytes");
                performance.CacheBytes = CacheBytes.NextValue()/1024;

                performance.TickCount = Environment.TickCount & Int32.MaxValue; // Время работы копьютера ШОК ПРЕОБРАЗОВАТЬ НАДО ВО ВРЕМЯ
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ProcessInfo)listView.SelectedItem).BaseProcess.Kill();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


    }
}
