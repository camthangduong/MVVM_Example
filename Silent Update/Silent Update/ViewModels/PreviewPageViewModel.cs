using Silent_Update.DAL;
using Silent_Update.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Silent_Update.ViewModels
{
    public class PreviewPageViewModel : BindableBase
    {
        private bool isCancelByUser; // This is tracking if the user decide to cancel the process
        private MainWindowViewModel mainView; // This is the main view model, to enable the interaction to the UI button
        private List<ClientInfo> _client;        

        /// <summary>
        /// Constructor
        /// </summary>
        public PreviewPageViewModel(MainWindowViewModel _mainView)
        {
            isCancelByUser = false;
            mainView = _mainView;
            // Ghost data
            _client = new List<ClientInfo>();
            CreateGhostData();            
        }

        public PreviewPageViewModel()
        {

        }

        private void CreateGhostData ()
        {
            // Hard-coded the path for testing
            string hardCodedPath = @"\\versions\Template\CAM\Client Files";
            // Get all the AC file from the path
            GetFileFromTargetFolder(hardCodedPath);            
        }

        private void GetFileFromTargetFolder (string folderPath)
        {
            // Colelcting the data from path
            int index = 0;            
            foreach (string acFilePath in SafeFileEnumerator.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".ac") || s.EndsWith(".ac_") || s.EndsWith(".AC") || s.EndsWith(".AC_")))
            {                
                ClientInfo curClient = new ClientInfo(index, acFilePath);
                curClient.Grouping = "To Be Rolled Forward";
                // Add client to data list
                _client.Add(curClient);                
                index++;                
            }
            GroupedClientData = new ListCollectionView(_client);
            GroupedClientData.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
            string stop = "";
        }

        #region Public method for binding        
        private string currentFileName;

        public string CurrentFileName { get { return currentFileName; } set { currentFileName = value; SetProperty(ref currentFileName, value); } }        

        public List<ClientInfo> ClientData
        {
            get { return _client; }
            set { this.ClientData = value; SetProperty(ref _client, value); }
        }

        private ICollectionView groupedClientData;
        public ICollectionView GroupedClientData
        {
            get { return groupedClientData; }
            private set
            {
                groupedClientData = value;
                SetProperty(ref groupedClientData, value);
            }
        }
        #endregion
    }
}
