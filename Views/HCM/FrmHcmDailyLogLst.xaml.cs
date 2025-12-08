using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.HCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.HCM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmHcmDailyLogLst : ContentView
    {
        #region "Declaring"
        VmlDailyLog mVmlDailyLog;
        #endregion
        #region "Constructor"
        public FrmHcmDailyLogLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlDailyLog = new VmlDailyLog();
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Add(new ERP.PL.HCM.DAT.DAT_EMPLOYEE_DAILY_LOG());
                mVmlDailyLog.getEmployeeDailyLog();
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
                            TabToday.Text = "Today (" + mVmlDailyLog.TodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlDailyLog.LastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlDailyLog.LastMonthList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabToday.Text = "ဒီနေ့ (" + mVmlDailyLog.TodayList.Count + ")";
                            TabLastWeek.Text = "ပြီးခဲ့သည့်အပတ် (" + mVmlDailyLog.LastWeekList.Count + ")";
                            TabLastMonth.Text = "ပြီးခဲ့သည့်လ (" + mVmlDailyLog.LastMonthList.Count + ")";

                            TEmployee.Title = "ဝန်ထမ်း";
                            TTicket.Title = "လုပ်ငန်းစဉ် ";
                            TROType.Title = "အထက်လူကြီးအမျိုးအစား";

                            WEmployee.Title = "ဝန်ထမ်း";
                            WTicket.Title = "လုပ်ငန်းစဉ် ";
                            WROType.Title = "အထက်လူကြီးအမျိုးအစား";

                            MEmployee.Title = "ဝန်ထမ်း";
                            MTicket.Title = "လုပ်ငန်းစဉ် ";
                            MROType.Title = "အထက်လူကြီးအမျိုးအစား";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabToday.Text = "Today (" + mVmlDailyLog.TodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlDailyLog.LastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlDailyLog.LastMonthList.Count + ")";
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
                    // Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
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
                    mVmlDailyLog.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlDailyLog.searchData("");
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
                var l_DAT_EMPLOYEE_DAILY_LOG = (DAT_EMPLOYEE_DAILY_LOG)e.SelectedItem;
                if (l_DAT_EMPLOYEE_DAILY_LOG != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_DAILY_LOG.Ask) ;
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
                var l_DAT_EMPLOYEE_DAILY_LOG = (DAT_EMPLOYEE_DAILY_LOG)e.SelectedItem;
                if (l_DAT_EMPLOYEE_DAILY_LOG != null)
                {
                    if (!Common.bindMenu("hcm-daily-log-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmDailyLogSet", MenuUrl = "hcm-daily-log-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_DAILY_LOG.Ask);
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
                mVmlDailyLog.getEmployeeDailyLog();
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
                //mVmlDailyLog.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}