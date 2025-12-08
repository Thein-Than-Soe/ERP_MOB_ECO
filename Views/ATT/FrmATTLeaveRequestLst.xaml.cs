using CS.ERP.PL.HCM.DAT;
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
    public partial class FrmATTLeaveRequestLst : ContentView
    {
        #region "Declaring"
        VmlLeaveRequest mVmlLeaveRequest;
        #endregion
        #region "Constructor"
        public FrmATTLeaveRequestLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlLeaveRequest = new VmlLeaveRequest();
                mVmlLeaveRequest.mJSN_REQ_EMPLOYEE_LEAVE_TAKEN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlLeaveRequest.mJSN_REQ_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN.Add(new DAT_EMPLOYEE_LEAVE_TAKEN());
              mVmlLeaveRequest.getLeaveTaken();
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
                            TabSubmit.Text = "Submit (" + mVmlLeaveRequest.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLeaveRequest.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text ="Reject  (" + mVmlLeaveRequest.LeaveTakenRejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlLeaveRequest.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlLeaveRequest.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text = "ငြင်းပယ် (" + mVmlLeaveRequest.LeaveTakenRejectList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SRemainLeave.Title = "ကျန်ရှိသောခွင့်";
                            SLeaveName.Title = "ခွင့်";
                            SLeaveTaken.Title = "ယူထားသောခွင့်";

                            ApEmployee.Title = "ဝန်ထမ်း";
                            ApRemainLeave.Title = "ကျန်ရှိသောခွင့်";
                            ApLeaveName.Title = "ခွင့်";
                            ApLeaveTaken.Title = "ယူထားသောခွင့်";

                            AEmployee.Title = "ဝန်ထမ်း";
                            ARemainLeave.Title = "ကျန်ရှိသောခွင့်";
                            ALeaveName.Title = "ခွင့်";
                            ALeaveTaken.Title = "ယူထားသောခွင့်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlLeaveRequest.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLeaveRequest.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text = "Reject (" + mVmlLeaveRequest.LeaveTakenRejectList.Count + ")";
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
                    mVmlLeaveRequest.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlLeaveRequest.searchData("");
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
                var l_DAT_EMPLOYEE_LEAVE_TAKEN = (DAT_EMPLOYEE_LEAVE_TAKEN)e.SelectedItem;
                if (l_DAT_EMPLOYEE_LEAVE_TAKEN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_LEAVE_TAKEN.Ask) ;
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
                var l_DAT_EMPLOYEE_LEAVE_TAKEN = (DAT_EMPLOYEE_LEAVE_TAKEN)e.SelectedItem;
                if (l_DAT_EMPLOYEE_LEAVE_TAKEN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_LEAVE_TAKEN.Ask);
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
               // mVmlLeaveRequest.getLeaveTaken();
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
                //mVmlLeaveRequest.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion


    }
}