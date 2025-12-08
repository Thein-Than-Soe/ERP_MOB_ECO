using System;
using SQLite;

namespace CS.ERP_MOB.DB
{
    public class ApiConfig
    {
        [PrimaryKey, AutoIncrement]
        public int Ask { get; set; }
        public string ProductCode { get; set; } = "SYS";
        public string UploadURL { get; set; } = "https://updqasrv.kumudr.com";
        public string APIURL { get; set; } = "https://sysqaapi.kumudr.com/Service.svc";
        public string APIProtocol { get; set; } = "https://";//http or https
        public string APIServer { get; set; } = "sysqaapi.kumudr.com/"; //IP
        public string APIPort { get; set; } = "";
        public string APIServiceName { get; set; } = "Service.svc/";
        public string ApiContentType { get; set; } = "application/json";
        public string ApiAcceptType { get; set; } = "application/json";
        public string ApiKey { get; set; } = "";
        public string PublicKey { get; set; } = "";
        public string SecreteKey { get; set; } = "";
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
        public int Sequence { get; set; } = 0;
    }
}
