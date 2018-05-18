using Simple_Data_List.Library;
using System.ComponentModel;
using System.Windows;

namespace Simple_Data_List.ViewModels
{
    /// <summary>
    /// MainWindowViewModel
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Private variable for this class

        private PreStatusViewModel preStatusVM;
        private PostStatusViewModel postStatusVM;
        private ViewModelBase currentVM;

        private bool homeEnabled, settingsEnabled, exitEnabled;

        /// <summary>
        /// Enable/Disable the navigation button
        /// </summary>
        /// <param name="setting">TRUE/FALSE</param>
        public void ToggleEnableProp(bool setting)
        {
            HomeEnabled = setting;
            settingsEnabled = setting;
            ExitEnabled = setting;
        }

        /// <summary>
        /// Event handler for Relay Command
        /// </summary>
        /// <param name="obj"></param>
        private void OnNavigation(string obj)
        {
            switch (obj)
            {
                case "home":
                    ToggleEnableProp(true);
                    preStatusVM = new PreStatusViewModel(this);
                    CurrentVM = preStatusVM;
                    break;
                case "settings":
                    ToggleEnableProp(true);
                    postStatusVM = new PostStatusViewModel(this);
                    CurrentVM = postStatusVM;
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
            }
        }

        #endregion

        #region Constructor
        public MainWindowViewModel()
        {
            // Assign the Navigation event
            NavCommand = new RelayCommand<string>(OnNavigation);
            ToggleEnableProp(true);
        }
        #endregion

        #region Public method for binding purpose
        /// <summary>
        /// Navigation Command
        /// </summary>
        public RelayCommand<string> NavCommand { get; private set; }

        /// <summary>
        /// Current View model active on the main window
        /// </summary>
        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                RaisePropertyChange("CurrentVM");
            }
        }

        public bool HomeEnabled { get { return homeEnabled; } set { homeEnabled = value; RaisePropertyChange("HomeEnabled"); } }
        public bool SettingEnabled { get { return settingsEnabled; } set { settingsEnabled = value; RaisePropertyChange("SettingEnabled"); } }
        public bool ExitEnabled { get { return exitEnabled; } set { exitEnabled = value; RaisePropertyChange("ExitEnabled"); } }

        #endregion

        #region INotifyPropertyChanged interface implementation
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChange(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }
}
