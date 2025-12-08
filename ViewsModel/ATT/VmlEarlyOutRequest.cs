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
    public class VmlEarlyOutRequest : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST = new JSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST();
        public JSN_RES_EMPLOYEE_EARLY_OUT_REQUEST mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST = new JSN_RES_EMPLOYEE_EARLY_OUT_REQUEST();
        public JSN_RES_LOAD_EMPLOYEE_REQUEST mJSN_RES_LOAD_EMPLOYEE_REQUEST = new JSN_RES_LOAD_EMPLOYEE_REQUEST();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEarlyOutRequest()
        {
            this.switchDisplayView(DisplayView.Card);
            EarlyoutLoad = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
            SubmitList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
            ApprovedList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
            RejectList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
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
        public JSN_RES_LOAD_EMPLOYEE_REQUEST EarlyoutLoad
        {
            get { return JSN_RES_LOAD_EMPLOYEE_REQUEST; }
            set { JSN_RES_LOAD_EMPLOYEE_REQUEST = value; NotifyPropertyChanged("EarlyoutLoad"); }
        }


        public DAT_EMPLOYEE_EARLY_OUT_REQUEST mDAT_EMPLOYEE_EARLY_OUT_REQUEST = new DAT_EMPLOYEE_EARLY_OUT_REQUEST();
        public DAT_EMPLOYEE_EARLY_OUT_REQUEST DAT_EMPLOYEE_EARLY_OUT_REQUEST
        {
            get { return mDAT_EMPLOYEE_EARLY_OUT_REQUEST; }
            set { mDAT_EMPLOYEE_EARLY_OUT_REQUEST = value; NotifyPropertyChanged("DAT_EMPLOYEE_EARLY_OUT_REQUEST"); }
        }

        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> mEarlyOutRequestList;
        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> EarlyOutRequestList
        {
            get { return mEarlyOutRequestList; }
            set { mEarlyOutRequestList = value; NotifyPropertyChanged("EarlyOutRequestList"); }
        }

        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> mSubmitList;
        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> SubmitList
        {
            get { return mSubmitList; }
            set { mSubmitList = value; NotifyPropertyChanged("SubmitList"); }
        }

        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> mApprovedList;
        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> ApprovedList
        {
            get { return mApprovedList; }
            set { mApprovedList = value; NotifyPropertyChanged("ApprovedList"); }
        }

        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> mRejectList;
        public List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> RejectList
        {
            get { return mRejectList; }
            set { mRejectList = value; NotifyPropertyChanged("RejectList"); }
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
                    mRefreshCommand = new Command(() => this.getEarlyOut());
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
        private void bindDataTab(List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_SUBMIT = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_APPROVED = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_REJECT = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();

                if (argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST != null && argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_EARLY_OUT_REQUEST l_DAT_EMPLOYEE_EARLY_OUT_REQUEST in argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST)
                    {
                        l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutDate = Utility.getDateTimeString(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutDate);

                        if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.ApproveStatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_SUBMIT.Add(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.ApproveStatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_APPROVED.Add(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.ApproveStatusAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_REJECT.Add(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST);
                        }
                    }

                    DAT_EMPLOYEE_EARLY_OUT_REQUEST = argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST[0];
                    EarlyOutRequestList = argDAT_EMPLOYEE_EARLY_OUT_REQUEST_LST;
                    SubmitList = l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_SUBMIT;
                    ApprovedList = l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_APPROVED;
                    RejectList = l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_REJECT;
                }
                else
                {
                    EarlyOutRequestList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                    SubmitList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                    ApprovedList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                    RejectList = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
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
                List<DAT_EMPLOYEE_EARLY_OUT_REQUEST> l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_lst = new List<DAT_EMPLOYEE_EARLY_OUT_REQUEST>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_EARLY_OUT_REQUEST l_DAT_EMPLOYEE_EARLY_OUT_REQUEST in mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutDate.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.EarlyOutReason.ToLower().Contains(argKeyword)
                            
                            || l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.EmployeeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_lst.Add(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_lst = mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST;// OriginalEarlyOutRequestList.GetRange(0, OriginalEarlyOutRequestList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_EARLY_OUT_REQUEST_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEarlyOut()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeEarlyOutRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_EARLY_OUT_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.Message.Message);
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

        public async void saveEarlyOut()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeEarlyOutRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_EARLY_OUT_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Count > 0)
                        {
                            DAT_EMPLOYEE_EARLY_OUT_REQUEST = this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_EARLY_OUT_REQUEST.Message.Message);
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

        public async void loadAbsentReq()
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
                        this.EarlyoutLoad = mJSN_RES_LOAD_EMPLOYEE_REQUEST;

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
