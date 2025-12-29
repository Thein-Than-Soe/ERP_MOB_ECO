using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.SYS
{
    public class VmlAccess : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_ACCESS mJSN_REQ_ACCESS = new JSN_REQ_ACCESS();
        public JSN_ACCESS mJSN_ACCESS = new JSN_ACCESS();
      
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAccess()
        {
            this.switchDisplayView(DisplayView.Card);
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
        public RES_ACCESS mRES_ACCESS = new RES_ACCESS();
        public RES_ACCESS RES_ACCESS
        {
            get { return mRES_ACCESS; }
            set { mRES_ACCESS = value; NotifyPropertyChanged("RES_ACCESS"); }
        }

        public List<RES_ACCESS> mAccessList;
        public List<RES_ACCESS> AccessList
        {
            get { return mAccessList; }
            set { mAccessList = value; NotifyPropertyChanged("AccessList"); }
        }

        public List<RES_ACCESS> mAccessActiveList;
        public List<RES_ACCESS> AccessActiveList
        {
            get { return mAccessActiveList; }
            set { mAccessActiveList = value; NotifyPropertyChanged("AccessActiveList"); }
        }

        public List<RES_ACCESS> mAccessInActiveList;
        public List<RES_ACCESS> AccessInActiveList
        {
            get { return mAccessInActiveList; }
            set { mAccessInActiveList = value; NotifyPropertyChanged("AccessInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getAccess());
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
        private void bindDataTab(List<RES_ACCESS> argRES_ACCESS_LST)
        {
            try
            {
                List<RES_ACCESS> l_RES_ACCESS_Active = new List<RES_ACCESS>();
                List<RES_ACCESS> l_RES_ACCESS_InActive = new List<RES_ACCESS>();
                if (argRES_ACCESS_LST != null && argRES_ACCESS_LST.Count>0)
                {
                    
                    foreach (RES_ACCESS l_RES_ACCESS in argRES_ACCESS_LST)
                    {
                        if (l_RES_ACCESS.StatusAsk.Equals("1"))
                        {
                            l_RES_ACCESS_Active.Add(l_RES_ACCESS);
                        }
                        else
                        {
                            l_RES_ACCESS_InActive.Add(l_RES_ACCESS);
                        }
                    }

                    RES_ACCESS = argRES_ACCESS_LST[0];
                    AccessList = argRES_ACCESS_LST;
                    AccessActiveList = l_RES_ACCESS_Active;
                    AccessInActiveList = l_RES_ACCESS_InActive;
                }
                else
                {
                    AccessList = new List<RES_ACCESS>();
                    AccessActiveList = new List<RES_ACCESS>();
                    AccessInActiveList = new List<RES_ACCESS>();
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
                List<RES_ACCESS> l_RES_ACCESS_lst = new List<RES_ACCESS>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_ACCESS l_RES_ACCESS in mJSN_ACCESS.RES_ACCESS_LST)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_ACCESS.AccessCode_0_50.ToLower().Contains(argKeyword)
                            || l_RES_ACCESS.AccessName_0_255.ToLower().Contains(argKeyword)
                            || l_RES_ACCESS.AccessDescription_0_500.ToLower().Contains(argKeyword))
                        {
                            l_RES_ACCESS_lst.Add(l_RES_ACCESS);
                        }
                    }
                }
                else
                {
                    l_RES_ACCESS_lst = mJSN_ACCESS.RES_ACCESS_LST;// OriginalAccessList.GetRange(0, OriginalAccessList.Count);
                }
                bindDataTab(l_RES_ACCESS_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAccess()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCESS);
                mResponse = await Sys_Service.ApiCall(mRequest, Sys_Name.wsgetAccess);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_ACCESS = JsonConvert.DeserializeObject<JSN_ACCESS>(mResponse);
                    if (mJSN_ACCESS.Message.Code == "7")
                    {
                        if( this.mJSN_ACCESS.RES_ACCESS_LST.Count>0)
                        {
                            bindDataTab(this.mJSN_ACCESS.RES_ACCESS_LST);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_ACCESS.Message.Message);
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

        public async void saveAccess()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ACCESS);
                mResponse = await Sys_Service.ApiCall(mRequest, Sys_Name.wsSaveAccess);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_ACCESS = JsonConvert.DeserializeObject<JSN_ACCESS>(mResponse);
                    if (mJSN_ACCESS.Message.Code == "7")
                    {
                        if (this.mJSN_ACCESS.RES_ACCESS_LST.Count > 0)
                        {
                            RES_ACCESS = this.mJSN_ACCESS.RES_ACCESS_LST[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_ACCESS.Message.Message);
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
