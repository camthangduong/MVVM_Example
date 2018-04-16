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

        /// <summary>
        /// Set of setting pages
        /// </summary>
        private HomePageViewModel homePageViewModel = new HomePageViewModel();
        private BothSetViewModel bothPageViewModel = new BothSetViewModel();
        private UpdateSetViewModel updatePageViewModel = new UpdateSetViewModel();
        private YECSetViewModel yecPageViewModel = new YECSetViewModel();
        private PreviewPageViewModel previewPageViewModel = new PreviewPageViewModel();
        private PostPageViewModel postPageViewModel = new PostPageViewModel();

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
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homePageViewModel;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homePageViewModel;
                    break;
                case "settings":
                    CurrentViewModel = bothPageViewModel;
                    break;
                case "preview":
                    // Collecting data for client file
                    // Need to check for the user credetial correct before process
                    // Include the Directory location, back up location if needed
                    // Check for selected master template if needed   
                    CurrentViewModel = previewPageViewModel;
                    break;
                case "run":
                    // This will run the long process
                    // To Perform the selected task
                    // Need to check for the user credetial correct before process
                    // Check for any file selected to be process                    
                    CurrentViewModel = postPageViewModel;
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
                    CurrentViewModel = homePageViewModel;
                    break;
            }
        }
    }
}
