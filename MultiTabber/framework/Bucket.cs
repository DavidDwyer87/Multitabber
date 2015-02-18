using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace framework
{
    class Bucket
    {
        public static Dictionary<IntPtr, int> AppPool { get; set; }
        public static List<string> AppExceptions { get; set; }
        public static List<string> ProcExceptions { get; set; }
        public static List<string> internalExceptionList { get; set; }
        public static List<string> appNameList { get; set; }
        public static Dictionary<int,Dictionary<int,Dictionary<string,IntPtr>>> app{get; set;}
        public static Dictionary<int, List<IntPtr>> apps { get; set; }
    }
}
