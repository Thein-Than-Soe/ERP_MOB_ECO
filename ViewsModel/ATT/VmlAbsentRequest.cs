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
    public class VmlAbsentRequest : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_ABSENT_REQUEST mJSN_REQ_EMPLOYEE_ABSENT_REQUEST = new JSN_REQ_EMPLOYEE_ABSENT_REQUEST();
        public JSN_RES_EMPLOYEE_ABSENT_REQUEST mJSN_RES_EMPLOYEE_ABSENT_REQUEST = new JSN_RES_EMPLOYEE_ABSENT_REQUEST();
        public JSN_RES_LOAD_EMPLOYEE_REQUEST mJSN_RES_LOAD_EMPLOYEE_REQUEST = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAbsentRequest()
        {
            this.switchDisplayView(DisplayView.Card);
            this.AbsentReqLoad = new JSN_RES_LOAD_EMPLOYEE_REQUEST();
            SubmitList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
            ApprovedList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
            RejectList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
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
        public JSN_RES_LOAD_EMPLOYEE_REQUEST AbsentReqLoad
        {
            get { return JSN_RES_LOAD_EMPLOYEE_REQUEST; }
            set { JSN_RES_LOAD_EMPLOYEE_REQUEST = value; NotifyPropertyChanged("AbsentReqLoad"); }
        }


        public DAT_EMPLOYEE_ABSENT_REQUEST mDAT_EMPLOYEE_ABSENT_REQUEST = new DAT_EMPLOYEE_ABSENT_REQUEST();
        public DAT_EMPLOYEE_ABSENT_REQUEST DAT_EMPLOYEE_ABSENT_REQUEST
        {
            get { return mDAT_EMPLOYEE_ABSENT_REQUEST; }
            set { mDAT_EMPLOYEE_ABSENT_REQUEST = value; NotifyPropertyChanged("DAT_EMPLOYEE_ABSENT_REQUEST"); }
        }

        public List<DAT_EMPLOYEE_ABSENT_REQUEST> mAbsentRequestList;
        public List<DAT_EMPLOYEE_ABSENT_REQUEST> AbsentRequestList
        {
            get { return mAbsentRequestList; }
            set { mAbsentRequestList = value; NotifyPropertyChanged("AbsentRequestList"); }
        }

        public List<DAT_EMPLOYEE_ABSENT_REQUEST> mSubmitList;
        public List<DAT_EMPLOYEE_ABSENT_REQUEST> SubmitList
        {
            get { return mSubmitList; }
            set { mSubmitList = value; NotifyPropertyChanged("SubmitList"); }
        }

        public List<DAT_EMPLOYEE_ABSENT_REQUEST> mApprovedList;
        public List<DAT_EMPLOYEE_ABSENT_REQUEST> ApprovedList
        {
            get { return mApprovedList; }
            set { mApprovedList = value; NotifyPropertyChanged("ApprovedList"); }
        }

        public List<DAT_EMPLOYEE_ABSENT_REQUEST> mRejectList;
        public List<DAT_EMPLOYEE_ABSENT_REQUEST> RejectList
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
                    mRefreshCommand = new Command(() => this.getAbsentRequest());
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
        private void bindDataTab(List<DAT_EMPLOYEE_ABSENT_REQUEST> argDAT_EMPLOYEE_ABSENT_REQUEST_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_ABSENT_REQUEST> l_DAT_ABSENT_REQUEST_SUBMIT = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                List<DAT_EMPLOYEE_ABSENT_REQUEST> l_DAT_ABSENT_REQUEST_APPROVED = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                List<DAT_EMPLOYEE_ABSENT_REQUEST> l_DAT_ABSENT_REQUEST_REJECT = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();

                if (argDAT_EMPLOYEE_ABSENT_REQUEST_LST != null && argDAT_EMPLOYEE_ABSENT_REQUEST_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_ABSENT_REQUEST l_DAT_EMPLOYEE_ABSENT_REQUEST in argDAT_EMPLOYEE_ABSENT_REQUEST_LST)
                    {
                        l_DAT_EMPLOYEE_ABSENT_REQUEST.AbsentDate = Utility.getDateTimeString(l_DAT_EMPLOYEE_ABSENT_REQUEST.AbsentDate);

                        if (l_DAT_EMPLOYEE_ABSENT_REQUEST.ApproveStatusAsk.Equals("1"))
                        {
                            l_DAT_ABSENT_REQUEST_SUBMIT.Add(l_DAT_EMPLOYEE_ABSENT_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_ABSENT_REQUEST.ApproveStatusAsk.Equals("2"))
                        {
                            l_DAT_ABSENT_REQUEST_APPROVED.Add(l_DAT_EMPLOYEE_ABSENT_REQUEST);
                        }
                        else if (l_DAT_EMPLOYEE_ABSENT_REQUEST.ApproveStatusAsk.Equals("3"))
                        {
                            l_DAT_ABSENT_REQUEST_REJECT.Add(l_DAT_EMPLOYEE_ABSENT_REQUEST);
                        }
                    }

                    DAT_EMPLOYEE_ABSENT_REQUEST = argDAT_EMPLOYEE_ABSENT_REQUEST_LST[0];
                    AbsentRequestList = argDAT_EMPLOYEE_ABSENT_REQUEST_LST;
                    SubmitList = l_DAT_ABSENT_REQUEST_SUBMIT;
                    ApprovedList = l_DAT_ABSENT_REQUEST_APPROVED;
                    RejectList = l_DAT_ABSENT_REQUEST_REJECT;
                }
                else
                {
                    AbsentRequestList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                    SubmitList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                    ApprovedList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                    RejectList = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
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
                List<DAT_EMPLOYEE_ABSENT_REQUEST> l_DAT_EMPLOYEE_ABSENT_REQUEST_lst = new List<DAT_EMPLOYEE_ABSENT_REQUEST>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_ABSENT_REQUEST l_DAT_EMPLOYEE_ABSENT_REQUEST in mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_ABSENT_REQUEST.AbsentDate.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_ABSENT_REQUEST.AbsentReason.ToLower().Contains(argKeyword)                            
                            || l_DAT_EMPLOYEE_ABSENT_REQUEST.EmployeeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_ABSENT_REQUEST_lst.Add(l_DAT_EMPLOYEE_ABSENT_REQUEST);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_ABSENT_REQUEST_lst = mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST;// OriginalAbsentRequestList.GetRange(0, OriginalAbsentRequestList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_ABSENT_REQUEST_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAbsentRequest()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_ABSENT_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetEmployeeAbsentRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_ABSENT_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_ABSENT_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.Message.Message);
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

        public async void saveAbsentRequest()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_ABSENT_REQUEST);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveEmployeeAbsentRequest);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_ABSENT_REQUEST>(mResponse);
                    if (mJSN_RES_EMPLOYEE_ABSENT_REQUEST.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST.Count > 0)
                        {
                            DAT_EMPLOYEE_ABSENT_REQUEST = this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.Message.Message);
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
                        this.AbsentReqLoad = mJSN_RES_LOAD_EMPLOYEE_REQUEST;
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_ABSENT_REQUEST.Message.Message);
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
