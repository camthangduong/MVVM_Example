﻿using SilentUpdate.Utilities;
using System.Windows;

namespace SilentUpdate.ViewModels
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
        private PreviewPageViewModel previewPageViewModel;

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

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            previewPageViewModel = new PreviewPageViewModel(this);
            CurrentViewModel = previewPageViewModel;
            HomeEnabled = true;
            SettingEnabled = true;
            PreviewEnabled = true;
            RunEnabled = true;
            SaveEnabled = true;
            ExitEnabled = true;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    previewPageViewModel = new PreviewPageViewModel(this);
                    CurrentViewModel = previewPageViewModel;
                    break;
                case "settings":
                    // Getting the option selection                    
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
                    previewPageViewModel = new PreviewPageViewModel(this);
                    CurrentViewModel = previewPageViewModel;
                    break;
            }
        }
    }
}
