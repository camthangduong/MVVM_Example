using System.Collections.ObjectModel;
using System.Windows;

namespace Test_Data_Grid
{
    public class ViewModelMain : ViewModelBase
    {
        // Command for binding to the event        
        private RelayCommand homeCommand, exitButtonCommand;
        private TableViewModel _tableView;
        ObservableCollection<ClientInfo> clientList;

        public ViewModelMain()
        {
            ExitButtonCommand = new RelayCommand(ExitApp);
            HomeCommand = new RelayCommand(HomeEvent);
        }

        private void HomeEvent(object obj)
        {
            _tableView = new TableViewModel();
            CurrentVM = _tableView;
            Mediator.Register("StatusChange", TableStatusChange);
        }

        private void TableStatusChange(object obj)
        {
            clientList = (ObservableCollection<ClientInfo>)obj;
        }

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentVM
        {
            get { return _currentViewModel; }
            set
            {
                this._currentViewModel = value;
                this.OnPropertyChanged("CurrentVM");
            }
        }

        private void ExitApp(object obj)
        {

            if (_tableView != null)
            {
                // bool test = clientList[0].Selected;
            }
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Public for binding to Exit button
        /// </summary>
        public RelayCommand ExitButtonCommand
        {
            get { return this.exitButtonCommand; }
            set
            {
                this.exitButtonCommand = value;
            }
        }

        /// <summary>
        /// Public for binding to Home button
        /// </summary>
        public RelayCommand HomeCommand
        {
            get { return this.homeCommand; }
            set
            {
                this.homeCommand = value;
            }
        }
    }
}
