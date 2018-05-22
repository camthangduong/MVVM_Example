using Data_Grid_Sample.Library;
using Data_Grid_Sample.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace Data_Grid_Sample.ViewModels
{
    public class StatusPageViewModel : ViewModelBase
    {
        public StatusPageViewModel()
        {
            CheckedChange = new RelayCommand<int>(OnCheckBoxChanged);
            GhostDataUtilities ghostUtil = new GhostDataUtilities();
            ClientList = ghostUtil.GhostPreStatusData();
            GroupClients = new ListCollectionView(ClientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            GroupClients.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("ClientList");
            this.OnPropertyChanged("GroupClients");
        }

        private void OnCheckBoxChanged(int index)
        {
            //
            // clientList[index].Selected = !ClientList[index].Selected;
        }

        private ObservableCollection<ClientInfo> clientList;
        public ObservableCollection<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; this.OnPropertyChanged("ClientList"); }
        }

        private ICollectionView groupedClients;
        public ICollectionView GroupClients
        {
            get { return groupedClients; }
            set
            {
                groupedClients = value;
                // 
                this.OnPropertyChanged("GroupClients");
                this.OnPropertyChanged("ClientList");
            }
        }

        public RelayCommand<int> CheckedChange { get; private set; }


    }
}
