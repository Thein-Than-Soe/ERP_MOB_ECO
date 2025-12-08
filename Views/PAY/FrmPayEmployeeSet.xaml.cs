using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.PAY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.PAY
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPayEmployeeSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE mDAT_EMPLOYEE = new DAT_EMPLOYEE();
       // private DAT_EMPLOYEE_CERTIFICATE mDAT_EMPLOYEE_CERTIFICATE = new DAT_EMPLOYEE_CERTIFICATE();
        //  private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        //public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();

        VmlEmployee mVmlEmployee;
        byte[] imageByteArr;
        string mRequest = "";
        string mResponse = "";
        #endregion
        #region"Constructor"
        public FrmPayEmployeeSet()
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

        public FrmPayEmployeeSet(string argAsk)
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
                 switchLanguage();
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

        public FrmPayEmployeeSet(string type, string name, string institde)
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

        public FrmPayEmployeeSet(DAT_EMPLOYEE argDAT_EMPLOYEE)
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
                if (!Common.bindMenu("pay-employee-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmPayEmployeeSet", MenuUrl = "pay-employee-lst", logoImg = "" };
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

        private void TgrPayItemGroup_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmPayItemGroupPopup());
        }               
        private void TgrPaySkim_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmPayItemGroupPopup());
        }
        private void TgrOpening_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmPayOpeningPopup());
        }
        private void TgrRecurring_Tapped(object sender, EventArgs e)
        {
            Navigation.ShowPopup(new FrmPayRecurringPopup());
        }
     
        private async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                var pickResult = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Pick an image"
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