using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.CRM;
using CS.ERP_MOB.ViewsModel.CRM;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;

using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.Views.CRM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCrmEvaluationSummaryLst : ContentView
    {

        #region "Declaring"
        VmlEvaluationSummary mVmlEvaluationSummary;
        #endregion
        #region "Constructor"
        public FrmCrmEvaluationSummaryLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEvaluationSummary = new VmlEvaluationSummary();
                mVmlEvaluationSummary.mJSN_REQ_TICKET_SUMMARY_DTL.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEvaluationSummary.mJSN_REQ_TICKET_SUMMARY_DTL.DAT_EVALUATION_PERIOD= new DAT_EVALUATION_PERIOD();
                mVmlEvaluationSummary.getEvaluationSummary();
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
                            TabOpen.Text = "Open (" + mVmlEvaluationSummary.SummaryOpenList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlEvaluationSummary.SummaryClosedList.Count + ")";
                            TabAll.Text = "All (" + mVmlEvaluationSummary.SummaryList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.Text = "အသုံးပြု (" + mVmlEvaluationSummary.SummaryOpenList.Count + ")";
                            TabClosed.Text = "အသုံးမပြု (" + mVmlEvaluationSummary.SummaryClosedList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlEvaluationSummary.SummaryList.Count + ")";

                            OPeriod.Title = "အကဲဖြတ်ကာလ";
                            OGrade.Title = "အမှတ်";
                            OTicket.Title = "စုစုပေါင်းလုပ်ငန်းစဉ်";
                            OPoint.Title = "စုစုပေါင်းအမှတ်";

                            CPeriod.Title = "အကဲဖြတ်ကာလ";
                            CGrade.Title = "အမှတ်";
                            CTicket.Title = "စုစုပေါင်းလုပ်ငန်းစဉ်";
                            CPoint.Title = "စုစုပေါင်းအမှတ်";

                            APeriod.Title = "အကဲဖြတ်ကာလ";
                            AGrade.Title = "အမှတ်";
                            ATicket.Title = "စုစုပေါင်းလုပ်ငန်းစဉ်";
                            APoint.Title = "စုစုပေါင်းအမှတ်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.Text = "Active (" + mVmlEvaluationSummary.SummaryOpenList.Count + ")";
                            TabClosed.Text = "Inactive (" + mVmlEvaluationSummary.SummaryClosedList.Count + ")";
                            TabAll.Text = "All (" + mVmlEvaluationSummary.SummaryList.Count + ")";
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
                    mVmlEvaluationSummary.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEvaluationSummary.searchData("");
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
                var l_DAT_EVALUATION_PERIOD = (DAT_EVALUATION_PERIOD)e.SelectedItem;
                if (l_DAT_EVALUATION_PERIOD != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EVALUATION_PERIOD.Ask) ;
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
                var l_DAT_EVALUATION_PERIOD = (DAT_EVALUATION_PERIOD)e.SelectedItem;
                if (l_DAT_EVALUATION_PERIOD != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EVALUATION_PERIOD.Ask);
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
                mVmlEvaluationSummary.getEvaluationSummary();
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
                //mVmlEvaluationSummary.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }


}