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
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.SYS
{
    public class VmlMyBilling : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_BILL_JUN mJSN_REQ_SALE_BILL_JUN = new JSN_REQ_SALE_BILL_JUN();
        public JSN_SALE_BILL_JUN mJSN_SALE_BILL_JUN = new JSN_SALE_BILL_JUN();
        public JSN_LOAD_SALE_BILLING mJSN_LOAD_SALE_BILLING = new JSN_LOAD_SALE_BILLING();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlMyBilling()
        {
            this.switchDisplayView(DisplayView.Card);
            BillingLoad = new JSN_LOAD_SALE_BILLING();
            MyBillingClosedList = new List<RES_SALE_BILLING>();
            MyBillingActiveList = new List<RES_SALE_BILLING>();
            MyBillingPartialList = new List<RES_SALE_BILLING>();
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

        public JSN_LOAD_SALE_BILLING JSN_LOAD_SALE_BILLING = new JSN_LOAD_SALE_BILLING();
        public JSN_LOAD_SALE_BILLING BillingLoad
        {
            get { return JSN_LOAD_SALE_BILLING; }
            set { JSN_LOAD_SALE_BILLING = value; NotifyPropertyChanged("BillingLoad"); }
        }
        public RES_SALE_BILLING mRES_SALE_BILLING = new RES_SALE_BILLING();
        public RES_SALE_BROWSE rES_SALE_BROWSE = new RES_SALE_BROWSE();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_SALE_BILLING RES_SALE_BILLING
        {
            get { return mRES_SALE_BILLING; }
            set { mRES_SALE_BILLING = value; NotifyPropertyChanged("RES_SALE_BILLING"); }
        }

        public RES_SALE_BROWSE RES_SALE_BROWSE
        {
            get { return rES_SALE_BROWSE; }
            set { rES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_BROWSE"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public List<RES_SALE_BILLING> mMyBillingClosedList;
        public List<RES_SALE_BILLING> MyBillingClosedList
        {
            get { return mMyBillingClosedList; }
            set { mMyBillingClosedList = value; NotifyPropertyChanged("MyBillingClosedList"); }
        }

        public List<RES_SALE_BILLING> mMyBillingActiveList;
        public List<RES_SALE_BILLING> MyBillingActiveList
        {
            get { return mMyBillingActiveList; }
            set { mMyBillingActiveList = value; NotifyPropertyChanged("MyBillingActiveList"); }
        }

        public List<RES_SALE_BILLING> mMyBillingPartialList;
        public List<RES_SALE_BILLING> MyBillingPartialList
        {
            get { return mMyBillingPartialList; }
            set { mMyBillingPartialList = value; NotifyPropertyChanged("MyBillingPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getMyBilling());
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
        private void bindDataTab(List<RES_SALE_BILLING> argRES_SALE_BILLING_LST)
        {
            try
            {
                List<RES_SALE_BILLING> l_RES_SALE_BILLING_ACTIVE = new List<RES_SALE_BILLING>();
                List<RES_SALE_BILLING> l_RES_SALE_BILLING_PARTIAL = new List<RES_SALE_BILLING>();
                List<RES_SALE_BILLING> l_RES_SALE_BILLING_CLOSED = new List<RES_SALE_BILLING>();

                if (argRES_SALE_BILLING_LST != null && argRES_SALE_BILLING_LST.Count > 0)
                {

                    foreach (RES_SALE_BILLING l_RES_SALE_BILLING in argRES_SALE_BILLING_LST)
                    {

                        l_RES_SALE_BILLING.BillingDate = Utility.getDateTimeString(l_RES_SALE_BILLING.BillingDate);

                        if (l_RES_SALE_BILLING.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_BILLING_ACTIVE.Add(l_RES_SALE_BILLING);
                        }
                        else if (l_RES_SALE_BILLING.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_BILLING_PARTIAL.Add(l_RES_SALE_BILLING);
                        }

                        else if (l_RES_SALE_BILLING.StatusAsk.Equals("3"))
                        {
                            l_RES_SALE_BILLING_CLOSED.Add(l_RES_SALE_BILLING);
                        }
                        }

                    RES_SALE_BILLING = argRES_SALE_BILLING_LST[0];
                    MyBillingClosedList = l_RES_SALE_BILLING_CLOSED;
                    MyBillingActiveList = l_RES_SALE_BILLING_ACTIVE;
                    MyBillingPartialList = l_RES_SALE_BILLING_PARTIAL;
                }
                else
                {
                    MyBillingClosedList = new List<RES_SALE_BILLING>();
                    MyBillingActiveList = new List<RES_SALE_BILLING>();
                    MyBillingPartialList = new List<RES_SALE_BILLING>();
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
                List<RES_SALE_BILLING> l_RES_SALE_BILLING_lst = new List<RES_SALE_BILLING>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_BILLING l_RES_SALE_BILLING in mJSN_SALE_BILL_JUN.RES_SALE_BILLING)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_BILLING.CustomerName_0_255.ToLower().Contains(argKeyword)
                            || l_RES_SALE_BILLING.BillingCode_0_50.ToLower().Contains(argKeyword)
                             || l_RES_SALE_BILLING.GrandTotal.ToLower().Contains(argKeyword)
                            || l_RES_SALE_BILLING.ParentCode_0_50.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_BILLING_lst.Add(l_RES_SALE_BILLING);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_BILLING_lst = mJSN_SALE_BILL_JUN.RES_SALE_BILLING;// OriginalMyBillingClosedList.GetRange(0, OriginalMyBillingClosedList.Count);
                }
                bindDataTab(l_RES_SALE_BILLING_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getMyBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_BILL_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleBillJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_BILL_JUN = JsonConvert.DeserializeObject<JSN_SALE_BILL_JUN>(mResponse);
                    if (mJSN_SALE_BILL_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_BILL_JUN.RES_SALE_BILLING.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_BILL_JUN.RES_SALE_BILLING);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_BILL_JUN.Message.Message);
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

        public async void saveMyBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_BILL_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleBillJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_BILL_JUN = JsonConvert.DeserializeObject<JSN_SALE_BILL_JUN>(mResponse);
                    if (mJSN_SALE_BILL_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_BILL_JUN.RES_SALE_BILLING.Count > 0)
                        {
                            RES_SALE_BILLING = this.mJSN_SALE_BILL_JUN.RES_SALE_BILLING[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_BILL_JUN.Message.Message);
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

        public async void loadMyBilling()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleBilling);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_BILLING = JsonConvert.DeserializeObject<JSN_LOAD_SALE_BILLING>(mResponse);
                    if (mJSN_LOAD_SALE_BILLING.Message.Code == "7")
                    {
                        this.BillingLoad = mJSN_LOAD_SALE_BILLING;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_BILLING.Message.Message);
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
