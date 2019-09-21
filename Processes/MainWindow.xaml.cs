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

namespace Processes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private object[] _processesList;

        public event PropertyChangedEventHandler PropertyChanged;

        public object[] ProcessesList {
            get => _processesList;
            set {
                _processesList = value;
                PropertyChanged?.Invoke( this, new PropertyChangedEventArgs("ProcessesList"));
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

        void PerformanceWHAAAAT()
        {
            while (true)
            {
                Thread.Sleep(1000);
                var processes = System.Diagnostics.Process.GetProcesses();

                int i;
                foreach (var Process in processes)
                {
                    i = Process.Threads.Count;
                    ;
                }


                //.Threads.Count.ToString();
                performance.TickCount = Environment.TickCount & Int32.MaxValue;
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
