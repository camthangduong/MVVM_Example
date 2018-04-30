using Silent_Update.Models;
using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var _clients = new List<ClientInfo>
            {
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            GroupIdentifier = "yes",
                            Group = "Successfull",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            GroupIdentifier = "no",
                            Group = "Not Successfull",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            GroupIdentifier = "no",
                            Group = "Not Successfull",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            GroupIdentifier = "yes",
                            Group = "Successfull",
                            Analytic = "Should Hide"
                        },
                     new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            GroupIdentifier = "no",
                            Group = "Not Successfull",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            GroupIdentifier = "no",
                            Group = "Not Successfull",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 2",
                            Version = "18.00",
                            GroupIdentifier = "yes",
                            Group = "Successfull",
                            Analytic = "Should Hide"
                        }
            };
            return _clients;
        }
    }
}
