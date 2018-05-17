using Simple_Data_List.Library;

namespace Simple_Data_List.Models
{
    public class GhostDataUtilities
    {
        public CollectionBinding<ClientInfo> GhostPreStatusData()
        {
            CollectionBinding<ClientInfo> clientList = new CollectionBinding<ClientInfo>
            {
                new ClientInfo
                {
                    Selected = true,
                    Name = "Client File 1",
                    Version = "19.00",
                    Group = "Processed"
                },
                new ClientInfo
                {
                    Selected = true,
                    Name = "Client File 2",
                    Version = "20.00",
                    Group = "Processed"
                },
                new ClientInfo
                {
                    Selected = false,
                    Name = "Client File 3",
                    Version = "Unknown",
                    Group = "Not Processed"
                },
                new ClientInfo
                {
                    Selected = false,
                    Name = "Client File 4",
                    Version = "16.00",
                    Group = "Not Processed"
                }
            };
            return clientList;
        }
    }
}
