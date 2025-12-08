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
    public partial class FrmPosSaleBiilingLst : ContentView
    {

        #region "Declaring"
        VmlSalesBilling mVmlSalesBilling;
        #endregion
        #region "Constructor"
        public FrmPosSaleBiilingLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesBilling = new VmlSalesBilling();
                mVmlSalesBilling.mJSN_REQ_SALE_BILL_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesBilling.mJSN_REQ_SALE_BILL_JUN.RES_SALE_BILLING = new RES_SALE_BILLING();
                mVmlSalesBilling.mJSN_REQ_SALE_BILL_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());               
                mVmlSalesBilling.getBill();
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
                            TabActive.Text = "Active (" + mVmlSalesBilling.BillActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesBilling.BillPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesBilling.BillClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "စာရင်းအဖွင့် (" + mVmlSalesBilling.BillActiveList.Count + ")";
                            TabPartial.Text = "လုပ်ဆောင်ဆဲ (" + mVmlSalesBilling.BillPartialList.Count + ")";
                            TabClosed.Text = "စာရင်းအပိတ် (" + mVmlSalesBilling.BillClosedList.Count + ")";

                            AcCode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            AcDate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            AcCustomer.Title = "၀ယ်ယူသူ";
                            AcAmount.Title = "စုစုပေါင်း သင့်ငွေ";


                            PCode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            PDate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            PCustomer.Title = "၀ယ်ယူသူ";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";

                            CCode.Title = "ငွေတောင်းခံလွှာအမှတ်";
                            CDate.Title = "ငွေတောင်းခံမည့် နေ့စွဲ";
                            CCustomer.Title = "၀ယ်ယူသူ";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlSalesBilling.BillActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesBilling.BillPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesBilling.BillClosedList.Count + ")";
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
                    mVmlSalesBilling.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSalesBilling.searchData("");
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
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_BILLING.Ask) ;
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
                var l_RES_SALE_BILLING = (RES_SALE_BILLING)e.SelectedItem;
                if (l_RES_SALE_BILLING != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_BILLING.Ask);
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
                mVmlSalesBilling.getBill();
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
                //mVmlSalesBilling.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}