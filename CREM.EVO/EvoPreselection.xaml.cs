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

namespace CREM.EVO
{
    /// <summary>
    /// EvoPreselection.xaml 的交互逻辑
    /// </summary>
    public partial class EvoPreselection : UserControl
    {
        public delegate void EventHandler(object sender, int e);
        public event EventHandler ValueChanged;
        public static DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(EvoPreselection));

        [System.ComponentModel.Description("Value")]
        [System.ComponentModel.Category("Value Category")]
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public int Value
        {
            get
            {
                return ((int)(base.GetValue(EvoPreselection.ValueProperty)));
            }
            set
            {
                base.SetValue(EvoPreselection.ValueProperty, value);
                updateModel();
            }
        }


        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(EvoPreselection), new UIPropertyMetadata(default(string), new PropertyChangedCallback(TextChanged)));

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d == null)
                return;
            var statusIcon = d as EvoPreselection;
            statusIcon.tb_txtinfo.Text = (string)e.NewValue;
        }

        [System.ComponentModel.Description("Text")]
        [System.ComponentModel.Category("Text Category")]
        [System.ComponentModel.Browsable(true)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public string Text
        {
            get
            {
                return ((string)(base.GetValue(EvoPreselection.TextProperty)));
            }
            set
            {
                base.SetValue(EvoPreselection.TextProperty, value);
                tb_txtinfo.Text = Text;
            }
        }


        public BitmapImage _emptybk;
        public BitmapImage _fillBk;
        public EvoPreselection()
        {
            InitializeComponent();
            
            BinaryReader bimg;
            FileInfo fileinfo;
            byte[] byttmp;
            try
            {
                _emptybk = new BitmapImage();
                bimg = new BinaryReader(File.Open("Icon\\default.png", FileMode.Open));
                fileinfo = new FileInfo("Icon\\default.png");
                byttmp = bimg.ReadBytes((int)fileinfo.Length);
                bimg.Close();
                _emptybk = new BitmapImage();
                _emptybk.BeginInit();
                _emptybk.StreamSource = new MemoryStream(byttmp);
                _emptybk.EndInit();

                _fillBk = new BitmapImage();
                bimg = new BinaryReader(File.Open("Icon\\press.png", FileMode.Open));
                fileinfo = new FileInfo("Icon\\press.png");
                byttmp = bimg.ReadBytes((int)fileinfo.Length);
                bimg.Close();
                _fillBk = new BitmapImage();
                _fillBk.BeginInit();
                _fillBk.StreamSource = new MemoryStream(byttmp);
                _fillBk.EndInit();

            }
            catch (Exception e1)
            {
            }
            this.Value = 3;
        }

        private void updateModel()
        {
            
            btn1.Background = new ImageBrush()
            {
                ImageSource = _emptybk
            };
            btn2.Background = new ImageBrush()
            {
                ImageSource = _emptybk
            };
            btn3.Background = new ImageBrush()
            {
                ImageSource = _emptybk
            };
            btn4.Background = new ImageBrush()
            {
                ImageSource = _emptybk
            };
            btn5.Background = new ImageBrush()
            {
                ImageSource = _emptybk
            };
            for (int i = 1; i <= this.Value; i++)
            {
                (this.FindName("btn"+i.ToString()) as Button).Background = new ImageBrush()
            {
                ImageSource = _fillBk
            };
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
          int tmp =int.Parse(  (sender as Button).Name.Substring(3, 1));
          this.Value = tmp;
          if (ValueChanged!=null)
          {
              ValueChanged(this, this.Value);
          }
        }
    }
}
