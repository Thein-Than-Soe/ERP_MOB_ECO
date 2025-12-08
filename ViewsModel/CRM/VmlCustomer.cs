using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.CRM.REQ;
using CS.ERP.PL.CRM.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.CRM;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.CRM
{
    public class VmlCustomer : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_CUSTOMERNCONTACT mJSN_REQ_CUSTOMERNCONTACT = new JSN_REQ_CUSTOMERNCONTACT();
        public JSN_RES_CUSTOMER_CONTACT mJSN_RES_CUSTOMER_CONTACT = new JSN_RES_CUSTOMER_CONTACT();
        public JSN_LOAD_CUSTOMER mJSN_LOAD_CUSTOMER = new JSN_LOAD_CUSTOMER();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlCustomer()
        {
            this.switchDisplayView(DisplayView.Card);
            CustomerLoad = new JSN_LOAD_CUSTOMER();
            CustomerActiveList = new List<DAT_CUSTOMER>();
            CustomerInActiveList = new List<DAT_CUSTOMER>();
            CustomerList = new List<DAT_CUSTOMER>();
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
            set { JSN_LOAD_CUSTOMER = value; NotifyPropertyChanged("CusotmerLoad"); }
        }


        public DAT_CUSTOMER mDAT_CUSTOMER = new DAT_CUSTOMER();
        public DAT_CUSTOMER DAT_CUSTOMER
        {
            get { return mDAT_CUSTOMER; }
            set { mDAT_CUSTOMER = value; NotifyPropertyChanged("DAT_CUSTOMER"); }
        }

        public List<DAT_CUSTOMER> mCustomerList;
        public List<DAT_CUSTOMER> CustomerList
        {
            get { return mCustomerList; }
            set { mCustomerList = value; NotifyPropertyChanged("CustomerList"); }
        }

        public List<DAT_CUSTOMER> mCustomerActiveList;
        public List<DAT_CUSTOMER> CustomerActiveList
        {
            get { return mCustomerActiveList; }
            set { mCustomerActiveList = value; NotifyPropertyChanged("CustomerActiveList"); }
        }

        public List<DAT_CUSTOMER> mCustomerInActiveList;
        public List<DAT_CUSTOMER> CustomerInActiveList
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
        private void bindDataTab(List<DAT_CUSTOMER> argDAT_CUSTOMER_LST)
        {
            try
            {
                List<DAT_CUSTOMER> l_DAT_CUSTOMER_ACTIVE = new List<DAT_CUSTOMER>();
                List<DAT_CUSTOMER> l_DAT_CUSTOMER_INACTIVE = new List<DAT_CUSTOMER>();

                if (argDAT_CUSTOMER_LST != null && argDAT_CUSTOMER_LST.Count > 0)
                {
                    foreach (DAT_CUSTOMER l_DAT_CUSTOMER in argDAT_CUSTOMER_LST)
                    {
                        if (l_DAT_CUSTOMER.StatusAsk.Equals("1"))
                        {
                            l_DAT_CUSTOMER_ACTIVE.Add(l_DAT_CUSTOMER);
                        }
                        else if (l_DAT_CUSTOMER.StatusAsk.Equals("8"))
                        {
                            l_DAT_CUSTOMER_INACTIVE.Add(l_DAT_CUSTOMER);
                        }
                    }

                    DAT_CUSTOMER = argDAT_CUSTOMER_LST[0];
                    CustomerList = argDAT_CUSTOMER_LST;
                    CustomerActiveList = l_DAT_CUSTOMER_ACTIVE;
                    CustomerInActiveList = l_DAT_CUSTOMER_INACTIVE;

                }
                else
                {
                    CustomerList = new List<DAT_CUSTOMER>();
                    CustomerActiveList = new List<DAT_CUSTOMER>();
                    CustomerInActiveList = new List<DAT_CUSTOMER>();

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
                List<DAT_CUSTOMER> l_DAT_CUSTOMER_lst = new List<DAT_CUSTOMER>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_CUSTOMER l_DAT_CUSTOMER in mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_CUSTOMER.CustomerName.ToLower().Contains(argKeyword)
                            || l_DAT_CUSTOMER.CustomerMobilePhone.ToLower().Contains(argKeyword)
                            || l_DAT_CUSTOMER.CustomerCode.ToLower().Contains(argKeyword))
                        {
                            l_DAT_CUSTOMER_lst.Add(l_DAT_CUSTOMER);
                        }
                    }
                }
                else
                {
                    l_DAT_CUSTOMER_lst = mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER;// OriginalCustomerList.GetRange(0, OriginalCustomerList.Count);
                }
                bindDataTab(l_DAT_CUSTOMER_lst);
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
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CUSTOMERNCONTACT);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetCustomerAndContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_CUSTOMER_CONTACT = JsonConvert.DeserializeObject<JSN_RES_CUSTOMER_CONTACT>(mResponse);
                    if (mJSN_RES_CUSTOMER_CONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_CUSTOMER_CONTACT.Message.Message);
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
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CUSTOMERNCONTACT);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsSaveCustomer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_CUSTOMER_CONTACT = JsonConvert.DeserializeObject<JSN_RES_CUSTOMER_CONTACT>(mResponse);
                    if (mJSN_RES_CUSTOMER_CONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER.Count > 0)
                        {
                            DAT_CUSTOMER = this.mJSN_RES_CUSTOMER_CONTACT.DAT_CUSTOMER[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_CUSTOMER_CONTACT.Message.Message);
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

        public async void LoadCustomer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsLoadCustomer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_CUSTOMER = JsonConvert.DeserializeObject<JSN_LOAD_CUSTOMER>(mResponse);
                    if (mJSN_LOAD_CUSTOMER.Message.Code == "7")
                    {
                        this.CustomerLoad = mJSN_LOAD_CUSTOMER;

                        //if (this.mJSN_LOAD_CUSTOMER.RES_SUPPLIER.Count > 0)
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_CUSTOMER_CONTACT.Message.Message);
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
