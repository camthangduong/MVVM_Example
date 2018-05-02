using Silent_Update.Utilities;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;

namespace Silent_Update.ViewModels
{
    public class SettingYECViewModel : BindableBase, INotifyPropertyChanged
    {
        /// <summary>
        /// Private commands
        /// </summary>
        public MyICommand<string> OpenFolderCommand { get; private set; }
        public MyICommand<object> OnKeyUp { get; private set; }

        private string _password = "";

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
                } else
                {
                    EnviSetting.Instance.Username[0] = Username;
                }                
            }            
        }

        private void OnOpenFolder(string obj)
        {
            CWIOUtility cwUtil = new CWIOUtility();
            string targetFolder = cwUtil.FolderDialog(EnviSetting.Instance.SourcePath, "");
            if (targetFolder != null && targetFolder != "" && Directory.Exists(targetFolder))
            {
                // Only continue when the folder is valid
                // Set the value to the binding property
                Source = targetFolder;
                EnviSetting.Instance.SourcePath = Source;
            }
        }

        ///
        /// Binding properties
        ///         
        private string _source;
        public string Source { get { return _source; } set { _source = value; SetProperty(ref _source, value); } }

        private bool _LockDownOriginal;
        public bool LockDownOriginal { get { return EnviSetting.Instance.LockDownOriginal; } set { EnviSetting.Instance.LockDownOriginal = value; SetProperty(ref _LockDownOriginal, value); } }

        private bool _EnableRange;
        public bool EnableRange { get { return EnviSetting.Instance.FindInRange; } set { EnviSetting.Instance.FindInRange = value; SetProperty(ref _EnableRange, value); } }

        private bool _EnableDuration;
        public bool EnableDuration { get { return EnviSetting.Instance.EnableHours; } set { EnviSetting.Instance.EnableHours = value; SetProperty(ref _EnableDuration, value); } }

        private string _Duration;
        public string Duration { get { return EnviSetting.Instance.Hours; } set { EnviSetting.Instance.Hours = value; SetProperty(ref _Duration, value); } }

        private bool _OPName;
        public bool OPName { get { return EnviSetting.Instance.OPName; } set { EnviSetting.Instance.OPName = value; SetProperty(ref _OPName, value); } }

        private bool _CLName;
        public bool CLName { get { return EnviSetting.Instance.CLName; } set { EnviSetting.Instance.CLName = value; SetProperty(ref _CLName, value); } }

        private string _NameDateFormat;
        public string NameDateFormat { get { return EnviSetting.Instance.NameDateFormat; } set { EnviSetting.Instance.NameDateFormat = value; SetProperty(ref _NameDateFormat, value); } }

        private string _username;
        public string Username { get { return _username; } set { _username = value; SetProperty(ref _username, value); } }

        private DateTime _fromDate;
        public DateTime FromDate
        {
            get
            {
                if (EnviSetting.Instance.FromDate == null)
                {
                    EnviSetting.Instance.FromDate = new DateTime();
                }
                return EnviSetting.Instance.FromDate;
            }
            set { EnviSetting.Instance.FromDate = value; SetProperty(ref _fromDate, value); }
        }

        private DateTime _toDate;
        public DateTime ToDate
        {
            get
            {
                if (EnviSetting.Instance.ToDate == null)
                {
                    EnviSetting.Instance.ToDate = new DateTime();
                }
                return EnviSetting.Instance.ToDate;
            }
            set { EnviSetting.Instance.ToDate = value; SetProperty(ref _toDate, value); }
        }

        public string Password { get { return _password; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public SettingYECViewModel()
        {
            // Initial command
            OpenFolderCommand = new MyICommand<string>(OnOpenFolder);
            OnKeyUp = new MyICommand<object>(OnKeyUpRelease);
            if (Directory.Exists(EnviSetting.Instance.SourcePath))
            {
                Source = EnviSetting.Instance.SourcePath;
            }
        }
    }
}
