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
using CREM.EVO.BLL;
using CREM.EVO.MODEL;

namespace CREM.EVO
{
    /// <summary>
    /// EVOLedColorManage.xaml 的交互逻辑
    /// </summary>
    public partial class EVOCleanWin : Window
    {
        private byte _IsParamCmd;
        public EVOCleanWin()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex!=-1)
            {
                _IsParamCmd =(byte) (sender as ComboBox).SelectedIndex;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            M2BClean mclean = new M2BClean(_IsParamCmd);
            byte[] sendcmd = mclean.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Function not complete!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Function not complete!");

        }
    }
}
