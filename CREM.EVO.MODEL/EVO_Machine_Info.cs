using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Windows.Media.Imaging;
using System.IO;

namespace CREM.EVO.MODEL
{
    public class EVO_Machine_Info : NotificationObject
    {
        public ObservableCollection<EVOMachine> _lstEVOMachine { get; set; }
    }
    [Serializable]
    [XmlRootAttribute("EVOMachineInfo", IsNullable = false)]
    public class EVOMachine : NotificationObject
    {
        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }      
        private string _Name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name"); } }

        }
        
        private int _MachineID;
        [XmlElementAttribute("MachineID")]
        public int MachineID
        {
            get { return _MachineID; }
            set { if (value != _MachineID) { _MachineID = value; this.RaisePropertyChanged("MachineID"); } }

        }

        private int _ValveAmt;

        public int ValveAmt
        {
            get { return _ValveAmt; }
            set { if (value != _ValveAmt) { _ValveAmt = value; this.RaisePropertyChanged("ValveAmt"); } }

        }

        private int _MixerAmt;

        public int MixerAmt
        {
            get { return _MixerAmt; }
            set { if (value != _MixerAmt) { _MixerAmt = value; this.RaisePropertyChanged("MixerAmt"); } }

        }

        private int _CanisterAmt;

        public int CanisterAmt
        {
            get { return _CanisterAmt; }
            set { if (value != _CanisterAmt) { _CanisterAmt = value; this.RaisePropertyChanged("CanisterAmt"); } }

        }

        private int _BeanHopperAmt;

        public int BeanHopperAmt
        {
            get { return _BeanHopperAmt; }
            set { if (value != _BeanHopperAmt) { _BeanHopperAmt = value; this.RaisePropertyChanged("BeanHopperAmt"); } }

        }

        [XmlElementAttribute("WaterValveList")]
        public ObservableCollection<WaterValve> _WaterValve { get; set; }
        [XmlElementAttribute("MixerUnitList")]
        public ObservableCollection<MixerUnit> _MixerUnit { get; set; }
        [XmlElementAttribute("CanisterUnitList")]
        public ObservableCollection<CanisterUnit> _CanisterUnit { get; set; }
        [XmlElementAttribute("BeanHopperUintList")]
        public ObservableCollection<BeanHopperUint> _BeanHopperUint { get; set; }
        public EVOMachine(bool a)
        {
            _WaterValve = new ObservableCollection<WaterValve>();
            _MixerUnit = new ObservableCollection<MixerUnit>();
            _CanisterUnit = new ObservableCollection<CanisterUnit>();
            _BeanHopperUint = new ObservableCollection<BeanHopperUint>();
        }  
        public EVOMachine()
        { 
        }
    }
    [Serializable]
    [XmlRootAttribute("WaterValve")]
    public class WaterValve : NotificationObject
    {
        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }     
        public WaterValve(string a, int b, int c)
        {
            _Name = a;
            _DeviceIoAdress = b;
            _Flow = c;
        }
        public WaterValve()
        {
            
        }
        private int _DeviceID;
        [XmlAttribute("DeviceID")]
        public int DeviceID
        {
            get { return _DeviceID; }
            set { if (value != _DeviceID) { _DeviceID = value; this.RaisePropertyChanged("DeviceID"); } }
        }
        private string _Name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name"); } }
        }
        
        private int _DeviceIoAdress;
        [XmlElementAttribute("DeviceIoAdress", IsNullable = false)]
        public int DeviceIoAdress
        {
            get { return _DeviceIoAdress; }
            set { if (value != _DeviceIoAdress) { _DeviceIoAdress = value; this.RaisePropertyChanged("DeviceIoAdress"); } }
        }
        
        private int _Flow;
        [XmlElementAttribute("Flow", IsNullable = false)]
        public int Flow
        {
            get { return _Flow; }
            set { if (value != _Flow) { _Flow = value; this.RaisePropertyChanged("Flow"); } }

        }


        public WaterValve copy()
        {
            WaterValve ret = new WaterValve();
            ret.Name = _Name;
            ret.DeviceIoAdress = _DeviceIoAdress;
            ret.Flow = _Flow;
            ret._DeviceID = _DeviceID;
            return ret;
        }
    }
    [Serializable]
    [XmlRootAttribute("MixerUnit")]
    public class MixerUnit : NotificationObject
    {
        public MixerUnit(string a, int b) 
        {
            _Name = a;
            _DeviceIoAdress = b;
        }
        public MixerUnit() { }
        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }
        private int _DeviceID;
        [XmlAttribute("DeviceID")]
        public int DeviceID
        {
            get { return _DeviceID; }
            set { if (value != _DeviceID) { _DeviceID = value; this.RaisePropertyChanged("DeviceID"); } }
        }
        private string _Name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name"); } }
        }

        private int _DeviceIoAdress;
        [XmlElementAttribute("DeviceIoAdress", IsNullable = false)]
        public int DeviceIoAdress
        {
            get { return _DeviceIoAdress; }
            set { if (value != _DeviceIoAdress) { _DeviceIoAdress = value; this.RaisePropertyChanged("DeviceIoAdress"); } }
        }

        private byte _Speed;
        [XmlElementAttribute("SpeedParam", IsNullable = false)]
        public byte Speed
        {
            get { return _Speed; }
            set { if (_Speed != value) { _Speed = value; this.RaisePropertyChanged("Speed"); } }
        }


        public MixerUnit copy()
        {
            MixerUnit ret = new MixerUnit();
            ret.Name = _Name;
            ret.DeviceIoAdress = _DeviceIoAdress;
            ret._DeviceID = _DeviceID;
           
            return ret;
        }
              
    }
    [Serializable]
    [XmlRootAttribute("CanisterUnit")]
    public class CanisterUnit : NotificationObject
    {        
        public CanisterUnit() { }
        public CanisterUnit(string a, int b, int c)
        {
            _Name = a;
            _DeviceIoAdress = b;
            _Flow = c;
        }

        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }
        private int _DeviceID;
        [XmlAttribute("DeviceID")]
        public int DeviceID
        {
            get { return _DeviceID; }
            set { if (value != _DeviceID) { _DeviceID = value; this.RaisePropertyChanged("DeviceID"); } }
        }
        private string _Name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name"); } }
        }

        private int _DeviceIoAdress;
        [XmlElementAttribute("DeviceIoAdress", IsNullable = false)]
        public int DeviceIoAdress
        {
            get { return _DeviceIoAdress; }
            set { if (value != _DeviceIoAdress) { _DeviceIoAdress = value; this.RaisePropertyChanged("DeviceIoAdress"); } }
        }

        private int _Flow;
        [XmlElementAttribute("Flow", IsNullable = false)]
        public int Flow
        {
            get { return _Flow; }
            set { if (value != _Flow) { _Flow = value; this.RaisePropertyChanged("Flow"); } }

        }

        private byte _postion;
        [XmlElementAttribute("Postion", IsNullable = false)]
        public byte Postion
        {
            get { return _postion; }
            set { if (value != _postion) { _postion = value; this.RaisePropertyChanged("Postion"); } }
        }

        private byte _powdertype;
        [XmlElementAttribute("PowderType", IsNullable = false)]
        public byte Powdertype
        {
            get { return _powdertype; }
            set { if (value != _powdertype) { _powdertype = value; this.RaisePropertyChanged("Powdertype"); } }
        }


        public CanisterUnit copy()
        {
            CanisterUnit ret = new CanisterUnit();
            ret.Name = _Name;
            ret.DeviceIoAdress = _DeviceIoAdress;
            ret.Flow = _Flow;
            ret.DeviceID = _DeviceID;
            ret.Postion = _postion;
            ret.Powdertype = _powdertype;
            return ret;
        }

        public string GetPowdertype()
        {
            string ret = "";
            PowderType tmp = (PowderType)_powdertype;
            switch (tmp)
            {
                case PowderType.POWER_NULL:
                    ret = "Not Used";
                    break;
                case PowderType.POWDER_ESPRESSO:
                    ret = "ESPRESSO";
                    break;
                case PowderType.POWDER_CHOCOLATE:
                    ret = "CHOCOLATE";
                    break;
                case PowderType.POWDER_SUGAR:
                    ret = "SUGAR";
                    break;
                case PowderType.POWDER_MILK:
                    ret = "MILK";
                    break;
                default:
                    break;
            }
            return ret;
        }
    }
    [Serializable]
    [XmlRootAttribute("BeanHopperUint")]
    public class BeanHopperUint : NotificationObject
    {
        public BeanHopperUint() { }
        public BeanHopperUint(string a, int b, int c)
        {
            _Name = a;
            _DeviceIoAdress = b;
            _Flow = c;
        }

        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }
        private int _DeviceID;
        [XmlAttribute("DeviceID")]
        public int DeviceID
        {
            get { return _DeviceID; }
            set { if (value != _DeviceID) { _DeviceID = value; this.RaisePropertyChanged("DeviceID"); } }
        }
        private string _Name;
        [XmlAttribute("Name")]
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name"); } }
        }

        private int _DeviceIoAdress;
        [XmlElementAttribute("DeviceIoAdress", IsNullable = false)]
        public int DeviceIoAdress
        {
            get { return _DeviceIoAdress; }
            set { if (value != _DeviceIoAdress) { _DeviceIoAdress = value; this.RaisePropertyChanged("DeviceIoAdress"); } }
        }

        private int _Flow;
        [XmlElementAttribute("Flow", IsNullable = false)]
        public int Flow
        {
            get { return _Flow; }
            set { if (value != _Flow) { _Flow = value; this.RaisePropertyChanged("Flow"); } }

        }

        public BeanHopperUint copy()
        {
            BeanHopperUint ret = new BeanHopperUint();
            ret.Name = _Name;
            ret.DeviceIoAdress = _DeviceIoAdress;
            ret.Flow = _Flow;
            ret.DeviceID = _DeviceID;
            ret.Powdertype = _powdertype;
            return ret;
        }
        private byte _powdertype;
        [XmlElementAttribute("PowderType", IsNullable = false)]
        public byte Powdertype
        {
            get { return _powdertype; }
            set { if (value != _powdertype) { _powdertype = value; this.RaisePropertyChanged("Powdertype"); } }
        }
        public string GetPowdertype()
        {
            string ret = "";
            GrinderType tmp = (GrinderType)(_powdertype+0x80);
            switch (tmp)
            {
                case GrinderType.GRINDER_NULL:
                    ret = "Not Used";
                    break;
                case GrinderType.GRINDER_COFFEE1:
                    ret = "CoffeeBean1";
                    break;
                case GrinderType.GRINDER_COFFEE2:
                    ret = "CoffeeBean2";
                    break;
                default:
                    break;
            }
            
            return ret;
        }
    }

    public class DeviceUnit : NotificationObject
    {
        public DeviceType _Type { get; set; }
        
        private int _DeviceID;
        public int DeviceID
        {
            get { return _DeviceID; }
            set { if (value != _DeviceID) { _DeviceID = value; this.RaisePropertyChanged("DeviceID"); this.RaisePropertyChanged("BkPicSorIn"); } }
        }
        private bool _IsSelected;
        
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (value != _IsSelected) { _IsSelected = value; this.RaisePropertyChanged("IsSelected"); } }

        }   
  
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { if (value != _Name) { _Name = value; this.RaisePropertyChanged("Name");  } }
        }

        private int _DeviceIoAdress;
        private byte _Speed;        
        public int DeviceIoAdress
        {
            get { return _DeviceIoAdress; }
            set { if (value != _DeviceIoAdress) { _DeviceIoAdress = value; this.RaisePropertyChanged("DeviceIoAdress"); } }
        }
        public byte Speed
        {
            get { return _Speed; }
            set { if (_Speed != value) { _Speed = value; this.RaisePropertyChanged("Speed"); } }
        }
        public BitmapImage BkPicSorIn
        {
            get { return Getpicsor(DeviceID.ToString()); }
               set{}
        }
        private BitmapImage Getpicsor(string a)
        {
            BinaryReader bimg;
            FileInfo fileinfo;
            byte[] byttmp;


            BitmapImage tmp = null;
            try
            {
                tmp = new BitmapImage();
                bimg = new BinaryReader(File.Open("DevPics\\" + a + ".jpg", FileMode.Open));
                fileinfo = new FileInfo("DevPics\\"+a+".jpg");
                byttmp = bimg.ReadBytes((int)fileinfo.Length);
                bimg.Close();
                tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.StreamSource = new MemoryStream(byttmp);
                tmp.EndInit();
            }
            catch(Exception e1)
            {
            }
            return tmp;

        }

       
    }
    public enum DeviceType
    {
        DEV_VALVE =0x01,
        DEV_MIXER,
        DEV_CANISTER,
        DEV_HOPPER
    }
    public enum PowderType
    {
        POWER_NULL=0x00,
        POWDER_ESPRESSO,
        POWDER_CHOCOLATE,
        POWDER_SUGAR,
        POWDER_MILK
    }
    public enum GrinderType
    {
        GRINDER_NULL=0x80,
        GRINDER_COFFEE1,
        GRINDER_COFFEE2

    }
}
