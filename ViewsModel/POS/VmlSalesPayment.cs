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
    public class VmlSalesPayment : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_PAYMENT_JUN mJSN_REQ_SALE_PAYMENT_JUN = new JSN_REQ_SALE_PAYMENT_JUN();
        public JSN_SALE_PAYMENT_JUN mJSN_SALE_PAYMENT_JUN = new JSN_SALE_PAYMENT_JUN();
        public JSN_LOAD_SALE_PAYMENT mJSN_LOAD_SALE_PAYMENT = new JSN_LOAD_SALE_PAYMENT();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSalesPayment()
        {
            this.switchDisplayView(DisplayView.Card);
            PaymentLoad = new JSN_LOAD_SALE_PAYMENT();
            PaymentClosedList = new List<RES_SALE_PAYMENT>();
            PaymentActiveList = new List<RES_SALE_PAYMENT>();
            PaymentPartialList = new List<RES_SALE_PAYMENT>();
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
        public JSN_LOAD_SALE_PAYMENT JSN_LOAD_SALE_PAYMENT =new JSN_LOAD_SALE_PAYMENT();
        public JSN_LOAD_SALE_PAYMENT PaymentLoad
        {
            get { return JSN_LOAD_SALE_PAYMENT; }
            set { JSN_LOAD_SALE_PAYMENT = value; NotifyPropertyChanged("PaymentLoad"); }
        }

        public RES_SALE_PAYMENT mRES_SALE_PAYMENT = new RES_SALE_PAYMENT();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_SALE_BROWSE mRES_SALE_BROWSE = new RES_SALE_BROWSE();

        public RES_SALE_BROWSE RES_SALE_BROWSE
        {
            get { return mRES_SALE_BROWSE; }
            set { mRES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_BROWSE"); }
        }

        public RES_SALE_PAYMENT RES_SALE_PAYMENT
        {
            get { return mRES_SALE_PAYMENT; }
            set { mRES_SALE_PAYMENT = value; NotifyPropertyChanged("RES_SALE_PAYMENT"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_SALE_PAYMENT"); }
        }


        public List<RES_SALE_PAYMENT> mPaymentClosedList;
        public List<RES_SALE_PAYMENT> PaymentClosedList
        {
            get { return mPaymentClosedList; }
            set { mPaymentClosedList = value; NotifyPropertyChanged("PaymentClosedList"); }
        }

        public List<RES_SALE_PAYMENT> mPaymentActiveList;
        public List<RES_SALE_PAYMENT> PaymentActiveList
        {
            get { return mPaymentActiveList; }
            set { mPaymentActiveList = value; NotifyPropertyChanged("PaymentActiveList"); }
        }

        public List<RES_SALE_PAYMENT> mPaymentPartialList;
        public List<RES_SALE_PAYMENT> PaymentPartialList
        {
            get { return mPaymentPartialList; }
            set { mPaymentPartialList = value; NotifyPropertyChanged("PaymentPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getPayment());
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
        private void bindDataTab(List<RES_SALE_PAYMENT> argRES_SALE_PAYMENT_LST)
        {
            try
            {
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_ACTIVE = new List<RES_SALE_PAYMENT>();
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_PARTIAL = new List<RES_SALE_PAYMENT>();
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_CLOSED = new List<RES_SALE_PAYMENT>();

                if (argRES_SALE_PAYMENT_LST != null && argRES_SALE_PAYMENT_LST.Count > 0)
                {

                    foreach (RES_SALE_PAYMENT l_RES_SALE_PAYMENT in argRES_SALE_PAYMENT_LST)
                    {
                       l_RES_SALE_PAYMENT.PaymentDate = Utility.getDateTimeString(l_RES_SALE_PAYMENT.PaymentDate);

                        if (l_RES_SALE_PAYMENT.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_PAYMENT_ACTIVE.Add(l_RES_SALE_PAYMENT);
                        }
                        else if (l_RES_SALE_PAYMENT.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_PAYMENT_PARTIAL.Add(l_RES_SALE_PAYMENT);
                        }
                        else if (l_RES_SALE_PAYMENT.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_PAYMENT_CLOSED.Add(l_RES_SALE_PAYMENT);
                        }
                    }

                    RES_SALE_PAYMENT = argRES_SALE_PAYMENT_LST[0];
                    PaymentClosedList = l_RES_SALE_PAYMENT_CLOSED;
                    PaymentActiveList = l_RES_SALE_PAYMENT_ACTIVE;
                    PaymentPartialList = l_RES_SALE_PAYMENT_PARTIAL;
                }
                else
                {
                    PaymentClosedList = new List<RES_SALE_PAYMENT>();
                    PaymentActiveList = new List<RES_SALE_PAYMENT>();
                    PaymentPartialList = new List<RES_SALE_PAYMENT>();
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
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_lst = new List<RES_SALE_PAYMENT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_PAYMENT l_RES_SALE_PAYMENT in mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_PAYMENT.PaymentCode.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT.PaymentDate.ToLower().Contains(argKeyword)
                             || l_RES_SALE_PAYMENT.PaymentTypeName.ToLower().Contains(argKeyword)
                              || l_RES_SALE_PAYMENT.OutstandingAmount.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT.ParentCode.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_PAYMENT_lst.Add(l_RES_SALE_PAYMENT);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_PAYMENT_lst = mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT;// OriginalPaymentClosedList.GetRange(0, OriginalPaymentClosedList.Count);
                }
                bindDataTab(l_RES_SALE_PAYMENT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getPayment()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_PAYMENT_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSalePaymentJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_PAYMENT_JUN = JsonConvert.DeserializeObject<JSN_SALE_PAYMENT_JUN>(mResponse);
                    if (mJSN_SALE_PAYMENT_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_PAYMENT_JUN.Message.Message);
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

        public async void savePayment()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_PAYMENT_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSalePaymentJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_PAYMENT_JUN = JsonConvert.DeserializeObject<JSN_SALE_PAYMENT_JUN>(mResponse);
                    if (mJSN_SALE_PAYMENT_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT.Count > 0)
                        {
                            RES_SALE_PAYMENT = this.mJSN_SALE_PAYMENT_JUN.RES_SALE_PAYMENT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_PAYMENT_JUN.Message.Message);
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

        public async void loadPayment()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsloadSalePayment);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_PAYMENT = JsonConvert.DeserializeObject<JSN_LOAD_SALE_PAYMENT>(mResponse);
                    if (mJSN_LOAD_SALE_PAYMENT.Message.Code == "7")
                    {
                        this.PaymentLoad = mJSN_LOAD_SALE_PAYMENT;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_PAYMENT.Message.Message);
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
