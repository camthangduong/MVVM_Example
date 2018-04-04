using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Silent_Update.Utilities
{
    public class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Interface implemetation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SetProperty<T>(ref T memeber, T val, [CallerMemberName] string propertyName = null)
        {
            memeber = val;
            // Invoke the property change event
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //}
            /**
             * Simplify of the statement above can be used
             * */
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
