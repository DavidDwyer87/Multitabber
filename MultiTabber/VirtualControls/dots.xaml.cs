using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VirtualControls
{
    /// <summary>
    /// Interaction logic for dots.xaml
    /// </summary>
    public partial class dots : UserControl
    {
        public static dots instance = null;
        public dots()
        {
            InitializeComponent();
            instance = this;

            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            
        }
    }
}
