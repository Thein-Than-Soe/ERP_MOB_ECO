using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.ATT.REQ;
using CS.ERP.PL.ATT.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.ATT;
using Microsoft.Maui.Controls;
using CS.ERP_MOB.General;

using System.Windows.Input;
using System;
using System.Collections.Generic;

namespace CS.ERP_MOB.ViewsModel.ATT
{
    public class VmlEmployeeRoaster : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_ROASTER_JUN mJSN_REQ_EMPLOYEE_ROASTER_JUN = new JSN_REQ_EMPLOYEE_ROASTER_JUN();
        public JSN_RES_EMPLOYEE_ROASTER_JUN mJSN_RES_EMPLOYEE_ROASTER_JUN = new JSN_RES_EMPLOYEE_ROASTER_JUN();
        public JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN mJSN_RES_LOAD_EMPLOYEE_ROASTER_JUN = new JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN();
        DateTime endDate;
        DateTime startDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = ""; 
        #endregion

        #region "Contructor"
        public VmlEmployeeRoaster()
        {
            this.switchDisplayView(DisplayView.Card);
            this.EmployeeRosterLoad = new JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN();
            EmployeeRoasterTodayList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
            EmployeeRoasterLastWeekList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
            EmployeeRoasterLastMonthList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
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
        public JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN = new JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN();
        public JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN EmployeeRosterLoad
        {
            get { return JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN; }
            set { JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN = value; NotifyPropertyChanged("EmployeeRosterLoad"); }
        }


        public DAT_EMPLOYEE_SHIFT_JUN mDAT_EMPLOYEE_SHIFT_JUN = new DAT_EMPLOYEE_SHIFT_JUN();

        public DAT_EMPLOYEE_ROASTER_JUN mDAT_EMPLOYEE_ROASTER_JUN = new DAT_EMPLOYEE_ROASTER_JUN();
        public DAT_EMPLOYEE_SHIFT_JUN DAT_EMPLOYEE_SHIFT_JUN
        {
            get { return mDAT_EMPLOYEE_SHIFT_JUN; }
            set { mDAT_EMPLOYEE_SHIFT_JUN = value; NotifyPropertyChanged("DAT_EMPLOYEE_SHIFT_JUN"); }
        }

        public DAT_EMPLOYEE_ROASTER_JUN DAT_EMPLOYEE_ROASTER_JUN
        {
            get { return mDAT_EMPLOYEE_ROASTER_JUN; }
            set { mDAT_EMPLOYEE_ROASTER_JUN = value; NotifyPropertyChanged("DAT_EMPLOYEE_SHIFT_JUN"); }
        }


        public List<DAT_EMPLOYEE_SHIFT_JUN> mEmployeeRoasterList;
        public List<DAT_EMPLOYEE_SHIFT_JUN> EmployeeRoasterList
        {
            get { return mEmployeeRoasterList; }
            set { mEmployeeRoasterList = value; NotifyPropertyChanged("EmployeeRoasterList"); }
        }

        public List<DAT_EMPLOYEE_SHIFT_JUN> mEmployeeRoasterTodayList;
        public List<DAT_EMPLOYEE_SHIFT_JUN> EmployeeRoasterTodayList
        {
            get { return mEmployeeRoasterTodayList; }
            set { mEmployeeRoasterTodayList = value; NotifyPropertyChanged("EmployeeRoasterTodayList"); }
        }

        public List<DAT_EMPLOYEE_SHIFT_JUN> mEmployeeRoasterLastWeekList;
        public List<DAT_EMPLOYEE_SHIFT_JUN> EmployeeRoasterLastWeekList
        {
            get { return mEmployeeRoasterLastWeekList; }
            set { mEmployeeRoasterLastWeekList = value; NotifyPropertyChanged("EmployeeRoasterLastWeekList"); }
        }

        public List<DAT_EMPLOYEE_SHIFT_JUN> mEmployeeRoasterLastMonthList;
        public List<DAT_EMPLOYEE_SHIFT_JUN> EmployeeRoasterLastMonthList
        {
            get { return mEmployeeRoasterLastMonthList; }
            set { mEmployeeRoasterLastMonthList = value; NotifyPropertyChanged("EmployeeRoasterLastMonthList"); }
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
                    mRefreshCommand = new Command(() => this.getEmployeeRoaster());
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
        private void bindDataTab(List<DAT_EMPLOYEE_SHIFT_JUN> argDAT_EMPLOYEE_SHIFT_JUN_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_SHIFT_JUN> l_DAT_EMPLOYEE_SHIFT_JUN_TODAY = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                List<DAT_EMPLOYEE_SHIFT_JUN> l_DAT_EMPLOYEE_SHIFT_JUN_LASTWEEK = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                List<DAT_EMPLOYEE_SHIFT_JUN> l_DAT_EMPLOYEE_SHIFT_JUN_LASTMONTH = new List<DAT_EMPLOYEE_SHIFT_JUN>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate = DateTime.Parse(todayDate);

                int lastweek = tDate.Day - 7;
                int lastmonth = tDate.Month - 1;

                if (argDAT_EMPLOYEE_SHIFT_JUN_LST != null && argDAT_EMPLOYEE_SHIFT_JUN_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_SHIFT_JUN l_DAT_EMPLOYEE_SHIFT_JUN in argDAT_EMPLOYEE_SHIFT_JUN_LST)
                    {
                        l_DAT_EMPLOYEE_SHIFT_JUN.SD = Utility.getDateTimeString(l_DAT_EMPLOYEE_SHIFT_JUN.SD);
                        l_DAT_EMPLOYEE_SHIFT_JUN.ED = Utility.getDateTimeString(l_DAT_EMPLOYEE_SHIFT_JUN.ED);


                        if (startDate.Month <= lastmonth && endDate.Month <= lastmonth)
                        {
                            l_DAT_EMPLOYEE_SHIFT_JUN_TODAY.Add(l_DAT_EMPLOYEE_SHIFT_JUN);
                        }
                        else if (startDate.Day >= lastweek && endDate.Day >= lastweek)
                        {
                            l_DAT_EMPLOYEE_SHIFT_JUN_LASTWEEK.Add(l_DAT_EMPLOYEE_SHIFT_JUN);
                        }
                        else if (startDate.Day.Equals(tDate.Day) && endDate.Day.Equals(tDate.Day))
                        {
                            l_DAT_EMPLOYEE_SHIFT_JUN_LASTMONTH.Add(l_DAT_EMPLOYEE_SHIFT_JUN);
                        }

                    }

                    DAT_EMPLOYEE_SHIFT_JUN = argDAT_EMPLOYEE_SHIFT_JUN_LST[0];
                    EmployeeRoasterList = argDAT_EMPLOYEE_SHIFT_JUN_LST;
                    EmployeeRoasterTodayList = l_DAT_EMPLOYEE_SHIFT_JUN_TODAY;
                    EmployeeRoasterLastWeekList = l_DAT_EMPLOYEE_SHIFT_JUN_LASTWEEK;
                    EmployeeRoasterLastMonthList = l_DAT_EMPLOYEE_SHIFT_JUN_LASTMONTH;

                }
                else
                {
                    EmployeeRoasterList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                    EmployeeRoasterTodayList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                    EmployeeRoasterLastWeekList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                    EmployeeRoasterLastMonthList = new List<DAT_EMPLOYEE_SHIFT_JUN>();
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
                List<DAT_EMPLOYEE_SHIFT_JUN> l_DAT_EMPLOYEE_SHIFT_JUN_lst = new List<DAT_EMPLOYEE_SHIFT_JUN>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_SHIFT_JUN l_DAT_EMPLOYEE_SHIFT_JUN in mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_SHIFT_JUN)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_SHIFT_JUN.ShiftName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_SHIFT_JUN.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_SHIFT_JUN.ShiftTypeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_SHIFT_JUN_lst.Add(l_DAT_EMPLOYEE_SHIFT_JUN);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_SHIFT_JUN_lst = mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_SHIFT_JUN;// OriginalEmployeeRoasterList.GetRange(0, OriginalEmployeeRoasterList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_SHIFT_JUN_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployeeRoaster()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_ROASTER_JUN);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeRoaster);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_ROASTER_JUN = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_ROASTER_JUN>(mResponse);
                    if (mJSN_RES_EMPLOYEE_ROASTER_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_ROASTER_JUN.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_SHIFT_JUN);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ROASTER_JUN.Message.Message);
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

        public async void saveEmployeeRoaster()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_ROASTER_JUN);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeRoaster);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_ROASTER_JUN = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_ROASTER_JUN>(mResponse);
                    if (mJSN_RES_EMPLOYEE_ROASTER_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_ROASTER_JUN.Count > 0)
                        {
                            DAT_EMPLOYEE_SHIFT_JUN = this.mJSN_RES_EMPLOYEE_ROASTER_JUN.DAT_EMPLOYEE_SHIFT_JUN[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ROASTER_JUN.Message.Message);
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

        public async void LoadEmployeeRoaster()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsloadEmployeeRoaster);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_LOAD_EMPLOYEE_ROASTER_JUN = JsonConvert.DeserializeObject<JSN_RES_LOAD_EMPLOYEE_ROASTER_JUN>(mResponse);
                    if (mJSN_RES_LOAD_EMPLOYEE_ROASTER_JUN.Message.Code == "7")
                    {
                        this.EmployeeRosterLoad = mJSN_RES_LOAD_EMPLOYEE_ROASTER_JUN;

                        //if (this.mJSN_RES_LOAD_EMPLOYEE_ROASTER_JUN.RES_SUPPLIER.Count > 0)
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ROASTER_JUN.Message.Message);
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
