using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ATT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTEmployeeInOutLst : ContentView
    {
        #region "Declaring"
        VmlEmployeeInOut mVmlEmployeeInOut;
        #endregion
        #region "Constructor"
        public FrmATTEmployeeInOutLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployeeInOut = new VmlEmployeeInOut();
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Add(new DAT_EMPLOYEE_IN_OUT());
                mVmlEmployeeInOut.getEmployeeInOut();
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
                            TabToday.Text = "Today (" + mVmlEmployeeInOut.EmployeeTodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlEmployeeInOut.EmployeeLastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlEmployeeInOut.EmployeeLastMonthList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabToday.Text = "တင်ပြချက် (" + mVmlEmployeeInOut.EmployeeTodayList.Count + ")";
                            TabLastWeek.Text = "အတည်ပြုချက် (" + mVmlEmployeeInOut.EmployeeLastWeekList.Count + ")";
                            TabLastMonth.Text = "ငြင်းပယ်ချက် (" + mVmlEmployeeInOut.EmployeeLastMonthList.Count + ")";

                            TEmployee.Title = "ဝန်ထမ်း";
                            Tfinger.Title = "လက်ဗွေရာ";
                            TType.Title = "တက်ရောက်သူ အမျိုးအစား";
                            TSD.Title = "စတင်သည့်ရက်စွဲ";

                            WEmployee.Title = "ဝန်ထမ်း";
                            Wfinger.Title = "လက်ဗွေရာ";
                            WType.Title = "တက်ရောက်သူ အမျိုးအစား";
                            WSD.Title = "စတင်သည့်ရက်စွဲ";

                            LEmployee.Title = "ဝန်ထမ်း";
                            Lfinger.Title = "လက်ဗွေရာ";
                            LType.Title = "တက်ရောက်သူ အမျိုးအစား";
                            LSD.Title = "စတင်သည့်ရက်စွဲ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabToday.Text = "Today (" + mVmlEmployeeInOut.EmployeeTodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlEmployeeInOut.EmployeeLastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlEmployeeInOut.EmployeeLastMonthList.Count + ")";
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
                if (!Common.bindMenu("att-employee-inout-set"))
                {
                    // Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("att-employee-inout-set"))
                //{
                //    Common.routeMenu("att-employee-inout-set", "Access Entry");
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
                    mVmlEmployeeInOut.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEmployeeInOut.searchData("");
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
                var l_DAT_EMPLOYEE_IN_OUT = (DAT_EMPLOYEE_IN_OUT)e.SelectedItem;
                if (l_DAT_EMPLOYEE_IN_OUT != null)
                {
                    if (!Common.bindMenu("att-employee-inout-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_IN_OUT.Ask);


                    //if (Common.bindMenu("att-employee-inout-set"))
                    //{
                    //    Common.routeMenuStr("att-employee-inout-set", "Access Entry", l_DAT_EMPLOYEE_IN_OUT.Ask);
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
                var l_DAT_EMPLOYEE_IN_OUT = (DAT_EMPLOYEE_IN_OUT)e.SelectedItem;
                if (l_DAT_EMPLOYEE_IN_OUT != null)
                {
                    if (!Common.bindMenu("att-employee-inout-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_IN_OUT.Ask);


                    //if (Common.bindMenu("att-employee-inout-set"))
                    //{
                    //    Common.routeMenuStr("att-employee-inout-set", "Access Entry", l_DAT_EMPLOYEE_IN_OUT.Ask);
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
                if (!Common.bindMenu("att-employee-inout-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("att-employee-inout-set"))
                //{
                //    Common.routeMenu("att-employee-inout-set", "Access Entry");
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
                mVmlEmployeeInOut.getEmployeeInOut();
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
                //mVmlEmployeeInOut.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}