using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ATT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTEmployeeInOutSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE_IN_OUT mDAT_EMPLOYEE_IN_OUT = new DAT_EMPLOYEE_IN_OUT();
       // private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlEmployeeInOut mVmlEmployeeInOut;
        #endregion
        #region"Constructor"
        public FrmATTEmployeeInOutSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployeeInOut = new VmlEmployeeInOut();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Add(new DAT_EMPLOYEE_IN_OUT());
                mVmlEmployeeInOut.loadEmployeeInOut();
               // loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmATTEmployeeInOutSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEmployeeInOut = new VmlEmployeeInOut();
                mDAT_EMPLOYEE_IN_OUT.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Add(mDAT_EMPLOYEE_IN_OUT);
                mVmlEmployeeInOut.getEmployeeInOut();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmATTEmployeeInOutSet(DAT_EMPLOYEE_IN_OUT argDAT_EMPLOYEE_IN_OUT)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EMPLOYEE_IN_OUT = argDAT_EMPLOYEE_IN_OUT;
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
                // mDAT_EMPLOYEE_IN_OUT.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                //mDAT_EMPLOYEE_IN_OUT.re = entClaimReason.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.SD = Checkin.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_IN_OUT.ED = Checkout.Date.ToUniversalTime().ToString();

                DateTime sDate = Checkin.Date.ToUniversalTime();
                DateTime eDate = Checkout.Date.ToUniversalTime();
                TimeSpan timespan = eDate.Subtract(sDate);
                entWorkingHours.Text = timespan.TotalHours.ToString();
                mDAT_EMPLOYEE_IN_OUT.InOutHours = timespan.TotalHours.ToString();

                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEmployeeInOut.mJSN_REQ_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Add(mDAT_EMPLOYEE_IN_OUT);
                

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
               
                mDAT_EMPLOYEE_IN_OUT.InOutReason = entReason.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_IN_OUT.SD = Checkin.Date.ToString();
                mDAT_EMPLOYEE_IN_OUT.ED = Checkout.Date.ToString();

                DateTime sDate = Checkin.Date.ToUniversalTime();
                DateTime eDate = Checkout.Date.ToUniversalTime();
                TimeSpan timespan = eDate.Subtract(sDate);
                entWorkingHours.Text = timespan.TotalHours.ToString();
                mDAT_EMPLOYEE_IN_OUT.InOutHours = timespan.TotalHours.ToString();


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
                mDAT_EMPLOYEE_IN_OUT = new DAT_EMPLOYEE_IN_OUT();
                entReason.Text = "";
                entReferenceNo.Text = "";
                entRemark.Text = "";
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
            //mDAT_EMPLOYEE = mVmlEmployeeInOut.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlEmployeeInOut.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlEmployeeInOut.getIndexByTypeAsk(mDAT_EMPLOYEE);
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
                    if (mDAT_EMPLOYEE_IN_OUT.Ask != "6")
                    {
                        mDAT_EMPLOYEE_IN_OUT.ApproveStatusAsk = "1";
                    }

                    BindDataToObject();
                    mVmlEmployeeInOut.saveEmployeeInOut();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_EMPLOYEE_IN_OUT.StatusAsk = "6";
                    BindDataToObject();
                    mVmlEmployeeInOut.saveEmployeeInOut();
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