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
using System.Windows.Shapes;
using CREM.EVO.MODEL;
namespace CREM.EVO
{
    /// <summary>
    /// EVO.xaml 的交互逻辑
    /// </summary>
    public partial class EVO : Window
    {
        public event MouseButtonEventHandler myclick;
        public ObservableCollection<IngredientInfo> _lstIngredientInfo { get; set; }
        public IngredientInfo _CRTIngredientInfo { get; set; }
        private IngredientStep _IngredientStep;
        public EVO()
        {
            InitializeComponent();
            _CRTIngredientInfo = new IngredientInfo();
            _IngredientStep = new IngredientStep();
            
        }
        public void  InitEVO(ObservableCollection<IngredientInfo> a)
        {
            _lstIngredientInfo = a;
            this.cb.ItemsSource = _lstIngredientInfo;
            this.tx.DataContext = _IngredientStep;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            
            _CRTIngredientInfo = (cb.SelectedItem as IngredientInfo);
            _IngredientStep.ID = _CRTIngredientInfo.ID;
            _IngredientStep.Name = _CRTIngredientInfo.Name;
            _IngredientStep.ScaleRate = 100;
            switch (_CRTIngredientInfo.Type)
            {
                case IngredientType.ESPRESSO:
                    _IngredientStep.UsedTime = _CRTIngredientInfo._Espresso.UsedTime;
                    break;
                case IngredientType.FILTERBREW:
                    _IngredientStep.UsedTime = _CRTIngredientInfo._FilterBrew.UsedTime;
                    break;
                case IngredientType.FRESHMILK:
                    _IngredientStep.UsedTime = _CRTIngredientInfo._FreshMilk.UsedTime;
                    break;
                case IngredientType.INSTANTPOWDER:
                    _IngredientStep.UsedTime = _CRTIngredientInfo._InstantPowder.UsedTime;
                    break;
                case IngredientType.NoSelect:
                    _IngredientStep.UsedTime = 0;
                    break;
                default:
                    break;
            }
            _IngredientStep._Type = (byte)_CRTIngredientInfo.Type;
            myclick.BeginInvoke(_IngredientStep, null, null, null);
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        

    }
    
}
