using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
            clientList = new ObservableCollection<ClientInfo>();
            clientList = FakeDatabaseLayer.GetPeopleFromDatabase();
            GroupClients = new ListCollectionView(clientList);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            CheckedCommand = new RelayCommand(ChangeStatus);
        }

        private void ChangeStatus(object obj)
        {
            TextBlock txtBlock = (TextBlock)obj;
            string indexStr = txtBlock.Text;
            MessageBox.Show("Click");
        }

        public ObservableCollection<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; this.OnPropertyChanged("ClientList"); }
        }
    }
}