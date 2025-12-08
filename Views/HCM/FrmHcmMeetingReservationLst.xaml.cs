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
    public partial class FrmHcmMeetingReservationLst : ContentView
    {
        #region "Declaring"
        VmlMeetingReservation mVmlMeetingReservation;
        #endregion
        #region "Constructor"
        public FrmHcmMeetingReservationLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMeetingReservation = new VmlMeetingReservation();
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMeetingReservation.mJSN_REQ_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Add(new DAT_MEETING_ROOM_BOOKING());
                mVmlMeetingReservation.getMeetingRoomBooking();
                switchLanguage();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion
        #region "Private Mehtod"
        public void switchLanguage()
        {
            try
            {
                switch (Common.mCommon.UserSetting.LanguageAsk)
                {
                    case "1"://English
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlMeetingReservation.MeetingRoomBookingSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlMeetingReservation.MeetingRoomBookingApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlMeetingReservation.MeetingRoomBookingRejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "Submit (" + mVmlMeetingReservation.MeetingRoomBookingSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlMeetingReservation.MeetingRoomBookingApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlMeetingReservation.MeetingRoomBookingRejectList.Count + ")";

                            REmployee.Title = "ဝန်ထမ်း";
                            RMeetingRoom.Title = "အစည်းအဝေးခန်း";
                            RSD.Title = "စတင်သည့်ရက်စွဲ";
                            RED.Title = "ကုန်ဆုံးရက်";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SMeetingRoom.Title = "အစည်းအဝေးခန်း";
                            SSD.Title = "စတင်သည့်ရက်စွဲ";
                            SED.Title = "ကုန်ဆုံးရက်";

                            ApEmployee.Title = "ဝန်ထမ်း";
                            ApMeetingRoom.Title = "အစည်းအဝေးခန်း";
                            ApSD.Title = "စတင်သည့်ရက်စွဲ";
                            ApED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlMeetingReservation.MeetingRoomBookingSubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlMeetingReservation.MeetingRoomBookingApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlMeetingReservation.MeetingRoomBookingRejectList.Count + ")";
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
        #region "Event"
        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("hcm-meeting-reservation-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "3", Text = "FrmHcmMeetingReservationSet", MenuUrl = "hcm-meeting-reservation-set", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (entSearch.Text != null && entSearch.Text != "")
                {
                    mVmlMeetingReservation.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlMeetingReservation.searchData("");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_DAT_MEETING_ROOM_BOOKING = (DAT_MEETING_ROOM_BOOKING)e.SelectedItem;
                if (l_DAT_MEETING_ROOM_BOOKING != null)
                {
                    if (!Common.bindMenu("hcm-meeting-reservation-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmMeetingRoom", MenuUrl = "hcm-meeting-reservation-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_MEETING_ROOM_BOOKING.Ask);

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_DAT_MEETING_ROOM_BOOKING = (DAT_MEETING_ROOM_BOOKING)e.SelectedItem;
                if (l_DAT_MEETING_ROOM_BOOKING != null)
                {
                    if (!Common.bindMenu("hcm-meeting-reservation-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmHcmMeetingRoom", MenuUrl = "hcm-meeting-reservation-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_MEETING_ROOM_BOOKING.Ask);

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrCardView_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
                entSearch.Text = "";
                mVmlMeetingReservation.getMeetingRoomBooking();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrDisplayColumn_Tapped(object sender, EventArgs e)
        {
            try
            {
                //mVmlMeetingReservation.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}