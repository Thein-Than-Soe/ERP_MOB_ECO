using CS.ERP.PL.ACC.DAT;
using CS.ERP.PL.ACC.REQ;
using CS.ERP.PL.ACC.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.ACC;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.ACC
{
    public class VmlAccountPeriod : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_ACCOUNT_PERIOD mJSN_REQ_ACCOUNT_PERIOD = new JSN_REQ_ACCOUNT_PERIOD();
        public JSN_RES_ACCOUNT_PERIOD mJSN_RES_ACCOUNT_PERIOD = new JSN_RES_ACCOUNT_PERIOD();
        public JSN_LOAD_ACCOUNT_PERIOD mJSN_LOAD_ACCOUNT_PERIOD = new JSN_LOAD_ACCOUNT_PERIOD();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAccountPeriod()
        {
            this.switchDisplayView(DisplayView.Card);
            AccountPeriodLoad = new JSN_LOAD_ACCOUNT_PERIOD();
            AccountPeriodList = new List<DAT_ACCOUNT_PERIOD>();
            AccountPeriodOpenList = new List<DAT_ACCOUNT_PERIOD>();
            AccountPeriodClosedList = new List<DAT_ACCOUNT_PERIOD>();
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
        public JSN_LOAD_ACCOUNT_PERIOD JSN_LOAD_ACCOUNT_PERIOD = new JSN_LOAD_ACCOUNT_PERIOD();
        public JSN_LOAD_ACCOUNT_PERIOD AccountPeriodLoad
        {
            get { return JSN_LOAD_ACCOUNT_PERIOD; }
            set { JSN_LOAD_ACCOUNT_PERIOD = value; NotifyPropertyChanged("AccountPeriodLoad"); }
        }

        public DAT_ACCOUNT_PERIOD mDAT_ACCOUNT_PERIOD = new DAT_ACCOUNT_PERIOD();
        public DAT_ACCOUNT_PERIOD DAT_ACCOUNT_PERIOD
        {
            get { return mDAT_ACCOUNT_PERIOD; }
            set { mDAT_ACCOUNT_PERIOD = value; NotifyPropertyChanged("DAT_ACCOUNT_PERIOD"); }
        }

        public List<DAT_ACCOUNT_PERIOD> mAccountPeriodList;
        public List<DAT_ACCOUNT_PERIOD> AccountPeriodList
        {
            get { return mAccountPeriodList; }
            set { mAccountPeriodList = value; NotifyPropertyChanged("AccountPeriodList"); }
        }

        public List<DAT_ACCOUNT_PERIOD> mAccountPeriodOpenList;
        public List<DAT_ACCOUNT_PERIOD> AccountPeriodOpenList
        {
            get { return mAccountPeriodOpenList; }
            set { mAccountPeriodOpenList = value; NotifyPropertyChanged("AccountPeriodOpenList"); }
        }

        public List<DAT_ACCOUNT_PERIOD> mAccountPeriodClosedList;
        public List<DAT_ACCOUNT_PERIOD> AccountPeriodClosedList
        {
            get { return mAccountPeriodClosedList; }
            set { mAccountPeriodClosedList = value; NotifyPropertyChanged("AccountPeriodClosedList"); }
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
                    mRefreshCommand = new Command(() => this.getAccountPeriod());
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
        private void bindDataTab(List<DAT_ACCOUNT_PERIOD> argDAT_ACCOUNT_PERIOD_LST)
        {
            try
            {
                List<DAT_ACCOUNT_PERIOD> l_DAT_ACCOUNT_PERIOD_OPEN = new List<DAT_ACCOUNT_PERIOD>();
                List<DAT_ACCOUNT_PERIOD> l_DAT_ACCOUNT_PERIOD_CLOSED = new List<DAT_ACCOUNT_PERIOD>();

                if (argDAT_ACCOUNT_PERIOD_LST != null && argDAT_ACCOUNT_PERIOD_LST.Count > 0)
                {

                    foreach (DAT_ACCOUNT_PERIOD l_DAT_ACCOUNT_PERIOD in argDAT_ACCOUNT_PERIOD_LST)
                    {
                        l_DAT_ACCOUNT_PERIOD.SD = Utility.getDateTimeString(l_DAT_ACCOUNT_PERIOD.SD);
                        l_DAT_ACCOUNT_PERIOD.ED = Utility.getDateTimeString(l_DAT_ACCOUNT_PERIOD.ED);

                        if (l_DAT_ACCOUNT_PERIOD.StatusAsk.Equals("1"))
                        {
                            l_DAT_ACCOUNT_PERIOD_OPEN.Add(l_DAT_ACCOUNT_PERIOD);
                        }
                        else if (l_DAT_ACCOUNT_PERIOD.StatusAsk.Equals("9"))
                        {
                            l_DAT_ACCOUNT_PERIOD_CLOSED.Add(l_DAT_ACCOUNT_PERIOD);
                        }
                    }

                    DAT_ACCOUNT_PERIOD = argDAT_ACCOUNT_PERIOD_LST[0];
                    AccountPeriodList = argDAT_ACCOUNT_PERIOD_LST;
                    AccountPeriodOpenList = l_DAT_ACCOUNT_PERIOD_OPEN;
                    AccountPeriodClosedList = l_DAT_ACCOUNT_PERIOD_CLOSED;

                }
                else
                {
                    AccountPeriodList = new List<DAT_ACCOUNT_PERIOD>();
                    AccountPeriodOpenList = new List<DAT_ACCOUNT_PERIOD>();
                    AccountPeriodClosedList = new List<DAT_ACCOUNT_PERIOD>();

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
                List<DAT_ACCOUNT_PERIOD> l_DAT_ACCOUNT_PERIOD_lst = new List<DAT_ACCOUNT_PERIOD>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_ACCOUNT_PERIOD l_DAT_ACCOUNT_PERIOD in mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_ACCOUNT_PERIOD.AccountPeriodName_0_255.ToLower().Contains(argKeyword)
                            || l_DAT_ACCOUNT_PERIOD.PeriodTypeName_0_255.ToLower().Contains(argKeyword)
                            || l_DAT_ACCOUNT_PERIOD.PeriodMethodName_0_255.ToLower().Contains(argKeyword))
                        {
                            l_DAT_ACCOUNT_PERIOD_lst.Add(l_DAT_ACCOUNT_PERIOD);
                        }
                    }
                }
                else
                {
                    l_DAT_ACCOUNT_PERIOD_lst = mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD;// OriginalAccountPeriodList.GetRange(0, OriginalAccountPeriodList.Count);
                }
                bindDataTab(l_DAT_ACCOUNT_PERIOD_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAccountPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT_PERIOD);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsgetAccountPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT_PERIOD = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT_PERIOD>(mResponse);
                    if (mJSN_RES_ACCOUNT_PERIOD.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT_PERIOD.Message.Message);
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
        public async void saveAccountPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT_PERIOD);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wssaveAccountPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT_PERIOD = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT_PERIOD>(mResponse);
                    if (mJSN_RES_ACCOUNT_PERIOD.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD.Count > 0)
                        {
                            DAT_ACCOUNT_PERIOD = this.mJSN_RES_ACCOUNT_PERIOD.DAT_ACCOUNT_PERIOD[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT_PERIOD.Message.Message);
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
        public async void loadAccountPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsloadAccountPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_ACCOUNT_PERIOD = JsonConvert.DeserializeObject<JSN_LOAD_ACCOUNT_PERIOD>(mResponse);
                    if (mJSN_LOAD_ACCOUNT_PERIOD.Message.Code == "7")
                    {
                        this.AccountPeriodLoad = mJSN_LOAD_ACCOUNT_PERIOD;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_ACCOUNT_PERIOD.Message.Message);
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
