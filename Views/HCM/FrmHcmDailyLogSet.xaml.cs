using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.HCM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.HCM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmHcmDailyLogSet : ContentView
    {
        #region"Declaring"
        private DAT_EMPLOYEE_DAILY_LOG mDAT_EMPLOYEE_DAILY_LOG = new DAT_EMPLOYEE_DAILY_LOG();
       // private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlDailyLog mVmlDailyLog;
        #endregion
        #region"Constructor"
        public FrmHcmDailyLogSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlDailyLog = new VmlDailyLog();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Add(new DAT_EMPLOYEE_DAILY_LOG());
                mVmlDailyLog.loadDailyLog();
                loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmHcmDailyLogSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlDailyLog = new VmlDailyLog();
                mDAT_EMPLOYEE_DAILY_LOG.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Add(mDAT_EMPLOYEE_DAILY_LOG);
                mVmlDailyLog.getEmployeeDailyLog();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmHcmDailyLogSet(DAT_EMPLOYEE_DAILY_LOG argDAT_EMPLOYEE_DAILY_LOG)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_EMPLOYEE_DAILY_LOG = argDAT_EMPLOYEE_DAILY_LOG;
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
                // mDAT_EMPLOYEE_DAILY_LOG.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                mDAT_EMPLOYEE_DAILY_LOG.EmployeeAsk = "0";
                mDAT_EMPLOYEE_DAILY_LOG.TicketAsk = "0";
                mDAT_EMPLOYEE_DAILY_LOG.ProjectAsk = "0";
                mDAT_EMPLOYEE_DAILY_LOG.Note = entNote.Text.Trim();
                mDAT_EMPLOYEE_DAILY_LOG.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_DAILY_LOG.SD = entSD.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_DAILY_LOG.ED = entED.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_DAILY_LOG.ReferenceDate = entED.Date.ToUniversalTime().ToString();
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Add(mDAT_EMPLOYEE_DAILY_LOG);
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
                //mDAT_EMPLOYEE_DAILY_LOG.TicketName = entTicketName.Text.Trim();
                mDAT_EMPLOYEE_DAILY_LOG.Note = entNote.Text.Trim();
                mDAT_EMPLOYEE_DAILY_LOG.ReferenceNo = entReferenceNo.Text.Trim();
                mDAT_EMPLOYEE_DAILY_LOG.SD = entSD.Date.ToUniversalTime().ToString();
                mDAT_EMPLOYEE_DAILY_LOG.ED = entED.Date.ToUniversalTime().ToString();
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlDailyLog.mJSN_REQ_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Add(mDAT_EMPLOYEE_DAILY_LOG);

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
                mDAT_EMPLOYEE_DAILY_LOG = new DAT_EMPLOYEE_DAILY_LOG();
                entNote.Text = "";
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
           //// mDAT_EMPLOYEE = mVmlDailyLog.ClaimLoad.DAT_EMPLOYEE_RO;
           // pikEmployee.ItemsSource = mVmlDailyLog.EmployeeRo;
           // pikEmployee.SelectedIndex = mVmlDailyLog.getIndexByTypeAsk(mDAT_EMPLOYEE);
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

                if (!Common.bindMenu("hcm-daily-log-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmDailyLogLst", MenuUrl = "hcm-daily-log-lst", logoImg = "" };
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
                    if (mDAT_EMPLOYEE_DAILY_LOG.Ask != "6")
                    {
                        mDAT_EMPLOYEE_DAILY_LOG.StatusAsk = "1";
                    }

                    BindDataToObject();
                    mVmlDailyLog.saveEmployeeDailyLog();
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_EMPLOYEE_DAILY_LOG.StatusAsk = "6";
                    BindDataToObject();
                    mVmlDailyLog.saveEmployeeDailyLog();
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