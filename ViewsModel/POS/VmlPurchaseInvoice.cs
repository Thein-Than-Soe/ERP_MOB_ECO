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
    public class VmlPurchaseInvoice : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_INVOICE_JUN mJSN_REQ_PURCHASE_INVOICE_JUN = new JSN_REQ_PURCHASE_INVOICE_JUN();
        public JSN_PURCHASE_INVOICE_JUN mJSN_PURCHASE_INVOICE_JUN = new JSN_PURCHASE_INVOICE_JUN();
        public JSN_LOAD_PURCHASE_PAYMENT mJSN_LOAD_PURCHASE_PAYMENT = new JSN_LOAD_PURCHASE_PAYMENT();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseInvoice()
        {
            this.switchDisplayView(DisplayView.Card);
            InvoiceLoad = new JSN_LOAD_PURCHASE_PAYMENT();
            InvoiceClosedList = new List<RES_PURCHASE_INVOICE>();
            InvoiceActiveList = new List<RES_PURCHASE_INVOICE>();
            InvoicePartialList = new List<RES_PURCHASE_INVOICE>();
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
        public JSN_LOAD_PURCHASE_PAYMENT JSN_LOAD_PURCHASE_PAYMENT = new JSN_LOAD_PURCHASE_PAYMENT();
        public JSN_LOAD_PURCHASE_PAYMENT InvoiceLoad
        {
            get { return JSN_LOAD_PURCHASE_PAYMENT; }
            set { JSN_LOAD_PURCHASE_PAYMENT = value; NotifyPropertyChanged("InvoiceLoad"); }
        }
        public RES_PURCHASE_INVOICE mRES_PURCHASE_INVOICE = new RES_PURCHASE_INVOICE();
        public RES_PURCHASE_INVOICE_DETAIL mRES_PURCHASE_INVOICE_DETAIL = new RES_PURCHASE_INVOICE_DETAIL();
        public RES_COMPANY mRES_COMPANY = new RES_COMPANY();
        public RES_PURCHASE_BROWSE rES_PURCHASE_BROWSE = new RES_PURCHASE_BROWSE();

        public RES_PURCHASE_INVOICE RES_PURCHASE_INVOICE
        {
            get { return mRES_PURCHASE_INVOICE; }
            set { mRES_PURCHASE_INVOICE = value; NotifyPropertyChanged("RES_PURCHASE_INVOICE"); }
        }
        public RES_PURCHASE_INVOICE_DETAIL RES_PURCHASE_INVOICE_DETAIL
        {
            get { return mRES_PURCHASE_INVOICE_DETAIL; }
            set { mRES_PURCHASE_INVOICE_DETAIL = value; NotifyPropertyChanged("RES_PURCHASE_INVOICE_DETAIL"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return mRES_COMPANY; }
            set { mRES_COMPANY = value; NotifyPropertyChanged("RES_PURCHASE_INVOICE_DETAIL"); }
        }

        public RES_PURCHASE_BROWSE RES_PURCHASE_BROWSE
        {
            get { return rES_PURCHASE_BROWSE; }
            set { rES_PURCHASE_BROWSE = value; NotifyPropertyChanged("RES_PURCHASE_BROWSE"); }
        }



        public List<RES_PURCHASE_INVOICE> mInvoiceClosedList;
        public List<RES_PURCHASE_INVOICE> InvoiceClosedList
        {
            get { return mInvoiceClosedList; }
            set { mInvoiceClosedList = value; NotifyPropertyChanged("InvoiceClosedList"); }
        }

        public List<RES_PURCHASE_INVOICE> mInvoiceActiveList;
        public List<RES_PURCHASE_INVOICE> InvoiceActiveList
        {
            get { return mInvoiceActiveList; }
            set { mInvoiceActiveList = value; NotifyPropertyChanged("InvoiceActiveList"); }
        }

        public List<RES_PURCHASE_INVOICE> mInvoicePartialList;
        public List<RES_PURCHASE_INVOICE> InvoicePartialList
        {
            get { return mInvoicePartialList; }
            set { mInvoicePartialList = value; NotifyPropertyChanged("InvoicePartialList"); }
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
                    mRefreshCommand = new Command(() => this.getInvoice());
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
        private void bindDataTab(List<RES_PURCHASE_INVOICE> argRES_PURCHASE_INVOICE_LST)
        {
            try
            {
                List<RES_PURCHASE_INVOICE> l_RES_PURCHASE_INVOICE_ACTIVE = new List<RES_PURCHASE_INVOICE>();
                List<RES_PURCHASE_INVOICE> l_RES_PURCHASE_INVOICE_PARTIAL = new List<RES_PURCHASE_INVOICE>();
                List<RES_PURCHASE_INVOICE> l_RES_PURCHASE_INVOICE_CLOSED = new List<RES_PURCHASE_INVOICE>();
                if (argRES_PURCHASE_INVOICE_LST != null && argRES_PURCHASE_INVOICE_LST.Count > 0)
                {

                    foreach (RES_PURCHASE_INVOICE l_RES_PURCHASE_INVOICE in argRES_PURCHASE_INVOICE_LST)
                    {                       
                        l_RES_PURCHASE_INVOICE.InvoiceDate = Utility.getDateTimeString(l_RES_PURCHASE_INVOICE.InvoiceDate);

                        if (l_RES_PURCHASE_INVOICE.StatusAsk.Equals("1"))
                        {
                            l_RES_PURCHASE_INVOICE_ACTIVE.Add(l_RES_PURCHASE_INVOICE);
                        }
                        else if (l_RES_PURCHASE_INVOICE.StatusAsk.Equals("8"))
                        {
                            l_RES_PURCHASE_INVOICE_PARTIAL.Add(l_RES_PURCHASE_INVOICE);
                        }
                        else if (l_RES_PURCHASE_INVOICE.StatusAsk.Equals("3"))
                        {
                            l_RES_PURCHASE_INVOICE_CLOSED.Add(l_RES_PURCHASE_INVOICE);
                        }
                    }

                    RES_PURCHASE_INVOICE = argRES_PURCHASE_INVOICE_LST[0];
                    InvoiceClosedList = l_RES_PURCHASE_INVOICE_CLOSED;
                    InvoiceActiveList = l_RES_PURCHASE_INVOICE_ACTIVE;
                    InvoicePartialList = l_RES_PURCHASE_INVOICE_PARTIAL;
                }
                else
                {
                    InvoiceClosedList = new List<RES_PURCHASE_INVOICE>();
                    InvoiceActiveList = new List<RES_PURCHASE_INVOICE>();
                    InvoicePartialList = new List<RES_PURCHASE_INVOICE>();
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
                List<RES_PURCHASE_INVOICE> l_RES_PURCHASE_INVOICE_lst = new List<RES_PURCHASE_INVOICE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_PURCHASE_INVOICE l_RES_PURCHASE_INVOICE in mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_PURCHASE_INVOICE.SupplierName.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_INVOICE.InvoiceCode.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_INVOICE.InvoiceDate.ToLower().Contains(argKeyword))
                        {
                            l_RES_PURCHASE_INVOICE_lst.Add(l_RES_PURCHASE_INVOICE);
                        }
                    }
                }
                else
                {
                    l_RES_PURCHASE_INVOICE_lst = mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE;// OriginalInvoiceClosedList.GetRange(0, OriginalInvoiceClosedList.Count);
                }
                bindDataTab(l_RES_PURCHASE_INVOICE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getInvoice()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_INVOICE_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseInvoice);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_INVOICE_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_INVOICE_JUN>(mResponse);
                    if (mJSN_PURCHASE_INVOICE_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE.Count > 0)
                        {
                            bindDataTab(this.mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_INVOICE_JUN.Message.Message);
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

        public async void saveInvoice()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_INVOICE_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseInvoiceJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_INVOICE_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_INVOICE_JUN>(mResponse);
                    if (mJSN_PURCHASE_INVOICE_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE.Count > 0)
                        {
                            RES_PURCHASE_INVOICE = this.mJSN_PURCHASE_INVOICE_JUN.RES_PURCHASE_INVOICE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_INVOICE_JUN.Message.Message);
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

        public async void loadInvoice()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadPurchaseInvoice);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_PAYMENT = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_PAYMENT>(mResponse);
                    if (mJSN_LOAD_PURCHASE_PAYMENT.Message.Code == "7")
                    {
                        this.InvoiceLoad = mJSN_LOAD_PURCHASE_PAYMENT;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_PAYMENT.Message.Message);
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
