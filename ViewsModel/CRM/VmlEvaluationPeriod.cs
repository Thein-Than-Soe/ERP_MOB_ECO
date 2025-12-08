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
    public class VmlEvaluationPeriod : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EVALUATION_PERIOD mJSN_REQ_EVALUATION_PERIOD = new JSN_REQ_EVALUATION_PERIOD();
        public JSN_RES_EVALUATION_PERIOD mJSN_RES_EVALUATION_PERIOD = new JSN_RES_EVALUATION_PERIOD();
        public JSN_LOAD_EVALUATION_PERIOD mJSN_LOAD_EVALUATION_PERIOD = new JSN_LOAD_EVALUATION_PERIOD();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEvaluationPeriod()
        {
            this.switchDisplayView(DisplayView.Card);
            EvaluationPeriodLoad = new JSN_LOAD_EVALUATION_PERIOD();
            ApplicantEvaluationOpenList = new List<DAT_EVALUATION_PERIOD>();
            ApplicantEvaluationCloseList = new List<DAT_EVALUATION_PERIOD>();
            ApplicantEvaluationList = new List<DAT_EVALUATION_PERIOD>();
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
        public JSN_LOAD_EVALUATION_PERIOD JSN_LOAD_EVALUATION_PERIOD = new JSN_LOAD_EVALUATION_PERIOD();
        public JSN_LOAD_EVALUATION_PERIOD EvaluationPeriodLoad
        {
            get { return JSN_LOAD_EVALUATION_PERIOD; }
            set { JSN_LOAD_EVALUATION_PERIOD = value; NotifyPropertyChanged("EvaluationPeriodLoad"); }
        }


        public DAT_EVALUATION_PERIOD mDAT_EVALUATION_PERIOD = new DAT_EVALUATION_PERIOD();
        public DAT_EVALUATION_PERIOD DAT_EVALUATION_PERIOD
        {
            get { return mDAT_EVALUATION_PERIOD; }
            set { mDAT_EVALUATION_PERIOD = value; NotifyPropertyChanged("DAT_EVALUATION_PERIOD"); }
        }

        public List<DAT_EVALUATION_PERIOD> mApplicantEvaluationList;
        public List<DAT_EVALUATION_PERIOD> ApplicantEvaluationList
        {
            get { return mApplicantEvaluationList; }
            set { mApplicantEvaluationList = value; NotifyPropertyChanged("ApplicantEvaluationList"); }
        }

        public List<DAT_EVALUATION_PERIOD> mApplicantEvaluationOpenList;
        public List<DAT_EVALUATION_PERIOD> ApplicantEvaluationOpenList
        {
            get { return mApplicantEvaluationOpenList; }
            set { mApplicantEvaluationOpenList = value; NotifyPropertyChanged("ApplicantEvaluationOpenList"); }
        }

        public List<DAT_EVALUATION_PERIOD> mApplicantEvaluationCloseList;
        public List<DAT_EVALUATION_PERIOD> ApplicantEvaluationCloseList
        {
            get { return mApplicantEvaluationCloseList; }
            set { mApplicantEvaluationCloseList = value; NotifyPropertyChanged("ApplicantEvaluationCloseList"); }
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
                    mRefreshCommand = new Command(() => this.getEvaluationPeriod());
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
        private void bindDataTab(List<DAT_EVALUATION_PERIOD> argDAT_EVALUATION_PERIOD_LST)
        {
            try
            {
                List<DAT_EVALUATION_PERIOD> l_DAT_EVALUATION_PERIOD_OPEN = new List<DAT_EVALUATION_PERIOD>();
                List<DAT_EVALUATION_PERIOD> l_DAT_EVALUATION_PERIOD_CLOSED = new List<DAT_EVALUATION_PERIOD>();


                if (argDAT_EVALUATION_PERIOD_LST != null && argDAT_EVALUATION_PERIOD_LST.Count > 0)
                {

                    foreach (DAT_EVALUATION_PERIOD l_DAT_EVALUATION_PERIOD in argDAT_EVALUATION_PERIOD_LST)
                    {

                        l_DAT_EVALUATION_PERIOD.SD = Utility.getDateTimeString(l_DAT_EVALUATION_PERIOD.SD);
                        l_DAT_EVALUATION_PERIOD.ED = Utility.getDateTimeString(l_DAT_EVALUATION_PERIOD.ED);

                        if (l_DAT_EVALUATION_PERIOD.StatusAsk.Equals("1"))
                        {
                            l_DAT_EVALUATION_PERIOD_OPEN.Add(l_DAT_EVALUATION_PERIOD);
                        }
                       
                        else if (l_DAT_EVALUATION_PERIOD.StatusAsk.Equals("9"))
                        {
                            l_DAT_EVALUATION_PERIOD_CLOSED.Add(l_DAT_EVALUATION_PERIOD);
                        }
                    }

                    DAT_EVALUATION_PERIOD = argDAT_EVALUATION_PERIOD_LST[0];
                    ApplicantEvaluationList = argDAT_EVALUATION_PERIOD_LST;
                    ApplicantEvaluationOpenList = l_DAT_EVALUATION_PERIOD_OPEN;                  
                    ApplicantEvaluationCloseList = l_DAT_EVALUATION_PERIOD_CLOSED;

                }
                else
                {
                    ApplicantEvaluationList = new List<DAT_EVALUATION_PERIOD>();
                    ApplicantEvaluationOpenList = new List<DAT_EVALUATION_PERIOD>();
                    ApplicantEvaluationCloseList = new List<DAT_EVALUATION_PERIOD>();
                  
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
                List<DAT_EVALUATION_PERIOD> l_DAT_EVALUATION_PERIOD_lst = new List<DAT_EVALUATION_PERIOD>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EVALUATION_PERIOD l_DAT_EVALUATION_PERIOD in mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EVALUATION_PERIOD.EvaluationPeriodName.ToLower().Contains(argKeyword)
                            || l_DAT_EVALUATION_PERIOD.EvaluationTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_EVALUATION_PERIOD.SD.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EVALUATION_PERIOD_lst.Add(l_DAT_EVALUATION_PERIOD);
                        }
                    }
                }
                else
                {
                    l_DAT_EVALUATION_PERIOD_lst = mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD;// OriginalApplicantEvaluationList.GetRange(0, OriginalApplicantEvaluationList.Count);
                }
                bindDataTab(l_DAT_EVALUATION_PERIOD_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEvaluationPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EVALUATION_PERIOD);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetEvaluationPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EVALUATION_PERIOD = JsonConvert.DeserializeObject<JSN_RES_EVALUATION_PERIOD>(mResponse);
                    if (mJSN_RES_EVALUATION_PERIOD.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EVALUATION_PERIOD.Message.Message);
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

        public async void saveEvaluationPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EVALUATION_PERIOD);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wssaveEvaluationPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EVALUATION_PERIOD = JsonConvert.DeserializeObject<JSN_RES_EVALUATION_PERIOD>(mResponse);
                    if (mJSN_RES_EVALUATION_PERIOD.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD.Count > 0)
                        {
                            DAT_EVALUATION_PERIOD = this.mJSN_RES_EVALUATION_PERIOD.DAT_EVALUATION_PERIOD[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EVALUATION_PERIOD.Message.Message);
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

        public async void LoadEvaluationPeriod()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsLoadCustomer);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EVALUATION_PERIOD = JsonConvert.DeserializeObject<JSN_LOAD_EVALUATION_PERIOD>(mResponse);
                    if (mJSN_LOAD_EVALUATION_PERIOD.Message.Code == "7")
                    {
                        this.EvaluationPeriodLoad = mJSN_LOAD_EVALUATION_PERIOD;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EVALUATION_PERIOD.Message.Message);
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
