using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;

using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.POS;
using System.Collections.Generic;
using System;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.Pos
{
    public class VmlMyTransaction : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_LOAD mJSN_REQ_SALE_LOAD = new JSN_REQ_SALE_LOAD();
        public JSN_LOAD_SALE_BROWSE mJSN_LOAD_SALE_BROWSE = new JSN_LOAD_SALE_BROWSE();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlMyTransaction()
        {
            this.switchDisplayView(DisplayView.Card);
            MyTransactionClosedList = new List<RES_SALE_BROWSE>();
            MyTransactionActiveList = new List<RES_SALE_BROWSE>();
            MyTransactionPartialList = new List<RES_SALE_BROWSE>();
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
        public RES_SALE_BROWSE mRES_SALE_BROWSE = new RES_SALE_BROWSE();
        public RES_STATUS mRES_STATUS = new RES_STATUS();
        public RES_PARENT_TYPE mRES_PARENT_TYPE = new RES_PARENT_TYPE();
        public RES_STATUS RES_STATUS
        {
            get { return mRES_STATUS; }
            set { mRES_STATUS = value; NotifyPropertyChanged("RES_STATUS"); }
        }

        public RES_PARENT_TYPE RES_PARENT_TYPE
        {
            get { return mRES_PARENT_TYPE; }
            set { mRES_PARENT_TYPE = value; NotifyPropertyChanged("RES_STATUS"); }
        }

        public RES_SALE_BROWSE RES_SALE_BROWSE
        {
            get { return mRES_SALE_BROWSE; }
            set { mRES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_BROWSE"); }
        }

        public List<RES_SALE_BROWSE> mMyTransactionClosedList;
        public List<RES_SALE_BROWSE> MyTransactionClosedList
        {
            get { return mMyTransactionClosedList; }
            set { mMyTransactionClosedList = value; NotifyPropertyChanged("MyTransactionClosedList"); }
        }

        public List<RES_SALE_BROWSE> mMyTransactionActiveList;
        public List<RES_SALE_BROWSE> MyTransactionActiveList
        {
            get { return mMyTransactionActiveList; }
            set { mMyTransactionActiveList = value; NotifyPropertyChanged("MyTransactionActiveList"); }
        }

        public List<RES_SALE_BROWSE> mMyTransactionPartialList;
        public List<RES_SALE_BROWSE> MyTransactionPartialList
        {
            get { return mMyTransactionPartialList; }
            set { mMyTransactionPartialList = value; NotifyPropertyChanged("MyTransactionPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getMyTransaction());
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
        private void bindDataTab(List<RES_SALE_BROWSE> argRES_SALE_BROWSE_LST)
        {
            try
            {
                List<RES_SALE_BROWSE> l_RES_SALE_BROWSE_ACTIVE = new List<RES_SALE_BROWSE>();
                List<RES_SALE_BROWSE> l_RES_SALE_BROWSE_PARTIAL = new List<RES_SALE_BROWSE>();
                List<RES_SALE_BROWSE> l_RES_SALE_BROWSE_CLOSED = new List<RES_SALE_BROWSE>();


                if (argRES_SALE_BROWSE_LST != null && argRES_SALE_BROWSE_LST.Count > 0)
                {
                    foreach (RES_SALE_BROWSE l_RES_SALE_BROWSE in argRES_SALE_BROWSE_LST)
                    {   
                        l_RES_SALE_BROWSE.SaleCloseDate = Utility.getDateTimeString(l_RES_SALE_BROWSE.SaleCloseDate);

                        if (l_RES_SALE_BROWSE.StatusAsk == "1")
                        {
                            l_RES_SALE_BROWSE_ACTIVE.Add(l_RES_SALE_BROWSE);
                        }
                        else if (l_RES_SALE_BROWSE.StatusAsk == "8")
                        {
                            l_RES_SALE_BROWSE_PARTIAL.Add(l_RES_SALE_BROWSE);
                        }
                        else if (l_RES_SALE_BROWSE.StatusAsk == "3")
                        {
                            l_RES_SALE_BROWSE_CLOSED.Add(l_RES_SALE_BROWSE);
                        }
                    }
                    RES_SALE_BROWSE = argRES_SALE_BROWSE_LST[0];
                    MyTransactionClosedList = l_RES_SALE_BROWSE_CLOSED;
                    MyTransactionActiveList = l_RES_SALE_BROWSE_ACTIVE;
                    MyTransactionPartialList = l_RES_SALE_BROWSE_PARTIAL;
                }
                else
                {
                    MyTransactionClosedList = new List<RES_SALE_BROWSE>();
                    MyTransactionActiveList = new List<RES_SALE_BROWSE>();
                    MyTransactionPartialList = new List<RES_SALE_BROWSE>();
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
                List<RES_SALE_BROWSE> l_RES_SALE_BROWSE_lst = new List<RES_SALE_BROWSE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_BROWSE l_RES_SALE_BROWSE in mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_BROWSE.GrandTotal.ToLower().Contains(argKeyword)
                            || l_RES_SALE_BROWSE.CustomerName_0_255.ToLower().Contains(argKeyword)
                              || l_RES_SALE_BROWSE.Code_0_50.ToLower().Contains(argKeyword)
                            || l_RES_SALE_BROWSE.SaleCloseDate.ToLower().Contains(argKeyword)
                            || l_RES_SALE_BROWSE.PaymentTypeName_0_255.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_BROWSE_lst.Add(l_RES_SALE_BROWSE);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_BROWSE_lst = mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE;// OriginalMyTransactionClosedList.GetRange(0, OriginalMyTransactionClosedList.Count);
                }
                bindDataTab(l_RES_SALE_BROWSE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getMyTransaction()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_LOAD);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsloadSaleTransHis);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_BROWSE = JsonConvert.DeserializeObject<JSN_LOAD_SALE_BROWSE>(mResponse);
                    if (mJSN_LOAD_SALE_BROWSE.Message.Code == "7")
                    {
                        if (this.mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE.Count > 0)
                        {
                            bindDataTab(this.mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_BROWSE.Message.Message);
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
        public async void saveMyTransaction()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_LOAD);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsloadSaleTransHis);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_BROWSE = JsonConvert.DeserializeObject<JSN_LOAD_SALE_BROWSE>(mResponse);
                    if (mJSN_LOAD_SALE_BROWSE.Message.Code == "7")
                    {
                        if (this.mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE.Count > 0)
                        {
                            RES_SALE_BROWSE = this.mJSN_LOAD_SALE_BROWSE.RES_SALE_BROWSE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_BROWSE.Message.Message);
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
