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
    public partial class FrmCrmEvaluationPeriodLst : ContentView
    {

        #region "Declaring"
        VmlEvaluationPeriod mVmlEvaluationPeriod;
        #endregion
        #region "Constructor"
        public FrmCrmEvaluationPeriodLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEvaluationPeriod = new VmlEvaluationPeriod();
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Add(new DAT_EVALUATION_PERIOD());
                mVmlEvaluationPeriod.getEvaluationPeriod();
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
                            TabOpen.Text = "Open (" + mVmlEvaluationPeriod.ApplicantEvaluationOpenList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlEvaluationPeriod.ApplicantEvaluationCloseList.Count + ")";
                            TabAll.Text = "All (" + mVmlEvaluationPeriod.ApplicantEvaluationList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.Text = "Open (" + mVmlEvaluationPeriod.ApplicantEvaluationOpenList.Count + ")";
                            TabClosed.Text = "ပိတ်သိမ်း (" + mVmlEvaluationPeriod.ApplicantEvaluationCloseList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlEvaluationPeriod.ApplicantEvaluationList.Count + ")";

                            OPeriod.Title = "အကဲဖြတ်ကာလ";
                            OEvaluationType.Title = "အကဲဖြတ်အမျိုးအစား";
                            OSD.Title = "စတင်သည့်ရက်စွဲ";
                            OED.Title = "ကုန်ဆုံးရက်";

                            CPeriod.Title = "အကဲဖြတ်ကာလ";
                            CEvaluationType.Title = "အကဲဖြတ်အမျိုးအစား";
                            CSD.Title = "စတင်သည့်ရက်စွဲ";
                            CED.Title = "ကုန်ဆုံးရက်";

                            APeriod.Title = "အကဲဖြတ်ကာလ";
                            AEvaluationType.Title = "အကဲဖြတ်အမျိုးအစား";
                            ASD.Title = "စတင်သည့်ရက်စွဲ";
                            AED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.Text = "Open (" + mVmlEvaluationPeriod.ApplicantEvaluationOpenList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlEvaluationPeriod.ApplicantEvaluationCloseList.Count + ")";
                            TabAll.Text = "All (" + mVmlEvaluationPeriod.ApplicantEvaluationList.Count + ")";
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
                if (!Common.bindMenu("crm-evaluation-period-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("crm-evaluation-period-set"))
                //{
                //    Common.routeMenu("crm-evaluation-period-set", "Access Entry");
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
                    mVmlEvaluationPeriod.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEvaluationPeriod.searchData("");
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
                    if (!Common.bindMenu("crm-evaluation-period-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCrmEvaluationPeriodSet", MenuUrl = "crm-evaluation-period-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EVALUATION_PERIOD.Ask);

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
                    if (!Common.bindMenu("crm-evaluation-period-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmCrmEvaluationPeriodSet", MenuUrl = "crm-evaluation-period-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EVALUATION_PERIOD.Ask);

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
                if (!Common.bindMenu("crm-evaluation-period-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("crm-evaluation-period-set"))
                //{
                //    Common.routeMenu("crm-evaluation-period-set", "Access Entry");
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
                mVmlEvaluationPeriod.getEvaluationPeriod();
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
                //mVmlEvaluationPeriod.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}