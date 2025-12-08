using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.ViewsModel.Frame;
using CS.ERP_MOB.ViewsModel.SYS;

namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCountryLst : ContentView
    {
        #region "Declaring"
        VmlCountry mVmlCountry;
       
        #endregion
        #region "Constructor"
        public FrmCountryLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlCountry = new VmlCountry();

                mVmlCountry.mJSN_REQ_COUNTRY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlCountry.mJSN_REQ_COUNTRY.RES_COUNTRY.Add(new RES_COUNTRY());
                mVmlCountry.getCountry();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion
        #region "Private Mehtod"

        #endregion
        #region "Event"
        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCountrySet", MenuUrl = "sys-country-set", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("country-set"))
                //{
                //    Common.routeMenu("country-set", "Country Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
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
                    mVmlCountry.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlCountry.searchData("");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_COUNTRY = (RES_COUNTRY)e.SelectedItem;
                if (l_RES_COUNTRY != null)
                {
                    if (!Common.bindMenu("sys-country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCountrySet", MenuUrl = "sys-country-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_RES_COUNTRY.Ask);
                    //Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_COUNTRY.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
              

        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_COUNTRY = (RES_COUNTRY)e.SelectedItem;
                if (l_RES_COUNTRY != null)
                {
                    if (!Common.bindMenu("sys-country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCountrySet", MenuUrl = "sys-country-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_RES_COUNTRY.Ask);
                    //Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_COUNTRY.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrCardView_Tapped(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_COUNTRY = (RES_COUNTRY)e.SelectedItem;
                if (l_RES_COUNTRY != null)
                {
                    if (!Common.bindMenu("sys-country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCountrySet", MenuUrl = "sys-country-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_RES_COUNTRY.Ask);
                    //Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_COUNTRY.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
                entSearch.Text ="";
                mVmlCountry.getCountry();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrDisplayColumn_Tapped(object sender, EventArgs e)
        {
            try
            {
                //mVmlCountry.GetCountryData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

   

        #endregion
    }
}