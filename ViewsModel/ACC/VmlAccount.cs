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
    public class VmlAccount : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_ACCOUNT mJSN_REQ_ACCOUNT = new JSN_REQ_ACCOUNT();
        public JSN_RES_ACCOUNT mJSN_RES_ACCOUNT = new JSN_RES_ACCOUNT();
        public JSN_LOAD_ACCOUNT mJSN_LOAD_ACCOUNT = new JSN_LOAD_ACCOUNT();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAccount()
        {
            this.switchDisplayView(DisplayView.Card);
            AccountLoad = new JSN_LOAD_ACCOUNT();
            AccountList = new List<DAT_ACCOUNT>();
            AccountDebitList = new List<DAT_ACCOUNT>();
            AccountCreditList = new List<DAT_ACCOUNT>();
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
        public JSN_LOAD_ACCOUNT JSN_LOAD_ACCOUNT = new JSN_LOAD_ACCOUNT();
        public JSN_LOAD_ACCOUNT AccountLoad
        {
            get { return JSN_LOAD_ACCOUNT; }
            set { JSN_LOAD_ACCOUNT = value; NotifyPropertyChanged("AccountLoad"); }
        }

        public DAT_ACCOUNT mDAT_ACCOUNT = new DAT_ACCOUNT();
        public DAT_ACCOUNT DAT_ACCOUNT
        {
            get { return mDAT_ACCOUNT; }
            set { mDAT_ACCOUNT = value; NotifyPropertyChanged("DAT_ACCOUNT"); }
        }

        public List<DAT_ACCOUNT> mAccountList;
        public List<DAT_ACCOUNT> AccountList
        {
            get { return mAccountList; }
            set { mAccountList = value; NotifyPropertyChanged("AccountList"); }
        }

        public List<DAT_ACCOUNT> mAccountDebitList;
        public List<DAT_ACCOUNT> AccountDebitList
        {
            get { return mAccountDebitList; }
            set { mAccountDebitList = value; NotifyPropertyChanged("AccountDebitList"); }
        }

        public List<DAT_ACCOUNT> mAccountCreditList;
        public List<DAT_ACCOUNT> AccountCreditList
        {
            get { return mAccountCreditList; }
            set { mAccountCreditList = value; NotifyPropertyChanged("AccountCreditList"); }
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
                    mRefreshCommand = new Command(() => this.getAccount());
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
        private void bindDataTab(List<DAT_ACCOUNT> argDAT_ACCOUNT_LST)
        {
            try
            {
                List<DAT_ACCOUNT> l_DAT_ACCOUNT_DEBIT = new List<DAT_ACCOUNT>();
                List<DAT_ACCOUNT> l_DAT_ACCOUNT_CREDIT = new List<DAT_ACCOUNT>();
               
                if (argDAT_ACCOUNT_LST != null && argDAT_ACCOUNT_LST.Count > 0)
                {
                    foreach (DAT_ACCOUNT l_DAT_ACCOUNT in argDAT_ACCOUNT_LST)
                    {
                        if (l_DAT_ACCOUNT.AccountNatureAsk.Equals("1"))
                        {
                            l_DAT_ACCOUNT_DEBIT.Add(l_DAT_ACCOUNT);
                        }
                        else if (l_DAT_ACCOUNT.AccountNatureAsk.Equals("2"))
                        {
                            l_DAT_ACCOUNT_CREDIT.Add(l_DAT_ACCOUNT);
                        }
                        
                    }

                    DAT_ACCOUNT = argDAT_ACCOUNT_LST[0];
                    AccountList = argDAT_ACCOUNT_LST;
                    AccountDebitList = l_DAT_ACCOUNT_DEBIT;
                    AccountCreditList = l_DAT_ACCOUNT_CREDIT;
                   

                }
                else
                {
                    AccountList = new List<DAT_ACCOUNT>();
                    AccountDebitList = new List<DAT_ACCOUNT>();
                    AccountCreditList = new List<DAT_ACCOUNT>();
                  
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
                List<DAT_ACCOUNT> l_DAT_ACCOUNT_lst = new List<DAT_ACCOUNT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_ACCOUNT l_DAT_ACCOUNT in mJSN_RES_ACCOUNT.DAT_ACCOUNT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_ACCOUNT.AccountName_0_255.ToLower().Contains(argKeyword)
                            || l_DAT_ACCOUNT.AccountCode_0_50.ToLower().Contains(argKeyword)
                             || l_DAT_ACCOUNT.StatusName_0_255.ToLower().Contains(argKeyword)
                            || l_DAT_ACCOUNT.AccountTypeName_0_255.ToLower().Contains(argKeyword))
                        {
                            l_DAT_ACCOUNT_lst.Add(l_DAT_ACCOUNT);
                        }
                    }
                }
                else
                {
                    l_DAT_ACCOUNT_lst = mJSN_RES_ACCOUNT.DAT_ACCOUNT;// OriginalAccountList.GetRange(0, OriginalAccountList.Count);
                }
                bindDataTab(l_DAT_ACCOUNT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAccount()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsgetAccount);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT>(mResponse);
                    if (mJSN_RES_ACCOUNT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT.DAT_ACCOUNT.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_ACCOUNT.DAT_ACCOUNT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT.Message.Message);
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

        public async void saveAccount()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wssaveAccount);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT>(mResponse);
                    if (mJSN_RES_ACCOUNT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT.DAT_ACCOUNT.Count > 0)
                        {
                            DAT_ACCOUNT = this.mJSN_RES_ACCOUNT.DAT_ACCOUNT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT.Message.Message);
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

        public async void loadAccount()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsloadAccount);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_ACCOUNT = JsonConvert.DeserializeObject<JSN_LOAD_ACCOUNT>(mResponse);
                    if (mJSN_LOAD_ACCOUNT.Message.Code == "7")
                    {
                        this.AccountLoad = mJSN_LOAD_ACCOUNT;
                      
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT.Message.Message);
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
