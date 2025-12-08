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
    public partial class FrmATTEarlyOutRequestSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE_EARLY_OUT_REQUEST mDAT_EMPLOYEE_EARLY_OUT_REQUEST = new DAT_EMPLOYEE_EARLY_OUT_REQUEST();
        //private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlEarlyOutRequest mVmlEarlyOutRequest;
        #endregion
        #region"Constructor"
        public FrmATTEarlyOutRequestSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEarlyOutRequest = new VmlEarlyOutRequest();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Add(new DAT_EMPLOYEE_EARLY_OUT_REQUEST());
                mVmlEarlyOutRequest.loadAbsentReq();
                //loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmATTEarlyOutRequestSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEarlyOutRequest = new VmlEarlyOutRequest();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Add(mDAT_EMPLOYEE_EARLY_OUT_REQUEST);
                mVmlEarlyOutRequest.getEarlyOut();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmATTEarlyOutRequestSet(DAT_EMPLOYEE_EARLY_OUT_REQUEST argDAT_EMPLOYEE_EARLY_OUT_REQUEST)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EMPLOYEE_EARLY_OUT_REQUEST = argDAT_EMPLOYEE_EARLY_OUT_REQUEST;
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
                // mDAT_EMPLOYEE_EARLY_OUT_REQUEST.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;
                
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.SD = FromDate.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.ED = ToDate.Date.ToUniversalTime().ToString();

                DateTime sDate = FromDate.Date.ToUniversalTime();
                DateTime eDate = ToDate.Date.ToUniversalTime();
                TimeSpan timespan = eDate.Subtract(sDate);
                entEarlyoutHours.Text = timespan.TotalHours.ToString();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutHours = timespan.TotalHours.ToString();
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Add(mDAT_EMPLOYEE_EARLY_OUT_REQUEST);
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
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutReason = entReason.Text.Trim();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.Remark = entRemark.Text.Trim();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.SD = FromDate.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST.ED = ToDate.Date.ToUniversalTime().ToString();
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
                mDAT_EMPLOYEE_EARLY_OUT_REQUEST = new DAT_EMPLOYEE_EARLY_OUT_REQUEST();
                entEarlyoutHours.Text = "";
                entReason.Text = "";
                entRemark.Text = "";
                entReferenceNo.Text = "";
                FromDate.Date = DateTime.Today;
                ToDate.Date = DateTime.Today;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void loadData()
        {
            //mDAT_EMPLOYEE = mVmlEarlyOutRequest.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlEarlyOutRequest.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlEarlyOutRequest.getIndexByTypeAsk(mDAT_EMPLOYEE);
        }

        #endregion
        #region"Publich Method"
        #endregion
        #region""
        #endregion
        #region"Event"

        private void OnEndDateSelected(object sender, DateChangedEventArgs args)
        {
            DateTime sDate = FromDate.Date.ToUniversalTime();
            DateTime eDate = ToDate.Date.ToUniversalTime();
            TimeSpan timespan = eDate.Subtract(sDate);
            entEarlyoutHours.Text = timespan.TotalHours.ToString();
        }

        private void OnStartDateSelected(object sender, DateChangedEventArgs args)
        {
            DateTime sDate = FromDate.Date.ToUniversalTime();
            DateTime eDate = ToDate.Date.ToUniversalTime();
            TimeSpan timespan = eDate.Subtract(sDate);
            entEarlyoutHours.Text = timespan.TotalHours.ToString();
        }


        private void BackTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                if (!Common.bindMenu("att-early-out-request-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTEarlyOutRequestLst", MenuUrl = "att-early-out-request-lst", logoImg = "" };
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
                    if (mDAT_EMPLOYEE_EARLY_OUT_REQUEST.Ask != "6")
                    {
                        mDAT_EMPLOYEE_EARLY_OUT_REQUEST.ApproveStatusAsk = "1";
                    }

                    BindDataToObject();
                    mVmlEarlyOutRequest.saveEarlyOut();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_EMPLOYEE_EARLY_OUT_REQUEST.StatusAsk = "6";
                    BindDataToObject();
                    mVmlEarlyOutRequest.saveEarlyOut();
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