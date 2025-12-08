using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.HCM.DAT;
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
    public partial class FrmATTOTRequestSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE_OT_REQUEST mDAT_EMPLOYEE_OT_REQUEST = new DAT_EMPLOYEE_OT_REQUEST();
        //private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlOTRequest mVmlOTRequest;
        #endregion
        #region"Constructor"
        public FrmATTOTRequestSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOTRequest = new VmlOTRequest();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.DAT_EMPLOYEE_OT_REQUEST.Add(new DAT_EMPLOYEE_OT_REQUEST());
                mVmlOTRequest.LoadOTRequest();
               // loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmATTOTRequestSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOTRequest = new VmlOTRequest();
                mDAT_EMPLOYEE_OT_REQUEST.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlOTRequest.mJSN_REQ_OT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.DAT_EMPLOYEE_OT_REQUEST.Add(mDAT_EMPLOYEE_OT_REQUEST);
                mVmlOTRequest.getOTRequest();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmATTOTRequestSet(DAT_EMPLOYEE_OT_REQUEST argDAT_EMPLOYEE_OT_REQUEST)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EMPLOYEE_OT_REQUEST = argDAT_EMPLOYEE_OT_REQUEST;
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
                // mDAT_EMPLOYEE_OT_REQUEST.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                mDAT_EMPLOYEE_OT_REQUEST.OTReason = entReason.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.SD = FromDate.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_OT_REQUEST.ED = ToDate.Date.ToUniversalTime().ToString();
                DateTime sDate = FromDate.Date.ToUniversalTime();
                DateTime eDate = ToDate.Date.ToUniversalTime();
                TimeSpan timespan = eDate.Subtract(sDate);
                mDAT_EMPLOYEE_OT_REQUEST.OTHours = timespan.TotalHours.ToString();
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.DAT_EMPLOYEE_OT_REQUEST.Add(mDAT_EMPLOYEE_OT_REQUEST);
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
                mDAT_EMPLOYEE_OT_REQUEST.OTReason = entReason.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_OT_REQUEST.SD = FromDate.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_OT_REQUEST.ED = ToDate.Date.ToUniversalTime().ToString();
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
                mDAT_EMPLOYEE_OT_REQUEST = new DAT_EMPLOYEE_OT_REQUEST();
                entOTHours.Text = "";
                entReason.Text = "";
                entRemark.Text = "";
                entReferenceNo.Text = "";
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
            //mDAT_EMPLOYEE = mVmlOTRequest.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlOTRequest.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlOTRequest.getIndexByTypeAsk(mDAT_EMPLOYEE);
        }

        private void OnStartDateSelected(object sender, DateChangedEventArgs args)
        {
            DateTime sDate = FromDate.Date.ToUniversalTime();
            DateTime eDate = ToDate.Date.ToUniversalTime();
            TimeSpan timespan = eDate.Subtract(sDate);
            entOTHours.Text = timespan.TotalHours.ToString();
        }

        private void OnEndDateSelected(object sender, DateChangedEventArgs args)
        {
            DateTime sDate = FromDate.Date.ToUniversalTime();
            DateTime eDate = ToDate.Date.ToUniversalTime();
            TimeSpan timespan = eDate.Subtract(sDate);
            entOTHours.Text = timespan.TotalHours.ToString();
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
                    if (mDAT_EMPLOYEE_OT_REQUEST.Ask != "6")
                    {
                        mDAT_EMPLOYEE_OT_REQUEST.ApproveStatusAsk = "1";
                    }

                    BindDataToObject();
                    mVmlOTRequest.saveOTRequest();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_EMPLOYEE_OT_REQUEST.StatusAsk = "6";
                    BindDataToObject();
                    mVmlOTRequest.saveOTRequest();
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