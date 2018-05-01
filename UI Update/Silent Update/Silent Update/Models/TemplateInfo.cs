using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Silent_Update.Models
{
    public class TemplateInfo : INotifyPropertyChanged
    {
        private bool seleted;

        public TemplateInfo(int index, bool selected, string name, string version, string minVer)
        {
            Index = index;
            Selected = selected;
            Name = name;
            Version = version;
            MinVersion = minVer;
        }

        public int Index { get; private set; }
        public bool Selected
        {
            get { return seleted; }
            set
            {
                this.seleted = value;
                RaiseChangedEvent("Selected");
            }
        }
        public string Name { get; set; }
        public string Version { get; set; }
        public string MinVersion { get; set; }

        public long VerInNum
        {
            get
            {
                // Version as int
                Regex digitsOnly = new Regex(@"[^\d]");
                return Int64.Parse(digitsOnly.Replace(this.Version, ""));
            }
        }

        public long MinVerInNum
        {
            get
            {
                // Version as int
                Regex digitsOnly = new Regex(@"[^\d]");
                return Int64.Parse(digitsOnly.Replace(this.MinVersion, ""));
            }
        }

        /// <summary>
        /// Implementation for interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaiseChangedEvent(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
