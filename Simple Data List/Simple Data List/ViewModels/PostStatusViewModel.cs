using Simple_Data_List.Library;
using Simple_Data_List.Models;
using System.ComponentModel;
using System.Windows.Data;

namespace Simple_Data_List.ViewModels
{
    public class PostStatusViewModel : ViewModelBase
    {
        private BackgroundWorker backgroundWorker;
        private CollectionBinding<ClientInfo> clientList;
        private ICollectionView groupedClients;
        private MainWindowViewModel mainVM;

        public ICollectionView GroupClients { get { return groupedClients; } set { groupedClients = value; NotifyPropertyChanged("ClientList"); SetProperty(ref groupedClients, value); } }

        public PostStatusViewModel(MainWindowViewModel _mainVM)
        {
            mainVM = _mainVM;
            clientList = new CollectionBinding<ClientInfo>();
            backgroundWorker = new BackgroundWorker();
            // Background Worker Process
            backgroundWorker.DoWork += BKDoWork;
            backgroundWorker.RunWorkerCompleted += BKCompleted;
            // Change status
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += BKChangedState;
            // Cancellation
            backgroundWorker.WorkerSupportsCancellation = true;
            // Start the worker
            if (!backgroundWorker.IsBusy)
            {
                mainVM.ToggleEnableProp(false);
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void BKChangedState(object sender, ProgressChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BKCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            mainVM.ToggleEnableProp(true);
        }

        private void BKDoWork(object sender, DoWorkEventArgs e)
        {
            GhostDataUtilities ghostUtil = new GhostDataUtilities();
            clientList = ghostUtil.GhostPreStatusData();
        }

        public CollectionBinding<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; SetProperty(ref clientList, value); }
        }
    }
}
