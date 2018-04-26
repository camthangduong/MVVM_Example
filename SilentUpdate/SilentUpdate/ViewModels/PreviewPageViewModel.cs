using SilentUpdate.Models;
using SilentUpdate.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace SilentUpdate.ViewModels
{
    public class PreviewPageViewModel : BindableBase
    {
        private MainWindowViewModel mainView;

        public ICollectionView GroupedClients { get; private set; }

        public PreviewPageViewModel()
        {

        }

        public PreviewPageViewModel(MainWindowViewModel _mainView)
        {
            mainView = _mainView;
            var _customers = new List<ClientInfo>
                                 {
                                     new ClientInfo
                                         {
                                             Group = "TBD",
                                             Selected = true,
                                             Name = "Moser 1",
                                             Versions = "19.00.310 Rev A",
                                             WPVersion = "2017.00",
                                             LastModified = DateTime.Now.ToShortDateString(),
                                             Analytics = ""
                                         },
                                     new ClientInfo
                                         {
                                            Group = "TBD",
                                            Selected = true,
                                             Name = "Moser 2",
                                             Versions = "19.00.310 Rev A",
                                             WPVersion = "2017.00",
                                             LastModified = DateTime.Now.ToShortDateString(),
                                             Analytics = ""
                                         },
                                     new ClientInfo
                                         {
                                            Group = "TBD",
                                            Selected = true,
                                             Name = "Moser 3",
                                             Versions = "19.00.310 Rev A",
                                             WPVersion = "2017.00",
                                             LastModified = DateTime.Now.ToShortDateString(),
                                             Analytics = ""
                                         },
                                     new ClientInfo
                                         {
                                            Group = "TBD",
                                            Selected = true,
                                             Name = "Moser 4",
                                             Versions = "19.00.310 Rev A",
                                             WPVersion = "2017.00",
                                             LastModified = DateTime.Now.ToShortDateString(),
                                             Analytics = ""
                                         }
                                 };
            GroupedClients = new ListCollectionView(_customers);
            GroupedClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }
    }
}
