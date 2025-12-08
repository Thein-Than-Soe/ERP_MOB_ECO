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
    public partial class FrmATTEmployeeScheduleLst : ContentView
    {
        #region "Declaring"
        VmlEmployeeRoaster mVmlEmployeeRoaster;
        #endregion
        #region "Constructor"
        public FrmATTEmployeeScheduleLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployeeRoaster = new VmlEmployeeRoaster();
                mVmlEmployeeRoaster.mJSN_REQ_EMPLOYEE_ROASTER_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployeeRoaster.mJSN_REQ_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_ROASTER_JUN.Add(new DAT_EMPLOYEE_ROASTER_JUN());
                mVmlEmployeeRoaster.mJSN_REQ_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_SHIFT_JUN.Add(new DAT_EMPLOYEE_SHIFT_JUN());
                mVmlEmployeeRoaster.getEmployeeRoaster();
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
                            TabToday.Text = "Today (" + mVmlEmployeeRoaster.EmployeeRoasterTodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlEmployeeRoaster.EmployeeRoasterLastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlEmployeeRoaster.EmployeeRoasterLastMonthList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabToday.Text = "ဒီနေ့ (" + mVmlEmployeeRoaster.EmployeeRoasterTodayList.Count + ")";
                            TabLastWeek.Text = "ပြီးခဲ့သည့်အပတ်က (" + mVmlEmployeeRoaster.EmployeeRoasterLastWeekList.Count + ")";
                            TabLastMonth.Text = "ပြီးခဲ့သည့်လ (" + mVmlEmployeeRoaster.EmployeeRoasterLastMonthList.Count + ")";

                            TEmployee.Title = "ဝန်ထမ်း";
                            TShift.Title = "အဆိုင်း";
                            TSD.Title = "စတင်သည့်ရက်စွဲ";
                            TED.Title = "ကုန်ဆုံးရက်";

                            WEmployee.Title = "ဝန်ထမ်း";
                            WShift.Title = "အဆိုင်း";
                            WSD.Title = "စတင်သည့်ရက်စွဲ";
                            WED.Title = "ကုန်ဆုံးရက်";

                            MEmployee.Title = "ဝန်ထမ်း";
                            MShift.Title = "အဆိုင်း";
                            MSD.Title = "စတင်သည့်ရက်စွဲ";
                            MED.Title = "ကုန်ဆုံးရက်";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabToday.Text = "Today (" + mVmlEmployeeRoaster.EmployeeRoasterTodayList.Count + ")";
                            TabLastWeek.Text = "Last Week (" + mVmlEmployeeRoaster.EmployeeRoasterLastWeekList.Count + ")";
                            TabLastMonth.Text = "Last Month (" + mVmlEmployeeRoaster.EmployeeRoasterLastMonthList.Count + ")";
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
                    mVmlEmployeeRoaster.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEmployeeRoaster.searchData("");
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
                var l_DAT_EMPLOYEE_ROASTER_JUN = (DAT_EMPLOYEE_ROASTER_JUN)e.SelectedItem;
                if (l_DAT_EMPLOYEE_ROASTER_JUN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_ROASTER_JUN.Ask) ;
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
                var l_DAT_EMPLOYEE_ROASTER_JUN = (DAT_EMPLOYEE_ROASTER_JUN)e.SelectedItem;
                if (l_DAT_EMPLOYEE_ROASTER_JUN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_ROASTER_JUN.Ask);
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
                mVmlEmployeeRoaster.getEmployeeRoaster();
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
                //mVmlEmployeeRoaster.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}