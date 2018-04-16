using Silent_Update.DAL;
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
        public bool YEC {
            get { return EnviSetting.Instance.RunYEC; }
            set
            {
                if (value == true)
                {
                    EnviSetting.Instance.RunYEC = true;
                    EnviSetting.Instance.RunUpdate = false;
                    EnviSetting.Instance.RunBoth = false;
                }
                RaisePropertyChanged("YEC");
            }
        }
        public bool UPDATE {
            get { return EnviSetting.Instance.RunUpdate; }
            set
            {
                if (value == true)
                {
                    EnviSetting.Instance.RunUpdate = true;
                    EnviSetting.Instance.RunYEC = false;
                    EnviSetting.Instance.RunBoth = false;
                }
                RaisePropertyChanged("UPDATE");
            }
        }
        public bool BOTH {
            get { return EnviSetting.Instance.RunBoth; }
            set
            {
                if (value == true)
                {
                    EnviSetting.Instance.RunBoth = true;
                    EnviSetting.Instance.RunUpdate = false;
                    EnviSetting.Instance.RunYEC = false;
                }
                RaisePropertyChanged("BOTH");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public HomePageViewModel() { }
    }
}
