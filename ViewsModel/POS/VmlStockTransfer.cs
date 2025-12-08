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
    public class VmlStockTransfer : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_INVENTORY_TRANSFER mJSN_REQ_INVENTORY_TRANSFER = new JSN_REQ_INVENTORY_TRANSFER();
        public JSN_INVENTRY_TRANSFER mJSN_INVENTRY_TRANSFER = new JSN_INVENTRY_TRANSFER();
        public JSN_LOAD_INVENTRY_TRANSFER mJSN_LOAD_INVENTRY_TRANSFER = new JSN_LOAD_INVENTRY_TRANSFER();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlStockTransfer()
        {
            this.switchDisplayView(DisplayView.Card);
            TransferLoad = new JSN_LOAD_INVENTRY_TRANSFER();
            TransferClosedlist = new List<RES_INVENTORY_TRANSFER>();
            TransferActiveList = new List<RES_INVENTORY_TRANSFER>();
            TransferPartialList = new List<RES_INVENTORY_TRANSFER>();
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
        public JSN_LOAD_INVENTRY_TRANSFER JSN_LOAD_INVENTRY_TRANSFER = new JSN_LOAD_INVENTRY_TRANSFER();
        public JSN_LOAD_INVENTRY_TRANSFER TransferLoad
        {
            get { return JSN_LOAD_INVENTRY_TRANSFER; }
            set { JSN_LOAD_INVENTRY_TRANSFER = value; NotifyPropertyChanged("TransferLoad"); }
        }
        public RES_INVENTORY_TRANSFER mRES_INVENTORY_TRANSFER = new RES_INVENTORY_TRANSFER();

        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_INVENTORY_TRANSFER RES_INVENTORY_TRANSFER
        {
            get { return mRES_INVENTORY_TRANSFER; }
            set { mRES_INVENTORY_TRANSFER = value; NotifyPropertyChanged("RES_INVENTORY_TRANSFER"); }
        }
      


        public List<RES_INVENTORY_TRANSFER> mTransferClosedlist;
        public List<RES_INVENTORY_TRANSFER> TransferClosedlist
        {
            get { return mTransferClosedlist; }
            set { mTransferClosedlist = value; NotifyPropertyChanged("TransferClosedlist"); }
        }

        public List<RES_INVENTORY_TRANSFER> mTransferActiveList;
        public List<RES_INVENTORY_TRANSFER> TransferActiveList
        {
            get { return mTransferActiveList; }
            set { mTransferActiveList = value; NotifyPropertyChanged("TransferActiveList"); }
        }

        public List<RES_INVENTORY_TRANSFER> mTransferPartialList;
        public List<RES_INVENTORY_TRANSFER> TransferPartialList
        {
            get { return mTransferPartialList; }
            set { mTransferPartialList = value; NotifyPropertyChanged("TransferPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getStockTransfer());
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
        private void bindDataTab(List<RES_INVENTORY_TRANSFER> argRES_INVENTORY_TRANSFER_LST)
        {
            try
            {
                List<RES_INVENTORY_TRANSFER> l_RES_INVENTORY_TRANSFER_ACTIVE = new List<RES_INVENTORY_TRANSFER>();
                List<RES_INVENTORY_TRANSFER> l_RES_INVENTORY_TRANSFER_PARTIAL = new List<RES_INVENTORY_TRANSFER>();
                List<RES_INVENTORY_TRANSFER> l_RES_INVENTORY_TRANSFER_CLOSED = new List<RES_INVENTORY_TRANSFER>();


                if (argRES_INVENTORY_TRANSFER_LST != null && argRES_INVENTORY_TRANSFER_LST.Count > 0)
                {
                    foreach (RES_INVENTORY_TRANSFER l_RES_INVENTORY_TRANSFER in argRES_INVENTORY_TRANSFER_LST)
                    {
                        l_RES_INVENTORY_TRANSFER.TransferDate = Utility.getDateTimeString(l_RES_INVENTORY_TRANSFER.TransferDate);


                        if (l_RES_INVENTORY_TRANSFER.StatusAsk == "1")
                        {
                            l_RES_INVENTORY_TRANSFER_ACTIVE.Add(l_RES_INVENTORY_TRANSFER);
                        }
                        else if (l_RES_INVENTORY_TRANSFER.StatusAsk == "8")
                        {
                            l_RES_INVENTORY_TRANSFER_PARTIAL.Add(l_RES_INVENTORY_TRANSFER);
                        }
                        else if (l_RES_INVENTORY_TRANSFER.StatusAsk == "3")
                        {
                            l_RES_INVENTORY_TRANSFER_CLOSED.Add(l_RES_INVENTORY_TRANSFER);
                        }
                    }
                    RES_INVENTORY_TRANSFER = argRES_INVENTORY_TRANSFER_LST[0];
                    TransferClosedlist = l_RES_INVENTORY_TRANSFER_CLOSED;
                    TransferActiveList = l_RES_INVENTORY_TRANSFER_ACTIVE;
                    TransferPartialList = l_RES_INVENTORY_TRANSFER_PARTIAL;
                }
                else
                {
                    TransferClosedlist = new List<RES_INVENTORY_TRANSFER>();
                    TransferActiveList = new List<RES_INVENTORY_TRANSFER>();
                    TransferPartialList = new List<RES_INVENTORY_TRANSFER>();
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
                List<RES_INVENTORY_TRANSFER> l_RES_INVENTORY_TRANSFER_lst = new List<RES_INVENTORY_TRANSFER>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_INVENTORY_TRANSFER l_RES_INVENTORY_TRANSFER in mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_INVENTORY_TRANSFER.TransferCode.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_TRANSFER.TransferDate.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_TRANSFER.ToLocationName.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_TRANSFER.TransferTypeName.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_TRANSFER.FromLocationName.ToLower().Contains(argKeyword))
                        {
                            l_RES_INVENTORY_TRANSFER_lst.Add(l_RES_INVENTORY_TRANSFER);
                        }
                    }
                }
                else
                {
                    l_RES_INVENTORY_TRANSFER_lst = mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER;// OriginalTransferClosedlist.GetRange(0, OriginalTransferClosedlist.Count);
                }
                bindDataTab(l_RES_INVENTORY_TRANSFER_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getStockTransfer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_TRANSFER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventryTransfer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTRY_TRANSFER = JsonConvert.DeserializeObject<JSN_INVENTRY_TRANSFER>(mResponse);
                    if (mJSN_INVENTRY_TRANSFER.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER.Count > 0)
                        {
                            bindDataTab(this.mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTRY_TRANSFER.Message.Message);
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
        public async void saveStockTransfer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_TRANSFER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveInventryTransfer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTRY_TRANSFER = JsonConvert.DeserializeObject<JSN_INVENTRY_TRANSFER>(mResponse);
                    if (mJSN_INVENTRY_TRANSFER.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER.Count > 0)
                        {
                            RES_INVENTORY_TRANSFER = this.mJSN_INVENTRY_TRANSFER.RES_INVENTRY_TRANSFER[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTRY_TRANSFER.Message.Message);
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
        public async void loadStockTransfer()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadInventryTransfer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_INVENTRY_TRANSFER = JsonConvert.DeserializeObject<JSN_LOAD_INVENTRY_TRANSFER>(mResponse);
                    if (mJSN_LOAD_INVENTRY_TRANSFER.Message.Code == "7")
                    {
                        this.TransferLoad = mJSN_LOAD_INVENTRY_TRANSFER;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_INVENTRY_TRANSFER.Message.Message);
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
