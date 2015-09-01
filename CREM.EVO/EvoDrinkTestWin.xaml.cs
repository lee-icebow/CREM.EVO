using System;
using System.Collections.Generic;
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

namespace CREM.EVO
{
    /// <summary>
    /// EvoDrinkTestWin.xaml 的交互逻辑
    /// </summary>
    public partial class EvoDrinkTestWin : Window
    {
        public DispatcherTimer Tmr = new DispatcherTimer();
        public EvoDrinkTestWin()
        {
            InitializeComponent();
            Tmr.Interval = TimeSpan.FromSeconds(1);
            Tmr.Tick += Tmr_Tick;
            btstart.IsEnabled = true;
            btstop.IsEnabled = false;
        }
        private int _Total = 0;
        private int _second = 30;
        private void Tmr_Tick(object sender, EventArgs e)
        {
            //Tmr.Stop();
            _second--;
            if (_second <= 0)
            {
                _second = 30;
                M2BMakeDrink makedrink = new M2BMakeDrink(8);
                byte[] sendcmd = makedrink.EnCode();
                comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
                _Total++;
                tbtotal.Text = string.Format("Total:{0}", _Total);
            }
            else
            {
                tbsecond.Text = string.Format("{0} s", _second);
            }
            
            //Tmr.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Tmr.IsEnabled)
            {
                M2BMakeDrink makedrink = new M2BMakeDrink(8);
                byte[] sendcmd = makedrink.EnCode();
                comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
                _Total++;
                tbtotal.Text = string.Format("Total:{0}", _Total);
                Tmr.Start();
                btstart.IsEnabled = false;
                btstop.IsEnabled = true;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Tmr.IsEnabled)
            {
                _second = 30;
                Tmr.Stop();
                btstart.IsEnabled = true;
                btstop.IsEnabled = false;
            }
            
        }
    }
}
