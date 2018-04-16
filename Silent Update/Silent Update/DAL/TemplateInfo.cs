using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Silent_Update.DAL
{
    public class TemplateInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// Private properties
        /// </summary>
        private string name, version, minVersion;
        private bool seleted;
        private int index;

        /// <summary>
        /// Implementation for interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaiseChangedEvent(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        /// <summary>
        /// Public properties
        /// </summary>
        public int Index { get { return this.index; } set { this.index = value; RaiseChangedEvent("Index"); } }
        public string Name { get { return this.name; } set { this.name = value; RaiseChangedEvent("Name"); } }
        public string Version { get { return this.version; } set { this.version = value; RaiseChangedEvent("Version"); } }
        public string MinVersion { get { return this.minVersion; } set { this.minVersion = value; RaiseChangedEvent("MinVersion"); } }
        public bool Selected { get { return this.seleted; } set { this.seleted = value; RaiseChangedEvent("Selected"); } }

        /// <summary>
        /// Public methods
        /// </summary>
        public long VerInNum
        {
            get
            {
                // Version as int
                Regex digitsOnly = new Regex(@"[^\d]");
                return Int64.Parse(digitsOnly.Replace(this.version, ""));
            }
        }

        public long MinVerInNum
        {
            get
            {
                // Version as int
                Regex digitsOnly = new Regex(@"[^\d]");
                return Int64.Parse(digitsOnly.Replace(this.minVersion, ""));
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="selected"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="minVersion"></param>
        public TemplateInfo(int idx, bool selected, string name, string version, string minVersion)
        {
            Index = idx;
            Name = name;
            Version = version;
            MinVersion = minVersion;
            Selected = selected;
        }                
    }
}
