using CS.ERP_MOB.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.ERP_MOB.Services.NTF
{
    public class Cht_Service
    {
        public Cht_Service()
        {
            //mApiConfig.APIProtocol = "https://";
        }
        public static ApiConfig mApiConfig = new ApiConfig();
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
