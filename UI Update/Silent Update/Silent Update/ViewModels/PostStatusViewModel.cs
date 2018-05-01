using Silent_Update.DAL;
using Silent_Update.Models;
using Silent_Update.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Silent_Update.ViewModels
{
    public class PostStatusViewModel : BindableBase
    {
        /// <summary>
        /// Public data for binding
        /// </summary>
        public ICollectionView Clients { get; set; }
        public ICollectionView GroupClients { get; set; }

        public PostStatusViewModel()
        {
            var _clients = GetGhostData();
            // Set the group view
            GroupClients = new ListCollectionView(_clients);
            GroupClients.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }

        /// <summary>
        /// Create a ghost data to test
        /// </summary>
        /// <returns></returns>
        private List<ClientInfo> GetGhostData()
        {
            GhostDataUtilities _dataUtilities = new GhostDataUtilities();
            return _dataUtilities.GhostPostStatusData();
        }
    }
}
