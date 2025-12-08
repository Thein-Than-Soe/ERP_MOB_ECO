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
    public class VmlSalesReturn : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_RETURN_JUN mJSN_REQ_SALE_RETURN_JUN = new JSN_REQ_SALE_RETURN_JUN();
        public JSN_SALE_RETURN_JUN mJSN_SALE_RETURN_JUN = new JSN_SALE_RETURN_JUN();
        public JSN_LOAD_SALE_RETURN mJSN_LOAD_SALE_RETURN = new JSN_LOAD_SALE_RETURN();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSalesReturn()
        {
            this.switchDisplayView(DisplayView.Card);
            ReturnLoad = new JSN_LOAD_SALE_RETURN();
            ReturnClosedList = new List<RES_SALE_RETURN>();
            ReturnActiveList = new List<RES_SALE_RETURN>();
            ReturnPartialList = new List<RES_SALE_RETURN>();
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
        public JSN_LOAD_SALE_RETURN JSN_LOAD_SALE_RETURN = new JSN_LOAD_SALE_RETURN();
        public JSN_LOAD_SALE_RETURN ReturnLoad
        {
            get { return JSN_LOAD_SALE_RETURN; }
            set { JSN_LOAD_SALE_RETURN = value; NotifyPropertyChanged("ReturnLoad"); }
        }
        public RES_SALE_RETURN mRES_SALE_RETURN = new RES_SALE_RETURN();
        public RES_SALE_BROWSE mRES_SALE_BROWSE = new RES_SALE_BROWSE();
        public RES_SALE_RETURN_DETAIL rES_SALE_RETURN_DETAIL = new RES_SALE_RETURN_DETAIL();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_SALE_PAYMENT rES_SALE_PAYMENT = new RES_SALE_PAYMENT();

        public RES_SALE_PAYMENT RES_SALE_PAYMENT
        {
            get { return rES_SALE_PAYMENT; }
            set { rES_SALE_PAYMENT = value; NotifyPropertyChanged("RES_SALE_PAYMENT"); }
        }
        public RES_SALE_RETURN RES_SALE_RETURN
        {
            get { return mRES_SALE_RETURN; }
            set { mRES_SALE_RETURN = value; NotifyPropertyChanged("RES_SALE_RETURN"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_SALE_RETURN"); }
        }
        public RES_SALE_BROWSE RES_SALE_BROWSE
        {
            get { return mRES_SALE_BROWSE; }
            set { mRES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_RETURN"); }
        }

        public RES_SALE_RETURN_DETAIL RES_SALE_RETURN_DETAIL
        {
            get { return rES_SALE_RETURN_DETAIL; }
            set { rES_SALE_RETURN_DETAIL = value; NotifyPropertyChanged("RES_SALE_RETURN"); }
        }


        public List<RES_SALE_RETURN> mReturnClosedList;
        public List<RES_SALE_RETURN> ReturnClosedList
        {
            get { return mReturnClosedList; }
            set { mReturnClosedList = value; NotifyPropertyChanged("ReturnClosedList"); }
        }

        public List<RES_SALE_RETURN> mReturnActiveList;
        public List<RES_SALE_RETURN> ReturnActiveList
        {
            get { return mReturnActiveList; }
            set { mReturnActiveList = value; NotifyPropertyChanged("ReturnActiveList"); }
        }

        public List<RES_SALE_RETURN> mReturnPartialList;
        public List<RES_SALE_RETURN> ReturnPartialList
        {
            get { return mReturnPartialList; }
            set { mReturnPartialList = value; NotifyPropertyChanged("ReturnPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getReturn());
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
        private void bindDataTab(List<RES_SALE_RETURN> argRES_SALE_RETURN_LST)
        {
            try
            {
                List<RES_SALE_RETURN> l_RES_SALE_RETURN_ACTIVE = new List<RES_SALE_RETURN>();
                List<RES_SALE_RETURN> l_RES_SALE_RETURN_PARTIAL = new List<RES_SALE_RETURN>();
                List<RES_SALE_RETURN> l_RES_SALE_RETURN_CLOSED= new List<RES_SALE_RETURN>();

                if (argRES_SALE_RETURN_LST != null && argRES_SALE_RETURN_LST.Count > 0)
                {

                    foreach (RES_SALE_RETURN l_RES_SALE_RETURN in argRES_SALE_RETURN_LST)
                    {
                        l_RES_SALE_RETURN.ReturnDate = Utility.getDateTimeString(l_RES_SALE_RETURN.ReturnDate);

                        if (l_RES_SALE_RETURN.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_RETURN_ACTIVE.Add(l_RES_SALE_RETURN);
                        }
                        else if (l_RES_SALE_RETURN.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_RETURN_PARTIAL.Add(l_RES_SALE_RETURN);
                        }
                        else if (l_RES_SALE_RETURN.StatusAsk.Equals("3"))
                        {
                            l_RES_SALE_RETURN_CLOSED.Add(l_RES_SALE_RETURN);
                        }
                    }

                    RES_SALE_RETURN = argRES_SALE_RETURN_LST[0];
                    ReturnClosedList = l_RES_SALE_RETURN_CLOSED;
                    ReturnActiveList = l_RES_SALE_RETURN_ACTIVE;
                    ReturnPartialList = l_RES_SALE_RETURN_PARTIAL;
                }
                else
                {
                    ReturnClosedList = new List<RES_SALE_RETURN>();
                    ReturnActiveList = new List<RES_SALE_RETURN>();
                    ReturnPartialList = new List<RES_SALE_RETURN>();
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
                List<RES_SALE_RETURN> l_RES_SALE_RETURN_lst = new List<RES_SALE_RETURN>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_RETURN l_RES_SALE_RETURN in mJSN_SALE_RETURN_JUN.RES_SALE_RETURN)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_RETURN.ReturnCode.ToLower().Contains(argKeyword)
                            || l_RES_SALE_RETURN.CustomerName.ToLower().Contains(argKeyword)
                            || l_RES_SALE_RETURN.GrandTotal.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_RETURN_lst.Add(l_RES_SALE_RETURN);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_RETURN_lst = mJSN_SALE_RETURN_JUN.RES_SALE_RETURN;// OriginalReturnClosedList.GetRange(0, OriginalReturnClosedList.Count);
                }
                bindDataTab(l_RES_SALE_RETURN_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getReturn()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_RETURN_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleReturnJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_RETURN_JUN = JsonConvert.DeserializeObject<JSN_SALE_RETURN_JUN>(mResponse);
                    if (mJSN_SALE_RETURN_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_RETURN_JUN.RES_SALE_RETURN.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_RETURN_JUN.RES_SALE_RETURN);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_RETURN_JUN.Message.Message);
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

        public async void saveReturn()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_RETURN_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveSaleReturnJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_RETURN_JUN = JsonConvert.DeserializeObject<JSN_SALE_RETURN_JUN>(mResponse);
                    if (mJSN_SALE_RETURN_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_RETURN_JUN.RES_SALE_RETURN.Count > 0)
                        {
                            RES_SALE_RETURN = this.mJSN_SALE_RETURN_JUN.RES_SALE_RETURN[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_RETURN_JUN.Message.Message);
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

        public async void loadReturn()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleReturn);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_RETURN = JsonConvert.DeserializeObject<JSN_LOAD_SALE_RETURN>(mResponse);
                    if (mJSN_LOAD_SALE_RETURN.Message.Code == "7")
                    {
                        this.ReturnLoad = mJSN_LOAD_SALE_RETURN;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_RETURN.Message.Message);
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
