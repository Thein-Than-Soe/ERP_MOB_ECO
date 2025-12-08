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
    public class VmlMyPayment : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_PAYMENT_LST mJSN_REQ_SALE_PAYMENT_LST = new JSN_REQ_SALE_PAYMENT_LST();
        public JSN_SALE_PAYMENT_LST mJSN_SALE_PAYMENT_LST = new JSN_SALE_PAYMENT_LST();
        public JSN_LOAD_SALE_PAYMENT mJSN_LOAD_SALE_PAYMENT = new JSN_LOAD_SALE_PAYMENT();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlMyPayment()
        {
            this.switchDisplayView(DisplayView.Card);
            PaymentLoad = new JSN_LOAD_SALE_PAYMENT();
            MyPaymentClosedList = new List<RES_SALE_PAYMENT>();
            MyPaymentActiveList = new List<RES_SALE_PAYMENT>();
            MyPaymentPartialList = new List<RES_SALE_PAYMENT>();
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
        public JSN_LOAD_SALE_PAYMENT JSN_LOAD_SALE_PAYMENT = new JSN_LOAD_SALE_PAYMENT();
        public JSN_LOAD_SALE_PAYMENT PaymentLoad
        {
            get { return JSN_LOAD_SALE_PAYMENT; }
            set { JSN_LOAD_SALE_PAYMENT = value; NotifyPropertyChanged("PaymentLoad"); }
        }
        public RES_SALE_PAYMENT_HEADER mRES_SALE_PAYMENT_HEADER = new RES_SALE_PAYMENT_HEADER();
        public RES_STATUS mRES_STATUS = new RES_STATUS();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_SALE_PAYMENT rES_SALE_PAYMENT = new RES_SALE_PAYMENT();


        public RES_SALE_PAYMENT RES_SALE_PAYMENT
        {
            get { return rES_SALE_PAYMENT; }
            set { rES_SALE_PAYMENT = value; NotifyPropertyChanged("RES_SALE_PAYMENT"); }
        }
        public RES_SALE_PAYMENT_HEADER RES_SALE_PAYMENT_HEADER
        {
            get { return mRES_SALE_PAYMENT_HEADER; }
            set { mRES_SALE_PAYMENT_HEADER = value; NotifyPropertyChanged("RES_SALE_PAYMENT_HEADER"); }
        }

        public RES_STATUS RES_STATUS
        {
            get { return mRES_STATUS; }
            set { mRES_STATUS = value; NotifyPropertyChanged("RES_STATUS"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public List<RES_SALE_PAYMENT> mMyPaymentClosedList;
        public List<RES_SALE_PAYMENT> MyPaymentClosedList
        {
            get { return mMyPaymentClosedList; }
            set { mMyPaymentClosedList = value; NotifyPropertyChanged("MyPaymentClosedList"); }
        }

        public List<RES_SALE_PAYMENT> mMyPaymentActiveList;
        public List<RES_SALE_PAYMENT> MyPaymentActiveList
        {
            get { return mMyPaymentActiveList; }
            set { mMyPaymentActiveList = value; NotifyPropertyChanged("MyPaymentActiveList"); }
        }

        public List<RES_SALE_PAYMENT> mMyPaymentPartialList;
        public List<RES_SALE_PAYMENT> MyPaymentPartialList
        {
            get { return mMyPaymentPartialList; }
            set { mMyPaymentPartialList = value; NotifyPropertyChanged("MyPaymentPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getMyPayment());
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
        private void bindDataTab(List<RES_SALE_PAYMENT> argRES_SALE_PAYMENT_HEADER_LST)
        {
            try
            {
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_HEADER_ACTIVE = new List<RES_SALE_PAYMENT>();
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_HEADER_PARTIAL= new List<RES_SALE_PAYMENT>();
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_HEADER_CLOSED= new List<RES_SALE_PAYMENT>();

                if (argRES_SALE_PAYMENT_HEADER_LST != null && argRES_SALE_PAYMENT_HEADER_LST.Count > 0)
                {                   

                    foreach (RES_SALE_PAYMENT l_RES_SALE_PAYMENT_HEADER in argRES_SALE_PAYMENT_HEADER_LST)
                    {
                        l_RES_SALE_PAYMENT_HEADER.PaymentDate = Utility.getDateTimeString(l_RES_SALE_PAYMENT_HEADER.PaymentDate);
                        if (l_RES_SALE_PAYMENT_HEADER.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_PAYMENT_HEADER_ACTIVE.Add(l_RES_SALE_PAYMENT_HEADER);
                        }
                        else if (l_RES_SALE_PAYMENT_HEADER.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_PAYMENT_HEADER_PARTIAL.Add(l_RES_SALE_PAYMENT_HEADER);
                        }
                        else if (l_RES_SALE_PAYMENT_HEADER.StatusAsk.Equals("3"))
                        {
                            l_RES_SALE_PAYMENT_HEADER_CLOSED.Add(l_RES_SALE_PAYMENT_HEADER);
                        }
                    }

                    RES_SALE_PAYMENT = argRES_SALE_PAYMENT_HEADER_LST[0];
                    MyPaymentClosedList = l_RES_SALE_PAYMENT_HEADER_CLOSED;
                    MyPaymentActiveList = l_RES_SALE_PAYMENT_HEADER_ACTIVE;
                    MyPaymentPartialList = l_RES_SALE_PAYMENT_HEADER_PARTIAL;
                }
                else
                {
                    MyPaymentClosedList = new List<RES_SALE_PAYMENT>();
                    MyPaymentActiveList = new List<RES_SALE_PAYMENT>();
                    MyPaymentPartialList = new List<RES_SALE_PAYMENT>();
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
                List<RES_SALE_PAYMENT> l_RES_SALE_PAYMENT_HEADER_lst = new List<RES_SALE_PAYMENT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_PAYMENT l_RES_SALE_PAYMENT_HEADER in mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_PAYMENT_HEADER.CustomerName_0_255.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT_HEADER.PaymentCode_0_50.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT_HEADER.GSTAmount.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT_HEADER.PaymentTypeName_0_255.ToLower().Contains(argKeyword)
                            || l_RES_SALE_PAYMENT_HEADER.PaymentDate.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_PAYMENT_HEADER_lst.Add(l_RES_SALE_PAYMENT_HEADER);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_PAYMENT_HEADER_lst = mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT;// OriginalMyPaymentClosedList.GetRange(0, OriginalMyPaymentClosedList.Count);
                }
                bindDataTab(l_RES_SALE_PAYMENT_HEADER_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getMyPayment()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_PAYMENT_LST);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsloadSalePayHis);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_PAYMENT_LST = JsonConvert.DeserializeObject<JSN_SALE_PAYMENT_LST>(mResponse);
                    if (mJSN_SALE_PAYMENT_LST.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_PAYMENT_LST.Message.Message);
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

        public async void saveMyPayment()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_PAYMENT_LST);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleBillJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_PAYMENT_LST = JsonConvert.DeserializeObject<JSN_SALE_PAYMENT_LST>(mResponse);
                    if (mJSN_SALE_PAYMENT_LST.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT.Count > 0)
                        {
                            RES_SALE_PAYMENT = this.mJSN_SALE_PAYMENT_LST.RES_SALE_PAYMENT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_PAYMENT_LST.Message.Message);
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

        public async void loadMyPayment()
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
