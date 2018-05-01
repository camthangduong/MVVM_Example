using System;
using System.ComponentModel;

namespace Silent_Update.Models
{
    public class ClientInfo : INotifyPropertyChanged
    {
        #region Properties will be displayed on data table
        private bool _selected;
        public bool Selected { get { return _selected; } set { _selected = value; NotifyPropertyChanged("Selected"); } } // Selection checkbox

        private string _name;
        public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } } // The file name

        private string _filePath;
        public string FilePath { get { return _filePath; } set { _filePath = value; NotifyPropertyChanged("FilePath"); } } // The file path

        private string _newFilePath;
        public string NewFilePath { get { return _newFilePath; } set { _newFilePath = value; NotifyPropertyChanged("NewFilePath"); } } // The file path after YEC

        private string _version;
        public string Version { get { return BuildDisplayVersion(); } set { _version = value; NotifyPropertyChanged("Vesion"); } } // This is the version display on the table
        // This version can be combined between the version of Audit and FIN

        private string _wpVrsion;
        public string WPVersion { get { return _wpVrsion; } set { _wpVrsion = value; NotifyPropertyChanged("WPVersion"); } } // This is the working papers where the last time file opened                                                                                                                             

        private string _lastModified;
        public string LastModified { get { return _lastModified; } set { _lastModified = value; NotifyPropertyChanged("LastModified"); } }

        private string _group;
        public string Group { get { return _group; } set { _group = value; NotifyPropertyChanged("Group"); } } // Group is the grouping where the file is belonging to

        private string _analysis;
        public string Analysis { get { return _analysis; } set { _analysis = value; NotifyPropertyChanged("Analysis"); } } // The analytic message
        #endregion

        #region Property will be hidden and use only in background
        public bool AuditFile { get; set; } // Is Audit client file
        public bool FinFile { get; set; } // Is Financial client file
        public string AuditVersion { get; set; } // Audit part version if has
        public string FinVersion { get; set; } // Fin part version if has
        public string AuditVersionBefore { get; set; } // Audit part version before update if has
        public string FinVersionBefore { get; set; } // Fin part version before update if has
        public string AuditMaster { get; set; }
        public string FinMaster { get; set; }
        public bool IsProtected { get; set; }
        public bool IsSignedOut { get; set; }
        public bool IsLocked { get; set; }
        public bool IsSync { get; set; }
        public bool IsCheckout { get; set; }
        public DateTime YearEnd { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsZip { get; set; } // This will indicate the file was zip before, and should be compressed after all performance
        public int RunYEC { get; set; } // 1 is completed, 0 is not run, -1 failed to completed
        #endregion

        #region Analysis message section
        public string Error { get; set; } // Error message if there is
        public bool ShouldRun { get; set; } // Tracker to see if the client should continue to run
        #endregion

        #region Constructor
        public ClientInfo(string acFilePath)
        {
            Selected = true;
            FilePath = acFilePath;
            Name = System.IO.Path.GetFileNameWithoutExtension(FilePath);
            WPVersion = "";
            LastModified = "";
            Analysis = "";
            Group = "";
            // Versioning
            AuditVersionBefore = "";
            FinVersionBefore = "";
            Version = "";
            NewFilePath = "";

            // Default setting values
            IsZip = FilePath.LastIndexOf(".ac_") > 0 || FilePath.LastIndexOf(".AC_") > 0;
            ShouldRun = true;
            IsProtected = false;
            IsSignedOut = false;
            IsLocked = false;
            IsSync = false;
            IsCheckout = false;
            Error = "";
        }
        #endregion

        #region Public method
        public bool IsAuditUpdateCompleted()
        {
            return AuditVersion != AuditVersionBefore;
        }

        public bool IsFinUpdateComplete()
        {
            return FinVersion != FinVersionBefore;
        }

        public bool IsYECCompleted()
        {
            return RunYEC == 1;
        }

        public string BuildDisplayVersion()
        {
            string displayVersion = AuditVersion;
            if (displayVersion == "")
            {
                displayVersion = FinVersion;
            }
            else if (FinVersion != "")
            {
                displayVersion = displayVersion + Environment.NewLine + FinVersion;
            }
            return displayVersion;
        }
        #endregion

        #region Implemetation of the interface
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
