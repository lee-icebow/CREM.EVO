using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CREM.EVO.MODEL
{
    [XmlRootAttribute("BaseSetting")]
    public class EVOBaseConf
    {
       [XmlArrayItem()]
       public List<DBItem> EvoDbTable { get; set; }
       public EVOBaseConf()
       {
           EvoDbTable = new List<DBItem>();
       }
       public DBItem Search(int ID)
       {
           return EvoDbTable.Find(c => c.DBID == ID);
       }
       public void Set(DBItem a)
       {
           DBItem tmp = EvoDbTable.Find(c => c.DBID == a.DBID);
           if (tmp == null)
           {
               DBItem tt = new DBItem();
               tt.DBID = a.DBID;
               tt.DBvalue = a.DBvalue;
               tt.Description = a.Description;
               EvoDbTable.Add(tt);
           }
           else
           {
               tmp.DBID = a.DBID;
               tmp.DBvalue = a.DBvalue;
               tmp.Description = a.Description;
           }
       }
    }

    [XmlRootAttribute("DBItem")]
    public class DBItem:NotificationObject
    {
        private int _DBID;
        private string _Description;
        private int _DBvalue;

        [XmlAttribute("DBID")]
        public int DBID
        {
            get { return _DBID; }
            set
            {
                if (value != _DBID)
                {
                    _DBID = value;
                    this.RaisePropertyChanged("DBID");
                }
            }
        }
        [XmlAttribute("Description")]
        public string Description
        {
            get { return _Description; }
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        [XmlAttribute("DBvalue")]
        public int DBvalue
        {
            get { return _DBvalue; }
            set
            {
                if (value != _DBvalue)
                {
                    _DBvalue = value;
                    this.RaisePropertyChanged("DBvalue");
                }
            }
        }
    }



}
