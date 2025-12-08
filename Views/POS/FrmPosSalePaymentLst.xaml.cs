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
    public partial class FrmPosSalePaymentLst : ContentView
    {

        #region "Declaring"
        VmlSalesPayment mVmlSalesPayment;
        #endregion
        #region "Constructor"
        public FrmPosSalePaymentLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesPayment = new VmlSalesPayment();
                mVmlSalesPayment.mJSN_REQ_SALE_PAYMENT_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesPayment.mJSN_REQ_SALE_PAYMENT_JUN.RES_SALE_PAYMENT = new RES_SALE_PAYMENT();
                mVmlSalesPayment.mJSN_REQ_SALE_PAYMENT_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesPayment.getPayment();
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
                            TabActive.Text = "Active (" + mVmlSalesPayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesPayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesPayment.PaymentClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "စာရင်းအဖွင့် (" + mVmlSalesPayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "လုပ်ဆောင်ဆဲ (" + mVmlSalesPayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "စာရင်းအပိတ် (" + mVmlSalesPayment.PaymentClosedList.Count + ")";

                            CCode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            CDate.Title = "ငွေပေးချေးသည့်ရက်စွဲ";
                            CCustomer.Title = "ငွေပေးချေမှု အမျိူးအစား";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";

                            AcCode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            AcDate.Title = "ငွေပေးချေးသည့်ရက်စွဲ";
                            AcCustomer.Title = "ငွေပေးချေမှု အမျိူးအစား";
                            AcAmount.Title = "စုစုပေါင်း သင့်ငွေ";

                            PCode.Title = "ငွေပေးချေးသည့် နံပါတ်";
                            PDate.Title = "ငွေပေးချေမှု အမျိူးအစား";
                            PCustomer.Title = "ငွေပေးချေမှုအမျိုးအစား";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlSalesPayment.PaymentActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesPayment.PaymentPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesPayment.PaymentClosedList.Count + ")";
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
                    mVmlSalesPayment.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSalesPayment.searchData("");
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
                var l_RES_SALE_PAYMENT = (RES_SALE_PAYMENT)e.SelectedItem;
                if (l_RES_SALE_PAYMENT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_PAYMENT.Ask) ;
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
                var l_RES_SALE_PAYMENT = (RES_SALE_PAYMENT)e.SelectedItem;
                if (l_RES_SALE_PAYMENT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_PAYMENT.Ask);
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
                mVmlSalesPayment.getPayment();
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
                //mVmlSalesPayment.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}