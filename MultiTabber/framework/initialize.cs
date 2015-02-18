using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace framework
{
    public class initialize
    {
        static int lasstactiveWindow = 0;

        public initialize()
        {
            Bucket.AppPool = new Dictionary<IntPtr, int>();
            
            Bucket.AppExceptions = new List<string>();
            Bucket.ProcExceptions = new List<string>();

            Bucket.internalExceptionList = new List<string>();
            Bucket.appNameList = new List<string>();
            //Bucket.app = new Dictionary<int, Dictionary<int, Dictionary<string, IntPtr>>>();
            Bucket.apps = new Dictionary<int, List<IntPtr>>();
            

            Bucket.internalExceptionList.Add("sidebar");
            Bucket.internalExceptionList.Add("MultiTabber");
            Bucket.internalExceptionList.Add("Start");
            Bucket.internalExceptionList.Add("Sticky Note");
            Bucket.internalExceptionList.Add("Program Manager");
            
            Bucket.internalExceptionList.Add("Indicator");
            Bucket.internalExceptionList.Add("Start menu");
            Bucket.internalExceptionList.Add("StikyNot");
            Bucket.internalExceptionList.Add("Network Flyout");
            Bucket.internalExceptionList.Add("View Available Networks");

            Bucket.internalExceptionList.Add("Notification Area Icons");
            Bucket.internalExceptionList.Add("Start");
            Bucket.internalExceptionList.Add("dfrgui");
            Bucket.internalExceptionList.Add("cleanmgr");
            Bucket.internalExceptionList.Add("Default IME");
            Bucket.internalExceptionList.Add("View Available Networks (tooltip)");

            FindAllProcess();
            addExcep();
        }               

        #region app search 
        void findApp()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (Filters.ValidProcess(p.ProcessName, p.MainWindowTitle))
                {
                    Bucket.AppPool.Add(p.MainWindowHandle, 0);
                    p.Exited +=new EventHandler(Filters.p_Exited);
                }
            }
        }

        void findShell()
        {
            try
            {
                SHDocVw.ShellWindows shell = new SHDocVw.ShellWindows();

                foreach (SHDocVw.InternetExplorer ie in shell)
                {
                    IntPtr ptr = new IntPtr(ie.HWND);
                    Bucket.AppPool.Add(ptr, 0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        
        #endregion

        void addExcep()
        {
            if (Properties.Settings1.Default.AppException != null)
                foreach (string str in Properties.Settings1.Default.AppException)
                    Bucket.AppExceptions.Add(str);
            else
                Properties.Settings1.Default.AppException = new System.Collections.Specialized.StringCollection();

            if (Properties.Settings1.Default.ProcException != null)
                foreach (string str in Properties.Settings1.Default.ProcException)
                    Bucket.ProcExceptions.Add(str);
            else
                Properties.Settings1.Default.ProcException = new System.Collections.Specialized.StringCollection();

            //Properties.Settings1.Default.AppException.Clear();
            Properties.Settings1.Default.Save();
        }

        void FindAllProcess()
        {
            Bucket.apps.Add(0, new List<IntPtr>());

            foreach (Process p in Process.GetProcesses())
            {
                if(Filters.ValidProcess(p.ProcessName,p.MainWindowTitle))
                {
                    try
                    {
                        p.Exited += new EventHandler(Filters.p_Exited);
                        p.EnableRaisingEvents = true;
                    }
                    catch (Exception)
                    { }

                    Bucket.apps[0].Add(p.MainWindowHandle);
                    Bucket.appNameList.Add(p.MainWindowTitle);
                }
            }

             SHDocVw.ShellWindows shell = new SHDocVw.ShellWindows();            

             foreach (SHDocVw.InternetExplorer ie in shell)
             {
                 IntPtr ptr = new IntPtr(ie.HWND);

                 if(!Bucket.apps[0].Contains(ptr))
                 {
                    Bucket.apps[0].Add(ptr);
                    Bucket.appNameList.Add(Lib.getHWDText(ptr));
                 }
             }

             Filters.AppSearch(0);
        }

        public void change(int scr)
        {
            View.hide();
            View.show(scr);
            lasstactiveWindow = scr;
        }        

        public void changeView(int scr)
        {
            ActiveWindow.window = Lib.GetForegroundWindow();            
            Filters.change(ActiveWindow.window, scr);
        }

        public void RollBack()
        {
            View.show();
        }

        public void Update_Bucket(int scr)
        {
            Filters.AppUpdate(scr);
            Filters.SearchExplorer(scr);
        }

        public static ObservableCollection<ApplicationData> appList()
        {
            ObservableCollection<ApplicationData> data = new ObservableCollection<ApplicationData>();
           
            List<string> name = new List<string>();
            bool _value = false;

            foreach (int scr in Bucket.apps.Keys)
            {
                foreach (IntPtr hwnd in Bucket.apps[scr])
                {
                    _value = false;                    

                    if(Lib.getHWDText(hwnd) !="")
                    {
                        if (Bucket.AppExceptions != null && Bucket.AppExceptions.Contains(Lib.getHWDText(hwnd)))
                        {
                            _value = true;
                        }

                        data.Add(new ApplicationData { 
                            Value = _value,
                            windowName = Lib.getHWDText(hwnd),
                            screen = (scr +1).ToString()
                        });
                        name.Add(Lib.getHWDText(hwnd));
                    }
                }
            }

            foreach (Process p in Process.GetProcesses())
            {
                _value = false;
                if (Filters.ValidProcessNuNEx(p.ProcessName, p.MainWindowTitle) && !name.Contains(p.MainWindowTitle))
                {
                    if (Bucket.AppExceptions != null && Bucket.AppExceptions.Contains(Lib.getHWDText(p.MainWindowHandle)))
                    {
                        _value = true;
                    }

                    data.Add(new ApplicationData
                    {
                        Value = _value,
                        windowName = p.MainWindowTitle,
                        screen = (lasstactiveWindow + 1).ToString()
                    });

                }
            }

            SHDocVw.ShellWindows shell = new SHDocVw.ShellWindows();

            foreach (SHDocVw.InternetExplorer ie in shell)
            {
                IntPtr ptr = new IntPtr(ie.HWND);

                if (!name.Contains(Lib.getHWDText(ptr)))
                {

                    if (Bucket.AppExceptions != null && Bucket.AppExceptions.Contains(Lib.getHWDText(ptr)))
                    {
                        _value = true;
                    }

                    data.Add(new ApplicationData
                    {
                        Value = _value,
                        windowName = Lib.getHWDText(ptr),
                        screen = (lasstactiveWindow + 1).ToString()
                    });

                }
            }           
            return data;
        }

        public static ObservableCollection<ProcessData> ProcessList()
        {
            ObservableCollection<ProcessData> data = new ObservableCollection<ProcessData>();
            foreach (int scr in Bucket.apps.Keys)
            {
                foreach (IntPtr hwnd in Bucket.apps[scr])
                {
                    int pid = Lib.GetProcessThreadFromWindow(hwnd.ToInt32());
                    bool check = false;

                    foreach (ProcessData inst in data)
                    {
                        if (inst.processName == Process.GetProcessById(pid).ProcessName)
                        {
                            check = true;
                            inst.applicationName = inst.applicationName + " |" + Lib.getHWDText(hwnd) + "|";
                        }
                    }

                    if (!check)
                    {
                        if (Lib.getHWDText(hwnd) != "")
                        {
                            bool _value = false;
                            if (Bucket.ProcExceptions != null && Bucket.ProcExceptions.Contains(Process.GetProcessById(pid).ProcessName))
                            {
                                _value = true;
                            }

                            data.Add(new ProcessData
                            {
                                Value = _value,
                                processName = Process.GetProcessById(pid).ProcessName,
                                applicationName = Lib.getHWDText(hwnd)
                            });
                        }                        
                    }
                }
            }
            return data;
        }

        public static List<string> exList()
        {
            return Bucket.AppExceptions;
        }

        public static List<string> pexList()
        {
            return Bucket.ProcExceptions;
        }

        public static void AddXlist(List<string> xlist,List<string>pxlist)
        {
            Bucket.ProcExceptions.Clear();
            Bucket.AppExceptions.Clear();

            foreach (string str in xlist)
            {
                Bucket.AppExceptions.Add(str);
            }

            foreach (string str in pxlist)
            {
                Bucket.ProcExceptions.Add(str);
            }
        }       

        public static void Save()
        {
            try
            {
                
                if (Properties.Settings1.Default.AppException == null)
                {
                    Properties.Settings1.Default.AppException = new System.Collections.Specialized.StringCollection();
                    foreach (string title in Bucket.AppExceptions)
                    {
                        Properties.Settings1.Default.AppException.Add(title);
                    }
                }
                else
                {
                    Properties.Settings1.Default.AppException.Clear();

                    foreach (string title in Bucket.AppExceptions)
                    {
                        Properties.Settings1.Default.AppException.Add(title);
                    }
                }


                if (Properties.Settings1.Default.ProcException == null)
                {
                    Properties.Settings1.Default.ProcException = new System.Collections.Specialized.StringCollection();
                    foreach (string title in Bucket.ProcExceptions)
                    {
                        Properties.Settings1.Default.ProcException.Add(title);
                    }
                }
                else
                {
                    Properties.Settings1.Default.ProcException.Clear();

                    foreach (string title in Bucket.ProcExceptions)
                    {
                        Properties.Settings1.Default.ProcException.Add(title);
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Properties.Settings1.Default.Save();
        }

        public void MoveLast(int last)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i > (last-1))
                {
                    if (!Bucket.apps.ContainsKey(i))
                    {
                        Bucket.apps.Add(i, new List<IntPtr>());
                    }

                    for (int j = 0; j < Bucket.apps[i].Count; j++)
                    {
                        Filters.change(Bucket.apps[i][j], last);
                    }
                }
            }
        }
    }
}
