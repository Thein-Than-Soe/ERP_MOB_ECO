using SQLite;
namespace CS.ERP_MOB.DB
{
    public class DbUser
    {
        [PrimaryKey, AutoIncrement]
        public int Ask { get; set; } = 0;
        public string UserAsk { get; set; } = "2";
        public string UserTS { get; set; } = "";
        public string UserUD { get; set; } = "";
        public string UserTD { get; set; } = "";
        public string UserID { get; set; } = "guest";
        public string UserDescription { get; set; } = "";
        public string UserPassword { get; set; } = "Ova@123!@#";
        public string UserEmail { get; set; } = "";
        public string UserPhone { get; set; } = "";
        public string ProfilePicture { get; set; } = "";
        public string SubscriberAsk { get; set; } = "";
        public string AccessAsk { get; set; } = "0";
        public string UserTypeAsk { get; set; } = "";
        public int UserStatus { get; set; } = 1;
        public string UserSequence { get; set; } = "";
        public string UserRemark { get; set; } = "";
        public string Remove { get; set; } = "";
    }
}





