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

namespace EVO.TOOL.USBFILE
{
    /// <summary>
    /// KeyInfo.xaml 的交互逻辑
    /// </summary>
    public partial class KeyInfo : Window
    {
        
        public KeyInfo(UsbKeyDataStruct a)
        {
            InitializeComponent();
            tbCpr.Text = System.Text.Encoding.ASCII.GetString(a.copyright,0,20).TrimEnd('\0');
            tbId.Text = a.UsrID.ToString();
            tbPID.Text = a.PID.ToString();
            tbVID.Text = a.VID.ToString();
            tbSN.Text = System.Text.Encoding.ASCII.GetString(a.Sn);
            cbkeylv.Text = getlv(a.UsrLv);
        }
        private string getlv(byte a)
        {
            if (a==3)
            {
                return "Service";
            }
            if (a==2)
            {
                return "Operator";
            }
            return "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
