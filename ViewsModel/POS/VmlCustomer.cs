using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
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
    public class VmlCustomer : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_CUSTOMER mJSN_REQ_CUSTOMER = new JSN_REQ_CUSTOMER();
        public JSN_CUSTOMERNCONTACT mJSN_CUSTOMERNCONTACT = new JSN_CUSTOMERNCONTACT();
        public JSN_LOAD_CUSTOMER mJSN_LOAD_CUSTOMER =new JSN_LOAD_CUSTOMER();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlCustomer()
        {
            this.switchDisplayView(DisplayView.Card);
            CustomerLoad = new JSN_LOAD_CUSTOMER();
            CustomerList = new List<RES_CUSTOMER>();
            CustomerActiveList = new List<RES_CUSTOMER>();
            CustomerInActiveList = new List<RES_CUSTOMER>();
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
        public JSN_LOAD_CUSTOMER JSN_LOAD_CUSTOMER = new JSN_LOAD_CUSTOMER();
        public JSN_LOAD_CUSTOMER CustomerLoad
        {
            get { return JSN_LOAD_CUSTOMER; }
            set { JSN_LOAD_CUSTOMER = value; NotifyPropertyChanged("CustomerLoad"); }
        }

        public RES_CUSTOMER mRES_CUSTOMER = new RES_CUSTOMER();
        public RES_CUSTOMER RES_CUSTOMER
        {
            get { return mRES_CUSTOMER; }
            set { mRES_CUSTOMER = value; NotifyPropertyChanged("RES_CUSTOMER"); }
        }

        public List<RES_CUSTOMER> mCustomerList;
        public List<RES_CUSTOMER> CustomerList
        {
            get { return mCustomerList; }
            set { mCustomerList = value; NotifyPropertyChanged("CustomerList"); }
        }

        public List<RES_CUSTOMER> mCustomerActiveList;
        public List<RES_CUSTOMER> CustomerActiveList
        {
            get { return mCustomerActiveList; }
            set { mCustomerActiveList = value; NotifyPropertyChanged("CustomerActiveList"); }
        }

        public List<RES_CUSTOMER> mCustomerInActiveList;
        public List<RES_CUSTOMER> CustomerInActiveList
        {
            get { return mCustomerInActiveList; }
            set { mCustomerInActiveList = value; NotifyPropertyChanged("CustomerInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getCustomer());
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
        private void bindDataTab(List<RES_CUSTOMER> argRES_CUSTOMER_LST)
        {
            try
            {
                List<RES_CUSTOMER> l_RES_CUSTOMER_ACTIVE = new List<RES_CUSTOMER>();
                List<RES_CUSTOMER> l_RES_CUSTOMER_InACTIVE = new List<RES_CUSTOMER>();
                if (argRES_CUSTOMER_LST != null && argRES_CUSTOMER_LST.Count > 0)
                {

                    foreach (RES_CUSTOMER l_RES_CUSTOMER in argRES_CUSTOMER_LST)
                    {
                        if (l_RES_CUSTOMER.StatusAsk.Equals("1"))
                        {
                            l_RES_CUSTOMER_ACTIVE.Add(l_RES_CUSTOMER);
                        }
                        else if (l_RES_CUSTOMER.StatusAsk.Equals("8"))
                        {
                            l_RES_CUSTOMER_InACTIVE.Add(l_RES_CUSTOMER);
                        }
                    }

                    RES_CUSTOMER = argRES_CUSTOMER_LST[0];
                    CustomerList = argRES_CUSTOMER_LST;
                    CustomerActiveList = l_RES_CUSTOMER_ACTIVE;
                    CustomerInActiveList = l_RES_CUSTOMER_InACTIVE;
                }
                else
                {
                    CustomerList = new List<RES_CUSTOMER>();
                    CustomerActiveList = new List<RES_CUSTOMER>();
                    CustomerInActiveList = new List<RES_CUSTOMER>();
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
                List<RES_CUSTOMER> l_RES_CUSTOMER_lst = new List<RES_CUSTOMER>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_CUSTOMER l_RES_CUSTOMER in mJSN_CUSTOMERNCONTACT.RES_CUSTOMER)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_CUSTOMER.CustomerCode.ToLower().Contains(argKeyword)
                            || l_RES_CUSTOMER.CustomerName.ToLower().Contains(argKeyword)
                            || l_RES_CUSTOMER.CustomerMobilePhone.ToLower().Contains(argKeyword)
                            || l_RES_CUSTOMER.CustomerEmail.ToLower().Contains(argKeyword))
                        {
                            l_RES_CUSTOMER_lst.Add(l_RES_CUSTOMER);
                        }
                    }
                }
                else
                {
                    l_RES_CUSTOMER_lst = mJSN_CUSTOMERNCONTACT.RES_CUSTOMER;// OriginalCustomerList.GetRange(0, OriginalCustomerList.Count);
                }
                bindDataTab(l_RES_CUSTOMER_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getCustomer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CUSTOMER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetCustomerAndContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_CUSTOMERNCONTACT = JsonConvert.DeserializeObject<JSN_CUSTOMERNCONTACT>(mResponse);
                    if (mJSN_CUSTOMERNCONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_CUSTOMERNCONTACT.RES_CUSTOMER.Count > 0)
                        {
                            bindDataTab(this.mJSN_CUSTOMERNCONTACT.RES_CUSTOMER);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_CUSTOMERNCONTACT.Message.Message);
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

        public async void saveCustomer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CUSTOMER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveCustomerContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_CUSTOMERNCONTACT = JsonConvert.DeserializeObject<JSN_CUSTOMERNCONTACT>(mResponse);
                    if (mJSN_CUSTOMERNCONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_CUSTOMERNCONTACT.RES_CUSTOMER.Count > 0)
                        {
                            RES_CUSTOMER = this.mJSN_CUSTOMERNCONTACT.RES_CUSTOMER[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_CUSTOMERNCONTACT.Message.Message);
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

        public async void loadCustomer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadCustomer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_CUSTOMER = JsonConvert.DeserializeObject<JSN_LOAD_CUSTOMER>(mResponse);
                    if (mJSN_LOAD_CUSTOMER.Message.Code == "7")
                    {
                        this.CustomerLoad = mJSN_LOAD_CUSTOMER;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_CUSTOMER.Message.Message);
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
