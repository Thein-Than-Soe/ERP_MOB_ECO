using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.HCM;
using Rg.Plugins.Popup.Services;
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
    public partial class FrmHcmCertificatePopup : Popup
    {
        #region "Declaring"
        
        VmlApplicant mVmlApplicant;
        DAT_APPLICANT_CERTIFICATE DAT_APPLICANT_CERTIFICATE = new DAT_APPLICANT_CERTIFICATE();
        #endregion
        #region "Constructor"
        public FrmHcmCertificatePopup()
        {
            try
            {
                InitializeComponent();
               
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
      
        private void okClick(object sender, EventArgs e)
        {
            try
            {
                DAT_APPLICANT_CERTIFICATE DAT_APPLICANT_CERTIFICATE = new DAT_APPLICANT_CERTIFICATE();
                DAT_APPLICANT_CERTIFICATE.CertificateName = entName.Text.Trim();
                DAT_APPLICANT_CERTIFICATE.InstituteName = entInstitude.Text.Trim();
                if (Common.bindMenu("hcm-applicant-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "3", Text = "FrmHcmApplicantSet", MenuUrl = "hcm-applicant-set", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                
                Common.routeMenu(Common.mCommon.SelectedMenu, entName.Text.Trim(), entInstitude.Text.Trim(), entInstitude.Text.Trim());



            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void cancelClick(object sender, EventArgs e)
        {
            
                await PopupNavigation.PopAsync(true);
           

        }

        #endregion
    }
}