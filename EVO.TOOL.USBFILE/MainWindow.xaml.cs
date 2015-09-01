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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using LibUsbDotNet.DeviceNotify;
using System.Security.Cryptography;
namespace EVO.TOOL.USBFILE
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //UsbDetector usbDetector;

        private IDeviceNotifier devNotifier;

        delegate void AppendNotifyDelegate(string s,LibUsbDotNet.DeviceNotify.Info.UsbDeviceNotifyInfo deviceinfo);
        public MainWindow()
        {
            InitializeComponent();
            _UsbKeyDataStruct = new UsbKeyDataStruct();
            devNotifier = DeviceNotifier.OpenDeviceNotifier();
            devNotifier.OnDeviceNotify += devNotifier_OnDeviceNotify;
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            btnG.IsEnabled =false;
           // byte[] aaa = new byte[12] { 0x35, 0x77, 0x61, 0x54, 0x42, 0x6A, 0x49, 0x44, 0x46, 0x2f, 0x34, 0x3d };
            //byte[] ret = Decrypt(aaa);
           // string aa = System.Text.Encoding.Default.GetString(ret);
            ;
        }

        void devNotifier_OnDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            Console.WriteLine(e.ToString());
            
            switch (e.EventType)
            {
                case EventType.CustomEvent:
                    break;
                case EventType.DeviceArrival:
                    if (e.DeviceType == DeviceType.DeviceInterface)
                    {
                        this.Dispatcher.BeginInvoke(new AppendNotifyDelegate(UpStatusbarInfo), "Connected", e.Device);
                        _UsbKeyDataStruct.PID = e.Device.IdProduct;
                        _UsbKeyDataStruct.VID = e.Device.IdVendor;
                        _UsbKeyDataStruct.Sn = System.Text.Encoding.ASCII.GetBytes(e.Device.SerialNumber);
                    }
                    break;
                case EventType.DeviceQueryRemove:
                    break;
                case EventType.DeviceQueryRemoveFailed:
                    break;
                case EventType.DeviceRemoveComplete:
                    this.Dispatcher.BeginInvoke(new AppendNotifyDelegate(UpStatusbarInfo), "Disconnected", e.Device);
                    break;
                case EventType.DeviceRemovePending:
                    break;
                case EventType.DeviceTypeSpecific:
                    break;
                default:
                    break;
            }
        }
        private int _PID;
        private int _VID;
        private string _SN;
        private void UpStatusbarInfo(string S, LibUsbDotNet.DeviceNotify.Info.UsbDeviceNotifyInfo deviceinfo)
        {
            switch (S)
            {
                case "Connected":
                    tbUSBSt.Foreground = Brushes.Green;
                    tbPID.Text = deviceinfo.IdProduct.ToString();
                    tbVID.Text = deviceinfo.IdVendor.ToString();
                    tbSN.Text = deviceinfo.SerialNumber;
                    btnG.IsEnabled =true;
                    break;
                case "Disconnected":
                    tbUSBSt.Foreground = Brushes.Red;
                    tbPID.Text = "";
                    tbVID.Text = "";
                    tbSN.Text  = "";
            btnG.IsEnabled =false;

                    break;
                default:
                    break;
            }
            tbUSBSt.Text = S;
        }
        private void usbDetector_StateChanged(bool arrival)
        {
            //if (arrival)
            //    Console.WriteLine("ADD");
            //else
            //    Console.WriteLine("Removed");

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


            //byte[] aaa = new byte[12] { 0x35, 0x77, 0x61, 0x54, 0x42, 0x6A, 0x49, 0x44, 0x46, 0x2f, 0x34, 0x3d };
           // byte[] ret = Decrypt(aaa);
           // string aa = System.Text.Encoding.Default.GetString(ret);
            ;
        }

        private UsbKeyDataStruct _UsbKeyDataStruct;
        private string _EncryptKey = "Crem1234";
        /// <summary>
        /// 进行DES加密。
        /// </summary>
        /// <param name="pToEncrypt">要加密的字符串。</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>以Base64格式返回的加密字符串。</returns>
        public byte[] Encrypt(byte[] inputByteArray)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {

                des.Key = ASCIIEncoding.ASCII.GetBytes(_EncryptKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(_EncryptKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string aa= Convert.ToBase64String(ms.ToArray());
                ms.Close();
                byte[] ret = System.Text.Encoding.UTF8.GetBytes(aa);
                return ret;
            }
        }

        /// <summary>
        /// 进行DES解密。
        /// </summary>
        /// <param name="pToDecrypt">要解密的以Base64</param>
        /// <param name="sKey">密钥，且必须为8位。</param>
        /// <returns>已解密的字符串。</returns>
        public byte[] Decrypt(byte[] inputByteArray)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(_EncryptKey);
                des.IV = ASCIIEncoding.ASCII.GetBytes(_EncryptKey);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                byte[] ret = ms.ToArray();
                ms.Close();
                return ret;
            }
        }

        private void btnG_Click(object sender, RoutedEventArgs e)
        {
            byte bytUsrLv =0;
            if (cbkeylv.SelectedItem== null)
            {
                MessageBox.Show("Plz Select the User Level!!");
                return;
            }
            bytUsrLv =byte.Parse((cbkeylv.SelectedItem as ComboBoxItem).Tag.ToString());
            Int32 usId = 0;
            try
            {
                usId = Int32.Parse(tbId.Text);
            }
            catch (Exception)
            {
                
                MessageBox.Show("Plz Insert Valid UserId!!");
                return;
            }
            _UsbKeyDataStruct.UsrLv = bytUsrLv;
            _UsbKeyDataStruct.UsrID = usId;
            byte[] tmp =System.Text.Encoding.ASCII.GetBytes(tbCpr.Text);
            _UsbKeyDataStruct.copyright = new byte[20];
            Array.Copy(tmp, _UsbKeyDataStruct.copyright, (int)(tmp.Length > 20 ? 20 : tmp.Length));
            _UsbKeyDataStruct.EncryptLen =(UInt16)( 4 + 4 + 1 + 4 + _UsbKeyDataStruct.Sn.Length);

            

            byte[] Encrypbuf = _UsbKeyDataStruct.Decode();
            byte[] toDecry = new byte[Encrypbuf.Length - 20];
            
            Array.Copy(Encrypbuf, 20, toDecry, 0, toDecry.Length);
            byte[] doencry = Encrypt(toDecry);

            byte[] Destbuf = new byte[doencry.Length+20];
            //save to file 
            //using (StreamWriter sw = new StreamWriter(@"USBKEY\SCR.TOK",false))
            // {
            //     sw.Write(Encrypbuf,0,
            //     sw.Flush();
            //     sw.Close();
            // }    
            Array.Copy(_UsbKeyDataStruct.copyright, 0, Destbuf, 0, 20);
            Array.Copy(doencry, 0, Destbuf, 20, doencry.Length);

            FileStream fs = new FileStream(@"USBKEY\SCR.TOK", FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            fs.Write(Destbuf, 0, Destbuf.Length);
            bw.Close();
            fs.Close();
            MessageBox.Show("Generate Key finished!!");
        }

        private void btnR_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.Filter = "TOK文件|*.TOK";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
            {
                FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
                BinaryReader bw = new BinaryReader(fs);
                byte[] toDecry = new byte[fs.Length - 20];
                byte[] Destbuf = new byte[fs.Length];

                byte[] readbuf = new byte[fs.Length];
                fs.Read(readbuf, 0, readbuf.Length);
                bw.Close();
                fs.Close();
                Array.Copy(readbuf, 20, toDecry, 0, toDecry.Length);
                byte[] inData = Convert.FromBase64String(System.Text.Encoding.UTF8.GetString(toDecry));

                byte[] Decryptbuf = Decrypt(inData);
               // [0, 21, 0, 0, 99, -121, 0, 0, 5, -113, 3, 0, 1, -78, 7, 69, 68, 66, 50, 52, 56, 50, 53, 32]
                Array.Copy(readbuf, 0, Destbuf, 0, 20);
                Array.Copy(Decryptbuf, 0, Destbuf, 20, Decryptbuf.Length);
                UsbKeyDataStruct tmp = new UsbKeyDataStruct(Destbuf);
                KeyInfo keywin = new KeyInfo(tmp);
                keywin.Show();
            }
        }        
                

    }
    public class UsbKeyDataStruct
    {
        public byte[] copyright { get; set; }
        public UInt16 EncryptLen { get; set; }
        public Int32 PID { get; set; }
        public Int32 VID { get; set; }
        public byte UsrLv { get; set; }
        public Int32 UsrID { get; set; }
        public byte[] Sn { get; set; }
        public byte XorValue { get; set; }
        public UsbKeyDataStruct()
        {
            copyright = new byte[20];
        }
        public byte[] Decode()
        {
            int pos = 0;
            byte[] outbuf = new byte[EncryptLen + 2 + 1 + 20];
            Array.Copy(copyright, 0, outbuf, pos, 20);
            pos += 20;
            outbuf[pos++] = (byte)(EncryptLen >> 8);
            outbuf[pos++] = (byte)(EncryptLen);
            outbuf[pos++] = (byte)(PID >> 24);
            outbuf[pos++] = (byte)(PID >> 16);
            outbuf[pos++] = (byte)(PID >> 8);
            outbuf[pos++] = (byte)(PID);
            outbuf[pos++] = (byte)(VID >> 24);
            outbuf[pos++] = (byte)(VID >> 16);
            outbuf[pos++] = (byte)(VID >> 8);
            outbuf[pos++] = (byte)(VID);
            outbuf[pos++] = UsrLv;
            outbuf[pos++] = (byte)(UsrID >> 24);
            outbuf[pos++] = (byte)(UsrID >> 16);
            outbuf[pos++] = (byte)(UsrID >> 8);
            outbuf[pos++] = (byte)(UsrID);
            Array.Copy(Sn, 0, outbuf, pos, Sn.Length);
            pos += Sn.Length;
            XorValue =0;
            for (int i = 20; i < outbuf.Length-1; i++)
            {
                XorValue += outbuf[i];
            }
            outbuf[pos++] = XorValue;
            return outbuf;
        }
        public UsbKeyDataStruct(byte[] a)
        {
            int pos = 0;
            copyright = new byte[20];
            Array.Copy(a, pos, copyright, 0, 20);
            pos += 20;
            EncryptLen =(UInt16) ((a[pos++] << 8 )+ a[pos++]);
            PID = (Int32)((a[pos++] << 24) + (a[pos++] << 16)+ (a[pos++] << 8) + a[pos++]);
            VID = (Int32)((a[pos++] << 24) + (a[pos++] << 16) + (a[pos++] << 8) + a[pos++]);
            UsrLv = a[pos++];
            UsrID = (Int32)((a[pos++] << 24) + (a[pos++] << 16) + (a[pos++] << 8) + a[pos++]);
            Sn = new byte[EncryptLen - 13];
            Array.Copy(a, pos, Sn, 0, EncryptLen - 13);
        }
    }
 
}
