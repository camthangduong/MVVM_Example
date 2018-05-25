using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Test_Data_Grid
{
    public class TableViewModel : ViewModelBase
    {
        private ObservableCollection<ClientInfo> clientList;
        private ICollectionView groupedClients;
        // private static BusyView busyIndicator;
        private static bool dataNotLoad;        

        public ICollectionView GroupClients
        {
            get { return groupedClients; }
            set
            {
                groupedClients = value;
                this.OnPropertyChanged("ClientList");
                this.OnPropertyChanged("GroupClients");
            }
        }

        public RelayCommand CheckedCommand { get; set; }

        public TableViewModel()
        {
            CheckedCommand = new RelayCommand(ChangeStatus);
            
            clientList = new ObservableCollection<ClientInfo>();
            dataNotLoad = true;
            InitializeMultiThread();
            InitializeAsync();
        }

        private void InitializeMultiThread ()
        {
            ///
            /// This is not asynchronous programming
            /// This is called multithreaded programming
            /// When you create a task using the StartNew method
            /// The body of the the task run on a thread pool thread
            ///
            Task<ObservableCollection<ClientInfo>> getList = Task<ObservableCollection<ClientInfo>>.Factory.StartNew(() =>
            {
                // When start new thread                 
                var resultList = FakeDatabaseLayer.GetPeopleFromDatabase();
                return resultList;
            });
            getList.ContinueWith(t => {
                dataNotLoad = false;
            });
            // Thread.Sleep(100);
            // busyIndicator.ShowDialog();
            while (dataNotLoad)
            {
            }
            clientList = getList.Result;
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }


        /// <summary>
        /// APM: Asynchronouse Programming Model .NET >= 1
        /// EAP: Event-Based Async Pattern .NET >= 2
        /// Tasks .NET >= 4
        /// </summary>
        private void InitializeAsync()
        {            
            
        }        

        private void ChangeStatus(object obj)
        {
            ///
            /// Raise the change to register
            ///
            Mediator.NotifyColleagues("StatusChange", ClientList);
        }

        public ObservableCollection<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; }
        }
    }
}