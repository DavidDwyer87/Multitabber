using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections;
using HWND = System.IntPtr;

namespace framework
{
    public class Lib
    {
        private const int SW_HIDE = 0x00;
        private const int SW_SHOW = 0x05;

        private const int WS_EX_APPWINDOW = 0x40000;
        private const int GWL_EXSTYLE = -0x14;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("User32.Dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);       

        [DllImport("user32", SetLastError = true)]
        private static extern int GetWindowThreadProcessId(int hwnd, ref int lProcessId);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);       

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);
        
        public static string getActiveWindow(IntPtr handler)
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

        public static int GetProcessThreadFromWindow(int hwnd)
        {
            int procid = 0;
            int threadid = GetWindowThreadProcessId(hwnd, ref procid);
            return procid;
        }

        public static void HideWindowInTaskbar(IntPtr pMainWindow)
        {            
            SetWindowLong(pMainWindow, GWL_EXSTYLE, WS_EX_APPWINDOW);
            ShowWindow(pMainWindow, SW_HIDE);
        }

        public static void ShowWindowInTaskbar(IntPtr pMainWindow)
        {
            SetWindowLong(pMainWindow, GWL_EXSTYLE, WS_EX_APPWINDOW);
            ShowWindow(pMainWindow, SW_SHOW);
        }

        public static string getHWDText(IntPtr hwd)
        {
            try
            {
                int chars = 256;
                StringBuilder buff = new StringBuilder(chars);

                if (GetWindowText(hwd, buff, chars) > 0)
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

        public static IDictionary<HWND, string> GetOpenWindows()
        {
            HWND lShellWindow = GetShellWindow();
            Dictionary<HWND, string> lWindows = new Dictionary<HWND, string>();

            EnumWindows(delegate(HWND hWnd, int lParam)
            {
                if (hWnd == lShellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int lLength = GetWindowTextLength(hWnd);
                if (lLength == 0) return true;

                StringBuilder lBuilder = new StringBuilder(lLength);
                GetWindowText(hWnd, lBuilder, lLength + 1);

                lWindows[hWnd] = lBuilder.ToString();
                return true;

            }, 0);

            return lWindows;
        }

        delegate bool EnumWindowsProc(HWND hWnd, int lParam);
        [DllImport("USER32.DLL")]

        static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
        [DllImport("USER32.DLL")]
        static extern IntPtr GetShellWindow();

    }
}
