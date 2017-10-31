using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVC_Binding.Utilities
{
    public class BindableBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Interface implementation
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            // Check for current set member and the new value
            // If they are the same, do nothing
            if (object.Equals(member, val)) return;

            member = val;
            // Invoke the property change event
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
