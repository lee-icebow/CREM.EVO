using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
using CREM.EVO.BLL;
using CREM.EVO.MODEL;
using CREM.EVO.Utility;
namespace CREM.EVO
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private CREM.EVO.BLL.Control _my = new BLL.Control();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _my;
            //_my.SaveEVOMachineSet();
            //EvoCalibrationWin wina = new EvoCalibrationWin(_my._MachineInfo);
            //wina.Show();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView tmp = sender as ListView;
            if (tmp.SelectedItem != null)
            {
                switch (tmp.Tag.ToString())
                {
                    case "Main":
                        _my.UpdateEvoMachine();
                        break;
                    case "Valve":
                        _my.UpdateWaterValve();
                        break;
                    case "Mixser":
                        _my.UpdateMixerUnit();
                        break;
                    case "Canister":
                        _my.UpdateCanisterValve();
                        break;
                    case "BeanHopper":
                        _my.UpdateBeanHopperValve();
                        break;
                    case "DEVTEST":
                        _my.UpdateDevice();
                        break;
                    case "Ingredient":
                        _my.UpdateIngredient();
                        grdingredient.IsEnabled = true;
                        grdcmd.IsEnabled = true;
                        FillPackageType();
                        ReloadCanisterIngredient();
                        FillBeanType();
                        break;
                    case "Recipe":
                        _my.UpdateRecipe();
                        tbrcp.IsEnabled = true;
                        spcmd.IsEnabled = true;
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(sender.GetType());
        }

        private void FillBeanType()
        {
            gg1.Items.Clear();
            gg2.Items.Clear();
            ComboBoxItem tmp;
            if (_my._CrtEVOMachine._BeanHopperUint.Count == 1)
            {
                _my._CrtIngredient._FilterBrew.Grind2Cnt = 0;
                _my._CrtIngredient._FilterBrew.Grind2Type = 0;
                g2.IsEnabled = false;
                gg2.IsEnabled = false;
                gg1.IsEnabled = false;
                tmp = new ComboBoxItem();
                tmp.Tag = _my._CrtEVOMachine._BeanHopperUint[0].Powdertype;
                tmp.Content = _my._CrtEVOMachine._BeanHopperUint[0].GetPowdertype();
                gg1.Items.Add(tmp);
                gg1.SelectedIndex = 0;
                _my._CrtIngredient._FilterBrew.Grind1Type = (byte)(_my._CrtEVOMachine._BeanHopperUint[0].Powdertype+0x80);
            }
            else if (_my._CrtEVOMachine._BeanHopperUint.Count == 2)
            {
                tmp = new ComboBoxItem();
                tmp.Tag = _my._CrtEVOMachine._BeanHopperUint[0].Powdertype;
                tmp.Content = _my._CrtEVOMachine._BeanHopperUint[0].GetPowdertype();
                gg1.Items.Add(tmp);
                gg1.SelectedIndex = 0;
                tmp = new ComboBoxItem();
                tmp.Tag = _my._CrtEVOMachine._BeanHopperUint[1].Powdertype;
                tmp.Content = _my._CrtEVOMachine._BeanHopperUint[1].GetPowdertype();
                gg2.Items.Add(tmp);
                gg2.SelectedIndex = 0;
                gg1.IsEnabled = false;
                gg2.IsEnabled = false;
                _my._CrtIngredient._FilterBrew.Grind1Type =(byte) (_my._CrtEVOMachine._BeanHopperUint[0].Powdertype+0x80);
                _my._CrtIngredient._FilterBrew.Grind2Type =(byte) (_my._CrtEVOMachine._BeanHopperUint[1].Powdertype+0x81);

            }
        }
        private void FillPackageType()
        {
            cc1.Items.Clear();
            cc3.Items.Clear();
            ComboBoxItem tmp;
            tmp = new ComboBoxItem();
            tmp.Tag = null;
            tmp.Content = "Not Used";
            cc1.Items.Add(tmp);
            tmp = new ComboBoxItem();
            tmp.Tag = null;
            tmp.Content = "Not Used";
            cc3.Items.Add(tmp);
            foreach (var item in _my._CrtEVOMachine._CanisterUnit)
            {
                tmp = new ComboBoxItem();
                tmp.Tag = item;
                tmp.Content = item.GetPowdertype();
                cc1.Items.Add(tmp);
                tmp = new ComboBoxItem();
                tmp.Tag = item;
                tmp.Content = item.GetPowdertype();
                cc3.Items.Add(tmp);
            }
        }
        private void ReloadCanisterIngredient()
        {
            cc2.IsEnabled = false;
            cc4.IsEnabled = false;
            if (_my._CrtIngredient._InstantPowder.PackageOneType != 0)
            {
                foreach (var item in cc1.Items)
                {
                    ComboBoxItem tmp = item as ComboBoxItem;
                    if (tmp.Tag == null)
                    {
                        continue;
                    }
                    if ((tmp.Tag as CanisterUnit).Powdertype == _my._CrtIngredient._InstantPowder.PackageOneType)
                    {
                        cc1.SelectedItem = tmp;
                    }

                }
                cc2.IsEnabled = true;
            }
            else
            {
                cc1.SelectedIndex = 0;
            }
            if (_my._CrtIngredient._InstantPowder.PackageTwoType != 0)
            {
                foreach (var item in cc3.Items)
                {
                    ComboBoxItem tmp = item as ComboBoxItem;
                    if (tmp.Tag == null)
                    {
                        continue;
                    }
                    if ((tmp.Tag as CanisterUnit).Powdertype == _my._CrtIngredient._InstantPowder.PackageTwoType)
                    {
                        cc3.SelectedItem = tmp;
                    }
                }
                cc4.IsEnabled = true;
            }
            else
            {
                cc3.SelectedIndex = 0;

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.InitialDirectory = "DrinkPics\\";
            openFileDialog.Title = "Select File";
            openFileDialog.Filter = "png文件|*.png";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
            {
                string filepath = string.Format("DrinkPics\\b{0}.png", _my._CrtRecipeInfo.ID);
                File.Copy(openFileDialog.FileName, filepath, true);
                _my._CrtRecipeInfo.refresh();

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.Filter = "RCP文件|*.RCP|HEX文件|*.Hex|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            if ((bool)openFileDialog.ShowDialog().GetValueOrDefault())
            {
                switch (btn.Tag.ToString())
                {
                    case "RCP":
                        tbxrcp.Text = openFileDialog.FileName;
                        _my._EvoUpdate.RcpFileName = openFileDialog.FileName;
                        break;
                    case "FIRM":
                        tbxfirm.Text = openFileDialog.FileName;
                        _my._EvoUpdate.FirmFileName = openFileDialog.FileName;
                        break;
                    default:
                        break;
                }
            }

        }

        private void cc_changed(object sender, TextChangedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx.Name.Equals("cc1") || tbx.Name.Equals("cc2"))
            {
                if (!tbx.Text.Equals(string.Empty) && !tbx.Text.Equals("0"))
                {
                    _my._CrtIngredient._InstantPowder.PackageTwoType = 0;
                    _my._CrtIngredient._InstantPowder.PackageTwoAmt = 0;
                }
            }
            else
            {
                if (!tbx.Text.Equals(string.Empty) && !tbx.Text.Equals("0"))
                {
                    _my._CrtIngredient._InstantPowder.PackageOneType = 0;
                    _my._CrtIngredient._InstantPowder.PackageOneAmt = 0;
                }
            }
        }

        private void PackageoneType_Change(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                cc3.Items.Clear();
                ComboBoxItem tmpp = new ComboBoxItem();
                tmpp.Tag = null;
                tmpp.Content = "Not Used";
                cc3.Items.Add(tmpp);
                ComboBoxItem obj = (ComboBoxItem)((sender as ComboBox).SelectedItem);
                cc2.IsEnabled = true;

                if (obj.Tag == null)
                {
                    _my._CrtIngredient._InstantPowder.PackageOneType = 0;
                    _my._CrtIngredient._InstantPowder.PackageOneAmt = 0;
                    return;
                }
                CanisterUnit tmp1 = (CanisterUnit)obj.Tag;

                _my._CrtIngredient._InstantPowder.PackageOneType = tmp1.Powdertype;
                foreach (var item in _my._CrtEVOMachine._CanisterUnit)
                {
                    ComboBoxItem tmp = new ComboBoxItem();
                    if (tmp1.Postion == 0)
                    {
                        if (item.Postion == 1)
                        {
                            tmp.Tag = item;
                            tmp.Content = item.GetPowdertype();
                            cc3.Items.Add(tmp);
                            break;

                        }
                    }
                    if (tmp1.Postion == 1)
                    {
                        if (item.Postion == 0)
                        {
                            tmp.Tag = item;
                            tmp.Content = item.GetPowdertype();
                            cc3.Items.Add(tmp);
                            break;

                        }

                    }
                    if (tmp1.Postion == 2)
                    {
                        if (item.Postion == 3)
                        {
                            tmp.Tag = item;
                            tmp.Content = item.GetPowdertype();
                            cc3.Items.Add(tmp);
                            break;

                        }

                    }
                    if (tmp1.Postion == 3)
                    {
                        if (item.Postion == 2)
                        {
                            tmp.Tag = item;
                            tmp.Content = item.GetPowdertype();
                            cc3.Items.Add(tmp);
                            break;

                        }

                    }
                }
            }
            else
            {
                // _my._CrtIngredient._InstantPowder.PackageOneType = 0;
                // _my._CrtIngredient._InstantPowder.PackageOneAmt = 0;
                cc2.IsEnabled = false;

            }
        }

        private void PackagetwoType_Change(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                ComboBoxItem obj = (ComboBoxItem)((sender as ComboBox).SelectedItem);
                cc4.IsEnabled = true;
                if (obj.Tag == null)
                {
                    _my._CrtIngredient._InstantPowder.PackageTwoType = 0;
                    _my._CrtIngredient._InstantPowder.PackageTwoAmt = 0;
                    return;
                }
                CanisterUnit tmp1 = (CanisterUnit)obj.Tag;
                _my._CrtIngredient._InstantPowder.PackageTwoType = tmp1.Powdertype;


            }
            else
            {
                // _my._CrtIngredient._InstantPowder.PackageTwoType = 0;
                // _my._CrtIngredient._InstantPowder.PackageTwoAmt = 0;

                cc4.IsEnabled = false; ;

            }
        }

        private void menu_check(object sender, RoutedEventArgs e)
        {
            menuType tmp = (menuType)(int.Parse((sender as MenuItem).Tag.ToString()));
            //MessageBox.Show(tmp.ToString());
            Process myProcess;
            switch (tmp)
            {
                case menuType.MENU_CALIBRATION:
                    EvoCalibrationWin wina = new EvoCalibrationWin(_my._MachineInfo);
                    wina.Show();
                    break;
                case menuType.MENU_IMPORT:
                    FunctionFileManage.ImpportDB("mydb.dbs");
                    break;
                case menuType.MENU_EXPORT:
                    FunctionFileManage.ExportDB("mydb.dbs");
                    break;
                case menuType.MENU_COM:
                    _my.ComCmd.Execute(513);
                    break;
                case menuType.MENU_VISUAL:
                    break;
                case menuType.MENU_PIC:
                    myProcess = new Process();
                    try
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.FileName = "EVO.TOOL.MAKEPIC.exe";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.Message);
                    }
                    break;
                case menuType.MENU_USB:
                    myProcess = new Process();
                    try
                    {
                        myProcess.StartInfo.UseShellExecute = false;
                        myProcess.StartInfo.FileName = "EVO.TOOL.USBFILE.exe";
                        myProcess.StartInfo.CreateNoWindow = true;
                        myProcess.Start();
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1.Message);
                    } 
                    break;
                case menuType.MENU_HELP:
                    MessageBox.Show("Coming Soon!!!");
                    break;
                case menuType.MENU_ABOUT:
                    MessageBox.Show("V1.00 supported by Crem", "Infomation");
                    break;
                default:
                    break;
            }
        }
    }
    public enum menuType
    {
        MENU_IMPORT =1,
        MENU_EXPORT,
        MENU_COM,
        MENU_VISUAL,
        MENU_PIC,
        MENU_USB,
        MENU_HELP,
        MENU_ABOUT,
        MENU_CALIBRATION
    }
   
}
