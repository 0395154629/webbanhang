namespace PNPStore.ModelView.Account
{

    public class JsonLogin
    {

        public string token { get; set; }
        public DateTime expiration { get; set; }
        public string id { get; set; }
        public string phone { get; set; }
        public string role { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string TimeInWorking { get; set; }
        public int countDay { get; set; }
        public int idDepartment { get; set; }
    }
}
