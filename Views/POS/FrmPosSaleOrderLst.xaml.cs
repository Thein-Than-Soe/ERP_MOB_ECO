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
    public partial class FrmPosSaleOrderLst : ContentView
    {

        #region "Declaring"
        VmlSalesOrder mVmlSalesOrder;
        #endregion
        #region "Constructor"
        public FrmPosSaleOrderLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesOrder = new VmlSalesOrder();
                mVmlSalesOrder.mJSN_REQ_SALE_ORDER.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesOrder.mJSN_REQ_SALE_ORDER.RES_SALE_ORDER = new RES_SALE_ORDER();           
                mVmlSalesOrder.getOrder();
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
                            TabActive.Text = "Active (" + mVmlSalesOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesOrder.OrderPartailList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesOrder.OrderClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "စာရင်းအဖွင့် (" + mVmlSalesOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "လုပ်ဆောင်ဆဲ (" + mVmlSalesOrder.OrderPartailList.Count + ")";
                            TabClosed.Text = "စာရင်းအပိတ် (" + mVmlSalesOrder.OrderClosedList.Count + ")";

                            CCode.Title = "အော်ဒါ နံပါတ်";
                            CDate.Title = "အော်ဒါ နေ့စွဲ";
                            CCustomer.Title = "၀ယ်ယူသူ";
                            CTotal.Title = "စုစုပေါင်း သင့်ငွေ";

                            AcCode.Title = "အော်ဒါ နံပါတ်";
                            AcDate.Title = "အော်ဒါ နေ့စွဲ";
                            AcCustomer.Title = "၀ယ်ယူသူ";
                            AcTotal.Title = "စုစုပေါင်း သင့်ငွေ";

                            PCode.Title = "အော်ဒါ နံပါတ်";
                            PDate.Title = "အော်ဒါ နေ့စွဲ";
                            PCustomer.Title = "၀ယ်ယူသူ";
                            PTotal.Title = "စုစုပေါင်း သင့်ငွေ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlSalesOrder.OrderActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesOrder.OrderPartailList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesOrder.OrderClosedList.Count + ")";
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
                    mVmlSalesOrder.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSalesOrder.searchData("");
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
                var l_RES_SALE_ORDER = (RES_SALE_ORDER)e.SelectedItem;
                if (l_RES_SALE_ORDER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_ORDER.Ask) ;
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
                var l_RES_SALE_ORDER = (RES_SALE_ORDER)e.SelectedItem;
                if (l_RES_SALE_ORDER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SALE_ORDER.Ask);
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
                mVmlSalesOrder.getOrder();
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
                //mVmlSalesOrder.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}