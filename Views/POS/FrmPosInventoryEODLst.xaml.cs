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
    public partial class FrmPosInventoryEODLst : ContentView
    {

        #region "Declaring"
        VmlInventoryEOD mVmlInventoryEOD;
        #endregion
        #region "Constructor"
        public FrmPosInventoryEODLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlInventoryEOD = new VmlInventoryEOD();
                mVmlInventoryEOD.mJSN_REQ_SETTLEMENT_INVENTORY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlInventoryEOD.mJSN_REQ_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY = new DAT_SETTLEMENT_INVENTORY();
                mVmlInventoryEOD.mJSN_REQ_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_SUMMARY.Add(new DAT_SETTLEMENT_SUMMARY());
                mVmlInventoryEOD.getInventoryEOD();
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
                            TabThisWeek.Text = "This Week (" + mVmlInventoryEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "This month (" + mVmlInventoryEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "All (" + mVmlInventoryEOD.InventoryList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabThisWeek.Text = "ယခုအပတ် (" + mVmlInventoryEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "ယခုလ (" + mVmlInventoryEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlInventoryEOD.InventoryList.Count + ")";

                            ASettlement.Title = "ကုန်စာရင်းပိတ်နံပါတ်";
                            ADate.Title = "ကုန်စာရင်းပိတ်ရက်စွဲ";
                            ATotalTransaction.Title = "စုစုပေါင်းဦးရေ";
                            ASalePerson.Title = "စာရင်းပိတ်သူ";

                            WSettlement.Title = "ကုန်စာရင်းပိတ်နံပါတ်";
                            WDate.Title = "ကုန်စာရင်းပိတ်ရက်စွဲ";
                            WTotalTransaction.Title = "စုစုပေါင်းဦးရေ";
                            WSalePerson.Title = "စာရင်းပိတ်သူ";

                            MSettlement.Title = "ကုန်စာရင်းပိတ်နံပါတ်";
                            MDate.Title = "ကုန်စာရင်းပိတ်ရက်စွဲ";
                            WTotalTransaction.Title = "စုစုပေါင်းဦးရေ";
                            MSalePerson.Title = "စာရင်းပိတ်သူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabThisWeek.Text = "This Week (" + mVmlInventoryEOD.ThisWeekList.Count + ")";
                            TabThisMonth.Text = "This month (" + mVmlInventoryEOD.ThisMonthList.Count + ")";
                            TabAll.Text = "All (" + mVmlInventoryEOD.InventoryList.Count + ")";

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
                    mVmlInventoryEOD.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlInventoryEOD.searchData("");
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
                var l_DAT_SETTLEMENT_INVENTORY = (DAT_SETTLEMENT_INVENTORY)e.SelectedItem;
                if (l_DAT_SETTLEMENT_INVENTORY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_SETTLEMENT_INVENTORY.Ask) ;
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
                var l_DAT_SETTLEMENT_INVENTORY = (DAT_SETTLEMENT_INVENTORY)e.SelectedItem;
                if (l_DAT_SETTLEMENT_INVENTORY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_SETTLEMENT_INVENTORY.Ask);
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
                mVmlInventoryEOD.getInventoryEOD();
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
                //mVmlInventoryEOD.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}