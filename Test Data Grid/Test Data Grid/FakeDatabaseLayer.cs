using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Data_Grid
{
    public class FakeDatabaseLayer
    {
        public static ObservableCollection<ClientInfo> GetPeopleFromDatabase()
        {
            //Simulate database extaction
            //For example from ADO DataSets or EF
            return new ObservableCollection<ClientInfo>
            {
                new ClientInfo { Selected=true, Name="Jones", Group="Good" },
                new ClientInfo { Selected=true, Name="Tracey", Group="Good" },
                new ClientInfo { Selected=false, Name="Hill", Group="Bad" }
            };
        }        
    }
}
