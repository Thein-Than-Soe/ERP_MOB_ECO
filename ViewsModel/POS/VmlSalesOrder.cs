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
    public class VmlSalesOrder : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_ORDER mJSN_REQ_SALE_ORDER = new JSN_REQ_SALE_ORDER();
        public JSN_SALE_ORDER mJSN_SALE_ORDER = new JSN_SALE_ORDER();
        public JSN_LOAD_SALE_ORDER mJSN_LOAD_SALE_ORDER = new JSN_LOAD_SALE_ORDER();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSalesOrder()
        {
            this.switchDisplayView(DisplayView.Card);
            OrderLoad = new JSN_LOAD_SALE_ORDER();
            OrderActiveList = new List<RES_SALE_ORDER>();
            OrderPartailList = new List<RES_SALE_ORDER>();
            OrderClosedList = new List<RES_SALE_ORDER>();
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
        public JSN_LOAD_SALE_ORDER JSN_LOAD_SALE_ORDER = new JSN_LOAD_SALE_ORDER();
        public JSN_LOAD_SALE_ORDER OrderLoad
        {
            get { return JSN_LOAD_SALE_ORDER; }
            set { JSN_LOAD_SALE_ORDER = value; NotifyPropertyChanged("OrderLoad"); }
        }

        public RES_SALE_ORDER mRES_SALE_ORDER = new RES_SALE_ORDER();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_SALE_ORDER RES_SALE_ORDER
        {
            get { return mRES_SALE_ORDER; }
            set { mRES_SALE_ORDER = value; NotifyPropertyChanged("RES_SALE_ORDER"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_SALE_ORDER"); }
        }


        public List<RES_SALE_ORDER> mOrderClosedList;
        public List<RES_SALE_ORDER> OrderClosedList
        {
            get { return mOrderClosedList; }
            set { mOrderClosedList = value; NotifyPropertyChanged("OrderClosedList"); }
        }

        public List<RES_SALE_ORDER> mOrderActiveList;
        public List<RES_SALE_ORDER> OrderActiveList
        {
            get { return mOrderActiveList; }
            set { mOrderActiveList = value; NotifyPropertyChanged("OrderActiveList"); }
        }

        public List<RES_SALE_ORDER> mOrderPartailList;
        public List<RES_SALE_ORDER> OrderPartailList
        {
            get { return mOrderPartailList; }
            set { mOrderPartailList = value; NotifyPropertyChanged("OrderPartailList"); }
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
                    mRefreshCommand = new Command(() => this.getOrder());
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
        private void bindDataTab(List<RES_SALE_ORDER> argRES_SALE_ORDER_LST)
        {
            try
            {
                List<RES_SALE_ORDER> l_RES_SALE_ORDER_ACTIVE = new List<RES_SALE_ORDER>();
                List<RES_SALE_ORDER> l_RES_SALE_ORDER_PARTIAL = new List<RES_SALE_ORDER>();
                List<RES_SALE_ORDER> l_RES_SALE_ORDER_CLOSED = new List<RES_SALE_ORDER>();
                if (argRES_SALE_ORDER_LST != null && argRES_SALE_ORDER_LST.Count > 0)
                {

                    foreach (RES_SALE_ORDER l_RES_SALE_ORDER in argRES_SALE_ORDER_LST)
                    {
                        l_RES_SALE_ORDER.OrderDate = Utility.getDateTimeString(l_RES_SALE_ORDER.OrderDate);

                        if (l_RES_SALE_ORDER.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_ORDER_ACTIVE.Add(l_RES_SALE_ORDER);
                        }
                        else if (l_RES_SALE_ORDER.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_ORDER_PARTIAL.Add(l_RES_SALE_ORDER);
                        }
                        else if (l_RES_SALE_ORDER.StatusAsk.Equals("3"))
                        {
                            l_RES_SALE_ORDER_CLOSED.Add(l_RES_SALE_ORDER);
                        }
                    }

                    RES_SALE_ORDER = argRES_SALE_ORDER_LST[0];
                    OrderClosedList = l_RES_SALE_ORDER_CLOSED;
                    OrderActiveList = l_RES_SALE_ORDER_ACTIVE;
                    OrderPartailList = l_RES_SALE_ORDER_PARTIAL;
                }
                else
                {
                    OrderClosedList = new List<RES_SALE_ORDER>();
                    OrderActiveList = new List<RES_SALE_ORDER>();
                    OrderPartailList = new List<RES_SALE_ORDER>();
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
                List<RES_SALE_ORDER> l_RES_SALE_ORDER_lst = new List<RES_SALE_ORDER>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_ORDER l_RES_SALE_ORDER in mJSN_SALE_ORDER.RES_SALE_ORDER)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_ORDER.OrderCode.ToLower().Contains(argKeyword)
                            || l_RES_SALE_ORDER.OrderDate.ToLower().Contains(argKeyword)
                            || l_RES_SALE_ORDER.GrandTotal.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_ORDER_lst.Add(l_RES_SALE_ORDER);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_ORDER_lst = mJSN_SALE_ORDER.RES_SALE_ORDER;// OriginalOrderClosedList.GetRange(0, OriginalOrderClosedList.Count);
                }
                bindDataTab(l_RES_SALE_ORDER_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getOrder()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_ORDER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleOrder);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_ORDER = JsonConvert.DeserializeObject<JSN_SALE_ORDER>(mResponse);
                    if (mJSN_SALE_ORDER.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_ORDER.RES_SALE_ORDER.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_ORDER.RES_SALE_ORDER);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_ORDER.Message.Message);
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

        public async void saveOrder()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_ORDER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleOrder);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_ORDER = JsonConvert.DeserializeObject<JSN_SALE_ORDER>(mResponse);
                    if (mJSN_SALE_ORDER.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_ORDER.RES_SALE_ORDER.Count > 0)
                        {
                            RES_SALE_ORDER = this.mJSN_SALE_ORDER.RES_SALE_ORDER[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_ORDER.Message.Message);
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

        public async void loadOrder()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleOrder);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_ORDER = JsonConvert.DeserializeObject<JSN_LOAD_SALE_ORDER>(mResponse);
                    if (mJSN_LOAD_SALE_ORDER.Message.Code == "7")
                    {
                        this.OrderLoad = mJSN_LOAD_SALE_ORDER;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_ORDER.Message.Message);
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
