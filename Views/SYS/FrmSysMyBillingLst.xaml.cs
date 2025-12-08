using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSysMyBillingLst : ContentView
    {
        #region "Declaring"
        VmlMyBilling mVmlMyBilling;
        #endregion
        #region "Constructor"
        public FrmSysMyBillingLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMyBilling = new VmlMyBilling();
                mVmlMyBilling.mJSN_REQ_SALE_BILL_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMyBilling.mJSN_REQ_SALE_BILL_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlMyBilling.mJSN_REQ_SALE_BILL_JUN.RES_SALE_BILLING = new RES_SALE_BILLING();
                mVmlMyBilling.getMyBilling();
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
                            TabActive.BadgeText ="Active (" + mVmlMyBilling.MyBillingActiveList.Count + ")";
                            TabPartial.BadgeText ="Inactive (" + mVmlMyBilling.MyBillingPartialList.Count + ")";
                            TabClosed.BadgeText ="All (" + mVmlMyBilling.MyBillingClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.BadgeText ="အသုံးပြု (" + mVmlMyBilling.MyBillingActiveList.Count + ")";
                            TabPartial.BadgeText ="အသုံးမပြု (" + mVmlMyBilling.MyBillingPartialList.Count + ")";
                            TabClosed.BadgeText ="အားလုံး (" + mVmlMyBilling.MyBillingClosedList.Count + ")";

                            AcCode.HeaderText = "ကုဒ်";
                            AcName.HeaderText ="အမည်";
                            AcCustomer.HeaderText ="သုံးစွဲသူ";
                            AcAmount.HeaderText ="ပမာဏ";

                            PCode.HeaderText ="ကုဒ်";
                            PName.HeaderText ="အမည်";
                            PCustomer.HeaderText ="သုံးစွဲသူ";
                            PAmount.HeaderText ="ပမာဏ";

                            CCode.HeaderText ="ကုဒ်";
                            CName.HeaderText ="အမည်";
                            CCustomer.HeaderText ="သုံးစွဲသူ";
                            CAmount.HeaderText ="ပမာဏ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.BadgeText ="Active (" + mVmlMyBilling.MyBillingActiveList.Count + ")";
                            TabPartial.BadgeText ="Inactive (" + mVmlMyBilling.MyBillingPartialList.Count + ")";
                            TabClosed.BadgeText ="All (" + mVmlMyBilling.MyBillingClosedList.Count + ")";
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
                if (!Common.bindMenu("country-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
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
                    mVmlMyBilling.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlMyBilling.searchData("");
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
                var l_RES_SALE_BILLING = (RES_SALE_BILLING)e.SelectedItem;
                if (l_RES_SALE_BILLING != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_BILLING.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    // }
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
                var l_RES_SALE_BILLING = (RES_SALE_BILLING)e.SelectedItem;
                if (l_RES_SALE_BILLING != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_BILLING.Ask);
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
                if (!Common.bindMenu("country-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
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
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
                entSearch.Text ="";
                mVmlMyBilling.getMyBilling();
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
                //mVmlMyBilling.GetCountryData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}