using CS.ERP.PL.SYS.DAT;
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
    public partial class FrmSysApiConfigLst : ContentView
    {
        #region"Declaring"
        List<ApiConfig> mApiConfig_lst = new List<ApiConfig>();
        ApiConfig mApiConfig = new ApiConfig();
        #endregion
        #region "constructor"

        public FrmSysApiConfigLst()
        {
            try
            {
                InitializeComponent();

                loadData();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion
        #region"Private"
        async void loadData()
        {
            try
            {
                //mApiConfig = new ApiConfig();
                List<ApiConfig> l_ApiConfig = await App.Database.getApiConfigAsync();

                colView.ItemsSource = await App.Database.getApiConfigAsync();

                //   if ()
                //  {
                //    collView.ItemsSource = await App.Database.getApiConfigAsync(mApiConfig);
                //}



            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region"Event"
     
        private void colView_onSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                mApiConfig = (ApiConfig)colView.SelectedItem;
                if (!Common.bindMenu("sys-apiconfig-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmApiConfigSet", MenuUrl = "sys-apiconfig-set", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu, mApiConfig.ProductCode);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    
        #endregion
    }
}