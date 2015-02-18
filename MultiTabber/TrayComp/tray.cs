namespace TrayComp
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using WindowsXception;
    using System.IO;

    public class tray
    {
        public static tray instance;
        public ContextMenu itemMenu;
        private MainWindow mw;
        public NotifyIcon notifyIcon;

        public tray()
        {
            instance = this;
            try
            {
                this.notifyIcon = new NotifyIcon();
                this.notifyIcon.Text = "MultiTabber";
                this.notifyIcon.BalloonTipText = "Each dot represents a virtual screen to navigate to a screen, click on one of the dots. For More Help Click help in the tray menu";
                if (File.Exists(Environment.CurrentDirectory + @"\video-display-2.ico"))
                    this.notifyIcon.Icon = new Icon(Environment.CurrentDirectory + @"\video-display-2.ico");
                else
                    this.notifyIcon.Icon = new Icon(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\video-display-2.ico");
                    
                    
                this.notifyIcon.Visible = true;
                this.notifyIcon.ShowBalloonTip(0x3e8);
                this.menuStruct();
                this.notifyIcon.ContextMenu = this.itemMenu;
                this.startEvent();
                this.setShortcuts();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void menuStruct()
        {
            this.itemMenu = new ContextMenu();
            this.itemMenu.MenuItems.Add("Hide");
            this.itemMenu.MenuItems.Add("Not on Top");
            this.itemMenu.MenuItems.Add("Universal Application");
            this.itemMenu.MenuItems.Add("HotKeys");
            //this.itemMenu.MenuItems.Add("Extension Manager");
            this.itemMenu.MenuItems.Add("Preference");
            this.itemMenu.MenuItems.Add("Help");            
            this.itemMenu.MenuItems.Add("Exit");
        }

        private void mw_Closed(object sender, EventArgs e)
        {
            this.mw = null;
        }

        private void notifyIcon_Disposed(object sender, EventArgs e)
        {
        }

        private void setShortcuts()
        {
            this.itemMenu.MenuItems[0].Shortcut = Shortcut.CtrlG;
            this.itemMenu.MenuItems[1].Shortcut = Shortcut.CtrlJ;
            this.itemMenu.MenuItems[2].Shortcut = Shortcut.CtrlU;
            this.itemMenu.MenuItems[3].Shortcut = Shortcut.CtrlK;
            this.itemMenu.MenuItems[4].Shortcut = Shortcut.CtrlE;
            this.itemMenu.MenuItems[5].Shortcut = Shortcut.CtrlM;
            //this.itemMenu.MenuItems[6].Shortcut = Shortcut.CtrlQ;
        }

        private void startEvent()
        {
            this.itemMenu.MenuItems[2].Click += new EventHandler(this.Uni_Click);
        }

        private void Uni_Click(object sender, EventArgs e)
        {
            this.uni_Window();
        }

        public void uni_Window()
        {
            try
            {
                if (this.mw == null)
                {
                    this.mw = new MainWindow();
                    this.mw.Closed += new EventHandler(this.mw_Closed);
                    this.mw.ShowDialog();
                }
                else
                {
                    this.mw.Focus();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}

