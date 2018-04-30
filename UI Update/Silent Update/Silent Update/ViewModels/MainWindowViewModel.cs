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

        public MainWindowViewModel()
        {
            // Event handler for icon navigation
            NavCommand = new MyICommand<string>(OnNav);

            preStatusViewModel = new PreStatusViewModel();
            homeViewModel = new HomeViewModel();
            postStatusViewModel = new PostStatusViewModel();
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
                    CurrentViewModel = preStatusViewModel;
                    break;
                case "preview":
                    // Collecting data for client file
                    // Need to check for the user credetial correct before process
                    // Include the Directory location, back up location if needed
                    // Check for selected master template if needed   
                    CurrentViewModel = postStatusViewModel;
                    break;
                case "run":
                    // This will run the long process
                    // To Perform the selected task
                    // Need to check for the user credetial correct before process
                    // Check for any file selected to be process                    
                    CurrentViewModel = homeViewModel;
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
    }
}
