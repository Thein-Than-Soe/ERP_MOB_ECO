using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.CRM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCrmUserGuideLst : ContentView
    {

        #region "Declaring"
        VmlUserGuide mVmlUserGuide;
        #endregion
        #region "Constructor"
        public FrmCrmUserGuideLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlUserGuide = new VmlUserGuide();
                mVmlUserGuide.mJSN_REQ_MANUAL_CONTENT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlUserGuide.mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT.Add(new DAT_MANUAL_CONTENT());
                mVmlUserGuide.getUserguide();
                switchLanguage();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion
        #region "Private Mehtod"
        public void switchLanguage()
        {
            try
            {
                switch (Common.mCommon.UserSetting.LanguageAsk)
                {
                    case "1"://English
                        {
                            entSearch.Placeholder = "Search";
                            TabMobile.Text = "Mobile (" + mVmlUserGuide.MobileList.Count + ")";
                            TabWeb.Text = "Web (" + mVmlUserGuide.WebList.Count + ")";
                            TabAll.Text = "All (" + mVmlUserGuide.UserguideList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabMobile.Text = "မိုဘိုင် (" + mVmlUserGuide.MobileList.Count + ")";
                            TabWeb.Text = "ဝဘ် (" + mVmlUserGuide.WebList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlUserGuide.UserguideList.Count + ")";

                            MName.Title = "အမည်";
                            MProject.Title = "စီမံကိန်း";
                            MProduct.Title = "ထုတ်ကုန်";

                            WName.Title = "အမည်";
                            WProject.Title = "စီမံကိန်း";
                            WProduct.Title = "ထုတ်ကုန်";

                            AName.Title = "အမည်";
                            AProject.Title = "စီမံကိန်း";
                            AProduct.Title = "ထုတ်ကုန်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabMobile.Text = "Mobile (" + mVmlUserGuide.MobileList.Count + ")";
                            TabWeb.Text = "Web (" + mVmlUserGuide.WebList.Count + ")";
                            TabAll.Text = "All (" + mVmlUserGuide.UserguideList.Count + ")";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Event"
        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
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
                    mVmlUserGuide.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlUserGuide.searchData("");
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
                var l_DAT_CUSTOMER = (DAT_CUSTOMER)e.SelectedItem;
                if (l_DAT_CUSTOMER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_CUSTOMER.Ask) ;
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
                var l_DAT_CUSTOMER = (DAT_CUSTOMER)e.SelectedItem;
                if (l_DAT_CUSTOMER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_CUSTOMER.Ask);
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
        private void TgrCardView_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
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
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
                entSearch.Text = "";
                mVmlUserGuide.getUserguide();
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
                //mVmlUserGuide.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}