using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CREM.EVO.MODEL;

namespace CREM.EVO.Utility
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class byteToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Enabled":
                        return ((int)value == 0) ? Visibility.Collapsed : Visibility.Visible;
                    case "Disabled":
                        return ((int)value > 0) ? Visibility.Collapsed : Visibility.Visible;
                    //case "Invaild":
                    //    return ((int)value == 2) ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }


    [ValueConversion(typeof(int), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Enabled":
                        return ((bool)value == true) ? Visibility.Collapsed : Visibility.Visible;
                    case "Disabled":
                        return ((bool)value == false) ? Visibility.Collapsed : Visibility.Visible;
                    //case "Invaild":
                    //    return ((int)value == 2) ? Visibility.Collapsed : Visibility.Visible;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }

    [ValueConversion(typeof(int), typeof(Visibility))]
    public class IngredientTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "ESPRESSO":
                        return ((IngredientType)value == IngredientType.ESPRESSO) ? Visibility.Visible : Visibility.Hidden;
                    case "FILTERBREW":
                        return ((IngredientType)value == IngredientType.FILTERBREW) ? Visibility.Visible : Visibility.Hidden;
                    case "INSTANTPOWDER":
                        return ((IngredientType)value == IngredientType.INSTANTPOWDER) ? Visibility.Visible : Visibility.Hidden;
                    case "FRESHMILK":
                        return ((IngredientType)value == IngredientType.FRESHMILK) ? Visibility.Visible : Visibility.Hidden;

                    case "WATER":
                        return ((IngredientType)value == IngredientType.Water) ? Visibility.Visible : Visibility.Hidden;
                
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }

    [ValueConversion(typeof(int), typeof(Boolean))]
    public class IntToIsChecked : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            return (int.Parse(value.ToString())) == 0 ? false : true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return ((bool)value == true) ? 1 : 0;
        }
    }

    [ValueConversion(typeof(int), typeof(int))]
    public class IntToselectIndex : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            int tmp =int.Parse(value.ToString());
            return (tmp) == 0 ? 1 : (tmp - 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();

            int tmp = int.Parse(value.ToString());
            return tmp + 1;
        }
    }

    /*      
            try
            {
                bimg = new BinaryReader(File.Open("images\\green.png", FileMode.Open));
                fileinfo = new FileInfo("images\\green.png");
                byttmp = bimg.ReadBytes((int)fileinfo.Length);
                bimg.Close();
                tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.StreamSource = new MemoryStream(byttmp);
                tmp.EndInit();
                _normalGreenpic = tmp;
     */
    [ValueConversion(typeof(int), typeof(BitmapImage))]
    public class IntToPicScr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            BinaryReader bimg;
            FileInfo fileinfo;
            byte[] byttmp;
            BitmapImage tmp = new BitmapImage();
            try
            {
                string filepath = string.Format("DrinkPics\\b{0}.png", value.ToString());

                bimg = new BinaryReader(File.Open(filepath, FileMode.Open));
                fileinfo = new FileInfo(filepath);
                byttmp = bimg.ReadBytes((int)fileinfo.Length);
                bimg.Close();
                tmp = new BitmapImage();
                tmp.BeginInit();
                tmp.StreamSource = new MemoryStream(byttmp);
                tmp.EndInit();
                return tmp;
            }
            catch (Exception)
            {
              return false;
            }
            
             
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }

    [ValueConversion(typeof(bool), typeof(string))]
    public class IntToStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            bool tmp = (bool)value;
            if (parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "OnOff":
                        return (tmp == true) ? "On" : "Off";
                    case "Level":
                        return (tmp == true) ? "High Level" : "Low Level";

                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }

    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class IntToErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return  new SolidColorBrush(Colors.Gray);
            }
            bool tmp = (bool)value;
            return (tmp == true) ? new SolidColorBrush(Colors.Red) :  new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }


    [ValueConversion(typeof(MachineState), typeof(string))]
    public class IntToModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "unknow";
            }
            MachineState tmp = (MachineState)value;
            string ret = "unknow";
            switch (tmp)
            {
                case MachineState.Mode_Normal:
                    ret = "Normal";
                    break;
                case MachineState.Mode_Sleep:
                    ret = "Sleep";
                    break;
                case MachineState.Mode_Backup:
                    ret = "Backup";
                    break;
                case MachineState.Mode_Clean:
                    ret = "Cleaning";
                    break;
                case MachineState.Mode_Clean_Finish:
                    ret = "CleanFinished";
                    break;
                case MachineState.Mode_Milk_Phase_Finish:
                    ret = "MilkFinished";
                    break;
                case MachineState.Mode_Dispense:
                    ret = "Dispensing";
                    break;
                case MachineState.Mode_Dispense_Finish:
                    ret = "DispenseFinished";
                    break;
                default:
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            return false;
        }
    }

    

}
