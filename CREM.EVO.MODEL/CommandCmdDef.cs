using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
   public class CommandCmdDef
    {
       public enum ComCmd
       {
           UI_COM_SET =0x201,

           TEST_START = 0x01,
           TEST_STOP  = 0x02,

           MAKE_INGERDIENT_DELETE = 0x71,
           MAKE_INGERDIENT_MODIFY = 0x72,
           MAKE_INGERDIENT_ADD    = 0x73,
           MAKE_INGERDIENT_REVIEW = 0x74,
           MAKE_INGERDIENT_BACKUP = 0x75,

           MAKE_BAVERAGE_DELETE =0x81,
           MAKE_BAVERAGE_MODIFY = 0x82,
           MAKE_BAVERAGE_ADD = 0x83,
           MAKE_BAVERAGE_REVIEW = 0x84,
           MAKE_BAVERAGE_BACKUP = 0x85,

           DB_SET = 0x90,


           UPDATE_RCP = 0x100,
           UPDATE_FIRM = 0x101,

           MAINTENCE_GET = 0x110
       }
       public enum BtnCmd
       {
           MAIN_SAVE  = 0x01,
           MAIN_BACK  =0x02,

           VALVE_ADD  = 0x10,
           VALVE_DEL  = 0x11,
           VALVE_SAVE = 0x12,

           CANISTER_ADD = 0x20,
           CANISTER_DEL = 0x21,
           CANISTER_SAVE = 0x22,

           MIXSER_ADD = 0x30,
           MIXSER_DEL = 0x31,
           MIXSER_SAVE = 0x32,

           BEANHOPPER_ADD = 0x40,
           BEANHOPPER_DEL = 0x41,
           BEANHOPPER_SAVE = 0x42,

           INGRED_ADD = 0x50,
           INGRED_DEL = 0x51,
           INGRED_SAVE = 0x52,

           RCP_ADD = 0x60,
           RCP_DEL = 0x61,
           RCP_SAVE = 0x62,

           RCP_INGRED_ADD  = 0x70,
           RCP_INGRED_DEL  = 0x71,
           RCP_INGRED_SAVE = 0x72,


           DB_GET =0x81,
           DB_SET =0x82,

           
           /*
            * 
            * 
            */
           UI_MainSelect  = 0x100,
           UI_MachineSet  = 0x101,
           UI_MachineTest = 0x102,
           UI_RECIPESYS = 0x103,
           UI_CLEANSYS =0x104,
           UI_DB = 0x105,
           UI_STCHECK =0x106,
           UI_UPDATE =0x107,

           /*
           * 
           * 
           */
           ASSIT_FUN =0x200
       }
       public enum UiIndex
       {
           UI_MainSelect = 0x100,
           UI_MachineSet = 0x101,
           UI_MachineTest = 0x102,
           UI_RECIPESYS = 0x103,
           UI_CLEANSYS = 0x104,
           UI_DB = 0x105,
           UI_STCHECK = 0x106,
           UI_UPDATE = 0x107,
           UI_COM_SET =0x201
       }
       public enum ModifyType
       {
           IDEL    = 0x10,
           NEWONE  = 0x01,
           MODIFY  = 0x02,
           UPDATED = 0x03
       }
    }
}
