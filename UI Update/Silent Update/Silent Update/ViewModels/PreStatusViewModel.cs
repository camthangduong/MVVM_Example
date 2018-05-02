using Silent_Update.DAL;
using Silent_Update.Models;
using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace Silent_Update.ViewModels
{
    public class PreStatusViewModel : BindableBase
    {
        private BackgroundWorker backgroundWorker;
        private ClientInfoUtil clientUtils;
        private bool isCancelByUser;
        private MainWindowViewModel _mainView;
        private ProgressBarViewModel _workingBar;
        private List<ClientInfo> _clientData;

        /// <summary>
        /// Event handler
        /// </summary>
        public MyICommand<object> GroupHeaderEventHandler { get; private set; }

        /// <summary>
        /// Public data for binding
        /// </summary>
        public ICollectionView Clients { get; set; }
        public ICollectionView GroupClients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PreStatusViewModel(MainWindowViewModel mainView)
        {
            _mainView = mainView;
            _workingBar = new ProgressBarViewModel();
            clientUtils = new ClientInfoUtil();
            isCancelByUser = false;
            _clientData = new List<ClientInfo>();
            // Assing the event
            GroupHeaderEventHandler = new MyICommand<object>(GroupHeaderEvent);
            // CollectingClientData();
            InitializeBackgroundWorker();
        }

        /// <summary>
        /// Colelcting client file data based on the directory selected during the settinf
        /// </summary>        
        private void CollectingClientData ()
        {
            // During the collecting progress, the new Progress bar will be display with the ability to cancel
            // The option navigation menu will be disabed
            var _clients = GetGhostData();
            // Set the group view
            GroupClients = new ListCollectionView(_clients);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        /// <summary>
        /// Event handler when the checkbox data list
        /// </summary>
        /// <param name="sender"></param>
        private void GroupHeaderEvent(object sender)
        {
            switch ("TEST")
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// Create a ghost data to test
        /// </summary>
        /// <returns></returns>
        private List<ClientInfo> GetGhostData()
        {
            GhostDataUtilities _dataUtilities = new GhostDataUtilities();
            return _dataUtilities.GhostPreStatusData();
        }

        #region Background worker events
        private void InitializeBackgroundWorker ()
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
                EnableDisableNavigation(false);
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void EnableDisableNavigation(bool setting)
        {
            _mainView.HomeEnabled = setting;
            _mainView.SettingEnabled = setting;
            _mainView.PreviewEnabled = setting;
            _mainView.RunEnabled = setting;
            _mainView.SaveEnabled = setting;
            _mainView.ExitEnabled = setting;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker.CancellationPending == true)
            {
                isCancelByUser = true;
            }
            else if (!isCancelByUser && _clientData.Count > 0)
            {
                // ClientInfo curClient = _clientData[e.ProgressPercentage];                
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_clientData.Count < 1)
            {
                MessageBox.Show(Application.Current.FindResource("NoFile").ToString());
                // Set the group view
                GroupClients = new ListCollectionView(_clientData);
                GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            }
            else
            {
                // Set the group view
                GroupClients = new ListCollectionView(_clientData);
                GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
            }            
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            // Find the client fille in the the folder
            string destinatedFolderPath = EnviSetting.Instance.SourcePath;
            // Colelcting the data from path
            int index = 0;
            foreach (string acFilePath in SafeFileEnumerator.EnumerateFiles(destinatedFolderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".ac") || s.EndsWith(".ac_")))
            {
                if (isCancelByUser)
                {
                    worker.CancelAsync();
                    break;
                }
                ClientInfo curClient = new ClientInfo(acFilePath);
                // Add client to data list
                _clientData.Add(curClient);
                worker.ReportProgress(index);
                index++;
                Thread.Sleep(100);
            }
        }
        #endregion
    }
}
