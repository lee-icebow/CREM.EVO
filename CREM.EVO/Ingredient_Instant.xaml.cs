using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CREM.EVO.MODEL;
using CREM.EVO.Utility;

namespace CREM.EVO
{
    /// <summary>
    /// EVO.xaml 的交互逻辑
    /// </summary>
    public partial class Ingredient_Instant : Window
    {
        public IngredientInfo _IngredientInfo { get; set; }
        public UInt16 _StartTime { get; set; }
        public UInt16 _ID { get; set; }
        public EvoRecipe _RecipeInfo;
        private ObservableCollection<CanisterUnit> _CanisterUnit;
        public Ingredient_Instant(UInt16 Id, UInt16 stm, EvoRecipe rcp,ObservableCollection<CanisterUnit> a)
        {
            _RecipeInfo = rcp;
            _StartTime = stm;
            _ID = Id;
            _CanisterUnit = a;
            _IngredientInfo = new IngredientInfo(IngredientType.INSTANTPOWDER);
            SetIngredientInfo(_IngredientInfo, (IngredientInfo)_RecipeInfo._lstIngredientInfo.First(c => c.ID == _ID));
            InitializeComponent();
            this.DataContext = _IngredientInfo;
            tbstm.Text = stm.ToString();
            fillpowder();
            ReloadCanisterIngredient();
        }
        private void ReloadCanisterIngredient()
        {
            cc2.IsEnabled = false;
            cc4.IsEnabled = false;
            if (_IngredientInfo._InstantPowder.PackageOneType != 0)
            {
                foreach (var item in cc1.Items)
                {
                    ComboBoxItem tmp = item as ComboBoxItem;
                    if (tmp.Tag == null)
                    {
                        continue;
                    }
                    if ((tmp.Tag as CanisterUnit).Powdertype == _IngredientInfo._InstantPowder.PackageOneType)
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
            if (_IngredientInfo._InstantPowder.PackageTwoType != 0)
            {
                foreach (var item in cc3.Items)
                {
                    ComboBoxItem tmp = item as ComboBoxItem;
                    if (tmp.Tag == null)
                    {
                        continue;
                    }
                    if ((tmp.Tag as CanisterUnit).Powdertype == _IngredientInfo._InstantPowder.PackageTwoType)
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
                    _IngredientInfo._InstantPowder.PackageOneType = 0;
                    _IngredientInfo._InstantPowder.PackageOneAmt = 0;
                    return;
                }
                CanisterUnit tmp1 = (CanisterUnit)obj.Tag;

                _IngredientInfo._InstantPowder.PackageOneType = tmp1.Powdertype;
                foreach (var item in _CanisterUnit)
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
                    _IngredientInfo._InstantPowder.PackageTwoType = 0;
                    _IngredientInfo._InstantPowder.PackageTwoAmt = 0;
                    return;
                }
                CanisterUnit tmp1 = (CanisterUnit)obj.Tag;
                _IngredientInfo._InstantPowder.PackageTwoType = tmp1.Powdertype;


            }
            else
            {
                // _my._CrtIngredient._InstantPowder.PackageTwoType = 0;
                // _my._CrtIngredient._InstantPowder.PackageTwoAmt = 0;

                cc4.IsEnabled = false; ;

            }
        }
        private void fillpowder()
        {
            ComboBoxItem tmp;
            tmp = new ComboBoxItem();
            tmp.Tag = null;
            tmp.Content = "Not Used";
            cc1.Items.Add(tmp);
            tmp = new ComboBoxItem();
            tmp.Tag = null;
            tmp.Content = "Not Used";
            cc3.Items.Add(tmp);
            foreach (var item in _CanisterUnit)
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

        private void SetIngredientInfo(IngredientInfo a, IngredientInfo b)
        {
            a.ID = b.ID;
            a.IsSelected = b.IsSelected;
            a.Name = b.Name;
            a.Type = b.Type;
            a.CrtModifyStatus = b.CrtModifyStatus;
            a._InstantPowder.MixIndex = b._InstantPowder.MixIndex;
            a._InstantPowder.PackageOneType = b._InstantPowder.PackageOneType;
            a._InstantPowder.PackageOneAmt = b._InstantPowder.PackageOneAmt;
            a._InstantPowder.PackageTwoType = b._InstantPowder.PackageTwoType;
            a._InstantPowder.PackageTwoAmt = b._InstantPowder.PackageTwoAmt;
            a._InstantPowder.AfterFlush = b._InstantPowder.AfterFlush;
            a._InstantPowder.PreFlush = b._InstantPowder.PreFlush;
            a._InstantPowder.WaterVolume = b._InstantPowder.WaterVolume;
            a._InstantPowder.WhipperSpeed = b._InstantPowder.WhipperSpeed;
            a._InstantPowder.UsedTime = b._InstantPowder.UsedTime;
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetIngredientInfo((IngredientInfo)_RecipeInfo._lstIngredientInfo.First(c => c.ID == _ID), _IngredientInfo);
            UInt16 stim;
            try
            {
               stim = UInt16.Parse(tbstm.Text);
            }
            catch (Exception)
            {

                stim = 0;
            }
            _RecipeInfo._lstRecipeInfo.First(c => c.IsSelected == true)._lstIngredientStep.First(a => a.ID == _ID).StartTime = stim;
            Function.XmlSerializer.SaveToXml("EVO.Ingredient.xml",_RecipeInfo, typeof(EvoRecipe), null);
            MessageBox.Show("Save Finished!");
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IngredientInfo tmp = new IngredientInfo(IngredientType.INSTANTPOWDER);
            SetIngredientInfo(tmp, _IngredientInfo);
            UInt16 stim;
            try
            {
                stim = UInt16.Parse(tbstm.Text);
            }
            catch (Exception)
            {

                stim = 0;
            }
            tmp.ID = ((IDGenrator)Function.XmlSerializer.LoadFromXml("EVO.ID.xml", typeof(IDGenrator))).GetID();
            tmp.Name = _IngredientInfo.Name + "_ID" + tmp.ID.ToString();
            _RecipeInfo._lstIngredientInfo.Add(tmp);
            _RecipeInfo._lstRecipeInfo.First(c => c.IsSelected == true)._lstIngredientStep.First(a => a.ID == _ID).StartTime = stim;
            _RecipeInfo._lstRecipeInfo.First(c => c.IsSelected == true)._lstIngredientStep.First(a => a.ID == _ID).Name = tmp.Name;
            _RecipeInfo._lstRecipeInfo.First(c => c.IsSelected == true)._lstIngredientStep.First(a => a.ID == _ID).ID = tmp.ID;
            Function.XmlSerializer.SaveToXml("EVO.Ingredient.xml", _RecipeInfo, typeof(EvoRecipe), null);
            MessageBox.Show("Save Finished!");
            this.Close();

        }
    }
}
