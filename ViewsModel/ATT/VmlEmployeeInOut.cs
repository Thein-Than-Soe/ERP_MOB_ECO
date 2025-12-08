using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.ATT.REQ;
using CS.ERP.PL.ATT.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.ATT;
using Microsoft.Maui.Controls;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.ATT
{
    public class VmlEmployeeInOut : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_IN_OUT mJSN_REQ_EMPLOYEE_IN_OUT = new JSN_REQ_EMPLOYEE_IN_OUT();
        public JSN_RES_EMPLOYEE_IN_OUT mJSN_RES_EMPLOYEE_IN_OUT = new JSN_RES_EMPLOYEE_IN_OUT();
        public JSN_RES_LOAD_EMPLOYEE_REQUEST mJSN_RES_LOAD_EMPLOYEE_REQUEST = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
        DateTime oDate;
        DateTime eDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEmployeeInOut()
        {
            this.switchDisplayView(DisplayView.Card);
            InOutLoad = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
            EmployeeTodayList = new List<DAT_EMPLOYEE_IN_OUT>();
            EmployeeLastWeekList = new List<DAT_EMPLOYEE_IN_OUT>();
            EmployeeLastMonthList = new List<DAT_EMPLOYEE_IN_OUT>();
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
        public JSN_RES_LOAD_EMPLOYEE_REQUEST JSN_RES_LOAD_EMPLOYEE_REQUEST = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
        public JSN_RES_LOAD_EMPLOYEE_REQUEST InOutLoad
        {
            get { return JSN_RES_LOAD_EMPLOYEE_REQUEST; }
            set { JSN_RES_LOAD_EMPLOYEE_REQUEST = value; NotifyPropertyChanged("InOutLoad"); }
        }


        public DAT_EMPLOYEE_IN_OUT mDAT_EMPLOYEE_IN_OUT = new DAT_EMPLOYEE_IN_OUT();
        public DAT_EMPLOYEE_IN_OUT DAT_EMPLOYEE_IN_OUT
        {
            get { return mDAT_EMPLOYEE_IN_OUT; }
            set { mDAT_EMPLOYEE_IN_OUT = value; NotifyPropertyChanged("DAT_EMPLOYEE_IN_OUT"); }
        }

        public List<DAT_EMPLOYEE_IN_OUT> mEmployeeInOutList;
        public List<DAT_EMPLOYEE_IN_OUT> EmployeeInOutList
        {
            get { return mEmployeeInOutList; }
            set { mEmployeeInOutList = value; NotifyPropertyChanged("EmployeeInOutList"); }
        }

        public List<DAT_EMPLOYEE_IN_OUT> mEmployeeTodayList;
        public List<DAT_EMPLOYEE_IN_OUT> EmployeeTodayList
        {
            get { return mEmployeeTodayList; }
            set { mEmployeeTodayList = value; NotifyPropertyChanged("EmployeeTodayList"); }
        }

        public List<DAT_EMPLOYEE_IN_OUT> mEmployeeLastWeekList;
        public List<DAT_EMPLOYEE_IN_OUT> EmployeeLastWeekList
        {
            get { return mEmployeeLastWeekList; }
            set { mEmployeeLastWeekList = value; NotifyPropertyChanged("EmployeeLastWeekList"); }
        }

        public List<DAT_EMPLOYEE_IN_OUT> mEmployeeLastMonthList;
        public List<DAT_EMPLOYEE_IN_OUT> EmployeeLastMonthList
        {
            get { return mEmployeeLastMonthList; }
            set { mEmployeeLastMonthList = value; NotifyPropertyChanged("EmployeeLastMonthList"); }
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
                    mRefreshCommand = new Command(() => this.getEmployeeInOut());
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
        private void bindDataTab(List<DAT_EMPLOYEE_IN_OUT> argDAT_EMPLOYEE_IN_OUT_LST)
         {
            try
            {
                List<DAT_EMPLOYEE_IN_OUT> l_DAT_EMPLOYEE_IN_OUT_TODAY = new List<DAT_EMPLOYEE_IN_OUT>();
                List<DAT_EMPLOYEE_IN_OUT> l_DAT_EMPLOYEE_IN_OUT_LASTWEEK = new List<DAT_EMPLOYEE_IN_OUT>();
                List<DAT_EMPLOYEE_IN_OUT> l_DAT_EMPLOYEE_IN_OUT_LASTMONTH = new List<DAT_EMPLOYEE_IN_OUT>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate = DateTime.Parse(todayDate);

                int lastweek = tDate.Day - 7;
                int lastmonth = tDate.Month - 1;


                if (argDAT_EMPLOYEE_IN_OUT_LST != null && argDAT_EMPLOYEE_IN_OUT_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_IN_OUT l_DAT_EMPLOYEE_IN_OUT in argDAT_EMPLOYEE_IN_OUT_LST)
                    {

                        l_DAT_EMPLOYEE_IN_OUT.SD = Utility.getDateTimeString(l_DAT_EMPLOYEE_IN_OUT.SD);
                        l_DAT_EMPLOYEE_IN_OUT.ED = Utility.getDateTimeString(l_DAT_EMPLOYEE_IN_OUT.ED);

                        if (oDate.Month <= lastmonth && eDate.Month <= lastmonth)
                        {
                            l_DAT_EMPLOYEE_IN_OUT_LASTMONTH.Add(l_DAT_EMPLOYEE_IN_OUT);
                        }
                        else if (oDate.Day >= lastweek && eDate.Day >= lastweek)
                        {
                            l_DAT_EMPLOYEE_IN_OUT_LASTWEEK.Add(l_DAT_EMPLOYEE_IN_OUT);
                        }
                        if (oDate.Day.Equals(tDate.Day) && eDate.Day.Equals(tDate.Day))
                        {
                            l_DAT_EMPLOYEE_IN_OUT_TODAY.Add(l_DAT_EMPLOYEE_IN_OUT);
                        }
                    }

                    DAT_EMPLOYEE_IN_OUT = argDAT_EMPLOYEE_IN_OUT_LST[0];
                    EmployeeInOutList = argDAT_EMPLOYEE_IN_OUT_LST;
                    EmployeeTodayList = l_DAT_EMPLOYEE_IN_OUT_TODAY;
                    EmployeeLastWeekList = l_DAT_EMPLOYEE_IN_OUT_LASTWEEK;
                    EmployeeLastMonthList = l_DAT_EMPLOYEE_IN_OUT_LASTMONTH;
                }
                else
                {
                    EmployeeInOutList = new List<DAT_EMPLOYEE_IN_OUT>();
                    EmployeeTodayList = new List<DAT_EMPLOYEE_IN_OUT>();
                    EmployeeLastWeekList = new List<DAT_EMPLOYEE_IN_OUT>();
                    EmployeeLastMonthList = new List<DAT_EMPLOYEE_IN_OUT>();
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
                List<DAT_EMPLOYEE_IN_OUT> l_DAT_EMPLOYEE_IN_OUT_lst = new List<DAT_EMPLOYEE_IN_OUT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_IN_OUT l_DAT_EMPLOYEE_IN_OUT in mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_IN_OUT.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_IN_OUT.InOutHours.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_IN_OUT.InOutReason.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_IN_OUT_lst.Add(l_DAT_EMPLOYEE_IN_OUT);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_IN_OUT_lst = mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT;// OriginalEmployeeInOutList.GetRange(0, OriginalEmployeeInOutList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_IN_OUT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployeeInOut()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_IN_OUT);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeInOut);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_IN_OUT = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_IN_OUT>(mResponse);
                    if (mJSN_RES_EMPLOYEE_IN_OUT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_IN_OUT.Message.Message);
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

        public async void saveEmployeeInOut()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_IN_OUT);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeInOut);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_IN_OUT = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_IN_OUT>(mResponse);
                    if (mJSN_RES_EMPLOYEE_IN_OUT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT.Count > 0)
                        {
                            DAT_EMPLOYEE_IN_OUT = this.mJSN_RES_EMPLOYEE_IN_OUT.DAT_EMPLOYEE_IN_OUT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_IN_OUT.Message.Message);
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

        public async void loadEmployeeInOut()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsloadEmployeeRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_LOAD_EMPLOYEE_REQUEST = JsonConvert.DeserializeObject<JSN_RES_LOAD_EMPLOYEE_REQUEST>(mResponse);
                    if (mJSN_RES_LOAD_EMPLOYEE_REQUEST.Message.Code == "7")
                    {
                        this.InOutLoad = mJSN_RES_LOAD_EMPLOYEE_REQUEST;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_IN_OUT.Message.Message);
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
