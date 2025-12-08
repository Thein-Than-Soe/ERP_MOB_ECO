using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.POS
{
    public class VmlStockIssue : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_INVENTORY_ISSUE_JUN mJSN_REQ_INVENTORY_ISSUE_JUN = new JSN_REQ_INVENTORY_ISSUE_JUN();
        public JSN_INVENTORY_ISSUE_JUN mJSN_INVENTORY_ISSUE_JUN = new JSN_INVENTORY_ISSUE_JUN();
        public JSN_LOAD_INVENTRY_ISSUE mJSN_LOAD_INVENTRY_ISSUE = new JSN_LOAD_INVENTRY_ISSUE();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlStockIssue()
        {
            this.switchDisplayView(DisplayView.Card);
            IssueLoad = new JSN_LOAD_INVENTRY_ISSUE();
            IssueClosedList = new List<RES_INVENTORY_ISSUE>();
            IssueActiveList = new List<RES_INVENTORY_ISSUE>();
            IssuePartialList = new List<RES_INVENTORY_ISSUE>();
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
        public JSN_LOAD_INVENTRY_ISSUE JSN_LOAD_INVENTRY_ISSUE = new JSN_LOAD_INVENTRY_ISSUE();
        public JSN_LOAD_INVENTRY_ISSUE IssueLoad
        {
            get { return JSN_LOAD_INVENTRY_ISSUE; }
            set { JSN_LOAD_INVENTRY_ISSUE = value; NotifyPropertyChanged("IssueLoad"); }
        }

        public RES_INVENTORY_ISSUE mRES_INVENTORY_ISSUE = new RES_INVENTORY_ISSUE();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_SALE_BROWSE rES_SALE_BROWSE = new RES_SALE_BROWSE();
        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_SALE_BROWSE RES_SALE_BROWSE
        {
            get { return rES_SALE_BROWSE; }
            set { rES_SALE_BROWSE = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_INVENTORY_ISSUE RES_INVENTORY_ISSUE
        {
            get { return mRES_INVENTORY_ISSUE; }
            set { mRES_INVENTORY_ISSUE = value; NotifyPropertyChanged("RES_INVENTORY_ISSUE"); }
        }

        public List<RES_INVENTORY_ISSUE> mIssueClosedList;
        public List<RES_INVENTORY_ISSUE> IssueClosedList
        {
            get { return mIssueClosedList; }
            set { mIssueClosedList = value; NotifyPropertyChanged("IssueClosedList"); }
        }

        public List<RES_INVENTORY_ISSUE> mIssueActiveList;
        public List<RES_INVENTORY_ISSUE> IssueActiveList
        {
            get { return mIssueActiveList; }
            set { mIssueActiveList = value; NotifyPropertyChanged("IssueActiveList"); }
        }

        public List<RES_INVENTORY_ISSUE> mIssuePartialList;
        public List<RES_INVENTORY_ISSUE> IssuePartialList
        {
            get { return mIssuePartialList; }
            set { mIssuePartialList = value; NotifyPropertyChanged("IssuePartialList"); }
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
                    mRefreshCommand = new Command(() => this.getStockIssue());
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
        private void bindDataTab(List<RES_INVENTORY_ISSUE> argRES_INVENTORY_ISSUE_LST)
        {
            try
            {
                List<RES_INVENTORY_ISSUE> l_RES_INVENTORY_ISSUE_ACTIVE = new List<RES_INVENTORY_ISSUE>();
                List<RES_INVENTORY_ISSUE> l_RES_INVENTORY_ISSUE_PARTIAL = new List<RES_INVENTORY_ISSUE>();
                List<RES_INVENTORY_ISSUE> l_RES_INVENTORY_ISSUE_CLOSED = new List<RES_INVENTORY_ISSUE>();


                if (argRES_INVENTORY_ISSUE_LST != null && argRES_INVENTORY_ISSUE_LST.Count > 0)
                {
                    foreach (RES_INVENTORY_ISSUE l_RES_INVENTORY_ISSUE in argRES_INVENTORY_ISSUE_LST)
                    {   
                        l_RES_INVENTORY_ISSUE.IssueDate = Utility.getDateTimeString(l_RES_INVENTORY_ISSUE.IssueDate);

                        if (l_RES_INVENTORY_ISSUE.StatusAsk == "1")
                        {
                            l_RES_INVENTORY_ISSUE_ACTIVE.Add(l_RES_INVENTORY_ISSUE);
                        }
                        else if (l_RES_INVENTORY_ISSUE.StatusAsk == "8")
                        {
                            l_RES_INVENTORY_ISSUE_PARTIAL.Add(l_RES_INVENTORY_ISSUE);
                        }
                        else if (l_RES_INVENTORY_ISSUE.StatusAsk == "3")
                        {
                            l_RES_INVENTORY_ISSUE_CLOSED.Add(l_RES_INVENTORY_ISSUE);
                        }
                    }
                    RES_INVENTORY_ISSUE = argRES_INVENTORY_ISSUE_LST[0];
                    IssueClosedList = l_RES_INVENTORY_ISSUE_CLOSED;
                    IssueActiveList = l_RES_INVENTORY_ISSUE_ACTIVE;
                    IssuePartialList = l_RES_INVENTORY_ISSUE_PARTIAL;
                }
                else
                {
                    IssueClosedList = new List<RES_INVENTORY_ISSUE>();
                    IssueActiveList = new List<RES_INVENTORY_ISSUE>();
                    IssuePartialList = new List<RES_INVENTORY_ISSUE>();
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
                List<RES_INVENTORY_ISSUE> l_RES_INVENTORY_ISSUE_lst = new List<RES_INVENTORY_ISSUE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_INVENTORY_ISSUE l_RES_INVENTORY_ISSUE in mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_INVENTORY_ISSUE.IssueCode.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_ISSUE.IssueDate.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_ISSUE.IssueTypeName.ToLower().Contains(argKeyword))
                        {
                            l_RES_INVENTORY_ISSUE_lst.Add(l_RES_INVENTORY_ISSUE);
                        }
                    }
                }
                else
                {
                    l_RES_INVENTORY_ISSUE_lst = mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE;// OriginalIssueClosedList.GetRange(0, OriginalIssueClosedList.Count);
                }
                bindDataTab(l_RES_INVENTORY_ISSUE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getStockIssue()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_ISSUE_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventryIssueJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_ISSUE_JUN = JsonConvert.DeserializeObject<JSN_INVENTORY_ISSUE_JUN>(mResponse);
                    if (mJSN_INVENTORY_ISSUE_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE.Count > 0)
                        {
                            bindDataTab(this.mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_ISSUE_JUN.Message.Message);
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
        public async void saveStockIssue()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_ISSUE_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveInventryIssueJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_ISSUE_JUN = JsonConvert.DeserializeObject<JSN_INVENTORY_ISSUE_JUN>(mResponse);
                    if (mJSN_INVENTORY_ISSUE_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE.Count > 0)
                        {
                            RES_INVENTORY_ISSUE = this.mJSN_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_ISSUE_JUN.Message.Message);
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

        public async void loadStockIssue()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadInventryIssue);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_INVENTRY_ISSUE = JsonConvert.DeserializeObject<JSN_LOAD_INVENTRY_ISSUE>(mResponse);
                    if (mJSN_LOAD_INVENTRY_ISSUE.Message.Code == "7")
                    {
                        this.IssueLoad = mJSN_LOAD_INVENTRY_ISSUE;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_INVENTRY_ISSUE.Message.Message);
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
