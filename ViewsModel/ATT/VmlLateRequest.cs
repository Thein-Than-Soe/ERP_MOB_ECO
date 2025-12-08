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
    public class VmlLateRequest : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_LATE_REQUEST mJSN_REQ_EMPLOYEE_LATE_REQUEST = new JSN_REQ_EMPLOYEE_LATE_REQUEST();
        public JSN_RES_EMPLOYEE_LATE_REQUEST mJSN_RES_EMPLOYEE_LATE_REQUEST = new JSN_RES_EMPLOYEE_LATE_REQUEST();
        public JSN_RES_LOAD_EMPLOYEE_REQUEST mJSN_RES_LOAD_EMPLOYEE_REQUEST = new JSN_RES_LOAD_EMPLOYEE_REQUEST();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlLateRequest()
        {
            this.switchDisplayView(DisplayView.Card);
            LateLoad = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
            SubmitList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
            ApprovedList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
            RejectList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
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
        public JSN_RES_LOAD_EMPLOYEE_REQUEST LateLoad
        {
            get { return JSN_RES_LOAD_EMPLOYEE_REQUEST; }
            set { JSN_RES_LOAD_EMPLOYEE_REQUEST = value; NotifyPropertyChanged("LateLoad"); }
        }

        public DAT_EMPLOYEE_LATE_REQUEST mDAT_EMPLOYEE_LATE_REQUEST = new DAT_EMPLOYEE_LATE_REQUEST();
        public DAT_EMPLOYEE_LATE_REQUEST DAT_EMPLOYEE_LATE_REQUEST
        {
            get { return mDAT_EMPLOYEE_LATE_REQUEST; }
            set { mDAT_EMPLOYEE_LATE_REQUEST = value; NotifyPropertyChanged("DAT_EMPLOYEE_LATE_REQUEST"); }
        }

        public List<DAT_EMPLOYEE_LATE_REQUEST> mLateRequestList;
        public List<DAT_EMPLOYEE_LATE_REQUEST> LateRequestList
        {
            get { return mLateRequestList; }
            set { mLateRequestList = value; NotifyPropertyChanged("LateRequestList"); }
        }

        public List<DAT_EMPLOYEE_LATE_REQUEST> mSubmitList;
        public List<DAT_EMPLOYEE_LATE_REQUEST> SubmitList
        {
            get { return mSubmitList; }
            set { mSubmitList = value; NotifyPropertyChanged("SubmitList"); }
        }

        public List<DAT_EMPLOYEE_LATE_REQUEST> mApprovedList;
        public List<DAT_EMPLOYEE_LATE_REQUEST> ApprovedList
        {
            get { return mApprovedList; }
            set { mApprovedList = value; NotifyPropertyChanged("ApprovedList"); }
        }

        public List<DAT_EMPLOYEE_LATE_REQUEST> mRejectList;
        public List<DAT_EMPLOYEE_LATE_REQUEST> RejectList
        {
            get { return mRejectList; }
            set { mRejectList = value; NotifyPropertyChanged("mRejectList"); }
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
                    mRefreshCommand = new Command(() => this.getLateRequest());
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
        private void bindDataTab(List<DAT_EMPLOYEE_LATE_REQUEST> argDAT_EMPLOYEE_LATE_REQUEST_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_LATE_REQUEST> l_DAT_EMPLOYEE_LATE_REQUEST_SUBMIT = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                List<DAT_EMPLOYEE_LATE_REQUEST> l_DAT_EMPLOYEE_LATE_REQUEST_APPROVED = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                List<DAT_EMPLOYEE_LATE_REQUEST> l_DAT_EMPLOYEE_LATE_REQUEST_REJECTED = new List<DAT_EMPLOYEE_LATE_REQUEST>();

                if (argDAT_EMPLOYEE_LATE_REQUEST_LST != null && argDAT_EMPLOYEE_LATE_REQUEST_LST.Count > 0)
                {
                    foreach (DAT_EMPLOYEE_LATE_REQUEST l_DAT_EMPLOYEE_LATE_REQUEST in argDAT_EMPLOYEE_LATE_REQUEST_LST)
                    {
                        l_DAT_EMPLOYEE_LATE_REQUEST.LateDate = Utility.getDateTimeString(l_DAT_EMPLOYEE_LATE_REQUEST.LateDate);
                        
                        if (l_DAT_EMPLOYEE_LATE_REQUEST.ApproveStatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_LATE_REQUEST_SUBMIT.Add(l_DAT_EMPLOYEE_LATE_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_LATE_REQUEST.ApproveStatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_LATE_REQUEST_APPROVED.Add(l_DAT_EMPLOYEE_LATE_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_LATE_REQUEST.ApproveStatusAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_LATE_REQUEST_REJECTED.Add(l_DAT_EMPLOYEE_LATE_REQUEST);
                        }
                    }

                    DAT_EMPLOYEE_LATE_REQUEST = argDAT_EMPLOYEE_LATE_REQUEST_LST[0];
                    LateRequestList = argDAT_EMPLOYEE_LATE_REQUEST_LST;
                    SubmitList = l_DAT_EMPLOYEE_LATE_REQUEST_SUBMIT;
                    ApprovedList = l_DAT_EMPLOYEE_LATE_REQUEST_APPROVED;
                    RejectList = l_DAT_EMPLOYEE_LATE_REQUEST_REJECTED;
                }
                else
                {
                    LateRequestList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                    SubmitList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                    ApprovedList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                    RejectList = new List<DAT_EMPLOYEE_LATE_REQUEST>();
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
                List<DAT_EMPLOYEE_LATE_REQUEST> l_DAT_EMPLOYEE_LATE_REQUEST_lst = new List<DAT_EMPLOYEE_LATE_REQUEST>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_LATE_REQUEST l_DAT_EMPLOYEE_LATE_REQUEST in mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_LATE_REQUEST.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_LATE_REQUEST.LateDate.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_LATE_REQUEST.LateReason.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_LATE_REQUEST_lst.Add(l_DAT_EMPLOYEE_LATE_REQUEST);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_LATE_REQUEST_lst = mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST;// OriginalLateRequestList.GetRange(0, OriginalLateRequestList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_LATE_REQUEST_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getLateRequest()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_LATE_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeLateRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_LATE_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_LATE_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_LATE_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_LATE_REQUEST.Message.Message);
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

        public async void saveLateRequest()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_LATE_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeLateRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_LATE_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_LATE_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_LATE_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST.Count > 0)
                        {
                            DAT_EMPLOYEE_LATE_REQUEST = this.mJSN_RES_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_LATE_REQUEST.Message.Message);
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

        public async void loadLateReq()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsloadAbsentReq);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_LOAD_EMPLOYEE_REQUEST = JsonConvert.DeserializeObject<JSN_RES_LOAD_EMPLOYEE_REQUEST>(mResponse);
                    if (mJSN_RES_LOAD_EMPLOYEE_REQUEST.Message.Code == "7")
                    {
                        this.LateLoad = mJSN_RES_LOAD_EMPLOYEE_REQUEST;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_LOAD_EMPLOYEE_REQUEST.Message.Message);
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
