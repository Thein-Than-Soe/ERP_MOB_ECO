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
    public class VmlPurchaseRequestion : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_REQUESTION mJSN_REQ_PURCHASE_REQUESTION = new JSN_REQ_PURCHASE_REQUESTION();
        public JSN_PURCHASE_REQUESTION mJSN_PURCHASE_REQUESTION = new JSN_PURCHASE_REQUESTION();
        public JSN_LOAD_PURCHASE_REQUESTION mJSN_LOAD_PURCHASE_REQUESTION = new JSN_LOAD_PURCHASE_REQUESTION();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseRequestion()
        {
            this.switchDisplayView(DisplayView.Card);
           RequestionLoad = new  JSN_LOAD_PURCHASE_REQUESTION();
            RequestionClosedList = new List<RES_PURCHASE_REQUESTION>();
            RequestionActiveList = new List<RES_PURCHASE_REQUESTION>();
            RequestionPartialList = new List<RES_PURCHASE_REQUESTION>();
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
        public JSN_LOAD_PURCHASE_REQUESTION JSN_LOAD_PURCHASE_REQUESTION = new JSN_LOAD_PURCHASE_REQUESTION();
        public JSN_LOAD_PURCHASE_REQUESTION RequestionLoad
        {
            get { return JSN_LOAD_PURCHASE_REQUESTION; }
            set { JSN_LOAD_PURCHASE_REQUESTION = value; NotifyPropertyChanged("RequestionLoad"); }
        }


        public RES_PURCHASE_REQUESTION mRES_PURCHASE_REQUESTION = new RES_PURCHASE_REQUESTION();

        public RES_COMPANY rES_COMPANY = new RES_COMPANY();


        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_PURCHASE_REQUESTION"); }
        }

        public RES_PURCHASE_REQUESTION RES_PURCHASE_REQUESTION
        {
            get { return mRES_PURCHASE_REQUESTION; }
            set { mRES_PURCHASE_REQUESTION = value; NotifyPropertyChanged("RES_PURCHASE_REQUESTION"); }
        }

        public List<RES_PURCHASE_REQUESTION> mRequestionClosedList;
        public List<RES_PURCHASE_REQUESTION> RequestionClosedList
        {
            get { return mRequestionClosedList; }
            set { mRequestionClosedList = value; NotifyPropertyChanged("RequestionClosedList"); }
        }

        public List<RES_PURCHASE_REQUESTION> mRequestionActiveList;
        public List<RES_PURCHASE_REQUESTION> RequestionActiveList
        {
            get { return mRequestionActiveList; }
            set { mRequestionActiveList = value; NotifyPropertyChanged("RequestionActiveList"); }
        }

        public List<RES_PURCHASE_REQUESTION> mRequestionPartialList;
        public List<RES_PURCHASE_REQUESTION> RequestionPartialList
        {
            get { return mRequestionPartialList; }
            set { mRequestionPartialList = value; NotifyPropertyChanged("RequestionPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getRequestion());
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
        private void bindDataTab(List<RES_PURCHASE_REQUESTION> argRES_PURCHASE_REQUESTION_LST)
        {
            try
            {
                List<RES_PURCHASE_REQUESTION> l_RES_PURCHASE_REQUESTION_ACTIVE = new List<RES_PURCHASE_REQUESTION>();
                List<RES_PURCHASE_REQUESTION> l_RES_PURCHASE_REQUESTION_PARTIAL = new List<RES_PURCHASE_REQUESTION>();
                List<RES_PURCHASE_REQUESTION> l_RES_PURCHASE_REQUESTION_CLOSED = new List<RES_PURCHASE_REQUESTION>();

                if (argRES_PURCHASE_REQUESTION_LST != null && argRES_PURCHASE_REQUESTION_LST.Count > 0)
                {

                    foreach (RES_PURCHASE_REQUESTION l_RES_PURCHASE_REQUESTION in argRES_PURCHASE_REQUESTION_LST)
                    {
                        l_RES_PURCHASE_REQUESTION.RequestionDate = Utility.getDateTimeString(l_RES_PURCHASE_REQUESTION.RequestionDate);

                        if (l_RES_PURCHASE_REQUESTION.StatusAsk.Equals("1"))
                        {
                            l_RES_PURCHASE_REQUESTION_ACTIVE.Add(l_RES_PURCHASE_REQUESTION);
                        }
                        else if (l_RES_PURCHASE_REQUESTION.StatusAsk.Equals("2"))
                        {
                            l_RES_PURCHASE_REQUESTION_PARTIAL.Add(l_RES_PURCHASE_REQUESTION);
                        }
                        else if (l_RES_PURCHASE_REQUESTION.StatusAsk.Equals("9"))
                        {
                            l_RES_PURCHASE_REQUESTION_CLOSED.Add(l_RES_PURCHASE_REQUESTION);
                        }
                    }

                    RES_PURCHASE_REQUESTION = argRES_PURCHASE_REQUESTION_LST[0];
                    RequestionClosedList = l_RES_PURCHASE_REQUESTION_CLOSED;
                    RequestionActiveList = l_RES_PURCHASE_REQUESTION_ACTIVE;
                    RequestionPartialList = l_RES_PURCHASE_REQUESTION_PARTIAL;
                }
                else
                {
                    RequestionClosedList = new List<RES_PURCHASE_REQUESTION>();
                    RequestionActiveList = new List<RES_PURCHASE_REQUESTION>();
                    RequestionPartialList = new List<RES_PURCHASE_REQUESTION>();
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
                List<RES_PURCHASE_REQUESTION> l_RES_PURCHASE_REQUESTION_lst = new List<RES_PURCHASE_REQUESTION>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_PURCHASE_REQUESTION l_RES_PURCHASE_REQUESTION in mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_PURCHASE_REQUESTION.RequestionCode.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_REQUESTION.RequestionDate.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_REQUESTION.RequesterName.ToLower().Contains(argKeyword))
                        {
                            l_RES_PURCHASE_REQUESTION_lst.Add(l_RES_PURCHASE_REQUESTION);
                        }
                    }
                }
                else
                {
                    l_RES_PURCHASE_REQUESTION_lst = mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION;// OriginalRequestionClosedList.GetRange(0, OriginalRequestionClosedList.Count);
                }
                bindDataTab(l_RES_PURCHASE_REQUESTION_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getRequestion()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_REQUESTION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseRequestion);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_REQUESTION = JsonConvert.DeserializeObject<JSN_PURCHASE_REQUESTION>(mResponse);
                    if (mJSN_PURCHASE_REQUESTION.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION.Count > 0)
                        {
                            bindDataTab(this.mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_REQUESTION.Message.Message);
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

        public async void saveRequestion()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_REQUESTION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseRequestion);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_REQUESTION = JsonConvert.DeserializeObject<JSN_PURCHASE_REQUESTION>(mResponse);
                    if (mJSN_PURCHASE_REQUESTION.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION.Count > 0)
                        {
                            RES_PURCHASE_REQUESTION = this.mJSN_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_REQUESTION.Message.Message);
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

        public async void loadQuotation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadPurchaseRequestion);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_REQUESTION = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_REQUESTION>(mResponse);
                    if (mJSN_LOAD_PURCHASE_REQUESTION.Message.Code == "7")
                    {
                        this.RequestionLoad = mJSN_LOAD_PURCHASE_REQUESTION;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_REQUESTION.Message.Message);
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
