using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CHARS.HR.COMMON.BOL;
using CS.ERP.BOL.API;
using CS.ERP_MOB.DB;

namespace CS.ERP_MOB.Services.POS
{
    public class Pos_Service
    {
        public static ApiConfig mApiConfig = new ApiConfig
                                            {
                                                Ask = 1,
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
                                                Sequence = 1
                                            };
        public static string mResponse = "";
        public Pos_Service()
        {
           // getService();
        }
       
        public async static Task<string> ApiCall(string argRequest, string argApiName)
        {
            try
            {
                StringContent content = new StringContent(argRequest, Encoding.UTF8, mApiConfig.ApiContentType);
                using (HttpClient client = new HttpClient())
                {

                    if (mApiConfig.ProductCode != "")
                    {
                        //client.BaseAddress = new Uri(mAPIProtocol + mAPIServer + mAPIPort + mAPIServiceName);
                        client.BaseAddress = new Uri(mApiConfig.APIProtocol + mApiConfig.APIServer + mApiConfig.APIPort + mApiConfig.APIServiceName);
                        HttpResponseMessage response = await client.PostAsync(argApiName, content);

                        if (response.IsSuccessStatusCode)
                        {
                            mResponse = await response.Content.ReadAsStringAsync();
                            //Debug.WriteLine(responseData);
                        }
                        //else{}}
                    }
                    else
                    {

                    }
                }
                return mResponse;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
      
        public static string getUploadURL()
        {
            try
            {
                return mApiConfig.UploadURL;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}