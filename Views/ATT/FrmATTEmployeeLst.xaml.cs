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


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTEmployeeLst : ContentView
    {

        #region "Declaring"
        VmlEmployee mVmlEmployee;
        #endregion
        #region "Constructor"
        public FrmATTEmployeeLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployee = new VmlEmployee();
                mVmlEmployee.mJSN_REQ_EMPLOYEE.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployee.mJSN_REQ_EMPLOYEE.DAT_EMPLOYEE.Add(new DAT_EMPLOYEE());
                mVmlEmployee.getEmployee();
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
                            TabSelf.Text = "Self (" + mVmlEmployee.SelfEmployeeList.Count + ")";
                            TabRO.Text = "RO (" + mVmlEmployee.EmployeeROList.Count + ")";
                            TabAll.Text = "All (" + mVmlEmployee.EmployeeList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSelf.Text = "ကိုယ်တိုင် (" + mVmlEmployee.SelfEmployeeList.Count + ")";
                            TabRO.Text = "အထက်လူကြီ (" + mVmlEmployee.EmployeeROList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlEmployee.EmployeeList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SDesignation.Title = "ရာထူး";
                            SType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            SDate.Title = "အငှားရက်စွဲ";

                            REmployee.Title = "ဝန်ထမ်း";
                            RDesignation.Title = "ရာထူး";
                            RType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            RDate.Title = "အငှားရက်စွဲ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            ADesignation.Title = "ရာထူး";
                            AType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            ADate.Title = "အငှားရက်စွဲ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSelf.Text = "Self (" + mVmlEmployee.SelfEmployeeList.Count + ")";
                            TabRO.Text = "RO (" + mVmlEmployee.EmployeeROList.Count + ")";
                            TabAll.Text = "All (" + mVmlEmployee.EmployeeList.Count + ")";
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
                    mVmlEmployee.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEmployee.searchData("");
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
                mVmlEmployee.getEmployee();
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
                //mVmlEmployee.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}