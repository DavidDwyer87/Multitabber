using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using framework;

namespace VirtualControls
{
    public partial class Dots
    {
        void rec_MouseEnter(object sender, MouseEventArgs e)
        {
            Ellipse el = sender as Ellipse;
            ActiveWindow.window = Lib.GetForegroundWindow();

            //Call the hightLight method to Update the user interface of Which tab the user hover over
            //this set the appropriate color for the stroke on the tab hover over.

            if(ellipseCollection[li.activeWindow] != el )
                el.Stroke = new SolidColorBrush(Dots.assignColor(Dots.stroke));           

            li.HighLight(Tindex[el.Name].ToString(), el.Margin.Left, el.Margin.Top);
        }
    }
}
