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
    public class VmlUserGuide : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_MANUAL_CONTENT mJSN_REQ_MANUAL_CONTENT = new JSN_REQ_MANUAL_CONTENT();
        public JSN_RES_MANUAL_CONTENT mJSN_RES_MANUAL_CONTENT = new JSN_RES_MANUAL_CONTENT();
        public JSN_LOAD_MANUAL_CONTENT mJSN_LOAD_MANUAL_CONTENT = new JSN_LOAD_MANUAL_CONTENT();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlUserGuide()
        {
            this.switchDisplayView(DisplayView.Card);
            UserGuideLoad = new JSN_LOAD_MANUAL_CONTENT();
            UserguideList = new List<DAT_MANUAL_CONTENT>();
            MobileList = new List<DAT_MANUAL_CONTENT>();
            WebList = new List<DAT_MANUAL_CONTENT>();          
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
        public JSN_LOAD_MANUAL_CONTENT JSN_LOAD_MANUAL_CONTENT = new JSN_LOAD_MANUAL_CONTENT();
        public JSN_LOAD_MANUAL_CONTENT UserGuideLoad
        {
            get { return JSN_LOAD_MANUAL_CONTENT; }
            set { JSN_LOAD_MANUAL_CONTENT = value; NotifyPropertyChanged("UserGuideLoad"); }
        }


        public DAT_MANUAL_CONTENT mDAT_MANUAL_CONTENT = new DAT_MANUAL_CONTENT();
        public DAT_MANUAL_CONTENT DAT_MANUAL_CONTENT
        {
            get { return mDAT_MANUAL_CONTENT; }
            set { mDAT_MANUAL_CONTENT = value; NotifyPropertyChanged("DAT_MANUAL_CONTENT"); }
        }

        public List<DAT_MANUAL_CONTENT> mUserguideList;
        public List<DAT_MANUAL_CONTENT> UserguideList
        {
            get { return mUserguideList; }
            set { mUserguideList = value; NotifyPropertyChanged("UserguideList"); }
        }

        public List<DAT_MANUAL_CONTENT> mMobileList;
        public List<DAT_MANUAL_CONTENT> MobileList
        {
            get { return mMobileList; }
            set { mMobileList = value; NotifyPropertyChanged("MobileList"); }
        }

        public List<DAT_MANUAL_CONTENT> mWebList;
        public List<DAT_MANUAL_CONTENT> WebList
        {
            get { return mWebList; }
            set { mWebList = value; NotifyPropertyChanged("WebList"); }
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
                    mRefreshCommand = new Command(() => this.getUserguide());
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
        private void bindDataTab(List<DAT_MANUAL_CONTENT> argDAT_MANUAL_CONTENT_LST)
        {
            try
            {
                List<DAT_MANUAL_CONTENT> l_DAT_MANUAL_CONTENT_MOBILE = new List<DAT_MANUAL_CONTENT>();
                List<DAT_MANUAL_CONTENT> l_DAT_MANUAL_CONTENT_WEB = new List<DAT_MANUAL_CONTENT>();
               
                if (argDAT_MANUAL_CONTENT_LST != null && argDAT_MANUAL_CONTENT_LST.Count > 0)
                {

                    foreach (DAT_MANUAL_CONTENT l_DAT_MANUAL_CONTENT in argDAT_MANUAL_CONTENT_LST)
                    {
                        if (l_DAT_MANUAL_CONTENT.TypeAsk.Equals("1"))
                        {
                            l_DAT_MANUAL_CONTENT_MOBILE.Add(l_DAT_MANUAL_CONTENT);
                        }
                        else if (l_DAT_MANUAL_CONTENT.TypeAsk.Equals("3"))
                        {
                            l_DAT_MANUAL_CONTENT_WEB.Add(l_DAT_MANUAL_CONTENT);
                        }
                        
                    }

                    DAT_MANUAL_CONTENT = argDAT_MANUAL_CONTENT_LST[0];
                    UserguideList = argDAT_MANUAL_CONTENT_LST;
                    MobileList = l_DAT_MANUAL_CONTENT_MOBILE;
                    WebList = l_DAT_MANUAL_CONTENT_WEB;
                   
                }
                else
                {
                    UserguideList = new List<DAT_MANUAL_CONTENT>();
                    MobileList = new List<DAT_MANUAL_CONTENT>();
                    WebList = new List<DAT_MANUAL_CONTENT>();
                   
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
                List<DAT_MANUAL_CONTENT> l_DAT_MANUAL_CONTENT_lst = new List<DAT_MANUAL_CONTENT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_MANUAL_CONTENT l_DAT_MANUAL_CONTENT in mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_MANUAL_CONTENT.ManualContentName.ToLower().Contains(argKeyword)
                            || l_DAT_MANUAL_CONTENT.ProjectName.ToLower().Contains(argKeyword)
                            || l_DAT_MANUAL_CONTENT.ProductName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_MANUAL_CONTENT_lst.Add(l_DAT_MANUAL_CONTENT);
                        }
                    }
                }
                else
                {
                    l_DAT_MANUAL_CONTENT_lst = mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT;// OriginalUserguideList.GetRange(0, OriginalUserguideList.Count);
                }
                bindDataTab(l_DAT_MANUAL_CONTENT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getUserguide()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_MANUAL_CONTENT);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetManualContent);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_MANUAL_CONTENT = JsonConvert.DeserializeObject<JSN_RES_MANUAL_CONTENT>(mResponse);
                    if (mJSN_RES_MANUAL_CONTENT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_MANUAL_CONTENT.Message.Message);
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

        public async void saveUserguide()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_MANUAL_CONTENT);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wssaveManualContent);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_MANUAL_CONTENT = JsonConvert.DeserializeObject<JSN_RES_MANUAL_CONTENT>(mResponse);
                    if (mJSN_RES_MANUAL_CONTENT.Message.Code == "7")
                    {
                        if (this.mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT.Count > 0)
                        {
                            DAT_MANUAL_CONTENT = this.mJSN_RES_MANUAL_CONTENT.DAT_MANUAL_CONTENT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_MANUAL_CONTENT.Message.Message);
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

        public async void LoadTicketLog()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsloadManualContent);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_MANUAL_CONTENT = JsonConvert.DeserializeObject<JSN_LOAD_MANUAL_CONTENT>(mResponse);
                    if (mJSN_LOAD_MANUAL_CONTENT.Message.Code == "7")
                    {
                        this.UserGuideLoad = mJSN_LOAD_MANUAL_CONTENT;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_MANUAL_CONTENT.Message.Message);
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
