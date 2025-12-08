using Android.Telephony;
using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ATT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTEmployeeSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE mDAT_EMPLOYEE = new DAT_EMPLOYEE();
        private DAT_EMPLOYEE_RO mDAT_EMPLOYEE_RO = new DAT_EMPLOYEE_RO();
        //  private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        //public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();

        VmlEmployee mVmlEmployee;
        byte[] imageByteArr;
        string mRequest = "";
        string mResponse = "";
        #endregion
        #region"Constructor"
        public FrmATTEmployeeSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployee = new VmlEmployee();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlEmployee.mJSN_REQ_EMPLOYEE.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployee.mJSN_REQ_EMPLOYEE.DAT_EMPLOYEE.Add(new DAT_EMPLOYEE());
                // mVmlEmployee.loadClaim();
                // switchLanguage();


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }



        public FrmATTEmployeeSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployee = new VmlEmployee();
                mDAT_EMPLOYEE.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlEmployee.mJSN_REQ_EMPLOYEE.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployee.mJSN_REQ_EMPLOYEE.DAT_EMPLOYEE.Add(mDAT_EMPLOYEE);
                // mVmlEmployee.loadClaim();
                //  switchLanguage();
                //if (mVmlEmployee.EmployeeRo.Count > 0)
                //{
                //    mVmlEmployee.getEmployeeClaim();
                //}

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmATTEmployeeSet(string type, string name, string institde)
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

        public FrmATTEmployeeSet(DAT_EMPLOYEE argDAT_EMPLOYEE)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EMPLOYEE = argDAT_EMPLOYEE;
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

                if (!Common.bindMenu("att-employee-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTEmployeeList", MenuUrl = "att-employee-lst", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void ControlTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           
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
                //     mDAT_EMPLOYEE.StatusAsk = "6";
                // }

                // request.DAT_EMPLOYEE.Add(mDAT_EMPLOYEE);
                // string requestJson = JsonConvert.SerializeObject(request);
                //// IntercomService.ShowLoader();
                // var response = await SYSConfigService.wsCall(requestJson, SYSConfigService.wssaveAccess);
                // if (response != null)
                // {
                //     var responseData = JsonConvert.DeserializeObject<JSN_ACCESS>(response);
                //     if (responseData.Message.Code == "7")
                //     {
                //         var data = responseData.DAT_EMPLOYEE_LST;
                //         if (data != null && data.Count > 0)
                //         {
                //             mDAT_EMPLOYEE = data[0];
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
        


        private void TgrAttendanceType_Tapped(object sender, EventArgs e)
        {

            Navigation.ShowPopup(new FrmATTTypePopup());
        }

        private void TgrEffective_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmATTEffectivePopup());
        }

        private void TgrExemption_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmATTExemptionPopup());
        }

        private void TgrEmployeeShift_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmATTEmployeeShiftPopup());
        }

       

        private async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                (sender as Button).IsEnabled = false;
                //Stream stream = await DependencyService.Get<IPhotoPickerService>().GetImageStreamAsync();
                //if (stream != null)
                //{
                //  imageByteArr = Utility.GetImageBytes(stream);
                // image.Source = ImageSource.FromStream(() => ImageConverter.BytesToStream(imageByteArr));
                //}
                (sender as Button).IsEnabled = true;
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