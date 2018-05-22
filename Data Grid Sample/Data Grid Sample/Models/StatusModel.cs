using Simple_Data_List.Library;

namespace Data_Grid_Sample.Models
{
    public class StatusModel
    {
        public CollectionBinding<ClientInfo> ListFile { get; set; }

        public void CreateGhostData()
        {
            GhostDataUtilities ghostUtil = new GhostDataUtilities();
            ListFile = ghostUtil.GhostPreStatusData();
        }
    }
}
