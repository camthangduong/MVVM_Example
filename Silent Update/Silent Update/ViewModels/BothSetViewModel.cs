﻿using Silent_Update.DAL;
using Silent_Update.Utilities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Silent_Update.ViewModels
{
    public class BothSetViewModel : BindableBase
    {
        /// <summary>
        /// Command
        /// </summary>
        public MyICommand<string> FolderCommand { get; private set; }
        public MyICommand<PasswordBox> OnKeyUp { get; private set; }
        public MyICommand<TextBlock> TemplateSelect { get; private set; }
        /// <summary>
        /// Private string for password field
        /// </summary>
        private string _password = "";

        /// <summary>
        /// Properties for Binding
        /// </summary>
        private string _source;
        public string Source { get { return _source; } set { EnviSetting.Instance.SourcePath = value; SetProperty(ref _source, value); } }

        private bool _LockDownOriginal;
        public bool LockDownOriginal { get { return EnviSetting.Instance.LockDownOriginal; } set { EnviSetting.Instance.LockDownOriginal = value; SetProperty(ref _LockDownOriginal, value); } }

        private bool _OPName;
        public bool OPName { get { return EnviSetting.Instance.OPName; } set { EnviSetting.Instance.OPName = value; SetProperty(ref _OPName, value); } }

        private bool _CLName;
        public bool CLName { get { return EnviSetting.Instance.CLName; } set { EnviSetting.Instance.CLName = value; SetProperty(ref _CLName, value); } }

        private string _NameDateFormat;
        public string NameDateFormat { get { return EnviSetting.Instance.NameDateFormat; } set { EnviSetting.Instance.NameDateFormat = value; SetProperty(ref _NameDateFormat, value); } }

        private bool _EnableRange;
        public bool EnableRange { get { return EnviSetting.Instance.FindInRange; } set { EnviSetting.Instance.FindInRange = value; SetProperty(ref _EnableRange, value); } }

        private bool _EnableDuration;
        public bool EnableDuration { get { return EnviSetting.Instance.EnableHours; } set { EnviSetting.Instance.EnableHours = value; SetProperty(ref _EnableDuration, value); } }

        private string _Duration;
        public string Duration { get { return EnviSetting.Instance.Hours; } set { EnviSetting.Instance.Hours = value; SetProperty(ref _Duration, value); } }

        private DateTime _fromDate;
        public DateTime FromDate {
            get {
                if (EnviSetting.Instance.FromDate == null)
                {
                    EnviSetting.Instance.FromDate = new DateTime();
                }
                return EnviSetting.Instance.FromDate;
            }
            set { EnviSetting.Instance.FromDate = value; SetProperty(ref _fromDate, value); }
        }

        private DateTime _toDate;
        public DateTime ToDate {
            get {
                if (EnviSetting.Instance.ToDate == null)
                {
                    EnviSetting.Instance.ToDate = new DateTime();
                }
                return EnviSetting.Instance.ToDate;
            }
            set { EnviSetting.Instance.ToDate = value; SetProperty(ref _toDate, value); }
        }

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
            ObservableCollection<TemplateInfo> TemplateTmp = new ObservableCollection<TemplateInfo>
            {
                new TemplateInfo(0, true, "Audit Internaltional", "20.00.283", "18.00.285"),
                new TemplateInfo(1, false, "Audit US", "19.00.289", "17.00.352"),
                new TemplateInfo(2, true, "Audit CWI", "18.00.285", "16.00.290")
            };
            return TemplateTmp;
        }


        /// <summary>
        /// Private method for the command
        /// </summary>
        /// <param name="commandParams"></param>
        private void OnCommandFolderClick(string commandParams)
        {
            string targetPath = OnOpenFolder(commandParams);
            Source = targetPath;
            EnviSetting.Instance.SourcePath = targetPath;
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

        private void OnKeyUpRelease(object obj)
        {
            PasswordBox pw = obj as PasswordBox;
            _password = pw.Password.ToString();
        }

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

        /// <summary>
        /// Constructor
        /// </summary>
        public BothSetViewModel() {
            FolderCommand = new MyICommand<string>(OnCommandFolderClick);
            OnKeyUp = new MyICommand<PasswordBox>(OnKeyUpRelease);
            TemplateSelect = new MyICommand<TextBlock>(OnTemplateSelectionChange);

            if (EnviSetting.Instance.FromDate == null || EnviSetting.Instance.FromDate.Date == null || EnviSetting.Instance.FromDate.Date.DayOfYear == 1)
            {
                EnviSetting.Instance.FromDate = DateTime.Now;
            }

            if (EnviSetting.Instance.ToDate == null || EnviSetting.Instance.ToDate.Date == null || EnviSetting.Instance.ToDate.Date.DayOfYear == 1)
            {
                EnviSetting.Instance.ToDate = DateTime.Now;
            }
            // Ghost data
            Templates = CreateGhostData();
            CWIOUtility cwUtil = new CWIOUtility();
            // Templates = cwUtil.GetInstalledMaster();
        }
    }
}
