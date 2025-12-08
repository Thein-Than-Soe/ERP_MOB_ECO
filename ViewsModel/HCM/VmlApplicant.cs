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
    public class VmlApplicant : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_APPLICANT mJSN_REQ_APPLICANT = new JSN_REQ_APPLICANT();
        public JSN_APPLICANT_DTL mJSN_APPLICANT_DTL = new JSN_APPLICANT_DTL();
        public JSN_LOAD_APPLICANT mJSN_LOAD_APPLICANT = new JSN_LOAD_APPLICANT();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlApplicant()
        {
            this.switchDisplayView(DisplayView.Card);
            ApplicantLoad = new JSN_LOAD_APPLICANT();
            ApplicantList = new List<DAT_APPLICANT>();
            ApplicantOpenList = new List<DAT_APPLICANT>();
            ApplicantInterviewList = new List<DAT_APPLICANT>();
            ApplicantOfferList = new List<DAT_APPLICANT>();
            ApplicantEmployeeList = new List<DAT_APPLICANT>();
            ApplicantClosedList = new List<DAT_APPLICANT>();
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
        public JSN_LOAD_APPLICANT JSN_LOAD_APPLICANT = new JSN_LOAD_APPLICANT();
        public JSN_LOAD_APPLICANT ApplicantLoad
        {
            get { return JSN_LOAD_APPLICANT; }
            set { JSN_LOAD_APPLICANT = value; NotifyPropertyChanged("ApplicantLoad"); }
        }

        public DAT_APPLICANT mDAT_APPLICANT = new DAT_APPLICANT();
        public DAT_APPLICANT DAT_APPLICANT
        {
            get { return mDAT_APPLICANT; }
            set { mDAT_APPLICANT = value; NotifyPropertyChanged("DAT_APPLICANT"); }
        }

        public List<DAT_APPLICANT> mApplicantList;
        public List<DAT_APPLICANT> ApplicantList
        {
            get { return mApplicantList; }
            set { mApplicantList = value; NotifyPropertyChanged("ApplicantList"); }
        }

        public List<DAT_APPLICANT> mApplicantOpenList;
        public List<DAT_APPLICANT> ApplicantOpenList
        {
            get { return mApplicantOpenList; }
            set { mApplicantOpenList = value; NotifyPropertyChanged("ApplicantOpenList"); }
        }

        public List<DAT_APPLICANT> mApplicantInterviewList;
        public List<DAT_APPLICANT> ApplicantInterviewList
        {
            get { return mApplicantInterviewList; }
            set { mApplicantInterviewList = value; NotifyPropertyChanged("ApplicantInterviewList"); }
        }

        public List<DAT_APPLICANT> mApplicantOfferList;
        public List<DAT_APPLICANT> ApplicantOfferList
        {
            get { return mApplicantOfferList; }
            set { mApplicantOfferList = value; NotifyPropertyChanged("ApplicantOfferList"); }
        }


        public List<DAT_APPLICANT> mApplicantEmployeeList;
        public List<DAT_APPLICANT> ApplicantEmployeeList
        {
            get { return mApplicantEmployeeList; }
            set { mApplicantEmployeeList = value; NotifyPropertyChanged("ApplicantEmployeeList"); }
        }

        public List<DAT_APPLICANT> mApplicantClosedList;
        public List<DAT_APPLICANT> ApplicantClosedList
        {
            get { return mApplicantClosedList; }
            set { mApplicantClosedList = value; NotifyPropertyChanged("ApplicantInterviewList"); }
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
                    mRefreshCommand = new Command(() => this.getApplicant());
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
        private void bindDataTab(List<DAT_APPLICANT> argDAT_APPLICANT_LST)
        {
            try
            {
                List<DAT_APPLICANT> l_DAT_APPLICANT_OPEN = new List<DAT_APPLICANT>();
                List<DAT_APPLICANT> l_DAT_APPLICANT_INTERVIEW = new List<DAT_APPLICANT>();
                List<DAT_APPLICANT> l_DAT_APPLICANT_OFFER = new List<DAT_APPLICANT>();
                List<DAT_APPLICANT> l_DAT_APPLICANT_EMPLOYEE = new List<DAT_APPLICANT>();
                List<DAT_APPLICANT> l_DAT_APPLICANT_CLOSED = new List<DAT_APPLICANT>();
                

                if (argDAT_APPLICANT_LST != null && argDAT_APPLICANT_LST.Count > 0)
                {

                    foreach (DAT_APPLICANT l_DAT_APPLICANT in argDAT_APPLICANT_LST)
                    {
                        if (l_DAT_APPLICANT.ApplicantStatusAsk.Equals("1"))
                        {
                            l_DAT_APPLICANT_OPEN.Add(l_DAT_APPLICANT);
                        }
                        else if (l_DAT_APPLICANT.ApplicantStatusAsk.Equals("2"))
                        {
                            l_DAT_APPLICANT_INTERVIEW.Add(l_DAT_APPLICANT);
                        }
                        else if (l_DAT_APPLICANT.ApplicantStatusAsk.Equals("3"))
                        {
                            l_DAT_APPLICANT_OFFER.Add(l_DAT_APPLICANT);
                        }
                        else if (l_DAT_APPLICANT.ApplicantStatusAsk.Equals("4"))
                        {
                            l_DAT_APPLICANT_EMPLOYEE.Add(l_DAT_APPLICANT);
                        }
                        else if (l_DAT_APPLICANT.ApplicantStatusAsk.Equals("5"))
                        {
                            l_DAT_APPLICANT_CLOSED.Add(l_DAT_APPLICANT);
                        }
                    }

                    DAT_APPLICANT = argDAT_APPLICANT_LST[0];
                    ApplicantList = argDAT_APPLICANT_LST;
                    ApplicantOpenList = l_DAT_APPLICANT_OPEN;
                    ApplicantInterviewList = l_DAT_APPLICANT_INTERVIEW;
                    ApplicantOfferList = l_DAT_APPLICANT_OFFER;
                    ApplicantEmployeeList = l_DAT_APPLICANT_EMPLOYEE;
                    ApplicantClosedList = l_DAT_APPLICANT_CLOSED;

                }
                else
                {
                    ApplicantList = new List<DAT_APPLICANT>();
                    ApplicantOpenList = new List<DAT_APPLICANT>();
                    ApplicantInterviewList = new List<DAT_APPLICANT>();
                    ApplicantOfferList = new List<DAT_APPLICANT>();
                    ApplicantEmployeeList = new List<DAT_APPLICANT>();
                    ApplicantClosedList = new List<DAT_APPLICANT>();
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
                List<DAT_APPLICANT> l_DAT_APPLICANT_lst = new List<DAT_APPLICANT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_APPLICANT l_DAT_APPLICANT in mJSN_APPLICANT_DTL.DAT_APPLICANT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_APPLICANT.ApplyDesignationName.ToLower().Contains(argKeyword)
                            || l_DAT_APPLICANT.ApplicantName.ToLower().Contains(argKeyword)
                            || l_DAT_APPLICANT.ApplicantAvailabilityName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_APPLICANT_lst.Add(l_DAT_APPLICANT);
                        }
                    }
                }
                else
                {
                    l_DAT_APPLICANT_lst = mJSN_APPLICANT_DTL.DAT_APPLICANT;// OriginalApplicantList.GetRange(0, OriginalApplicantList.Count);
                }
                bindDataTab(l_DAT_APPLICANT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getApplicant()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_APPLICANT);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetApplicantVacancy);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_APPLICANT_DTL = JsonConvert.DeserializeObject<JSN_APPLICANT_DTL>(mResponse);
                    if (mJSN_APPLICANT_DTL.Message.Code == "7")
                    {
                        if (this.mJSN_APPLICANT_DTL.DAT_APPLICANT.Count > 0)
                        {
                            bindDataTab(this.mJSN_APPLICANT_DTL.DAT_APPLICANT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_APPLICANT_DTL.Message.Message);
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
        public async void saveApplicant()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_APPLICANT);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveApplicantDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_APPLICANT_DTL = JsonConvert.DeserializeObject<JSN_APPLICANT_DTL>(mResponse);
                    if (mJSN_APPLICANT_DTL.Message.Code == "7")
                    {
                        if (this.mJSN_APPLICANT_DTL.DAT_APPLICANT.Count > 0)
                        {
                            DAT_APPLICANT = this.mJSN_APPLICANT_DTL.DAT_APPLICANT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_APPLICANT_DTL.Message.Message);
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
        public async void loadApplicant()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadApplicant);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_APPLICANT = JsonConvert.DeserializeObject<JSN_LOAD_APPLICANT>(mResponse);
                    if (mJSN_LOAD_APPLICANT.Message.Code == "7")
                    {
                        this.ApplicantLoad = mJSN_LOAD_APPLICANT;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_APPLICANT.Message.Message);
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
