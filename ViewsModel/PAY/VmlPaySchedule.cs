using CS.ERP.PL.PAY;
using CS.ERP.PL.PAY.REQ;
using CS.ERP.PL.PAY.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.PAY;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.PAY
{
    public class VmlPaySchedule : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SCHEDULE mJSN_REQ_SCHEDULE = new JSN_REQ_SCHEDULE();
        public JSN_RES_SCHEDULE mJSN_RES_SCHEDULE = new JSN_RES_SCHEDULE();
        public JSN_LOAD_PAY_SCHEDULE mJSN_LOAD_PAY_SCHEDULE =new JSN_LOAD_PAY_SCHEDULE();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPaySchedule()
        {
            this.switchDisplayView(DisplayView.Card);
            PayScheduleLoad = new JSN_LOAD_PAY_SCHEDULE();
            MonthlyList = new List<DAT_SCHEDULE>();
            WeeklyList = new List<DAT_SCHEDULE>();
            DailyList = new List<DAT_SCHEDULE>();
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
        public JSN_LOAD_PAY_SCHEDULE JSN_LOAD_PAY_SCHEDULE = new JSN_LOAD_PAY_SCHEDULE();
        public JSN_LOAD_PAY_SCHEDULE PayScheduleLoad
        {
            get { return JSN_LOAD_PAY_SCHEDULE; }
            set { JSN_LOAD_PAY_SCHEDULE = value; NotifyPropertyChanged("PayScheduleLoad"); }
        }


        public DAT_SCHEDULE mDAT_SCHEDULE = new DAT_SCHEDULE();
        public DAT_SCHEDULE DAT_SCHEDULE
        {
            get { return mDAT_SCHEDULE; }
            set { mDAT_SCHEDULE = value; NotifyPropertyChanged("DAT_SCHEDULE"); }
        }

        public List<DAT_SCHEDULE> mPayScheduleList;
        public List<DAT_SCHEDULE> PayScheduleList
        {
            get { return mPayScheduleList; }
            set { mPayScheduleList = value; NotifyPropertyChanged("PayScheduleList"); }
        }

        public List<DAT_SCHEDULE> mMonthlyList;
        public List<DAT_SCHEDULE> MonthlyList
        {
            get { return mMonthlyList; }
            set { mMonthlyList = value; NotifyPropertyChanged("MonthlyList"); }
        }

        public List<DAT_SCHEDULE> mWeeklyList;
        public List<DAT_SCHEDULE> WeeklyList
        {
            get { return mWeeklyList; }
            set { mWeeklyList = value; NotifyPropertyChanged("WeeklyList"); }
        }

        public List<DAT_SCHEDULE> mDailyList;
        public List<DAT_SCHEDULE> DailyList
        {
            get { return mDailyList; }
            set { mDailyList = value; NotifyPropertyChanged("DailyList"); }
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
                    mRefreshCommand = new Command(() => this.getPaySchedule());
                }
                return mRefreshCommand;
            }
        }
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
        private void bindDataTab(List<DAT_SCHEDULE> argDAT_SCHEDULE_LST)
        {
            try
            {
                List<DAT_SCHEDULE> l_DAT_SCHEDULE_MONTHLY = new List<DAT_SCHEDULE>();
                List<DAT_SCHEDULE> l_DAT_SCHEDULE_WEEKLY = new List<DAT_SCHEDULE>();
                List<DAT_SCHEDULE> l_DAT_SCHEDULE_DAILY = new List<DAT_SCHEDULE>();

                if (argDAT_SCHEDULE_LST != null && argDAT_SCHEDULE_LST.Count > 0)
                {
                    foreach (DAT_SCHEDULE l_DAT_SCHEDULE in argDAT_SCHEDULE_LST)
                    {
                        l_DAT_SCHEDULE.SD = Utility.getDateTimeString(l_DAT_SCHEDULE.SD);
                        l_DAT_SCHEDULE.ED = Utility.getDateTimeString(l_DAT_SCHEDULE.ED);

                        if (l_DAT_SCHEDULE.PayScheduleTypeAsk.Equals("4"))
                        {
                            l_DAT_SCHEDULE_MONTHLY.Add(l_DAT_SCHEDULE);
                        }
                        else if (l_DAT_SCHEDULE.PayScheduleTypeAsk.Equals("2"))
                        {
                            l_DAT_SCHEDULE_WEEKLY.Add(l_DAT_SCHEDULE);
                        }
                        else if (l_DAT_SCHEDULE.PayScheduleTypeAsk.Equals("1"))
                        {
                            l_DAT_SCHEDULE_DAILY.Add(l_DAT_SCHEDULE);
                        }
                    }

                    DAT_SCHEDULE = argDAT_SCHEDULE_LST[0];
                    PayScheduleList = argDAT_SCHEDULE_LST;
                    MonthlyList = l_DAT_SCHEDULE_MONTHLY;
                    WeeklyList = l_DAT_SCHEDULE_WEEKLY;
                    DailyList = l_DAT_SCHEDULE_DAILY;
                }
                else
                {
                    PayScheduleList = new List<DAT_SCHEDULE>();
                    MonthlyList = new List<DAT_SCHEDULE>();
                    WeeklyList = new List<DAT_SCHEDULE>();
                    DailyList = new List<DAT_SCHEDULE>();
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
                List<DAT_SCHEDULE> l_DAT_SCHEDULE_lst = new List<DAT_SCHEDULE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_SCHEDULE l_DAT_SCHEDULE in mJSN_RES_SCHEDULE.DAT_SCHEDULE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_SCHEDULE.PayScheduleName.ToLower().Contains(argKeyword)
                            || l_DAT_SCHEDULE.PayTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_SCHEDULE.StatusName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_SCHEDULE_lst.Add(l_DAT_SCHEDULE);
                        }
                    }
                }
                else
                {
                    l_DAT_SCHEDULE_lst = mJSN_RES_SCHEDULE.DAT_SCHEDULE;// OriginalPayScheduleList.GetRange(0, OriginalPayScheduleList.Count);
                }
                bindDataTab(l_DAT_SCHEDULE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getPaySchedule()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SCHEDULE);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wsgetPaySchedule);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_SCHEDULE = JsonConvert.DeserializeObject<JSN_RES_SCHEDULE>(mResponse);
                    if (mJSN_RES_SCHEDULE.Message.Code == "7")
                    {
                        if (this.mJSN_RES_SCHEDULE.DAT_SCHEDULE.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_SCHEDULE.DAT_SCHEDULE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_SCHEDULE.Message.Message);
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

        public async void savePaySchedule()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SCHEDULE);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wssaveEmployee);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_SCHEDULE = JsonConvert.DeserializeObject<JSN_RES_SCHEDULE>(mResponse);
                    if (mJSN_RES_SCHEDULE.Message.Code == "7")
                    {
                        if (this.mJSN_RES_SCHEDULE.DAT_SCHEDULE.Count > 0)
                        {
                            DAT_SCHEDULE = this.mJSN_RES_SCHEDULE.DAT_SCHEDULE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_SCHEDULE.Message.Message);
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

        public async void loadPaySchedule()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wsloadPaySchedule);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PAY_SCHEDULE = JsonConvert.DeserializeObject<JSN_LOAD_PAY_SCHEDULE>(mResponse);
                    if (mJSN_LOAD_PAY_SCHEDULE.Message.Code == "7")
                    {
                        this.PayScheduleLoad = mJSN_LOAD_PAY_SCHEDULE;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PAY_SCHEDULE.Message.Message);
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
