using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CREM.EVO.MODEL
{
   public class EvoUpdate:NotificationObject
    {
        public byte FileType { get; set; }
        private string _rcpFileName;

        public string RcpFileName
        {
            get { return _rcpFileName; }
            set { if (_rcpFileName != value) { _rcpFileName = value; this.RaisePropertyChanged("RcpFileName"); } }
        }

        private string _firmFileName;

        public string FirmFileName
        {
            get { return _firmFileName; }
            set { if (_firmFileName != value) { _firmFileName = value; this.RaisePropertyChanged("FirmFileName"); } }
        }

        private int _totalAmount;
        public int TotalAmount
        {
            get { return _totalAmount; }
            set { if (_totalAmount != value) { _totalAmount = value; this.RaisePropertyChanged("TotalAmount"); } }
        }

        private int _crtAmount;
        public int CrtAmount
        {
            get { return _crtAmount; }
            set { if (_crtAmount != value) { _crtAmount = value; this.RaisePropertyChanged("CrtAmount"); } }
        }
        public EvoUpdate()
        {
            CrtAmount = 0;
            TotalAmount = 0;
            UpdateInfo = new ObservableCollection<string>();
            filetable = new Hashtable();
        }
        private string _info;
        public string Info
        {
            get { return _info; }
            set { if (_info != value) { _info = value; this.RaisePropertyChanged("Info"); UpdateInfo.Add(Info); } }
        }
        public ObservableCollection<string> UpdateInfo { get; set; }

        public Hashtable filetable { get; set; }
        public void Clearfiletable()
        {
            filetable.Clear();
        }
        public void AddtoHashMap(int key, byte[] buf)
        {
            filetable.Add(key, buf);
        }
        public byte[] GetOnePackge(int key)
        {
            foreach (DictionaryEntry de in filetable)
            {
                if ((int)de.Key == key)
                {
                    byte[] ret = (byte[])de.Value;
                    return ret;
                }
            }
            return null;
        }
    }
}
