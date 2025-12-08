using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.HCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.HCM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmHcmApplicantPersonalPopup : Popup
    {
        #region "Declaring"
        VmlApplicant mVmlApplicant;
        #endregion
        #region "Constructor"
        public FrmHcmApplicantPersonalPopup()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlApplicant = new VmlApplicant();
                mVmlApplicant.mJSN_REQ_APPLICANT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlApplicant.mJSN_REQ_APPLICANT.DAT_APPLICANT.Add(new DAT_APPLICANT());
                mVmlApplicant.loadApplicant();
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
                           
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";


                            Designation.Title = "အမည်";
                            JD.Title = "လျှောက်ထားသောရာထူး";
                            Status.Title = "လျှောက်ထားသူခေါ်ယူမှု";

                           
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                           
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
     
        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (entSearch.Text != null && entSearch.Text != "")
                {
                    mVmlApplicant.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlApplicant.searchData("");
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
                    if (!Common.bindMenu("hcm-applicants-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "3", Text = "FrmHcmApplicantSet", MenuUrl = "hcm-applicants-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    //Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_JOB_VACANCY);

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }         

        #endregion
    }
}