using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Test_Data_Grid
{
    public class TableViewModel : ViewModelBase
    {
        private ObservableCollection<ClientInfo> clientList;
        private ICollectionView groupedClients;
        private BusyView busyIndicator;
        private bool dataNotLoad;
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
            busyIndicator = new BusyView();
            clientList = new ObservableCollection<ClientInfo>();
            dataNotLoad = true;
            InitializeAsync();
        }

        private void InitializeAsync()
        {
            var _list = await CallLoadData();

            busyIndicator.Close();
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        private async Task<ObservableCollection<ClientInfo>> CallLoadData()
        {
            var task = Task.Run(() =>
            {
                clientList = FakeDatabaseLayer.GetPeopleFromDatabase();
            });

            return await (ObservableCollection<ClientInfo>)task.R;
        }

        private void ChangeStatus(object obj)
        {
            // TextBlock txtBlock = (TextBlock)obj;
            //string indexStr = txtBlock.Text;
            //MessageBox.Show(GroupClients.CurrentPosition.ToString());
            Mediator.NotifyColleagues("StatusChange", ClientList);
        }

        public ObservableCollection<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; }
        }
    }
}