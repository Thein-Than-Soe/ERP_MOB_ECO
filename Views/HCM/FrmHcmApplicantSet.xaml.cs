using Android.Telephony;
using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.HCM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.HCM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmHcmApplicantSet : ContentView
    {
        #region"Declaring"
        private DAT_APPLICANT mDAT_APPLICANT = new DAT_APPLICANT();
        private DAT_APPLICANT_CERTIFICATE mDAT_APPLICANT_CERTIFICATE = new DAT_APPLICANT_CERTIFICATE();
        private DAT_JOB_VACANCY mDAT_JOB_VACANCY = new DAT_JOB_VACANCY();
        //  private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        //public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();

        VmlApplicant mVmlApplicant;
        byte[] imageByteArr;
        string mRequest = "";
        string mResponse = "";
        #endregion
        #region"Constructor"
        public FrmHcmApplicantSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlApplicant = new VmlApplicant();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlApplicant.mJSN_REQ_APPLICANT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlApplicant.mJSN_REQ_APPLICANT.DAT_APPLICANT.Add(new DAT_APPLICANT());
               // mVmlApplicant.loadClaim();
               // switchLanguage();


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }



        public FrmHcmApplicantSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlApplicant = new VmlApplicant();
                mDAT_APPLICANT.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlApplicant.mJSN_REQ_APPLICANT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlApplicant.mJSN_REQ_APPLICANT.DAT_APPLICANT.Add(mDAT_APPLICANT);
               // mVmlApplicant.loadClaim();
                switchLanguage();
              
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
     
        public FrmHcmApplicantSet(string type, string name, string institde)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                //BindDataToForm();
                entName.Text = type;
                entEmail.Text = name;
                entMobile.Text = institde; 
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmHcmApplicantSet(DAT_JOB_VACANCY argDAT_JOB_VACANCY)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_JOB_VACANCY = argDAT_JOB_VACANCY;
                // BindDataToForm();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        #endregion
        #region"Private Method"     
        public void switchLanguage()
        {
            try
            {
                switch (Common.mCommon.UserSetting.LanguageAsk)
                {
                    case "1"://English
                        {
                            
                        }
                        break;
                    case "2"://Myanmar
                        {
                           
                        }
                        break;
                    default:
                        {
                            
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
        #region"Publich Method"
        #endregion
        #region""
        #endregion
        #region"Event"
        private void BackTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                if (!Common.bindMenu("access-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmClaimLst", MenuUrl = "hcm-claim-lst", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                ////ContentView contentView = new AccessListPage();
                ////RoutingModel routemodel = new RoutingModel("Access List", contentView);
                ////MessagingCenter.Send<Application, RoutingModel>(Application.Current, "ViewChange", routemodel);
                //Common.routeMenu("access-lst", "Access List");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void ControlTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //try
            //{
            //    RES_CONTROL control = (RES_CONTROL)(((Xamarin.Forms.Frame)sender).BindingContext);
            //    if (control.link.Equals("btnSave_onClick"))
            //    {
            //        if (mDAT_APPLICANT.Ask != "6")
            //        {
            //            mDAT_APPLICANT.ApproveStatusAsk = "1";
            //        }

            //        BindDataToObject();
            //        mVmlApplicant.saveEmployeeClaim();
            //    }
            //    else if (control.link.Equals("btnDelete_onClick"))
            //    {
            //        mDAT_APPLICANT.StatusAsk = "6";
            //        BindDataToObject();
            //        mVmlApplicant.saveEmployeeClaim();
            //    }
            //    else if (control.link.Equals("btnNew_onClick"))
            //    {
            //        ClearFormData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex.InnerException;
            //}
        }

        private async void btnSave_onClickAsync(string action)
        {
            try
            {
                // JSN_REQ_ACCESS request = new JSN_REQ_ACCESS();
                // REQ_AUTHORIZATION authData = Common.mCommon.REQ_AUTHORIZATION;

                // //Crypto crypto = new Crypto();
                // //authData.UserPassword = crypto.Decrypt(authData.UserPassword);
                // authData.UserID = "yi";
                // authData.UserPassword = "Artsign@123";
                // authData.TransactionName = "1";
                // request.REQ_AUTHORIZATION = authData;

                // if (action.Equals("DEL"))
                // {
                //     mDAT_APPLICANT.StatusAsk = "6";
                // }

                // request.DAT_APPLICANT.Add(mDAT_APPLICANT);
                // string requestJson = JsonConvert.SerializeObject(request);
                //// IntercomService.ShowLoader();
                // var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wssaveAccess);
                // if (response != null)
                // {
                //     var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
                //     if (responseData.Message.Code == "7")
                //     {
                //         var data = responseData.DAT_APPLICANT_LST;
                //         if (data != null && data.Count > 0)
                //         {
                //             mDAT_APPLICANT = data[0];
                //             BindDataToForm();
                //         }

                //         if (action.Equals("DEL"))
                //         {
                //             ClearFormData();
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Delete Success");
                //         }
                //         else if (action.Equals("VOID"))
                //         {
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Void Success");
                //         }
                //         else
                //         {
                //             MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Save Success");
                //         }

                //     }
                //     else
                //     {
                //         MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Fail");
                //     }

                //     //await IntercomService.CloseLoader();

                // }
                // else
                // {
                //     MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "Server Err");
                //     //await IntercomService.CloseLoader();
                // }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void NewTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
               // ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void Input_Focused(object sender, FocusEventArgs e)
        {
            try
            {//AbsoluteLayout.SetLayoutBounds(FooterLayout, new Rectangle(0, 0.5, 1, 40));
             //AbsoluteLayout.SetLayoutFlags(FooterLayout, AbsoluteLayoutFlags.PositionProportional);
             //AbsoluteLayout.SetLayoutFlags(FooterLayout, AbsoluteLayoutFlags.WidthProportional);
             //RootLayout.TranslationY = -250;
                RootLayout.Margin = new Thickness(0, 0, 0, 250);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void Input_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                //RootLayout.TranslationY = 0;
                RootLayout.Margin = new Thickness(0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrBack_Tapped(object sender, EventArgs e)
        {
            try
            {
              //  ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
               // ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void TgrJobVacancy_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmApplicantPersonalPopup()); 
            
        }

        private void TgrCertificate_Tapped(object sender, EventArgs e)
        {
           
            Navigation.ShowPopup(new FrmHcmCertificatePopup());
           // Navigation.PushAsync(new FrmHcmCertificate());
        }

        private void TgrContact_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmContactPopup());
        }

        private void TgrWorkingExperience_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmWorkingExperiencePopup());
        }

        private void TgrEducation_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmEducationPopup());
        }

        private void TgrHobby_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmHobbyPopup());
        }

        private void TgrInternship_Tapped(object sender, EventArgs e)
        {
           Navigation.ShowPopup(new FrmHcmInternPopup());
        }

        private void TgrLanguageSkill_Tapped(object sender, EventArgs e)
        {
           Navigation.ShowPopup(new FrmHcmLanguagePopup());
        }

        private void TgrQualification_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmOthereQualificationPopup());
        }

        private void TgrPhoto_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmPhotoPopup());
        }

        private void TgrSkill_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmSkillPopup());
        }

        private void TgrSocialAffais_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmSocialAffairsPopup());
        }
        private void TgrTraining_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmHcmTrainPopup());
        }

        private void colView_onSelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
               //var mJobVacancy = (DAT_JOB_VACANCY)colView.SelectedItem;
               // Navigation.ShowPopup(new FrmHcmApplicantPersonalPopup());

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                var pickResult = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images, PickerTitle = "Pick an image"
                });

                if (pickResult != null)
                {
                    var stream = await pickResult.OpenReadAsync();
                    image.Source = ImageSource.FromStream(() => stream);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void TgrControl_Tapped(object sender, EventArgs e)
        {
            try
            {
              //  ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        async void btnAdd_onClick(object sender, EventArgs e)
        {
            try
            {
               // await Navigation.PushAsync(new EN_VARIABLE_SET());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion          
    }
}