using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Threading;

namespace VirtualControls
{
    public class LabelIndicator
    {
        public Label indi = null;
        Label indi2 = null;

        ControlScreen.Indicator ini = null;

        public int activeWindow = 0;

        public LabelIndicator()
        {
            indi = new Label();
            indi.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            indi.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            indi.Foreground = new SolidColorBrush(Colors.WhiteSmoke);           
            

            indi2 = new Label();
            indi2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            indi2.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            indi2.Foreground = new SolidColorBrush(Dots.assignColor(Dots.fground));

            indi2.MouseLeave += new System.Windows.Input.MouseEventHandler(indi2_MouseLeave);
            indi2.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(indi_MouseLeftButtonUp);
            indi2.MouseRightButtonUp += new System.Windows.Input.MouseButtonEventHandler(indi2_MouseRightButtonUp);

            dots.instance.sprite.Children.Add(indi);
            dots.instance.sprite.Children.Add(indi2);
        }

        void indi2_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            int num = Convert.ToInt16(label.Content);
            Dots.fw.changeView(num);
        }

        void indi_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            resetDots();
            Hide();       

            Label lble = sender as Label;
            int num = Convert.ToInt16(lble.Content) - 1;

            Dots.fw.Update_Bucket(activeWindow);

            activeWindow = num;

            Dots.fw.change(num);

            Ellipse ell = Dots.ellipseCollection[num];

            ell.Fill = new SolidColorBrush(Colors.Gray);
            ell.Stroke = new SolidColorBrush(Colors.WhiteSmoke);

            ell.Height = 18;
            ell.Width = 18;

            show(lble.Content.ToString(), lble.Margin.Left, lble.Margin.Top);
            ini = new ControlScreen.Indicator();

            if(num == 9)
                ini.display((num + 1).ToString());
            else
                ini.display(" "+(num + 1).ToString());

            ini = null;
           
        }

        void indi2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label lble = sender as Label;
            int num = Convert.ToInt16(lble.Content) - 1;

            if (indi.Content != null)
            {
                if (!indi.Content.Equals(indi2.Content))
                {
                    Dots.ellipseCollection[num].Stroke = new SolidColorBrush(Colors.Transparent);
                }
            }
            else
            {
                Dots.ellipseCollection[num].Stroke = new SolidColorBrush(Colors.Transparent);
            }

            Hide2();
        }

        public Label show(string Content, double left, double top)
        {
            indi.Content = Content;
            indi.Margin = new System.Windows.Thickness(left+1, top, 0, 0);
            indi.Visibility = System.Windows.Visibility.Visible;

            return indi;
        }

        public Label HighLight(string Content, double left, double top)
        {
            indi2.Content = Content;

            if (indi2.Content.Equals("10"))
            {
                indi2.Margin = new System.Windows.Thickness(left -3, top - 5, 0, 0);
            }
            else
            {
                indi2.Margin = new System.Windows.Thickness(left, top - 5, 0, 0);
            }


            indi2.Foreground = new SolidColorBrush(Dots.assignColor(Dots.fground));
            indi2.Visibility = System.Windows.Visibility.Visible;

            return indi2;
        }

        public void Hide()
        {
            indi.Visibility = System.Windows.Visibility.Hidden;
        }

        public void Hide2()
        {
            indi2.Visibility = System.Windows.Visibility.Hidden;
        }

        #region reset
        void resetDots()
        {
            foreach (Ellipse el in Dots.ellipseCollection)
            {
                el.Height = 16;
                el.Width = 16;

                el.Fill = new SolidColorBrush(Dots.assignColor(Dots.color));
                el.Stroke = new SolidColorBrush(Colors.Transparent);
            }
        }       

        void PublicAccessresetDots()
        {
            foreach (Ellipse e in Dots.ellipseCollection)
            {
                e.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                          new Action(
                              delegate()
                              {
                                  e.Height = 16;
                                  e.Width = 16;

                                  e.Fill = new SolidColorBrush(Dots.assignColor(Dots.color));
                                  e.Stroke = new SolidColorBrush(Colors.Transparent);
                              }
                      ));
            }
        }
        #endregion

        public void Display(int index)
        {
            //MessageBox.Show(Dots.ellipseCollection.Count.ToString());
            if(index < Dots.ellipseCollection.Count && index != activeWindow)
            {
                PublicAccessresetDots();

                Dots.fw.Update_Bucket(activeWindow);
                Dots.fw.change(index);

                activeWindow = index;

                Ellipse ellip = Dots.ellipseCollection[index];

                    indi.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            if (index == 9)
                            {
                                
                                show((index + 1).ToString(), ellip.Margin.Left - 3, ellip.Margin.Top - 5);
                                ini = new ControlScreen.Indicator();
                                ini.display((index + 1).ToString());
                                ini = null;
                            }
                            else
                            {
                                show((index + 1).ToString(), ellip.Margin.Left, ellip.Margin.Top - 5);
                                ini = new ControlScreen.Indicator();
                                ini.display(" " + (index + 1).ToString());
                                ini = null;
                            }
                        }
                ));            

                ellip.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal, new Action(
                    delegate()
                    {
                        ellip.Fill = new SolidColorBrush(Colors.Gray);
                        ellip.Stroke = new SolidColorBrush(Colors.WhiteSmoke);

                        ellip.Height = 18;
                        ellip.Width = 18;                   
                    }
                ));
            }
        }

        public void lableColor(Brush c)
        {
            indi.Foreground = c;
        }       
    }
}
