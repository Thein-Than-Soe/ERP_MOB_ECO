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
    public partial class FrmHcmClaimLst : ContentView
    {
        #region "Declaring"
        VmlClaim mVmlClaim;
        #endregion
        #region "Constructor"
        public FrmHcmClaimLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlClaim = new VmlClaim();
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Add(new DAT_EMPLOYEE_CLAIM());
                mVmlClaim.getEmployeeClaim();
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
                            TabSubmit.Text = "Submit (" + mVmlClaim.EmployeeClaimSubmitList.Count + ")";
                            TabApproved.Text = "Approve (" + mVmlClaim.EmployeeClaimApprovedList.Count + ")";
                            TabAll.Text = "Reject (" + mVmlClaim.EmployeeClaimList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlClaim.EmployeeClaimSubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlClaim.EmployeeClaimApprovedList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlClaim.EmployeeClaimList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SClaimType.Title = "ရရှိမှုအမျိုးအစား";
                            SDate.Title = "ရရှိသောရက်စွဲ";
                            SAmount.Title = "ရှိသောပမာဏ";

                            ApEmployee.Title = "ဝန်ထမ်း";
                            ApClaimType.Title = "ရရှိမှုအမျိုးအစား";
                            ApDate.Title = "ရရှိသောရက်စွဲ";
                            ApAmount.Title = "ရှိသောပမာဏ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            AClaimType.Title = "ရရှိမှုအမျိုးအစား";
                            ADate.Title = "ရရှိသောရက်စွဲ";
                            AAmount.Title = "ရှိသောပမာဏ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlClaim.EmployeeClaimSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlClaim.EmployeeClaimApprovedList.Count + ")";
                            TabAll.Text = "Reject (" + mVmlClaim.EmployeeClaimList.Count + ")";
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
                    mVmlClaim.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlClaim.searchData("");
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
                var l_DAT_EMPLOYEE_CLAIM = (DAT_EMPLOYEE_CLAIM)e.SelectedItem;
                if (l_DAT_EMPLOYEE_CLAIM != null)
                {
                    if (!Common.bindMenu("hcm-claim-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "hcm-claim-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_CLAIM.Ask);
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
                var l_DAT_EMPLOYEE_CLAIM = (DAT_EMPLOYEE_CLAIM)e.SelectedItem;
                if (l_DAT_EMPLOYEE_CLAIM != null)
                {
                    if (!Common.bindMenu("hcm-claim-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmClaimSet", MenuUrl = "hcm-claim-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_CLAIM.Ask);

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrCardView_Tapped(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_DAT_EMPLOYEE_CLAIM = (DAT_EMPLOYEE_CLAIM)e.SelectedItem;
                if (l_DAT_EMPLOYEE_CLAIM != null)
                {
                    if (!Common.bindMenu("hcm-claim-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmClaimSet", MenuUrl = "hcm-claim-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_CLAIM.Ask);

                }
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
                mVmlClaim.getEmployeeClaim();
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
                //mVmlClaim.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}