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
using System.Windows.Forms;
using VirtualControls;
using System.Diagnostics;
using System.Threading;
using System.Drawing;


namespace MultiTabber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow instance = null;
        TrayOperation t = null;       
        
        public MainWindow()
        {
            InitializeComponent();
            instance = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Checkprocess();
            
            SplashScreen scr = new SplashScreen();
            scr.Left = (Screen.PrimaryScreen.Bounds.Width / 2) - (640 / 2);
            scr.Top = (Screen.PrimaryScreen.Bounds.Height / 2) - (480 / 2);
            scr.Show();                       

            Thread.Sleep(5000);
            scr.Close();

            //GUI
            App.tab = new Dots();
            App.tab.setInstance(this);

            App.tab.createDots(Properties.Settings.Default.Fill, Properties.Settings.Default.stroke, Properties.Settings.Default.forground, Properties.Settings.Default.Display);
            sprite.Children.Add(dots.instance);

            //location
            PanelLocation pl = new PanelLocation();
            pl.setLocation(this, Properties.Settings.Default.Horizontal,
                Properties.Settings.Default.Vertical);

            //tray
            t = new TrayOperation(pl);
            //System.Windows.MessageBox.Show(Environment.OSVersion.Version.ToString());          
        }      

        void Checkprocess()
        {
            int checker = 0;
            foreach (Process p in Process.GetProcesses())
            {
                if (p.ProcessName == "MultiTabber")
                {
                    checker++;
                }
            }

            if (checker > 1)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.tab.Rollback();
        }      
    }
}
