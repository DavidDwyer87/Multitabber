using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TrayComp;
using System.ComponentModel;
using System.Threading;

namespace MultiTabber
{
    class TrayOperation
    {
        public static tray _tray = null;
        static Options.MainOption option = null;
        static PanelLocation plocation = null;
        static hotKeys.MainWindow mw =null;

        public TrayOperation(PanelLocation pl)
        {
            _tray = new tray();

            startTrayEvents();
            plocation = pl;
        }       

        void startTrayEvents()
        {
            _tray.itemMenu.MenuItems[0].Click += new EventHandler(hide_Click);
            _tray.itemMenu.MenuItems[1].Click += new EventHandler(TopMost_Click);           
            _tray.itemMenu.MenuItems[3].Click += new EventHandler(HotKeys_Click);
            _tray.itemMenu.MenuItems[4].Click += new EventHandler(Option_Click);            
            _tray.itemMenu.MenuItems[5].Click += new EventHandler(help_Click);            
            _tray.itemMenu.MenuItems[6].Click += new EventHandler(exit_Click);            
        }      

        void HotKeys_Click(object sender, EventArgs e)
        {
            hotK();
        }

        static void mw_Closed(object sender, EventArgs e)
        {
            mw = null;
        }               

        void help_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("Help page not implemented as yet");
                System.Diagnostics.Process.Start("http://multitabber.codeplex.com/documentation");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       

        void TopMost_Click(object sender, EventArgs e)
        {
            if (_tray.itemMenu.MenuItems[1].Text == "Not on Top")
            {
                MainWindow.instance.Topmost = false;
                _tray.itemMenu.MenuItems[1].Text = "Always on Top";
            }
            else
            {
                _tray.itemMenu.MenuItems[1].Text = "Not on Top";
                MainWindow.instance.Topmost = true;
            }
        }

        void hide_Click(object sender, EventArgs e)
        {
            if (_tray.itemMenu.MenuItems[0].Text == "Hide")
            {
                MainWindow.instance.Hide();
                _tray.itemMenu.MenuItems[0].Text = "Show";
            }
            else
            {
                MainWindow.instance.Show();
                _tray.itemMenu.MenuItems[0].Text = "Hide";
            }
        }

        void Option_Click(object sender, EventArgs e)
        {
            getOpt();
        }

        static void option_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (option.state)
                {        
                    saveOpt();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message +"Error saving user settings");
            }
        }        

        static void option_Closed(object sender, EventArgs e)
        {                    
            option = null;
        }

        void exit_Click(object sender, EventArgs e)
        {
            _tray.notifyIcon.Dispose();
            MainWindow.instance.Close();
        }

        static void saveColors(string fill,string stroke,string fground)
        {
            Properties.Settings.Default.Fill = fill;

            if (stroke != "")
            {
                Properties.Settings.Default.stroke = stroke;
            }

            Properties.Settings.Default.forground = fground;           
        }

        static void savePosition(string h, string v)
        {
            if (h != "")
            {
                Properties.Settings.Default.Horizontal = h;
            }

            if (v != "")
            {
                Properties.Settings.Default.Vertical = v;
            }         
        }

        static void saveTab(int number)
        {
            if (number != 0)
            {
                Properties.Settings.Default.Display = number;
            }
        }

        public static void hotK()
        {
            if (mw == null)
            {
                mw = new hotKeys.MainWindow();
                mw.getTabCount(Properties.Settings.Default.Display);
                mw.Closed += new EventHandler(mw_Closed);
                mw.ShowDialog();
            }
            else
            {
                try
                {
                    mw.Focus();
                }
                catch (Exception) { }
            }
        }

        public static void getOpt()
        {
            if (option == null)
            {
                option = new Options.MainOption(Properties.Settings.Default.Fill, Properties.Settings.Default.stroke, Properties.Settings.Default.forground,
                    Properties.Settings.Default.Display);
                option.Dispatcher.Invoke(new Action(delegate()
                {
                    option.currentPosition(Properties.Settings.Default.Horizontal, Properties.Settings.Default.Vertical);

                    if (App.tab.li.activeWindow == 0)
                    {
                        //option.currentColors(VirtualControls.Dots.ellipseCollection[1].Fill, VirtualControls.Dots.ellipseCollection[1].Stroke, App.tab.li.indi.Foreground);
                    }
                    else if (App.tab.li.activeWindow > 0)
                    {
                        //option.currentColors(VirtualControls.Dots.ellipseCollection[App.tab.li.activeWindow - 1].Fill, VirtualControls.Dots.ellipseCollection[App.tab.li.activeWindow - 1].Stroke, App.tab.li.indi.Foreground);
                    }

                    option.Closed += new EventHandler(option_Closed);
                    option.Closing += new System.ComponentModel.CancelEventHandler(option_Closing);
                    
                    option.Hide();
                    option.ShowDialog();

                }), System.Windows.Threading.DispatcherPriority.Render, null);
                
            }
            else
            {
                option.Dispatcher.Invoke(new Action(delegate()
                {
                    option.Focus();
                }), System.Windows.Threading.DispatcherPriority.Render, null);                
            }
        }

        static void saveOpt()
        {
            App.tab.changeState(option.sample.Fill, option.ellipse1.Stroke, option.label8.Foreground);
            string strk = "";

            if (option.ellipse1.Stroke != null)
            {
                strk = option.ellipse1.Stroke.ToString();
            }

            if (Properties.Settings.Default.Display != Convert.ToInt16(option.label10.Content))
            {
                MultiTabber.App.tab.TabNumber(Convert.ToInt16(option.label10.Content));
            }

            saveColors(option.sample.Fill.ToString(), strk, option.label8.Foreground.ToString());

            string h = "";
            string v = "";

            #region horizontal positioning
            if (option.radioButton1.IsChecked == true)
            {
                plocation.setHorizon("left");
                h = "left";
            }
            else if (option.radioButton2.IsChecked == true)
            {
                plocation.setHorizon("center");
                h = "center";
            }
            else if (option.radioButton3.IsChecked == true)
            {
                plocation.setHorizon("right");
                h = "right";
            }
            #endregion

            #region vertical positioning
            if (option.radioButton4.IsChecked == true)
            {
                plocation.setVertical("top");
                v = "top";
            }
            else if (option.radioButton5.IsChecked == true)
            {
                plocation.setVertical("bottom");
                v = "bottom";
            }
            #endregion

            savePosition(h, v);

            saveTab(Convert.ToInt32(option.slider2.Value));

            Properties.Settings.Default.Save();
        }

        public delegate void tabber(int num);

        public static void addtab()
        {
            if ((Properties.Settings.Default.Display + 1) <= 10)
            {
                tabber t = new tabber(MultiTabber.App.tab.TabNumber);
                t((Properties.Settings.Default.Display + 1));
            }
            else
            {
                MessageBox.Show("No more tabs available");
            }
        }

        public static void subtab()
        {
            if ((Properties.Settings.Default.Display - 1) >= 2)
            {
                MultiTabber.App.tab.TabNumber(Properties.Settings.Default.Display - 1);
            }
            else
            {
                MessageBox.Show("this is mininum number of tabs a lotted and can't be reduce any further.");
            }            
        }
    }
}
