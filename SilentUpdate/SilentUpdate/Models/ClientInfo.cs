namespace SilentUpdate.Models
{
    public class ClientInfo
    {
        public ClientInfo()
        {

        }

        public string Group { get; set; }
        public bool Selected { get; set; }
        public string Name { get; set; }
        public string Versions { get; set; }
        public string WPVersion { get; set; }
        public string LastModified { get; set; }
        public string Analytics { get; set; }
    }
}
