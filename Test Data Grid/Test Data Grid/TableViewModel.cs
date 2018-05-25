using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Test_Data_Grid
{
    public class TableViewModel : ViewModelBase
    {
        private ObservableCollection<ClientInfo> clientList;
        private ICollectionView groupedClients;
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
            Initialize();
        }

        private void Initialize()
        {
            clientList = new ObservableCollection<ClientInfo>();
            clientList = FakeDatabaseLayer.GetPeopleFromDatabase();
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
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