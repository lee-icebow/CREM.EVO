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
using System.Windows.Shapes;
using CREM.EVO.MODEL;

namespace CREM.EVO
{
    /// <summary>
    /// IngredientSelecWin.xaml 的交互逻辑
    /// </summary>
    public partial class IngredientSelecWin : Window
    {
        public IngredientSelecWin()
        {
            InitializeComponent();
        }
        private IngredientType _type = IngredientType.NoSelect;

        public IngredientType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _type = IngredientType.NoSelect;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem != null)
            {
                if (cb.SelectedIndex==0)
                {
                    _type = IngredientType.ESPRESSO;
                }
                if (cb.SelectedIndex == 1)
                {
                    _type = IngredientType.FILTERBREW;
                }
                if (cb.SelectedIndex == 2)
                {
                    _type = IngredientType.INSTANTPOWDER;
                }
                if (cb.SelectedIndex == 3)
                {
                    _type = IngredientType.FRESHMILK;
                }
                if (cb.SelectedIndex == 4)
                {
                    _type = IngredientType.Water;
                }
            }
        }
    }
}
