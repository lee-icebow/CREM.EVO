using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
    public class M2BTest:IComCmd
    {
        public static byte Turn_on_always = 0x01;
        public static byte Turn_off = 0x00;
        public static byte Turn_on_once = 0x02;

        public byte testID { get; set; }
        public byte testFlag { get; set; }
        public byte testSpeed { get; set; }
        public M2BTest(byte Id,byte flag,byte speed=0)
        {
            testID = Id;
            testFlag = flag;
            testSpeed = speed;
        }
        public byte[] EnCode()
        {
            return new byte[] { 0x09, testFlag, testID, testSpeed };
        }
    }

    public class M2BModeRequest : IComCmd
    {
        
        public byte[] EnCode()
        {
            return new byte[1] { 0x00};
        }
    }

    public class M2BMatch : IComCmd
    {
        public byte[] Sn { get; set; }
        public M2BMatch()
        {
            Sn = new byte[11];
        }
        public byte[] EnCode()
        {
            int pos =0;
            byte[] ret = new byte[12];
            ret[pos] = 0x01;
            Array.Copy(Sn, 0, ret, pos, 11);
            return ret;
        }
        public void Decode(byte[] a)
        {
        }
    }

    public class M2BClean : IComCmd
    {
        public static byte CleanBrew    = 0x00;
        public static byte CleanMilk    = 0x01;
        public static byte CleanGrinder = 0x02;
        public static byte CleanMixer   = 0x03;
        public byte Action { get; set; }
        public M2BClean(byte action)
        {
            Action = action;
        }
        public byte[] EnCode()
        {
            return new byte[2] { 0x02, Action };
        }
    }

    public class M2BStopClean : IComCmd
    {
        public byte[] EnCode()
        {
            return new byte[1] { 0x03 };
        }
    }

    public class M2BMakeDrink : IComCmd
    {
        public static byte OP_START = 0x01;
        public static byte OP_STOP  = 0x00;

        public UInt16 ID { get; set; }
        public byte SetStrength { get; set; }
        public byte SetVolume { get; set; }
        public byte SetMilk { get; set; }
        public byte SetSugar { get; set; }
        public byte SetCups { get; set; }
        public byte SetTopping { get; set; }
        public byte OperateType { get; set; }
        public M2BMakeDrink() { }
        public M2BMakeDrink(UInt16 a,byte opcmd=0x01,byte SetStrength = 0x80, byte SetVolume = 0x80, byte SetMilk = 0x80, byte SetSugar = 0x80, byte SetCups = 0x01,byte SetTopping=0x80)
        {
            ID = a;
            this.SetStrength = SetStrength;
            this.SetVolume = SetVolume;
            this.SetMilk = SetMilk;
            this.SetSugar = SetSugar;
            this.SetCups = SetCups;
            this.OperateType = opcmd;
            this.SetTopping = SetTopping;
        }
        public byte[] EnCode()
        {
            int pos = 0;
            byte[] ret = new byte[10];
            ret[pos++] = 0x06;
            ret[pos++] = (byte)(this.ID >> 8);
            ret[pos++] = (byte)(this.ID );
            ret[pos++] = this.SetStrength;
            ret[pos++] = this.SetVolume;
            ret[pos++] = this.SetMilk;
            ret[pos++] = this.SetSugar;
            ret[pos++] = this.SetCups;
            ret[pos++] = this.SetTopping;
            ret[pos++] = this.OperateType;
            return ret;
        }
    }

    public class M2BMakeIngredient:IComCmd
    {
        /// <summary>
        /// Ingredient structure-filter brew
        /// </summary>
        public class FilterBrewPhaseStep
        {
            public UInt16 motorWaitTime { get; set; }
            public UInt16 WaterFlowTime { get; set; }
            public UInt16 grinder1Type { get; set; }
            public UInt16 grinder1powder { get; set; }
            public UInt16 grinder2Type { get; set; }
            public UInt16 grinder2powder { get; set; }
            public byte[] EnCode()
            {
                int pos = 0;
                byte[] ret = new byte[8+4];
                ret[pos++] = (byte)(motorWaitTime >> 8);
                ret[pos++] = (byte)(motorWaitTime );
                ret[pos++] = (byte)(WaterFlowTime >> 8);
                ret[pos++] = (byte)(WaterFlowTime);
                ret[pos++] = (byte)(grinder1Type >> 8);
                ret[pos++] = (byte)(grinder1Type);
                ret[pos++] = (byte)(grinder1powder >> 8);
                ret[pos++] = (byte)(grinder1powder);
                ret[pos++] = (byte)(grinder2Type >> 8);
                ret[pos++] = (byte)(grinder2Type);
                ret[pos++] = (byte)(grinder2powder >> 8);
                ret[pos++] = (byte)(grinder2powder);
                return ret;
            }
        }
        public class ProcessFilterBrew
        {
            
            public byte PhaseCount { get; set; }
            public List<FilterBrewPhaseStep> FilterBrewPhaseGroup { get; set; }
            public byte postionUp { get; set; }
            public byte postionDown{ get; set; }
            public ProcessFilterBrew()
            {
                PhaseCount = 5;
                postionUp = 50;
                postionDown = 50;
                FilterBrewPhaseGroup = new List<FilterBrewPhaseStep>(PhaseCount);
            }
            public byte[] EnCode()
            {
                int pos = 0;
                byte[] ret = new byte[12 * PhaseCount + 1+2];
                ret[pos++] = PhaseCount;
                foreach (var item in FilterBrewPhaseGroup)
                {
                    Array.Copy(item.EnCode(), 0, ret, pos, 12);
                    pos += 12;
                }
                ret[pos++] = postionUp;
                ret[pos++] = postionDown;
                return ret;
            }
        }
        /// <summary>
        /// Ingredient structure-instant power process
        /// </summary>
        public class ProcessInstantPowder
        {
            public byte MixIndex { get; set; }
            public UInt16 preFlushTime { get; set; } //pre water
            public UInt16 pauseTime { get; set; }
            public UInt16 pauseTime1 { get; set; }
            public UInt16 aftFlushTime { get; set; }
            public UInt16 waterDispenseTime { get; set; }
            public byte whipperSpeed { get; set; } //1=10% max 10
            public byte waterType { get; set; }
            public byte canisterCnt { get; set; } //default=3 高字节为小数点前的值 字节为小数点后的值             
            public List<UInt32> canisterPowderGroup { get; set; }
            public ProcessInstantPowder() {
                canisterCnt = 2;
                canisterPowderGroup = new List<UInt32>(canisterCnt);
            }

            public byte[] EnCode()
            {
                byte[] ret = new byte[14 + 4 * canisterCnt];
                int pos = 0;
                ret[pos++] = MixIndex;
                ret[pos++] = (byte)(preFlushTime >> 8);
                ret[pos++] = (byte)(preFlushTime);
                ret[pos++] = (byte)(pauseTime >> 8);
                ret[pos++] = (byte)(pauseTime);
                ret[pos++] = (byte)(pauseTime1 >> 8);
                ret[pos++] = (byte)(pauseTime1);
                ret[pos++] = (byte)(waterDispenseTime >> 8);
                ret[pos++] = (byte)(waterDispenseTime);
                ret[pos++] = (byte)(aftFlushTime >> 8);
                ret[pos++] = (byte)(aftFlushTime);
                ret[pos++] = whipperSpeed;
                ret[pos++] = waterType;
                ret[pos++] = canisterCnt;
                foreach (var item in canisterPowderGroup)
                {
                    ret[pos++] = (byte)(item >> 24);
                    ret[pos++] = (byte)(item>> 16);
                    ret[pos++] = (byte)(item >> 8);
                    ret[pos++] = (byte)(item );
                }
                return ret;
            }

        }
        /// <summary>
        /// Ingredient structure- milk process
        /// </summary>
        public class ProcessFreshMilk
        {
            public UInt16 preFlushTime { get; set; }
            public UInt16 dispenseMilkTime { get; set; }
            public byte heaterAction { get; set; }
            public UInt16 aftFlushTime { get; set; }
            public byte whipperSpeed { get; set; } //1=10% max 10
            public byte[] EnCode()
            {
                byte[] ret = new byte[8];
                int pos = 0;
                ret[pos++] = (byte)(preFlushTime >> 8);
                ret[pos++] = (byte)(preFlushTime);
                ret[pos++] = (byte)(dispenseMilkTime >> 8);
                ret[pos++] = (byte)(dispenseMilkTime);
                ret[pos++] = (byte)(heaterAction);
                ret[pos++] = (byte)(aftFlushTime >> 8);
                ret[pos++] = (byte)(aftFlushTime);
                ret[pos++] = (byte)(whipperSpeed);
                return ret;
            }
        }
        /// <summary>
        /// TODO:
        /// Ingredient structure – espresso 
        /// </summary>
        public class ProcessEspresso
        {
        }
        /// <summary>
        /// Ingredient structure – Water 
        /// </summary>
        public class ProcessWater
        {
            public byte WaterType { get; set; }
            public UInt16 DispenseTm { get; set; }
            public byte[] EnCode()
            {
                byte[] ret = new byte[3];
                int pos = 0;
                ret[pos++] = (byte)(WaterType);
                ret[pos++] = (byte)(DispenseTm >> 8);
                ret[pos++] = (byte)(DispenseTm);
                return ret;
            }
        }
        public ProcessFilterBrew _ProcessFilterBrew { get; set; }
        public ProcessInstantPowder _ProcessInstantPowder { get; set; }
        public ProcessFreshMilk _ProcessFreshMilk { get; set; }
        public ProcessEspresso _ProcessEspresso { get; set; }
        public ProcessWater _ProcessWater { get; set; }
        public UInt16 ID { get; set; }
        //Operate cmd	description
        //0x01	Delete 
        //0x02	Modify
        //0x03	add
        //0x04	review
        public byte operatetype { get; set; }
        public byte Type { get; set; }
        /// <summary>
        /// Ingredient type	description
        /// 0x01	espresso
        /// 0x02	Filter brew
        /// 0x03	Milk process
        /// 0x04	Instant power process
        /// 0x05	Water process
        /// </summary>
        /// <param name="Type"></param>
        public M2BMakeIngredient(byte Type)
        {
            this.Type = Type;
            switch (Type)
            {
                case 1:
                    _ProcessEspresso = new ProcessEspresso();
                    break;
                case 2:
                    _ProcessFilterBrew = new ProcessFilterBrew();
                    break;
                case 3:
                    _ProcessFreshMilk = new ProcessFreshMilk();
                    break;
                case 4:
                    _ProcessInstantPowder = new ProcessInstantPowder();
                    break;
                case 5:
                    _ProcessWater = new ProcessWater();
                    break;
                default:
                    break;
            }
        }
        public byte[] EnCode()
        {
            int pos = 0;
            byte[] rettmp = new byte[512];
            byte[] tmp;
            rettmp[pos++] = 0x07;
            rettmp[pos++] = operatetype;
            rettmp[pos++] = (byte)(ID >> 8);
            rettmp[pos++] = (byte)(ID);
            rettmp[pos++] = Type;
            switch (Type)
            {
                case 1:
                    //_ProcessEspresso = new ProcessEspresso();
                    break;
                case 2:
                    //_ProcessFilterBrew = new ProcessFilterBrew();
                     tmp =_ProcessFilterBrew.EnCode();
                     Array.Copy(tmp, 0, rettmp, pos, tmp.Length);
                     pos += tmp.Length;
                    break;
                case 3:
                    //_ProcessFreshMilk = new ProcessFreshMilk();
                    tmp = _ProcessFreshMilk.EnCode();
                    Array.Copy(tmp, 0, rettmp, pos , tmp.Length);
                    pos += tmp.Length;
                    break;
                case 4:
                    tmp = _ProcessInstantPowder.EnCode();
                    Array.Copy(tmp, 0, rettmp, pos, tmp.Length);
                    pos += tmp.Length;
                    //_ProcessInstantPowder = new ProcessInstantPowder();
                    break;
                case 5:
                    tmp = _ProcessWater.EnCode();
                    Array.Copy(tmp, 0, rettmp, pos, tmp.Length);
                    pos += tmp.Length;
                    break;
                default:
                    break;
            }
            byte[] ret = new byte[pos];
            Array.Copy(rettmp,0,ret,0,pos);
            return ret;

        }
    }
    
    public class M2BMakeBeverage : IComCmd
    {
        public class IngredientStep
        {
            public UInt16 ID { get; set; }
            public byte _Type { get; set; }
            public UInt16 startTime { get; set; }
            public UInt16 percent { set; get; }
            public byte[] EnCode()
            {
                byte[] ret = new byte[5+2];
                int pos = 0;
                ret[pos++] = (byte)(ID >> 8);
                ret[pos++] = (byte)(ID );
                ret[pos++] = (byte)(_Type);
                ret[pos++] = (byte)(startTime >> 8);
                ret[pos++] = (byte)(startTime);
                ret[pos++] = (byte)(percent >> 8);
                ret[pos++] = (byte)(percent);
                return ret;
            }
        }
        public class AdditionalMsg
        {
            //=0 invalible
            //=1 valible 
            public byte CupSensor { get; set; }
            public byte UseCount { get; set; }
            public byte Ledmode { get; set; }
            public byte LedColor { get; set; }
            public byte LedIntensity { get; set; }
            public byte DispenseType { get; set; }
            public byte setStrength { get; set; }
            public byte setVolume   { get; set; }
            public byte setMilk     { get; set; }
            public byte setSugar    { get; set; }
            public byte setCups     { get; set; }
            public UInt16 price     { get; set; }
            public byte[] EnCode()
            {
                byte[] ret = new byte[13];
                int pos = 0;
                ret[pos++] = (byte)CupSensor;
                ret[pos++] = (byte)UseCount;
                ret[pos++] = (byte)Ledmode;
                ret[pos++] = (byte)LedColor;
                ret[pos++] = (byte)LedIntensity;
                ret[pos++] = (byte)DispenseType;
                ret[pos++] = (byte)setStrength;
                ret[pos++] = (byte)setVolume;
                ret[pos++] = (byte)setMilk;
                ret[pos++] = (byte)setSugar;
                ret[pos++] = (byte)setCups;
                ret[pos++] = (byte)(price >> 8);
                ret[pos++] = (byte)(price);
                return ret;
            }
            public AdditionalMsg(publicInfo item)
            {
                CupSensor       = item.CupSensor;
                UseCount        = item.UseCount     ;
                Ledmode         = item.Ledmode      ;
                LedColor        = item.LedColor     ;
                LedIntensity    = item.LedIntensity ;
                DispenseType    = item.DispenseType ;
                setStrength     = item.setStrength  ;
                setVolume       = item.setVolume    ;
                setMilk         = item.setMilk      ;
                setSugar        = item.setSugar     ;
                setCups         = item.setCups      ;
                price = item.price;
            }
        }
             
        public UInt16 ID { get; set; }
        public byte operatetype { get; set; }
        public byte ingredientCnt { get; set; }
        public List<IngredientStep> igredientStepGroup { get; set; }
        public AdditionalMsg addMsg { get; set; }
        public M2BMakeBeverage(publicInfo item)
        {
            igredientStepGroup = new List<IngredientStep>();
            addMsg = new AdditionalMsg(item);

        }
        public byte[] EnCode()
        {
            ingredientCnt = (byte)igredientStepGroup.Count;
            byte[] rettmp = new byte[7 * ingredientCnt + 13 + 5];
            int pos = 0;
            rettmp[pos++] = 0x08;
            rettmp[pos++] = operatetype;
            rettmp[pos++] = (byte)(ID>>8);
            rettmp[pos++] = (byte)(ID);
            
            rettmp[pos++] = ingredientCnt;
            foreach (var item in igredientStepGroup)
            {
                Array.Copy(item.EnCode(), 0, rettmp, pos, 7);
                pos += 7;
            }
            Array.Copy(addMsg.EnCode(), 0, rettmp, pos, 13);
            pos += 13;
            return rettmp;

        }
    }
    /// <summary>
    /// 3.21	State Query Cmd(状态查询命令 0xA)
    /// </summary>
    public class M2BStateQuery : IComCmd
    {
        public byte[] EnCode()
        {
            return new byte[1] { 0x0a };
        }
    }

    public class M2BTransfer : IComCmd
    {
        public static byte TYPE_FILE =0x01;
        public static byte TYPE_Program = 0x02;
        public byte dataType { get; set; }
        public UInt16 totalCnt { get; set; }
        public UInt16 crtCnt { get; set; }
        public byte[] data { get; set; }
        public M2BTransfer(byte dataType, UInt16 total, UInt16 current, byte[] data, int datalen)
        {
            this.dataType = dataType;
            this.totalCnt = total;
            this.crtCnt = current;
            this.data = new byte[datalen];
            Array.Copy(data, 0, this.data, 0, datalen);
        }
        public byte[] EnCode()
        {
            int pos = 0;
            byte[] ret = new byte[6 + this.data.Length];
            ret[pos++] = 0x0D;
            ret[pos++] = (byte)(totalCnt >> 8);
            ret[pos++] = (byte)(totalCnt);
            ret[pos++] = (byte)(crtCnt >> 8);
            ret[pos++] = (byte)(crtCnt);
            Array.Copy(data, 0, ret, pos, data.Length);
            pos += data.Length;
            return ret;

        }
    }

    public class M2BMaintenance : IComCmd
    {
        public byte idCnt { get; set; }
        public List<byte> idLst { get; set; }
        public M2BMaintenance(byte idCnt = 0)
        {
            this.idCnt = idCnt;
            if (idCnt != 0)
            {
                idLst = new List<byte>(idCnt);
            }
        }
        public void AddtoIdList(byte a)
        {
            idLst.Add(a);
        }
        public byte[] EnCode()
        {
            int pos = 0;
            byte[] ret = new byte[1 + 1 + idCnt];
            ret[pos++] = 0x10;
            ret[pos++] = this.idCnt;
            if (this.idCnt!=0)
            {
                foreach (var item in this.idLst)
                {
                    ret[pos++] = (byte)item;
                }
            }
            
            return ret;
        }
    }

    public class M2BDbSetting : IComCmd
    {
        public byte DBCnt { get; set; }
        public List<DBStruct> _DBGroup { get; set; }
        public M2BDbSetting(byte cnt=1)
        {
            DBCnt = cnt;
            _DBGroup = new List<DBStruct>();
        }
        public byte[] EnCode()
        {
            int pos = 0;
            byte[] ret = new byte[1 + 1 + 5 * DBCnt];
            ret[pos++] = 0x0C;
            ret[pos++] = DBCnt;
            if (this.DBCnt != 0)
            {
                foreach (var item in _DBGroup)
                {
                    ret[pos++] = item.DBID;
                    ret[pos++] = (byte)(item.DBValue >> 24);
                    ret[pos++] = (byte)(item.DBValue >> 16);
                    ret[pos++] = (byte)(item.DBValue >> 8);
                    ret[pos++] = (byte)item.DBValue;
                }
            }
            return ret;
        }
    }
    public class DBStruct
    {
        public byte DBID { get; set; }
        public UInt32 DBValue { get; set; }
    }

    public class M2BCalibration : IComCmd
    {
        public byte calPartNo { get; set; }
        public UInt16 calValue { get; set; }
        public M2BCalibration(byte calPartNo, UInt16 calValue)
        {
            this.calPartNo = calPartNo;
            this.calValue  = calValue;
        }
        public byte[] EnCode()
        {
            return new byte[] { 0x11, calPartNo, (byte)(calValue >> 8), (byte)calValue };
        }
    }

    public class M2BSleepMode:IComCmd
    {
        public byte OperationCmd { get; set; }
        public M2BSleepMode(bool IsExit = false)
        {
            if (IsExit)
            {
                OperationCmd = 2;
            }
            else
            {
                OperationCmd = 1;

            }
        }
        public byte[] EnCode()
        {
            return new byte[] { 0x12, OperationCmd };
        }
    }

    public class B2MSleepMode
    {
        public byte operationCmd { get; set; }
        public byte Result { get; set; }
        public B2MSleepMode(byte[] inbuf, int index)
        {
            int pos = index+1;
            operationCmd = inbuf[pos++];
            Result = inbuf[pos];
        }
    }

    public class B2MModeRequest
    {
        public byte Result { get; set; }
        public B2MModeRequest(byte[] inbuf,int index)
        {
            int pos = index;
            Result = inbuf[pos];
        }
    }

    public class B2MMatch
    {
        public byte Result { get; set; }
        public B2MMatch(byte[] inbuf, int index)
        {
            int pos = index;
            Result = inbuf[pos];
        }
    }

    public class B2MClean
    {
        public byte Result { get; set; }
        public B2MClean(byte[] inbuf, int index)
        {
            int pos = index;
            Result = inbuf[pos];
        }
    }

    public class B2MStopClean
    {
        public byte Result { get; set; }
        public B2MStopClean(byte[] inbuf, int index)
        {
            int pos = index;
            Result = inbuf[pos];
        }
    }

    public class B2MMakeDrink
    {
        public UInt16 ID { get; set; }
        public byte Result { get; set; }
        public B2MMakeDrink(byte[] inbuf, int index)
        {
            int pos = index+1;
            ID = (UInt16)((inbuf[pos++] << 8) + inbuf[pos++]);
            Result = inbuf[pos];
        }
    }

    public class B2MMakeIngredient
    {
        public byte operatetype { get; set; }
        public UInt16 ID { get; set; }
        public byte Result { get; set; }
        public B2MMakeIngredient(byte[] inbuf, int index)
        {
            int pos = index+1;
            operatetype = inbuf[pos++];
            ID = (UInt16)((inbuf[pos++] << 8) + inbuf[pos++]);
            Result = inbuf[pos];
        }
    }

    public class B2MTest
    {
        
        public byte operatetype { get; set; }
        public byte ID { get; set; }
        public byte Result { get; set; }
        public B2MTest(byte[] inbuf, int index)
        {
            int pos = index+1;
            operatetype = inbuf[pos++];
            ID = inbuf[pos++];
            Result = inbuf[pos];
        }
    }

    public class B2MMakeBeverage
    {
        public byte operatetype { get; set; }
        public UInt16 ID { get; set; }
        public byte Result { get; set; }
        public B2MMakeBeverage(byte[] inbuf, int index)
        {
            int pos = index+1;
            operatetype = inbuf[pos++];
            ID = (UInt16)((inbuf[pos++] << 8) + inbuf[pos++]);
            Result = inbuf[pos];
        }
    }

    public class B2MStateQuery
    {
        public MachineState machinestate { get; set; }
        public ModuleState modulestate { get; set; }
        public TemperatureInfo temperatureinfo { get; set; }
        public SensorState sensorstate { get; set; }
        public B2MStateQuery(byte[] inbuf, int index)
        {
            int pos = index+1;
            machinestate = (MachineState)inbuf[pos++];
            modulestate = new ModuleState(inbuf[pos++]);
            temperatureinfo = new TemperatureInfo(inbuf, pos);
            pos += TemperatureInfo.length;
            sensorstate = new SensorState(inbuf[pos++]);
        }
    }
    public class ModuleState
    {
        public bool IsCleanErr { get; set; }
        public bool IsSoldoutErr { get; set; }
        public bool IsHeatingErr { get; set; }
        public bool IsCoolingErr { get; set; }
        public bool IsWaterErr { get; set; }
        public ModuleState(byte a)
        {
            IsCleanErr = ((a & 0x01) == 0x01 ? true : false);
            IsSoldoutErr = ((a & 0x02) == 0x02 ? true : false);
            IsHeatingErr = ((a & 0x04) == 0x04 ? true : false);
            IsCoolingErr = ((a & 0x08) == 0x08 ? true : false);
            IsWaterErr = ((a & 0x10) == 0x10 ? true : false);

        }
    }
    public enum MachineState
    {
        Mode_Normal = 0x08,
        Mode_Sleep = 0x10,
        Mode_Backup = 0x18,
        Mode_Clean = 0x20,
        Mode_Clean_Finish = 0x21,
        Mode_Milk_Phase_Finish = 0x22,
        Mode_Dispense = 0x28,
        Mode_Dispense_Finish = 0x29
    }
    public class TemperatureInfo
    {
        public static int length = 6;
        public float Fridge { get; set; }
        public float Fan { get; set; }
        public float Water { get; set; }
        public TemperatureInfo(byte[] inbuf, int index)
        {
            int pos = index;
            Fridge = float.Parse(string.Format("{0}.{1}", (inbuf[pos++]), (inbuf[pos++])));
            Fan = float.Parse(string.Format("{0}.{1}", (inbuf[pos++]), (inbuf[pos++])));
            Water = float.Parse(string.Format("{0}.{1}", (inbuf[pos++]), (inbuf[pos++])));

        }
    }
    public class SensorState
    {
        public bool door { get; set; }
        public bool driptrayswitch { get; set; }
        public bool driptraylevel { get; set; }
        public bool cup1 { get; set; }
        public bool cup2 { get; set; }
        public SensorState(byte a)
        {
            door = ((a & 0x01) == 0x01 ? true : false);
            driptrayswitch = ((a & 0x02) == 0x02 ? true : false);
            driptraylevel = ((a & 0x04) == 0x04 ? true : false);
            cup1 = ((a & 0x08) == 0x08 ? true : false);
            cup2 = ((a & 0x10) == 0x10 ? true : false);

        }

    }

    public class B2MMaintenance
    {
        public byte idCnt { get; set; }
        public List<MaintenanceStruct> _table { get; set; }
        public byte Result { get; set; }
        public B2MMaintenance(byte[] inbuf, int index)
        {
            int pos = index + 1;
            this.idCnt = inbuf[pos++];
            _table = new List<MaintenanceStruct>(this.idCnt);
            for (int i = 0; i < this.idCnt; i++)
            {
                MaintenanceStruct tmp = new MaintenanceStruct(inbuf, pos);
                pos += 5;
                _table.Add(tmp);
            }
            this.Result = inbuf[pos++];
        }
    }
    public class MaintenanceStruct
    {
        public byte id { get; set; }
        public int value { get; set; }
        public MaintenanceStruct(byte[] inbuf, int index)
        {
            int pos = index;
            this.id = inbuf[pos++];
            Array.Reverse(inbuf, pos, 4);
            this.value = BitConverter.ToInt32(inbuf, pos);
            
        }
    }

    public class B2MDbSetting
    {
        public byte dbCnt { get; set; }
        public byte Result { get; set; }
        public B2MDbSetting(byte[] inbuf, int index)
        {
            int pos = index+1;
            dbCnt = inbuf[pos++];
            Result = inbuf[pos];
        }
    }
    public class B2MCalibration
    {
        public byte calPartNo { get; set; }
        public UInt16 calValue { get; set; }
        public byte Result { get; set; }
        public B2MCalibration(byte[] inbuf, int index)
        {
            int pos = index+1;
            calPartNo = inbuf[pos++];
            pos += 4;
            Result = inbuf[pos];
        }
    }
    public interface IComCmd
    {
       byte[] EnCode();
       //void DeCode(byte[] a);
    }
    public enum CmdType
    {
        M2B_CMD_MODE_REQUEST = 0x00,
        B2M_CMD_MODE_REQUEST = 0x80,
        M2B_CMD_MATCH = 0x01,
        B2M_CMD_MATCH = 0x81,
        M2B_CMD_CLEAN = 0x02,
        B2M_CMD_CLEAN = 0x82,
        M2B_CMD_MAKE_DRINK = 0x06,
        B2M_CMD_MAKE_DRINK = 0x86,
        M2B_CMD_MAKE_INGREDIENT = 0x07,
        B2M_CMD_MAKE_INGREDIENT = 0x87,
        M2B_CMD_MAKE_BERVAGE = 0x08,
        B2M_CMD_MAKE_BERVAGE = 0x88,
        M2B_CMD_TEST =  0x09,
        B2M_CMD_TEST =  0x89,
        M2B_CMD_QUERY = 0x0A,
        B2M_CMD_QUERY = 0x8A,
        M2B_CMD_DB_SET =0x0C,
        B2M_CMD_DB_SET = 0x8C,

        M2B_CMD_MAINTENCE =0x10,
        B2M_CMD_MAINTENCE = 0x90,
        M2B_CMD_CAL = 0x11,
        B2M_CMD_CAL = 0x91
    }
    public class EVOCommand
    {
         
        public CmdType _CmdType { get; set; }
        public EVOCommand(CmdType type)
        {
            _CmdType = type;
            switch (_CmdType)
            {
                case CmdType.M2B_CMD_CLEAN:
                    break;
                case CmdType.M2B_CMD_MAKE_BERVAGE:
                    break;
                case CmdType.M2B_CMD_MAKE_INGREDIENT:
                    break;
                case CmdType.M2B_CMD_TEST:
                    break;
                case CmdType.M2B_CMD_QUERY:
                    break;
                default:
                    break;
            }
        }
    }

    
}
