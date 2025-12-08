using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.SYS
{
    public class VmlCountry:BaseViewModel
{
        #region "Declaring"
       public JSN_REQ_COUNTRY mJSN_REQ_COUNTRY = new JSN_REQ_COUNTRY();
       public JSN_COUNTRY mJSN_COUNTRY = new JSN_COUNTRY();

        string mRequest = "";
        string mResponse = "";
        #endregion
     

        #region "Contructor"
        public VmlCountry()
        {
            this.switchDisplayView(DisplayView.Card);
            RES_COUNTRY = new RES_COUNTRY();
            CountryList = new List<RES_COUNTRY>();
            CountryActiveList = new List<RES_COUNTRY>();
            CountryInActiveList = new List<RES_COUNTRY>();
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
        public RES_COUNTRY mRES_COUNTRY = new RES_COUNTRY();
        public RES_COUNTRY RES_COUNTRY
        {
            get { return mRES_COUNTRY; }
            set { mRES_COUNTRY = value; NotifyPropertyChanged("RES_COUNTRY"); }
        }

        public List<RES_COUNTRY> mCountryList;
        public List<RES_COUNTRY> CountryList
        {
            get { return mCountryList; }
            set { mCountryList = value; NotifyPropertyChanged("CountryList"); }
        }

        public List<RES_COUNTRY> mCountryActiveList;
        public List<RES_COUNTRY> CountryActiveList
        {
            get { return mCountryActiveList; }
            set { mCountryActiveList = value; NotifyPropertyChanged("CountryActiveList"); }
        }

        public List<RES_COUNTRY> mCountryInActiveList;
        public List<RES_COUNTRY> CountryInActiveList
        {
            get { return mCountryInActiveList; }
            set { mCountryInActiveList = value; NotifyPropertyChanged("CountryInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getCountry());
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
        private void bindDataTab(List<RES_COUNTRY> argRES_COUNTRY_LST)
        {
            try
            {
                List<RES_COUNTRY> l_RES_COUNTRY_Active = new List<RES_COUNTRY>();
                List<RES_COUNTRY> l_RES_COUNTRY_InActive = new List<RES_COUNTRY>();
                if (argRES_COUNTRY_LST != null && argRES_COUNTRY_LST.Count > 0)
                {

                    foreach (RES_COUNTRY l_RES_COUNTRY in argRES_COUNTRY_LST)
                    {
                        if (l_RES_COUNTRY.StatusAsk.Equals("1"))
                        {
                            l_RES_COUNTRY_Active.Add(l_RES_COUNTRY);
                        }
                        else
                        {
                            l_RES_COUNTRY_InActive.Add(l_RES_COUNTRY);
                        }
                    }

                    RES_COUNTRY = argRES_COUNTRY_LST[0];
                    CountryList = argRES_COUNTRY_LST;
                    CountryActiveList = l_RES_COUNTRY_Active;
                    CountryInActiveList = l_RES_COUNTRY_InActive;
                }
                else
                {
                    CountryList = new List<RES_COUNTRY>();
                    CountryActiveList = new List<RES_COUNTRY>();
                    CountryInActiveList = new List<RES_COUNTRY>();
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
                List<RES_COUNTRY> l_RES_COUNTRY_lst = new List<RES_COUNTRY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_COUNTRY l_RES_COUNTRY in mJSN_COUNTRY.RES_COUNTRY_LST)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_COUNTRY.CountryCode_0_50.ToLower().Contains(argKeyword)
                            || l_RES_COUNTRY.CountryName_0_255.ToLower().Contains(argKeyword)
                            || l_RES_COUNTRY.CountryDescription_0_500.ToLower().Contains(argKeyword))
                        {
                            l_RES_COUNTRY_lst.Add(l_RES_COUNTRY);
                        }
                    }
                }
                else
                {
                    l_RES_COUNTRY_lst = mJSN_COUNTRY.RES_COUNTRY_LST;// OriginalCountryList.GetRange(0, OriginalCountryList.Count);
                }
                bindDataTab(l_RES_COUNTRY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getCountry()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_COUNTRY);
                mResponse = await Sys_Service.ApiCall(mRequest, Sys_Name.wsgetCountry);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_COUNTRY = JsonConvert.DeserializeObject<JSN_COUNTRY>(mResponse);
                    if (mJSN_COUNTRY.Message.Code == "7")
                    {
                        if (this.mJSN_COUNTRY.RES_COUNTRY_LST.Count > 0)
                        {
                            bindDataTab(this.mJSN_COUNTRY.RES_COUNTRY_LST);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_COUNTRY.Message.Message);
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

        public async void saveCountry()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_COUNTRY);
                mResponse = await Sys_Service.ApiCall(mRequest, Sys_Name.wsSaveCountry);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_COUNTRY = JsonConvert.DeserializeObject<JSN_COUNTRY>(mResponse);
                    if (mJSN_COUNTRY.Message.Code == "7")
                    {
                        if (this.mJSN_COUNTRY.RES_COUNTRY_LST.Count > 0)
                        {
                            RES_COUNTRY = this.mJSN_COUNTRY.RES_COUNTRY_LST[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_COUNTRY.Message.Message);
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
