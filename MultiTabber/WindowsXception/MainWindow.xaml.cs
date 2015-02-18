using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using framework;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace WindowsXception
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> app = new List<string>();
        List<string> proc = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public ObservableCollection<ApplicationData> ApplicationCollection
        {
            get { return initialize.appList(); }
        }

        public ObservableCollection<ProcessData> ProcessCollection
        {
            get { return initialize.ProcessList(); }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            initialize.AddXlist(app, proc);
            
            //save exception list
            initialize.Save();
          
            this.Close();
        }

        private void row_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            app.Add(((ApplicationData)box.Tag).windowName);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            proc.Add(((ProcessData)box.Tag).processName);                     
        }

        private void row_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;            
            app.Remove(((ApplicationData)box.Tag).windowName);                      
        }

        private void processRow_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            proc.Remove(((ProcessData)box.Tag).processName);         
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            foreach (string str in initialize.exList())
                app.Add(str);

            foreach (string str in initialize.pexList())
                proc.Add(str);
        }
    }    
}
