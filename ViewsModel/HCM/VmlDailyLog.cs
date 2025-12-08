using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlDailyLog : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_DAILY_LOG mJSN_REQ_EMPLOYEE_DAILY_LOG = new JSN_REQ_EMPLOYEE_DAILY_LOG();
        public JSN_EMPLOYEE_DAILY_LOG mJSN_EMPLOYEE_DAILY_LOG = new JSN_EMPLOYEE_DAILY_LOG();
        public JSN_LOAD_EMPLOYEE_DAILY_LOG mJSN_LOAD_EMPLOYEE_DAILY_LOG = new JSN_LOAD_EMPLOYEE_DAILY_LOG();
        DateTime oDate;
        DateTime eDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlDailyLog()
        {
            this.switchDisplayView(DisplayView.Card);
            DailyLogLoad = new JSN_LOAD_EMPLOYEE_DAILY_LOG();
            TodayList = new List<DAT_EMPLOYEE_DAILY_LOG>();
            LastWeekList = new List<DAT_EMPLOYEE_DAILY_LOG>();
            LastMonthList = new List<DAT_EMPLOYEE_DAILY_LOG>();           
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
        public JSN_LOAD_EMPLOYEE_DAILY_LOG JSN_LOAD_EMPLOYEE_DAILY_LOG = new JSN_LOAD_EMPLOYEE_DAILY_LOG();
        public JSN_LOAD_EMPLOYEE_DAILY_LOG DailyLogLoad
        {
            get { return JSN_LOAD_EMPLOYEE_DAILY_LOG; }
            set { JSN_LOAD_EMPLOYEE_DAILY_LOG = value; NotifyPropertyChanged("DailyLogLoad"); }
        }


        public DAT_EMPLOYEE_DAILY_LOG mDAT_EMPLOYEE_DAILY_LOG = new DAT_EMPLOYEE_DAILY_LOG();
        public DAT_EMPLOYEE_DAILY_LOG DAT_EMPLOYEE_DAILY_LOG
        {
            get { return mDAT_EMPLOYEE_DAILY_LOG; }
            set { mDAT_EMPLOYEE_DAILY_LOG = value; NotifyPropertyChanged("DAT_EMPLOYEE_DAILY_LOG"); }
        }

        public List<DAT_EMPLOYEE_DAILY_LOG> mEmployeeDailyLogList;
        public List<DAT_EMPLOYEE_DAILY_LOG> EmployeeDailyLogList
        {
            get { return mEmployeeDailyLogList; }
            set { mEmployeeDailyLogList = value; NotifyPropertyChanged("EmployeeDailyLogList"); }
        }

        public List<DAT_EMPLOYEE_DAILY_LOG> mTodayList;
        public List<DAT_EMPLOYEE_DAILY_LOG> TodayList
        {
            get { return mTodayList; }
            set { mTodayList = value; NotifyPropertyChanged("TodayList"); }
        }

        public List<DAT_EMPLOYEE_DAILY_LOG> mLastWeekList;
        public List<DAT_EMPLOYEE_DAILY_LOG> LastWeekList
        {
            get { return mLastWeekList; }
            set { mLastWeekList = value; NotifyPropertyChanged("LastWeekList"); }
        }

        public List<DAT_EMPLOYEE_DAILY_LOG> mLastMonthList;
        public List<DAT_EMPLOYEE_DAILY_LOG> LastMonthList
        {
            get { return mLastMonthList; }
            set { mLastMonthList = value; NotifyPropertyChanged("LastMonthList"); }
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
                    mRefreshCommand = new Command(() => this.getEmployeeDailyLog());
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
        private void bindDataTab(List<DAT_EMPLOYEE_DAILY_LOG> argDAT_EMPLOYEE_DAILY_LOG_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_DAILY_LOG> l_DAT_EMPLOYEE_DAILY_LOG_TODAY = new List<DAT_EMPLOYEE_DAILY_LOG>();
                List<DAT_EMPLOYEE_DAILY_LOG> l_DAT_EMPLOYEE_DAILY_LOG_LASTWEEK = new List<DAT_EMPLOYEE_DAILY_LOG>();
                List<DAT_EMPLOYEE_DAILY_LOG> l_DAT_EMPLOYEE_DAILY_LOG_LASTMONTH = new List<DAT_EMPLOYEE_DAILY_LOG>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate= DateTime.Parse(todayDate);
                
                int lastweek = tDate.Day - 7 ;
                int lastmonth = tDate.Month - 1;                
              

                if (argDAT_EMPLOYEE_DAILY_LOG_LST != null && argDAT_EMPLOYEE_DAILY_LOG_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_DAILY_LOG l_DAT_EMPLOYEE_DAILY_LOG in argDAT_EMPLOYEE_DAILY_LOG_LST)
                    {
                        l_DAT_EMPLOYEE_DAILY_LOG.SD = Utility.getDateTimeString(l_DAT_EMPLOYEE_DAILY_LOG.SD);
                        l_DAT_EMPLOYEE_DAILY_LOG.ED = Utility.getDateTimeString(l_DAT_EMPLOYEE_DAILY_LOG.ED);

                        if (oDate.Month <= lastmonth && eDate.Month <= lastmonth)
                        {
                            l_DAT_EMPLOYEE_DAILY_LOG_LASTMONTH.Add(l_DAT_EMPLOYEE_DAILY_LOG);
                        }
                        else if (oDate.Day >= lastweek && eDate.Day >= lastweek)
                        {
                            l_DAT_EMPLOYEE_DAILY_LOG_LASTWEEK.Add(l_DAT_EMPLOYEE_DAILY_LOG);
                        }
                       else if (oDate.Day.Equals(tDate.Day) && eDate.Day.Equals(tDate.Day))
                        {
                            l_DAT_EMPLOYEE_DAILY_LOG_TODAY.Add(l_DAT_EMPLOYEE_DAILY_LOG);
                        }

                    }

                    DAT_EMPLOYEE_DAILY_LOG = argDAT_EMPLOYEE_DAILY_LOG_LST[0];
                    EmployeeDailyLogList = argDAT_EMPLOYEE_DAILY_LOG_LST;
                    TodayList = l_DAT_EMPLOYEE_DAILY_LOG_TODAY;
                    LastWeekList = l_DAT_EMPLOYEE_DAILY_LOG_LASTWEEK;
                    LastMonthList = l_DAT_EMPLOYEE_DAILY_LOG_LASTMONTH;
                }
                else
                {
                    EmployeeDailyLogList = new List<DAT_EMPLOYEE_DAILY_LOG>();
                    TodayList = new List<DAT_EMPLOYEE_DAILY_LOG>();
                    LastWeekList = new List<DAT_EMPLOYEE_DAILY_LOG>();
                    LastMonthList = new List<DAT_EMPLOYEE_DAILY_LOG>();
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
                List<DAT_EMPLOYEE_DAILY_LOG> l_DAT_EMPLOYEE_DAILY_LOG_lst = new List<DAT_EMPLOYEE_DAILY_LOG>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_DAILY_LOG l_DAT_EMPLOYEE_DAILY_LOG in mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_DAILY_LOG.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_DAILY_LOG.TicketName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_DAILY_LOG.ROTypeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_DAILY_LOG_lst.Add(l_DAT_EMPLOYEE_DAILY_LOG);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_DAILY_LOG_lst = mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG;// OriginalEmployeeDailyLogList.GetRange(0, OriginalEmployeeDailyLogList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_DAILY_LOG_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployeeDailyLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_DAILY_LOG);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeDailyLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_DAILY_LOG = JsonConvert.DeserializeObject<JSN_EMPLOYEE_DAILY_LOG>(mResponse);
                    if (mJSN_EMPLOYEE_DAILY_LOG.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Count > 0)
                        {
                            bindDataTab(this.mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_DAILY_LOG.Message.Message);
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

        public async void saveEmployeeDailyLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_DAILY_LOG);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeDailyLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_DAILY_LOG = JsonConvert.DeserializeObject<JSN_EMPLOYEE_DAILY_LOG>(mResponse);
                    if (mJSN_EMPLOYEE_DAILY_LOG.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG.Count > 0)
                        {
                            DAT_EMPLOYEE_DAILY_LOG = this.mJSN_EMPLOYEE_DAILY_LOG.DAT_EMPLOYEE_DAILY_LOG[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_DAILY_LOG.Message.Message);
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

        public async void loadDailyLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployeeDailyLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_DAILY_LOG = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_DAILY_LOG>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_DAILY_LOG.Message.Code == "7")
                    {
                        this.DailyLogLoad = mJSN_LOAD_EMPLOYEE_DAILY_LOG;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE_DAILY_LOG.Message.Message);
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
