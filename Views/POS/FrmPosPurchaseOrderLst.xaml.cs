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
    public partial class FrmPosPurchaseOrderLst : ContentView
    {

        #region "Declaring"
        VmlPurchaseOrder mVmlPurchaseOrder;
        #endregion
        #region "Constructor"
        public FrmPosPurchaseOrderLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchaseOrder = new VmlPurchaseOrder();
                mVmlPurchaseOrder.mJSN_REQ_PURCHASE_ORDER_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchaseOrder.mJSN_REQ_PURCHASE_ORDER_JUN.RES_PURCHASE_ORDER = new RES_PURCHASE_ORDER();
                mVmlPurchaseOrder.mJSN_REQ_PURCHASE_ORDER_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchaseOrder.getOrder();
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
                            TabActive.Text = "Active (" + mVmlPurchaseOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseOrder.OrderPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseOrder.OrderClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchaseOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlPurchaseOrder.OrderPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchaseOrder.OrderClosedList.Count + ")";

                            CCode.Title = "အော်ဒါ နံပါတ်";
                            CDate.Title = "အော်ဒါ နေ့စွဲ";
                            CAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            CSupplier.Title = "ကုန်တင်သွင်းသူ";

                            PCode.Title = "အော်ဒါ နံပါတ်";
                            PDate.Title = "အော်ဒါ နေ့စွဲ";
                            PAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            PSupplier.Title = "ကုန်တင်သွင်းသူ";

                            ACode.Title = "အော်ဒါ နံပါတ်";
                            ADate.Title = "အော်ဒါ နေ့စွဲ";
                            AAmount.Title = "စုစုပေါင်း သင့်ငွေ";
                            ASupplier.Title = "ကုန်တင်သွင်းသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchaseOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseOrder.OrderPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseOrder.OrderClosedList.Count + ")";
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
                    mVmlPurchaseOrder.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchaseOrder.searchData("");
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
                var l_RES_PURCHASE_ORDER = (RES_PURCHASE_ORDER)e.SelectedItem;
                if (l_RES_PURCHASE_ORDER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_ORDER.Ask) ;
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
                var l_RES_PURCHASE_ORDER = (RES_PURCHASE_ORDER)e.SelectedItem;
                if (l_RES_PURCHASE_ORDER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_ORDER.Ask);
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
                mVmlPurchaseOrder.getOrder();
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
                //mVmlPurchaseOrder.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}