using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Simple_Data_List.Library
{
    /// <summary>
    /// Base view model abstract class, all other view model will be based 
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
