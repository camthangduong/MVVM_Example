using Silent_Update.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Silent_Update.DAL
{
    public class GhostDataUtilities
    {
        public List<ClientInfo> GhostPreStatusData()
        {
            var _clients = new List<ClientInfo>
            {
                    new ClientInfo (@"C:\TEST\Client1.ac")
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            Group = "To Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client2.ac")
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client3.ac")
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client4.ac")
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            Group = "To Be Process",
                            Analysis = "Should Hide"
                        },
                     new ClientInfo (@"C:\TEST\Client5.ac")
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client6.ac")
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client7.ac")
                        {
                            Selected = true,
                            Name = "Client 2",
                            Version = "18.00",
                            Group = "To Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client8.ac")
                        {
                            Selected = true,
                            Name = "Client to be added",
                            Version = "18.00",
                            Group = "To Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client9.ac")
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\TEST\Client10.ac")
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            Group = "Not Be Process",
                            Analysis = "Should Hide"
                        }

            };
            return _clients;
        }

        public List<ClientInfo> GhostPostStatusData()
        {
            var _clients = new List<ClientInfo>
            {
                    new ClientInfo (@"C:\Test\Client1.ac")
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            Group = "Successfull",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\Test\Client2.ac")
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            Group = "Not Successfull",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\Test\Client3.ac")
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            Group = "Not Successfull",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\Test\Client4.ac")
                        {
                            Selected = true,
                            Name = "Client 1 to test the file with the longest name",
                            Version = "19.00",
                            Group = "Successfull",
                            Analysis = "Should Hide"
                        },
                     new ClientInfo (@"C:\Test\Client5.ac")
                        {
                            Selected = false,
                            Name = "Client 3",
                            Version = "17.00",
                            Group = "Not Successfull",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\Test\Client6.ac")
                        {
                            Selected = false,
                            Name = "Client 4",
                            Version = "16.00",
                            Group = "Not Successfull",
                            Analysis = "Should Hide"
                        },
                    new ClientInfo (@"C:\Test\Client7.ac")
                        {
                            Selected = true,
                            Name = "Client 2",
                            Version = "18.00",
                            Group = "Successfull",
                            Analysis = "Should Hide"
                        }
            };
            return _clients;
        }

        public ObservableCollection<TemplateInfo> GhostMasterTemplate()
        {
            ObservableCollection<TemplateInfo> TemplateTmp = new ObservableCollection<TemplateInfo>
            {
                new TemplateInfo(0, true, "Audit Internaltional", "19.00.289", "17.00.352"),
                new TemplateInfo(1, false, "Audit US", "19.00.289", "17.00.352"),
                new TemplateInfo(2, true, "Audit CWI", "18.00.285", "16.00.290")
            };
            return TemplateTmp;
        }
    }
}
