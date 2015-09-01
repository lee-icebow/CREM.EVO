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
using System.Windows.Shapes;
using System.Windows.Threading;
using CREM.EVO.BLL;
using CREM.EVO.MODEL;
using CREM.EVO.Utility;

namespace CREM.EVO
{
    /// <summary>
    /// EvoDrinkUi.xaml 的交互逻辑
    /// </summary>
    public partial class EvoDrinkUi : Window
    {
        EvoRecipe _EvoRecipe;
        private UInt16 _crtID = 0;
        public DispatcherTimer Tmr = new DispatcherTimer();
        public EvoDrinkUi()
        {
            InitializeComponent();
            Tmr.Interval = TimeSpan.FromSeconds(1);
            Tmr.Tick += Tmr_Tick;
            Tmr.Start();
            _EvoRecipe = (EvoRecipe)Function.XmlSerializer.LoadFromXml("EVO.Ingredient.xml", typeof(EvoRecipe));
            Btn1.Visibility = System.Windows.Visibility.Hidden;
            Btn2.Visibility = System.Windows.Visibility.Hidden;
            Btn3.Visibility = System.Windows.Visibility.Hidden;
            Btn4.Visibility = System.Windows.Visibility.Hidden;
            Btn5.Visibility = System.Windows.Visibility.Hidden;
            Btn6.Visibility = System.Windows.Visibility.Hidden;
            Btn7.Visibility = System.Windows.Visibility.Hidden;
            Btn8.Visibility = System.Windows.Visibility.Hidden;
            Btn9.Visibility = System.Windows.Visibility.Hidden;
            InitDrinkBtn();
            grd_preselect.Visibility = System.Windows.Visibility.Hidden;
            grd_layout1.Visibility = System.Windows.Visibility.Visible;
            grd_process.Visibility = System.Windows.Visibility.Hidden;
            comunication.EVOEvent += comunication_EVOEvent;
        }

        private void comunication_EVOEvent(object sender, EVOData e)
        {
            Console.WriteLine(e._cmdType.ToString());
            switch (e._cmdType)
            {
                case CmdType.B2M_CMD_CAL:
                    break;
                case CmdType.B2M_CMD_CLEAN:
                    break;
                case CmdType.B2M_CMD_DB_SET:
                    break;
                case CmdType.B2M_CMD_MAINTENCE:
                    break;
                case CmdType.B2M_CMD_MAKE_BERVAGE:
                    break;
                case CmdType.B2M_CMD_MAKE_DRINK:
                    B2MMakeDrink cmd = new B2MMakeDrink(e.datain, 0);
                if (cmd.Result ==1 )
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => { grd_process.Visibility = System.Windows.Visibility.Visible; }));

                }
                    break;
                case CmdType.B2M_CMD_MAKE_INGREDIENT:
                    break;
                case CmdType.B2M_CMD_MATCH:
                    break;
                case CmdType.B2M_CMD_MODE_REQUEST:
                    break;
                case CmdType.B2M_CMD_QUERY:
                    break;
                case CmdType.B2M_CMD_TEST:
                    break;
                case CmdType.M2B_CMD_CAL:
                    break;
                case CmdType.M2B_CMD_CLEAN:
                    break;
                case CmdType.M2B_CMD_DB_SET:
                    break;
                case CmdType.M2B_CMD_MAINTENCE:
                    break;
                case CmdType.M2B_CMD_MAKE_BERVAGE:
                    break;
                case CmdType.M2B_CMD_MAKE_DRINK:
                    break;
                case CmdType.M2B_CMD_MAKE_INGREDIENT:
                    break;
                case CmdType.M2B_CMD_MATCH:
                    break;
                case CmdType.M2B_CMD_MODE_REQUEST:
                    break;
                case CmdType.M2B_CMD_QUERY:
                    break;
                case CmdType.M2B_CMD_TEST:
                    break;
                default:
                    break;
            }
            
        }

        void Tmr_Tick(object sender, EventArgs e)
        {
            tbdata.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ddd");
        }
        private void InitDrinkBtn()
        {
            int cnt = 9;
            if (_EvoRecipe._lstRecipeInfo.Count<9)
            {
                cnt = _EvoRecipe._lstRecipeInfo.Count;
            }
            BinaryReader bimg;
            FileInfo fileinfo;
            byte[] byttmp;
            BitmapImage tmp = new BitmapImage();
            for (int i = 0; i < cnt; i++)
            {
                (this.FindName("Btn" + (i + 1).ToString()) as Button).Visibility = System.Windows.Visibility.Visible;
                (this.FindName("Btn" + (i + 1).ToString()) as Button).Tag = _EvoRecipe._lstRecipeInfo[i];
                if (((RecipeInfo)(this.FindName("Btn" + (i + 1).ToString()) as Button).Tag)._publicInfo.DispenseType == 2)
                {
                    (this.FindName("Btn" + (i + 1).ToString()) as Button).Click += DrinkClick;
                }
                else
                {
                    (this.FindName("Btn" + (i + 1).ToString()) as Button).AddHandler(Button.MouseDownEvent , new RoutedEventHandler(EvoDrinkUi_MouseDown),true);
                    (this.FindName("Btn" + (i + 1).ToString()) as Button).AddHandler(Button.MouseUpEvent, new RoutedEventHandler(EvoDrinkUi_MouseUp), true);
                }
                
                try
                {
                    string filepath = string.Format("DrinkPics\\b{0}.png", _EvoRecipe._lstRecipeInfo[i].ID.ToString());

                    bimg = new BinaryReader(File.Open(filepath, FileMode.Open));
                    fileinfo = new FileInfo(filepath);
                    byttmp = bimg.ReadBytes((int)fileinfo.Length);
                    bimg.Close();
                    tmp = new BitmapImage();
                    tmp.BeginInit();
                    tmp.StreamSource = new MemoryStream(byttmp);
                    tmp.EndInit();
                    (this.FindName("Btn" + (i + 1).ToString()) as Button).Background = new ImageBrush()
                    {
                        ImageSource = tmp
                    };
                }
                catch (Exception)
                {
                    
                }

            }
        }

        private void EvoDrinkUi_MouseDown(object sender, RoutedEventArgs e)
        {
            disablebtn();
            (sender as Button).Visibility = System.Windows.Visibility.Visible;
            _crtID = ((RecipeInfo)(sender as Button).Tag).ID;
            M2BMakeDrink makedrink = new M2BMakeDrink(_crtID);
            byte[] sendcmd = makedrink.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
        }

        private void EvoDrinkUi_MouseUp(object sender, RoutedEventArgs e)
        {
            M2BMakeDrink makedrink = new M2BMakeDrink(_crtID, 0);
            _crtID = ((RecipeInfo)(sender as Button).Tag).ID;
            byte[] sendcmd = makedrink.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
            recoverybtn();
        }

      

        private void DrinkClick(object sender, RoutedEventArgs e)
        {
                grd_preselect.Visibility = System.Windows.Visibility.Visible;
                grd_layout1.Visibility = System.Windows.Visibility.Hidden;
                _crtID = ((RecipeInfo)(sender as Button).Tag).ID;
                InitpreUi(((RecipeInfo)(sender as Button).Tag));
                btn_icon.Background = (sender as Button).Background;
            
        }
        private int _cupcnt = 1;
        private byte _preVolume = 0x80;
        private byte _preStrength = 0x80;
        private byte _preMilk = 0x80;
        private byte _preSugar = 0x80;

        private void InitpreUi(RecipeInfo a)
        {
            _preVolume = 0x80;
            _preStrength = 0x80;
            _preMilk = 0x80;
            _preSugar = 0x80;
            grd_fun.Children.Clear();
            grd_fun.RowDefinitions.Clear();
            EvoPreselection tmp;
            tb_cups.Text = _cupcnt.ToString();
            int grdrowindex = 0;
            if (a._publicInfo.setCups == 1)
            {
                grd_cup.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                grd_cup.Visibility = System.Windows.Visibility.Hidden;

            }
            if (a._publicInfo.setVolume ==1)
            {
                grd_fun.RowDefinitions.Add(new RowDefinition());
                tmp = new EvoPreselection();
                tmp.SetValue(Grid.RowProperty, grdrowindex);
                tmp.Margin = new Thickness(0, 5, 0, 5);
                tmp.Text = "Volume.";
                tmp.ValueChanged += tmp_ValueChanged;

                grd_fun.Children.Add(tmp);
                grdrowindex++;
            }
            if (a._publicInfo.setStrength == 1)
            {
                grd_fun.RowDefinitions.Add(new RowDefinition());
                tmp = new EvoPreselection();
                tmp.SetValue(Grid.RowProperty, grdrowindex);
                tmp.Margin = new Thickness(0, 5,0,5);
                tmp.Text = "Strength.";
                tmp.ValueChanged += tmp_ValueChanged;
                grd_fun.Children.Add(tmp);
                grdrowindex++;
            }
            if (a._publicInfo.setMilk == 1)
            {
                grd_fun.RowDefinitions.Add(new RowDefinition());
                tmp = new EvoPreselection();
                tmp.SetValue(Grid.RowProperty, grdrowindex);
                tmp.Margin = new Thickness(0, 5, 0, 5);
                tmp.Text = "Milk.";
                tmp.ValueChanged += tmp_ValueChanged;

                grd_fun.Children.Add(tmp);
                grdrowindex++;
            }
            if (a._publicInfo.setSugar == 1)
            {
                grd_fun.RowDefinitions.Add(new RowDefinition());
                tmp = new EvoPreselection();
                tmp.SetValue(Grid.RowProperty, grdrowindex);
                tmp.Margin = new Thickness(0, 5, 0, 5);
                tmp.Text = "Sugar.";
                tmp.ValueChanged += tmp_ValueChanged;

                grd_fun.Children.Add(tmp);
                grdrowindex++;
            }
        }

        void tmp_ValueChanged(object sender, int e)
        {
            EvoPreselection tmp = sender as EvoPreselection;
            string title = tmp.Text;
            if (title.StartsWith("Volume"))
            {
                MessageBox.Show("this is Volume!!!");
                _preVolume = (e < 3 ? (byte)e : (byte)(e - 3 + 0x80)); 
            }
            else if (title.StartsWith("Strength"))
            {
                MessageBox.Show("this is Strength!!!");
                _preStrength = (e < 3 ? (byte)e : (byte)(e - 3 + 0x80));
            }
            else if (title.StartsWith("Milk"))
            {
                MessageBox.Show("this is Milk!!!");
                _preMilk = (e < 3 ? (byte)e : (byte)(e - 3 + 0x80));
            }
            else if (title.StartsWith("Sugar"))
            {
                MessageBox.Show("this is Sugar!!!");
                _preSugar = (e < 3 ? (byte)e : (byte)(e - 3 + 0x80));
            }

        }
        private void Btn_start(object sender, RoutedEventArgs e)
        {

            M2BMakeDrink makedrink = new M2BMakeDrink(_crtID);
            makedrink.SetMilk = _preMilk;
            makedrink.SetStrength = _preStrength;
            makedrink.SetSugar = _preSugar;
            makedrink.SetVolume = _preVolume;
            byte[] sendcmd = makedrink.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);

        }

        private void disablebtn()
        {
            int cnt = 9;
            if (_EvoRecipe._lstRecipeInfo.Count < 9)
            {
                cnt = _EvoRecipe._lstRecipeInfo.Count;
            }
            for (int i = 0; i < cnt; i++)
            {
                (this.FindName("Btn" + (i + 1).ToString()) as Button).Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void recoverybtn()
        {
            int cnt = 9;
            if (_EvoRecipe._lstRecipeInfo.Count < 9)
            {
                cnt = _EvoRecipe._lstRecipeInfo.Count;
            }
            for (int i = 0; i < cnt; i++)
            {
                (this.FindName("Btn" + (i + 1).ToString()) as Button).Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void drinkd(object sender, MouseButtonEventArgs e)
        {
            
            
        }

        private void drinku(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_stop(object sender, RoutedEventArgs e)
        {
            grd_process.Visibility = System.Windows.Visibility.Hidden;
            grd_preselect.Visibility = System.Windows.Visibility.Hidden;
            grd_layout1.Visibility = System.Windows.Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            grd_process.Visibility = System.Windows.Visibility.Hidden;
            grd_preselect.Visibility = System.Windows.Visibility.Hidden;
            grd_layout1.Visibility = System.Windows.Visibility.Visible;
        }

        private void cups_down(object sender, RoutedEventArgs e)
        {
            _cupcnt = (_cupcnt > 1 ? _cupcnt - 1 : 1);
            tb_cups.Text = _cupcnt.ToString();

        }

        private void cups_up(object sender, RoutedEventArgs e)
        {
            _cupcnt = (_cupcnt < 12 ? _cupcnt + 1 : 12);
            tb_cups.Text = _cupcnt.ToString();

        }
    }
}
