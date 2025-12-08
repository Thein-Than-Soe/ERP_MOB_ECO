using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.ATT.REQ;
using CS.ERP.PL.ATT.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.ATT;
using Microsoft.Maui.Controls;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.ATT
{
    public class VmlOnduty : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_ON_DUTY mJSN_REQ_ON_DUTY = new JSN_REQ_ON_DUTY();
        public JSN_RES_ON_DUTY mJSN_RES_ON_DUTY = new JSN_RES_ON_DUTY();
        JSN_RES_LOAD_ON_DUTY mJSN_RES_LOAD_ON_DUTY = new JSN_RES_LOAD_ON_DUTY();
        DateTime oDate;

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlOnduty()
        {
            this.switchDisplayView(DisplayView.Card);
            OndutyLoad = new JSN_RES_LOAD_ON_DUTY();
            OnServiceList = new List<DAT_ON_DUTY>();
            OnEventList = new List<DAT_ON_DUTY>();
            OnSiteList = new List<DAT_ON_DUTY>();
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
        public JSN_RES_LOAD_ON_DUTY JSN_RES_LOAD_ON_DUTY = new JSN_RES_LOAD_ON_DUTY();
        public JSN_RES_LOAD_ON_DUTY OndutyLoad
        {
            get { return JSN_RES_LOAD_ON_DUTY; }
            set { JSN_RES_LOAD_ON_DUTY = value; NotifyPropertyChanged("OndutyLoad"); }
        }

        public DAT_ON_DUTY mDAT_ON_DUTY = new DAT_ON_DUTY();
        public DAT_ON_DUTY DAT_ON_DUTY
        {
            get { return mDAT_ON_DUTY; }
            set { mDAT_ON_DUTY = value; NotifyPropertyChanged("DAT_ON_DUTY"); }
        }

        public List<DAT_ON_DUTY> mOnDutyList;
        public List<DAT_ON_DUTY> OnDutyList
        {
            get { return mOnDutyList; }
            set { mOnDutyList = value; NotifyPropertyChanged("OnDutyList"); }
        }

        public List<DAT_ON_DUTY> mOnServiceList;
        public List<DAT_ON_DUTY> OnServiceList
        {
            get { return mOnServiceList; }
            set { mOnServiceList = value; NotifyPropertyChanged("OnServiceList"); }
        }

        public List<DAT_ON_DUTY> mOnSiteList;
        public List<DAT_ON_DUTY> OnSiteList
        {
            get { return mOnSiteList; }
            set { mOnSiteList = value; NotifyPropertyChanged("OnSiteList"); }
        }

        public List<DAT_ON_DUTY> mOnEventList;
        public List<DAT_ON_DUTY> OnEventList
        {
            get { return mOnEventList; }
            set { mOnEventList = value; NotifyPropertyChanged("OnEventList"); }
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
                    mRefreshCommand = new Command(() => this.getOnDuty());
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
        private void bindDataTab(List<DAT_ON_DUTY> argDAT_ON_DUTY_LST)
        {
            try
            {
                List<DAT_ON_DUTY> l_DAT_ON_DUTY_ONSERVICE = new List<DAT_ON_DUTY>();
                List<DAT_ON_DUTY> l_DAT_ON_DUTY_ONSITE = new List<DAT_ON_DUTY>();
                List<DAT_ON_DUTY> l_DAT_ON_DUTY_ONEVENT = new List<DAT_ON_DUTY>();

                if (argDAT_ON_DUTY_LST != null && argDAT_ON_DUTY_LST.Count > 0)
                {

                    foreach (DAT_ON_DUTY l_DAT_ON_DUTY in argDAT_ON_DUTY_LST)
                    {
                        l_DAT_ON_DUTY.SD = Utility.getDateTimeString(l_DAT_ON_DUTY.SD);
                        l_DAT_ON_DUTY.ED = Utility.getDateTimeString(l_DAT_ON_DUTY.ED);

                        if (l_DAT_ON_DUTY.Status.Equals("1"))
                        {                          
                            l_DAT_ON_DUTY_ONSERVICE.Add(l_DAT_ON_DUTY);
                        }
                        else if (l_DAT_ON_DUTY.Status.Equals("2"))
                        {                           
                            l_DAT_ON_DUTY_ONSITE.Add(l_DAT_ON_DUTY);
                        }
                        else if (l_DAT_ON_DUTY.Status.Equals("3"))
                        { 
                            l_DAT_ON_DUTY_ONEVENT.Add(l_DAT_ON_DUTY);
                        }
                    }

                    DAT_ON_DUTY = argDAT_ON_DUTY_LST[0];
                    OnDutyList = argDAT_ON_DUTY_LST;
                    OnServiceList = l_DAT_ON_DUTY_ONSERVICE;
                    OnSiteList = l_DAT_ON_DUTY_ONSITE;
                    OnEventList = l_DAT_ON_DUTY_ONEVENT;
                }
                else
                {
                    OnDutyList = new List<DAT_ON_DUTY>();
                    OnServiceList = new List<DAT_ON_DUTY>();
                    OnSiteList = new List<DAT_ON_DUTY>();
                    OnEventList = new List<DAT_ON_DUTY>();
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
                List<DAT_ON_DUTY> l_DAT_ON_DUTY_lst = new List<DAT_ON_DUTY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_ON_DUTY l_DAT_ON_DUTY in mJSN_RES_ON_DUTY.DAT_ON_DUTY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_ON_DUTY.InOutName.ToLower().Contains(argKeyword)
                            || l_DAT_ON_DUTY.EmployeeName.ToLower().Contains(argKeyword)
                            
                            || l_DAT_ON_DUTY.EmployeeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_ON_DUTY_lst.Add(l_DAT_ON_DUTY);
                        }
                    }
                }
                else
                {
                    l_DAT_ON_DUTY_lst = mJSN_RES_ON_DUTY.DAT_ON_DUTY;// OriginalOnDutyList.GetRange(0, OriginalOnDutyList.Count);
                }
                bindDataTab(l_DAT_ON_DUTY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getOnDuty()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ON_DUTY);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsgetOnDuty);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ON_DUTY = JsonConvert.DeserializeObject<JSN_RES_ON_DUTY>(mResponse);
                    if (mJSN_RES_ON_DUTY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ON_DUTY.DAT_ON_DUTY.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_ON_DUTY.DAT_ON_DUTY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ON_DUTY.Message.Message);
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

        public async void saveOnDuty()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_ON_DUTY);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wssaveOnDuty);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_ON_DUTY = JsonConvert.DeserializeObject<JSN_RES_ON_DUTY>(mResponse);
                    if (mJSN_RES_ON_DUTY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_ON_DUTY.DAT_ON_DUTY.Count > 0)
                        {
                            DAT_ON_DUTY = this.mJSN_RES_ON_DUTY.DAT_ON_DUTY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ON_DUTY.Message.Message);
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

        public async void LoadOnduty()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Att_Service.ApiCall(mRequest, Att_Name.wsLoadOnDuty);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_LOAD_ON_DUTY = JsonConvert.DeserializeObject<JSN_RES_LOAD_ON_DUTY>(mResponse);
                    if (mJSN_RES_LOAD_ON_DUTY.Message.Code == "7")
                    {
                        this.OndutyLoad= mJSN_RES_LOAD_ON_DUTY;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_ON_DUTY.Message.Message);
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
