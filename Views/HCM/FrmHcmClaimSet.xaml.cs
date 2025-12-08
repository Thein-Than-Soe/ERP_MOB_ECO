using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.ViewsModel.HCM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.HCM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmHcmClaimSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE_CLAIM mDAT_CLAIM = new DAT_EMPLOYEE_CLAIM();
        private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();
       
        VmlClaim mVmlClaim;
        string mRequest = "";
       string mResponse = "";
        #endregion
        #region"Constructor"
        public FrmHcmClaimSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlClaim = new VmlClaim();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Add(new DAT_EMPLOYEE_CLAIM());
                mVmlClaim.loadClaim();
                switchLanguage();
              
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public FrmHcmClaimSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlClaim = new VmlClaim();
                mDAT_CLAIM.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Add(mDAT_CLAIM);
                mVmlClaim.loadClaim();
                switchLanguage();
                if(mVmlClaim.EmployeeRo.Count > 0)
                {
                    mVmlClaim.getEmployeeClaim();
                }
                         
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        public FrmHcmClaimSet(DAT_EMPLOYEE_CLAIM argDAT_EMPLOYEE_CLAIM)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_CLAIM = argDAT_EMPLOYEE_CLAIM;
                BindDataToForm();
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
                            mDAT_EMPLOYEE= mVmlClaim.EmployeeRo;                         
                           
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entClaimAmount.Placeholder = "ရယူမည့်ပမာဏထည့်သွင်းပါ";
                            entClaimReason.Text = "ရယူမည့် အကြောင်းပြချက် ထည့်သွင်းပါ";
                        }
                        break;
                    default:
                        {
                            entClaimAmount.Placeholder = "ရယူမည့်ပမာဏထည့်သွင်းပါ";
                            entClaimReason.Text = "ရယူမည့် အကြောင်းပြချက် ထည့်သွင်းပါ";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void BindDataToObject()
        {
            try
            {
                //var selectedEmployee = pikEmployee.SelectedItem as Variable;
               // mDAT_CLAIM.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;
                mDAT_CLAIM.ClaimReason = entClaimReason.Text.Trim();
                mDAT_CLAIM.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_CLAIM.Remark = entRemark.Text.Trim();
                mDAT_CLAIM.ClaimDate = startDatePicker.Date.ToUniversalTime().ToString();
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlClaim.mJSN_REQ_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Add(mDAT_CLAIM);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void BindDataToForm()
        {
            try
            {
                mDAT_CLAIM.ClaimReason = entClaimReason.Text.Trim();
                mDAT_CLAIM.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_CLAIM.Remark = entRemark.Text.Trim();
                mDAT_CLAIM.ClaimDate = startDatePicker.Date.ToString();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public void ClearFormData()
        {
            try
            {
                mDAT_CLAIM = new DAT_EMPLOYEE_CLAIM();
                entClaimAmount.Text = "";
                //entName.Text = "";
                //entDescription.Text = "";
                //entMobileCode.Text = "";
                //entScheme.Text = "";
                //entSuperscheme.Text = "";
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void loadData()
        {
                       
           pikEmployee.ItemsSource = mJSN_LOAD_EMPLOYEE_CLAIM.DAT_EMPLOYEE_RO;
           pikEmployee.SelectedIndex = mVmlClaim.getIndexByTypeAsk(mJSN_LOAD_EMPLOYEE_CLAIM.DAT_EMPLOYEE_RO);
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
            try
            {
                RES_CONTROL control = (RES_CONTROL)(((Xamarin.Forms.Frame)sender).BindingContext);
                if (control.link.Equals("btnSave_onClick"))
                {
                    if (mDAT_CLAIM.Ask != "6")
                    {
                        mDAT_CLAIM.ApproveStatusAsk = "1";
                    }                   

                    BindDataToObject();
                    mVmlClaim.saveEmployeeClaim();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_CLAIM.StatusAsk = "6";
                    BindDataToObject();
                    mVmlClaim.saveEmployeeClaim();
                }
                else if (control.link.Equals("btnNew_onClick"))
                {
                    ClearFormData();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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
                //     mDAT_CLAIM.StatusAsk = "6";
                // }

                // request.DAT_EMPLOYEE_CLAIM.Add(mDAT_CLAIM);
                // string requestJson = JsonConvert.SerializeObject(request);
                //// IntercomService.ShowLoader();
                // var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wssaveAccess);
                // if (response != null)
                // {
                //     var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
                //     if (responseData.Message.Code == "7")
                //     {
                //         var data = responseData.DAT_EMPLOYEE_CLAIM_LST;
                //         if (data != null && data.Count > 0)
                //         {
                //             mDAT_CLAIM = data[0];
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
                ClearFormData();
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
                ClearFormData();
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
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void TgrControl_Tapped(object sender, EventArgs e)
        {
            try
            {
                ClearFormData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        #endregion          
    }
}