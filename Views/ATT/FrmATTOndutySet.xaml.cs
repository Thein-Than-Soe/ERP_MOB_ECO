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
    public partial class FrmATTOndutySet : ContentView
    {
        #region"Declaring"
        private DAT_ON_DUTY mDAT_ON_DUTY = new DAT_ON_DUTY();
        //private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlOnduty mVmlOnduty;
        #endregion
        #region"Constructor"
        public FrmATTOndutySet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOnduty = new VmlOnduty();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlOnduty.mJSN_REQ_ON_DUTY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOnduty.mJSN_REQ_ON_DUTY.DAT_ON_DUTY.Add(new DAT_ON_DUTY());
                mVmlOnduty.LoadOnduty();
              
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmATTOndutySet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOnduty = new VmlOnduty();
                mDAT_ON_DUTY.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlOnduty.mJSN_REQ_ON_DUTY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOnduty.mJSN_REQ_ON_DUTY.DAT_ON_DUTY.Add(mDAT_ON_DUTY);
                mVmlOnduty.getOnDuty();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmATTOndutySet(DAT_ON_DUTY argDAT_ON_DUTY)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_ON_DUTY = argDAT_ON_DUTY;
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
                // mDAT_ON_DUTY.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                mDAT_ON_DUTY.Remark = entRemark.Text.Trim();
                mDAT_ON_DUTY.SD = fromDate.Date.ToUniversalTime().ToString();
                mDAT_ON_DUTY.ED = toDate.Date.ToUniversalTime().ToString();
                mVmlOnduty.mJSN_REQ_ON_DUTY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOnduty.mJSN_REQ_ON_DUTY.DAT_ON_DUTY.Add(mDAT_ON_DUTY);
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
                mDAT_ON_DUTY.Remark = entRemark.Text.Trim();
                mDAT_ON_DUTY.SD = fromDate.Date.ToUniversalTime().ToString();
                mDAT_ON_DUTY.ED = toDate.Date.ToUniversalTime().ToString();
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
                mDAT_ON_DUTY = new DAT_ON_DUTY();
                entRemark.Text = "";
               // entReason.Text = "";
                //entRemark.Text = "";
                //entReferenceNo.Text = "";
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
            //mDAT_EMPLOYEE = mVmlOnduty.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlOnduty.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlOnduty.getIndexByTypeAsk(mDAT_EMPLOYEE);
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

                if (!Common.bindMenu("att-onduty-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTOndutyLst", MenuUrl = "att-onduty-lst", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                ////ContentView contentView = new AccessListPage();
                ////RoutingModel routemodel = new RoutingModel("Access List", contentView);
                ////MessagingCenter.Send<Application, RoutingModel>(Application.Current, "ViewChange", routemodel);
                //Common.routeMenu("att-onduty-lst", "Access List");
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
                    if (mDAT_ON_DUTY.Ask != "6")
                    {
                        mDAT_ON_DUTY.Status = "1";
                        BindDataToObject();
                        mVmlOnduty.saveOnDuty();
                    }

                   
                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_ON_DUTY.Status = "6";
                    BindDataToObject();
                    mVmlOnduty.saveOnDuty();
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