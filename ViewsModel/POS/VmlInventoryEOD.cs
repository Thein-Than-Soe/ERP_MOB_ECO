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
    public class VmlInventoryEOD : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SETTLEMENT_INVENTORY mJSN_REQ_SETTLEMENT_INVENTORY = new JSN_REQ_SETTLEMENT_INVENTORY();
        public JSN_RES_SETTLEMENT_INVENTORY mJSN_RES_SETTLEMENT_INVENTORY = new JSN_RES_SETTLEMENT_INVENTORY();
        public JSN_LOAD_SETTLEMENT mJSN_LOAD_SETTLEMENT = new JSN_LOAD_SETTLEMENT();
        DateTime oDate;
        DateTime eDate;
        DateTime tDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlInventoryEOD()
        {
            this.switchDisplayView(DisplayView.Card);
            InventoryEodLoad = new JSN_LOAD_SETTLEMENT();
            InventoryList = new List<DAT_SETTLEMENT_INVENTORY>();
            ThisWeekList = new List<DAT_SETTLEMENT_INVENTORY>();
            ThisMonthList = new List<DAT_SETTLEMENT_INVENTORY>();
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
        public JSN_LOAD_SETTLEMENT JSN_LOAD_SETTLEMENT = new JSN_LOAD_SETTLEMENT();
        public JSN_LOAD_SETTLEMENT InventoryEodLoad
        {
            get { return JSN_LOAD_SETTLEMENT; }
            set { JSN_LOAD_SETTLEMENT = value; NotifyPropertyChanged("InventoryEodLoad"); }
        }

        public DAT_SETTLEMENT_INVENTORY mDAT_SETTLEMENT_INVENTORY = new DAT_SETTLEMENT_INVENTORY();

        public DAT_SETTLEMENT_SUMMARY mDAT_SETTLEMENT_SUMMARY = new DAT_SETTLEMENT_SUMMARY();

        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public DAT_SETTLEMENT_INVENTORY DAT_SETTLEMENT_INVENTORY
        {
            get { return mDAT_SETTLEMENT_INVENTORY; }
            set { mDAT_SETTLEMENT_INVENTORY = value; NotifyPropertyChanged("DAT_SETTLEMENT_INVENTORY"); }
        }

        public DAT_SETTLEMENT_SUMMARY DAT_SETTLEMENT_SUMMARY
        {
            get { return mDAT_SETTLEMENT_SUMMARY; }
            set { mDAT_SETTLEMENT_SUMMARY = value; NotifyPropertyChanged("DAT_SETTLEMENT_SUMMARY"); }
        }


        public List<DAT_SETTLEMENT_INVENTORY> mInventoryList;
        public List<DAT_SETTLEMENT_INVENTORY> InventoryList
        {
            get { return mInventoryList; }
            set { mInventoryList = value; NotifyPropertyChanged("InventoryList"); }
        }

        public List<DAT_SETTLEMENT_INVENTORY> mThisWeekList;
        public List<DAT_SETTLEMENT_INVENTORY> ThisWeekList
        {
            get { return mThisWeekList; }
            set { mThisWeekList = value; NotifyPropertyChanged("ThisWeekList"); }
        }

        public List<DAT_SETTLEMENT_INVENTORY> mThisMonthList;
        public List<DAT_SETTLEMENT_INVENTORY> ThisMonthList
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
                    mRefreshCommand = new Command(() => this.getInventoryEOD());
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
        private void bindDataTab(List<DAT_SETTLEMENT_INVENTORY> argDAT_SETTLEMENT_INVENTORY_LST)
        {
            try
            {
                List<DAT_SETTLEMENT_INVENTORY> l_DAT_SETTLEMENT_INVENTORY_THIS_WEEK = new List<DAT_SETTLEMENT_INVENTORY>();
                List<DAT_SETTLEMENT_INVENTORY> l_DAT_SETTLEMENT_INVENTORY_MONTH = new List<DAT_SETTLEMENT_INVENTORY>();

                String todayDate = DateTime.UtcNow.ToString();
                tDate = DateTime.Parse(todayDate);

                int lastweek = tDate.Day - 7;
                int lastmonth = tDate.Month - 1;

                if (argDAT_SETTLEMENT_INVENTORY_LST != null && argDAT_SETTLEMENT_INVENTORY_LST.Count > 0)
                {
                    foreach (DAT_SETTLEMENT_INVENTORY l_DAT_SETTLEMENT_INVENTORY in argDAT_SETTLEMENT_INVENTORY_LST)
                    {                        
                        oDate = DateTime.Parse(l_DAT_SETTLEMENT_INVENTORY.SD);
                        eDate = DateTime.Parse(l_DAT_SETTLEMENT_INVENTORY.ED);
                       
                        l_DAT_SETTLEMENT_INVENTORY.SettlementDate = Utility.getDateTimeString(l_DAT_SETTLEMENT_INVENTORY.SettlementDate);

                        if (oDate.Month <= lastmonth && eDate.Month <= lastmonth)
                        {
                            l_DAT_SETTLEMENT_INVENTORY_MONTH.Add(l_DAT_SETTLEMENT_INVENTORY);
                        }
                        else if (oDate.Day >= lastweek && eDate.Day >= lastweek)
                        {
                            l_DAT_SETTLEMENT_INVENTORY_THIS_WEEK.Add(l_DAT_SETTLEMENT_INVENTORY);
                        }
                    }
                    DAT_SETTLEMENT_INVENTORY = argDAT_SETTLEMENT_INVENTORY_LST[0];
                    InventoryList = argDAT_SETTLEMENT_INVENTORY_LST;
                    ThisWeekList = l_DAT_SETTLEMENT_INVENTORY_THIS_WEEK;
                    ThisMonthList = l_DAT_SETTLEMENT_INVENTORY_MONTH;
                }
                else
                {
                    InventoryList = new List<DAT_SETTLEMENT_INVENTORY>();
                    ThisWeekList = new List<DAT_SETTLEMENT_INVENTORY>();
                    ThisMonthList = new List<DAT_SETTLEMENT_INVENTORY>();
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
                List<DAT_SETTLEMENT_INVENTORY> l_DAT_SETTLEMENT_INVENTORY_lst = new List<DAT_SETTLEMENT_INVENTORY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_SETTLEMENT_INVENTORY l_DAT_SETTLEMENT_INVENTORY in mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_SETTLEMENT_INVENTORY.SettlementCode.ToLower().Contains(argKeyword)
                            || l_DAT_SETTLEMENT_INVENTORY.TotalTransaction.ToLower().Contains(argKeyword)
                            || l_DAT_SETTLEMENT_INVENTORY.SaleManName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_SETTLEMENT_INVENTORY_lst.Add(l_DAT_SETTLEMENT_INVENTORY);
                        }
                    }
                }
                else
                {
                    l_DAT_SETTLEMENT_INVENTORY_lst = mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY;// OriginalInventoryList.GetRange(0, OriginalInventoryList.Count);
                }
                bindDataTab(l_DAT_SETTLEMENT_INVENTORY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getInventoryEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SETTLEMENT_INVENTORY);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventorySettlement);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_SETTLEMENT_INVENTORY = JsonConvert.DeserializeObject<JSN_RES_SETTLEMENT_INVENTORY>(mResponse);
                    if (mJSN_RES_SETTLEMENT_INVENTORY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_SETTLEMENT_INVENTORY.Message.Message);
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

        public async void saveInventoryEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SETTLEMENT_INVENTORY);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveSaleSettlement);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_SETTLEMENT_INVENTORY = JsonConvert.DeserializeObject<JSN_RES_SETTLEMENT_INVENTORY>(mResponse);
                    if (mJSN_RES_SETTLEMENT_INVENTORY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY.Count > 0)
                        {
                            DAT_SETTLEMENT_INVENTORY = this.mJSN_RES_SETTLEMENT_INVENTORY.DAT_SETTLEMENT_INVENTORY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_SETTLEMENT_INVENTORY.Message.Message);
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

        public async void loadInventoryEOD()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleSettlement);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SETTLEMENT = JsonConvert.DeserializeObject<JSN_LOAD_SETTLEMENT>(mResponse);
                    if (mJSN_LOAD_SETTLEMENT.Message.Code == "7")
                    {
                        this.InventoryEodLoad = mJSN_LOAD_SETTLEMENT;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SETTLEMENT.Message.Message);
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
