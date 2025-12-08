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
    public partial class FrmHcmAppraisalLst : ContentView
    {
        #region "Declaring"
        VmlAppraisal mVmlAppraisal;
        #endregion
        #region "Constructor"
        public FrmHcmAppraisalLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAppraisal = new VmlAppraisal();
                mVmlAppraisal.mJSN_REQ_EMPLOYEE_APPRAISAL.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAppraisal.mJSN_REQ_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL=new DAT_EMPLOYEE_APPRAISAL();
                mVmlAppraisal.getAppraisal();
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
                            TabOpen.Text = "Open (" + mVmlAppraisal.AppraisalOpenList.Count + ")";
                            TabSubmit.Text = "Submit (" + mVmlAppraisal.AppraisalSubmitList.Count + ")";
                            TabAll.Text = "All (" + mVmlAppraisal.AppraisalList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.Text = "Open (" + mVmlAppraisal.AppraisalOpenList.Count + ")";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlAppraisal.AppraisalSubmitList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlAppraisal.AppraisalList.Count + ")";

                            AEmployee.Title = "ဝန်ထမ်း";
                            AAppraisalForm.Title = "အကဲဖြတ်ပုံစံ";
                            ARO.Title = "အထက်လူကြီး";

                            OEmployee.Title = "ဝန်ထမ်း";
                            OAppraisalForm.Title = "အကဲဖြတ်ပုံစံ";
                            ORO.Title = "အထက်လူကြီး";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SAppraisalForm.Title = "အကဲဖြတ်ပုံစံ";
                            SRO.Title = "အထက်လူကြီး";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.Text = "Open (" + mVmlAppraisal.AppraisalOpenList.Count + ")";
                            TabSubmit.Text = "Submit (" + mVmlAppraisal.AppraisalSubmitList.Count + ")";
                            TabAll.Text = "All (" + mVmlAppraisal.AppraisalList.Count + ")";
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
                    mVmlAppraisal.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlAppraisal.searchData("");
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
                var l_DAT_EMPLOYEE_APPRAISAL_DETAIL = (DAT_EMPLOYEE_APPRAISAL_DETAIL)e.SelectedItem;
                if (l_DAT_EMPLOYEE_APPRAISAL_DETAIL != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_APPRAISAL_DETAIL.Ask) ;
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
                var l_DAT_EMPLOYEE_APPRAISAL_DETAIL = (DAT_EMPLOYEE_APPRAISAL_DETAIL)e.SelectedItem;
                if (l_DAT_EMPLOYEE_APPRAISAL_DETAIL != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_APPRAISAL_DETAIL.Ask);
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
                mVmlAppraisal.getAppraisal();
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
                //mVmlAppraisal.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}