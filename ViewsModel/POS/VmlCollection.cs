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
    public class VmlCollection : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_COLLECTION mJSN_REQ_COLLECTION = new JSN_REQ_COLLECTION();
        public JSN_RES_COLLECTION mJSN_RES_COLLECTION = new JSN_RES_COLLECTION();
        public JSN_LOAD_COLLECTION mJSN_LOAD_COLLECTION = new JSN_LOAD_COLLECTION();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlCollection()
        {
            this.switchDisplayView(DisplayView.Card);
            CollectionLoad = new JSN_LOAD_COLLECTION();
            CollectionClosedList = new List<DAT_COLLECTION_DETAIL>();
            CollectionOpenList = new List<DAT_COLLECTION_DETAIL>();
            CollectionReadyList = new List<DAT_COLLECTION_DETAIL>();
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
        public JSN_LOAD_COLLECTION JSN_LOAD_COLLECTION = new JSN_LOAD_COLLECTION();
        public JSN_LOAD_COLLECTION CollectionLoad
        {
            get { return JSN_LOAD_COLLECTION; }
            set { JSN_LOAD_COLLECTION = value; NotifyPropertyChanged("CollectionLoad"); }
        }


        public DAT_COLLECTION_DETAIL mDAT_COLLECTION_DETAIL = new DAT_COLLECTION_DETAIL();
        public DAT_COLLECTION_DETAIL DAT_COLLECTION_DETAIL
        {
            get { return mDAT_COLLECTION_DETAIL; }
            set { mDAT_COLLECTION_DETAIL = value; NotifyPropertyChanged("DAT_COLLECTION_DETAIL"); }
        }

        public List<DAT_COLLECTION_DETAIL> mCollectionClosedList;
        public List<DAT_COLLECTION_DETAIL> CollectionClosedList
        {
            get { return mCollectionClosedList; }
            set { mCollectionClosedList = value; NotifyPropertyChanged("CollectionClosedList"); }
        }

        public List<DAT_COLLECTION_DETAIL> mCollectionOpenList;
        public List<DAT_COLLECTION_DETAIL> CollectionOpenList
        {
            get { return mCollectionOpenList; }
            set { mCollectionOpenList = value; NotifyPropertyChanged("CollectionOpenList"); }
        }

        public List<DAT_COLLECTION_DETAIL> mCollectionReadyList;
        public List<DAT_COLLECTION_DETAIL> CollectionReadyList
        {
            get { return mCollectionReadyList; }
            set { mCollectionReadyList = value; NotifyPropertyChanged("CollectionReadyList"); }
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
                    mRefreshCommand = new Command(() => this.getCollection());
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
        private void bindDataTab(List<DAT_COLLECTION_DETAIL> argDAT_COLLECTION_DETAIL_LST)
        {
            try
            {
                List<DAT_COLLECTION_DETAIL> l_DAT_COLLECTION_DETAIL_OPEN = new List<DAT_COLLECTION_DETAIL>();
                List<DAT_COLLECTION_DETAIL> l_DAT_COLLECTION_DETAIL_READY = new List<DAT_COLLECTION_DETAIL>();
                List<DAT_COLLECTION_DETAIL> l_DAT_COLLECTION_DETAIL_CLOSED = new List<DAT_COLLECTION_DETAIL>();
                if (argDAT_COLLECTION_DETAIL_LST != null && argDAT_COLLECTION_DETAIL_LST.Count > 0)
                {
                    foreach (DAT_COLLECTION_DETAIL l_DAT_COLLECTION_DETAIL in argDAT_COLLECTION_DETAIL_LST)
                    {                       
                        l_DAT_COLLECTION_DETAIL.CollectionDate = Utility.getDateTimeString(l_DAT_COLLECTION_DETAIL.CollectionDate);

                        if (l_DAT_COLLECTION_DETAIL.CollectionStatusAsk.Equals("1"))
                        {
                            l_DAT_COLLECTION_DETAIL_OPEN.Add(l_DAT_COLLECTION_DETAIL);
                        }
                        else if (l_DAT_COLLECTION_DETAIL.CollectionStatusAsk.Equals("2"))
                        {
                            l_DAT_COLLECTION_DETAIL_READY.Add(l_DAT_COLLECTION_DETAIL);
                        }
                        else if (l_DAT_COLLECTION_DETAIL.CollectionStatusAsk.Equals("3"))
                        {
                            l_DAT_COLLECTION_DETAIL_CLOSED.Add(l_DAT_COLLECTION_DETAIL);
                        }
                    }

                    DAT_COLLECTION_DETAIL = argDAT_COLLECTION_DETAIL_LST[0];
                    CollectionClosedList = l_DAT_COLLECTION_DETAIL_CLOSED;
                    CollectionOpenList = l_DAT_COLLECTION_DETAIL_OPEN;
                    CollectionReadyList = l_DAT_COLLECTION_DETAIL_READY;
                }
                else
                {
                    CollectionClosedList = new List<DAT_COLLECTION_DETAIL>();
                    CollectionOpenList = new List<DAT_COLLECTION_DETAIL>();
                    CollectionReadyList = new List<DAT_COLLECTION_DETAIL>();
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
                List<DAT_COLLECTION_DETAIL> l_DAT_COLLECTION_DETAIL_lst = new List<DAT_COLLECTION_DETAIL>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_COLLECTION_DETAIL l_DAT_COLLECTION_DETAIL in mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_COLLECTION_DETAIL.CollectionDate.ToLower().Contains(argKeyword)
                            || l_DAT_COLLECTION_DETAIL.CollectionCode.ToLower().Contains(argKeyword)
                            || l_DAT_COLLECTION_DETAIL.CustomerName.ToLower().Contains(argKeyword)
                            
                            || l_DAT_COLLECTION_DETAIL.OutstandingAmount.ToLower().Contains(argKeyword))
                        {
                            l_DAT_COLLECTION_DETAIL_lst.Add(l_DAT_COLLECTION_DETAIL);
                        }
                    }
                }
                else
                {
                    l_DAT_COLLECTION_DETAIL_lst = mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL;// OriginalCollectionClosedList.GetRange(0, OriginalCollectionClosedList.Count);
                }
                bindDataTab(l_DAT_COLLECTION_DETAIL_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getCollection()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_COLLECTION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetCollection);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_COLLECTION = JsonConvert.DeserializeObject<JSN_RES_COLLECTION>(mResponse);
                    if (mJSN_RES_COLLECTION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_COLLECTION.Message.Message);
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

        public async void saveCollection()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_COLLECTION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveCollection);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_COLLECTION = JsonConvert.DeserializeObject<JSN_RES_COLLECTION>(mResponse);
                    if (mJSN_RES_COLLECTION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL.Count > 0)
                        {
                            DAT_COLLECTION_DETAIL = this.mJSN_RES_COLLECTION.DAT_COLLECTION_DETAIL[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_COLLECTION.Message.Message);
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

        public async void loadCollection()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadCollection);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_COLLECTION = JsonConvert.DeserializeObject<JSN_LOAD_COLLECTION>(mResponse);
                    if (mJSN_LOAD_COLLECTION.Message.Code == "7")
                    {
                        this.CollectionLoad = mJSN_LOAD_COLLECTION;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_COLLECTION.Message.Message);
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
