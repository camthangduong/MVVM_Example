using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Test_Data_Grid
{
    public class ViewModelMain : ViewModelBase
    {
        // Command for binding to the event        
        private RelayCommand homeCommand, exitButtonCommand;
        private TableViewModel _tableView;

        public ViewModelMain()
        {
            ExitButtonCommand = new RelayCommand(ExitApp);
            HomeCommand = new RelayCommand(HomeEvent);                                   
        }

        private void HomeEvent(object obj)
        {
            _tableView = new TableViewModel();
            CurrentVM = _tableView;
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
            ObservableCollection<ClientInfo> clientList;
            if (_tableView != null)
            {
                clientList = _tableView.ClientList;
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
