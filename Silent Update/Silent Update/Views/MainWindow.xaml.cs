using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Silent_Update
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Current active button
        /// </summary>
        private TextBlock activeOption;

        public MainWindow()
        {
            InitializeComponent();
            /// Init the UI
            RemoveAllHighLight();
            activeOption = home;
            preview.IsEnabled = false;
            run.IsEnabled = false;
            save.IsEnabled = false;
            HighLightHome();
        }

        private void HighLightHome()
        {
            HighLightText(activeOption, true);
            HighLightText(home, true);
        }

        private void HighLightSetting()
        {
            HighLightText(activeOption, true);
            HighLightText(settings, true);
        }

        private void HighLightPreview()
        {
            HighLightText(activeOption, true);
            HighLightText(preview, true);
        }

        private void HighLightRun()
        {
            HighLightText(activeOption, true);
            HighLightText(run, true);
        }

        private void HighLightSave()
        {
            HighLightText(activeOption, true);
            HighLightText(save, true);
        }

        private void BtnMouseEnter(object sender, MouseEventArgs e)
        {
            // Get the name of the object sender
            RemoveAllHighLight();
            TextBlock current = sender as TextBlock;
            string name = current.Name;
            switch (name)
            {
                case "home":
                    HighLightHome();
                    break;
                case "settings":
                    // YEC only setting
                    HighLightSetting();
                    break;
                case "preview":
                    HighLightPreview();
                    break;
                case "run":
                    HighLightRun();
                    break;
                case "save":
                    HighLightSave();
                    break;
                case "exit":
                    // Need to reset the flag for Silent Update before shut down
                    HighLightText(activeOption, true);
                    HighLightText(exit, true);
                    break;
                default:
                    HighLightHome();
                    break;
            }
        }

        private void RemoveAllHighLight()
        {
            HighLightText(home, false);
            HighLightText(settings, false);
            HighLightText(preview, false);
            HighLightText(run, false);
            HighLightText(save, false);
            HighLightText(exit, false);
        }

        private void BtnMouseLeave(object sender, MouseEventArgs e)
        {
            RemoveAllHighLight();
            HighLightText(activeOption, true);
        }

        private void BtnMouseDown(object sender, MouseEventArgs e)
        {
            // Get the name of the object sender
            RemoveAllHighLight();
            TextBlock current = sender as TextBlock;
            activeOption = current;
            HighLightText(activeOption, true);
            string name = current.Name;
            switch (name)
            {
                case "home":
                    preview.IsEnabled = false;
                    run.IsEnabled = false;
                    save.IsEnabled = false;
                    break;
                case "settings":
                    // YEC only setting
                    preview.IsEnabled = true;
                    run.IsEnabled = false;
                    save.IsEnabled = false;
                    break;
                case "preview":
                    preview.IsEnabled = true;
                    run.IsEnabled = true;
                    save.IsEnabled = false;
                    break;
                case "run":
                    preview.IsEnabled = true;
                    run.IsEnabled = true;
                    save.IsEnabled = true;
                    HighLightRun();
                    break;
                case "save":
                    break;
                case "exit":
                    break;
                default:
                    HighLightHome();
                    preview.IsEnabled = false;
                    run.IsEnabled = false;
                    save.IsEnabled = false;
                    break;
            }
        }

        private void HighLightText(TextBlock target, bool isHighlight)
        {
            if (isHighlight)
            {
                target.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                target.Foreground = new SolidColorBrush(Color.FromArgb(68, 173, 173, 173));
            }
        }
    }
}
