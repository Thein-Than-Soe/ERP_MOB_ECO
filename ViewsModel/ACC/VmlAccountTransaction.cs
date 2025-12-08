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
    public class VmlAccountTransaction : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_ACCOUNT_TRANSACTION mJSN_REQ_ACCOUNT_TRANSACTION = new JSN_REQ_ACCOUNT_TRANSACTION();
        public JSN_RES_ACCOUNT_TRANSACTION mJSN_RES_ACCOUNT_TRANSACTION = new JSN_RES_ACCOUNT_TRANSACTION();
        public JSN_LOAD_ACCOUNT_TRANSACTION mJSN_LOAD_ACCOUNT_TRANSACTION = new JSN_LOAD_ACCOUNT_TRANSACTION();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAccountTransaction()
        {
            this.switchDisplayView(DisplayView.Card);
            this.AccountTransaction = new JSN_LOAD_ACCOUNT_TRANSACTION();
            AccountTransactionList = new List<DAT_ACCOUNT_TRANSACTION>();
            AccountTransactionDebitList = new List<DAT_ACCOUNT_TRANSACTION>();
            AccountTransactionCreditList = new List<DAT_ACCOUNT_TRANSACTION>();
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

        public JSN_LOAD_ACCOUNT_TRANSACTION JSN_LOAD_ACCOUNT_TRANSACTION = new JSN_LOAD_ACCOUNT_TRANSACTION();
        public JSN_LOAD_ACCOUNT_TRANSACTION AccountTransaction
        {
            get { return JSN_LOAD_ACCOUNT_TRANSACTION; }
            set { JSN_LOAD_ACCOUNT_TRANSACTION = value; NotifyPropertyChanged("AccountTransaction"); }
        }

        public DAT_ACCOUNT_TRANSACTION mDAT_ACCOUNT_TRANSACTION = new DAT_ACCOUNT_TRANSACTION();
        public DAT_ACCOUNT_TRANSACTION DAT_ACCOUNT_TRANSACTION
        {
            get { return mDAT_ACCOUNT_TRANSACTION; }
            set { mDAT_ACCOUNT_TRANSACTION = value; NotifyPropertyChanged("DAT_ACCOUNT_TRANSACTION"); }
        }

        public List<DAT_ACCOUNT_TRANSACTION> mAccountTransactionList;
        public List<DAT_ACCOUNT_TRANSACTION> AccountTransactionList
        {
            get { return mAccountTransactionList; }
            set { mAccountTransactionList = value; NotifyPropertyChanged("AccountTransactionList"); }
        }

        public List<DAT_ACCOUNT_TRANSACTION> mAccountTransactionDebitList;
        public List<DAT_ACCOUNT_TRANSACTION> AccountTransactionDebitList
        {
            get { return mAccountTransactionDebitList; }
            set { mAccountTransactionDebitList = value; NotifyPropertyChanged("AccountTransactionDebitList"); }
        }

        public List<DAT_ACCOUNT_TRANSACTION> mAccountTransactionCreditList;
        public List<DAT_ACCOUNT_TRANSACTION> AccountTransactionCreditList
        {
            get { return mAccountTransactionCreditList; }
            set { mAccountTransactionCreditList = value; NotifyPropertyChanged("AccountTransactionCreditList"); }
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
                    mRefreshCommand = new Command(() => this.getAccountTransaction());
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
        private void bindDataTab(List<DAT_ACCOUNT_TRANSACTION> argDAT_ACCOUNT_TRANSACTION_LST)
        {
            try
            {
                List<DAT_ACCOUNT_TRANSACTION> l_DAT_ACCOUNT_TRANSACTION_DEBIT = new List<DAT_ACCOUNT_TRANSACTION>();
                List<DAT_ACCOUNT_TRANSACTION> l_DAT_ACCOUNT_TRANSACTION_CREDIT = new List<DAT_ACCOUNT_TRANSACTION>();

                if (argDAT_ACCOUNT_TRANSACTION_LST != null && argDAT_ACCOUNT_TRANSACTION_LST.Count > 0)
                {
                    foreach (DAT_ACCOUNT_TRANSACTION l_DAT_ACCOUNT_TRANSACTION in argDAT_ACCOUNT_TRANSACTION_LST)
                    {
                        //if (l_DAT_ACCOUNT_TRANSACTION.AccountNatureAsk.Equals("1"))
                        //{
                        //    l_DAT_ACCOUNT_TRANSACTION_DEBIT.Add(l_DAT_ACCOUNT_TRANSACTION);
                        //}
                        //else if (l_DAT_ACCOUNT_TRANSACTION.AccountNatureAsk.Equals("2"))
                        //{
                        //    l_DAT_ACCOUNT_TRANSACTION_CREDIT.Add(l_DAT_ACCOUNT_TRANSACTION);
                        //}

                    }

                    DAT_ACCOUNT_TRANSACTION = argDAT_ACCOUNT_TRANSACTION_LST[0];
                    AccountTransactionList = argDAT_ACCOUNT_TRANSACTION_LST;
                    AccountTransactionDebitList = l_DAT_ACCOUNT_TRANSACTION_DEBIT;
                    AccountTransactionCreditList = l_DAT_ACCOUNT_TRANSACTION_CREDIT;


                }
                else
                {
                    AccountTransactionList = new List<DAT_ACCOUNT_TRANSACTION>();
                    AccountTransactionDebitList = new List<DAT_ACCOUNT_TRANSACTION>();
                    AccountTransactionCreditList = new List<DAT_ACCOUNT_TRANSACTION>();

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
                List<DAT_ACCOUNT_TRANSACTION> l_DAT_ACCOUNT_TRANSACTION_lst = new List<DAT_ACCOUNT_TRANSACTION>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_ACCOUNT_TRANSACTION l_DAT_ACCOUNT_TRANSACTION in mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION)
                    {
                        argKeyword = argKeyword.ToLower();
                        //if (l_DAT_ACCOUNT_TRANSACTION.AccountTransactionCode.ToLower().Contains(argKeyword)
                        //    || l_DAT_ACCOUNT_TRANSACTION.AccountTransactionName.ToLower().Contains(argKeyword)
                        //    || l_DAT_ACCOUNT_TRANSACTION.AccountTransactionCode.ToLower().Contains(argKeyword)
                        //    || l_DAT_ACCOUNT_TRANSACTION.CurrencyName.ToLower().Contains(argKeyword))
                        //{
                        //    l_DAT_ACCOUNT_TRANSACTION_lst.Add(l_DAT_ACCOUNT_TRANSACTION);
                        //}
                    }
                }
                else
                {
                    l_DAT_ACCOUNT_TRANSACTION_lst = mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION;// OriginalAccountTransactionList.GetRange(0, OriginalAccountTransactionList.Count);
                }
                bindDataTab(l_DAT_ACCOUNT_TRANSACTION_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAccountTransaction()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT_TRANSACTION);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsgetAccountTransaction);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT_TRANSACTION = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT_TRANSACTION>(mResponse);
                    if (mJSN_RES_ACCOUNT_TRANSACTION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT_TRANSACTION.Message.Message);
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

        public async void saveAccountTransaction()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCOUNT_TRANSACTION);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wssaveAccountTransaction);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ACCOUNT_TRANSACTION = JsonConvert.DeserializeObject<JSN_RES_ACCOUNT_TRANSACTION>(mResponse);
                    if (mJSN_RES_ACCOUNT_TRANSACTION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION.Count > 0)
                        {
                            DAT_ACCOUNT_TRANSACTION = this.mJSN_RES_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ACCOUNT_TRANSACTION.Message.Message);
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

        public async void loadSupplier()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Acc_Service.ApiCall(mRequest, Acc_Name.wsloadAccountTransaction);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_ACCOUNT_TRANSACTION = JsonConvert.DeserializeObject<JSN_LOAD_ACCOUNT_TRANSACTION>(mResponse);
                    if (JSN_LOAD_ACCOUNT_TRANSACTION.Message.Code == "7")
                    {
                        this.AccountTransaction = JSN_LOAD_ACCOUNT_TRANSACTION;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_ACCOUNT_TRANSACTION.Message.Message);
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
