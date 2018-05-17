using Simple_Data_List.Library;
using Simple_Data_List.Models;
using System.ComponentModel;
using System.Windows.Data;

namespace Simple_Data_List.ViewModels
{
    public class PreStatusViewModel : ViewModelBase
    {
        private CollectionBinding<ClientInfo> clientList;
        private ICollectionView groupedClients;

        public ICollectionView GroupClients { get { return groupedClients; } set { groupedClients = value; SetProperty(ref groupedClients, value); } }

        public PreStatusViewModel()
        {
            clientList = new CollectionBinding<ClientInfo>();
        }

        private void GhostData()
        {
            GhostDataUtilities ghostUtil = new GhostDataUtilities();
            clientList = ghostUtil.GhostPreStatusData();
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        public CollectionBinding<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; SetProperty(ref clientList, value); }
        }

        public void InitializeData()
        {
            GhostData();
        }
    }
}
