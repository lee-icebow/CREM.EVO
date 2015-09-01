using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
    public class EvoMaintenceInfo:NotificationObject
    {
        public EvoMaintenceInfo(string a, string b)
        {
            _description = a;
            _value = b;
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; this.RaisePropertyChanged("Description"); }
        }
        private string _value;

        public string Value
        {
            get { return _value; }
            set { _value = value; this.RaisePropertyChanged("Value"); }
        }
    }
    public class Maintence_T
    {
        public MaintenceType type { get; set; }
        public UInt32 value { get; set; }
        public Maintence_T(byte[] buf,int index)
        {
            int pos = index;
            this.type = (MaintenceType)buf[pos++];
            this.value = (UInt32)((buf[pos++] << 24) + (buf[pos++] << 16) + (buf[pos++] << 8) + buf[pos++]);
        }
        public Maintence_T(byte type, UInt32 value)
        {
            this.type = (MaintenceType)type;
            this.value = value;
        }
        public string ToDescription()
        {
            string ret = "not define";
            switch (type)
            {
                case MaintenceType.Total_water_outlet_volume:
                    break;
                case MaintenceType.Hot_water_out_valve_counts:
                    break;
                case MaintenceType.Cold_water_out_valve_counts:
                    break;
                case MaintenceType.Carbonic_water_out_valve_counts:
                    break;
                case MaintenceType.Mix_cold_water_valve_counts:
                    break;
                case MaintenceType.brew_hot_water_valve_counts:
                    break;
                case MaintenceType.brew_motor_counts:
                    break;
                case MaintenceType.MIXER1_water_valve_counts:
                    break;
                case MaintenceType.MIXER2_water_valve_counts:
                    break;
                case MaintenceType.Grinder1_running_time_:
                    break;
                case MaintenceType.Canister1_running_time_:
                    break;
                case MaintenceType.Canister2_running_time_:
                    break;
                case MaintenceType.Canister3_running_time_:
                    break;
                case MaintenceType.Mixer1_whipper_running_time:
                    break;
                case MaintenceType.Mixer2_whipper_running_time:
                    break;
                case MaintenceType.Grinder2_running_time_:
                    break;
                case MaintenceType.Canister4_running_time_:
                    break;
                default:
                    break;
            }
            return this.type.ToString();
        }
        public string ToValue()
        {
            string ret = "0";
            if (type == MaintenceType.Total_water_outlet_volume)
            {
                ret = (this.value << 8).ToString() + "." + ((byte)this.value).ToString();
            }
            else
            {
                ret = this.value.ToString();
            }
                return ret;
        }
    }
    public enum MaintenceType
    {
        Total_water_outlet_volume = 0x01,
        Hot_water_out_valve_counts,
        Cold_water_out_valve_counts,
        Carbonic_water_out_valve_counts,
        Mix_cold_water_valve_counts,
        brew_hot_water_valve_counts,
        brew_motor_counts,
        MIXER1_water_valve_counts,
        MIXER2_water_valve_counts,
        Grinder1_running_time_,
        Canister1_running_time_,
        Canister2_running_time_,
        Canister3_running_time_,
        Mixer1_whipper_running_time,
        Mixer2_whipper_running_time,
        Grinder2_running_time_,
        Canister4_running_time_

    }
}
