using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CREM.EVO.MODEL
{
    #region Espresso process extern interface
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    [XmlRootAttribute("Espresso")]
    public class Ingredient_Espresso_extern : NotificationObject
    {
        public Ingredient_Espresso_extern() { }
        private UInt16 _WaterVolume;
        [XmlAttribute("WaterVolume")]
        public UInt16 WaterVolume
        {
            get { return _WaterVolume; }
            set { if (value != _WaterVolume) { _WaterVolume = value; this.RaisePropertyChanged("WaterVolume"); } }
        } //0-300ml  水量
        private UInt16 _Grind1Cnt;
        [XmlAttribute("Grind1Cnt")]
        public UInt16 Grind1Cnt
        {
            get { return _Grind1Cnt; }
            set { if (value != _Grind1Cnt) { _Grind1Cnt = value; this.RaisePropertyChanged("Grind1Cnt"); } }
        }   //0-50g 磨豆机1粉量
        private UInt16 _Grind2Cnt;
        [XmlAttribute("Grind2Cnt")]
        public UInt16 Grind2Cnt
        {
            get { return _Grind2Cnt; }
            set { if (value != _Grind2Cnt) { _Grind2Cnt = value; this.RaisePropertyChanged("Grind2Cnt"); } }
        }   //0-50g 磨豆机2粉量 
        private UInt16 _Tm_Infusio;
        [XmlAttribute("Tm_Infusion")]
        public UInt16 Tm_Infusio
        {
            get { return _Tm_Infusio; }
            set { if (value != _Tm_Infusio) { _Tm_Infusio = value; this.RaisePropertyChanged("Tm_Infusio"); this.RaisePropertyChanged("UsedTime"); } }
        }  //浸泡水量
        private UInt16 _Tm_Pause;
        [XmlAttribute("Tm_Pause")]
        public UInt16 Tm_Pause
        {
            get { return _Tm_Pause; }
            set { if (value != _Tm_Pause) { _Tm_Pause = value; this.RaisePropertyChanged("Tm_Pause"); this.RaisePropertyChanged("UsedTime"); } }
        }    //浸泡等待时间
        private UInt16 _Tm_Press;
        [XmlAttribute("Tm_Press")]
        public UInt16 Tm_Press
        {
            get { return _Tm_Press; }
            set { if (value != _Tm_Press) { _Tm_Press = value; this.RaisePropertyChanged("Tm_Press"); this.RaisePropertyChanged("UsedTime"); } }
        }    //压力时间 
        private UInt16 _Tm_DelayOpen;
        [XmlAttribute("Tm_DelayOpen")]
        public UInt16 Tm_DelayOpen
        {
            get { return _Tm_DelayOpen; }
            set { if (value != _Tm_DelayOpen) { _Tm_DelayOpen = value; this.RaisePropertyChanged("Tm_DelayOpen"); this.RaisePropertyChanged("UsedTime"); } }
        }//出饮料时间


        [XmlAttribute("UsedTime")]
        public UInt16 UsedTime
        {
            get
            {
                return (UInt16)(Tm_Press + Tm_Pause + Tm_Infusio + Tm_DelayOpen);
            }
            set { }
        }
    } 
    #endregion

    #region filter brew extern interface
    
    [XmlRootAttribute("FilterBrew")]
    public class Ingredient_Filter_Brew_extern:NotificationObject
    {
        public Ingredient_Filter_Brew_extern() { }
        private UInt16 _WaterVolume;
        [XmlAttribute("WaterVolume")]
        public UInt16 WaterVolume
        {
            get { return _WaterVolume; }
            set { if (value != _WaterVolume) { _WaterVolume = value; this.RaisePropertyChanged("WaterVolume"); this.RaisePropertyChanged("UsedTime"); } }
        } //0-300ml  水量

        private UInt16 _Grind1Type;
        [XmlAttribute("Grind1Type")]
        public UInt16 Grind1Type
        {
            get { return _Grind1Type; }
            set { if (value != _Grind1Type) { _Grind1Type = value; this.RaisePropertyChanged("Grind1Type"); } }
        }   
        //0-50g 磨豆机1粉量
        private UInt16 _Grind1Cnt;
        [XmlAttribute("Grind1Cnt")]
        public UInt16 Grind1Cnt
        {
            get { return _Grind1Cnt; }
            set { if (value != _Grind1Cnt) { _Grind1Cnt = value; this.RaisePropertyChanged("Grind1Cnt"); this.RaisePropertyChanged("UsedTime"); } }
        }   //0-50g 磨豆机1粉量

        private UInt16 _Grind2Type;
        [XmlAttribute("Grind2Type")]
        public UInt16 Grind2Type
        {
            get { return _Grind2Type; }
            set { if (value != _Grind2Type) { _Grind2Type = value; this.RaisePropertyChanged("Grind2Type"); } }
        } 
        private UInt16 _Grind2Cnt;
        [XmlAttribute("Grind2Cnt")]
        public UInt16 Grind2Cnt
        {
            get { return _Grind2Cnt; }
            set { if (value != _Grind2Cnt) { _Grind2Cnt = value; this.RaisePropertyChanged("Grind2Cnt"); this.RaisePropertyChanged("UsedTime"); } }
        }   //0-50g 磨豆机2粉量 
        private UInt16 _Tm_Pre;
        [XmlAttribute("Tm_Pre")]
        public UInt16 Tm_Pre
        {
            get { return _Tm_Pre; }
            set { if (value != _Tm_Pre) { _Tm_Pre = value; this.RaisePropertyChanged("Tm_Pre"); this.RaisePropertyChanged("UsedTime"); } }
        }       //预冲泡等待时间
        private UInt16 _Tm_Press;
        [XmlAttribute("Tm_Press")]
        public UInt16 Tm_Press
        {
            get { return _Tm_Press; }
            set { if (value != _Tm_Press) { _Tm_Press = value; this.RaisePropertyChanged("Tm_Press"); this.RaisePropertyChanged("UsedTime"); } }
        }     //压力时间
        private UInt16 _Tm_Depress;
        [XmlAttribute("Tm_Depress")]
        public UInt16 Tm_Depress
        {
            get { return _Tm_Depress; }
            set { if (value != _Tm_Depress) { _Tm_Depress = value; this.RaisePropertyChanged("Tm_Depress"); this.RaisePropertyChanged("UsedTime"); } }
        }   //负压时间
        private UInt16 _Tm_DelayOpen;
        [XmlAttribute("Tm_DelayOpen")]
        public UInt16 Tm_DelayOpen
        {
            get { return _Tm_DelayOpen; }
            set { if (value != _Tm_DelayOpen) { _Tm_DelayOpen = value; this.RaisePropertyChanged("Tm_DelayOpen"); this.RaisePropertyChanged("UsedTime"); } }
        } //出饮料时间

        private byte _actionUpPostion;//first up postion
        [XmlAttribute("UpPostion")]
        public byte ActionUpPostion
        {
            get { return _actionUpPostion; }
            set { if (_actionUpPostion != value) { _actionUpPostion = value; this.RaisePropertyChanged("ActionUpPostion"); } }
        }
        private byte _actionDownPostion;//first down postion
        [XmlAttribute("DownPostion")]
        public byte ActionDownPostion
        {
            get { return _actionDownPostion; }
            set { if (_actionDownPostion != value) { _actionDownPostion = value; this.RaisePropertyChanged("ActionDownPostion"); } }
        }
        [XmlAttribute("UsedTime")]
        public UInt16 UsedTime
        {
            get
            {
                return (UInt16)(Tm_Press + Tm_Pre + Tm_Depress + Tm_DelayOpen);
            }
            set { }
        }
    }
    #endregion

    #region Instant Powerder Process extern interface
    
    [XmlRootAttribute("InstantPowder")]
    public class Ingredient_InstantPowder_extern:NotificationObject
    {
        public Ingredient_InstantPowder_extern() { }

        private UInt16 _MixIndex;
        [XmlAttribute("MixIndex-RSV")]
        public UInt16 MixIndex
        {
            get { return _MixIndex; }
            set { if (value != _MixIndex) { _MixIndex = value; this.RaisePropertyChanged("MixIndex"); } }
        }
        private UInt16 _WaterVolume;
        [XmlAttribute("WaterVolume")]
        public UInt16 WaterVolume
        {
            get { return _WaterVolume; }
            set { if (value != _WaterVolume) { _WaterVolume = value; this.RaisePropertyChanged("WaterVolume"); this.RaisePropertyChanged("UsedTime"); } }
        } //0-300ml  水量

        private UInt16 _PackageOneType;
        [XmlAttribute("PackageOneType")]
        public UInt16 PackageOneType
        {
            get { return _PackageOneType; }
            set { if (value != _PackageOneType) { _PackageOneType = value; this.RaisePropertyChanged("PackageOneType"); } }
        }//Canister motor 1的粉量

        private UInt16 _PackageOneAmt;
        [XmlAttribute("PackageOneAmt")]
        public UInt16 PackageOneAmt
        {
            get { return _PackageOneAmt; }
            set { if (value != _PackageOneAmt) { _PackageOneAmt = value; this.RaisePropertyChanged("PackageOneAmt"); } }
        }//Canister motor2 的粉量 如果有填入实际值，没有填零	

        private UInt16 _PackageTwoType;
        [XmlAttribute("PackageTwoType")]
        public UInt16 PackageTwoType
        {
            get { return _PackageTwoType; }
            set { if (value != _PackageTwoType) { _PackageTwoType = value; this.RaisePropertyChanged("PackageTwoType"); } }
        }//Canister motor 3的粉量 如果有填入实际值，没有填零

        private UInt16 _PackageTwoAmt;
        [XmlAttribute("PackageTwoAmt")]
        public UInt16 PackageTwoAmt
        {
            get { return _PackageTwoAmt; }
            set { if (value != _PackageTwoAmt) { _PackageTwoAmt = value; this.RaisePropertyChanged("PackageTwoAmt"); } }
        }//Canister motor 3的粉量 如果有填入实际值，没有填零

        private UInt16 _PreFlush;
        [XmlAttribute("PreFlush")]
        public UInt16 PreFlush
        {
            get { return _PreFlush; }
            set { if (value != _PreFlush) { _PreFlush = value; this.RaisePropertyChanged("PreFlush"); this.RaisePropertyChanged("UsedTime"); } }
        }    //预冲洗

        private UInt16 _AfterFlush;
        [XmlAttribute("AfterFlush")]
        public UInt16 AfterFlush
        {
            get { return _AfterFlush; }
            set { if (value != _AfterFlush) { _AfterFlush = value; this.RaisePropertyChanged("AfterFlush"); this.RaisePropertyChanged("UsedTime"); } }
        }  //最后冲洗

        private UInt16 _WhipperSpeed;
        [XmlAttribute("WhipperSpeed")]
        public UInt16 WhipperSpeed
        {
            get { return _WhipperSpeed; }
            set { if (value != _WhipperSpeed) { _WhipperSpeed = value; this.RaisePropertyChanged("WhipperSpeed"); } }
        }//搅拌强度
        
        [XmlAttribute("Volume")]
        public UInt16 UsedTime
        {
            get
            {
                return (UInt16)(_PreFlush + _AfterFlush + _WaterVolume);
            }
            set { }
        }

        private byte _waterType;
        [XmlAttribute("WaterType")]
        public byte WaterType //水的类型
        {
          get { return _waterType; }
            set { if (_waterType != value) { _waterType = value; this.RaisePropertyChanged("WaterType"); } }
        }
        
       

    }
#endregion

    #region Fresh_Milk Process
    [XmlRootAttribute("FreshMilk")]
    public class Ingredient_Fresh_Milk_extern:NotificationObject
    {
        private byte _HeaterFlag;
        [XmlAttribute("HeaterFlag")]
        public byte HeaterFlag
        {
            get { return _HeaterFlag; }
            set { if (value != _HeaterFlag) { _HeaterFlag = value; this.RaisePropertyChanged("HeaterFlag"); this.RaisePropertyChanged("UsedTime"); } }
        }     //1	是否开启加热	
        private UInt16 _MilkVolume;
        [XmlAttribute("MilkVolume")]
        public UInt16 MilkVolume
        {
            get { return _MilkVolume; }
            set { if (value != _MilkVolume) { _MilkVolume = value; this.RaisePropertyChanged("MilkVolume"); this.RaisePropertyChanged("UsedTime"); } }
        }   //2	milk的量	
        private byte _Preflush;
        [XmlAttribute("Preflush")]
        public byte Preflush
        {
            get { return _Preflush; }
            set { if (value != _Preflush) { _Preflush = value; this.RaisePropertyChanged("Preflush"); this.RaisePropertyChanged("UsedTime"); } }
        }    //3	是否要搅拌	
        private UInt16 _WhipperSpeed;
        [XmlAttribute("WhipperSpeed")]
        public UInt16 WhipperSpeed
        {
            get { return _WhipperSpeed; }
            set { if (value != _WhipperSpeed) { _WhipperSpeed = value; this.RaisePropertyChanged("WhipperSpeed"); this.RaisePropertyChanged("UsedTime"); } }
        } //4	搅拌强度
        private UInt16 _AfterFlush;
        [XmlAttribute("AfterFlush")]
        public UInt16 AfterFlush
        {
            get { return _AfterFlush; }
            set { if (value != _AfterFlush) { _AfterFlush = value; this.RaisePropertyChanged("AfterFlush"); this.RaisePropertyChanged("UsedTime"); } }
        }   //     最后冲洗

        [XmlAttribute("UsedTime")]
        public UInt16 UsedTime
        {
            get
            {
                return (UInt16)(500);
            }
            set { }
        }
    }
#endregion

    #region Water_Process
    [XmlRootAttribute("Water")]
    public class Ingredient_Water : NotificationObject
    {
        private byte _waterType;
        [XmlAttribute("WaterType")]
        public byte WaterType
        {
            get { return _waterType; }
            set { _waterType = value; this.RaisePropertyChanged("WaterType"); }
        }
        private UInt16 _WaterVolume;
        [XmlAttribute("WaterVolume")]
        public UInt16 WaterVolume
        {
            get { return _WaterVolume; }
            set { _WaterVolume = value; this.RaisePropertyChanged("WaterVolume"); }
        }
        [XmlAttribute("UsedTime")]
        public UInt16 UsedTime
        {
            get
            {
                return 500;
            }
            set { }
        }
    }
    #endregion

    public enum IngredientType
    {
        NoSelect      =0x00,
        ESPRESSO      =0x01,
        FILTERBREW    =0x02,
        INSTANTPOWDER =0x04,
        FRESHMILK     =0x03,
        Water =0x05
    }
    public enum InstantWaterType
    {
        HotWater =0x00,
        WarmWater,
        ColdWater
    }
}