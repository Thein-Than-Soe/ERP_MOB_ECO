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
    public partial class FrmHcmApplicantEvaluationLst : ContentView
    {
        #region "Declaring"
        VmlApplicantEvaluation mVmlApplicantEvaluation;
        #endregion
        #region "Constructor"
        public FrmHcmApplicantEvaluationLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlApplicantEvaluation = new VmlApplicantEvaluation();
                mVmlApplicantEvaluation.mJSN_REQ_APPLICANT_EVALUATION.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlApplicantEvaluation.mJSN_REQ_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION_DETAIL.Add(new DAT_APPLICANT_EVALUATION_DETAIL());
                mVmlApplicantEvaluation.getApplicantEvaluation();
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
                            TabTop3.Text = "Top3 (" + mVmlApplicantEvaluation.Top3List.Count + ")";
                            TabTop5.Text = "Top5 (" + mVmlApplicantEvaluation.Top5List.Count + ")";
                            TabAll.Text = "All (" + mVmlApplicantEvaluation.ApplicantEvaluationList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabTop3.Text = "ထိပ်ဆုံး ၃ ဦး (" + mVmlApplicantEvaluation.Top3List.Count + ")";
                            TabTop5.Text = "ထိပ်ဆုံး ၅ ဦး (" + mVmlApplicantEvaluation.Top5List.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlApplicantEvaluation.ApplicantEvaluationList.Count + ")";

                            AApplicant.Title = "လျှောက်ထားသူအမည်";
                            ADesignation.Title = "ရာထူး";
                            ATotalRate.Title = "စုစုပေါင်းနှုန်း";
                            AEvaluationForm.Title = "အကဲဖြတ်ပုံစံ";

                            Top3Applicant.Title = "လျှောက်ထားသူအမည်";
                            Top3Designation.Title = "ရာထူး";
                            Top3TotalRate.Title = "စုစုပေါင်းနှုန်း";
                            Top3EvaluationForm.Title = "အကဲဖြတ်ပုံစံ";

                            Top5Applicant.Title = "လျှောက်ထားသူအမည်";
                            Top5Designation.Title = "ရာထူး";
                            Top5TotalRate.Title = "စုစုပေါင်းနှုန်း";
                            Top5EvaluationForm.Title = "အကဲဖြတ်ပုံစံ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabTop3.Text = "Top3 (" + mVmlApplicantEvaluation.Top3List.Count + ")";
                            TabTop5.Text = "Top5 (" + mVmlApplicantEvaluation.Top5List.Count + ")";
                            TabAll.Text = "All (" + mVmlApplicantEvaluation.ApplicantEvaluationList.Count + ")";
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
                    mVmlApplicantEvaluation.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlApplicantEvaluation.searchData("");
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
                var l_DAT_APPLICANT_EVALUATION_DETAIL = (DAT_APPLICANT_EVALUATION_DETAIL)e.SelectedItem;
                if (l_DAT_APPLICANT_EVALUATION_DETAIL != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_APPLICANT_EVALUATION_DETAIL.Ask) ;
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
                var l_DAT_APPLICANT_EVALUATION_DETAIL = (DAT_APPLICANT_EVALUATION_DETAIL)e.SelectedItem;
                if (l_DAT_APPLICANT_EVALUATION_DETAIL != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_APPLICANT_EVALUATION_DETAIL.Ask);
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
                mVmlApplicantEvaluation.getApplicantEvaluation();
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
                //mVmlApplicantEvaluation.getApplicantEvaluationData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}