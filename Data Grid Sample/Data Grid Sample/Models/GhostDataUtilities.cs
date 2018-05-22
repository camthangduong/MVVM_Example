using Simple_Data_List.Library;

namespace Data_Grid_Sample.Models
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
                    Index = 0,
                    Name = "Client File 1",
                    Version = "19.00",
                    Group = "Processed"
                },
                new ClientInfo
                {
                    Selected = true,
                    Index = 1,
                    Name = "Client File 2",
                    Version = "20.00",
                    Group = "Processed"
                },
                new ClientInfo
                {
                    Selected = false,
                    Index = 2,
                    Name = "Client File 3",
                    Version = "Unknown",
                    Group = "Not Processed"
                },
                new ClientInfo
                {
                    Selected = false,
                    Index = 3,
                    Name = "Client File 4",
                    Version = "16.00",
                    Group = "Not Processed"
                }
            };
            // Delay the time
            double i = 1;
            while (i < 20000000)
            {
                i++;
            }
            return clientList;
        }
    }
}
