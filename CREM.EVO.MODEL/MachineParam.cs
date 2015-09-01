using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
   public class MachineParam
    {
       public static int Mixer1FlowRate = 1000;
       public static int Mixer2FlowRate = 1000;
       public static int HotwaterFlowRate = 1000;
       public static int BrewFlowRate = 1000;

       public enum IDTYPE
       {
           MIXER1watervalve=5,
           MIXER2watervalve=6,
           brehotwatervalve=14
       }
    }
}
