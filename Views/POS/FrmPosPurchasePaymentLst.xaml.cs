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
    public partial class FrmPosPurchasePaymentLst : ContentView
    {

        #region "Declaring"
        VmlPurchasePayment mVmlPurchasePayment;
        #endregion
        #region "Constructor"
        public FrmPosPurchasePaymentLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchasePayment = new VmlPurchasePayment();
                mVmlPurchasePayment.mJSN_REQ_PURCHASE_PAYMENT_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchasePayment.mJSN_REQ_PURCHASE_PAYMENT_JUN.RES_PURCHASE_PAYMENT = new RES_PURCHASE_PAYMENT();
                mVmlPurchasePayment.mJSN_REQ_PURCHASE_PAYMENT_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchasePayment.getPayment();
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
                            TabActive.Text = "Active (" + mVmlPurchasePayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchasePayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchasePayment.PaymentClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchasePayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlPurchasePayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchasePayment.PaymentClosedList.Count + ")";

                            CCode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            CDate.Title = "ငွေပေးချေးသည့်ရက်စွဲ";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            CType.Title = "ကုန်ရောင်းခြင်း အမျိူးအစား";

                            PCode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            PDate.Title = "ငွေပေးချေးသည့်ရက်စွဲ";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            PType.Title = "ကုန်ရောင်းခြင်း အမျိူးအစား";

                            ACode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            ADate.Title = "ငွေပေးချေးသည့်ရက်စွဲ";
                            AAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            AType.Title = "ကုန်ရောင်းခြင်း အမျိူးအစား";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchasePayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchasePayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchasePayment.PaymentClosedList.Count + ")";
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
                    mVmlPurchasePayment.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchasePayment.searchData("");
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
                var l_RES_PURCHASE_PAYMENT = (RES_PURCHASE_PAYMENT)e.SelectedItem;
                if (l_RES_PURCHASE_PAYMENT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_PAYMENT.Ask) ;
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
                var l_RES_PURCHASE_PAYMENT = (RES_PURCHASE_PAYMENT)e.SelectedItem;
                if (l_RES_PURCHASE_PAYMENT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_PAYMENT.Ask);
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
                mVmlPurchasePayment.getPayment();
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
                //mVmlPurchasePayment.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}