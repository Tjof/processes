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

namespace Processes
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            new Thread(ProcRefresh).Start();
        }

        void ProcRefresh()
        {
            Process[] processes;
            while (true)
            {
                processes = System.Diagnostics.Process.GetProcesses();
                Thread.Sleep(1000);
                // listView.ItemsSource = processes.Select(p => new ProcessInfo(p) { SizeType = 1 }).ToArray();
                Dispatcher.BeginInvoke(new Action(() => listView.ItemsSource = processes.Select(p => new ProcessInfo(p) { SizeType = 1 }).ToArray()));
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
