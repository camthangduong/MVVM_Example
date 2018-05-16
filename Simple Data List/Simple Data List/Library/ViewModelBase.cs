using System.ComponentModel;

namespace Simple_Data_List.Library
{
    /// <summary>
    /// Base view model abstract class, all other view model will be based 
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string prop)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(prop));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
