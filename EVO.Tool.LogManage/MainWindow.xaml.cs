using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EVO.Tool.LogManage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _LogLineTxt = new List<LogLineTxt>();
        }
        private List<LogLineTxt> _LogLineTxt;
        private void menu_check(object sender, RoutedEventArgs e)
        {
            string tmp = (sender as MenuItem).Tag.ToString();
            if (tmp.Equals("open"))
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Title = "Select File";
                openFileDialog.Filter = "Log文件|*.Log|所有文件|*.*";
                openFileDialog.FileName = string.Empty;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
                {
                    _LogLineTxt.Clear();

                    Fun_Show_txt.Items.Clear();
                    using (StreamReader streamReader = File.OpenText(openFileDialog.FileName))
                    {
                        
                        //Paragraph paragraph = new Paragraph();
                        //paragraph.Inlines.Add(streamReader.ReadToEnd());
                        LogLineTxt tmptxt;
                        while (!streamReader.EndOfStream)
                        {
                            string linetxt = streamReader.ReadLine();
                            tmptxt = new LogLineTxt(linetxt);
                            _LogLineTxt.Add(tmptxt);
                            Fun_Show_txt.Items.Add(linetxt);
                            
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbtype.SelectedIndex == 0) //error
            {
                List<LogLineTxt> tmplist = _LogLineTxt.FindAll(c => c.strType == "[Error]");
                Fun_Show_txt.Items.Clear();
                foreach (var item in tmplist)
                {
                    Fun_Show_txt.Items.Add(item.ToString());
                }
            }
            else if (cbtype.SelectedIndex == 1)
            {
                List<LogLineTxt> tmplist = _LogLineTxt.FindAll(c => c.strType == "[Warning]");
                Fun_Show_txt.Items.Clear();
                foreach (var item in tmplist)
                {
                    Fun_Show_txt.Items.Add(item.ToString());
                }
            }
            else if (cbtype.SelectedIndex == 2)
            {
                List<LogLineTxt> tmplist = _LogLineTxt.FindAll(c => c.strType == "[Operate]");
                Fun_Show_txt.Items.Clear();
                foreach (var item in tmplist)
                {
                    Fun_Show_txt.Items.Add(item.ToString());
                }

            }
            else if (cbtype.SelectedIndex == 3)
            {
                Fun_Show_txt.Items.Clear();
                foreach (var item in _LogLineTxt)
                {
                    Fun_Show_txt.Items.Add(item.ToString());
                }
            }
        }
    }
    public class LogLineTxt
    {
        public string strTime { get; set; }
        public string strType { get; set; }
        public string strInfo { get; set; }
        public LogLineTxt(string linestr)
        {
            string[] stringSeparators = new string[] {"] ["};
            string[] tmp = linestr.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in tmp)
            {
                Console.WriteLine(item);
            }
            strTime = tmp[0] + "]";
            strType = "[" + tmp[1] + "]";
            strInfo = "[" + tmp[2];
        }
        public string ToString()
        {
            return strTime + " " + strType + " " + strInfo;
        }
    }
}
