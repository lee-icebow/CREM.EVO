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
using CREM.EVO.MODEL;
using CREM.EVO.Utility;

namespace CREM.EVO
{
    /// <summary>
    /// EVOTestForm.xaml 的交互逻辑
    /// </summary>
    public partial class EVOTestForm : Window
    {
        public ComSetting _ComSetting { get; set; }
        public EVOTestForm()
        {
            
            InitializeComponent();
            Microsoft.VisualBasic.Devices.Computer pc = new Microsoft.VisualBasic.Devices.Computer();
            if (pc.Ports.SerialPortNames.Count == 0)
            {
                this.comport.Items.Add("No Com Port Use!!!");
                return;
            }
            foreach (string s in pc.Ports.SerialPortNames)
            {
                this.comport.Items.Add(s);
            }
            _ComSetting = new ComSetting();
            LoadComSet();
            this.DataContext = _ComSetting;
            
        }
        private void LoadComSet()
        {
            _ComSetting = (ComSetting)Function.XmlSerializer.LoadFromXml("EVO.com.xml", typeof(ComSetting));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Function.XmlSerializer.SaveToXml("EVO.com.xml", _ComSetting, typeof(ComSetting), null);
            MessageBox.Show("Save OK");                  
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    
}
