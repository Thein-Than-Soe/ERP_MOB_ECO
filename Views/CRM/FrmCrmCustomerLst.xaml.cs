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
    public partial class FrmCrmCustomerLst : ContentView
    {
       
            #region "Declaring"
            VmlCustomer mVmlCustomer;
            #endregion
            #region "Constructor"
            public FrmCrmCustomerLst()
            {
                try
                {
                    InitializeComponent();
                    BindingContext = mVmlCustomer = new VmlCustomer();
                    mVmlCustomer.mJSN_REQ_CUSTOMERNCONTACT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                    mVmlCustomer.mJSN_REQ_CUSTOMERNCONTACT.DAT_CUSTOMER= new DAT_CUSTOMER();
                    mVmlCustomer.getCustomer();
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
                            TabActive.Text = "Active (" + mVmlCustomer.CustomerActiveList.Count + ")";
                            TabInactive.Text = "Inactive (" + mVmlCustomer.CustomerInActiveList.Count + ")";
                            TabAll.Text = "All (" + mVmlCustomer.CustomerList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "အသုံးပြု (" + mVmlCustomer.CustomerActiveList.Count + ")";
                            TabInactive.Text = "အသုံးမပြု (" + mVmlCustomer.CustomerInActiveList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlCustomer.CustomerList.Count + ")";

                            AcCustomer.Title = "သုံးစွဲသူ";
                            AcCode.Title = "ကုဒ်";
                            AcPhone.Title = "ဖုန်း";
                            AcEmail.Title = "အီးမေးလ်";

                            ACustomer.Title = "သုံးစွဲသူ";
                            ACode.Title = "ကုဒ်";
                            APhone.Title = "ဖုန်း";
                            AEmail.Title = "အီးမေးလ်";

                            ICustomer.Title = "သုံးစွဲသူ";
                            ICode.Title = "ကုဒ်";
                            IPhone.Title = "ဖုန်း";
                            IEmail.Title = "အီးမေးလ်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlCustomer.CustomerActiveList.Count + ")";
                            TabInactive.Text = "Inactive (" + mVmlCustomer.CustomerInActiveList.Count + ")";
                            TabAll.Text = "All (" + mVmlCustomer.CustomerList.Count + ")";
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
                        mVmlCustomer.searchData(e.NewTextValue);
                    }
                    else
                    {
                        mVmlCustomer.searchData("");
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
                    mVmlCustomer.getCustomer();
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
                    //mVmlCustomer.GetAccessData();
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }

            }

            #endregion
        }

    }
