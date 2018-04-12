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
        /// <summary>
        /// Interface implemetation
        /// </summary>
        public event PropertyChangedEventHandler HomePropertyChanged = delegate { };        

        private void RaisePropertyChanged(string v)
        {
            HomePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        /// <summary>
        /// Public properties for bind to the view
        /// </summary>
        public bool YEC { get; set; }
        public bool UPDATE { get; set; }
        public bool BOTH { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public HomePageViewModel() { }
    }
}
