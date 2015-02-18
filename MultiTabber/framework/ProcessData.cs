using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace framework
{
    public class ProcessData : INotifyPropertyChanged
    {
        private bool m_value;

        public string processName { get; set; }
        public string applicationName { get; set; }

        public bool Value
        {
            get { return m_value; }
            set
            {
                if (m_value == value)
                    return;
                m_value = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
