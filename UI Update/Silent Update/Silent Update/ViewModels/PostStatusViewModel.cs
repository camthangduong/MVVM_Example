using Silent_Update.DAL;
using Silent_Update.Models;
using Silent_Update.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Data;

namespace Silent_Update.ViewModels
{
    public class PostStatusViewModel : BindableBase
    {
        private BackgroundWorker backgroundWorker;
        private bool isCancelByUser;
        private ProgressBarViewModel _workingBar;
        private List<ClientInfo> clientData;

        /// <summary>
        /// Public data for binding
        /// </summary>
        private ICollectionView _groupedClients;
        public ICollectionView Clients { get; set; }
        public ICollectionView GroupClients { get { return _groupedClients; } set { _groupedClients = value; SetProperty(ref _groupedClients, value); } }

        public PostStatusViewModel(List<ClientInfo> _clientData)
        {
            clientData = _clientData;
            clientData = GetGhostData();
            _workingBar = new ProgressBarViewModel();
            InitializeBackgroundWorker();
        }

        /// <summary>
        /// Create a ghost data to test
        /// </summary>
        /// <returns></returns>
        private List<ClientInfo> GetGhostData()
        {
            GhostDataUtilities _dataUtilities = new GhostDataUtilities();
            return _dataUtilities.GhostPostStatusData();
        }

        #region Background worker events
        public void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();

            // Background Process
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

            // Progress
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;

            // Cancellation
            backgroundWorker.WorkerSupportsCancellation = true;

            if (!backgroundWorker.IsBusy)
            {
                _workingBar.ShowDialog();
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                isCancelByUser = true;
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _workingBar.CloseDialog();
            // Set the group view
            GroupClients = new ListCollectionView(clientData);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // Find the client fille in the the folder
            string destinatedFolderPath = EnviSetting.Instance.SourcePath;
            // Colelcting the data from path                        
            // Create a ghost data
            Thread.Sleep(100);

            Thread.Sleep(100);
        }
        #endregion
    }
}
