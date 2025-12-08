using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmApiConfigLst : ContentView
    {

        #region"Declaring"
        List<ApiConfig> mApiConfig_lst = new List<ApiConfig>();
        ApiConfig mApiConfig = new ApiConfig();
        #endregion

        #region "constructor"

        public FrmApiConfigLst()
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

        #region "method"

        

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
                Common.routeMenu(Common.mCommon.SelectedMenu, mApiConfig.Ask.ToString());

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
     
        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (entSearch.Text != null && entSearch.Text != "")
                {
                    mApiConfig = new ApiConfig();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    
        public void searchData(string argKeyword)
        {
            try
            {
                List<ApiConfig> l_ApiConfig_lst = new List<ApiConfig>();

                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (ApiConfig l_ApiConfig in l_ApiConfig_lst)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_ApiConfig.ProductCode.ToLower().Contains(argKeyword)
                            || l_ApiConfig.APIServiceName.ToLower().Contains(argKeyword)
                            || l_ApiConfig.APIURL.ToLower().Contains(argKeyword))
                        {
                            l_ApiConfig_lst.Add(l_ApiConfig);
                        }
                    }
                }
                else
                {
                    // l_ApiConfig_lst = ApiConfig;// OriginalApplicantList.GetRange(0, OriginalApplicantList.Count);
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}