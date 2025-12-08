using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.CRM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCrmEvaluationPeriodSet : ContentView
    {
        #region"Declaring"
        private DAT_EVALUATION_PERIOD mDAT_EVALUATION_PERIOD = new DAT_EVALUATION_PERIOD();
       // private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlEvaluationPeriod mVmlEvaluationPeriod;
        #endregion
        #region"Constructor"
        public FrmCrmEvaluationPeriodSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEvaluationPeriod = new VmlEvaluationPeriod();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Add(new DAT_EVALUATION_PERIOD());
                mVmlEvaluationPeriod.LoadEvaluationPeriod ();
                loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmCrmEvaluationPeriodSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEvaluationPeriod = new VmlEvaluationPeriod();
                mDAT_EVALUATION_PERIOD.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Add(mDAT_EVALUATION_PERIOD);
                mVmlEvaluationPeriod.getEvaluationPeriod();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmCrmEvaluationPeriodSet(DAT_EVALUATION_PERIOD argDAT_EVALUATION_PERIOD)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EVALUATION_PERIOD = argDAT_EVALUATION_PERIOD;
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
                // mDAT_EVALUATION_PERIOD.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                mDAT_EVALUATION_PERIOD.EvaluationPeriodCode = entCode.Text.Trim();
                mDAT_EVALUATION_PERIOD.EvaluationPeriodName = entName.Text.Trim();
                mDAT_EVALUATION_PERIOD.Remark = entRemark.Text.Trim();
                mDAT_EVALUATION_PERIOD.EvaluationPeriodDescription = entDesription.Text.Trim();
               
                mDAT_EVALUATION_PERIOD.SD = fromDate.Date.ToUniversalTime().ToString();
                mDAT_EVALUATION_PERIOD.ED = toDate.Date.ToUniversalTime().ToString();
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEvaluationPeriod.mJSN_REQ_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Add(mDAT_EVALUATION_PERIOD);
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
                mDAT_EVALUATION_PERIOD.EvaluationPeriodCode = entCode.Text.Trim();
                mDAT_EVALUATION_PERIOD.EvaluationPeriodName = entName.Text.Trim();
                mDAT_EVALUATION_PERIOD.Remark = entRemark.Text.Trim();
                mDAT_EVALUATION_PERIOD.SD = fromDate.Date.ToUniversalTime().ToString();
                mDAT_EVALUATION_PERIOD.SD = fromDate.Date.ToUniversalTime().ToString();
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
                mDAT_EVALUATION_PERIOD = new DAT_EVALUATION_PERIOD();
                entCode.Text = "";
                entName.Text = "";
                entDesription.Text = "";
                entRemark.Text = "";
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
           // mDAT_EMPLOYEE = mVmlEvaluationPeriod.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlEvaluationPeriod.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlEvaluationPeriod.getIndexByTypeAsk(mDAT_EMPLOYEE);
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
                    if (mDAT_EVALUATION_PERIOD.Ask != "6")
                    {
                        mDAT_EVALUATION_PERIOD.StatusAsk = "1";
                        BindDataToObject();
                        mVmlEvaluationPeriod.saveEvaluationPeriod();
                    }

                    
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_EVALUATION_PERIOD.StatusAsk = "6";
                    BindDataToObject();
                    mVmlEvaluationPeriod.saveEvaluationPeriod();
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