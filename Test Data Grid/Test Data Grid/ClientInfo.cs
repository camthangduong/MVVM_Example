﻿using System.ComponentModel;

namespace Test_Data_Grid
{
    public class ClientInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; RaisePropertyChanged("Selected"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged("Name"); }
        }

        private string _group;
        public string Group
        {
            get { return _group; }
            set { _group = value; RaisePropertyChanged("Group"); }
        }

        public bool IsEnable { get; set; }
    }
}
