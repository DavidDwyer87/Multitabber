using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace framework
{
    public class ActiveWindow
    {
        static IntPtr wnd = IntPtr.Zero;

        public ActiveWindow()
        {

        }

        public static IntPtr window
        {
            get { return wnd; }
            set
            {
                if (getActiveWindow(GetForegroundWindow()) != string.Empty &&
                    getActiveWindow(GetForegroundWindow()) != "MultiTabber")
                {                   
                    wnd = GetForegroundWindow();
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);

        static string getActiveWindow(IntPtr handler)
        {
            int chars = 256;
            StringBuilder buff = new StringBuilder(chars);
            try
            {
                // Update the controls.
                if (GetWindowText(handler, buff, chars) > 0)
                {
                    return buff.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return "";
        }
    }
}
