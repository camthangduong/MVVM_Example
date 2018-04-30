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
        private MainWindowViewModel mainView; // This is the main view model, to enable the interaction to the UI button
        private List<ClientInfo> _clients;
        public MyICommand<object> GroupHeaderEventHandler { get; private set; }


        /// <summary>
        /// Public data for binding
        /// </summary>
        public ICollectionView Clients { get; set; }
        public ICollectionView GroupClients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PreviewPageViewModel(MainWindowViewModel _mainView)
        {            
            mainView = _mainView;
            // Assing the event
            GroupHeaderEventHandler = new MyICommand<object>(GroupHeaderEvent);
            // Ghost data            
            _clients = CreateGhostData();
            GroupClients = new ListCollectionView(_clients);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Grouping"));
        }

        public PreviewPageViewModel()
        {

        }

        private void GroupHeaderEvent(object sender)
        {
            switch ("TEST")
            {
                default:
                    break;
            }
        }

        private List<ClientInfo> CreateGhostData ()
        {
            // Hard-coded the path for testing
            string hardCodedPath = @"\\versions\Template\CAM\Client Files";
            // Get all the AC file from the path
            return GetFileFromTargetFolder(hardCodedPath);            
        }

        private List<ClientInfo> GetFileFromTargetFolder (string folderPath)
        {
            // Colelcting the data from path
            var _clients = new List<ClientInfo>();
            int index = 0;            
            foreach (string acFilePath in SafeFileEnumerator.EnumerateFiles(folderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".ac") || s.EndsWith(".ac_") || s.EndsWith(".AC") || s.EndsWith(".AC_")))
            {                
                ClientInfo curClient = new ClientInfo(index, acFilePath);
                curClient.Grouping = "To Be Rolled Forward";
                // Add client to data list
                _clients.Add(curClient);                
                index++;                
            }
            return _clients;
        }

        #region Public method for binding        
        private string currentFileName;

        public string CurrentFileName { get { return currentFileName; } set { currentFileName = value; SetProperty(ref currentFileName, value); } }        

        public List<ClientInfo> ClientData
        {
            get { return _clients; }
            set { this.ClientData = value; SetProperty(ref _clients, value); }
        }        
        #endregion
    }
}
