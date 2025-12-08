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
    public class VmlPurchaseReturn : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_RETURN_JUN mJSN_REQ_PURCHASE_RETURN_JUN = new JSN_REQ_PURCHASE_RETURN_JUN();
        public JSN_PURCHASE_RETURN_JUN mJSN_PURCHASE_RETURN_JUN = new JSN_PURCHASE_RETURN_JUN();
        public JSN_LOAD_PURCHASE_RETURN mJSN_LOAD_PURCHASE_RETURN = new JSN_LOAD_PURCHASE_RETURN();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseReturn()
        {
            this.switchDisplayView(DisplayView.Card);
            ReturnLoad = new JSN_LOAD_PURCHASE_RETURN();
            ReturnClosedList = new List<RES_PURCHASE_RETURN>();
            ReturnActiveList = new List<RES_PURCHASE_RETURN>();
            ReturnPartialList = new List<RES_PURCHASE_RETURN>();
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
        public JSN_LOAD_PURCHASE_RETURN JSN_LOAD_PURCHASE_RETURN = new JSN_LOAD_PURCHASE_RETURN();
        public JSN_LOAD_PURCHASE_RETURN ReturnLoad
        {
            get { return JSN_LOAD_PURCHASE_RETURN; }
            set { JSN_LOAD_PURCHASE_RETURN = value; NotifyPropertyChanged("ReturnLoad"); }
        }


        public RES_PURCHASE_RETURN mRES_PURCHASE_RETURN = new RES_PURCHASE_RETURN();
        public RES_PURCHASE_RETURN_DETAIL mRES_PURCHASE_RETURN_DETAIL = new RES_PURCHASE_RETURN_DETAIL();
        public RES_COMPANY mRES_COMPANY = new RES_COMPANY();
        public RES_PURCHASE_BROWSE rES_PURCHASE_BROWSE = new RES_PURCHASE_BROWSE();

        public RES_PURCHASE_BROWSE RES_PURCHASE_BROWSE
        {
            get { return rES_PURCHASE_BROWSE; }
            set { rES_PURCHASE_BROWSE = value; NotifyPropertyChanged("rES_PURCHASE_BROWSE"); }
        }

        public RES_PURCHASE_RETURN RES_PURCHASE_RETURN
        {
            get { return mRES_PURCHASE_RETURN; }
            set { mRES_PURCHASE_RETURN = value; NotifyPropertyChanged("RES_PURCHASE_RETURN"); }
        }
        public RES_PURCHASE_RETURN_DETAIL RES_PURCHASE_RETURN_DETAIL
        {
            get { return mRES_PURCHASE_RETURN_DETAIL; }
            set { mRES_PURCHASE_RETURN_DETAIL = value; NotifyPropertyChanged("RES_PURCHASE_RETURN_DETAIL"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return mRES_COMPANY; }
            set { mRES_COMPANY = value; NotifyPropertyChanged("RES_PURCHASE_RETURN_DETAIL"); }
        }

        public List<RES_PURCHASE_RETURN> mReturnClosedList;
        public List<RES_PURCHASE_RETURN> ReturnClosedList
        {
            get { return mReturnClosedList; }
            set { mReturnClosedList = value; NotifyPropertyChanged("ReturnClosedList"); }
        }

        public List<RES_PURCHASE_RETURN> mReturnActiveList;
        public List<RES_PURCHASE_RETURN> ReturnActiveList
        {
            get { return mReturnActiveList; }
            set { mReturnActiveList = value; NotifyPropertyChanged("ReturnActiveList"); }
        }

        public List<RES_PURCHASE_RETURN> mReturnPartialList;
        public List<RES_PURCHASE_RETURN> ReturnPartialList
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
        private void bindDataTab(List<RES_PURCHASE_RETURN> argRES_PURCHASE_RETURN_LST)
        {
            try
            {
                List<RES_PURCHASE_RETURN> l_RES_PURCHASE_RETURN_ACTIVE = new List<RES_PURCHASE_RETURN>();
                List<RES_PURCHASE_RETURN> l_RES_PURCHASE_RETURN_PARTIAL = new List<RES_PURCHASE_RETURN>();
                List<RES_PURCHASE_RETURN> l_RES_PURCHASE_RETURN_CLOSED = new List<RES_PURCHASE_RETURN>();
                if (argRES_PURCHASE_RETURN_LST != null && argRES_PURCHASE_RETURN_LST.Count > 0)
                {

                    foreach (RES_PURCHASE_RETURN l_RES_PURCHASE_RETURN in argRES_PURCHASE_RETURN_LST)
                    {
                       
                        l_RES_PURCHASE_RETURN.ReturnDate = Utility.getDateTimeString(l_RES_PURCHASE_RETURN.ReturnDate);

                        if (l_RES_PURCHASE_RETURN.StatusAsk.Equals("1"))
                        {
                            l_RES_PURCHASE_RETURN_ACTIVE.Add(l_RES_PURCHASE_RETURN);
                        }
                        else if (l_RES_PURCHASE_RETURN.StatusAsk.Equals("2"))
                        {
                            l_RES_PURCHASE_RETURN_PARTIAL.Add(l_RES_PURCHASE_RETURN);
                        }
                        else if (l_RES_PURCHASE_RETURN.StatusAsk.Equals("9"))
                        {
                            l_RES_PURCHASE_RETURN_CLOSED.Add(l_RES_PURCHASE_RETURN);
                        }
                    }

                    RES_PURCHASE_RETURN = argRES_PURCHASE_RETURN_LST[0];
                    ReturnClosedList = l_RES_PURCHASE_RETURN_CLOSED;
                    ReturnActiveList = l_RES_PURCHASE_RETURN_ACTIVE;
                    ReturnPartialList = l_RES_PURCHASE_RETURN_PARTIAL;
                }
                else
                {
                    ReturnClosedList = new List<RES_PURCHASE_RETURN>();
                    ReturnActiveList = new List<RES_PURCHASE_RETURN>();
                    ReturnPartialList = new List<RES_PURCHASE_RETURN>();
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
                List<RES_PURCHASE_RETURN> l_RES_PURCHASE_RETURN_lst = new List<RES_PURCHASE_RETURN>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_PURCHASE_RETURN l_RES_PURCHASE_RETURN in mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_PURCHASE_RETURN.ReturnCode.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_RETURN.SupplierName.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_RETURN.ParentCode.ToLower().Contains(argKeyword))
                        {
                            l_RES_PURCHASE_RETURN_lst.Add(l_RES_PURCHASE_RETURN);
                        }
                    }
                }
                else
                {
                    l_RES_PURCHASE_RETURN_lst = mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN;// OriginalReturnClosedList.GetRange(0, OriginalReturnClosedList.Count);
                }
                bindDataTab(l_RES_PURCHASE_RETURN_lst);
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
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_RETURN_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseReturn);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_RETURN_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_RETURN_JUN>(mResponse);
                    if (mJSN_PURCHASE_RETURN_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN.Count > 0)
                        {
                            bindDataTab(this.mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_RETURN_JUN.Message.Message);
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
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_RETURN_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseReturn);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_RETURN_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_RETURN_JUN>(mResponse);
                    if (mJSN_PURCHASE_RETURN_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN.Count > 0)
                        {
                            RES_PURCHASE_RETURN = this.mJSN_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_RETURN_JUN.Message.Message);
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
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadPurchaseReturn);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_RETURN = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_RETURN>(mResponse);
                    if (mJSN_LOAD_PURCHASE_RETURN.Message.Code == "7")
                    {
                        this.ReturnLoad = mJSN_LOAD_PURCHASE_RETURN;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_RETURN.Message.Message);
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
