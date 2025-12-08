using System;
using System.Collections.Generic;
using CS.ERP_MOB.DB;

namespace CS.ERP_MOB.Data
{
    public class Data_ApiConfig
    {
        //public static Dictionary<string, ContentView> RouteModels { get; private set; }

        public static List<ApiConfig> mApiConfig_Lst { get; private set; }
        static Data_ApiConfig()
        {
            mApiConfig_Lst = new List<ApiConfig>();
            //SYS
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "SYS",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://sysqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "sysqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 1
            });
            //POS
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "POS",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://posqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "posqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 2
            });
            //HCM
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "HCM",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://hcmqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "hcmqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 3
            });
            //ATT
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "ATT",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://attqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "attqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 4
            });
            //PAY
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "PAY",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://payqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "payqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 5
            });
            //CRM
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "CRM",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://crmqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "crmqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 6
            });
            //ACC
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "ACC",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://accqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "accqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 7
            });
            //WMS
            mApiConfig_Lst.Add(new ApiConfig
            {
                Ask = 0,
                ProductCode = "WMS",
                UploadURL = "https://updqasrv.kumudr.com",
                APIURL = "https://wmsqaapi.kumudr.com/Service.svc",
                APIProtocol = "https://",
                APIServer = "wmsqaapi.kumudr.com/",
                APIPort = "",
                APIServiceName = "Service.svc/",
                ApiContentType = "application/json",
                ApiAcceptType = "application/json",
                ApiKey = "",
                PublicKey = "",
                SecreteKey = "",
                User = "",
                Password = "",
                Sequence = 8
            });
        }
    }
}
