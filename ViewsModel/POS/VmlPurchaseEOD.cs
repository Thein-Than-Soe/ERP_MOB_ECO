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
    public class VmlPurchaseEOD : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_SETTLEMENT mJSN_REQ_PURCHASE_SETTLEMENT = new JSN_REQ_PURCHASE_SETTLEMENT();
        public JSN_RES_PURCHASE_SETTLEMENT mJSN_RES_PURCHASE_SETTLEMENT = new JSN_RES_PURCHASE_SETTLEMENT();
        public JSN_LOAD_PURCHASE_SETTLEMENT mJSN_LOAD_PURCHASE_SETTLEMENT = new JSN_LOAD_PURCHASE_SETTLEMENT();
        DateTime oDate;
        DateTime eDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseEOD()
        {
            this.switchDisplayView(DisplayView.Card);
            PurchaseEODLoad = new JSN_LOAD_PURCHASE_SETTLEMENT();
            PurchaseEODList = new List<RES_SETTLEMENT>();
            ThisWeekList = new List<RES_SETTLEMENT>();
            ThisMonthList = new List<RES_SETTLEMENT>();
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
        public JSN_LOAD_PURCHASE_SETTLEMENT JSN_LOAD_PURCHASE_SETTLEMENT = new JSN_LOAD_PURCHASE_SETTLEMENT();
        public JSN_LOAD_PURCHASE_SETTLEMENT PurchaseEODLoad
        {
            get { return JSN_LOAD_PURCHASE_SETTLEMENT; }
            set { JSN_LOAD_PURCHASE_SETTLEMENT = value; NotifyPropertyChanged("PurchaseEODLoad"); }
        }


        public RES_SETTLEMENT_PAY mRES_SETTLEMENT_PAY = new RES_SETTLEMENT_PAY();
        public RES_SETTLEMENT mRES_SETTLEMENT = new RES_SETTLEMENT();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }
        public RES_SETTLEMENT RES_SETTLEMENT
        {
            get { return mRES_SETTLEMENT; }
            set { mRES_SETTLEMENT = value; NotifyPropertyChanged("RES_SETTLEMENT"); }
        }

        public RES_SETTLEMENT_PAY RES_SETTLEMENT_PAY
        {
            get { return mRES_SETTLEMENT_PAY; }
            set { mRES_SETTLEMENT_PAY = value; NotifyPropertyChanged("RES_SETTLEMENT_PAY"); }
        }

        public List<RES_SETTLEMENT> mPurchaseEODList;
        public List<RES_SETTLEMENT> PurchaseEODList
        {
            get { return mPurchaseEODList; }
            set { mPurchaseEODList = value; NotifyPropertyChanged("PurchaseEODList"); }
        }

        public List<RES_SETTLEMENT> mThisWeekList;
        public List<RES_SETTLEMENT> ThisWeekList
        {
            get { return mThisWeekList; }
            set { mThisWeekList = value; NotifyPropertyChanged("ThisWeekList"); }
        }

        public List<RES_SETTLEMENT> mThisMonthList;
        public List<RES_SETTLEMENT> ThisMonthList
        {
            get { return mThisMonthList; }
            set { mThisMonthList = value; NotifyPropertyChanged("ThisMonthList"); }
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
                    mRefreshCommand = new Command(() => this.getPurchaseEOD());
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
        private void bindDataTab(List<RES_SETTLEMENT> argRES_SETTLEMENT_LST)
        {
            try
            {
                List<RES_SETTLEMENT> l_RES_SETTLEMENT_THIS_WEEK = new List<RES_SETTLEMENT>();
                List<RES_SETTLEMENT> l_RES_SETTLEMENT_MONTH = new List<RES_SETTLEMENT>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate = DateTime.Parse(todayDate);

                int lastweek = tDate.Day - 7;
                int lastmonth = tDate.Month - 1;

                if (argRES_SETTLEMENT_LST != null && argRES_SETTLEMENT_LST.Count > 0)
                {
                    foreach (RES_SETTLEMENT l_RES_SETTLEMENT in argRES_SETTLEMENT_LST)
                    {
                        oDate = DateTime.Parse(l_RES_SETTLEMENT.SD);
                        eDate = DateTime.Parse(l_RES_SETTLEMENT.ED);

                        l_RES_SETTLEMENT.SettlementDate = Utility.getDateTimeString(l_RES_SETTLEMENT.SettlementDate);

                        if (oDate.Month <= lastmonth && eDate.Month <= lastmonth)
                        {
                            l_RES_SETTLEMENT_MONTH.Add(l_RES_SETTLEMENT);
                        }
                        else if (oDate.Day >= lastweek && eDate.Day >= lastweek)
                        {
                            l_RES_SETTLEMENT_THIS_WEEK.Add(l_RES_SETTLEMENT);
                        }
                    }
                    RES_SETTLEMENT = argRES_SETTLEMENT_LST[0];
                    PurchaseEODList = argRES_SETTLEMENT_LST;
                    ThisWeekList = l_RES_SETTLEMENT_THIS_WEEK;
                    ThisMonthList = l_RES_SETTLEMENT_MONTH;
                }
                else
                {
                    PurchaseEODList = new List<RES_SETTLEMENT>();
                    ThisWeekList = new List<RES_SETTLEMENT>();
                    ThisMonthList = new List<RES_SETTLEMENT>();
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
                List<RES_SETTLEMENT> l_RES_SETTLEMENT_lst = new List<RES_SETTLEMENT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SETTLEMENT l_RES_SETTLEMENT in mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SETTLEMENT.SettlementCode.ToLower().Contains(argKeyword)
                            || l_RES_SETTLEMENT.TotalAmount.ToLower().Contains(argKeyword)
                            || l_RES_SETTLEMENT.SaleManName.ToLower().Contains(argKeyword))
                        {
                            l_RES_SETTLEMENT_lst.Add(l_RES_SETTLEMENT);
                        }
                    }
                }
                else
                {
                    l_RES_SETTLEMENT_lst = mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT;// OriginalPurchaseEODList.GetRange(0, OriginalPurchaseEODList.Count);
                }
                bindDataTab(l_RES_SETTLEMENT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getPurchaseEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_SETTLEMENT);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseSettlement);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_PURCHASE_SETTLEMENT = JsonConvert.DeserializeObject<JSN_RES_PURCHASE_SETTLEMENT>(mResponse);
                    if (mJSN_RES_PURCHASE_SETTLEMENT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_PURCHASE_SETTLEMENT.Message.Message);
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

        public async void savePurchaseEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_SETTLEMENT);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseSettlement);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_PURCHASE_SETTLEMENT = JsonConvert.DeserializeObject<JSN_RES_PURCHASE_SETTLEMENT>(mResponse);
                    if (mJSN_RES_PURCHASE_SETTLEMENT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT.Count > 0)
                        {
                            RES_SETTLEMENT = this.mJSN_RES_PURCHASE_SETTLEMENT.RES_SETTLEMENT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_PURCHASE_SETTLEMENT.Message.Message);
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

        public async void loadPurchaseEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadCollection);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_SETTLEMENT = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_SETTLEMENT>(mResponse);
                    if (mJSN_LOAD_PURCHASE_SETTLEMENT.Message.Code == "7")
                    {
                        this.PurchaseEODLoad = mJSN_LOAD_PURCHASE_SETTLEMENT;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_SETTLEMENT.Message.Message);
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
