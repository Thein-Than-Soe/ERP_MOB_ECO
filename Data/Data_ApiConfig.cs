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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://sysdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "sysdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://posdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "posdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://hcmdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "hcmdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://attdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "attdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://paydevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "paydevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://crmdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "crmdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://accdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "accdevapi.kumudr.com/",
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
                UploadURL = "http://upddevsrv.kumudr.com",
                APIURL = "http://wmsdevapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
                APIServer = "wmsdevapi.kumudr.com/",
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
