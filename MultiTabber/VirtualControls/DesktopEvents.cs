using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace VirtualControls
{
    public partial class Dots
    {
        void desktop_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle desk = sender as Rectangle;
            desk.Fill = new SolidColorBrush(Colors.Gray);
        }

        void desktop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          
        }

        void desktop_MouseLeave(object sender, MouseEventArgs e)
        {
            Rectangle desk = sender as Rectangle;
            desk.Fill = new SolidColorBrush(Colors.LightGray);
        }
    }
}
