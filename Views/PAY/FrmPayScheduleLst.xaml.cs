using CS.ERP.PL.PAY;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.PAY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.PAY
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPayScheduleLst : ContentView
    {
        #region "Declaring"
        VmlPaySchedule mVmlPaySchedule;
        #endregion
        #region "Constructor"
        public FrmPayScheduleLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPaySchedule = new VmlPaySchedule();
                mVmlPaySchedule.mJSN_REQ_SCHEDULE.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPaySchedule.mJSN_REQ_SCHEDULE.DAT_SCHEDULE =new DAT_SCHEDULE();
                mVmlPaySchedule.getPaySchedule();
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
                            TabMonthly.Text = "Monthly (" + mVmlPaySchedule.MonthlyList.Count + ")";
                            TabWeekly.Text = "Weekly (" + mVmlPaySchedule.WeeklyList.Count + ")";
                            TabDaily.Text = "Daily (" + mVmlPaySchedule.DailyList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabMonthly.Text = "Monthly (" + mVmlPaySchedule.MonthlyList.Count + ")";
                            TabWeekly.Text = "Weekly (" + mVmlPaySchedule.WeeklyList.Count + ")";
                            TabDaily.Text = "Daily (" + mVmlPaySchedule.DailyList.Count + ")";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabMonthly.Text = "Monthly (" + mVmlPaySchedule.MonthlyList.Count + ")";
                            TabWeekly.Text = "Weekly (" + mVmlPaySchedule.WeeklyList.Count + ")";
                            TabDaily.Text = "Daily (" + mVmlPaySchedule.DailyList.Count + ")";
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
                    mVmlPaySchedule.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPaySchedule.searchData("");
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
                var l_DAT_SCHEDULE = (DAT_SCHEDULE)e.SelectedItem;
                if (l_DAT_SCHEDULE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_SCHEDULE.Ask) ;
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
                var l_DAT_SCHEDULE = (DAT_SCHEDULE)e.SelectedItem;
                if (l_DAT_SCHEDULE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_SCHEDULE.Ask);
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
                mVmlPaySchedule.getPaySchedule();
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
                //mVmlPaySchedule.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}