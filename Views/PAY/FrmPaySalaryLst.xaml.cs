using CS.ERP.PL.HCM.DAT;
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
    public partial class FrmPaySalaryLst : ContentView
    {
        #region "Declaring"
        VmlPaySalary mVmlPaySalary;
        #endregion
        #region "Constructor"
        public FrmPaySalaryLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPaySalary = new VmlPaySalary();
                mVmlPaySalary.mREQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                //mVmlPaySalary.mREQ_AUTHORIZATION= new ERP.PL.SYS.REQ.REQ_AUTHORIZATION();
                mVmlPaySalary.getEmployeeSalary();
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
                            TabLastOne.Text = "Last 1 month (" + mVmlPaySalary.LastMonthList.Count + ")";
                            TabLastFour.Text = "Last 4 Month (" + mVmlPaySalary.LastMonth4List.Count + ")";
                            TabAll.Text = "All (" + mVmlPaySalary.ScheduleDetailList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabLastOne.Text = "လွန်ခဲ့သောတစ်လ(" + mVmlPaySalary.LastMonthList.Count + ")";
                            TabLastFour.Text = "လွန်ခဲ့သောလေးလ (" + mVmlPaySalary.LastMonth4List.Count + ")";
                            TabAll.Text = "အားလုံ (" + mVmlPaySalary.ScheduleDetailList.Count + ")";

                            ASchedule.Title = "အချိန်ဇယား";
                            ASalary.Title = "လစာ";

                            L4Schedule.Title = "အချိန်ဇယား";
                            L4Salary.Title = "လစာ";

                            LSchedule.Title = "အချိန်ဇယား";
                            LSalary.Title = "လစာ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabLastOne.Text = "Last 1 month (" + mVmlPaySalary.LastMonthList.Count + ")";
                            TabLastFour.Text = "Last 4 Month (" + mVmlPaySalary.LastMonth4List.Count + ")";
                            TabAll.Text = "All (" + mVmlPaySalary.ScheduleDetailList.Count + ")";
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
                    mVmlPaySalary.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPaySalary.searchData("");
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
                var l_DAT_EMPLOYEE = (DAT_EMPLOYEE)e.SelectedItem;
                if (l_DAT_EMPLOYEE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE.Ask) ;
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
                var l_DAT_EMPLOYEE = (DAT_EMPLOYEE)e.SelectedItem;
                if (l_DAT_EMPLOYEE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE.Ask);
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
                mVmlPaySalary.getEmployeeSalary();
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
                //mVmlPaySalary.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}