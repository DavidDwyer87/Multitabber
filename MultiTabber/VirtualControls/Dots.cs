using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;

namespace VirtualControls
{
    public partial class Dots
    {         
        public static string color = "";
        public static string stroke = "";
        public static string fground = "";

        double _left = 0;

        dots _dots = null;
        public static List<Ellipse> ellipseCollection = new List<Ellipse>();
        Dictionary<string, int> Tindex = new Dictionary<string, int>();

        public LabelIndicator li = null;

        public static framework.initialize fw = null;

        Window wnd = null;
        //UIElement rectan = null;

        public void createDots(string colour,string stroke,string foreground,int displays)
        {
            setColor(colour);
            setForeground(foreground);
            setStroke(stroke);

            //(4) Create the object for the tab container
            _dots = new dots();
           
            load(displays);
        }

        #region Color methods
        /// <summary>
        /// (1) Assign the hex value for the 
        ///fill color for the tabs. assign the value to the public accessable varible ("color").
        ///This is to allow the other method have access to the color value for the fill value for the Tabs
        /// </summary>
        /// <param name="_color">set the publicly accessable varible color</param>
        public void setColor(string _color)
        {
            color = _color;
        }

        /// <summary>
        /// (3) Set the stroke color for the tab that is hover over highlighted. 
        /// </summary>
        /// <param name="_color">set the publicly accessable varible stroke</param>
        public void setStroke(string _color)
        {
            stroke = _color;
        }

        /// <summary>
        /// (2) Set the foreground color for the highlighted label indicator. This effect will be
        /// seen when the user hovers over the tabs. The value of the foreground color for label indicator
        /// is pass to a public accesable varible to allow other methods to access the value.
        /// </summary>
        /// <param name="_color">set the publicly accessable varible fground</param>
        public void setForeground(string _color)
        {
            fground = _color;
        }
        #endregion       

        /// <summary>
        /// (1) Create dots for tab control Assign color and name the controls
        /// </summary>
        /// <param name="i">number representation of each virtual window</param>
        /// <returns></returns>
        Ellipse scrTab(int i)
        {
            try
            {
                Ellipse ellip = new Ellipse();
                ellip.Fill = new SolidColorBrush(assignColor(color));
                ellip.Height = 16;
                ellip.Width = 16;

                ellip.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                ellip.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                switch (i)
                {
                    case 0:
                        {
                            ellip.Name = "scr0";
                            break;
                        }
                    case 1:
                        {
                            ellip.Name = "scr1";
                            break;
                        }
                    case 2:
                        {
                            ellip.Name = "scr2";
                            break;
                        }
                    case 3:
                        {
                            ellip.Name = "scr3";
                            break;
                        }
                    case 4:
                        {
                            ellip.Name = "scr4";
                            break;
                        }
                    case 5:
                        {
                            ellip.Name = "scr5";
                            break;
                        }
                    case 6:
                        {
                            ellip.Name = "scr6";
                            break;
                        }
                    case 7:
                        {
                            ellip.Name = "scr7";
                            break;
                        }
                    case 8:
                        {
                            ellip.Name = "scr8";
                            break;
                        }
                    case 9:
                        {
                            ellip.Name = "scr9";
                            break;
                        }
                    default: break;
                }

                ellipseCollection.Add(ellip);
                return ellip;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        /// <summary>
        /// (1) Add the dots to the tab container and create the event handler for each dot.
        /// </summary>
        /// <param name="rec">Add Ellipse tab to sprite to be displayed back to the users</param>
        /// <param name="i">number representation of each virtual window</param>
        void addStack(Ellipse rec, int i)
        {
            try
            {
                switch (i)
                {
                    case 0:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                           
                            rec.Name = "scr0";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 1:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                           

                            rec.Name = "scr1";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 2:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                            

                            rec.Name = "scr2";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }

                    case 3:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                            
                            rec.Name = "scr3";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 4:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                            

                            rec.Name = "scr4";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 5:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                           

                            rec.Name = "scr5";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 6:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                           
                            rec.Name = "scr6";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 7:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                            

                            rec.Name = "scr7";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 8:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                           
                            rec.Name = "scr8";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    case 9:
                        {
                            rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            rec.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            rec.Margin = new Thickness(_left, 78, 0, 0);

                            rec.MouseEnter += new MouseEventHandler(rec_MouseEnter);
                            

                            rec.Name = "scr9";
                            _dots.sprite.Children.Add(rec);
                            break;
                        }
                    default: break;
                }
                _left = _left + 25;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void clearsprite()
        {
            //remove everything except mod-rec
            for (int i = 0; i < _dots.sprite.Children.Count; i++)
            {
                if (_dots.sprite.Children[i].GetType() != typeof(Rectangle))
                {
                    _dots.sprite.Children.Remove(_dots.sprite.Children[i]);
                }
            }
        }

        /// <summary>
        /// (5) Create and load the tabs For tab container.      
        /// </summary>
        /// <param name="Displays">the param set the value on how many tabs to display</param>
        public void load(int Displays)
        {
            try
            {
                //rectan = _dots.sprite.Children[0];
                _dots.sprite.Children.Clear();

                //clearsprite();

                ellipseCollection.Clear();

                _left = 20;

                //add rec
                //((Rectangle)rectan).Width = 27*Displays; 
                //_dots.sprite.Children.Add(rectan);

                for (int i = 0; i < Displays; i++)
                {
                    addStack(scrTab(i), i);
                    Tindex.Add("scr" + i, i + 1);
                }               

                //desktop controll starts here                
                ellipseCollection[0].Height = 18;
                ellipseCollection[0].Width = 18;

                ellipseCollection[0].Fill = new SolidColorBrush(Colors.Gray);
                ellipseCollection[0].Stroke = new SolidColorBrush(Colors.White);

                //Set the selected window
                li = new LabelIndicator();
                li.show(Tindex[ellipseCollection[0].Name].ToString(), ellipseCollection[0].Margin.Left, ellipseCollection[0].Margin.Top-5);

                fw = new framework.initialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error load the display resources for the tabs. "+ex.Message);
            }
        }

        /// <summary>
        /// Covert the string hex decimal value To a color type
        /// </summary>
        /// <param name="c">string hex - decimal value to converted to a color type</param>
        /// <returns>return type is A Color</returns>
        public static Color assignColor(string c)
        {
            string hex = c;
            int a = int.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int r = int.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int b = int.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int g = int.Parse(hex.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

            return Color.FromArgb((byte)a, (byte)r, (byte)b, (byte)g);
        }

        public void changeState(Brush fill,Brush Stroke, Brush foregound)
        {
            //change foreground on the lables color
            fground = foregound.ToString();

            if (Stroke != null)
            {
                stroke = Stroke.ToString();
            }

            //update color varible
            color = fill.ToString();

            //change color on the dots
            foreach (Ellipse e in ellipseCollection)
            {
                if (ellipseCollection[li.activeWindow] != e)
                {
                    e.Fill = fill;
                    if (ellipseCollection[li.activeWindow] == e && Stroke !=null)
                    {
                        e.Stroke = Stroke;
                    }
                }
            }
        }

        /// <summary>
        /// Redraw the amount of tabs that represents virtual desktop needed by the user
        /// </summary>
        /// <param name="count">number virtual windows needed</param>
        public void TabNumber(int count)
        {
            _dots.Margin = new Thickness(0, 0, 0, 0);
            _left = 20;

            ellipseCollection.Clear();
            _dots.sprite.Children.Clear();
            //clearsprite();

            li.activeWindow = count;

            Tindex.Clear();

            try
            {
                ////add rec
                //((Rectangle)rectan).Width = 28 * count;
                //_dots.sprite.Children.Add(rectan);

                for (int i = 0; i < count; i++)
                {
                    addStack(scrTab(i), i);
                    Tindex.Add("scr" + i, i + 1);
                }

                ellipseCollection[count - 1].Height = 18;
                ellipseCollection[count - 1].Width = 18;

                ellipseCollection[count - 1].Fill = new SolidColorBrush(Colors.Gray);
                ellipseCollection[count - 1].Stroke = new SolidColorBrush(Colors.White);

                li = new LabelIndicator();

                if (count == 10)
                {
                    li.show(Tindex[ellipseCollection[count - 1].Name].ToString(), ellipseCollection[count - 1].Margin.Left - 3, ellipseCollection[count - 1].Margin.Top - 5);
                }
                else
                {
                    li.show(Tindex[ellipseCollection[count - 1].Name].ToString(), ellipseCollection[count - 1].Margin.Left, ellipseCollection[count - 1].Margin.Top - 5);
                }

                fw.MoveLast(count);
                li.Display(count - 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Rollback()
        {
            fw.RollBack();           
        }       

        public void Vis(bool flag)
        {
            wnd.Dispatcher.Invoke(new Action(delegate()
            {
                if (flag)
                    wnd.Show();
                else
                    wnd.Hide();

            }), System.Windows.Threading.DispatcherPriority.Render, null);
        }

        public void topMost(bool flag)
        {
            wnd.Dispatcher.Invoke(new Action(delegate()
            {               
                wnd.Topmost = flag;
            }), System.Windows.Threading.DispatcherPriority.Render, null);
        }

        public void setInstance(Window wind)
        {
            wnd = wind;
        }
    }
}
