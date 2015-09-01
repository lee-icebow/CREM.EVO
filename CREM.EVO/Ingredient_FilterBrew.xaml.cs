using System;
using System.Collections.Generic;
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
    public partial class Ingredient_FilterBrew : Window
    {
        public IngredientInfo _IngredientInfo { get; set; }
        public UInt16 _StartTime { get; set; }
        public UInt16 _ID { get; set; }
        public EvoRecipe _RecipeInfo;
        public Ingredient_FilterBrew(UInt16 Id,UInt16 stm,EvoRecipe rcp)
        {
           // _EvoRecipe = (EvoRecipe)Function.XmlSerializer.LoadFromXml("EVO.Ingredient.xml", typeof(EvoRecipe));
            _RecipeInfo = rcp;
            _StartTime = stm;
            _ID = Id;
            _IngredientInfo = new IngredientInfo(IngredientType.FILTERBREW);
            SetIngredientInfo(_IngredientInfo, (IngredientInfo)_RecipeInfo._lstIngredientInfo.First(c => c.ID == _ID));
            InitializeComponent();
            this.DataContext = _IngredientInfo;
            tbstm.Text = stm.ToString();
            tbpkg1.Text = Function.GetBeanType(_IngredientInfo._FilterBrew.Grind1Type);
            tbpkg2.Text = Function.GetBeanType(_IngredientInfo._FilterBrew.Grind2Type);

        }
        private void SetIngredientInfo(IngredientInfo a, IngredientInfo b)
        {
            a.ID = b.ID;
            a.IsSelected = b.IsSelected;
            a.Name = b.Name;
            a.Type = b.Type;
            a.CrtModifyStatus = b.CrtModifyStatus;
            a._FilterBrew.ActionDownPostion = b._FilterBrew.ActionDownPostion;
            a._FilterBrew.ActionUpPostion   = b._FilterBrew.ActionUpPostion  ;
            a._FilterBrew.Grind1Cnt         = b._FilterBrew.Grind1Cnt        ;
            a._FilterBrew.Grind2Cnt         = b._FilterBrew.Grind2Cnt        ;
            a._FilterBrew.Tm_DelayOpen      = b._FilterBrew.Tm_DelayOpen     ;
            a._FilterBrew.Tm_Depress        = b._FilterBrew.Tm_Depress       ;
            a._FilterBrew.Tm_Pre            = b._FilterBrew.Tm_Pre           ;
            a._FilterBrew.Tm_Press          = b._FilterBrew.Tm_Press         ;
            a._FilterBrew.WaterVolume = b._FilterBrew.WaterVolume;
            a._FilterBrew.Grind1Type = b._FilterBrew.Grind1Type;
            a._FilterBrew.Grind2Type = b._FilterBrew.Grind2Type;
            
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
            IngredientInfo tmp = new IngredientInfo(IngredientType.FILTERBREW);
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
