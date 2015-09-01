using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
 
namespace CREM.EVO.MODEL
{
   [XmlRootAttribute("EvoRecipe")]
   public class EvoRecipe:NotificationObject
   {
       [XmlArrayItem()]
       public ObservableCollection<IngredientInfo> _lstIngredientInfo { get; set; }
       [XmlArrayItem()]
       public ObservableCollection<RecipeInfo> _lstRecipeInfo { get; set; }
       public EvoRecipe()
       {
           _lstIngredientInfo = new ObservableCollection<IngredientInfo>();
           _lstRecipeInfo = new ObservableCollection<RecipeInfo>();
       }
   }

   [XmlRootAttribute("RecipeInfo")]
   public class RecipeInfo:NotificationObject
   {
       public RecipeInfo()
       {
           _lstIngredientStep = new ObservableCollection<IngredientStep>();
           _publicInfo = new publicInfo();
           _crtModifyStatus = CommandCmdDef.ModifyType.IDEL;
       }
       [XmlEnum()]
       private CREM.EVO.MODEL.CommandCmdDef.ModifyType _crtModifyStatus;

       public CREM.EVO.MODEL.CommandCmdDef.ModifyType CrtModifyStatus
       {
           get { return _crtModifyStatus; }
           set { _crtModifyStatus = value; }
       }
       private bool _IsSelected;
       [XmlIgnore]
       public bool IsSelected
       {
           get { return _IsSelected; }
           set
           {
               if (value != _IsSelected)
               {
                   _IsSelected = value;
                   this.RaisePropertyChanged("IsSelected");
               }
           }
       }
       private UInt16 _ID;
       [XmlElementAttribute("ID")]
       public UInt16 ID
       {
           get { return _ID; }
           set { if (_ID != value) { _ID = value; this.RaisePropertyChanged("ID"); } }
       }
       private string _Name;
       [XmlElementAttribute("Name")]
       public string Name
       {
           get { return _Name; }
           set { if (_Name != value) { _Name = value; this.RaisePropertyChanged("Name"); } }
       }

       [XmlArrayItem()]
       public ObservableCollection<IngredientStep> _lstIngredientStep { get; set; }
       [XmlElementAttribute("PublicInfo")]
       public publicInfo _publicInfo { get; set; }

       public void refresh()
       {
           this.RaisePropertyChanged("ID");
       }

   }


   public class publicInfo:NotificationObject
   {
       private byte _CupSensor  ;
       private byte _UseCount   ;
       private byte _Ledmode    ;
       private byte _LedColor;
       private byte _LedIntensity;
       private byte _DispenseType ;
       private byte _setStrength;
       private byte _setVolume;
       private byte _setMilk ;
       private byte _setSugar;
       private byte _setCups ;
       private UInt16 _price ;


       [XmlAttribute]
       public byte CupSensor
       {
           get { return _CupSensor; }
           set { if (_CupSensor != value) { _CupSensor = value; this.RaisePropertyChanged("CupSensor"); } }
       }


       [XmlAttribute]
       public byte UseCount
       {
           get { return _UseCount; }
           set { if (_UseCount != value) { _UseCount = value; this.RaisePropertyChanged("UseCount"); } }
       }
       [XmlAttribute]
       public byte Ledmode
       {
           get { return _Ledmode; }
           set { if (_Ledmode != value) { _Ledmode = value; this.RaisePropertyChanged("Ledmode"); } }
       }
       [XmlAttribute]
       public byte LedColor
       {
           get { return _LedColor; }
           set { if (_LedColor != value) { _LedColor = value; this.RaisePropertyChanged("LedColor"); } }
       }
       [XmlAttribute]
       public byte LedIntensity
       {
           get { return _LedIntensity; }
           set { if (_LedIntensity != value) { _LedIntensity = value; this.RaisePropertyChanged("LedIntensity"); } }
       }
       [XmlAttribute]
       public byte DispenseType
       {
           get { return _DispenseType; }
           set { if (_DispenseType != value) { _DispenseType = value; this.RaisePropertyChanged("DispenseType"); } }
       }
       [XmlAttribute]
       public byte setStrength
       {
           get { return _setStrength; }
           set { if (_setStrength != value) { _setStrength = value; this.RaisePropertyChanged("setStrength"); } }
       }
       [XmlAttribute]
       public byte setVolume
       {
           get { return _setVolume; }
           set { if (_setVolume != value) { _setVolume = value; this.RaisePropertyChanged("setVolume"); } }
       }
       [XmlAttribute]
       public byte setMilk
       {
           get { return _setMilk; }
           set { if (_setMilk != value) { _setMilk = value; this.RaisePropertyChanged("setMilk"); } }
       }
       [XmlAttribute]
       public byte setSugar
       {
           get { return _setSugar; }
           set { if (_setSugar != value) { _setSugar = value; this.RaisePropertyChanged("setSugar"); } }
       }
       [XmlAttribute]
       public byte setCups
       {
           get { return _setCups; }
           set { if (_setCups != value) { _setCups = value; this.RaisePropertyChanged("setCups"); } }
       }
       [XmlAttribute]
       public UInt16 price
       {
           get { return _price; }
           set { if (_price != value) { _price = value; this.RaisePropertyChanged("price"); } }
       }
       public publicInfo()
       {
           _CupSensor=1;
           _UseCount = 1;
           _Ledmode = 1;
           _LedColor = 1;
           _LedIntensity = 1;
           _DispenseType = 2;
           _setStrength = 1;
           _setVolume = 1;
           _setMilk = 1;
           _setSugar = 1;
           _setCups = 1;
           _price = 0;
       }
   }
   public class IngredientStep:NotificationObject
   {
       private bool _IsSelected;
       [XmlIgnore]
       public bool IsSelected
       {
           get { return _IsSelected; }
           set
           {
               if (value != _IsSelected)
               {
                   _IsSelected = value;
                   this.RaisePropertyChanged("IsSelected");
               }
           }
       }      
       private UInt16 _startTime;
       [XmlAttribute("StartTime")]
       public UInt16 StartTime
       {
           get { return _startTime; }
           set { _startTime = value; }
       }
       private UInt16 _ID;
       [XmlAttribute("ID")]
       public UInt16 ID
       {
           get { return _ID; }
           set { _ID = value; }
       }
       private string _Name;
       [XmlAttribute("Name")]
       public string Name
       {
           get { return _Name; }
           set { if (_Name != value) { _Name = value; this.RaisePropertyChanged("Name"); } }
       }
       private UInt16 _UsedTime;
       [XmlAttribute("Volume")]
       public UInt16 UsedTime
       {
           get { return _UsedTime; }
           set { if (_UsedTime != value) { _UsedTime = value; this.RaisePropertyChanged("UsedTime"); } }
       }
       [XmlAttribute("Type")]
       public byte _Type { get; set; }
       private UInt16 _ScaleRate;
       [XmlAttribute("ScaleRate")]
       public UInt16 ScaleRate
       {
           get { return _ScaleRate; }
           set { if (_ScaleRate != value) { _ScaleRate = value; this.RaisePropertyChanged("ScaleRate"); this.RaisePropertyChanged("UsedTime"); } }
       }
   }
   [XmlRootAttribute("IngredientInfo")]
   public class IngredientInfo:NotificationObject
   {
       [XmlEnum()]
       private CREM.EVO.MODEL.CommandCmdDef.ModifyType _crtModifyStatus;

       public CREM.EVO.MODEL.CommandCmdDef.ModifyType CrtModifyStatus
       {
           get { return _crtModifyStatus; }
           set { _crtModifyStatus = value; }
       }
       private bool _IsSelected;
       [XmlIgnore]
       public bool IsSelected
       {
           get { return _IsSelected; }
           set
           {
               if (value != _IsSelected)
               {
                   _IsSelected = value;
                   this.RaisePropertyChanged("IsSelected");
               }
           }
       }      
       [XmlEnum()]
       private IngredientType _Type;
       public IngredientType Type
       {
           get { return _Type; }
           set { if (_Type != value) { _Type = value; this.RaisePropertyChanged("Type"); } }
       }
       private UInt16 _ID;
       [XmlElementAttribute("ID")]
       public UInt16 ID
       {
           get { return _ID; }
           set { if (_ID != value) { _ID = value; this.RaisePropertyChanged("ID"); } }
       }
       private string _Name;
       [XmlElementAttribute("Name")]
       public string Name
       {
           get { return _Name; }
           set { if (_Name != value) { _Name = value; this.RaisePropertyChanged("Name"); } }
       }
       [XmlElementAttribute("Espresso")]
       public Ingredient_Espresso_extern _Espresso { get; set; }
       [XmlElementAttribute("FilterBrew")]
       public Ingredient_Filter_Brew_extern _FilterBrew { get; set; }
       [XmlElementAttribute("InstantPowder")]
       public Ingredient_InstantPowder_extern _InstantPowder { get; set; }
       [XmlElementAttribute("FreshMilk")]
       public Ingredient_Fresh_Milk_extern _FreshMilk { get; set; }
       [XmlElementAttribute("Water")]
       public Ingredient_Water _Water { get; set; }
       public IngredientInfo(bool crtflag)
       {
           _Espresso = new Ingredient_Espresso_extern();
           _FilterBrew = new Ingredient_Filter_Brew_extern();
           _InstantPowder = new Ingredient_InstantPowder_extern();
           _FreshMilk = new Ingredient_Fresh_Milk_extern();
           _Water = new Ingredient_Water();

       }
       public IngredientInfo() 
       {
           _crtModifyStatus = CommandCmdDef.ModifyType.IDEL;
       }
       public IngredientInfo(IngredientType a)
       {
           CrtModifyStatus = CommandCmdDef.ModifyType.NEWONE;
           Type = a;
           switch (a)
           {
               case IngredientType.ESPRESSO:
                   _Espresso = new Ingredient_Espresso_extern();
                   Name = "ESPRESSO";
                   break;
               case IngredientType.FILTERBREW:
                   _FilterBrew = new Ingredient_Filter_Brew_extern();
                   Name = "FILTERBREW";
                   break;
               case IngredientType.INSTANTPOWDER:
                   _InstantPowder = new Ingredient_InstantPowder_extern();
                   Name = "INSTANTPOWDER";
                   break;
               case IngredientType.FRESHMILK:
                   _FreshMilk = new Ingredient_Fresh_Milk_extern();
                   Name = "FreshMilk";
                   break;
               case IngredientType.Water:
                   _Water = new Ingredient_Water();
                   Name = "Water";
                   break;
               default:
                   break;
           }
       }
   }

   [XmlRootAttribute("IDControl")] 
   public class IDGenrator
   {
      
       private ObservableCollection<IDProperty> _LstIngIDProperty;
       [XmlArrayItem()]
       public ObservableCollection<IDProperty> LstIngIDProperty
       {
           get { return _LstIngIDProperty; }
           set { _LstIngIDProperty = value; }
       }

       private ObservableCollection<IDProperty> _LstRcpIDProperty;
       [XmlArrayItem()]
       public ObservableCollection<IDProperty> LstRcpIDProperty
       {
           get { return _LstRcpIDProperty; }
           set { _LstRcpIDProperty = value; }
       }
       
       public IDGenrator()
       {
           _LstIngIDProperty = new ObservableCollection<IDProperty>();
           _LstRcpIDProperty = new ObservableCollection<IDProperty>();
       }
       public void CGID(UInt16 id,byte type=0)
       {
           if(type ==0)
               _LstIngIDProperty.First(c => c.ID == id).IsUsed = false;
           else
               LstRcpIDProperty.First(c => c.ID == id).IsUsed = false;
       }
       public UInt16 GetID(byte type = 0)
       {
           UInt16 ret = 0;
            
           try
           {
               if (type == 0)
               {

                   IDProperty item = _LstIngIDProperty.First(c => c.IsUsed == false);
                   ret = item.ID;
                   item.IsUsed = true;
               }
               else
               {
                   IDProperty item = LstRcpIDProperty.First(c => c.IsUsed == false);
                   ret = item.ID;
                   item.IsUsed = true;
               }
               
           }
           catch (Exception)
           {
               
              
           }
           return ret;
            
       }
   }
    [XmlRootAttribute("IDProperty")]
    public class IDProperty
    {
        [XmlElementAttribute("ID")]
        public UInt16 ID   {get;set;}
        [XmlElementAttribute("IsUsed")]
        public bool IsUsed {get;set;}
    }
}
