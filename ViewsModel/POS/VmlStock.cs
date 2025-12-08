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
    public class VmlStock : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_INVENTRY_STOCK_NEW mJSN_REQ_INVENTRY_STOCK_NEW = new JSN_REQ_INVENTRY_STOCK_NEW();
        public JSN_INVENTORY_STOCK mJSN_INVENTORY_STOCK = new JSN_INVENTORY_STOCK();
        public JSN_LOAD_INVENTRY_STOCK mJSN_LOAD_INVENTRY_STOCK = new JSN_LOAD_INVENTRY_STOCK();

        DateTime oDate;
        DateTime eDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlStock()
        {
            this.switchDisplayView(DisplayView.Card);
            StockLoad = new JSN_LOAD_INVENTRY_STOCK();
            StockActiveList = new List<RES_STOCK>();
            StockInActiveList = new List<RES_STOCK>();
            StockList = new List<RES_STOCK>();
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
        public JSN_LOAD_INVENTRY_STOCK JSN_LOAD_INVENTRY_STOCK = new JSN_LOAD_INVENTRY_STOCK();
        public JSN_LOAD_INVENTRY_STOCK StockLoad
        {
            get { return JSN_LOAD_INVENTRY_STOCK; }
            set { JSN_LOAD_INVENTRY_STOCK = value; NotifyPropertyChanged("StockLoad"); }
        }

        public RES_STOCK mRES_STOCK = new RES_STOCK();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_STOCK RES_STOCK
        {
            get { return mRES_STOCK; }
            set { mRES_STOCK = value; NotifyPropertyChanged("RES_STOCK"); }
        }

        public List<RES_STOCK> mStockList;
        public List<RES_STOCK> StockList
        {
            get { return mStockList; }
            set { mStockList = value; NotifyPropertyChanged("StockList"); }
        }

        public List<RES_STOCK> mStockActiveList;
        public List<RES_STOCK> StockActiveList
        {
            get { return mStockActiveList; }
            set { mStockActiveList = value; NotifyPropertyChanged("StockActiveList"); }
        }

        public List<RES_STOCK> mStockInActiveList;
        public List<RES_STOCK> StockInActiveList
        {
            get { return mStockInActiveList; }
            set { mStockInActiveList = value; NotifyPropertyChanged("StockInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getStock());
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
        private void bindDataTab(List<RES_STOCK> argRES_STOCK_LST)
        {
            try
            {
                List<RES_STOCK> l_RES_STOCK_ACTIVE = new List<RES_STOCK>();
                List<RES_STOCK> l_RES_STOCK_INACTIVE = new List<RES_STOCK>();
                List<RES_STOCK> l_RES_STOCK_ALL = new List<RES_STOCK>();

                if (argRES_STOCK_LST != null && argRES_STOCK_LST.Count > 0)
                {
                    foreach (RES_STOCK l_RES_STOCK in argRES_STOCK_LST)
                    {
                        if (l_RES_STOCK.StatusAsk == "1")
                        {
                            l_RES_STOCK_ACTIVE.Add(l_RES_STOCK);
                        }
                        else if (l_RES_STOCK.StatusAsk == "8")
                        {
                            l_RES_STOCK_INACTIVE.Add(l_RES_STOCK);
                        }
                    }
                    RES_STOCK = argRES_STOCK_LST[0];
                    StockList = argRES_STOCK_LST;
                    StockActiveList = l_RES_STOCK_ACTIVE;
                    StockInActiveList = l_RES_STOCK_INACTIVE;
                }
                else
                {
                    StockList = new List<RES_STOCK>();
                    StockActiveList = new List<RES_STOCK>();
                    StockInActiveList = new List<RES_STOCK>();
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
                List<RES_STOCK> l_RES_STOCK_lst = new List<RES_STOCK>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_STOCK l_RES_STOCK in mJSN_INVENTORY_STOCK.RES_STOCK)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_STOCK.StockCode.ToLower().Contains(argKeyword)
                            || l_RES_STOCK.StockName.ToLower().Contains(argKeyword)
                           
                            || l_RES_STOCK.StockOnhandQty.ToLower().Contains(argKeyword))
                        {
                            l_RES_STOCK_lst.Add(l_RES_STOCK);
                        }
                    }
                }
                else
                {
                    l_RES_STOCK_lst = mJSN_INVENTORY_STOCK.RES_STOCK;// OriginalStockList.GetRange(0, OriginalStockList.Count);
                }
                bindDataTab(l_RES_STOCK_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getStock()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTRY_STOCK_NEW);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventoryStock);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_STOCK = JsonConvert.DeserializeObject<JSN_INVENTORY_STOCK>(mResponse);
                    if (mJSN_INVENTORY_STOCK.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_STOCK.RES_STOCK.Count > 0)
                        {
                            bindDataTab(this.mJSN_INVENTORY_STOCK.RES_STOCK);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_STOCK.Message.Message);
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

        public async void saveStock()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTRY_STOCK_NEW);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventoryStock);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_STOCK = JsonConvert.DeserializeObject<JSN_INVENTORY_STOCK>(mResponse);
                    if (mJSN_INVENTORY_STOCK.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_STOCK.RES_STOCK.Count > 0)
                        {
                            RES_STOCK = this.mJSN_INVENTORY_STOCK.RES_STOCK[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_STOCK.Message.Message);
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

        public async void loadStock()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadInventryStock);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_INVENTRY_STOCK = JsonConvert.DeserializeObject<JSN_LOAD_INVENTRY_STOCK>(mResponse);
                    if (mJSN_LOAD_INVENTRY_STOCK.Message.Code == "7")
                    {
                        this.StockLoad = mJSN_LOAD_INVENTRY_STOCK;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_INVENTRY_STOCK.Message.Message);
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
