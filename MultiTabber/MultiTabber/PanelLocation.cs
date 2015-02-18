using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace MultiTabber
{
    class PanelLocation
    {
        int width = 0;
        int height = 0;

        public PanelLocation()
        {
            width = Screen.PrimaryScreen.Bounds.Width;
            height = Screen.PrimaryScreen.Bounds.Height;  
          
            //temp
            MainWindow.instance.Topmost = true; 
        }

        public void FindOtherScreens()
        {
            MessageBox.Show(Screen.AllScreens.LongLength.ToString());
        }

        public void setLocation(MainWindow mw,string  Hposition,string  Vposition)
        {
            mw.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            mw.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            setHorizon(Hposition);
            setVertical(Vposition);
        }        

        double findCenter(double _width)
        {
            return (width - _width) / 2;   
        }

        public void setHorizon(string Hposition)
        {
            switch(Hposition)
            {
                case "left":
                    {
                        MainWindow.instance.Left = 10;
                        break;
                    }
                case "right":
                    {
                        MainWindow.instance.Left = width - MainWindow.instance.Width; 
                        break;
                    }
                case "center":
                    {
                        MainWindow.instance.Left = findCenter(MainWindow.instance.Width);
                        break;
                    }
                default: break;
            }
        }

        public void setVertical(string Vposition)
        {
            switch (Vposition)
            {
                case "top":
                    {
                        MainWindow.instance.Top = -60;
                        break;
                    }
                case "bottom":
                    {
                        MainWindow.instance.Top = height - 150;
                        break;
                    }
                default: break;
            }
        }
    }
}
