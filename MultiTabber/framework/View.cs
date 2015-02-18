using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;

namespace framework
{
    class View
    {
        public static void show(int scr)
        {
            try
            {
                if (!Bucket.apps.ContainsKey(scr))
                {
                    Bucket.apps.Add(scr, new List<IntPtr>());
                }

                foreach (IntPtr wnd in Bucket.apps[scr])
                {
                    Lib.ShowWindowInTaskbar(wnd);
                }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                show();
            }
        }

        public static void hide()
        {
            try
            {
                foreach (int scr in Bucket.apps.Keys)
                {
                    foreach (IntPtr hwnd in Bucket.apps[scr])
                    {
                        if (!Bucket.AppExceptions.Contains(Lib.getHWDText(hwnd)) && !Bucket.ProcExceptions.Contains(Process.GetProcessById(Lib.GetProcessThreadFromWindow(hwnd.ToInt32())).ProcessName))
                            Lib.HideWindowInTaskbar(hwnd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                show();
            }
        }

        public static void hide(IntPtr handler)
        {
            try
            {
                foreach (int scr in Bucket.apps.Keys)
                {
                    foreach (IntPtr hwd in Bucket.apps[scr])
                    {
                        if (handler == hwd)
                        {
                            Lib.HideWindowInTaskbar(hwd);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                show();
            }
        }

        public static void show()
        {
            foreach (int scr in Bucket.apps.Keys)
            {
                foreach (IntPtr hwnd in Bucket.apps[scr])
                {
                    Lib.ShowWindowInTaskbar(hwnd);
                }
            }
        }
    }
}
