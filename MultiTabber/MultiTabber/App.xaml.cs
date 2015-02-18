#define addTab //this function is to add/minus tabs by keyboard shortcut

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Hooks;
using VirtualControls;
using System.Threading;


namespace MultiTabber
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        KeyboardListener kb = new KeyboardListener(); 
        List<string> keys = new List<string>();
        public static Dots tab = new Dots();
        Thread tt = null;
       
        
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            kb.KeyDown += new RawKeyEventHandler(kb_KeyDown);
            kb.KeyUp += new RawKeyEventHandler(kb_KeyUp);             
        }

        void kb_KeyUp(object sender, RawKeyEventArgs args)
        {
            #region screen shortcut
            if (keys.Contains("LeftCtrl") && keys.Contains("D1") || keys.Contains("RightCtrl") && keys.Contains("D1"))
            {
                tab.li.Display(0);                
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D2") || keys.Contains("RightCtrl") && keys.Contains("D2"))
            {
                tab.li.Display(1);                
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D3") || keys.Contains("RightCtrl") && keys.Contains("D3"))
            {
                tab.li.Display(2);               
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D4") || keys.Contains("RightCtrl") && keys.Contains("D4"))
            {
                tab.li.Display(3);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D5") || keys.Contains("RightCtrl") && keys.Contains("D5"))
            {
                tab.li.Display(4);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D6") || keys.Contains("RightCtrl") && keys.Contains("D6"))
            {
                tab.li.Display(5);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D7") || keys.Contains("RightCtrl") && keys.Contains("D7"))
            {
                tab.li.Display(6);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D8") || keys.Contains("RightCtrl") && keys.Contains("D8"))
            {
                tab.li.Display(7);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D9") || keys.Contains("RightCtrl") && keys.Contains("D9"))
            {
                tab.li.Display(8);
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("D0") || keys.Contains("RightCtrl") && keys.Contains("D0"))
            {
                tab.li.Display(9);
            }
            #endregion

            #region tray shortcut
            if (keys.Contains("LeftCtrl") && keys.Contains("K") || keys.Contains("RightCtrl") && keys.Contains("K"))
            {
                try
                {
                    tt = new Thread(new ThreadStart(TrayOperation.hotK));
                    tt.SetApartmentState(ApartmentState.STA);
                    tt.Start();   
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("U") || keys.Contains("RightCtrl") && keys.Contains("U"))
            {                
                try
                {
                    tt = new Thread(new ThreadStart(TrayOperation._tray.uni_Window));
                    tt.SetApartmentState(ApartmentState.STA);
                    tt.Start(); 
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("E") || keys.Contains("RightCtrl") && keys.Contains("E"))
            {
                try
                {
                    tt = new Thread(new ThreadStart(TrayOperation.getOpt));
                    tt.SetApartmentState(ApartmentState.STA);
                    tt.Start();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //topmost
            if (keys.Contains("LeftCtrl") && keys.Contains("J") || keys.Contains("RightCtrl") && keys.Contains("J"))
            {
                try
                {
                    if (TrayOperation._tray.itemMenu.MenuItems[1].Text == "Not on Top")
                    {


                        tab.topMost(false);
                        TrayOperation._tray.itemMenu.MenuItems[1].Text = "Always on Top";
                    }
                    else
                    {
                        tab.topMost(true);
                        TrayOperation._tray.itemMenu.MenuItems[1].Text = "Not on Top";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //hide/show control
            if (keys.Contains("LeftCtrl") && keys.Contains("G") || keys.Contains("RightCtrl") && keys.Contains("G"))
            {
                try
                {
                    if (TrayOperation._tray.itemMenu.MenuItems[0].Text == "Hide")
                    {
                        TrayOperation._tray.itemMenu.MenuItems[0].Text = "Show";
                        tab.Vis(true);
                    }
                    else
                    {
                        TrayOperation._tray.itemMenu.MenuItems[0].Text = "Hide";
                        tab.Vis(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (keys.Contains("LeftCtrl") && keys.Contains("Q") || keys.Contains("RightCtrl") && keys.Contains("Q"))
            {
                try
                {
                    tab.Rollback();
                    TrayOperation._tray.notifyIcon.Dispose();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            #endregion

#if !addTab
            if (keys.Contains("LeftCtrl") && keys.Contains("OemMinus") || keys.Contains("RightCtrl") && keys.Contains("OemMinus"))
            {
                TrayOperation.subtab();
            }

            else if (keys.Contains("LeftCtrl") && keys.Contains("OemPlus") || keys.Contains("RightCtrl") && keys.Contains("OemPlus"))
            {
                new Thread(new ThreadStart(
                    delegate()
                    {
                        TrayOperation.addtab();
                    }
                )).Start();
            }
#endif
            keys.Clear();           
        }

        void kb_KeyDown(object sender, RawKeyEventArgs args)
        {
            if (!keys.Contains(args.Key.ToString()))
            {
                //MessageBox.Show(args.Key.ToString());
                keys.Add(args.Key.ToString());
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            kb.Dispose();
        }     
    }
}
