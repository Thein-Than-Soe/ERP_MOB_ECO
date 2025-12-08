using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlVacancy : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_JOB_VACANCY mJSN_REQ_JOB_VACANCY = new JSN_REQ_JOB_VACANCY();
        public JSN_JOB_VACANCY mJSN_JOB_VACANCY = new JSN_JOB_VACANCY();
        public JSN_LOAD_JOB_VACANCY mJSN_LOAD_JOB_VACANCY = new JSN_LOAD_JOB_VACANCY();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlVacancy()
        {
            this.switchDisplayView(DisplayView.Card);
            VacancyLoad = new JSN_LOAD_JOB_VACANCY();
            JobVacancyList = new List<DAT_JOB_VACANCY>();
            JobVacancyActiveList = new List<DAT_JOB_VACANCY>();
            JobVacancyClosedList = new List<DAT_JOB_VACANCY>();
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
        public JSN_LOAD_JOB_VACANCY JSN_LOAD_JOB_VACANCY = new JSN_LOAD_JOB_VACANCY();
        public JSN_LOAD_JOB_VACANCY VacancyLoad
        {
            get { return JSN_LOAD_JOB_VACANCY; }
            set { JSN_LOAD_JOB_VACANCY = value; NotifyPropertyChanged("VacancyLoad"); }
        }

        public DAT_JOB_VACANCY mDAT_JOB_VACANCY = new DAT_JOB_VACANCY();
        public DAT_JOB_VACANCY DAT_JOB_VACANCY
        {
            get { return mDAT_JOB_VACANCY; }
            set { mDAT_JOB_VACANCY = value; NotifyPropertyChanged("DAT_JOB_VACANCY"); }
        }

        public List<DAT_JOB_VACANCY> mJobVacancyList;
        public List<DAT_JOB_VACANCY> JobVacancyList
        {
            get { return mJobVacancyList; }
            set { mJobVacancyList = value; NotifyPropertyChanged("JobVacancyList"); }
        }

        public List<DAT_JOB_VACANCY> mJobVacancyActiveList;
        public List<DAT_JOB_VACANCY> JobVacancyActiveList
        {
            get { return mJobVacancyActiveList; }
            set { mJobVacancyActiveList = value; NotifyPropertyChanged("JobVacancyActiveList"); }
        }

        public List<DAT_JOB_VACANCY> mJobVacancyClosedList;
        public List<DAT_JOB_VACANCY> JobVacancyClosedList
        {
            get { return mJobVacancyClosedList; }
            set { mJobVacancyClosedList = value; NotifyPropertyChanged("JobVacancyClosedList"); }
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
                    mRefreshCommand = new Command(() => this.getJobVacancy());
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
        private void bindDataTab(List<DAT_JOB_VACANCY> argDAT_JOB_VACANCY_LST)
        {
            try
            {
                List<DAT_JOB_VACANCY> l_DAT_JOB_VACANCY_ACTIVE = new List<DAT_JOB_VACANCY>();
                List<DAT_JOB_VACANCY> l_DAT_JOB_VACANCY_CLOSED = new List<DAT_JOB_VACANCY>();
                if (argDAT_JOB_VACANCY_LST != null && argDAT_JOB_VACANCY_LST.Count > 0)
                {
                    foreach (DAT_JOB_VACANCY l_DAT_JOB_VACANCY in argDAT_JOB_VACANCY_LST)
                    {
                        l_DAT_JOB_VACANCY.SD = Utility.getDateTimeString(l_DAT_JOB_VACANCY.SD);
                        l_DAT_JOB_VACANCY.ED = Utility.getDateTimeString(l_DAT_JOB_VACANCY.ED);

                        if (l_DAT_JOB_VACANCY.StatusAsk.Equals("1"))
                        {
                            l_DAT_JOB_VACANCY_ACTIVE.Add(l_DAT_JOB_VACANCY);
                        }
                        else if (l_DAT_JOB_VACANCY.StatusAsk.Equals("9"))
                        {
                            l_DAT_JOB_VACANCY_CLOSED.Add(l_DAT_JOB_VACANCY);
                        }
                    }

                    DAT_JOB_VACANCY = argDAT_JOB_VACANCY_LST[0];
                    JobVacancyList = argDAT_JOB_VACANCY_LST;
                    JobVacancyActiveList = l_DAT_JOB_VACANCY_ACTIVE;
                    JobVacancyClosedList = l_DAT_JOB_VACANCY_CLOSED;
                }
                else
                {
                    JobVacancyList = new List<DAT_JOB_VACANCY>();
                    JobVacancyActiveList = new List<DAT_JOB_VACANCY>();
                    JobVacancyClosedList = new List<DAT_JOB_VACANCY>();
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
                List<DAT_JOB_VACANCY> l_DAT_JOB_VACANCY_lst = new List<DAT_JOB_VACANCY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_JOB_VACANCY l_DAT_JOB_VACANCY in mJSN_JOB_VACANCY.DAT_JOB_VACANCY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_JOB_VACANCY.DesignationName.ToLower().Contains(argKeyword)
                            || l_DAT_JOB_VACANCY.EmploymentTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_JOB_VACANCY.JD.ToLower().Contains(argKeyword))
                        {
                            l_DAT_JOB_VACANCY_lst.Add(l_DAT_JOB_VACANCY);
                        }
                    }
                }
                else
                {
                    l_DAT_JOB_VACANCY_lst = mJSN_JOB_VACANCY.DAT_JOB_VACANCY;// OriginalJobVacancyList.GetRange(0, OriginalJobVacancyList.Count);
                }
                bindDataTab(l_DAT_JOB_VACANCY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getJobVacancy()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_JOB_VACANCY);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetJobVacancy);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_JOB_VACANCY = JsonConvert.DeserializeObject<JSN_JOB_VACANCY>(mResponse);
                    if (mJSN_JOB_VACANCY.Message.Code == "7")
                    {
                        if (this.mJSN_JOB_VACANCY.DAT_JOB_VACANCY.Count > 0)
                        {
                            bindDataTab(this.mJSN_JOB_VACANCY.DAT_JOB_VACANCY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_JOB_VACANCY.Message.Message);
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
        public async void saveJobVacancy()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_JOB_VACANCY);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveJobVacancy);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_JOB_VACANCY = JsonConvert.DeserializeObject<JSN_JOB_VACANCY>(mResponse);
                    if (mJSN_JOB_VACANCY.Message.Code == "7")
                    {
                        if (this.mJSN_JOB_VACANCY.DAT_JOB_VACANCY.Count > 0)
                        {
                            DAT_JOB_VACANCY = this.mJSN_JOB_VACANCY.DAT_JOB_VACANCY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_JOB_VACANCY.Message.Message);
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
        public async void loadVacancy()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadJobVacancy);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_JOB_VACANCY = JsonConvert.DeserializeObject<JSN_LOAD_JOB_VACANCY>(mResponse);
                    if (mJSN_LOAD_JOB_VACANCY.Message.Code == "7")
                    {
                        this.VacancyLoad = mJSN_LOAD_JOB_VACANCY;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_JOB_VACANCY.Message.Message);
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
