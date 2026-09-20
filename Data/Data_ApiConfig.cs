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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://sysqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://posqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://hcmqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://attqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://payqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://crmqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://accqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
                UploadURL = "http://updqasrv.kumudr.com",
                APIURL = "http://wmsqaapi.kumudr.com/Service.svc",
                APIProtocol = "http://",
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
