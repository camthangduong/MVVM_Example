using Data_Grid_Sample.Library;
using Data_Grid_Sample.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Data_Grid_Sample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private StatusPageViewModel preStatusVM;

        public MainWindowViewModel()
        {
            // Assign the Navigation event
            NavCommand = new RelayCommand<string>(OnNavigation);
        }

        /// <summary>
        /// Navigation Command
        /// </summary>
        public RelayCommand<string> NavCommand { get; private set; }

        /// <summary>
        /// Current View model active on the main window
        /// </summary>
        private ViewModelBase currentVM;
        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set
            {
                currentVM = value;
                this.OnPropertyChanged("CurrentVM");
            }
        }

        private void OnNavigation(string obj)
        {
            switch (obj)
            {
                case "home":
                    preStatusVM = new StatusPageViewModel();
                    CurrentVM = preStatusVM;
                    break;
                case "settings":
                    if (preStatusVM != null)
                    {
                        ObservableCollection<ClientInfo> clientList = preStatusVM.ClientList;
                    }
                    break;
                case "exit":
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
