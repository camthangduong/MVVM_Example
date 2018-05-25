using System.Collections.ObjectModel;

namespace Test_Data_Grid
{
    public class FakeDatabaseLayer
    {
        public static ObservableCollection<ClientInfo> GetPeopleFromDatabase()
        {
            //Simulate database extaction
            System.Threading.Thread.Sleep(3000);
            return new ObservableCollection<ClientInfo>
            {
                new ClientInfo { Selected=true, Name="Jones", Group="Good" },
                new ClientInfo { Selected=true, Name="Tracey", Group="Good" },
                new ClientInfo { Selected=false, Name="Hill", Group="Bad" }
            };
        }
    }
}
