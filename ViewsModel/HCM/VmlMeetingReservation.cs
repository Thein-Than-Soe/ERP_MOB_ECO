using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.HCM;
using System;
using CS.ERP_MOB.General;
using System.Collections.Generic;
using System.Windows.Input;
using Java.Util;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlMeetingReservation : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_MEETING_ROOM_BOOKING mJSN_REQ_MEETING_ROOM_BOOKING = new JSN_REQ_MEETING_ROOM_BOOKING();
        public JSN_MEETING_ROOM_BOOKING mJSN_MEETING_ROOM_BOOKING = new JSN_MEETING_ROOM_BOOKING();
        public JSN_LOAD_MEETING_ROOM_BOOKING mJSN_LOAD_MEETING_ROOM_BOOKING = new JSN_LOAD_MEETING_ROOM_BOOKING();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlMeetingReservation()
        {
            this.switchDisplayView(DisplayView.Card);
            MeetingLoad = new JSN_LOAD_MEETING_ROOM_BOOKING();
            MeetingRoomBookingSubmitList = new List<DAT_MEETING_ROOM_BOOKING>();
            MeetingRoomBookingRejectList = new List<DAT_MEETING_ROOM_BOOKING>();
            MeetingRoomBookingApprovedList = new List<DAT_MEETING_ROOM_BOOKING>();
        }
        #endregion

        #region "Display View"
        private bool mIsCardView;
        public bool IsCardView
        {
            get
            {
                return mIsCardView;
            }
            set
            {
                mIsCardView = value;
                NotifyPropertyChanged("IsCardView");
            }
        }

        private bool mIsListView;
        public bool IsListView
        {
            get
            {
                return mIsListView;
            }
            set
            {
                mIsListView = value;
                NotifyPropertyChanged("IsListView");
            }
        }

        private bool mIsGridView;
        public bool IsGridView
        {
            get
            {
                return mIsGridView;
            }
            set
            {
                mIsGridView = value;
                NotifyPropertyChanged("IsGridView");
            }
        }

        private bool mIsRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return mIsRefreshing;
            }
            set
            {
                mIsRefreshing = value;
                NotifyPropertyChanged("IsRefreshing");
            }
        }
        #endregion

        #region "Data Tab"
        public JSN_LOAD_MEETING_ROOM_BOOKING JSN_LOAD_MEETING_ROOM_BOOKING = new JSN_LOAD_MEETING_ROOM_BOOKING();
        public JSN_LOAD_MEETING_ROOM_BOOKING MeetingLoad
        {
            get { return JSN_LOAD_MEETING_ROOM_BOOKING; }
            set { JSN_LOAD_MEETING_ROOM_BOOKING = value; NotifyPropertyChanged("MeetingLoad"); }
        }


        public DAT_MEETING_ROOM_BOOKING mDAT_MEETING_ROOM_BOOKING = new DAT_MEETING_ROOM_BOOKING();
        public DAT_MEETING_ROOM_BOOKING DAT_MEETING_ROOM_BOOKING
        {
            get { return mDAT_MEETING_ROOM_BOOKING; }
            set { mDAT_MEETING_ROOM_BOOKING = value; NotifyPropertyChanged("DAT_MEETING_ROOM_BOOKING"); }
        }

        public List<DAT_MEETING_ROOM_BOOKING> mMeetingRoomBookingList;
        public List<DAT_MEETING_ROOM_BOOKING> MeetingRoomBookingList
        {
            get { return mMeetingRoomBookingList; }
            set { mMeetingRoomBookingList = value; NotifyPropertyChanged("MeetingRoomBookingList"); }
        }

        public List<DAT_MEETING_ROOM_BOOKING> mMeetingRoomBookingSubmitList;
        public List<DAT_MEETING_ROOM_BOOKING> MeetingRoomBookingSubmitList
        {
            get { return mMeetingRoomBookingSubmitList; }
            set { mMeetingRoomBookingSubmitList = value; NotifyPropertyChanged("MeetingRoomBookingSubmitList"); }
        }

        public List<DAT_MEETING_ROOM_BOOKING> mMeetingRoomBookingRejectList;
        public List<DAT_MEETING_ROOM_BOOKING> MeetingRoomBookingRejectList
        {
            get { return mMeetingRoomBookingRejectList; }
            set { mMeetingRoomBookingRejectList = value; NotifyPropertyChanged("MeetingRoomBookingRejectList"); }
        }

        public List<DAT_MEETING_ROOM_BOOKING> mMeetingRoomBookingApprovedList;
        public List<DAT_MEETING_ROOM_BOOKING> MeetingRoomBookingApprovedList
        {
            get { return mMeetingRoomBookingApprovedList; }
            set { mMeetingRoomBookingApprovedList = value; NotifyPropertyChanged("MeetingRoomBookingApprovedList"); }
        }



        #endregion

        #region "Commands"
        private ICommand mCardViewCommand;
        public ICommand CardViewCommand
        {
            get
            {
                if (mCardViewCommand == null)
                {
                    mCardViewCommand = new Command(() => this.switchDisplayView(DisplayView.Card));
                }
                return mCardViewCommand;
            }
        }

        private ICommand mListViewCommand;
        public ICommand ListViewCommand
        {
            get
            {
                if (mListViewCommand == null)
                {
                    mListViewCommand = new Command(() => this.switchDisplayView(DisplayView.List));
                }
                return mListViewCommand;
            }
        }

        private ICommand mGridViewCommand;
        public ICommand GridViewCommand
        {
            get
            {
                if (mGridViewCommand == null)
                {
                    mGridViewCommand = new Command(() => this.switchDisplayView(DisplayView.Grid));
                }
                return mGridViewCommand;
            }
        }

        private ICommand mRefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (mRefreshCommand == null)
                {
                    mRefreshCommand = new Command(() => this.getMeetingRoomBooking());
                }
                return mRefreshCommand;
            }
        }

        public object LocalDateTime { get; private set; }
        #endregion

        #region "Method"
        private void switchDisplayView(DisplayView argDisplayView)
        {
            try
            {
                IsCardView = argDisplayView == DisplayView.Card;
                IsListView = argDisplayView == DisplayView.List;
                IsGridView = argDisplayView == DisplayView.Grid;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void bindDataTab(List<DAT_MEETING_ROOM_BOOKING> argDAT_MEETING_ROOM_BOOKING_LST)
        {
            try
            {
                List<DAT_MEETING_ROOM_BOOKING> l_DAT_MEETING_ROOM_BOOKING_SUBMIT = new List<DAT_MEETING_ROOM_BOOKING>();
                List<DAT_MEETING_ROOM_BOOKING> l_DAT_MEETING_ROOM_BOOKING_APPROVED = new List<DAT_MEETING_ROOM_BOOKING>();
                List<DAT_MEETING_ROOM_BOOKING> l_DAT_MEETING_ROOM_BOOKING_REJECTED = new List<DAT_MEETING_ROOM_BOOKING>();

                if (argDAT_MEETING_ROOM_BOOKING_LST != null && argDAT_MEETING_ROOM_BOOKING_LST.Count > 0)
                {
                    foreach (DAT_MEETING_ROOM_BOOKING l_DAT_MEETING_ROOM_BOOKING in argDAT_MEETING_ROOM_BOOKING_LST)
                    {
                        l_DAT_MEETING_ROOM_BOOKING.SD = Utility.getDateTimeString(l_DAT_MEETING_ROOM_BOOKING.SD);
                        l_DAT_MEETING_ROOM_BOOKING.ED = Utility.getDateTimeString(l_DAT_MEETING_ROOM_BOOKING.ED);

                        if (l_DAT_MEETING_ROOM_BOOKING.StatusAsk.Equals("1"))     
                        {                          
                            l_DAT_MEETING_ROOM_BOOKING_SUBMIT.Add(l_DAT_MEETING_ROOM_BOOKING);
                        }
                        else if (l_DAT_MEETING_ROOM_BOOKING.StatusAsk.Equals("2"))
                        {
                            l_DAT_MEETING_ROOM_BOOKING_APPROVED.Add(l_DAT_MEETING_ROOM_BOOKING);
                        }
                        else if (l_DAT_MEETING_ROOM_BOOKING.StatusAsk.Equals("3"))
                        {
                            l_DAT_MEETING_ROOM_BOOKING_REJECTED.Add(l_DAT_MEETING_ROOM_BOOKING);
                        }

                    }

                    DAT_MEETING_ROOM_BOOKING = argDAT_MEETING_ROOM_BOOKING_LST[0];
                    MeetingRoomBookingList = argDAT_MEETING_ROOM_BOOKING_LST;
                    MeetingRoomBookingSubmitList = l_DAT_MEETING_ROOM_BOOKING_SUBMIT;
                    MeetingRoomBookingApprovedList = l_DAT_MEETING_ROOM_BOOKING_APPROVED;
                    MeetingRoomBookingRejectList = l_DAT_MEETING_ROOM_BOOKING_APPROVED;

                }
                else
                {
                    MeetingRoomBookingList = new List<DAT_MEETING_ROOM_BOOKING>();
                    MeetingRoomBookingSubmitList = new List<DAT_MEETING_ROOM_BOOKING>();
                    MeetingRoomBookingApprovedList = new List<DAT_MEETING_ROOM_BOOKING>();
                    MeetingRoomBookingRejectList = new List<DAT_MEETING_ROOM_BOOKING>();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void searchData(string argKeyword)
        {
            try
            {
                List<DAT_MEETING_ROOM_BOOKING> l_DAT_MEETING_ROOM_BOOKING_lst = new List<DAT_MEETING_ROOM_BOOKING>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_MEETING_ROOM_BOOKING l_DAT_MEETING_ROOM_BOOKING in mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_MEETING_ROOM_BOOKING.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_MEETING_ROOM_BOOKING.MeetingRoomName.ToLower().Contains(argKeyword)
                            || l_DAT_MEETING_ROOM_BOOKING.SD.ToLower().Contains(argKeyword))
                            
                        {
                            l_DAT_MEETING_ROOM_BOOKING_lst.Add(l_DAT_MEETING_ROOM_BOOKING);
                        }
                    }
                }
                else
                {
                    l_DAT_MEETING_ROOM_BOOKING_lst = mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING;// OriginalMeetingRoomBookingList.GetRange(0, OriginalMeetingRoomBookingList.Count);
                }
                bindDataTab(l_DAT_MEETING_ROOM_BOOKING_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getMeetingRoomBooking()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_MEETING_ROOM_BOOKING);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetMeetingRoomBooking);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_MEETING_ROOM_BOOKING = JsonConvert.DeserializeObject<JSN_MEETING_ROOM_BOOKING>(mResponse);
                    if (mJSN_MEETING_ROOM_BOOKING.Message.Code == "7")
                    {
                        if (this.mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Count > 0)
                        {
                            bindDataTab(this.mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_MEETING_ROOM_BOOKING.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.WebServiceErr);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void saveMeetingRoomBooking()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_MEETING_ROOM_BOOKING);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveMeetingRoomBooking);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_MEETING_ROOM_BOOKING = JsonConvert.DeserializeObject<JSN_MEETING_ROOM_BOOKING>(mResponse);
                    if (mJSN_MEETING_ROOM_BOOKING.Message.Code == "7")
                    {
                        if (this.mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING.Count > 0)
                        {
                            DAT_MEETING_ROOM_BOOKING = this.mJSN_MEETING_ROOM_BOOKING.DAT_MEETING_ROOM_BOOKING[0];
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.SaveSuccess);
                            //route parent list form after save
                            Common.routeMenu(Common.mCommon.SelectedMenu);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_MEETING_ROOM_BOOKING.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.WebServiceErr);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void loadMeetingReservation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadMeetingRoomBooking);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_MEETING_ROOM_BOOKING = JsonConvert.DeserializeObject<JSN_LOAD_MEETING_ROOM_BOOKING>(mResponse);
                    if (mJSN_LOAD_MEETING_ROOM_BOOKING.Message.Code == "7")
                    {
                        this.MeetingLoad = mJSN_LOAD_MEETING_ROOM_BOOKING;

                        //if (this.mJSN_LOAD_APPLICANT.RES_SUPPLIER.Count > 0)
                        //{
                        //   this.JSN_LOAD_SUPPLIER= bindDataTab(this.mJSN_SUPPLIERNCONTACT.RES_SUPPLIER);
                        //    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        //}
                        //else
                        //{
                        //    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        //}
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_MEETING_ROOM_BOOKING.Message.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.WebServiceErr);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion



    }

   
}
