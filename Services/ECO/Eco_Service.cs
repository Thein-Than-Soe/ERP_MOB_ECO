using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CS.ERP_MOB.DB;

namespace CS.ERP_MOB.Services.ECO
{
    public class Eco_Service
    {
        public Eco_Service()
        {
            //mApiConfig.APIProtocol = "https://";
        }
        public static ApiConfig mApiConfig = new ApiConfig
        {
            Ask = 1,
            ProductCode = "ECO",
            UploadURL = "https://updqasrv.kumudr.com",
            APIURL = "https://ecoqaapi.kumudr.com/Service.svc",
            APIProtocol = "https://",
            APIServer = "ecoqaapi.kumudr.com/",
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
        public async static Task<string> ApiCall(string argRequest, string argApiName)
        {
            try
            {
                StringContent content = new StringContent(argRequest, Encoding.UTF8, mApiConfig.ApiContentType);
                using (HttpClient client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(mAPIProtocol + mAPIServer + mAPIPort + mAPIServiceName);
                    client.BaseAddress = new Uri(mApiConfig.APIProtocol + mApiConfig.APIServer + mApiConfig.APIPort + mApiConfig.APIServiceName);
                    HttpResponseMessage response = await client.PostAsync(argApiName, content);

                    if (response.IsSuccessStatusCode)
                    {
                        mResponse = await response.Content.ReadAsStringAsync();
                        //Debug.WriteLine(responseData);
                    }
                    //else{}
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
