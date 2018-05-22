using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Simple_Data_List.Library
{
    /// <summary>
    /// The CollectionBinding class
    /// Inherrit from ObservableCollection to store the data list and binding it to DataGrid
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CollectionBinding<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        ///
        /// Implementation for interface of property change
        /// 
        public event PropertyChangedEventHandler ItemPropertyChanged = delegate { };

        ///
        /// Constructor
        /// 
        public CollectionBinding() : base()
        {
            CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ObservableCollectionChanged);
        }

        /// <summary>
        /// Observable collection changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObservableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // For new items
            if (e.NewItems != null)
            {
                foreach (object item in e.NewItems)
                {
                    // Add the property change event to each item changed
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(RaiseItemPropertyChanged);
                }
            }
            // For old items
            if (e.OldItems != null)
            {
                foreach (object item in e.OldItems)
                {
                    // Add the property change event to each item changed
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(RaiseItemPropertyChanged);
                }
            }

        }

        /// <summary>
        /// Property change event for each item in the collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RaiseItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChangedEventArgs arg = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            try
            {
                OnCollectionChanged(arg);
                ItemPropertyChanged?.Invoke(sender, e);
            }
            catch (Exception) { }
        }
    }
}
