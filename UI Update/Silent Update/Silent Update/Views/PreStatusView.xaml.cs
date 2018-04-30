using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Silent_Update.Views
{
    /// <summary>
    /// Interaction logic for PreStatusView.xaml
    /// </summary>
    public partial class PreStatusView : UserControl
    {
        public PreStatusView()
        {
            InitializeComponent();
        }        
    }

    public class YesNoToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                case "to be process":
                    return true;
                case "no":
                case "non":
                case "not be process":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }

    public class YesToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                case "to be process":
                    return true;
                case "no":
                case "non":
                case "not be process":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "yes";
        }
    }
}
