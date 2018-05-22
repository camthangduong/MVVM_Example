using System.ComponentModel;

namespace Data_Grid_Sample.Models
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
        private int index;
        #endregion

        #region Public properties
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged("Index");
            }
        }

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
