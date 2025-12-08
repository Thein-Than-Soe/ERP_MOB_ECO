using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.HCM;
using Microsoft.Maui.Controls;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.HCM
{
        public class VmlEmployee : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE mJSN_REQ_EMPLOYEE = new JSN_REQ_EMPLOYEE();
        public JSN_EMPLOYEE mJSN_EMPLOYEE = new JSN_EMPLOYEE();
        public JSN_LOAD_EMPLOYEE mJSN_LOAD_EMPLOYEE = new JSN_LOAD_EMPLOYEE();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEmployee()
        {
            this.switchDisplayView(DisplayView.Card);
            EmployeeLoad = new JSN_LOAD_EMPLOYEE();
            SelfEmployeeList = new List<DAT_EMPLOYEE>();
            EmployeeROList = new List<DAT_EMPLOYEE>();
            EmployeeSupervisionList = new List<DAT_EMPLOYEE>();
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
        public JSN_LOAD_EMPLOYEE JSN_LOAD_EMPLOYEE = new JSN_LOAD_EMPLOYEE();
        public JSN_LOAD_EMPLOYEE EmployeeLoad
        {
            get { return JSN_LOAD_EMPLOYEE; }
            set { JSN_LOAD_EMPLOYEE = value; NotifyPropertyChanged("EmployeeLoad"); }
        }

        public DAT_EMPLOYEE mDAT_EMPLOYEE = new DAT_EMPLOYEE();
        public DAT_EMPLOYEE DAT_EMPLOYEE
        {
            get { return mDAT_EMPLOYEE; }
            set { mDAT_EMPLOYEE = value; NotifyPropertyChanged("DAT_EMPLOYEE"); }
        }

        public List<DAT_EMPLOYEE> mEmployeeList;
        public List<DAT_EMPLOYEE> EmployeeList
        {
            get { return mEmployeeList; }
            set { mEmployeeList = value; NotifyPropertyChanged("EmployeeList"); }
        }

        public List<DAT_EMPLOYEE> mSelfEmployeeList;
        public List<DAT_EMPLOYEE> SelfEmployeeList
        {
            get { return mSelfEmployeeList; }
            set { mSelfEmployeeList = value; NotifyPropertyChanged("SelfEmployeeList"); }
        }

        public List<DAT_EMPLOYEE> mEmployeeROList;
        public List<DAT_EMPLOYEE> EmployeeROList
        {
            get { return mEmployeeROList; }
            set { mEmployeeROList = value; NotifyPropertyChanged("EmployeeROList"); }
        }

        public List<DAT_EMPLOYEE> mEmployeeSupervisionList;
        public List<DAT_EMPLOYEE> EmployeeSupervisionList
        {
            get { return mEmployeeSupervisionList; }
            set { mEmployeeSupervisionList = value; NotifyPropertyChanged("EmployeeSupervisionList"); }
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
                    mRefreshCommand = new Command(() => this.getEmployee());
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
        private void bindDataTab(List<DAT_EMPLOYEE> argDAT_EMPLOYEE_LST)
        {
            try
            {
                List<DAT_EMPLOYEE> l_DAT_EMPLOYEE_SELF = new List<DAT_EMPLOYEE>();
                List<DAT_EMPLOYEE> l_DAT_EMP = new List<DAT_EMPLOYEE>();
                List<DAT_EMPLOYEE> l_DAT_EMPLOYEE_SUPERVISION = new List<DAT_EMPLOYEE>();

                if (argDAT_EMPLOYEE_LST != null && argDAT_EMPLOYEE_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE l_DAT_EMPLOYEE in argDAT_EMPLOYEE_LST)
                    {
                        l_DAT_EMPLOYEE.DateofHire = Utility.getDateTimeString(l_DAT_EMPLOYEE.DateofHire);
                        if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_SELF.Add(l_DAT_EMPLOYEE);
                        }
                        else if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("2"))
                        {
                            l_DAT_EMP.Add(l_DAT_EMPLOYEE);
                        }
                        else if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_SUPERVISION.Add(l_DAT_EMPLOYEE);
                        }

                    }

                    DAT_EMPLOYEE = argDAT_EMPLOYEE_LST[0];
                    EmployeeList = argDAT_EMPLOYEE_LST;
                    SelfEmployeeList = l_DAT_EMPLOYEE_SELF;
                    EmployeeROList = l_DAT_EMP;
                    EmployeeSupervisionList = l_DAT_EMPLOYEE_SUPERVISION;
                }
                else
                {
                    EmployeeList = new List<DAT_EMPLOYEE>();
                    SelfEmployeeList = new List<DAT_EMPLOYEE>();
                    EmployeeROList = new List<DAT_EMPLOYEE>();
                    EmployeeSupervisionList = new List<DAT_EMPLOYEE>();
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
                List<DAT_EMPLOYEE> l_DAT_EMPLOYEE_lst = new List<DAT_EMPLOYEE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE l_DAT_EMPLOYEE in mJSN_EMPLOYEE.DAT_EMPLOYEE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE.EmploymentTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE.DesignationName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE.EmployeeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_lst.Add(l_DAT_EMPLOYEE);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_lst = mJSN_EMPLOYEE.DAT_EMPLOYEE;// OriginalEmployeeList.GetRange(0, OriginalEmployeeList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployee()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE = JsonConvert.DeserializeObject<JSN_EMPLOYEE>(mResponse);
                    if (mJSN_EMPLOYEE.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE.DAT_EMPLOYEE.Count > 0)
                        {
                            bindDataTab(this.mJSN_EMPLOYEE.DAT_EMPLOYEE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE.Message.Message);
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

        public async void saveEmployee()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE = JsonConvert.DeserializeObject<JSN_EMPLOYEE>(mResponse);
                    if (mJSN_EMPLOYEE.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE.DAT_EMPLOYEE.Count > 0)
                        {
                            DAT_EMPLOYEE = this.mJSN_EMPLOYEE.DAT_EMPLOYEE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE.Message.Message);
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

        public async void loadEmployee()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployee);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE.Message.Code == "7")
                    {
                        this.EmployeeLoad = mJSN_LOAD_EMPLOYEE;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE.Message.Message);
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
