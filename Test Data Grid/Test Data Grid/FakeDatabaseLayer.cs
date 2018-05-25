using System.Collections.ObjectModel;

namespace Test_Data_Grid
{
    public class FakeDatabaseLayer
    {
        public static ObservableCollection<ClientInfo> GetPeopleFromDatabase()
        {
            //Simulate database extaction
            // System.Threading.Thread.Sleep(1000);
            return new ObservableCollection<ClientInfo>
            {
                new ClientInfo { Selected=true, IsEnable = true, Name="Jones", Group="Process" },
                new ClientInfo { Selected=true, IsEnable = true, Name="Tracey", Group="Process" },
                new ClientInfo { Selected=false, IsEnable = false,  Name="Hill", Group="Not Process" }
            };
        }
    }
}
