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
    public partial class FrmHcmMeetingReservationSet : ContentView
    {
        #region"Declaring"
        private DAT_MEETING_ROOM_BOOKING mDAT_MEETING_ROOM_BOOKING = new DAT_MEETING_ROOM_BOOKING();
       // private List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        VmlMeetingReservation mVmlMeetingReservation;
        #endregion
        #region"Constructor"
        public FrmHcmMeetingReservationSet()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMeetingReservation = new VmlMeetingReservation();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Add(new DAT_MEETING_ROOM_BOOKING());
                mVmlMeetingReservation.loadMeetingReservation();
                loadData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmHcmMeetingReservationSet(string argAsk)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMeetingReservation = new VmlMeetingReservation();
                mDAT_MEETING_ROOM_BOOKING.Ask = argAsk;
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Add(mDAT_MEETING_ROOM_BOOKING);
                mVmlMeetingReservation.getMeetingRoomBooking();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public FrmHcmMeetingReservationSet(DAT_MEETING_ROOM_BOOKING argDAT_MEETING_ROOM_BOOKING)
        {
            try
            {
                InitializeComponent();
                List<RES_CONTROL> controls = Common.mCommon.SelectedMenu.button.GetRange(0, 2);
                BindableLayout.SetItemsSource(ControlButtons, controls);

                mDAT_MEETING_ROOM_BOOKING = argDAT_MEETING_ROOM_BOOKING;
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


        private void BindDataToObject()
        {
            try
            {
                //var selectedEmployee = pikEmployee.SelectedItem as Variable;
                // mDAT_MEETING_ROOM_BOOKING.EmployeeAsk = selectedEmployee.Ask;

                //.EmployeeAsk= pikEmployee.SelectedItem as RES_Emplo;

                mDAT_MEETING_ROOM_BOOKING.SpecialRequest = entSpecialReq.Text.Trim();
                mDAT_MEETING_ROOM_BOOKING.Remark = entRemark.Text.Trim();
               
                mDAT_MEETING_ROOM_BOOKING.SD = dateSD.Date.ToUniversalTime().ToString();
                mDAT_MEETING_ROOM_BOOKING.ED = dateED.Date.ToUniversalTime().ToString();
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Add(mDAT_MEETING_ROOM_BOOKING);
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
                mDAT_MEETING_ROOM_BOOKING.SpecialRequest = entSpecialReq.Text.Trim();
                mDAT_MEETING_ROOM_BOOKING.SD = dateSD.Date.ToUniversalTime().ToString();
                mDAT_MEETING_ROOM_BOOKING.Remark = entRemark.Text.Trim();
                mDAT_MEETING_ROOM_BOOKING.SD = dateED.Date.ToUniversalTime().ToString();
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
                mDAT_MEETING_ROOM_BOOKING = new DAT_MEETING_ROOM_BOOKING();
                entSpecialReq.Text = "";
                entRemark.Text = "";
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
            //mDAT_EMPLOYEE = mVmlMeetingReservation.ClaimLoad.DAT_EMPLOYEE_RO;
            //pikEmployee.ItemsSource = mVmlMeetingReservation.EmployeeRo;
            //pikEmployee.SelectedIndex = mVmlMeetingReservation.getIndexByTypeAsk(mDAT_EMPLOYEE);
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

                if (!Common.bindMenu("hcm-meeting-reservation-lst"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmMeetingRoomReservation", MenuUrl = "hcm-meeting-reservation-lst", logoImg = "" };
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
                    if (mDAT_MEETING_ROOM_BOOKING.Ask != "6")
                    {
                        mDAT_MEETING_ROOM_BOOKING.StatusAsk = "1";
                        BindDataToObject();
                        mVmlMeetingReservation.saveMeetingRoomBooking();
                    }

                }
                else if (control.link.Equals("btnDelete_onClick"))
                {
                    mDAT_MEETING_ROOM_BOOKING.StatusAsk = "6";
                    BindDataToObject();
                    mVmlMeetingReservation.saveMeetingRoomBooking();
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