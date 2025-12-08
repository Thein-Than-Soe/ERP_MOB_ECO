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
    public class VmlPurchaseQuotation : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PURCHASE_QUOTATION_JUN mJSN_REQ_PURCHASE_QUOTATION_JUN = new JSN_REQ_PURCHASE_QUOTATION_JUN();
        public JSN_PURCHASE_QUOTATION_JUN mJSN_PURCHASE_QUOTATION_JUN = new JSN_PURCHASE_QUOTATION_JUN();
        public JSN_LOAD_PURCHASE_QUOTATION mJSN_LOAD_PURCHASE_QUOTATION = new JSN_LOAD_PURCHASE_QUOTATION();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlPurchaseQuotation()
        {
            this.switchDisplayView(DisplayView.Card);
            QuotationLoad = new JSN_LOAD_PURCHASE_QUOTATION();
            QuotationClosedList = new List<RES_PURCHASE_QUOTATION>();
            QuotationActiveList = new List<RES_PURCHASE_QUOTATION>();
            QuotationPartialList = new List<RES_PURCHASE_QUOTATION>();
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
        public JSN_LOAD_PURCHASE_QUOTATION JSN_LOAD_PURCHASE_QUOTATION = new JSN_LOAD_PURCHASE_QUOTATION();
        public JSN_LOAD_PURCHASE_QUOTATION QuotationLoad
        {
            get { return JSN_LOAD_PURCHASE_QUOTATION; }
            set { JSN_LOAD_PURCHASE_QUOTATION = value; NotifyPropertyChanged("QuotationLoad"); }
        }

        public RES_PURCHASE_QUOTATION mRES_PURCHASE_QUOTATION = new RES_PURCHASE_QUOTATION();
        public RES_PURCHASE_QUOTATION_DETAIL mRES_PURCHASE_QUOTATION_DETAIL = new RES_PURCHASE_QUOTATION_DETAIL();
        public RES_PURCHASE_BROWSE mRES_PURCHASE_BROWSE = new RES_PURCHASE_BROWSE();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_PURCHASE_QUOTATION RES_PURCHASE_QUOTATION
        {
            get { return mRES_PURCHASE_QUOTATION; }
            set { mRES_PURCHASE_QUOTATION = value; NotifyPropertyChanged("RES_PURCHASE_QUOTATION"); }
        }

        public RES_PURCHASE_BROWSE RES_PURCHASE_BROWSE
        {
            get { return mRES_PURCHASE_BROWSE; }
            set { mRES_PURCHASE_BROWSE = value; NotifyPropertyChanged("RES_PURCHASE_BROWSE"); }
        }


        public RES_PURCHASE_QUOTATION_DETAIL RES_PURCHASE_QUOTATION_DETAIL
        {
            get { return mRES_PURCHASE_QUOTATION_DETAIL; }
            set { mRES_PURCHASE_QUOTATION_DETAIL = value; NotifyPropertyChanged("RES_PURCHASE_QUOTATION_DETAIL"); }
        }

        public List<RES_PURCHASE_QUOTATION> mQuotationClosedList;
        public List<RES_PURCHASE_QUOTATION> QuotationClosedList
        {
            get { return mQuotationClosedList; }
            set { mQuotationClosedList = value; NotifyPropertyChanged("QuotationClosedList"); }
        }

        public List<RES_PURCHASE_QUOTATION> mQuotationActiveList;
        public List<RES_PURCHASE_QUOTATION> QuotationActiveList
        {
            get { return mQuotationActiveList; }
            set { mQuotationActiveList = value; NotifyPropertyChanged("QuotationActiveList"); }
        }

        public List<RES_PURCHASE_QUOTATION> mQuotationPartialList;
        public List<RES_PURCHASE_QUOTATION> QuotationPartialList
        {
            get { return mQuotationPartialList; }
            set { mQuotationPartialList = value; NotifyPropertyChanged("QuotationPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getQuotation());
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
        private void bindDataTab(List<RES_PURCHASE_QUOTATION> argRES_PURCHASE_QUOTATION_LST)
        {
            try
            {
                List<RES_PURCHASE_QUOTATION> l_RES_PURCHASE_QUOTATION_ACTIVE = new List<RES_PURCHASE_QUOTATION>();
                List<RES_PURCHASE_QUOTATION> l_RES_PURCHASE_QUOTATION_InACTIVE = new List<RES_PURCHASE_QUOTATION>();
                if (argRES_PURCHASE_QUOTATION_LST != null && argRES_PURCHASE_QUOTATION_LST.Count > 0)
                {
                    foreach (RES_PURCHASE_QUOTATION l_RES_PURCHASE_QUOTATION in argRES_PURCHASE_QUOTATION_LST)
                    {
                        
                        l_RES_PURCHASE_QUOTATION.QuotationDate = Utility.getDateTimeString(l_RES_PURCHASE_QUOTATION.QuotationDate);

                        if (l_RES_PURCHASE_QUOTATION.StatusAsk.Equals("1"))
                        {
                            l_RES_PURCHASE_QUOTATION_ACTIVE.Add(l_RES_PURCHASE_QUOTATION);
                        }
                        else if (l_RES_PURCHASE_QUOTATION.StatusAsk.Equals("8"))
                        {
                            l_RES_PURCHASE_QUOTATION_InACTIVE.Add(l_RES_PURCHASE_QUOTATION);
                        }
                    }

                    RES_PURCHASE_QUOTATION = argRES_PURCHASE_QUOTATION_LST[0];
                    QuotationClosedList = argRES_PURCHASE_QUOTATION_LST;
                    QuotationActiveList = l_RES_PURCHASE_QUOTATION_ACTIVE;
                    QuotationPartialList = l_RES_PURCHASE_QUOTATION_InACTIVE;
                }
                else
                {
                    QuotationClosedList = new List<RES_PURCHASE_QUOTATION>();
                    QuotationActiveList = new List<RES_PURCHASE_QUOTATION>();
                    QuotationPartialList = new List<RES_PURCHASE_QUOTATION>();
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
                List<RES_PURCHASE_QUOTATION> l_RES_PURCHASE_QUOTATION_lst = new List<RES_PURCHASE_QUOTATION>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_PURCHASE_QUOTATION l_RES_PURCHASE_QUOTATION in mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_PURCHASE_QUOTATION.SupplierName.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_QUOTATION.QuotationCode.ToLower().Contains(argKeyword)
                            || l_RES_PURCHASE_QUOTATION.QuotationDate.ToLower().Contains(argKeyword))
                        {
                            l_RES_PURCHASE_QUOTATION_lst.Add(l_RES_PURCHASE_QUOTATION);
                        }
                    }
                }
                else
                {
                    l_RES_PURCHASE_QUOTATION_lst = mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION;// OriginalQuotationClosedList.GetRange(0, OriginalQuotationClosedList.Count);
                }
                bindDataTab(l_RES_PURCHASE_QUOTATION_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getQuotation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_QUOTATION_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetPurchaseQuotation);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_QUOTATION_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_QUOTATION_JUN>(mResponse);
                    if (mJSN_PURCHASE_QUOTATION_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION.Count > 0)
                        {
                            bindDataTab(this.mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_QUOTATION_JUN.Message.Message);
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

        public async void saveQuotation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PURCHASE_QUOTATION_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssavePurchaseQuotation);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_PURCHASE_QUOTATION_JUN = JsonConvert.DeserializeObject<JSN_PURCHASE_QUOTATION_JUN>(mResponse);
                    if (mJSN_PURCHASE_QUOTATION_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION.Count > 0)
                        {
                            RES_PURCHASE_QUOTATION = this.mJSN_PURCHASE_QUOTATION_JUN.RES_PURCHASE_QUOTATION[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_PURCHASE_QUOTATION_JUN.Message.Message);
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
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadPurchaseQuotation);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PURCHASE_QUOTATION = JsonConvert.DeserializeObject<JSN_LOAD_PURCHASE_QUOTATION>(mResponse);
                    if (mJSN_LOAD_PURCHASE_QUOTATION.Message.Code == "7")
                    {
                        this.QuotationLoad = mJSN_LOAD_PURCHASE_QUOTATION;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PURCHASE_QUOTATION.Message.Message);
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
