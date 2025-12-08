
using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.ViewsModel.Frame;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlSurvey : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_SURVEY mJSN_REQ_EMPLOYEE_SURVEY = new JSN_REQ_EMPLOYEE_SURVEY();
        public JSN_RES_EMPLOYEE_SURVEY mJSN_RES_EMPLOYEE_SURVEY = new JSN_RES_EMPLOYEE_SURVEY();
        public JSN_LOAD_EMPLOYEE_SURVEY mJSN_LOAD_EMPLOYEE_SURVEY = new JSN_LOAD_EMPLOYEE_SURVEY();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSurvey()
        {
            this.switchDisplayView(DisplayView.Card);
            SurveyLoad = new JSN_LOAD_EMPLOYEE_SURVEY();
            SurveyList = new List<DAT_EMPLOYEE_SURVEY>();
            SurveyOpenList = new List<DAT_EMPLOYEE_SURVEY>();
            SurveySubmittedList = new List<DAT_EMPLOYEE_SURVEY>();
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
        public JSN_LOAD_EMPLOYEE_SURVEY JSN_LOAD_EMPLOYEE_SURVEY = new JSN_LOAD_EMPLOYEE_SURVEY();
        public JSN_LOAD_EMPLOYEE_SURVEY SurveyLoad
        {
            get { return JSN_LOAD_EMPLOYEE_SURVEY; }
            set { JSN_LOAD_EMPLOYEE_SURVEY = value; NotifyPropertyChanged("SurveyLoad"); }
        }

        public DAT_EMPLOYEE_SURVEY mDAT_EMPLOYEE_SURVEY = new DAT_EMPLOYEE_SURVEY();
        public DAT_EMPLOYEE_SURVEY DAT_EMPLOYEE_SURVEY
        {
            get { return mDAT_EMPLOYEE_SURVEY; }
            set { mDAT_EMPLOYEE_SURVEY = value; NotifyPropertyChanged("DAT_EMPLOYEE_SURVEY"); }
        }

        public List<DAT_EMPLOYEE_SURVEY> mSurveyList;
        public List<DAT_EMPLOYEE_SURVEY> SurveyList
        {
            get { return mSurveyList; }
            set { mSurveyList = value; NotifyPropertyChanged("SurveyList"); }
        }

        public List<DAT_EMPLOYEE_SURVEY> mSurveyOpenList;
        public List<DAT_EMPLOYEE_SURVEY> SurveyOpenList
        {
            get { return mSurveyOpenList; }
            set { mSurveyOpenList = value; NotifyPropertyChanged("SurveyOpenList"); }
        }

        public List<DAT_EMPLOYEE_SURVEY> mSurveySubmittedList;
        public List<DAT_EMPLOYEE_SURVEY> SurveySubmittedList
        {
            get { return mSurveySubmittedList; }
            set { mSurveySubmittedList = value; NotifyPropertyChanged("SurveySubmittedList"); }
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
                    mRefreshCommand = new Command(() => this.getSurvey());
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
        private void bindDataTab(List<DAT_EMPLOYEE_SURVEY> argDAT_EMPLOYEE_SURVEY_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_SURVEY> l_DAT_EMPLOYEE_SURVEY_OPEN = new List<DAT_EMPLOYEE_SURVEY>();
                List<DAT_EMPLOYEE_SURVEY> l_DAT_EMPLOYEE_SURVEY_SUBMIT = new List<DAT_EMPLOYEE_SURVEY>();
                if (argDAT_EMPLOYEE_SURVEY_LST != null && argDAT_EMPLOYEE_SURVEY_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_SURVEY l_DAT_EMPLOYEE_SURVEY in argDAT_EMPLOYEE_SURVEY_LST)
                    {
                        l_DAT_EMPLOYEE_SURVEY.SurveyDate = Utility.getDateTimeString(l_DAT_EMPLOYEE_SURVEY.SurveyDate);

                        if (l_DAT_EMPLOYEE_SURVEY.StatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_SURVEY_OPEN.Add(l_DAT_EMPLOYEE_SURVEY);
                        }
                        else if (l_DAT_EMPLOYEE_SURVEY.StatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_SURVEY_SUBMIT.Add(l_DAT_EMPLOYEE_SURVEY);
                        }
                    }

                    DAT_EMPLOYEE_SURVEY = argDAT_EMPLOYEE_SURVEY_LST[0];
                    SurveyList = argDAT_EMPLOYEE_SURVEY_LST;
                    SurveyOpenList = l_DAT_EMPLOYEE_SURVEY_OPEN;
                    SurveySubmittedList = l_DAT_EMPLOYEE_SURVEY_SUBMIT;
                }
                else
                {
                    SurveyList = new List<DAT_EMPLOYEE_SURVEY>();
                    SurveyOpenList = new List<DAT_EMPLOYEE_SURVEY>();
                    SurveySubmittedList = new List<DAT_EMPLOYEE_SURVEY>();
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
                List<DAT_EMPLOYEE_SURVEY> l_DAT_EMPLOYEE_SURVEY_lst = new List<DAT_EMPLOYEE_SURVEY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_SURVEY l_DAT_EMPLOYEE_SURVEY in mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_SURVEY.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_SURVEY.SurveyName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_SURVEY.SurveyDate.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_SURVEY_lst.Add(l_DAT_EMPLOYEE_SURVEY);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_SURVEY_lst = mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY;// OriginalSurveyList.GetRange(0, OriginalSurveyList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_SURVEY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getSurvey()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_SURVEY);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeSurveyDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_SURVEY = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_SURVEY>(mResponse);
                    if (mJSN_RES_EMPLOYEE_SURVEY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_SURVEY.Message.Message);
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

        public async void saveSurvey()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_SURVEY);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeSurvey);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_SURVEY = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_SURVEY>(mResponse);
                    if (mJSN_RES_EMPLOYEE_SURVEY.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY.Count > 0)
                        {
                            DAT_EMPLOYEE_SURVEY = this.mJSN_RES_EMPLOYEE_SURVEY.DAT_EMPLOYEE_SURVEY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_SURVEY.Message.Message);
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

        public async void loadSurvey()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployeeSurvey);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_SURVEY = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_SURVEY>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_SURVEY.Message.Code == "7")
                    {
                        this.SurveyLoad = mJSN_LOAD_EMPLOYEE_SURVEY;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE_SURVEY.Message.Message);
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
