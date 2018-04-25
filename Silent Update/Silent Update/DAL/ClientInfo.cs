using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent_Update.DAL
{
    public class ClientInfo : BindableBase
    {
        private bool selected, selectEnable;
        private string filePath, name, version, wpVersion, lastModified, analysis, grouping, newFilePath;

        #region Property will be display on the data table
        public bool Selected { get { return selected; } set { selected = value; SetProperty(ref selected, value); } }
        public bool SelectEnable { get { return selectEnable; } set { selectEnable = value; SetProperty(ref selectEnable, value); } }
        public string FilePath { get { return filePath; } set { filePath = value; } }
        public string NewFilePath { get { return newFilePath; } set { newFilePath = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string Version { get { return version; } set { version = value; } } // This can be combined between Audit version and Fin version
        public string WPVersion { get { return wpVersion; } set { wpVersion = value; } }
        public string LastModified { get { return lastModified; } set { lastModified = value; } }
        public string Analysis { get { return analysis; } set { analysis = value; } }
        public string Grouping { get { return grouping; } set { grouping = value; } } // This value should be the text to be display on group
        public int Index { get; set; } // Use as index for checkbox selection
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

        public void BuildDisplayVersion()
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
            Version = displayVersion;            
        }               

        #region Public method
        public bool IsAuditUpdateCompleted()
        {
            return AuditVersion != AuditVersionBefore; // If the Audit version and before is changed, then the update completed
        }

        public bool IsFinUpdateComplete()
        {
            return FinVersion != FinVersionBefore; // If the Fin version and before is changed, then the update completed
        }

        public bool IsYECCompleted()
        {
            return RunYEC == 1; // Check if the YEC process finished
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">This is the index of the file in the list, this will be use to ensure the selection is correct</param>
        /// <param name="acFilePath"></param>
        public ClientInfo(int index, string acFilePath)
        {
            Index = index;
            Selected = true;
            SelectEnable = true;
            FilePath = acFilePath;
            Name = System.IO.Path.GetFileNameWithoutExtension(FilePath);
            WPVersion = "";
            LastModified = "";
            Analysis = "";
            Grouping = "";
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
    }
}
