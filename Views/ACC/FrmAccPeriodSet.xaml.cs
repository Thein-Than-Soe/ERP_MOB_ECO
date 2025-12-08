using CS.ERP.PL.ACC.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.ACC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAccPeriodSet : ContentView
    {
        #region"Declaring"
        private DAT_ACCOUNT_PERIOD mDAT_ACCOUNT_PERIOD = new DAT_ACCOUNT_PERIOD();
       // private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
       // public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();

        VmlAccountPeriod mVmlAccountPeriod;
        string mRequest = "";
        string mResponse = "";
        #endregion
        #region"Constructor"
        public FrmAccPeriodSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAccountPeriod = new VmlAccountPeriod();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Add(new DAT_ACCOUNT_PERIOD());
                mVmlAccountPeriod.loadAccountPeriod();


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmAccPeriodSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAccountPeriod = new VmlAccountPeriod();
                mDAT_ACCOUNT_PERIOD.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Add(mDAT_ACCOUNT_PERIOD);
                mVmlAccountPeriod.getAccountPeriod();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmAccPeriodSet(DAT_ACCOUNT_PERIOD argDAT_ACCOUNT_PERIOD)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_ACCOUNT_PERIOD = argDAT_ACCOUNT_PERIOD;
                BindDataToForm();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
        #region"Private Method"     

        private void BindDataToObject()
        {
            try
            {
                //var selectedEmployee = pikEmployee.SelectedItem as Variable;
                // mDAT_ACCOUNT_PERIOD.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;
                mDAT_ACCOUNT_PERIOD.AccountPeriodCode_0_50 = entCode.Text.Trim();
                mDAT_ACCOUNT_PERIOD.AccountPeriodName_0_255 = entName.Text.Trim();
                mDAT_ACCOUNT_PERIOD.Remark = entRemark.Text.Trim();
                mDAT_ACCOUNT_PERIOD.FromDate = fromDatePicker.Date.ToUniversalTime().ToString();
                mDAT_ACCOUNT_PERIOD.ToDate = toDatePicker.Date.ToUniversalTime().ToString();
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccountPeriod.mJSN_REQ_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Add(mDAT_ACCOUNT_PERIOD);
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
                mDAT_ACCOUNT_PERIOD.AccountPeriodCode_0_50 = entCode.Text.Trim();
                mDAT_ACCOUNT_PERIOD.AccountPeriodName_0_255 = entName.Text.Trim();
                mDAT_ACCOUNT_PERIOD.AccountPeriodDescription_0_500 = entDescription.Text.Trim();
                mDAT_ACCOUNT_PERIOD.Remark = entRemark.Text.Trim();
                mDAT_ACCOUNT_PERIOD.FromDate = fromDatePicker.Date.ToString();
                mDAT_ACCOUNT_PERIOD.ToDate = toDatePicker.Date.ToString();
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
                mDAT_ACCOUNT_PERIOD = new DAT_ACCOUNT_PERIOD();
                entCode.Text ="";
                entName.Text ="";
                entDescription.Text ="";
                fromDatePicker.Date = DateTime.Today;
                toDatePicker.Date = DateTime.Today;
                //entSuperscheme.BadgeText ="";
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void loadData()
        {

            //pikEmployee.ItemsSource = mJSN_LOAD_EMPLOYEE_CLAIM.DAT_EMPLOYEE_RO;
            //pikEmployee.SelectedIndex = mVmlAccountPeriod.getIndexByTypeAsk(mJSN_LOAD_EMPLOYEE_CLAIM.DAT_EMPLOYEE_RO);
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
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmACCPeriodLst", MenuUrl = "hcm-claim-lst", logoImg = "" };
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
            try
            {
                RES_CONTROL control = (RES_CONTROL)(((Microsoft.Maui.Controls.Frame)sender).BindingContext);
                if (control.link.Equals("btnSave_onClick"))
                {
                    if (mDAT_ACCOUNT_PERIOD.Ask != "6")
                    {
                        BindDataToObject();
                        mVmlAccountPeriod.saveAccountPeriod();
                    }

                 
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_ACCOUNT_PERIOD.StatusAsk = "6";
                    BindDataToObject();
                    mVmlAccountPeriod.saveAccountPeriod();
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