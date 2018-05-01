using Silent_Update.Utilities;
using System.Windows;

namespace Silent_Update.ViewModels
{
    public class MainWindowViewModel : BindableBase
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

            preStatusViewModel = new PreStatusViewModel();
            homeViewModel = new HomeViewModel();
            postStatusViewModel = new PostStatusViewModel();
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
                    }
                    else if (homeViewModel.UPDATE)
                    {
                        // Update only option selection
                        CurrentViewModel = updateViewModel;
                    }
                    else
                    {
                        // Year End Close only selected
                        CurrentViewModel = yecViewModel;
                    }
                    break;
                case "preview":
                    // Check if the client directory is selected
                    // Check if the selection is Update/Both, if YES then the master selection need to be check
                    // Collecting data for client file when the check is passed
                    // Need to check for the user credetial correct before process, get all the password from the password list
                    // Include the Directory location, back up location if needed
                    // Check for selected master template if needed   
                    CurrentViewModel = preStatusViewModel;
                    break;
                case "run":
                    // This will run the long process
                    // To Perform the selected task
                    // Need to check for the user credetial correct before process
                    // Check for any file selected to be process                    
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
                SetProperty(ref _CurrentViewModel, value);
            }
        }

        /// <summary>
        /// Export the enable property when the process running
        /// </summary>
        private bool homeEnabled, settingEnabled, previewEnabled, runEnabled, saveEnabled, exitEnabled;

        public bool HomeEnabled
        {
            get { return homeEnabled; }
            set { homeEnabled = value; SetProperty(ref homeEnabled, value); }
        }

        public bool SettingEnabled
        {
            get { return settingEnabled; }
            set { settingEnabled = value; SetProperty(ref settingEnabled, value); }
        }

        public bool PreviewEnabled
        {
            get { return previewEnabled; }
            set { previewEnabled = value; SetProperty(ref previewEnabled, value); }
        }

        public bool RunEnabled
        {
            get { return runEnabled; }
            set { runEnabled = value; SetProperty(ref runEnabled, value); }
        }

        public bool SaveEnabled
        {
            get { return saveEnabled; }
            set { saveEnabled = value; SetProperty(ref saveEnabled, value); }
        }

        public bool ExitEnabled
        {
            get { return exitEnabled; }
            set { exitEnabled = value; SetProperty(ref exitEnabled, value); }
        }
    }
}
