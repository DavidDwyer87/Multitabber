using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics;


namespace framework
{
    class Filters
    {
        public static bool ValidProcess(string pname, string title)
        {
            if (pname != string.Empty && title != string.Empty && !Bucket.internalExceptionList.Contains(pname)
                && !Bucket.internalExceptionList.Contains(title) && !Bucket.AppExceptions.Contains(title) && !Bucket.ProcExceptions.Contains(pname))
            {
                return true;
            }          

            return false;
        }

        public static bool ValidProcessNuNEx(string pname, string title)
        {
            if (pname != string.Empty && title != string.Empty && !Bucket.internalExceptionList.Contains(pname)
                && !Bucket.internalExceptionList.Contains(title))
            {
                return true;
            }
            return false;
        }

        //change an application from one screen to the next
        public static void change(IntPtr hwd, int scr)
        {
            try
            {
                if (!Bucket.AppExceptions.Contains(Lib.getHWDText(hwd)))
                {
                    if (!Bucket.internalExceptionList.Contains(Lib.getHWDText(hwd)))
                    {
                        for (int i = 0; i <10; i++)
                        {
                            if(Bucket.apps.ContainsKey(i))
                            {
                                if (Bucket.apps[i].Contains(hwd))
                                {
                                    Bucket.apps[i].Remove(hwd);
                                    //break;
                                }
                            }
                        }

                        if (Bucket.apps.ContainsKey(scr-1))
                        {
                            Bucket.apps[scr-1].Add(hwd);
                        }
                        else
                        {
                            Bucket.apps.Add(scr - 1, new List<IntPtr>());
                            Bucket.apps[scr - 1].Add(hwd);
                        }
                        View.hide(hwd);
                    }
                    else
                    {
                        MessageBox.Show("This application not allowed to change windows");
                    }
                }
                else
                {
                    if (MessageBox.Show(Lib.getHWDText(hwd) +
                        " is on the exception list do you want to remove this application the list?","", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Bucket.apps[scr-1].Add(hwd);
                        View.hide(hwd);
                        Bucket.AppExceptions.Remove(Lib.getHWDText(hwd));                        

                        if (Properties.Settings1.Default.AppException.Contains(Lib.getHWDText(hwd)))
                        {
                            Properties.Settings1.Default.AppException.Remove(Lib.getHWDText(hwd));
                        }

                        if (Bucket.ProcExceptions.Contains(Process.GetProcessById(Lib.GetProcessThreadFromWindow(hwd.ToInt32())).ProcessName))
                        {
                            Bucket.ProcExceptions.Remove(Process.GetProcessById(Lib.GetProcessThreadFromWindow(hwd.ToInt32())).ProcessName);
                            if (Properties.Settings1.Default.ProcException.Contains(Process.GetProcessById(Lib.GetProcessThreadFromWindow(hwd.ToInt32())).ProcessName))
                            {
                                Properties.Settings1.Default.ProcException.Remove(Process.GetProcessById(Lib.GetProcessThreadFromWindow(hwd.ToInt32())).ProcessName);
                            }                            
                        }

                        Properties.Settings1.Default.Save();
                    }
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Ooops hwd doesn't exist in bucket list");
                View.show();                
            }
        }

        public static void remove(IntPtr hwd)
        {
            try
            {
                Bucket.AppPool.Remove(hwd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Error removing windows handler from bucket list");
                View.show();
            }
        }        

        public static void AppUpdate(int scr)
        {
            try
            {
                foreach(Process p in Process.GetProcesses())
                {
                    //check if it is a valid process
                    if (Filters.ValidProcess(p.ProcessName, p.MainWindowTitle))
                    {
                        //set event handler on the window handler 
                        try
                        {
                            p.Exited += new EventHandler(p_Exited);
                            p.EnableRaisingEvents = true;
                        }
                        catch (Exception)
                        { }

                        //update states of the application handler on the current windows
                        if (!inBucket(p.MainWindowHandle,scr))
                        {
                            if (Bucket.apps.ContainsKey(scr))
                            {
                                Bucket.appNameList.Add(p.MainWindowTitle);
                                if (!Bucket.apps[scr].Contains(p.MainWindowHandle))
                                {
                                    int i = 0;
                                    List<int> screens = new List<int>();
                                    foreach (int s in Bucket.apps.Keys)
                                    {
                                        screens.Add(s);
                                    }

                                    for (int x = 0; x < screens.Count; x++)
                                    {
                                        for (int j = 0; j < Bucket.apps[screens[x]].Count; j++)
                                        {
                                            if (p.MainWindowTitle == Lib.getHWDText(Bucket.apps[screens[x]][j]))
                                            {
                                                int index = Bucket.apps[screens[x]].IndexOf(Bucket.apps[screens[x]][j]);
                                                Bucket.apps[screens[x]][index] = p.MainWindowHandle;
                                                i++;
                                            }
                                        }
                                    }

                                    if (i == 0)
                                    {
                                        Bucket.apps[scr].Add(p.MainWindowHandle);
                                    }
                                }
                            }
                            else
                            {
                                Bucket.apps.Add(scr, new List<IntPtr>());
                                Bucket.appNameList.Add(p.MainWindowTitle);

                                if (!Bucket.apps[scr].Contains(p.MainWindowHandle))
                                {
                                    int i = 0;
                                    List<int> screens = new List<int>();
                                    foreach (int s in Bucket.apps.Keys)
                                    {
                                        screens.Add(s);
                                    }

                                    for (int x = 0; x < screens.Count; x++)
                                    {
                                        for (int j = 0; j < Bucket.apps[screens[x]].Count; j++)
                                        {
                                            if (p.MainWindowTitle == Lib.getHWDText(Bucket.apps[screens[x]][j]))
                                            {
                                                int index = Bucket.apps[scr].IndexOf(Bucket.apps[screens[x]][j]);
                                                Bucket.apps[screens[x]][index] = p.MainWindowHandle;
                                                i++;
                                            }
                                        }
                                    }

                                    if (i == 0)
                                    {
                                        Bucket.apps[scr].Add(p.MainWindowHandle);
                                    }
                                }
                            }
                        }                      
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                View.show();
            }
        }
       
        public static void p_Exited(object sender, EventArgs e)
        {
            try
            {
                Process p = sender as Process;
                List<int> num = new List<int>();               

                foreach (int id in Bucket.apps.Keys)
                {
                    num.Add(id);
                }

                for (int i = 0; i <num.Count; i++)
                {
                    if (Bucket.apps[num[i]].Contains(p.MainWindowHandle))
                    {
                        Bucket.apps[num[i]].Remove(p.MainWindowHandle);                        
                    }
                }

                if (Bucket.appNameList.Contains(p.MainWindowTitle))
                {
                    Bucket.appNameList.Remove(p.MainWindowTitle);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                View.show();
            }
        }

        public static void SearchExplorer(int scr)
        {
            try
            {
                SHDocVw.ShellWindows shell = new SHDocVw.ShellWindows();

                foreach (SHDocVw.InternetExplorer ie in shell)
                {
                    IntPtr ptr = new IntPtr(ie.HWND);
                    if (Bucket.apps.ContainsKey(scr))
                    {
                        bool flag = false;

                        foreach (int s in Bucket.apps.Keys)
                        {
                            if (Bucket.apps[s].Contains(ptr))
                            {
                                flag = true;
                            }
                        }

                        if (!flag)
                        {
                            Bucket.apps[scr].Add(ptr);
                        }
                    }
                    else
                    {
                        //Bucket.apps.Add(scr, new List<IntPtr>());
                        //Bucket.apps[scr].Add(ptr);
                    }

                    
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        public static void AppSearch(int scr)
        {
            try
            {
                foreach (KeyValuePair<IntPtr, string> lWindow in Lib.GetOpenWindows())
                {
                    IntPtr lHandle = lWindow.Key;
                    string lTitle = lWindow.Value;

                    if (Bucket.apps.ContainsKey(scr))
                    {
                        bool flag = false;

                        foreach (IntPtr hwnd in Bucket.apps[scr])
                        {
                            string tit = Lib.getHWDText(hwnd);
                            if (tit == lTitle || hwnd == lHandle)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (!flag && ValidProcess(Process.GetProcessById(Lib.GetProcessThreadFromWindow(lHandle.ToInt32())).ProcessName, lTitle))
                        {
                            Bucket.apps[scr].Add(lHandle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                View.show();
            }
        }

        static bool inBucket(IntPtr hwnd,int scr)
        {
            bool check = false;
            for (int i = 0; i < Bucket.apps[scr].Count; i++)
            {
                if (Lib.getHWDText(hwnd) == Lib.getHWDText(Bucket.apps[scr][i]))
                {
                    //int index = Bucket.apps[scr].IndexOf(Bucket.apps[scr][i]);
                    Bucket.apps[scr][i] = hwnd;
                    check = true;

                    break;
                }
            }
            return check;
        }
    }
}
