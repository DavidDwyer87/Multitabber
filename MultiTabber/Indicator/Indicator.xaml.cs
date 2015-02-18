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
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;


namespace ControlScreen
{
    /// <summary>
    /// Interaction logic for Indicator.xaml
    /// </summary>
    public partial class Indicator : Window
    {
        public static Indicator instance = null;

        Timer timer = null;

        public Indicator()
        {
            InitializeComponent();
            instance = this;

            this.Top = (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2;
            this.Left = (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2;           

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
            timer.Stop();
        }          

        public void display(string val)
        {
            label1.Content = val;
            this.Show();
            timer.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
        }       
    }
}
