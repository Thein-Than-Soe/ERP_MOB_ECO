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
    public partial class FrmHcmVacancyLst : ContentView
    {
        #region "Declaring"
        VmlVacancy mVmlVacancy;
       
        #endregion
        #region "Constructor"
        public FrmHcmVacancyLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlVacancy = new VmlVacancy();
                mVmlVacancy.mJSN_REQ_JOB_VACANCY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlVacancy.mJSN_REQ_JOB_VACANCY.DAT_JOB_VACANCY.Add(new ERP.PL.HCM.DAT.DAT_JOB_VACANCY());
                mVmlVacancy.getJobVacancy();
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
                            TabActive.Text = "Active (" + mVmlVacancy.JobVacancyActiveList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlVacancy.JobVacancyClosedList.Count + ")";
                            TabAll.Text = "All (" + mVmlVacancy.JobVacancyList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "အသုံးပြု (" + mVmlVacancy.JobVacancyActiveList + ")";
                            TabClosed.Text = "ပိတ်သိမ်း (" + mVmlVacancy.JobVacancyClosedList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlVacancy.JobVacancyList.Count + ")";

                            ADesignation.Title = "ရာထူး";
                            AType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            AED.Title = "ကုန်ဆုံးရက်";

                            CDesignation.Title = "ရာထူး";
                            CType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            CED.Title = "ကုန်ဆုံးရက်";

                            AcDesignation.Title = "ရာထူး";
                            AcType.Title = "အလုပ်အကိုင်အမျိုးအစား";
                            AcED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlVacancy.JobVacancyActiveList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlVacancy.JobVacancyClosedList.Count + ")";
                            TabAll.Text = "All (" + mVmlVacancy.JobVacancyList.Count + ")";
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
                    mVmlVacancy.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlVacancy.searchData("");
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
                var l_DAT_JOB_VACANCY = (DAT_JOB_VACANCY)e.SelectedItem;
                if (l_DAT_JOB_VACANCY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_JOB_VACANCY.Ask) ;
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
                var l_DAT_JOB_VACANCY = (DAT_JOB_VACANCY)e.SelectedItem;
                if (l_DAT_JOB_VACANCY != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_JOB_VACANCY.Ask);
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
                mVmlVacancy.getJobVacancy();
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
                //mVmlVacancy.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}