using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.PAY;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.PAY;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.PAY
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPaySkimPopup : Popup
    {
        #region "Declaring"
        VmlEmployee mVmlEmployee;
      
         DAT_SKIM_DETAIL DAT_SKIM_DETAIL = new DAT_SKIM_DETAIL();

        #endregion
        #region "Constructor"
        public FrmPaySkimPopup()
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
                            //entSearch.Placeholder = "Search";

                        }
                        break;
                    case "2"://Myanmar
                        {
                            //entSearch.Placeholder = "ရှာဖွေရန်";


                            //Designation.Title = "အမည်";
                            //JD.Title = "လျှောက်ထားသောရာထူး";
                            //Status.Title = "လျှောက်ထားသူခေါ်ယူမှု";


                        }
                        break;
                    default:
                        {
                            //entSearch.Placeholder = "Search";

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
        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_DAT_SKIM_DETAIL = (DAT_SKIM_DETAIL)e.SelectedItem;
                if (l_DAT_SKIM_DETAIL != null)
                {
                    if (!Common.bindMenu("pay-employee-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "5", Text = "FrmPayEmployeeSet", MenuUrl = "pay-employee-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);
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


                if (Common.bindMenu("hcm-applicant-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "3", Text = "FrmHcmApplicantSet", MenuUrl = "hcm-applicant-set", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }

                // Common.routeMenu(Common.mCommon.SelectedMenu, DAT_ITEM_GROUP_DETAIL.InstituteName, DAT_ITEM_GROUP_DETAIL.CertificateName, DAT_ITEM_GROUP_DETAIL.InstituteName);



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