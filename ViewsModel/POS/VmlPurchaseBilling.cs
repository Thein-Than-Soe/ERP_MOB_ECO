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
    public class VmlPurchaseBilling : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_BILL_JUN mJSN_REQ_PURCHASE_BILL_JUN = new JSN_REQ_PURCHASE_BILL_JUN();
        public JSN_PURCHASE_BILL_JUN mJSN_PURCHASE_BILL_JUN = new JSN_PURCHASE_BILL_JUN();
        public JSN_LOAD_PURCHASE_BILLING mJSN_LOAD_PURCHASE_BILLING = new JSN_LOAD_PURCHASE_BILLING();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseBilling()
        {
            this.switchDisplayView(DisplayView.Card);
            BillingLoad = new JSN_LOAD_PURCHASE_BILLING();
            BillingClosedList = new List<RES_PURCHASE_BILLING>();
            BillingActiveList = new List<RES_PURCHASE_BILLING>();
            BillingPartialList = new List<RES_PURCHASE_BILLING>();
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
        public JSN_LOAD_PURCHASE_BILLING JSN_LOAD_PURCHASE_BILLING = new JSN_LOAD_PURCHASE_BILLING();
        public JSN_LOAD_PURCHASE_BILLING BillingLoad
        {
            get { return JSN_LOAD_PURCHASE_BILLING; }
            set { JSN_LOAD_PURCHASE_BILLING = value; NotifyPropertyChanged("BiilingLoad"); }
        }

        public RES_PURCHASE_BILLING mRES_PURCHASE_BILLING = new RES_PURCHASE_BILLING();
        public RES_PURCHASE_BROWSE mRES_PURCHASE_BROWSE = new RES_PURCHASE_BROWSE();
        public RES_COMPANY mRES_COMPANY = new RES_COMPANY();
        public RES_PURCHASE_BILLING RES_PURCHASE_BILLING
        {
            get { return mRES_PURCHASE_BILLING; }
            set { mRES_PURCHASE_BILLING = value; NotifyPropertyChanged("RES_PURCHASE_BILLING"); }
        }
        public RES_PURCHASE_BROWSE RES_PURCHASE_BROWSE
        {
            get { return mRES_PURCHASE_BROWSE; }
            set { mRES_PURCHASE_BROWSE = value; NotifyPropertyChanged("RES_PURCHASE_BROWSE"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return mRES_COMPANY; }
            set { mRES_COMPANY = value; NotifyPropertyChanged("RES_PURCHASE_BROWSE"); }
        }

        public List<RES_PURCHASE_BILLING> mBillingClosedList;
        public List<RES_PURCHASE_BILLING> BillingClosedList
        {
            get { return mBillingClosedList; }
            set { mBillingClosedList = value; NotifyPropertyChanged("BillingClosedList"); }
        }

        public List<RES_PURCHASE_BILLING> mBillingActiveList;
        public List<RES_PURCHASE_BILLING> BillingActiveList
        {
            get { return mBillingActiveList; }
            set { mBillingActiveList = value; NotifyPropertyChanged("BillingActiveList"); }
        }

        public List<RES_PURCHASE_BILLING> mBillingPartialList;
        public List<RES_PURCHASE_BILLING> BillingPartialList
        {
            get { return mBillingPartialList; }
            set { mBillingPartialList = value; NotifyPropertyChanged("BillingPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getBilling());
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
        private void bindDataTab(List<RES_PURCHASE_BILLING> argRES_PURCHASE_BILLING_LST)
        {
            try
            {
                List<RES_PURCHASE_BILLING> l_RES_PURCHASE_BILLING_ACTIVE = new List<RES_PURCHASE_BILLING>();
                List<RES_PURCHASE_BILLING> l_RES_PURCHASE_BILLING_PARTIAL = new List<RES_PURCHASE_BILLING>();
                List<RES_PURCHASE_BILLING> l_RES_PURCHASE_BILLING_CLOSED = new List<RES_PURCHASE_BILLING>();
                if (argRES_PURCHASE_BILLING_LST != null && argRES_PURCHASE_BILLING_LST.Count > 0)
                {

                    foreach (RES_PURCHASE_BILLING l_RES_PURCHASE_BILLING in argRES_PURCHASE_BILLING_LST)
                    {
                        l_RES_PURCHASE_BILLING.BillingDate = Utility.getDateTimeString(l_RES_PURCHASE_BILLING.BillingDate);

                        if (l_RES_PURCHASE_BILLING.StatusAsk.Equals("1"))
                        {
                            l_RES_PURCHASE_BILLING_ACTIVE.Add(l_RES_PURCHASE_BILLING);
                        }
                        else if (l_RES_PURCHASE_BILLING.StatusAsk.Equals("2"))
                        {
                            l_RES_PURCHASE_BILLING_PARTIAL.Add(l_RES_PURCHASE_BILLING);
                        }
                        else if (l_RES_PURCHASE_BILLING.StatusAsk.Equals("9"))
                        {
                            l_RES_PURCHASE_BILLING_CLOSED.Add(l_RES_PURCHASE_BILLING);
                        }
                    }

                    RES_PURCHASE_BILLING = argRES_PURCHASE_BILLING_LST[0];
                    BillingClosedList = l_RES_PURCHASE_BILLING_CLOSED;
                    BillingActiveList = l_RES_PURCHASE_BILLING_ACTIVE;
                    BillingPartialList = l_RES_PURCHASE_BILLING_PARTIAL;
                }
                else
                {
                    BillingClosedList = new List<RES_PURCHASE_BILLING>();
                    BillingActiveList = new List<RES_PURCHASE_BILLING>();
                    BillingPartialList = new List<RES_PURCHASE_BILLING>();
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
                List<RES_PURCHASE_BILLING> l_RES_PURCHASE_BILLING_lst = new List<RES_PURCHASE_BILLING>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_PURCHASE_BILLING l_RES_PURCHASE_BILLING in mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_PURCHASE_BILLING.SupplierName.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_BILLING.BillingCode.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_BILLING.BillingDate.ToLower().Contains(argKeyword))
                        {
                            l_RES_PURCHASE_BILLING_lst.Add(l_RES_PURCHASE_BILLING);
                        }
                    }
                }
                else
                {
                    l_RES_PURCHASE_BILLING_lst = mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING;// OriginalBillingClosedList.GetRange(0, OriginalBillingClosedList.Count);
                }
                bindDataTab(l_RES_PURCHASE_BILLING_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_BILL_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseBillingJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_BILL_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_BILL_JUN>(mResponse);
                    if (mJSN_PURCHASE_BILL_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING.Count > 0)
                        {
                            bindDataTab(this.mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_BILL_JUN.Message.Message);
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

        public async void saveBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_BILL_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseBillingJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_BILL_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_BILL_JUN>(mResponse);
                    if (mJSN_PURCHASE_BILL_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING.Count > 0)
                        {
                            RES_PURCHASE_BILLING = this.mJSN_PURCHASE_BILL_JUN.RES_PURCHASE_BILLING[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_BILL_JUN.Message.Message);
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

        public async void loadBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadPurchaseBilling);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_BILLING = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_BILLING>(mResponse);
                    if (mJSN_LOAD_PURCHASE_BILLING.Message.Code == "7")
                    {
                        this.BillingLoad = mJSN_LOAD_PURCHASE_BILLING;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_BILLING.Message.Message);
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
