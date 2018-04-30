using Silent_Update.Models;
using Silent_Update.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
            var _clients = new List<ClientInfo>
            {
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            GroupIdentifier = "yes",
                            Group = "To Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            GroupIdentifier = "yes",
                            Group = "To Be Process",
                            Analytic = "Should Hide"
                        },
                     new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client 2",
                            Version = "18.00",
                            GroupIdentifier = "yes",
                            Group = "To Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = true,
                            Name = "Client to be added",
                            Version = "18.00",
                            GroupIdentifier = "yes",
                            Group = "To Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        },
                    new ClientInfo
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            GroupIdentifier = "no",
                            Group = "Not Be Process",
                            Analytic = "Should Hide"
                        }

            };
            return _clients;
        }
    }
}
