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
    public partial class FrmSysMyOrderLst : ContentView
    {
        #region "Declaring"
        VmlMyOrder mVmlMyOrder;
        #endregion
        #region "Constructor"
        public FrmSysMyOrderLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMyOrder = new VmlMyOrder();
                mVmlMyOrder.mJSN_REQ_SALE_LOAD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMyOrder.mJSN_REQ_SALE_LOAD.RES_SALE_BROWSE= new RES_SALE_BROWSE();
                mVmlMyOrder.getMyOrder();
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
                            TabActive.BadgeText ="Active (" + mVmlMyOrder.MyOrderActiveList.Count + ")";
                            TabPartial.BadgeText ="Partial (" + mVmlMyOrder.MyOrderPartialList.Count + ")";
                            TabClosed.BadgeText ="Closed (" + mVmlMyOrder.MyOrderClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.BadgeText ="အသုံးပြု (" + mVmlMyOrder.MyOrderActiveList.Count + ")";
                            TabPartial.BadgeText ="တစိတ်တပိုင်း (" + mVmlMyOrder.MyOrderPartialList.Count + ")";
                            TabClosed.BadgeText ="ပိတ်သိမ်း (" + mVmlMyOrder.MyOrderClosedList.Count + ")";

                            ACode.HeaderText ="ကုဒ်";
                            AAmount.HeaderText ="ပမာဏ";
                            ADate.HeaderText ="ရက်စွဲ";
                            ACustomer.HeaderText ="သုံးစွဲသူ";

                            PCode.HeaderText ="ကုဒ်";
                            PAmount.HeaderText ="ပမာဏ";
                            PDate.HeaderText ="ရက်စွဲ";
                            PCustomer.HeaderText ="သုံးစွဲသူ";

                            CCode.HeaderText ="ကုဒ်";
                            CAmount.HeaderText ="ပမာဏ";
                            CDate.HeaderText ="ရက်စွဲ";
                            CCustomer.HeaderText ="သုံးစွဲသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.BadgeText ="Active (" + mVmlMyOrder.MyOrderActiveList.Count + ")";
                            TabPartial.BadgeText ="Partial (" + mVmlMyOrder.MyOrderPartialList.Count + ")";
                            TabClosed.BadgeText ="Closed (" + mVmlMyOrder.MyOrderClosedList.Count + ")";
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
                    mVmlMyOrder.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlMyOrder.searchData("");
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
                var l_RES_SALE_BROWSE = (RES_SALE_BROWSE)e.SelectedItem;
                if (l_RES_SALE_BROWSE != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_BROWSE.Ask);
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
                var l_RES_SALE_BROWSE = (RES_SALE_BROWSE)e.SelectedItem;
                if (l_RES_SALE_BROWSE != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_BROWSE.Ask);
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
                mVmlMyOrder.getMyOrder();
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
                //mVmlMyOrder.GetCountryData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}