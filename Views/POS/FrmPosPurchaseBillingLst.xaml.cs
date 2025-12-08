using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosPurchaseBillingLst : ContentView
    {

        #region "Declaring"
        VmlPurchaseBilling mVmlPurchaseBilling;
        #endregion
        #region "Constructor"
        public FrmPosPurchaseBillingLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchaseBilling = new VmlPurchaseBilling();
                mVmlPurchaseBilling.mJSN_REQ_PURCHASE_BILL_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchaseBilling.mJSN_REQ_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING = new RES_PURCHASE_BILLING();
                mVmlPurchaseBilling.mJSN_REQ_PURCHASE_BILL_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchaseBilling.getBilling();
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
                            TabActive.Text = "Active (" + mVmlPurchaseBilling.BillingActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseBilling.BillingPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseBilling.BillingClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchaseBilling.BillingActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlPurchaseBilling.BillingPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchaseBilling.BillingClosedList.Count + ")";

                            CCode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            CDate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            CSupplier.Title = "ကုန်တင်သွင်းသူ";

                            PCode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            PDate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            PSupplier.Title = "ကုန်တင်သွင်းသူ";

                            ACode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            ADate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            AAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            ASupplier.Title = "ကုန်တင်သွင်းသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchaseBilling.BillingActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseBilling.BillingPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseBilling.BillingClosedList.Count + ")";
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
                    mVmlPurchaseBilling.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchaseBilling.searchData("");
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
                var l_RES_PURCHASE_BILLING = (RES_PURCHASE_BILLING)e.SelectedItem;
                if (l_RES_PURCHASE_BILLING != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_BILLING.Ask) ;
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
                var l_RES_PURCHASE_BILLING = (RES_PURCHASE_BILLING)e.SelectedItem;
                if (l_RES_PURCHASE_BILLING != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_BILLING.Ask);
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
                mVmlPurchaseBilling.getBilling();
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
                //mVmlPurchaseBilling.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}