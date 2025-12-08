

using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.ATT;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.ATT
{
    public class VmlEmployee : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE mJSN_REQ_EMPLOYEE = new JSN_REQ_EMPLOYEE();
        public JSN_EMPLOYEE mJSN_EMPLOYEE = new JSN_EMPLOYEE();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEmployee()
        {
            this.switchDisplayView(DisplayView.Card);
            SelfEmployeeList = new List<DAT_EMPLOYEE_RO>();
            EmployeeROList = new List<DAT_EMPLOYEE_RO>();
            EmployeeSupervisionList = new List<DAT_EMPLOYEE_RO>();
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
        public DAT_EMPLOYEE_RO mDAT_EMPLOYEE_RO = new DAT_EMPLOYEE_RO();
        public DAT_EMPLOYEE_RO DAT_EMPLOYEE_RO
        {
            get { return mDAT_EMPLOYEE_RO; }
            set { mDAT_EMPLOYEE_RO = value; NotifyPropertyChanged("DAT_EMPLOYEE_RO"); }
        }

        public List<DAT_EMPLOYEE_RO> mEmployeeList;
        public List<DAT_EMPLOYEE_RO> EmployeeList
        {
            get { return mEmployeeList; }
            set { mEmployeeList = value; NotifyPropertyChanged("EmployeeList"); }
        }

        public List<DAT_EMPLOYEE_RO> mSelfEmployeeList;
        public List<DAT_EMPLOYEE_RO> SelfEmployeeList
        {
            get { return mSelfEmployeeList; }
            set { mSelfEmployeeList = value; NotifyPropertyChanged("SelfEmployeeList"); }
        }

        public List<DAT_EMPLOYEE_RO> mEmployeeROList;
        public List<DAT_EMPLOYEE_RO> EmployeeROList
        {
            get { return mEmployeeROList; }
            set { mEmployeeROList = value; NotifyPropertyChanged("EmployeeROList"); }
        }

        public List<DAT_EMPLOYEE_RO> mEmployeeSupervisionList;
        public List<DAT_EMPLOYEE_RO> EmployeeSupervisionList
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
        private void bindDataTab(List<DAT_EMPLOYEE_RO> argDAT_EMPLOYEE_RO_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_RO> l_DAT_EMPLOYEE_SELF = new List<DAT_EMPLOYEE_RO>();
                List<DAT_EMPLOYEE_RO> l_DAT_EMPLOYEE_RO = new List<DAT_EMPLOYEE_RO>();
                List<DAT_EMPLOYEE_RO> l_DAT_EMPLOYEE_SUPERVISION = new List<DAT_EMPLOYEE_RO>();

                if (argDAT_EMPLOYEE_RO_LST != null && argDAT_EMPLOYEE_RO_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_RO l_DAT_EMPLOYEE in argDAT_EMPLOYEE_RO_LST)
                    {
                        

                        if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_SELF.Add(l_DAT_EMPLOYEE);
                        }
                        else if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_RO.Add(l_DAT_EMPLOYEE);
                        }
                        else if (l_DAT_EMPLOYEE.ROTypeAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_SUPERVISION.Add(l_DAT_EMPLOYEE);
                        }

                    }

                    DAT_EMPLOYEE_RO = argDAT_EMPLOYEE_RO_LST[0];
                    EmployeeList = argDAT_EMPLOYEE_RO_LST;
                    SelfEmployeeList = l_DAT_EMPLOYEE_SELF;
                    EmployeeROList = l_DAT_EMPLOYEE_RO;
                    EmployeeSupervisionList = l_DAT_EMPLOYEE_SUPERVISION;
                }
                else
                {
                    EmployeeList = new List<DAT_EMPLOYEE_RO>();
                    SelfEmployeeList = new List<DAT_EMPLOYEE_RO>();
                    EmployeeROList = new List<DAT_EMPLOYEE_RO>();
                    EmployeeSupervisionList = new List<DAT_EMPLOYEE_RO>();
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
                List<DAT_EMPLOYEE_RO> l_DAT_EMPLOYEE_RO_lst = new List<DAT_EMPLOYEE_RO>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_RO l_DAT_EMPLOYEE_RO in mJSN_EMPLOYEE.DAT_EMPLOYEE_RO)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_RO.ROName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_RO.ROTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_RO.EmployeeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_RO_lst.Add(l_DAT_EMPLOYEE_RO);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_RO_lst = mJSN_EMPLOYEE.DAT_EMPLOYEE_RO;// OriginalEmployeeList.GetRange(0, OriginalEmployeeList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_RO_lst);
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
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE = JsonConvert.DeserializeObject<JSN_EMPLOYEE>(mResponse);
                    if (mJSN_EMPLOYEE.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE.DAT_EMPLOYEE_RO.Count > 0)
                        {
                            bindDataTab(this.mJSN_EMPLOYEE.DAT_EMPLOYEE_RO);
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
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE = JsonConvert.DeserializeObject<JSN_EMPLOYEE>(mResponse);
                    if (mJSN_EMPLOYEE.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE.DAT_EMPLOYEE_RO.Count > 0)
                        {
                            DAT_EMPLOYEE_RO = this.mJSN_EMPLOYEE.DAT_EMPLOYEE_RO[0];
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

        #endregion

    }
}
