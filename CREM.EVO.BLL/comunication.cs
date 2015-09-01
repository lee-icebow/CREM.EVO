using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using CREM.EVO.MODEL;

namespace CREM.EVO.BLL
{
    public class comunication
    {

        private enum SendState
        {
            IDEL,
            SENDING,
            RECEIVEING,
            TIMEOUT
        }
        private static comunication _mycomunication = null;
        private static UInt16 STX = 0xFEFC;
        private SerialPort _mySerialPort = null;
        private string _portname;
        private int _BaudRate;
        private bool _isopen;
        private byte packetNo = 0;
        private List<byte> _CrtSendBuf;
        private object m_lock = new object();
        private byte _crtPacketNo = 0;  //当前发送包序号
        private SendState _crtSendState = SendState.IDEL;
        public ManualResetEvent eventWait = new ManualResetEvent(false);

        public delegate void EVOEventHandler(object sender, EVOData e);
        //用event 关键字声明事件对象
        public static event EVOEventHandler EVOEvent;
        public void AddtoSend(byte[] inData, byte inLen)
        {
            Console.WriteLine("_crtSendState:" + _crtSendState);
            if (_crtSendState != SendState.IDEL)
            {
                //throw new Exception("Com not ready!!");
            }
            else
            {
                _crtSendState = SendState.SENDING;
                byte[] sendbuf = Packet2Cmd(inData, inLen);
                _CrtSendBuf = sendbuf.ToList();
                _InData.Clear();
                PrintCmd(sendbuf);
                Sendcmd(sendbuf, sendbuf.Length);
                _crtSendState = SendState.RECEIVEING;
                tmr.Start();
            }
        }
        public bool IsTransfer;
        public UInt16 CurrentCnt { get; set; }
        public UInt16 TotalCnt { get; set; }
        private void PrintCmd(byte[] cmd)
        {
            string ret = "Send Cmd >> ";
            for (int i = 0; i < cmd.Length; i++)
            {
                ret += " 0X" + cmd[i].ToString("X2");
            }
            Console.WriteLine(ret);
        }
        private DispatcherTimer tmr = new DispatcherTimer();
        private byte[] Packet2Cmd(byte[] inData, byte inLen)
        {
            byte[] sendbuf = new byte[inLen + 5];
            int pos = 0;
            sendbuf[pos++] = (byte)(STX >> 8);
            sendbuf[pos++] = (byte)(STX);
            sendbuf[pos++] = (byte)(2 + inLen);
            sendbuf[pos++] = packetNo;
            _crtPacketNo = packetNo;
            Array.Copy(inData, 0, sendbuf, pos, inLen);
            pos += inLen;
            sendbuf[pos++] = Checksum(inData, inLen, packetNo++);
            return sendbuf;

        }

        private byte Checksum(byte[] inData, byte inLen, byte packetno)
        {
            byte ret = packetno;
            for (int i = 0; i < inLen; i++)
            {
                ret ^= inData[i];
            }
            return ret;
        }
        public comunication()
        {
            _mySerialPort = new SerialPort();
            tmr.Interval = TimeSpan.FromMilliseconds(500);
            tmr.Tick += tmr_Tick;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            Console.WriteLine("Time Out!!!");
            _InData.Clear();
            _crtSendState = SendState.IDEL;
        }
        public static comunication Getinstance()
        {
            if (_mycomunication == null) _mycomunication = new comunication();
            return _mycomunication;
        }
        public void SetComPort(string port, int baudrate)
        {
            if (_mySerialPort != null)
            {
                _portname = port;
                _BaudRate = baudrate;
            }
        }
        public int Open()
        {
            if (_isopen)
            {
                _mySerialPort.Close();
            }
            try
            {
                _mySerialPort.PortName = _portname;
                _mySerialPort.BaudRate = _BaudRate;
                _mySerialPort.DataBits = 8;
                _mySerialPort.Parity = Parity.None;
                _mySerialPort.StopBits = StopBits.One;
                _mySerialPort.ReadTimeout = 10;
                _mySerialPort.Open();
                _isopen = true;
                _mySerialPort.DataReceived += _mySerialPort_DataReceived;
                return 0;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        List<byte> _InData = new List<byte>(512);
        void _mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] inbuf = new byte[512];
            lock (m_lock)
            {
                try
                {
                    int indatalen = _mySerialPort.Read(inbuf, 0, 512);
                    byte[] inb = new byte[indatalen];
                    Array.Copy(inbuf, 0, inb, 0, indatalen);
                    _InData.AddRange(inb.ToList());
                    DealwithDataIn();
                }
                catch (Exception)
                {
                    Console.WriteLine("DataReceive Time Error!");
                }
                
            }

        }

        private void DealwithDataIn()
        {

            string outstr = "Data Received:";
            foreach (var item in _InData)
            {
                outstr += "0x" + item.ToString("X2") + " ";
            }
            Console.WriteLine(outstr);
            if (_crtSendState != SendState.RECEIVEING)
            {

                Console.WriteLine("RECEIVEING ERROR");
                _InData.Clear();
                _crtSendState = SendState.IDEL;
                if (IsTransfer)
                {
                    eventWait.Set();
                }
                return;
            }
            if (_InData.Count >= 5)
            {
                if (_InData[0] != 0xFE || _InData[1] != 0xFC)
                {
                    Console.WriteLine("STX ERROR");
                    _InData.Clear();
                    _crtSendState = SendState.IDEL;
                    if (IsTransfer)
                    {
                        eventWait.Set();
                    }
                    return;
                }
                if (_InData[2] < _InData.Count - 3)
                {
                    _InData.Clear();
                    _crtSendState = SendState.IDEL;
                    if (IsTransfer)
                    {
                        eventWait.Set();
                    }
                    return;
                }
                if (_InData[2] == _InData.Count - 3)
                {
                    tmr.Stop();
                    Console.WriteLine("Receive Ok!!!!!!!!!");
                    byte[] tmp = _InData.ToArray();
                    _InData.Clear();
                    _crtSendState = SendState.IDEL;
                    DecodeMsg(tmp);
                }

            }
        }
        private bool GetCheckSum(byte[] datain,int datalen,byte packetno,byte checksum)
        {
            byte ret = packetno;
            for (int i = 0; i < datalen; i++)
            {
                ret ^= datain[i];
            }
            if (ret == checksum)
            {
                return true;
            }
            return false;
        }
        private void DecodeMsg(object indat)
        {
            lock (m_lock)
            {
                CurrentCnt++;
                eventWait.Set();
                byte[] tmp = (byte[])indat;
                EVOData evodata = new EVOData();
                evodata.datainLen = tmp[2];
                evodata.datainno = tmp[3];
                evodata.datain = new byte[evodata.datainLen-2];
                evodata._cmdType = (CmdType)tmp[4];
                evodata.CheckSum =tmp[tmp.Length-1];
                Array.Copy(tmp, 4, evodata.datain, 0, evodata.datain.Length);
                //TODO:过滤无效的数据包
                if (GetCheckSum(evodata.datain, evodata.datain.Length, evodata.datainno,evodata.CheckSum))
                {
                    if (EVOEvent != null)
                    {
                        EVOEvent(this, evodata);
                    }
                }
                
                //Console.WriteLine(tmp.Length);
            }
        }
        public void Close()
        {
            if (_isopen)
            {
                _mySerialPort.Close();
                _isopen = false;
            }
        }
        public void Sendcmd(byte[] sendbuf, int len)
        {
            lock (m_lock)
            {
                if (_mySerialPort.IsOpen)
                {
                    Thread.Sleep(5);
                    _mySerialPort.Write(sendbuf, 0, len);
                }
            }
        }

    }

    
    public class EVOData
    {
        public CmdType _cmdType { get; set; }
        public byte datainLen { get; set; }
        public byte datainno { get; set; }
        public byte CheckSum { get; set; }
        public byte[] datain { get; set; } 
    }
}
