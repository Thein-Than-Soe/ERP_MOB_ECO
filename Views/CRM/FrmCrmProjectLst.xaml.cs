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
    public partial class FrmCrmProjectLst : ContentView
    {

        #region "Declaring"
        VmlProject mVmlProject;
        #endregion
        #region "Constructor"
        public FrmCrmProjectLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlProject = new VmlProject();
                mVmlProject.mJSN_REQ_PROJECT_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlProject.mJSN_REQ_PROJECT_JUN.DAT_PROJECT= new DAT_PROJECT();
                mVmlProject.getProject();
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
                            TabActive.Text = "Active (" + mVmlProject.ProjectActiveList.Count + ")";
                            TabInActive.Text = "Inactive (" + mVmlProject.ProjectInActiveList.Count + ")";
                            TabAll.Text = "All (" + mVmlProject.ProjectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "အသုံးပြု (" + mVmlProject.ProjectActiveList.Count + ")";
                            TabInActive.Text = "အသုံးမပြု (" + mVmlProject.ProjectInActiveList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlProject.ProjectList.Count + ")";

                            AcCustomer.Title = "သုံးစွဲသူ";
                            AcProject.Title = "စီမံကိန်း";
                            AcSD.Title = "စတင်သည့်ရက်စွဲ";
                            AcED.Title = "ကုန်ဆုံးရက်";

                            ICustomer.Title = "သုံးစွဲသူ";
                            IProject.Title = "စီမံကိန်း";
                            ISD.Title = "စတင်သည့်ရက်စွဲ";
                            IED.Title = "ကုန်ဆုံးရက်";

                            ACustomer.Title = "သုံးစွဲသူ";
                            AProject.Title = "စီမံကိန်း";
                            ASD.Title = "စတင်သည့်ရက်စွဲ";
                            AED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlProject.ProjectActiveList.Count + ")";
                            TabInActive.Text = "Inactive (" + mVmlProject.ProjectInActiveList.Count + ")";
                            TabAll.Text = "All (" + mVmlProject.ProjectList.Count + ")";
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
                    mVmlProject.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlProject.searchData("");
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
                mVmlProject.getProject();
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
                //mVmlProject.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}