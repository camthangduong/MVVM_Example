using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silent_Update.ViewModels
{
    public class HomePageViewModel : BindableBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler HomePropertyChanged = delegate { };

        public HomePageViewModel() { }

        private void RaisePropertyChanged(string v)
        {
            HomePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}
