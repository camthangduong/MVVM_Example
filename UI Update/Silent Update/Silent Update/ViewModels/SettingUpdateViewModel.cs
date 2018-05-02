using Silent_Update.DAL;
using Silent_Update.Models;
using Silent_Update.Utilities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Silent_Update.ViewModels
{
    public class SettingUpdateViewModel : BindableBase
    {
        // Command
        public MyICommand<string> FolderCommand { get; private set; }
        public MyICommand<object> OnKeyUp { get; private set; }
        public MyICommand<TextBlock> TemplateSelect { get; private set; }
        private string _password = "";

        private void OnTemplateSelectionChange(TextBlock obj)
        {
            // Casting the object
            TextBlock indexObj = obj as TextBlock;
            string indexStr = indexObj.Text;
            // Convert string to int
            Regex digitsOnly = new Regex(@"[^\d]");
            int index = (int)(Int64.Parse(digitsOnly.Replace(indexStr, "")));
            // Set the seletion
            Templates[index].Selected = !Templates[index].Selected;
            EnviSetting.Instance.MasterTemplate[index].Selected = Templates[index].Selected;
        }

        private void OnCommandFolderClick(string commandParams)
        {
            string targetPath = OnOpenFolder(commandParams);
            switch (commandParams)
            {
                case "backup":
                    // back directory click
                    Backup = targetPath;
                    EnviSetting.Instance.BackupPath = targetPath;
                    break;
                default:
                    Source = targetPath;
                    EnviSetting.Instance.SourcePath = targetPath;
                    // If nothing launch for source
                    break;
            }
        }

        private void OnKeyUpRelease(object obj)
        {
            if (obj is PasswordBox)
            {
                PasswordBox pw = obj as PasswordBox;
                _password = pw.Password.ToString();
                if (EnviSetting.Instance.Password.Count < 1)
                {
                    EnviSetting.Instance.Password.Add(_password);
                }
                else
                {
                    EnviSetting.Instance.Password[0] = _password;
                }
            }
            else if (obj is TextBox)
            {
                TextBox user = obj as TextBox;
                Username = user.Text.ToString();
                if (EnviSetting.Instance.Username.Count < 1)
                {
                    EnviSetting.Instance.Username.Add(Username);
                }
                else
                {
                    EnviSetting.Instance.Username[0] = Username;
                }
            }

        }

        private string OnOpenFolder(string obj)
        {
            CWIOUtility cwUtil = new CWIOUtility();
            string targetFolder = cwUtil.FolderDialog(EnviSetting.Instance.SourcePath, "");
            if (targetFolder == null || targetFolder == "" || !Directory.Exists(targetFolder))
            {
                // Not valid Directory Path
                targetFolder = "";
            }
            return targetFolder;
        }

        private string _source;
        public string Source { get { return _source; } set { EnviSetting.Instance.SourcePath = value; SetProperty(ref _source, value); } }

        private string _bk;
        public string Backup { get { return _bk; } set { EnviSetting.Instance.BackupPath = value; SetProperty(ref _bk, value); } }

        private bool _EnableRange;
        public bool EnableRange { get { return EnviSetting.Instance.FindInRange; } set { EnviSetting.Instance.FindInRange = value; SetProperty(ref _EnableRange, value); } }

        private bool _EnableDuration;
        public bool EnableDuration { get { return EnviSetting.Instance.EnableHours; } set { EnviSetting.Instance.EnableHours = value; SetProperty(ref _EnableDuration, value); } }

        private string _Duration;
        public string Duration { get { return EnviSetting.Instance.Hours; } set { EnviSetting.Instance.Hours = value; SetProperty(ref _Duration, value); } }

        private DateTime _fromDate;
        public DateTime FromDate { get { return EnviSetting.Instance.FromDate; } set { EnviSetting.Instance.FromDate = value; SetProperty(ref _fromDate, value); } }

        private DateTime _toDate;
        public DateTime ToDate { get { return EnviSetting.Instance.ToDate; } set { EnviSetting.Instance.ToDate = value; SetProperty(ref _toDate, value); } }

        private string _username;
        public string Username { get { return _username; } set { _username = value; SetProperty(ref _username, value); } }

        public string Password { get { return _password; } }

        private ObservableCollection<TemplateInfo> _templates;
        public ObservableCollection<TemplateInfo> Templates
        {
            get { return EnviSetting.Instance.MasterTemplate; }
            set
            {
                EnviSetting.Instance.MasterTemplate = value;
                SetProperty(ref _templates, value);
            }
        }

        private ObservableCollection<TemplateInfo> CreateGhostData()
        {
            GhostDataUtilities ghostData = new GhostDataUtilities();
            return ghostData.GhostMasterTemplate();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// 
        public SettingUpdateViewModel()
        {
            FolderCommand = new MyICommand<string>(OnCommandFolderClick);
            OnKeyUp = new MyICommand<object>(OnKeyUpRelease);
            TemplateSelect = new MyICommand<TextBlock>(OnTemplateSelectionChange);
            // Ghost data
            // Templates = CreateGhostData();
            CWIOUtility cwUtil = new CWIOUtility();            
            Templates = cwUtil.GetInstalledMaster();
            EnviSetting.Instance.MasterTemplate = Templates;
            if (Directory.Exists(EnviSetting.Instance.SourcePath))
            {
                Source = EnviSetting.Instance.SourcePath;
            }
            if (Directory.Exists(EnviSetting.Instance.BackupPath))
            {
                Backup = EnviSetting.Instance.BackupPath;
            }
        }
    }
}
