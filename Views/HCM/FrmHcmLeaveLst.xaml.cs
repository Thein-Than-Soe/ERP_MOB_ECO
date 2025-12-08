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
    public partial class FrmHcmLeaveLst : ContentView
    {

        #region "Declaring"
        VmlLeave mVmlLeave;
        #endregion
        #region "Constructor"
        public FrmHcmLeaveLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlLeave = new VmlLeave();
                mVmlLeave.mJSN_REQ_EMPLOYEE_LEAVE_TAKEN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlLeave.mJSN_REQ_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN.Add(new DAT_EMPLOYEE_LEAVE_TAKEN());
                mVmlLeave.getLeaveTaken();
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
                            TabSubmit.Text = "Submit (" + mVmlLeave.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLeave.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text = "Reject (" + mVmlLeave.LeaveTakenRejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlLeave.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlLeave.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text = "ငြင်းပယ် (" + mVmlLeave.LeaveTakenRejectList.Count + ")";

                            SName.Title = "ဝန်ထမ်း";
                            SRemain.Title = "ကျန်ရှိနေသေးသောခွင့်";
                            SLeave.Title = "ခွင့်";
                            SLeaveTaken.Title = "ယူထားပီးသောခွင့်";
                            SSD.Title = "စတင်သည့်ရက်စွဲ";
                            SED.Title = "ကုန်ဆုံးရက်";

                            ApName.Title = "ဝန်ထမ်း";
                            ApRemain.Title = "ကျန်ရှိနေသေးသောခွင့်";
                            ApLeave.Title = "ခွင့်";
                            ApLeaveTaken.Title = "ယူထားပီးသောခွင့်";
                            ApSD.Title = "စတင်သည့်ရက်စွဲ";
                            ApED.Title = "ကုန်ဆုံးရက်";

                            AName.Title = "ဝန်ထမ်း";
                            ARemain.Title = "ကျန်ရှိနေသေးသောခွင့်";
                            ALeave.Title = "ခွင့်";
                            ALeaveTaken.Title = "ယူထားပီးသောခွင့်";
                            ASD.Title = "စတင်သည့်ရက်စွဲ";
                            AED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlLeave.LeaveTakenSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLeave.LeaveTakenApprovedList.Count + ")";
                            TabAll.Text = "ငြင်းပယ် (" + mVmlLeave.LeaveTakenList.Count + ")";
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
                    mVmlLeave.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlLeave.searchData("");
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
                mVmlLeave.getLeaveTaken();
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
                //mVmlLeave.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}