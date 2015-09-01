using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Drawing;

namespace EVO.TOOL.MAKEPIC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (System.Windows.Media.FontFamily font in Fonts.SystemFontFamilies)  //遍历字体集合中德字体
            {

                cbfont.Items.Add(new Items(font));//将参数传递到自定义控件
            }
        }
        private bool isdrag = false;

        private void c_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //isdrag = true;
        }
        private void c_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //isdrag = false;
        }
        private void c_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (!isdrag) return;
            //var control = (sender as UIElement);
            //control.SetValue(Canvas.LeftProperty, e.GetPosition(container).X - control.DesiredSize.Width / 2);
            //control.SetValue(Canvas.TopProperty, e.GetPosition(container).Y - control.DesiredSize.Height / 2);
            //var vector = VisualTreeHelper.GetOffset(control);
            //this.Title = vector.X + "/" + vector.Y;
        }
        public List<TextBlock> _myLstText = new List<TextBlock>();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                        
        }

        private void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isdrag = false;
           
        }
        private UIElement _crtchd;
        private void Element_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!isdrag) return;
            var control = (sender as UIElement);
            control.SetValue(Canvas.LeftProperty, e.GetPosition(container).X - control.DesiredSize.Width / 2);
            control.SetValue(Canvas.TopProperty, e.GetPosition(container).Y - control.DesiredSize.Height / 2);
            var vector = VisualTreeHelper.GetOffset(control);
            
        }

        private void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isdrag = true;
            _crtchd = sender as UIElement;
           
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextBlock mytext = new TextBlock();
            container.Children.Add(mytext);
            mytext.FontSize = _fontSize;
            mytext.Foreground = _fontColor;
            mytext.Text = TextIn.Text;
            mytext.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            mytext.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            mytext.SetValue(Canvas.TopProperty, (double)imgbk.GetValue(Canvas.HeightProperty)/2);
            mytext.SetValue(Canvas.LeftProperty, (double)imgbk.GetValue(Canvas.WidthProperty)/2);
            mytext.AddHandler(System.Windows.Controls.Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown), true);
            mytext.AddHandler(System.Windows.Controls.Button.MouseMoveEvent, new System.Windows.Input.MouseEventHandler(Element_MouseMove), true);
            mytext.AddHandler(System.Windows.Controls.Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp), true);
            _myLstText.Add(mytext);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var item in _myLstText)
            {
                if (item.Equals((_crtchd as TextBlock)))
                {
                    _myLstText.Remove(item);
                    break;
                }
            }

             container.Children.Remove(_crtchd);
        }
        private double _fontSize = 48;
        private System.Windows.Media.Brush _fontColor = System.Windows.Media.Brushes.Black;
        private System.Windows.Media.FontFamily _FontFamily =null;
        private double _sacleRate = 1;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            BitmapSource bgImage = (BitmapSource)img.Source;
            RenderTargetBitmap composeImage = new RenderTargetBitmap(bgImage.PixelWidth, bgImage.PixelHeight, bgImage.DpiX, bgImage.DpiY, PixelFormats.Default);
            double scaleX =  bgImage.Width/imgbk.ActualWidth;
            double scaleY =  bgImage.Height/imgbk.ActualHeight ;

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(bgImage, new Rect(0, 0, bgImage.Width, bgImage.Height));

            foreach (var item in _myLstText)
            {
                FormattedText signatureTxt = new FormattedText(item.Text,
                                                   System.Globalization.CultureInfo.CurrentCulture,
                                                   System.Windows.FlowDirection.LeftToRight,
                                                   new Typeface(System.Windows.SystemFonts.MessageFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                                                   item.FontSize * scaleX,
                                                   item.Foreground);

                
                
                double x2 = ((double)item.GetValue(Canvas.LeftProperty)-(double)imgbk.GetValue(Canvas.LeftProperty)) * scaleX;
                double y2 = ((double)item.GetValue(Canvas.TopProperty) - (double)imgbk.GetValue(Canvas.TopProperty)) * scaleY;
                drawingContext.DrawText(signatureTxt, new System.Windows.Point(x2, y2));
            }
            drawingContext.Close();
            composeImage.Render(drawingVisual);
            //定义一个JPG编码器
            JpegBitmapEncoder bitmapEncoder = new JpegBitmapEncoder();
            //加入第一帧
            bitmapEncoder.Frames.Add(BitmapFrame.Create(composeImage));

            //保存至文件（不会修改源文件，将修改后的图片保存至程序目录下）


            Microsoft.Win32.SaveFileDialog openFileDialog = new Microsoft.Win32.SaveFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";
            openFileDialog.FilterIndex = 1;
            if ((bool)openFileDialog.ShowDialog())
            {
                string savePath = openFileDialog.FileName;
                FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate);

                bitmapEncoder.Save(fs);
                fs.Close();
            }     
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.Filter = "png文件|*.png|jpg文件|*.jpg|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
            {
                img.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.AllowFullOpen = true;
            colorDialog.ShowDialog();
            System.Windows.Media.SolidColorBrush scb = new System.Windows.Media.SolidColorBrush();
            System.Windows.Media.Color color = new System.Windows.Media.Color();
            color.A = colorDialog.Color.A;
            color.B = colorDialog.Color.B;
            color.G = colorDialog.Color.G;
            color.R = colorDialog.Color.R;
            scb.Color = color;
            _fontColor = new SolidColorBrush(color);
            rectcolor.Fill = new SolidColorBrush(color);
            if (_crtchd !=null)
            {
                (_crtchd as TextBlock).Foreground = rectcolor.Fill;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            fontsize.Text = (sender as Slider).Value.ToString();
            _fontSize = (sender as Slider).Value;
            if (_crtchd != null)
            {
                (_crtchd as TextBlock).FontSize = (sender as Slider).Value;
            }
        }

        private void font_chg(object sender, SelectionChangedEventArgs e)
        {
            object tmp = (sender as System.Windows.Controls.ComboBox).SelectedItem;
            if ( tmp!= null)
            {
                Items myfont = tmp as  Items;
                _FontFamily = myfont.GetFontFamily;
                if (_crtchd!=null)
                {
                    (_crtchd as TextBlock).FontFamily = _FontFamily;
                    
                }
            }
        }

        private void tbwidth_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                imgbk.Width = double.Parse(tbwidth.Text);
                this.Title = imgbk.GetValue(Canvas.LeftProperty).ToString() + "/" + imgbk.GetValue(Canvas.TopProperty).ToString();
               

            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("Error width");
            }
        }

        private void tbheight_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                imgbk.Height = double.Parse(tbheight.Text);
                this.Title = imgbk.GetValue(Canvas.LeftProperty).ToString() + "/" + imgbk.GetValue(Canvas.TopProperty).ToString();
                

            }
            catch (Exception)
            {

                System.Windows.MessageBox.Show("Error Height");
            }
        }

        private void btnscaleup_Click(object sender, RoutedEventArgs e)
        {
            if (_sacleRate *10<=10000)
            {
                _sacleRate = _sacleRate*10;
            }
            var group = mywin.FindResource("myscale") as TransformGroup;
            var transform = group.Children[0] as ScaleTransform;
            transform.ScaleX += 0.1;
            transform.ScaleY += 0.1;
        }

        private void btnscaledown_Click(object sender, RoutedEventArgs e)
        {
            if (_sacleRate / 10 >0.0001)
            {
                _sacleRate = _sacleRate / 10;
            }
            var group = mywin.FindResource("myscale") as TransformGroup;
            var transform = group.Children[0] as ScaleTransform;
            transform.ScaleX -= 0.1;
            transform.ScaleY -= 0.1;
        }

        
        
    }
}
