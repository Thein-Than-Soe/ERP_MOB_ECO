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
    public partial class FrmPosSalesEodLst : ContentView
    {

        #region "Declaring"
        VmlSalesEOD mVmlSalesEOD;
        #endregion
        #region "Constructor"
        public FrmPosSalesEodLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesEOD = new VmlSalesEOD();
                mVmlSalesEOD.mJSN_REQ_SETTLEMENT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesEOD.mJSN_REQ_SETTLEMENT.RES_SETTLEMENT_PAY.Add(new RES_SETTLEMENT_PAY());
                mVmlSalesEOD.mJSN_REQ_SETTLEMENT.RES_SETTLEMENT= new RES_SETTLEMENT();
                mVmlSalesEOD.getSaleEOD();
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
                            TabThisWeek.Text = "This Week (" + mVmlSalesEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "This month (" + mVmlSalesEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "All (" + mVmlSalesEOD.SaleEODList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabThisWeek.Text = "ယခုအပတ် (" + mVmlSalesEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "ယခုလ (" + mVmlSalesEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlSalesEOD.SaleEODList.Count + ")";

                            ASettlement.Title = "ငွေစာရင်းပိတ်နံပါတ်";
                            ADate.Title = "ငွေစာရင်းပိတ်ရက်စွဲ";
                            AAmount.Title = "စုစုပေါင်းငွေပမာဏ";
                            AMan.Title = "ငွေစာရင်းပိတ်သူ";

                            WSettlement.Title = "ငွေစာရင်းပိတ်နံပါတ်";
                            WDate.Title = "ငွေစာရင်းပိတ်ရက်စွဲ";
                            WAmount.Title = "စုစုပေါင်းငွေပမာဏ";
                            WMan.Title = "ငွေစာရင်းပိတ်သူ";

                            MSettlement.Title = "ငွေစာရင်းပိတ်နံပါတ်";
                            MDate.Title = "ငွေစာရင်းပိတ်ရက်စွဲ";
                            MAmount.Title = "စုစုပေါင်းငွေပမာဏ";
                            MMan.Title = "ငွေစာရင်းပိတ်သူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabThisWeek.Text = "This Week (" + mVmlSalesEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "This month (" + mVmlSalesEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "All (" + mVmlSalesEOD.SaleEODList.Count + ")";

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
                    mVmlSalesEOD.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSalesEOD.searchData("");
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
                var l_RES_SETTLEMENT_PAY = (RES_SETTLEMENT_PAY)e.SelectedItem;
                if (l_RES_SETTLEMENT_PAY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SETTLEMENT_PAY.Ask) ;
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
                var l_RES_SETTLEMENT_PAY = (RES_SETTLEMENT_PAY)e.SelectedItem;
                if (l_RES_SETTLEMENT_PAY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SETTLEMENT_PAY.Ask);
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
                mVmlSalesEOD.getSaleEOD();
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
                //mVmlSalesEOD.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}