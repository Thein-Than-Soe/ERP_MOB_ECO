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
    public class VmlSalesEnquiry : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SALE_ENQUIRY mJSN_REQ_SALE_ENQUIRY = new JSN_REQ_SALE_ENQUIRY();
        public JSN_SALE_ENQUIRY mJSN_SALE_ENQUIRY = new JSN_SALE_ENQUIRY();
        public JSN_LOAD_SALE_ENQUIRY mJSN_LOAD_SALE_ENQUIRY = new JSN_LOAD_SALE_ENQUIRY();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSalesEnquiry()
        {
            this.switchDisplayView(DisplayView.Card);
            EnquiryLoad = new JSN_LOAD_SALE_ENQUIRY();
            EnquiryClosedList = new List<RES_SALE_ENQUIRY>();
            EnquiryActiveList = new List<RES_SALE_ENQUIRY>();
            EnquiryPartialList = new List<RES_SALE_ENQUIRY>();
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
        public JSN_LOAD_SALE_ENQUIRY JSN_LOAD_SALE_ENQUIRY = new JSN_LOAD_SALE_ENQUIRY();
        public JSN_LOAD_SALE_ENQUIRY EnquiryLoad
        {
            get { return JSN_LOAD_SALE_ENQUIRY; }
            set { JSN_LOAD_SALE_ENQUIRY = value; NotifyPropertyChanged("EnquiryLoad"); }
        }

        public RES_SALE_ENQUIRY mRES_SALE_ENQUIRY = new RES_SALE_ENQUIRY();
        public RES_COMPANY rES_COMPANY = new RES_COMPANY();

        public RES_SALE_ENQUIRY RES_SALE_ENQUIRY
        {
            get { return mRES_SALE_ENQUIRY; }
            set { mRES_SALE_ENQUIRY = value; NotifyPropertyChanged("RES_SALE_ENQUIRY"); }
        }

        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_SALE_ENQUIRY"); }
        }


        public List<RES_SALE_ENQUIRY> mEnquiryClosedList;
        public List<RES_SALE_ENQUIRY> EnquiryClosedList
        {
            get { return mEnquiryClosedList; }
            set { mEnquiryClosedList = value; NotifyPropertyChanged("EnquiryClosedList"); }
        }

        public List<RES_SALE_ENQUIRY> mEnquiryActiveList;
        public List<RES_SALE_ENQUIRY> EnquiryActiveList
        {
            get { return mEnquiryActiveList; }
            set { mEnquiryActiveList = value; NotifyPropertyChanged("EnquiryActiveList"); }
        }

        public List<RES_SALE_ENQUIRY> mEnquiryPartialList;
        public List<RES_SALE_ENQUIRY> EnquiryPartialList
        {
            get { return mEnquiryPartialList; }
            set { mEnquiryPartialList = value; NotifyPropertyChanged("EnquiryPartialList"); }
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
                    mRefreshCommand = new Command(() => this.getEnquiry());
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
        private void bindDataTab(List<RES_SALE_ENQUIRY> argRES_SALE_ENQUIRY_LST)
        {
            try
            {
                List<RES_SALE_ENQUIRY> l_RES_SALE_ENQUIRY_ACTIVE = new List<RES_SALE_ENQUIRY>();
                List<RES_SALE_ENQUIRY> l_RES_SALE_ENQUIRY_PARTIAL = new List<RES_SALE_ENQUIRY>();
                List<RES_SALE_ENQUIRY> l_RES_SALE_ENQUIRY_CLOSED = new List<RES_SALE_ENQUIRY>();
                if (argRES_SALE_ENQUIRY_LST != null && argRES_SALE_ENQUIRY_LST.Count > 0)
                {
                    foreach (RES_SALE_ENQUIRY l_RES_SALE_ENQUIRY in argRES_SALE_ENQUIRY_LST)
                    {
                       l_RES_SALE_ENQUIRY.EnquiryDate = Utility.getDateTimeString(l_RES_SALE_ENQUIRY.EnquiryDate);

                        if (l_RES_SALE_ENQUIRY.StatusAsk.Equals("1"))
                        {
                            l_RES_SALE_ENQUIRY_ACTIVE.Add(l_RES_SALE_ENQUIRY);
                        }
                        else if (l_RES_SALE_ENQUIRY.StatusAsk.Equals("8"))
                        {
                            l_RES_SALE_ENQUIRY_PARTIAL.Add(l_RES_SALE_ENQUIRY);
                        }
                        else if (l_RES_SALE_ENQUIRY.StatusAsk.Equals("3"))
                        {
                            l_RES_SALE_ENQUIRY_CLOSED.Add(l_RES_SALE_ENQUIRY);
                        }
                    }

                    RES_SALE_ENQUIRY = argRES_SALE_ENQUIRY_LST[0];
                    EnquiryClosedList = l_RES_SALE_ENQUIRY_CLOSED;
                    EnquiryActiveList = l_RES_SALE_ENQUIRY_ACTIVE;
                    EnquiryPartialList = l_RES_SALE_ENQUIRY_PARTIAL;
                }
                else
                {
                    EnquiryClosedList = new List<RES_SALE_ENQUIRY>();
                    EnquiryActiveList = new List<RES_SALE_ENQUIRY>();
                    EnquiryPartialList = new List<RES_SALE_ENQUIRY>();
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
                List<RES_SALE_ENQUIRY> l_RES_SALE_ENQUIRY_lst = new List<RES_SALE_ENQUIRY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_ENQUIRY l_RES_SALE_ENQUIRY in mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_ENQUIRY.EnquiryCode.ToLower().Contains(argKeyword)
                            || l_RES_SALE_ENQUIRY.EnquiryDate.ToLower().Contains(argKeyword)
                            || l_RES_SALE_ENQUIRY.StatusName.ToLower().Contains(argKeyword)
                            || l_RES_SALE_ENQUIRY.CustomerName.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_ENQUIRY_lst.Add(l_RES_SALE_ENQUIRY);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_ENQUIRY_lst = mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY;// OriginalEnquiryClosedList.GetRange(0, OriginalEnquiryClosedList.Count);
                }
                bindDataTab(l_RES_SALE_ENQUIRY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEnquiry()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_ENQUIRY);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleEnquiry);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_ENQUIRY = JsonConvert.DeserializeObject<JSN_SALE_ENQUIRY>(mResponse);
                    if (mJSN_SALE_ENQUIRY.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY.Count > 0)
                        {
                            bindDataTab(this.mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_ENQUIRY.Message.Message);
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

        public async void saveEnquiry()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_ENQUIRY);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleEnquiry);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SALE_ENQUIRY = JsonConvert.DeserializeObject<JSN_SALE_ENQUIRY>(mResponse);
                    if (mJSN_SALE_ENQUIRY.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY.Count > 0)
                        {
                            RES_SALE_ENQUIRY = this.mJSN_SALE_ENQUIRY.RES_SALE_ENQUIRY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SALE_ENQUIRY.Message.Message);
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

        public async void loadEnquiry()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleEnquiry);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SALE_ENQUIRY = JsonConvert.DeserializeObject<JSN_LOAD_SALE_ENQUIRY>(mResponse);
                    if (mJSN_LOAD_SALE_ENQUIRY.Message.Code == "7")
                    {
                        this.EnquiryLoad = mJSN_LOAD_SALE_ENQUIRY;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SALE_ENQUIRY.Message.Message);
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
