using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmApiConfigSet : ContentView
    {
        #region"Declaring"
        private ApiConfig mapiConfig = new ApiConfig();
        public List<ApiConfig> mApiConfig_lst = new List<ApiConfig>();
        public string code;
        #endregion

        #region "constructor"
        public FrmApiConfigSet()
        {
            InitializeComponent();
            ClearFormData();
        }

      

        public FrmApiConfigSet(string argCode)
        {
            InitializeComponent();
           
            // App.Database.getApiConfigAsync(Ask);
            this.mapiConfig.ProductCode = argCode;
            code = argCode;
            // string ask = argApiconfig.Ask.ToString();
            App.Database.getApiConfigAsync(argCode);
            bindView();
            //this.mapiConfig = App.Database.getApiConfigAsync(argCode);
        }
        #endregion

        #region "method"
        public void ClearFormData()
        {
            try
            {
                entApiAcceptType.Text ="";
                entApiContentType.Text ="";
                entCode.Text ="";
                entUploadURL.Text ="";
                entAPIURL.Text ="";
                entAPIProtocol.Text ="";
                entAPIServer.Text ="";
                entAPIPort.Text ="";
                entAPIServiceName.Text ="";
                entApiContentType.Text ="";
                entApiKey.Text ="";
                entPublicKey.Text ="";
                entSecreteKey.Text ="";
                entUser.Text ="";
                entPassword.Text ="";
                entSequence.Text ="";
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindObject()
        {
            bool flag = false;
            try
            {
                if (!string.IsNullOrWhiteSpace(entCode.Text) &&  !string.IsNullOrWhiteSpace(entUploadURL.Text) && !string.IsNullOrWhiteSpace(entAPIURL.Text) && !string.IsNullOrWhiteSpace(entAPIProtocol.Text) && !string.IsNullOrWhiteSpace(entAPIServiceName.Text) && !string.IsNullOrWhiteSpace(entAPIServer.Text) && !string.IsNullOrWhiteSpace(entApiContentType.Text) && !string.IsNullOrWhiteSpace(entApiAcceptType.Text))
                {
                    mapiConfig.ProductCode = entCode.Text.Trim().ToString();
                    mapiConfig.Sequence = int.Parse(entSequence.Text.Trim());
                    mapiConfig.UploadURL = entUploadURL.Text.Trim().ToString();
                    mapiConfig.APIURL = entAPIURL.Text.Trim().ToString();
                    mapiConfig.APIProtocol = entAPIProtocol.Text.Trim().ToString();
                    mapiConfig.APIServer = entAPIServer.Text.Trim().ToString();
                    mapiConfig.APIServiceName = entAPIServiceName.Text.Trim().ToString();
                    mapiConfig.ApiContentType = entApiContentType.Text.Trim().ToString();
                    mapiConfig.ApiAcceptType = entApiAcceptType.Text.Trim().ToString();

                    flag = true;
                }
                else
                {
                    flag = false;
                    entCode.Focus();
                }
                            

                if (!string.IsNullOrWhiteSpace(entAPIPort.Text))
                {
                    mapiConfig.APIPort = entAPIPort.Text.Trim().ToString();
                }
                else
                {
                    mapiConfig.APIPort = "";
                }

                if (!string.IsNullOrWhiteSpace(entApiKey.Text))
                {
                    mapiConfig.ApiKey = entApiKey.Text.Trim().ToString();

                }
                else
                {
                    mapiConfig.ApiKey = "";

                }

                if (!string.IsNullOrWhiteSpace(entPublicKey.Text))
                {
                    mapiConfig.PublicKey = entPublicKey.Text.Trim().ToString();

                }
                else
                {
                    mapiConfig.PublicKey = "";

                }

                if (!string.IsNullOrWhiteSpace(entSecreteKey.Text))
                {
                    mapiConfig.SecreteKey = entSecreteKey.Text.Trim().ToString();

                }
                else
                {
                    mapiConfig.SecreteKey = "";

                }

                if (!string.IsNullOrWhiteSpace(entUser.Text))
                {
                    mapiConfig.User = entUser.Text.Trim().ToString();

                }
                else
                {
                    mapiConfig.User = "";

                }

                if (!string.IsNullOrWhiteSpace(entPassword.Text))
                {
                    mapiConfig.Password = entPassword.Text.Trim().ToString();

                }
                else
                {
                    mapiConfig.Password = "";
                }


                return flag;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private async void bindView()
        {
            try
            {
                this.mapiConfig = await App.Database.getApiConfigAsync(mapiConfig.ProductCode);
               
                BindingContext = mapiConfig;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion

        #region "Event"

        async void btnSave_onClick(object sender, EventArgs e)
        {
            try
            {
                if (bindObject())
                {
                    await App.Database.saveApiConfigAsync(mapiConfig);
                    await Navigation.PopAsync();
                    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, "save Successfully! ");
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, "Fail, Please try again! ");
                }
               
                
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        async void btnCancel_onClick(object sender, EventArgs e)
        {
            try
            {
                ClearFormData();
                          }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        #endregion

    }
}