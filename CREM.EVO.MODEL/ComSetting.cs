using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CREM.EVO.MODEL
{
  [XmlRootAttribute("ComPort-Setting", IsNullable = false)]
  public class ComSetting:NotificationObject
    {
      public ComSetting() { }
      public ComSetting(string port ,int baudRate)
      {
          _Port = port;
          _BaudRate = baudRate;
      }
      [XmlElementAttribute("Port")]
      private string _Port;
      public string Port
      {
          get { return _Port; }
          set { if (value != _Port) { _Port = value; this.RaisePropertyChanged("Port"); } }
      }
      [XmlElementAttribute("BaudRate")]
      private int _BaudRate;
      public int BaudRate
      {
          get { return _BaudRate; }
          set { if (value != _BaudRate) { _BaudRate = value; this.RaisePropertyChanged("BaudRate"); } }
      }
    }
}
