using Silent_Update.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Silent_Update.Utilities
{
    /// <summary>
    /// Implement as singleton instance
    /// </summary>
    public sealed class EnviSetting
    {
        private static EnviSetting instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public EnviSetting()
        {
            MasterTemplate = new ObservableCollection<TemplateInfo>();
            Username = new List<string>();
            Password = new List<string>();
            RunBoth = true;
            RunYEC = false;
            RunUpdate = false;
            SourcePath = "";
            BackupPath = "";
            Hours = "";
            OPName = true;
            CLName = false;
            FindInRange = false;
            LockDownOriginal = false;
        }

        /// <summary>
        /// Export public singleton
        /// </summary>
        public static EnviSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EnviSetting();
                }
                return instance;
            }
        }

        public bool RunYEC { get; set; }
        public bool RunUpdate { get; set; }
        public bool RunBoth { get; set; }
        public string SourcePath { get; set; }
        public string BackupPath { get; set; }
        public List<string> Username { get; set; }
        public List<string> Password { get; set; }
        public bool LockDownOriginal { get; set; }
        public bool FindInRange { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool EnableHours { get; set; }
        public string Hours { get; set; }
        public bool OPName { get; set; }
        public bool CLName { get; set; }
        public string NameDateFormat { get; set; }
        public ObservableCollection<TemplateInfo> MasterTemplate { get; set; }

        public bool CheckIfMasterTemplateSelected ()
        {
            bool isSelected = false;
            if (this.MasterTemplate.Count > 0)
            {
                foreach (TemplateInfo item in this.MasterTemplate)
                {
                    if (item.Selected)
                    {
                        isSelected = true;
                        break;
                    }
                }
            }
            return isSelected;
        }

        public void GetPasswordList ()
        {            
            // Check for the password file
            try
            {
                string[] lines = System.IO.File.ReadAllLines("Password.txt");

                foreach (string line in lines)
                {
                    string[] credential = line.Split('/');

                    if (credential.Length == 2 && credential[0] != "" && credential[1] != "")
                    {
                        // Get Username
                        Username.Add(credential[0]);
                        // Get Password
                        Password.Add(credential[1]);
                    }
                }
            }
            catch (System.IO.FileNotFoundException){}
        }
    }
}
