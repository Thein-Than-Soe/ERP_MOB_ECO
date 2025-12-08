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
    public partial class FrmPosPurchaseInvoiceLst : ContentView
    {

        #region "Declaring"
        VmlPurchaseInvoice mVmlPurchaseInvoice;
        #endregion
        #region "Constructor"
        public FrmPosPurchaseInvoiceLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchaseInvoice = new VmlPurchaseInvoice();
                mVmlPurchaseInvoice.mJSN_REQ_PURCHASE_INVOICE_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchaseInvoice.mJSN_REQ_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE = new RES_PURCHASE_INVOICE();
                mVmlPurchaseInvoice.mJSN_REQ_PURCHASE_INVOICE_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchaseInvoice.getInvoice();
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
                            TabActive.Text = "Active (" + mVmlPurchaseInvoice.InvoiceActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseInvoice.InvoicePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseInvoice.InvoiceClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchaseInvoice.InvoiceActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlPurchaseInvoice.InvoicePartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchaseInvoice.InvoiceClosedList.Count + ")";

                            CCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            CDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            CSupplier.Title = "ကုန်တင်သွင်းသူ";

                            PCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            PDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            PSupplier.Title = "ကုန်တင်သွင်းသူ";

                            AcCode.Title = "ကုန်ရောင်းပြေစာအမှတ်";
                            AcDate.Title = "ကုန်ရောင်း နေ့စွဲ";
                            AcAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            AcSupplier.Title = "ကုန်တင်သွင်းသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchaseInvoice.InvoiceActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseInvoice.InvoicePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseInvoice.InvoiceClosedList.Count + ")";
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
                    mVmlPurchaseInvoice.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchaseInvoice.searchData("");
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
                var l_RES_PURCHASE_INVOICE = (RES_PURCHASE_INVOICE)e.SelectedItem;
                if (l_RES_PURCHASE_INVOICE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_INVOICE.Ask) ;
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
                var l_RES_PURCHASE_INVOICE = (RES_PURCHASE_INVOICE)e.SelectedItem;
                if (l_RES_PURCHASE_INVOICE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_INVOICE.Ask);
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
                mVmlPurchaseInvoice.getInvoice();
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
                //mVmlPurchaseInvoice.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}