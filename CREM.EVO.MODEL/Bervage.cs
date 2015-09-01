using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CREM.EVO.Utility;
namespace CREM.EVO.MODEL
{
    public class Bervage
    {
        public UInt16 _ID;
        private string _name;//最大20个char

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private byte[] _chrName;
        private List<BasciAction> _ActionList;
        public List<BasciAction> ActionList
        {
            get { return _ActionList; }
            set { _ActionList = value; }
        }
        public Bervage()
        {
            _ActionList = new List<BasciAction>();
            _chrName = new byte[20];
        }
        public void AddAction(BasciAction ba)
        {
            _ActionList.Add(ba);
        }
        public byte[] ToByteArray()
        {
            byte[] tmpbuf = new byte[512];
            byte[] tmp;
            int pos = 0;
            foreach (var item in _ActionList)
            {
                switch (item._type)
                {
                    case BasicActionType.Espresso:
                        tmpbuf[pos++] = (byte)item._type;
                        tmpbuf[pos++] = (byte)(item._startTm >> 8);
                        tmpbuf[pos++] = (byte)item._startTm;
                        tmp = Function.classToByteArray<Ingredient_Espresso_extern>((Ingredient_Espresso_extern)item._obj);
                        Array.Copy(tmp, 0, tmpbuf, pos, tmp.Length);
                        pos += tmp.Length;
                        break;
                    case BasicActionType.Filter:
                        tmpbuf[pos++] = (byte)item._type;
                        tmpbuf[pos++] = (byte)(item._startTm >> 8);
                        tmpbuf[pos++] = (byte)item._startTm;
                        tmp = Function.classToByteArray<Ingredient_Filter_Brew_extern>((Ingredient_Filter_Brew_extern)item._obj);
                        Array.Copy(tmp, 0, tmpbuf, pos, tmp.Length);
                        pos += tmp.Length;
                        break;
                    case BasicActionType.Instantpower:
                        tmpbuf[pos++] = (byte)item._type;
                        tmpbuf[pos++] = (byte)(item._startTm >> 8);
                        tmpbuf[pos++] = (byte)item._startTm;
                        tmp = Function.classToByteArray<Ingredient_InstantPowder_extern>((Ingredient_InstantPowder_extern)item._obj);
                        Array.Copy(tmp, 0, tmpbuf, pos, tmp.Length);
                        pos += tmp.Length;
                        break;
                    case BasicActionType.FreshMilk:
                        tmpbuf[pos++] = (byte)item._type;
                        tmpbuf[pos++] = (byte)(item._startTm >> 8);
                        tmpbuf[pos++] = (byte)item._startTm;
                        tmp = Function.classToByteArray<Ingredient_Fresh_Milk_extern>((Ingredient_Fresh_Milk_extern)item._obj);
                        Array.Copy(tmp, 0, tmpbuf, pos, tmp.Length);
                        pos += tmp.Length;
                        break;
                    default:
                        break;
                }


            }
            byte[] retbuf = new byte[pos + 20 + 2];
            retbuf[0] =(byte)( _ID >> 8);
            retbuf[1] = (byte)(_ID);
            Array.Copy(
            Array.Copy(tmpbuf, 0, retbuf, 0,pos);
            return retbuf;
        }
         
    }

   public class BasciAction
   {
       public object _obj { get; set; }
       public BasicActionType _type { get; set; }
       public UInt16 _startTm { get; set; }
       public BasciAction(BasicActionType type)
       {
           _type = type;
           switch (_type)
           {
               case BasicActionType.Espresso:
                   _obj = new Ingredient_Espresso_extern();
                   break;
               case BasicActionType.Filter:
                   _obj = new  Ingredient_Filter_Brew_extern();
                   break;
               case BasicActionType.Instantpower:
                   _obj = new Ingredient_InstantPowder_extern();
                   break;
               case BasicActionType.FreshMilk:
                   _obj = new Ingredient_Fresh_Milk_extern();
                   break;
               default:
                   break;
           }
       }            
   }
   public enum BasicActionType
   {
       Espresso    =0x01,
       Filter      =0x02,
       Instantpower=0x03,
       FreshMilk   =0x04
   }
     
}
