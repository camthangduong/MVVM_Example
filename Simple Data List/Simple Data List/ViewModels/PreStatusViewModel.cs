using Simple_Data_List.Library;
using Simple_Data_List.Models;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Simple_Data_List.ViewModels
{
    public class PreStatusViewModel : ViewModelBase
    {

        private BackgroundWorker backgroundWorker;
        private CollectionBinding<ClientInfo> clientList;
        private ICollectionView groupedClients;
        private MainWindowViewModel mainVM;         

        public ICollectionView GroupClients { get { return groupedClients; } set { groupedClients = value; SetProperty(ref groupedClients, value); } }

        private RelayCommand<TextBlock> clientSelected;
        public RelayCommand<TextBlock> ClientStatusChange
        {
            get { return clientSelected; }
            private set { clientSelected = value; }
        }        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_mainVM"></param>
        public PreStatusViewModel()
        {            
            // mainVM = _mainVM;            
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
                // mainVM.ToggleEnableProp(false);
                backgroundWorker.RunWorkerAsync();
            }
            ClientStatusChange = new RelayCommand<TextBlock>(ClientSelectedEvent);
        }

        private void ClientSelectedEvent(object obj)
        {
            // Casting the object
            TextBlock indexObj = obj as TextBlock;
            string indexStr = indexObj.Text;
            // Convert string to int
            Regex digitsOnly = new Regex(@"[^\d]");
            int index = (int)(Int64.Parse(digitsOnly.Replace(indexStr, "")));
            // Set the seletion
            ClientList[index].Selected = !ClientList[index].Selected;            
        }

        private void BKChangedState(object sender, ProgressChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        private void BKCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            GroupClients = new ListCollectionView(clientList);
            // GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            // mainVM.ToggleEnableProp(true);            
        }

        private void BKDoWork(object sender, DoWorkEventArgs e)
        {
            GhostDataUtilities ghostUtil = new GhostDataUtilities();
            clientList = ghostUtil.GhostPreStatusData();
        }

        public CollectionBinding<ClientInfo> ClientList
        {
            get { return clientList; }
            set { clientList = value; }
        }
    }
}
