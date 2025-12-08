using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP.PL.PAY;
using CS.ERP.PL.PAY.RES;
using CS.ERP.PL.SYS.REQ;
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
    public class VmlPaySalary : BaseViewModel
    {
        #region "Declaring"
        public REQ_AUTHORIZATION mREQ_AUTHORIZATION = new REQ_AUTHORIZATION();
        public JSN_RES_OUT mJSN_RES_OUT = new JSN_RES_OUT();
        public JSN_LOAD_EMPLOYEE_PAY mJSN_LOAD_EMPLOYEE_PAY = new JSN_LOAD_EMPLOYEE_PAY();
        DateTime endDate;
        DateTime startDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPaySalary()
        {
            this.switchDisplayView(DisplayView.Card);
            PaySalaryLoad = new JSN_LOAD_EMPLOYEE_PAY();
            ScheduleDetailList = new List<DAT_SCHEDULE_DETAIL>();
            LastMonthList = new List<DAT_SCHEDULE_DETAIL>();
            LastMonth4List = new List<DAT_SCHEDULE_DETAIL>();
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
        public JSN_LOAD_EMPLOYEE_PAY JSN_LOAD_EMPLOYEE_PAY = new JSN_LOAD_EMPLOYEE_PAY();
        public JSN_LOAD_EMPLOYEE_PAY PaySalaryLoad
        {
            get { return JSN_LOAD_EMPLOYEE_PAY; }
            set { JSN_LOAD_EMPLOYEE_PAY = value; NotifyPropertyChanged("PaySalaryLoad"); }
        }

        public DAT_SCHEDULE_DETAIL mDAT_SCHEDULE_DETAIL = new DAT_SCHEDULE_DETAIL();
        public DAT_SCHEDULE_DETAIL DAT_SCHEDULE_DETAIL
        {
            get { return mDAT_SCHEDULE_DETAIL; }
            set { mDAT_SCHEDULE_DETAIL = value; NotifyPropertyChanged("DAT_SCHEDULE_DETAIL"); }
        }

        public List<DAT_SCHEDULE_DETAIL> mScheduleDetailList;
        public List<DAT_SCHEDULE_DETAIL> ScheduleDetailList
        {
            get { return mScheduleDetailList; }
            set { mScheduleDetailList = value; NotifyPropertyChanged("ScheduleDetailList"); }
        }

        public List<DAT_SCHEDULE_DETAIL> mLastMonthList;
        public List<DAT_SCHEDULE_DETAIL> LastMonthList
        {
            get { return mLastMonthList; }
            set { mLastMonthList = value; NotifyPropertyChanged("LastMonthList"); }
        }

        public List<DAT_SCHEDULE_DETAIL> mLastMonth4List;
        public List<DAT_SCHEDULE_DETAIL> LastMonth4List
        {
            get { return mLastMonth4List; }
            set { mLastMonth4List = value; NotifyPropertyChanged("LastMonth4List"); }
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
                    mRefreshCommand = new Command(() => this.getEmployeeSalary());
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
        private void bindDataTab(List<DAT_SCHEDULE_DETAIL> argDAT_SCHEDULE_DETAIL_LST)
        {
            try
            {
                List<DAT_SCHEDULE_DETAIL> l_DAT_SCHEDULE_DETAIL_LAST = new List<DAT_SCHEDULE_DETAIL>();
               
                List<DAT_SCHEDULE_DETAIL> l_DAT_SCHEDULE_DETAIL_LASTMONTH = new List<DAT_SCHEDULE_DETAIL>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate = DateTime.Parse(todayDate);

                int lastmonth = tDate.Month - 4;
                int last = tDate.Month - 1;

                if (argDAT_SCHEDULE_DETAIL_LST != null && argDAT_SCHEDULE_DETAIL_LST.Count > 0)
                {

                    foreach (DAT_SCHEDULE_DETAIL l_DAT_SCHEDULE_DETAIL in argDAT_SCHEDULE_DETAIL_LST)
                    {
                        endDate = DateTime.Parse(l_DAT_SCHEDULE_DETAIL.ED);
                        startDate = DateTime.Parse(l_DAT_SCHEDULE_DETAIL.SD);
                        l_DAT_SCHEDULE_DETAIL.Salary = Utility.getAmountString(l_DAT_SCHEDULE_DETAIL.Salary);


                        if (endDate.Month >= lastmonth && startDate.Month >= lastmonth)
                        {
                            l_DAT_SCHEDULE_DETAIL_LASTMONTH.Add(l_DAT_SCHEDULE_DETAIL);
                        }

                        if (endDate.Month == last && startDate.Month == last)
                        {
                            l_DAT_SCHEDULE_DETAIL_LAST.Add(l_DAT_SCHEDULE_DETAIL);
                        }                       

                    }

                    DAT_SCHEDULE_DETAIL = argDAT_SCHEDULE_DETAIL_LST[0];
                    ScheduleDetailList = argDAT_SCHEDULE_DETAIL_LST;
                    LastMonthList = l_DAT_SCHEDULE_DETAIL_LAST;
                    LastMonth4List = l_DAT_SCHEDULE_DETAIL_LASTMONTH;
                  
                }
                else
                {
                    ScheduleDetailList = new List<DAT_SCHEDULE_DETAIL>();
                    LastMonthList = new List<DAT_SCHEDULE_DETAIL>();
                    LastMonth4List = new List<DAT_SCHEDULE_DETAIL>();
                    
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
                List<DAT_SCHEDULE_DETAIL> l_DAT_SCHEDULE_DETAIL_lst = new List<DAT_SCHEDULE_DETAIL>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_SCHEDULE_DETAIL l_DAT_SCHEDULE_DETAIL in mJSN_RES_OUT.DAT_SCHEDULE_DETAIL)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_SCHEDULE_DETAIL.PayScheduleName.ToLower().Contains(argKeyword)
                            || l_DAT_SCHEDULE_DETAIL.PayItemGroupName.ToLower().Contains(argKeyword)
                            || l_DAT_SCHEDULE_DETAIL.Salary.ToLower().Contains(argKeyword)
                         )
                        {
                            l_DAT_SCHEDULE_DETAIL_lst.Add(l_DAT_SCHEDULE_DETAIL);
                        }
                    }
                }
                else
                {
                    l_DAT_SCHEDULE_DETAIL_lst = mJSN_RES_OUT.DAT_SCHEDULE_DETAIL;// OriginalScheduleDetailList.GetRange(0, OriginalScheduleDetailList.Count);
                }
                bindDataTab(l_DAT_SCHEDULE_DETAIL_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployeeSalary()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mREQ_AUTHORIZATION);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wsgetEmployeeSalary);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_OUT = JsonConvert.DeserializeObject<JSN_RES_OUT>(mResponse);
                    if (mJSN_RES_OUT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_OUT.DAT_SCHEDULE_DETAIL.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_OUT.DAT_SCHEDULE_DETAIL);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_OUT.Message.Message);
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

        public async void saveEmployeeSalary()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mREQ_AUTHORIZATION);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wssaveEmployee);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_OUT = JsonConvert.DeserializeObject<JSN_RES_OUT>(mResponse);
                    if (mJSN_RES_OUT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_OUT.DAT_SCHEDULE_DETAIL.Count > 0)
                        {
                            DAT_SCHEDULE_DETAIL = this.mJSN_RES_OUT.DAT_SCHEDULE_DETAIL[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_OUT.Message.Message);
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

        public async void loadEmployeeSalary()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pay_Service.ApiCall(mRequest, Pay_Name.wsloadPayData);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_PAY = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_PAY>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_PAY.Message.Code == "7")
                    {
                        this.PaySalaryLoad = mJSN_LOAD_EMPLOYEE_PAY;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE_PAY.Message.Message);
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
