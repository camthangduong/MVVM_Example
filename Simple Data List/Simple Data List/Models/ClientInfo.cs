using System.ComponentModel;

namespace Simple_Data_List.Models
{
    public class ClientInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #region Private properties
        private bool selected;
        private string name;
        private string version;
        private string group;
        #endregion

        #region Public properties
        public int Index { get; set; }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; RaisePropertyChanged("Selected"); }
        }
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }
        public string Version
        {
            get { return version; }
            set { version = value; RaisePropertyChanged("Version"); }
        }
        public string Group
        {
            get { return group; }
            set { group = value; RaisePropertyChanged("Group"); }
        }
        #endregion
    }
}
