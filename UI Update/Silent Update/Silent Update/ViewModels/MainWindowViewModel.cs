using Silent_Update.Utilities;
using System.Windows;
using System.IO;
using System.ComponentModel;

namespace Silent_Update.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Navigation control command
        /// </summary>
        public MyICommand<string> NavCommand { get; private set; }

        private PreStatusViewModel preStatusViewModel;
        private HomeViewModel homeViewModel;
        private PostStatusViewModel postStatusViewModel;
        private SettingBothViewModel bothViewModel;
        private SettingHealthCheckViewModel healthCheckViewModel;
        private SettingUpdateViewModel updateViewModel;
        private SettingYECViewModel yecViewModel;

        public MainWindowViewModel()
        {
            // Event handler for icon navigation
            NavCommand = new MyICommand<string>(OnNav);            
            homeViewModel = new HomeViewModel();            
            bothViewModel = new SettingBothViewModel();
            healthCheckViewModel = new SettingHealthCheckViewModel();
            updateViewModel = new SettingUpdateViewModel();
            yecViewModel = new SettingYECViewModel();            

            HomeEnabled = true;
            SettingEnabled = true;
            PreviewEnabled = true;
            RunEnabled = true;
            SaveEnabled = true;
            ExitEnabled = true;

            CurrentViewModel = homeViewModel;
        }

        private void EnableDisableNavigation (bool setting)
        {
            HomeEnabled = setting;
            SettingEnabled = setting;
            PreviewEnabled = setting;
            RunEnabled = setting;
            SaveEnabled = setting;
            ExitEnabled = setting;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homeViewModel;                                        
                    break;
                case "settings":
                    // Getting the option selection
                    if (homeViewModel.BOTH)
                    {
                        // Both option selected
                        CurrentViewModel = bothViewModel;
                        EnviSetting.Instance.RunBoth = true;
                        EnviSetting.Instance.RunYEC = false;
                        EnviSetting.Instance.RunUpdate = false;
                    }
                    else if (homeViewModel.UPDATE)
                    {
                        // Update only option selection
                        CurrentViewModel = updateViewModel;
                        EnviSetting.Instance.RunBoth = false;
                        EnviSetting.Instance.RunYEC = false;
                        EnviSetting.Instance.RunUpdate = true;
                    }
                    else
                    {
                        // Year End Close only selected
                        CurrentViewModel = yecViewModel;
                        EnviSetting.Instance.RunBoth = false;
                        EnviSetting.Instance.RunYEC = true;
                        EnviSetting.Instance.RunUpdate = false;
                    }
                    break;
                case "preview":
                    // Check if the client directory is selected
                    string selectedDirectory = EnviSetting.Instance.SourcePath;
                    string backupDirectory = EnviSetting.Instance.BackupPath;                    
                    bool isContinue = true;                    
                    if (Directory.Exists(selectedDirectory))
                    {
                        // Check if the selection is Update/Both, if YES then the master selection need to be check
                        if (EnviSetting.Instance.RunBoth || EnviSetting.Instance.RunUpdate)
                        {
                            // Check if the master template is selected or there is template list
                            if (EnviSetting.Instance.MasterTemplate.Count < 1 || !EnviSetting.Instance.CheckIfMasterTemplateSelected())
                            {
                                // there is no master or none of them is selected
                                MessageBox.Show(Application.Current.FindResource("NoMaster").ToString());
                                break;
                            }
                        }
                        // Check if the update process and no back up location selected
                        if (EnviSetting.Instance.RunUpdate && !Directory.Exists(backupDirectory))
                        {
                            MessageBoxResult response = MessageBox.Show(Application.Current.FindResource("NoBKSelect").ToString());
                            isContinue = response == MessageBoxResult.OK || response == MessageBoxResult.Yes;
                        }
                        if (!isContinue)
                        {
                            // User select cancel
                            break;
                        }                        
                    }
                    else
                    {
                        // There is no selection
                        MessageBox.Show(Application.Current.FindResource("SelectDirectory").ToString());
                        break;
                    }
                    // Collecting the password list
                    EnviSetting.Instance.GetPasswordList();
                    // Collecting data for client file when the check is passed
                    EnableDisableNavigation(false);
                    preStatusViewModel = new PreStatusViewModel(this);
                    CurrentViewModel = preStatusViewModel;
                    // EnableDisableNavigation(true);
                    break;
                case "run":
                    // This will run the long process
                    // To Perform the selected task
                    // Need to check for the user credetial correct before process
                    // Check for any file selected to be process  
                    postStatusViewModel = new PostStatusViewModel();
                    CurrentViewModel = postStatusViewModel;
                    break;
                case "save":
                    // Save the current result to PDF file
                    // Open the PDF file with default application setting on the machine
                    break;
                case "exit":
                    // Need to reset the flag for Silent Update before shut down
                    Application.Current.Shutdown();
                    break;
                default:
                    CurrentViewModel = homeViewModel;
                    break;
            }
        }        

        /// <summary>
        /// Current active page
        /// </summary>
        private BindableBase _CurrentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                _CurrentViewModel = value;
                RaisePropertyChange("CurrentViewModel");
            }
        }

        /// <summary>
        /// Export the enable property when the process running
        /// </summary>
        private bool homeEnabled, settingEnabled, previewEnabled, runEnabled, saveEnabled, exitEnabled;

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChange (string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool HomeEnabled
        {
            get { return homeEnabled; }
            set { homeEnabled = value; RaisePropertyChange("HomeEnabled"); }
        }

        public bool SettingEnabled
        {
            get { return settingEnabled; }
            set { settingEnabled = value; RaisePropertyChange("SettingEnabled"); }
        }

        public bool PreviewEnabled
        {
            get { return previewEnabled; }
            set { previewEnabled = value; RaisePropertyChange("PreviewEnabled"); }
        }

        public bool RunEnabled
        {
            get { return runEnabled; }
            set { runEnabled = value; RaisePropertyChange("RunEnabled"); }
        }

        public bool SaveEnabled
        {
            get { return saveEnabled; }
            set { saveEnabled = value; RaisePropertyChange("SaveEnabled"); }
        }

        public bool ExitEnabled
        {
            get { return exitEnabled; }
            set { exitEnabled = value; RaisePropertyChange("ExitEnabled"); }
        }
    }
}
