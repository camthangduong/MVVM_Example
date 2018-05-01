using Silent_Update.DAL;
using Silent_Update.Models;
using Silent_Update.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Silent_Update.ViewModels
{
    public class PreStatusViewModel : BindableBase
    {
        /// <summary>
        /// Event handler
        /// </summary>
        public MyICommand<object> GroupHeaderEventHandler { get; private set; }

        /// <summary>
        /// Public data for binding
        /// </summary>
        public ICollectionView Clients { get; set; }
        public ICollectionView GroupClients { get; set; }


        public PreStatusViewModel()
        {
            // Assing the event
            GroupHeaderEventHandler = new MyICommand<object>(GroupHeaderEvent);
            var _clients = GetGhostData();
            // Set the group view
            GroupClients = new ListCollectionView(_clients);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        private void GroupHeaderEvent(object sender)
        {
            switch ("TEST")
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// Create a ghost data to test
        /// </summary>
        /// <returns></returns>
        private List<ClientInfo> GetGhostData()
        {
            GhostDataUtilities _dataUtilities = new GhostDataUtilities();
            return _dataUtilities.GhostPreStatusData();
        }
    }
}
