using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CREM.EVO.MODEL
{
   [XmlRootAttribute("CLEAN")]
   public class EVOCleaning:NotificationObject
   {
       public BrewCleanProcess brewCleanProcess { get; set; }
       public GrinderCleanProcess grinderCleanProcess { get; set; }
       public MixerCleanProcess mixerCleanProcess { get; set; }
       public EVOCleaning()
       {
           brewCleanProcess = new BrewCleanProcess();
           grinderCleanProcess = new GrinderCleanProcess();
           mixerCleanProcess = new MixerCleanProcess();
       }
   }

    [XmlRootAttribute("BREW")]
   public class BrewCleanProcess : NotificationObject
    {
        private byte _cleanCnt;
        [XmlElementAttribute("COUNT")]
        public byte CleanCnt
        {
            get { return _cleanCnt; }
            set
            {
                if (_cleanCnt != value)
                {
                    _cleanCnt = value; this.RaisePropertyChanged("CleanCnt");
                }

            }
        }
        private UInt16 _waterValveOpenTime;
        [XmlElementAttribute("WaterValveOpenTime")]
        public UInt16 WaterValveOpenTime
        {
            get { return _waterValveOpenTime; }
            set
            {
                if (_waterValveOpenTime != value)
                {
                    _waterValveOpenTime = value; this.RaisePropertyChanged("WaterValveOpenTime");
                }
            }
        }

        private UInt16 _waterPauseTime;
        [XmlElementAttribute("WaterPauseTime")]
        public UInt16 WaterPauseTime
        {
            get { return _waterPauseTime; }
            set
            {
                if (_waterPauseTime != value)
                {
                    _waterPauseTime = value; this.RaisePropertyChanged("WaterPauseTime");
                }
            }
        }
        private UInt16 _waterReleaseTime;
        [XmlElementAttribute("WaterReleaseTime")]
        public UInt16 WaterReleaseTime
        {
            get { return _waterReleaseTime; }
            set
            {
                if (_waterReleaseTime != value)
                {
                    _waterReleaseTime = value; this.RaisePropertyChanged("WaterReleaseTime");
                }
            }
        }

        private UInt16 _brewMotorRunTime;
        [XmlElementAttribute("BrewMotorRunTime")]
        public UInt16 BrewMotorRunTime
        {
            get { return _brewMotorRunTime; }
            set
            {
                if (_brewMotorRunTime != value)
                {
                    _brewMotorRunTime = value; this.RaisePropertyChanged("BrewMotorRunTime");
                }
            }
        }
         
    }
    [XmlRootAttribute("GRINDER")]
    public class GrinderCleanProcess : NotificationObject
    {
        private byte _cleanCnt;
        [XmlElementAttribute("COUNT")]
        public byte CleanCnt
        {
            get { return _cleanCnt; }
            set
            {
                if (_cleanCnt != value)
                {
                    _cleanCnt = value; this.RaisePropertyChanged("CleanCnt");
                }

            }
        }
        private UInt16 _motorRunTime;
        [XmlElementAttribute("MotorRunTime")]
        public UInt16 MotorRunTime
        {
            get { return _motorRunTime; }
            set { _motorRunTime = value; }
        }
    }
    [XmlRootAttribute("MIXER")]
    public class MixerCleanProcess:NotificationObject
    {
        private byte _cleanCnt;
        [XmlElementAttribute("COUNT")]
        public byte CleanCnt
        {
            get { return _cleanCnt; }
            set
            {
                if (_cleanCnt != value)
                {
                    _cleanCnt = value; this.RaisePropertyChanged("CleanCnt");
                }

            }
        }
        private UInt16 _waterValveOpenTime;
        [XmlElementAttribute("WaterValveOpenTime")]
        public UInt16 WaterValveOpenTime
        {
            get { return _waterValveOpenTime; }
            set
            {
                if (_waterValveOpenTime != value)
                {
                    _waterValveOpenTime = value; this.RaisePropertyChanged("WaterValveOpenTime");
                }
            }
        }
        private UInt16 _mixRunDelay;
        [XmlElementAttribute("MixRunDelay")]
        public UInt16 MixRunDelay
        {
            get { return _mixRunDelay; }
            set
            {
                if (_mixRunDelay != value)
                {
                    _mixRunDelay = value; this.RaisePropertyChanged("MixRunDelay");
                }
            }
        }
        private UInt16 _mixRunTime;
        [XmlElementAttribute("MixRunTime")]
        public UInt16 MixRunTime
        {
            get { return _mixRunTime; }
            set
            {
                if (_mixRunTime != value)
                {
                    _mixRunTime = value; this.RaisePropertyChanged("MixRunTime");
                }
            }
        }
    }
}
