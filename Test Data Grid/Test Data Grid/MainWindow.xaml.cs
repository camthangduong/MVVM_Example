using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace Test_Data_Grid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ElementBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button target = (Button)sender;
            ButtonProgressAssist.SetValue(target, 99);
        }

        private void ElementBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button target = (Button)sender;
            ButtonProgressAssist.SetValue(target, 0);
        }
    }
}
