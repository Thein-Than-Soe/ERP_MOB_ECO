using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using CS.ERP.PL.SYS.DAT;
using SQLite;

namespace CS.ERP_MOB.DB
{
    public class Database
    {
        readonly SQLiteAsyncConnection mDatabase;
        public Database()
        {
            try
            {
                string DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "kmd.db");

                Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                //Stream embeddedDatabaseStream = assembly.GetManifestResourceStream("CS.ERP_MOB.kmd.db");
                if (!File.Exists(DatabasePath))
                {
                    //FileStream fileStreamToWrite = File.Create(DatabasePath);
                    //embeddedDatabaseStream.Seek(0, SeekOrigin.Begin);
                    //embeddedDatabaseStream.CopyTo(fileStreamToWrite);
                    //fileStreamToWrite.Close();

                    mDatabase = new SQLiteAsyncConnection(DatabasePath);
                    mDatabase.CreateTableAsync<DbUser>().Wait();
                    mDatabase.CreateTableAsync<ApiConfig>().Wait();

                    #region"User"
                    //Guest
                    DbUser l_DbUser = new DbUser();
                    saveUserAsync(l_DbUser);
                    #endregion
                    #region"Web Service"
                    //SYS
                    saveApiConfigAsync(new ApiConfig());
                    //POS
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "POS",
                        UploadURL = "http://updqasrv.kumudr.com",
                        APIURL = "https://posqaapi.kumudr.com/Service.svc",
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
                        Sequence = 1
                    });
                    //HCM
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "HCM",
                        UploadURL = "http://updqasrv.kumudr.com",
                        APIURL = "https://sysqaapi.kumudr.com/Service.svc",
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
                    //ATT
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "ATT",
                        UploadURL = "http://updqasrv.kumudr.com",
                        APIURL = "https://sysqaapi.kumudr.com/Service.svc",
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
                    //PAY
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "PAY",
                        UploadURL = "https://updqasrv.kumudr.com",
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
                    //CRM
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "CRM",
                        UploadURL = "https://updqasrv.kumudr.com",
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
                    //ACC
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        ProductCode = "ACC",
                        UploadURL = "https://updqasrv.kumudr.com",
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
                    //WMS
                    saveApiConfigAsync(new ApiConfig
                    {
                        Ask = 1,
                        UploadURL = "https://updqasrv.kumudr.com",
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
                    #endregion
                }else
                {
                    mDatabase = new SQLiteAsyncConnection(DatabasePath);
                }
                //mDatabase = new SQLiteAsyncConnection(DatabasePath);
                //mDatabase.CreateTableAsync<DbUser>().Wait();
                //mDatabase.CreateTableAsync<ApiConfig>().Wait();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #region "RES_USER"
        public Task<int> saveUserAsync(DbUser argDbUser)
        {
            try
            {
                if (argDbUser.Ask == 0)
                {
                    return mDatabase.InsertAsync(argDbUser);
                }
                else
                {
                    return mDatabase.UpdateAsync(argDbUser);
                }
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public Task<List<DbUser>> getUserAsync()
        {
            try
            {
                return mDatabase.Table<DbUser>().OrderBy(x => x.UserSequence).ToListAsync();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public Task<DbUser> getUserAsync(int argStatus)
        {
            try
            {
                return mDatabase.Table<DbUser>().Where(x => x.UserStatus.Equals(argStatus)).FirstAsync();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        public Task<int> deleteUserAsync(DbUser argDbUser)
        {
            try
            {
                return mDatabase.DeleteAsync(argDbUser);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
      


        public Task<List<RES_USER>> getUserAsync(RES_USER argRES_USER)
        {
            try
            {
                if (argRES_USER.UserRemark != "")
                {
                    return mDatabase.Table<RES_USER>().Where(x =>
                    x.UserID.ToLower().Contains(argRES_USER.UserRemark.ToLower())
                    || x.UserEmail.ToLower().Contains(argRES_USER.UserRemark.ToLower())
                    || x.UserDescription.ToLower().Contains(argRES_USER.UserRemark.ToLower())).OrderBy(x => x.UserSequence).ToListAsync();
                }
                else
                {
                    return mDatabase.Table<RES_USER>().OrderBy(x => x.UserSequence).ToListAsync();
                }

            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        #endregion

        #region "ApiConfig"
        public Task<int> saveApiConfigAsync(ApiConfig argApiConfig)
        {
            try
            {
                if (argApiConfig.Ask == 0)
                {
                    return mDatabase.InsertAsync(argApiConfig);
                }
                else
                {
                    return mDatabase.UpdateAsync(argApiConfig);
                }
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public Task<List<ApiConfig>> getApiConfigAsync()
        {
            try
            {
                return mDatabase.Table<ApiConfig>().OrderBy(x => x.ProductCode).ToListAsync();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public Task<ApiConfig> getApiConfigAsync(string argProductCode)
        {
            try
            {
                return mDatabase.Table<ApiConfig>().Where(x => x.ProductCode.Equals(argProductCode)).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        public Task<int> deleteApiConfigAsync(ApiConfig argApiConfig)
        {
            try
            {
                return mDatabase.DeleteAsync(argApiConfig);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
        #endregion



    }
}