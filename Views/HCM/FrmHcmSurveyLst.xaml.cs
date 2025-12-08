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
    public partial class FrmHcmSurveyLst : ContentView
    {
        #region "Declaring"
        VmlSurvey mVmlSurvey;
        #endregion
        #region "Constructor"
        public FrmHcmSurveyLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSurvey = new VmlSurvey();
                mVmlSurvey.mJSN_REQ_EMPLOYEE_SURVEY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSurvey.mJSN_REQ_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY=new DAT_EMPLOYEE_SURVEY();
                mVmlSurvey.getSurvey();
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
                            TabOpen.Text = "Open (" + mVmlSurvey.SurveyOpenList.Count + ")";
                            TabSubmit.Text = "Submit (" + mVmlSurvey.SurveySubmittedList.Count + ")";
                            TabAll.Text = "All (" + mVmlSurvey.SurveyList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.Text = "Open (" + mVmlSurvey.SurveyOpenList.Count + ")";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlSurvey.SurveySubmittedList.Count + ")";
                            TabAll.Text = "အားလုံ (" + mVmlSurvey.SurveyList.Count + ")";

                            SName.Title = "ဝန်ထမ်း";
                            SSurvey.Title = "စစ်တမ်";
                            SDate.Title = "စစ်တမ်းရက်စွဲ";

                            OName.Title = "ဝန်ထမ်း";
                            OSurvey.Title = "စစ်တမ်";
                            ODate.Title = "စစ်တမ်းရက်စွဲ";

                            AName.Title = "ဝန်ထမ်း";
                            ASurvey.Title = "စစ်တမ်";
                            ADate.Title = "စစ်တမ်းရက်စွဲ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.Text = "Open (" + mVmlSurvey.SurveyOpenList.Count + ")";
                            TabSubmit.Text = "Submit (" + mVmlSurvey.SurveySubmittedList.Count + ")";
                            TabAll.Text = "All (" + mVmlSurvey.SurveyList.Count + ")";
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
                    mVmlSurvey.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSurvey.searchData("");
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
                var l_DAT_EMPLOYEE_SURVEY = (DAT_EMPLOYEE_SURVEY)e.SelectedItem;
                if (l_DAT_EMPLOYEE_SURVEY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_SURVEY.Ask) ;
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
                var l_DAT_EMPLOYEE_SURVEY = (DAT_EMPLOYEE_SURVEY)e.SelectedItem;
                if (l_DAT_EMPLOYEE_SURVEY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_SURVEY.Ask);
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
                mVmlSurvey.getSurvey();
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
                //mVmlSurvey.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}