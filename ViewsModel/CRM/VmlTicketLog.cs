using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.CRM.REQ;
using CS.ERP.PL.CRM.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.CRM;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.CRM
{
    public class VmlTicketLog : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_TICKET_LOG mJSN_REQ_TICKET_LOG = new JSN_REQ_TICKET_LOG();
        public JSN_RES_TICKET_LOG mJSN_RES_TICKET_LOG = new JSN_RES_TICKET_LOG();
        public JSN_LOAD_TICKET_LOG mJSN_LOAD_TICKET_LOG = new JSN_LOAD_TICKET_LOG();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlTicketLog()
        {
            this.switchDisplayView(DisplayView.Card);
            TicketLogLoad = new JSN_LOAD_TICKET_LOG();
            TicketLogList = new List<DAT_TICKET_LOG>();
            NewRequestList = new List<DAT_TICKET_LOG>();
            ChangeRequestList = new List<DAT_TICKET_LOG>();
            ErrorRequestList = new List<DAT_TICKET_LOG>();
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
        public JSN_LOAD_TICKET_LOG JSN_LOAD_TICKET_LOG = new JSN_LOAD_TICKET_LOG();
        public JSN_LOAD_TICKET_LOG TicketLogLoad
        {
            get { return JSN_LOAD_TICKET_LOG; }
            set { JSN_LOAD_TICKET_LOG = value; NotifyPropertyChanged("ReleaseLoad"); }
        }

        public DAT_TICKET_LOG mDAT_TICKET_LOG = new DAT_TICKET_LOG();
        public DAT_TICKET_LOG DAT_TICKET_LOG
        {
            get { return mDAT_TICKET_LOG; }
            set { mDAT_TICKET_LOG = value; NotifyPropertyChanged("DAT_TICKET_LOG"); }
        }

        public List<DAT_TICKET_LOG> mTicketLogList;
        public List<DAT_TICKET_LOG> TicketLogList
        {
            get { return mTicketLogList; }
            set { mTicketLogList = value; NotifyPropertyChanged("TicketLogList"); }
        }

        public List<DAT_TICKET_LOG> mNewRequestList;
        public List<DAT_TICKET_LOG> NewRequestList
        {
            get { return mNewRequestList; }
            set { mNewRequestList = value; NotifyPropertyChanged("NewRequestList"); }
        }

        public List<DAT_TICKET_LOG> mChangeRequestList;
        public List<DAT_TICKET_LOG> ChangeRequestList
        {
            get { return mChangeRequestList; }
            set { mChangeRequestList = value; NotifyPropertyChanged("ChangeRequestList"); }
        }

        public List<DAT_TICKET_LOG> mErrorRequestList;
        public List<DAT_TICKET_LOG> ErrorRequestList
        {
            get { return mErrorRequestList; }
            set { mErrorRequestList = value; NotifyPropertyChanged("ErrorRequestList"); }
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
                    mRefreshCommand = new Command(() => this.getTicketLog());
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
        private void bindDataTab(List<DAT_TICKET_LOG> argDAT_TICKET_LOG_LST)
        {
            try
            {
                List<DAT_TICKET_LOG> l_DAT_TICKET_LOG_NEW = new List<DAT_TICKET_LOG>();
                List<DAT_TICKET_LOG> l_DAT_TICKET_LOG_CHANGE = new List<DAT_TICKET_LOG>();
                List<DAT_TICKET_LOG> l_DAT_TICKET_LOG_ERROR = new List<DAT_TICKET_LOG>();

                if (argDAT_TICKET_LOG_LST != null && argDAT_TICKET_LOG_LST.Count > 0)
                {
                    foreach (DAT_TICKET_LOG l_DAT_TICKET_LOG in argDAT_TICKET_LOG_LST)
                    {
                        if (l_DAT_TICKET_LOG.TicketTypeAsk.Equals("1"))
                        {
                            l_DAT_TICKET_LOG_NEW.Add(l_DAT_TICKET_LOG);
                        }
                        else if (l_DAT_TICKET_LOG.StatusAsk.Equals("2"))
                        {
                            l_DAT_TICKET_LOG_CHANGE.Add(l_DAT_TICKET_LOG);
                        }
                        else if (l_DAT_TICKET_LOG.StatusAsk.Equals("4") || l_DAT_TICKET_LOG.StatusAsk.Equals("5") || l_DAT_TICKET_LOG.StatusAsk.Equals("10"))
                        {
                            l_DAT_TICKET_LOG_ERROR.Add(l_DAT_TICKET_LOG);
                        }
                    }
                    DAT_TICKET_LOG = argDAT_TICKET_LOG_LST[0];
                    TicketLogList = argDAT_TICKET_LOG_LST;
                    NewRequestList = l_DAT_TICKET_LOG_NEW;
                    ChangeRequestList = l_DAT_TICKET_LOG_CHANGE;
                    ErrorRequestList = l_DAT_TICKET_LOG_ERROR;

                }
                else
                {
                    TicketLogList = new List<DAT_TICKET_LOG>();
                    NewRequestList = new List<DAT_TICKET_LOG>();
                    ChangeRequestList = new List<DAT_TICKET_LOG>();
                    ErrorRequestList = new List<DAT_TICKET_LOG>();
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
                List<DAT_TICKET_LOG> l_DAT_TICKET_LOG_lst = new List<DAT_TICKET_LOG>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_TICKET_LOG l_DAT_TICKET_LOG in mJSN_RES_TICKET_LOG.DAT_TICKET_LOG)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_TICKET_LOG.TicketLogCode.ToLower().Contains(argKeyword)
                            || l_DAT_TICKET_LOG.TicketLogName.ToLower().Contains(argKeyword)
                             || l_DAT_TICKET_LOG.ResolvedbyName.ToLower().Contains(argKeyword)
                            || l_DAT_TICKET_LOG.TicketTypeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_TICKET_LOG_lst.Add(l_DAT_TICKET_LOG);
                        }
                    }
                }
                else
                {
                    l_DAT_TICKET_LOG_lst = mJSN_RES_TICKET_LOG.DAT_TICKET_LOG;// OriginalTicketLogList.GetRange(0, OriginalTicketLogList.Count);
                }
                bindDataTab(l_DAT_TICKET_LOG_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getTicketLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_TICKET_LOG);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetTicketLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_TICKET_LOG = JsonConvert.DeserializeObject<JSN_RES_TICKET_LOG>(mResponse);
                    if (mJSN_RES_TICKET_LOG.Message.Code == "7")
                    {
                        if (this.mJSN_RES_TICKET_LOG.DAT_TICKET_LOG.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_TICKET_LOG.DAT_TICKET_LOG);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_TICKET_LOG.Message.Message);
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

        public async void saveTicketLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_TICKET_LOG);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wssaveTicketLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_TICKET_LOG = JsonConvert.DeserializeObject<JSN_RES_TICKET_LOG>(mResponse);
                    if (mJSN_RES_TICKET_LOG.Message.Code == "7")
                    {
                        if (this.mJSN_RES_TICKET_LOG.DAT_TICKET_LOG.Count > 0)
                        {
                            DAT_TICKET_LOG = this.mJSN_RES_TICKET_LOG.DAT_TICKET_LOG[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_TICKET_LOG.Message.Message);
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

        public async void LoadTicketLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsloadTicketLog);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_TICKET_LOG = JsonConvert.DeserializeObject<JSN_LOAD_TICKET_LOG>(mResponse);
                    if (mJSN_LOAD_TICKET_LOG.Message.Code == "7")
                    {
                        this.TicketLogLoad = mJSN_LOAD_TICKET_LOG;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_TICKET_LOG.Message.Message);
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
