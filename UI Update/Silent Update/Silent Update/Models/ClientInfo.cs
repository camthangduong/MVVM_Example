using System.ComponentModel;

namespace Silent_Update.Models
{
    public class ClientInfo : INotifyPropertyChanged
    {
        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                NotifyPropertyChanged("Selected");
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                NotifyPropertyChanged("Vesion");
            }
        }

        private string _groupIdentifier;
        public string GroupIdentifier
        {
            get { return _groupIdentifier; }
            set
            {
                _groupIdentifier = value;
                NotifyPropertyChanged("GroupIdentifier");
            }
        }

        private string _group;
        public string Group
        {
            get { return _group; }
            set
            {
                _group = value;
                NotifyPropertyChanged("Group");
            }
        }

        private string _analytic;
        public string Analytic
        {
            get { return _analytic; }
            set
            {
                _analytic = value;
                NotifyPropertyChanged("Analytic");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
