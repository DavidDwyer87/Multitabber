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
using System.Windows.Ink;

namespace Options
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainOption : Window
    {
        private DrawingAttributes drawingAttributes = new DrawingAttributes();
        private Color selectedColor = Colors.Transparent;
        bool IsMouseDown = false;
        
        string activeSetting = "";
        public bool state = false;        

        public MainOption(string fill,string stroke, string fground,int displays)
        {
            InitializeComponent();                        
            activeSetting = "sample";

            sample.Fill = new SolidColorBrush(assignColor(fill));
            ellipse1.Fill = new SolidColorBrush(assignColor(fill));
            ellipse2.Fill = new SolidColorBrush(assignColor(fill));

            ellipse1.Stroke = new SolidColorBrush(assignColor(stroke));
            label8.Foreground = new SolidColorBrush(assignColor(fground));

            label10.Content = displays;
            slider2.Value = displays;

            this.Show();
        }

        public void currentColors(Brush fill, Brush stroke, Brush fground)
        {
            sample.Fill = fill;
            ellipse1.Fill = fill;
            ellipse2.Fill = fill;

            ellipse1.Stroke = stroke;

            label8.Foreground = fground;
        }
        
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            state = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            state = false;
            this.Close();
        }        

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;
        }

        private void image1_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;
        }

        /// <summary>
        /// gets or privately sets the selected
        /// Color. When the Color is set 
        /// the CreateAlphaLinearBrush()/UpdateTextBoxes()
        /// and UpdateInk() methods are called
        /// </summary>
        public Color SelectedColor
        {
            get { return selectedColor; }
            private set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    //CreateAlphaLinearBrush();
                    UpdateTextBoxes();
                    UpdateInk();
                    
                     switch(activeSetting)
                     {
                         case "sample":
                             {
                                 sample.Fill = new SolidColorBrush(selectedColor);
                                 ellipse1.Fill = new SolidColorBrush(selectedColor);
                                 ellipse2.Fill = new SolidColorBrush(selectedColor);
                                 break;
                             }
                         case "el1":
                            {
                                ellipse1.Stroke = new SolidColorBrush(selectedColor);
                                break;
                            }
                         case "el2":
                            {
                                label8.Foreground = new SolidColorBrush(selectedColor);
                                break;
                            }
                         default: break;
                     }
                }
            }
        }

        /// <summary>
        /// Updates Ink stroked based on SelectedColor
        /// </summary>
        private void UpdateInk()
        {
            drawingAttributes.Color = SelectedColor;
            drawingAttributes.StylusTip = StylusTip.Ellipse;
            drawingAttributes.Width = 5;

            // Update DA on previewPresenter
            //foreach (Stroke s in previewPresenter.Strokes)
            //{
            //    s.DrawingAttributes = drawingAttributes;
            //}
        }

        /// <summary>
        /// Update text box values based on SelectedColor
        /// </summary>
        private void UpdateTextBoxes()
        {
            //txtAlpha.Text = SelectedColor.A.ToString();
            txtAlphaHex.Text = SelectedColor.A.ToString("X");
            //txtRed.Text = SelectedColor.R.ToString();
            txtRedHex.Text = SelectedColor.R.ToString("X");
            //txtGreen.Text = SelectedColor.G.ToString();
            txtGreenHex.Text = SelectedColor.G.ToString("X");
            //txtBlue.Text = SelectedColor.B.ToString();
            txtBlueHex.Text = SelectedColor.B.ToString("X");
            txtAll.Text = String.Format("#{0}{1}{2}{3}",
                    txtAlphaHex.Text, txtRedHex.Text,
                    txtGreenHex.Text, txtBlueHex.Text);
        }      

        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMouseDown)
                return;

            try
            {
                CroppedBitmap cb = new CroppedBitmap(image1.Source as BitmapSource,
                    new Int32Rect((int)Mouse.GetPosition(image1).X,
                        (int)Mouse.GetPosition(image1).Y, 1, 1));

                byte[] pixels = new byte[4];

                try
                {
                    cb.CopyPixels(pixels, 4, 0);
                }
                catch (Exception)
                {
                    //Ooops
                }

                //Ok now, so update the mouse cursor position and the SelectedColor
                //sample.SetValue(Canvas.LeftProperty, (double)(Mouse.GetPosition(image1).X - 5));
                //sample.SetValue(Canvas.TopProperty, (double)(Mouse.GetPosition(image1).Y - 5));
                image1.InvalidateVisual();
                SelectedColor = Color.FromArgb((byte)255, pixels[2], pixels[1], pixels[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " error getting color code");
            }
        }

        private void image1_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = true;
        }

        private void image1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsMouseDown = false;
        }

        private void sample_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ellipse3.Margin = new Thickness(0, 0, 0, 0);
            activeSetting = "sample";
        }

        private void ellipse1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ellipse3.Margin = new Thickness(140, 0, 0, 0);
            activeSetting = "el1";
        }       

        private void label8_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ellipse3.Margin = new Thickness(0, 65, 0, 0);
            activeSetting = "el2";
        }

        private void ellipse2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ellipse3.Margin = new Thickness(0, 65, 0, 0);
            activeSetting = "el2";
        }

        private void defaultColr_Click(object sender, RoutedEventArgs e)
        {
            string hex = "#FFEB0F0F";
            int a = int.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int r = int.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int b = int.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int g = int.Parse(hex.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            
            sample.Fill = new SolidColorBrush(Color.FromArgb((byte)a, (byte)r, (byte)b, (byte)g));
            ellipse1.Fill = new SolidColorBrush(Color.FromArgb((byte)a, (byte)r, (byte)b, (byte)g));
            ellipse2.Fill = new SolidColorBrush(Color.FromArgb((byte)a, (byte)r, (byte)b, (byte)g));

            ellipse1.Stroke = new SolidColorBrush(Colors.White);
            label8.Foreground = new SolidColorBrush(Colors.White);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                int num = Convert.ToInt16(slider2.Value);
                label10.Content = num;

                stackPanel1.Children.Clear();

                for (int i = 0; i < num; i++)
                {
                    Ellipse el = scrRec();
                    stackPanel1.Children.Add(el);
                }
            }
            catch (Exception)
            {
            }
        }

        private void tabItem2_Loaded(object sender, RoutedEventArgs e)
        {            
            //for (int i = 0; i < 2; i++)
            //{
            //    Ellipse el = scrRec();                
            //    stackPanel1.Children.Add(el);                
            //}            
        }

        Ellipse scrRec()
        {           
            Ellipse ellip = new Ellipse();
            ellip.Fill = sample.Fill;
            ellip.Height = 16;
            ellip.Width = 16;

            ellip.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            ellip.VerticalAlignment = System.Windows.VerticalAlignment.Top;


            return ellip;
        }

        Color assignColor(string c)
        {
            string hex = c;
            int a = int.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int r = int.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int b = int.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            int g = int.Parse(hex.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier);

            return Color.FromArgb((byte)a, (byte)r, (byte)b, (byte)g);
        }

        public void currentPosition(string h,string v)
        {
            switch (h)
            {
                case "left":
                    {
                        radioButton1.IsChecked = true;
                        break;
                    }
                case "center":
                    {
                        radioButton2.IsChecked = true;
                        break;
                    }
                case "right":
                    {
                        radioButton3.IsChecked = true;
                        break;
                    }
                default: break;
            }

            switch (v)
            {
                case "top":
                    {
                        radioButton4.IsChecked = true;
                        break;
                    }
                case "bottom":
                    {
                        radioButton5.IsChecked = true;
                        break;
                    }
                default: break;
            }
        }       

        private void checkBox2_Checked(object sender, RoutedEventArgs e)
        {
            //if (checkBox2.IsChecked == true)
            //{
            //    checkBox2.Content = "Turn off manual Control";
            //}
            //else if(checkBox2.IsChecked == false)
            //{
            //    checkBox2.Content = "Turn on manual Control";
            //}
        }        
    }
}
