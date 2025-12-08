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
    public class VmlStockReceived : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_INVENTORY_RECEIVED_JUN mJSN_REQ_INVENTORY_RECEIVED_JUN = new JSN_REQ_INVENTORY_RECEIVED_JUN();
        public JSN_INVENTORY_RECEIVED_JUN mJSN_INVENTORY_RECEIVED_JUN = new JSN_INVENTORY_RECEIVED_JUN();
        public JSN_LOAD_INVENTRY_RECEIVED mJSN_LOAD_INVENTRY_RECEIVED = new JSN_LOAD_INVENTRY_RECEIVED();


        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlStockReceived()
        {
            this.switchDisplayView(DisplayView.Card);
            ReceivedLoad = new JSN_LOAD_INVENTRY_RECEIVED();
            ReceivedClosedList = new List<RES_INVENTORY_RECEIVED>();
            ReceivedActiveList = new List<RES_INVENTORY_RECEIVED>();
            ReceivedPartialList = new List<RES_INVENTORY_RECEIVED>();
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
        public JSN_LOAD_INVENTRY_RECEIVED JSN_LOAD_INVENTRY_RECEIVED = new JSN_LOAD_INVENTRY_RECEIVED();
        public JSN_LOAD_INVENTRY_RECEIVED ReceivedLoad
        {
            get { return JSN_LOAD_INVENTRY_RECEIVED; }
            set { JSN_LOAD_INVENTRY_RECEIVED = value; NotifyPropertyChanged("ReceivedLoad"); }
        }

        public RES_INVENTORY_RECEIVED mRES_INVENTORY_RECEIVED = new RES_INVENTORY_RECEIVED();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_PURCHASE_BROWSE mRES_PURCHASE_BROWSE = new RES_PURCHASE_BROWSE();
        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_PURCHASE_BROWSE RES_PURCHASE_BROWSE
        {
            get { return mRES_PURCHASE_BROWSE; }
            set { mRES_PURCHASE_BROWSE = value; NotifyPropertyChanged("RES_INVENTORY_RECEIVED"); }
        }

        public RES_INVENTORY_RECEIVED RES_INVENTORY_RECEIVED
        {
            get { return mRES_INVENTORY_RECEIVED; }
            set { mRES_INVENTORY_RECEIVED = value; NotifyPropertyChanged("RES_INVENTORY_RECEIVED"); }
        }

        public List<RES_INVENTORY_RECEIVED> mReceivedClosedList;
        public List<RES_INVENTORY_RECEIVED> ReceivedClosedList
        {
            get { return mReceivedClosedList; }
            set { mReceivedClosedList = value; NotifyPropertyChanged("ReceivedClosedList"); }
        }

        public List<RES_INVENTORY_RECEIVED> mReceivedActiveList;
        public List<RES_INVENTORY_RECEIVED> ReceivedActiveList
        {
            get { return mReceivedActiveList; }
            set { mReceivedActiveList = value; NotifyPropertyChanged("ReceivedActiveList"); }
        }

        public List<RES_INVENTORY_RECEIVED> mReceivedPartialList;
        public List<RES_INVENTORY_RECEIVED> ReceivedPartialList
        {
            get { return mReceivedPartialList; }
            set { mReceivedPartialList = value; NotifyPropertyChanged("ReceivedPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getStockReceived());
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
        private void bindDataTab(List<RES_INVENTORY_RECEIVED> argRES_INVENTORY_RECEIVED_LST)
        {
            try
            {
                List<RES_INVENTORY_RECEIVED> l_RES_INVENTORY_RECEIVED_ACTIVE = new List<RES_INVENTORY_RECEIVED>();
                List<RES_INVENTORY_RECEIVED> l_RES_INVENTORY_RECEIVED_PARTIAL = new List<RES_INVENTORY_RECEIVED>();
                List<RES_INVENTORY_RECEIVED> l_RES_INVENTORY_RECEIVED_CLOSED = new List<RES_INVENTORY_RECEIVED>();


                if (argRES_INVENTORY_RECEIVED_LST != null && argRES_INVENTORY_RECEIVED_LST.Count > 0)
                {
                    foreach (RES_INVENTORY_RECEIVED l_RES_INVENTORY_RECEIVED in argRES_INVENTORY_RECEIVED_LST)
                    {
                        l_RES_INVENTORY_RECEIVED.ReceivedDate = Utility.getDateTimeString(l_RES_INVENTORY_RECEIVED.ReceivedDate);

                        if (l_RES_INVENTORY_RECEIVED.StatusAsk == "1")
                        {
                            l_RES_INVENTORY_RECEIVED_ACTIVE.Add(l_RES_INVENTORY_RECEIVED);
                        }
                        else if (l_RES_INVENTORY_RECEIVED.StatusAsk == "8")
                        {
                            l_RES_INVENTORY_RECEIVED_PARTIAL.Add(l_RES_INVENTORY_RECEIVED);
                        }
                        else if (l_RES_INVENTORY_RECEIVED.StatusAsk == "3")
                        {
                            l_RES_INVENTORY_RECEIVED_CLOSED.Add(l_RES_INVENTORY_RECEIVED);
                        }
                    }
                    RES_INVENTORY_RECEIVED = argRES_INVENTORY_RECEIVED_LST[0];
                    ReceivedClosedList = l_RES_INVENTORY_RECEIVED_CLOSED;
                    ReceivedActiveList = l_RES_INVENTORY_RECEIVED_ACTIVE;
                    ReceivedPartialList = l_RES_INVENTORY_RECEIVED_PARTIAL;
                }
                else
                {
                    ReceivedClosedList = new List<RES_INVENTORY_RECEIVED>();
                    ReceivedActiveList = new List<RES_INVENTORY_RECEIVED>();
                    ReceivedPartialList = new List<RES_INVENTORY_RECEIVED>();
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
                List<RES_INVENTORY_RECEIVED> l_RES_INVENTORY_RECEIVED_lst = new List<RES_INVENTORY_RECEIVED>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_INVENTORY_RECEIVED l_RES_INVENTORY_RECEIVED in mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_INVENTORY_RECEIVED.ReceivedCode.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_RECEIVED.LocationName.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_RECEIVED.ReceivedTypeName.ToLower().Contains(argKeyword))
                        {
                            l_RES_INVENTORY_RECEIVED_lst.Add(l_RES_INVENTORY_RECEIVED);
                        }
                    }
                }
                else
                {
                    l_RES_INVENTORY_RECEIVED_lst = mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED;// OriginalReceivedClosedList.GetRange(0, OriginalReceivedClosedList.Count);
                }
                bindDataTab(l_RES_INVENTORY_RECEIVED_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getStockReceived()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_RECEIVED_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventoryReceivedJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_RECEIVED_JUN = JsonConvert.DeserializeObject<JSN_INVENTORY_RECEIVED_JUN>(mResponse);
                    if (mJSN_INVENTORY_RECEIVED_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED.Count > 0)
                        {
                            bindDataTab(this.mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_RECEIVED_JUN.Message.Message);
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
        public async void saveStockReceived()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_RECEIVED_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveInventryReceivedJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_RECEIVED_JUN = JsonConvert.DeserializeObject<JSN_INVENTORY_RECEIVED_JUN>(mResponse);
                    if (mJSN_INVENTORY_RECEIVED_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED.Count > 0)
                        {
                            RES_INVENTORY_RECEIVED = this.mJSN_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_RECEIVED_JUN.Message.Message);
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

        public async void loadStockReceived()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadInventryReceived);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_INVENTRY_RECEIVED = JsonConvert.DeserializeObject<JSN_LOAD_INVENTRY_RECEIVED>(mResponse);
                    if (mJSN_LOAD_INVENTRY_RECEIVED.Message.Code == "7")
                    {
                        this.ReceivedLoad = mJSN_LOAD_INVENTRY_RECEIVED;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_INVENTRY_RECEIVED.Message.Message);
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
