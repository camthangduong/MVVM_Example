using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    break;
                case "preview":
                    // Collecting data for client file
                    // Need to check for the user credetial correct before process
                    // Include the Directory location, back up location if needed
                    // Check for selected master template if needed                    
                    break;
                case "run":
                    // This will run the long process
                    // To Perform the selected task
                    // Need to check for the user credetial correct before process
                    // Check for any file selected to be process                    
                    CurrentViewModel = homePageViewModel;
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
