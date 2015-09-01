using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CREM.EVO
{
    /// <summary>
    /// EVOToolWin.xaml 的交互逻辑
    /// </summary>
    public partial class EVOToolWin : Window
    {
        public EVOToolWin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "EVO.TOOL.MAKEPIC.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                myProcess.StartInfo.FileName = "EVO.TOOL.USBFILE.exe";
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.Start();
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }  
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EvoDrinkUi tmp = new EvoDrinkUi();
            tmp.Show();
        }

        private void Led_colorset(object sender, RoutedEventArgs e)
        {

        }

        private void Led_clean(object sender, RoutedEventArgs e)
        {
            EVOCleanWin tmp = new EVOCleanWin();
            tmp.Show();
        }

        private void drink_test(object sender, RoutedEventArgs e)
        {
            EvoDrinkTestWin tmp = new EvoDrinkTestWin();
            tmp.Show();
        }

        
    }
}
