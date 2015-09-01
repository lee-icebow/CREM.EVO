using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfEToolkits.ShapeDrawings;

namespace CREM.EVO
{
    /// <summary>
    /// EvoStCheck.xaml 的交互逻辑
    /// </summary>
    public partial class EvoStCheck : Window
    {
        public ObservableCollection<string> _secondInfo = new ObservableCollection<string>();

        public ObservableCollection<KeyValuePair<int, double>> valueList_fridge = new ObservableCollection<KeyValuePair<int, double>>();
        public ObservableCollection<KeyValuePair<int, double>> valueList_Water = new ObservableCollection<KeyValuePair<int, double>>();
        public DispatcherTimer Tmr = new DispatcherTimer();
        public MachineInfo _myMachineInfo = new MachineInfo();
        public EvoStCheck(MachineInfo _MachineInfo)
        {
            Tmr.Interval = TimeSpan.FromSeconds(1);
            Tmr.Tick += Tmr_Tick;
            InitializeComponent();
            _myMachineInfo = _MachineInfo;
            this.DataContext = _MachineInfo;
            lbinfo.ItemsSource = _secondInfo;
            //Tmr.Start();
            tm1.ItemsSource = valueList_Water;
            tm2.ItemsSource = valueList_fridge;
        }
        int i = 1;
        void Tmr_Tick(object sender, EventArgs e)
        {
            _secondInfo.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            valueList_Water.Add(new KeyValuePair<int, double>(i, _myMachineInfo.Temp_water));
            valueList_fridge.Add(new KeyValuePair<int, double>(i, _myMachineInfo.Temp_fridge));
            i++;
           
        }

        private void waitingtasks_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Tmr.Interval = TimeSpan.FromSeconds(e.NewValue);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Tmr.Start();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Tmr.Stop();
        }
        private bool _IsSleepMode = false;

        public bool IsSleepMode
        {
            get { return _IsSleepMode; }
            set { _IsSleepMode = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            M2BSleepMode tmp = new M2BSleepMode();
            byte[] sendcmd = tmp.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            M2BSleepMode tmp = new M2BSleepMode(false);
            byte[] sendcmd = tmp.EnCode();
            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
        }
        
    }
}
