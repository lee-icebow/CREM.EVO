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
    /// EvoCalibrationWin.xaml 的交互逻辑
    /// </summary>
    public partial class EvoCalibrationWin : Window
    {
        private MachineInfo _MachineInfo;
        public EvoCalibrationWin(MachineInfo _MachineInfo)
        {
            InitializeComponent();
            this._MachineInfo = _MachineInfo;
            this.grd_test.Visibility = Visibility.Hidden;
            grd_test_temp.Visibility = System.Windows.Visibility.Hidden;
            this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
            grd_test_brew.Visibility = System.Windows.Visibility.Hidden;
            this.grd_Process.Visibility = System.Windows.Visibility.Hidden;
            Tmr.Interval = TimeSpan.FromSeconds(5);
            Tmr.Tick += Tmr_Tick;
            Tmrpb.Interval = TimeSpan.FromSeconds(0.1);
            Tmrpb.Tick += Tmrpb_Tick;
            comunication.EVOEvent += comunication_EVOEvent;
        }

        void Tmrpb_Tick(object sender, EventArgs e)
        {
            Tmrpb.Stop();
            _crtpbValue++;
            if (_crtpbValue >= 150)
            {
                this.grd_Process.Visibility = System.Windows.Visibility.Hidden;
                grd_cal.Visibility = System.Windows.Visibility.Visible;
                this.tb_title_cal.Text = this.tb_title.Text;
                return;
            }
            else
            {
                pbar.Value = _crtpbValue;
            }
            Tmrpb.Start();
        }

        void comunication_EVOEvent(object sender, EVOData e)
        {
            if (e._cmdType == CmdType.B2M_CMD_DB_SET)
            {
                B2MDbSetting dbset = new B2MDbSetting(e.datain, 0);
                if (dbset.Result == 1)
                {
                    MessageBox.Show("Calibration finished!");
                }
                else
                {
                    MessageBox.Show("Calibration Failed!");
                }
            }
            if (e._cmdType == CmdType.B2M_CMD_CAL)
            {
                B2MCalibration dbcal = new B2MCalibration(e.datain, 0);
                if (dbcal.Result == 1)
                {
                    if (dbcal.calPartNo <=0x8d )
                    {
                        System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            _crtpbValue = 0;
                            this.grd_Process.Visibility = System.Windows.Visibility.Visible;
                            Tmrpb.Start();
                        }));
                    }
                }
            }
        }

        void Tmr_Tick(object sender, EventArgs e)
        {
            tb_crt_temp.Text = this._MachineInfo.Temp_water.ToString();
            M2BStateQuery _stqury = new M2BStateQuery();
            byte[] sendcmd = _stqury.EnCode();
            try
            {
                lock (this)
                {
                    comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
                }
            }
            catch (Exception)
            {

            }
        }

        private byte _CalPartNo = 0;
        private UInt16 _CalValue = 0;
        public DispatcherTimer Tmr = new DispatcherTimer();
        public DispatcherTimer Tmrpb = new DispatcherTimer();
        private int _crtpbValue = 0;
        /*
         * 
         */
        private void Test_Click(object sender, RoutedEventArgs e)
        {
            Button tmp = sender as Button;
            _CalPartNo = byte.Parse(tmp.Tag.ToString());
            CalibrateType cltype = (CalibrateType)int.Parse(tmp.Tag.ToString());
            this.tb_title.Text = cltype.ToString();

            switch (cltype)
            {
                case CalibrateType.CAL_HOT:
                case CalibrateType.CAL_HOT_BREW:
                case CalibrateType.CAL_HOT_MIX_ONE:
                case CalibrateType.CAL_HOT_MIX_TWO:
                case CalibrateType.CAL_CANISTER_ONE:
                case CalibrateType.CAL_CANISTER_TWO:
                case CalibrateType.CAL_CANISTER_THREE:
                case CalibrateType.CAL_CANISTER_FOUR:
                case CalibrateType.CAL_BREW_100:
                case CalibrateType.CAL_BREW_75:
                case CalibrateType.CAL_BREW_50:
                case CalibrateType.CAL_CARBON:
                case CalibrateType.CAL_COLD:
                case CalibrateType.CAL_COLD_MIX:
                    //grd_test_brew.Visibility = System.Windows.Visibility.Visible;
                    //this.grd_test.Visibility = Visibility.Hidden;
                    //this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
                    //this.grd_test_temp.Visibility = System.Windows.Visibility.Hidden;
                    //this.tb_brew.Text = cltype.ToString();
                    if (MessageBox.Show(this, "Please place the cup,then press the ok to start!", "Calibration", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        M2BCalibration calcmd = new M2BCalibration(_CalPartNo, _CalValue);
                        byte[] sendcmd = calcmd.EnCode();
                        lock (this)
                        {
                            comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
                        }
                        
                        //this.grd_Process.Visibility = System.Windows.Visibility.Visible;
                        //Tmrpb.Start(); 
                    }
                   
                    break;
                case CalibrateType.CAL_TEMPERATURE:
                    grd_test_brew.Visibility = System.Windows.Visibility.Hidden;
                    this.grd_test.Visibility = Visibility.Hidden;
                    this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
                    this.grd_test_temp.Visibility = System.Windows.Visibility.Visible;
                    this.tb_title1.Text = cltype.ToString();
                    //TODO:开启温度查询
                    Tmr.Start();
                    break;
                default:
                    break;
            }
        }

        private void Cal_Click(object sender, RoutedEventArgs e)
        {

            Button tmp = sender as Button;
            if (_CalPartNo != byte.Parse(tmp.Tag.ToString()) || _CalValue == 0)
            {
                MessageBox.Show("Error!");
                return;
            }
            CalibrateType cltype = (CalibrateType)int.Parse(tmp.Tag.ToString());
            this.tb_title_cal.Text = cltype.ToString();
            this.grd_cal.Visibility = System.Windows.Visibility.Visible;
            switch (cltype)
            {
                case CalibrateType.CAL_HOT:
                    CalInport1.Maximum = 1000;
                    CalInport1.Minimum = 50;
                    break;
                case CalibrateType.CAL_HOT_BREW:
                    CalInport1.Maximum = 1000;
                    CalInport1.Minimum = 50;
                    break;
                case CalibrateType.CAL_HOT_MIX_ONE:
                    CalInport1.Maximum = 1000;
                    CalInport1.Minimum = 50;
                    break;
                case CalibrateType.CAL_HOT_MIX_TWO:
                    CalInport1.Maximum = 1000;
                    CalInport1.Minimum = 50;
                    break;
                case CalibrateType.CAL_CANISTER_ONE:
                    CalInport1.Maximum = 100;
                    CalInport1.Minimum = 20;
                    break;
                case CalibrateType.CAL_CANISTER_TWO:
                    CalInport1.Maximum = 100;
                    CalInport1.Minimum = 20;
                    break;
                case CalibrateType.CAL_CANISTER_THREE:
                    CalInport1.Maximum = 100;
                    CalInport1.Minimum = 20;
                    break;
                case CalibrateType.CAL_CANISTER_FOUR:
                    CalInport1.Maximum = 100;
                    CalInport1.Minimum = 20;
                    break;
                case CalibrateType.CAL_BREW_100:
                case CalibrateType.CAL_BREW_75:
                case CalibrateType.CAL_BREW_50:
                    this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
                    break;
                case CalibrateType.CAL_TEMPERATURE:
                    this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
                    break;
                default:
                    break;
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO:Send Cal CMD
            _CalValue = (UInt16)CalInport.Value;
            if (_CalValue == 0)
            {
                MessageBox.Show("Value can not be null!!!");
                return;
            }
            M2BCalibration calcmd = new M2BCalibration(_CalPartNo, _CalValue);
            byte[] sendcmd = calcmd.EnCode();
            lock (this)
            {
                comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
            }
            MessageBox.Show("Calibration is start ,please wait");
            grd_test.Visibility = System.Windows.Visibility.Hidden;
            grd_test_temp.Visibility = System.Windows.Visibility.Hidden;
            this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
            grd_test_brew.Visibility = System.Windows.Visibility.Hidden;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            grd_test_brew.Visibility = System.Windows.Visibility.Hidden;
            this.grd_cal.Visibility = System.Windows.Visibility.Hidden;
            grd_test.Visibility = System.Windows.Visibility.Hidden;
            grd_test_temp.Visibility = System.Windows.Visibility.Hidden;
            Tmr.Stop();

        }

        private void temp_Cal(object sender, RoutedEventArgs e)
        {
           
                M2BDbSetting dbset = new M2BDbSetting();
                DBStruct tmp = new DBStruct();
                tmp.DBID = 0x8d;
                tmp.DBValue = (uint)water_temp.Value;
                dbset._DBGroup.Add(tmp);
                byte[] sendcmd = dbset.EnCode();
                lock (this)
                {
                    comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
                }
                MessageBox.Show("Set Temp Finished");
           
        }

        private void Cal_Button_Click(object sender, RoutedEventArgs e)
        {
            M2BDbSetting dbset = new M2BDbSetting();
            DBStruct tmp = new DBStruct();
            tmp.DBID = _CalPartNo;
            tmp.DBValue = (uint)CalInport1.Value;
            dbset._DBGroup.Add(tmp);
            byte[] sendcmd = dbset.EnCode();
            lock (this)
            {
                comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
            }
            //MessageBox.Show("SET DB");
        }

        private void brew_Cal(object sender, RoutedEventArgs e)
        {
            M2BCalibration calcmd = new M2BCalibration(_CalPartNo, _CalValue);
            byte[] sendcmd = calcmd.EnCode();
            lock (this)
            {
                comunication.Getinstance().AddtoSend(sendcmd, (byte)sendcmd.Length);
            }
        }

        
    }
    public enum CalibrateType
    {
        CAL_HOT = 0x85,
        CAL_COLD = 0x84,
        CAL_CARBON =0x86,
        CAL_HOT_BREW = 0x80,
        CAL_HOT_MIX_ONE = 0x81,
        CAL_HOT_MIX_TWO = 0x82,
        CAL_COLD_MIX =0x83,
        CAL_CANISTER_ONE = 0x87,
        CAL_CANISTER_TWO = 0x8a,
        CAL_CANISTER_THREE = 0x8b,
        CAL_CANISTER_FOUR = 0x8c,
       
        CAL_TEMPERATURE=0x8d,
        CAL_BREW_100 = 0x8e,
        CAL_BREW_75 = 0x8f,
        CAL_BREW_50 = 0x90


    }
}
