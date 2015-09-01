using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
   public class UiData : NotificationObject
    {       
        private bool _is_mainselect_visible;

        public bool is_mainselect_visible
        {
            get { return _is_mainselect_visible; }
            set { if (_is_mainselect_visible == value)return; _is_mainselect_visible = value; this.RaisePropertyChanged("is_mainselect_visible"); }
        }

        private bool _is_machineset_visible;

        public bool is_machineset_visible
        {
            get { return _is_machineset_visible; }
            set { if (_is_machineset_visible == value)return; _is_machineset_visible = value; this.RaisePropertyChanged("is_machineset_visible"); }
        }
        private bool _is_machinetest_visible;
        public bool is_machinetest_visible
        {
            get { return _is_machinetest_visible; }
            set { if (_is_machinetest_visible == value)return; _is_machinetest_visible = value; this.RaisePropertyChanged("is_machinetest_visible"); }
        }

        private bool _is_ComSet_visible;
        public bool is_ComSet_visible
        {
            get { return _is_ComSet_visible; }
            set { if (_is_ComSet_visible == value)return; _is_ComSet_visible = value; this.RaisePropertyChanged("is_ComSet_visible"); }
        }

        private bool _is_Recipe_visible;
        public bool is_Recipe_visible
        {
            get { return _is_Recipe_visible; }
            set { if (_is_Recipe_visible == value)return; _is_Recipe_visible = value; this.RaisePropertyChanged("is_Recipe_visible"); }
        }
        private bool _is_Clean_visible;
        public bool is_Clean_visible
        {
            get { return _is_Clean_visible; }
            set { if (_is_Clean_visible == value)return; _is_Clean_visible = value; this.RaisePropertyChanged("is_Clean_visible"); }
        }

        private bool _is_DB_visible;
        public bool is_DB_visible
        {
            get { return _is_DB_visible; }
            set { if (_is_DB_visible == value)return; _is_DB_visible = value; this.RaisePropertyChanged("is_DB_visible"); }
        }

        private bool _is_Update_visible;
        public bool is_Update_visible
        {
            get { return _is_Update_visible; }
            set { if (_is_Update_visible == value)return; _is_Update_visible = value; this.RaisePropertyChanged("is_Update_visible"); }
        }


        private void InitAll()
        {
            is_mainselect_visible = false;
            is_machineset_visible = false;
            is_machinetest_visible = false;
            is_ComSet_visible = false;
            is_Recipe_visible = false;
            is_Clean_visible = false;
            is_DB_visible = false;
            is_Update_visible = false;
        }
        public void SetUiVisible(CommandCmdDef.UiIndex a)
        {
            InitAll();
            switch (a)
            {
                case CommandCmdDef.UiIndex.UI_UPDATE:
                    is_Update_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_MainSelect:
                    is_mainselect_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_MachineSet:
                    is_machineset_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_MachineTest:
                    is_machinetest_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_COM_SET:
                    break;
                case CommandCmdDef.UiIndex.UI_RECIPESYS:
                    is_Recipe_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_CLEANSYS:
                    is_Clean_visible = true;
                    break;
                case CommandCmdDef.UiIndex.UI_DB:
                    is_DB_visible = true;
                    break;
                default:
                    break;
            }
        }
    }
   public class MachineInfo : NotificationObject
   {
       private MachineState _machinestate;
       public MachineState machinestate
       {
           get { return _machinestate; }
           set
           {
               if (value!= _machinestate)
               {
                   _machinestate = value; this.RaisePropertyChanged("machinestate");
               }
           }
       }

       private bool _err_clean;

       public bool Err_clean
       {
           get { return _err_clean; }
           set
           {
               if (_err_clean != value)
               {
                   _err_clean = value; this.RaisePropertyChanged("Err_clean");
               }
           }
       }

       private bool _err_soldout;

       public bool Err_soldout
       {
           get { return _err_soldout; }
           set
           {
               if (_err_soldout != value)
               {
                   _err_soldout = value; this.RaisePropertyChanged("Err_soldout");
               }
           }
       }

       private bool _err_heating;

       public bool Err_heating
       {
           get { return _err_heating; }
           set
           {
               if (_err_heating != value)
               {
                   _err_heating = value; this.RaisePropertyChanged("Err_heating");
               }
           }
       }

       private bool _err_cooling;

       public bool Err_cooling
       {
           get { return _err_cooling; }
           set
           {
               if (_err_cooling != value)
               {
                   _err_cooling = value; this.RaisePropertyChanged("Err_cooling");
               }
           }
       }

       private bool _err_water;

       public bool Err_water
       {
           get { return _err_water; }
           set
           {
               if (_err_water != value)
               {
                   _err_water = value; this.RaisePropertyChanged("Err_water");
               }
           }
       }

       private float _temp_fridge;
       public float Temp_fridge
       {
           get { return _temp_fridge; }
           set
           {
               if (_temp_fridge != value)
               {
                   _temp_fridge = value; this.RaisePropertyChanged("Temp_fridge");
               }
           }
       }

       private float _temp_water;
       public float Temp_water
       {
           get { return _temp_water; }
           set
           {
               if (_temp_water != value)
               {
                   _temp_water = value; this.RaisePropertyChanged("Temp_water");
               }
           }
       }

       private float _temp_fan;
       public float Temp_fan
       {
           get { return _temp_fan; }
           set
           {
               if (_temp_fan != value)
               {
                   _temp_fan = value; this.RaisePropertyChanged("Temp_fan");
               }
           }
       }

       private bool _exist_door;
       public bool Exist_door
       {
           get { return _exist_door; }
           set
           {
               if (_exist_door != value)
               {
                   _exist_door = value; this.RaisePropertyChanged("Exist_door");
               }
           }
       }

       private bool _exist_dripswitch;
       public bool Exist_dripswitch
       {
           get { return _exist_dripswitch; }
           set
           {
               if (_exist_dripswitch != value)
               {
                   _exist_dripswitch = value; this.RaisePropertyChanged("Exist_dripswitch");
               }
           }
       }

       private bool _exist_driplevel;
       public bool Exist_driplevel
       {
           get { return _exist_driplevel; }
           set
           {
               if (_exist_driplevel != value)
               {
                   _exist_driplevel = value; this.RaisePropertyChanged("Exist_driplevel");
               }
           }
       }

       private bool _exist_cup1;
       public bool Exist_cup1
       {
           get { return _exist_cup1; }
           set
           {
               if (_exist_cup1 != value)
               {
                   _exist_cup1 = value; this.RaisePropertyChanged("Exist_cup1");
               }
           }
       }

       private bool _exist_cup2;
       public bool Exist_cup2
       {
           get { return _exist_cup2; }
           set
           {
               if (_exist_cup2 != value)
               {
                   _exist_cup2 = value; this.RaisePropertyChanged("Exist_cup2");
               }
           }
       }
   }
   public class MaintenanceInfo : NotificationObject
   {
       private byte _maintenanceId;

       public byte MaintenanceId
       {
           get { return _maintenanceId; }
           set { if (_maintenanceId != value) { _maintenanceId = value; this.RaisePropertyChanged("MaintenanceId"); this.RaisePropertyChanged("MaintenanceDiscription"); } }
       }
       private int _maintenanceValue;

       public int MaintenanceValue
       {
           get { return _maintenanceValue; }
           set { if (_maintenanceValue != value) { _maintenanceValue = value; this.RaisePropertyChanged("MaintenanceValue"); } }
       }
       public string MaintenanceDiscription
       {
           set { }
           get { return ""; }
       }
       private string GetMaintenanceDiscription()
       {
           string ret = "UnDefined";
           switch (_maintenanceId)
           {
               case 1:
                   break;
               default:
                   break;
           }
           return ret;
       }

   }
}
