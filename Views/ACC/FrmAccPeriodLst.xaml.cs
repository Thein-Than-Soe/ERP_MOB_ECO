using CS.ERP.PL.ACC.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.ACC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAccPeriodLst : ContentView
    {
        #region "Declaring"
        VmlAccountPeriod mVmlAccountPeriod;
        #endregion
        #region "Constructor"
        public FrmAccPeriodLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAccountPeriod = new VmlAccountPeriod();
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Add(new DAT_ACCOUNT_PERIOD());
                mVmlAccountPeriod.getAccountPeriod();
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
                            TabOpen.BadgeText = "Open (" + mVmlAccountPeriod.AccountPeriodOpenList.Count + ")";
                            TabClosed.BadgeText = "Closed (" + mVmlAccountPeriod.AccountPeriodClosedList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccountPeriod.AccountPeriodList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.BadgeText = "ဖွင့်သည်။ (" + mVmlAccountPeriod.AccountPeriodOpenList.Count + ")";
                            TabClosed.BadgeText = "ပိတ်သိမ် (" + mVmlAccountPeriod.AccountPeriodClosedList.Count + ")";
                            TabAll.BadgeText = "အားလုံး (" + mVmlAccountPeriod.AccountPeriodList.Count + ")";
                            OName.HeaderText = "အမည်";
                            OType.HeaderText = "အကောင့်အမျိုးအစား";
                            OSD.HeaderText = "စတင်သည့်ရက်စွဲ";
                            OED.HeaderText = "ကုန်ဆုံးရက်";
                                                        
                            CName.HeaderText = "အမည်";
                            CType.HeaderText = "အကောင့်အမျိုးအစား";
                            CSD.HeaderText = "စတင်သည့်ရက်စွဲ";
                            CED.HeaderText = "ကုန်ဆုံးရက်";

                            AName.HeaderText = "အမည်";
                            AType.HeaderText = "အကောင့်အမျိုးအစား";
                            ASD.HeaderText = "စတင်သည့်ရက်စွဲ";
                            AED.HeaderText = "ကုန်ဆုံးရက်";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.BadgeText = "Open (" + mVmlAccountPeriod.AccountPeriodOpenList.Count + ")";
                            TabClosed.BadgeText = "Closed (" + mVmlAccountPeriod.AccountPeriodClosedList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccountPeriod.AccountPeriodList.Count + ")";
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
                    mVmlAccountPeriod.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlAccountPeriod.searchData("");
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
                var l_DAT_ACCOUNT_PERIOD = (DAT_ACCOUNT_PERIOD)e.SelectedItem;
                if (l_DAT_ACCOUNT_PERIOD != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT_PERIOD.Ask) ;
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
                var l_DAT_ACCOUNT_PERIOD = (DAT_ACCOUNT_PERIOD)e.SelectedItem;
                if (l_DAT_ACCOUNT_PERIOD != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT_PERIOD.Ask);
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
                entSearch.Text ="";
                mVmlAccountPeriod.getAccountPeriod();
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
                //mVmlAccountPeriod.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}