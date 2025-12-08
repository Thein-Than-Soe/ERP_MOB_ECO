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
using System.Linq;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlApplicantEvaluation : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_APPLICANT_EVALUATION mJSN_REQ_APPLICANT_EVALUATION = new JSN_REQ_APPLICANT_EVALUATION();
        public JSN_RES_APPLICANT_EVALUATION mJSN_RES_APPLICANT_EVALUATION = new JSN_RES_APPLICANT_EVALUATION();
        public JSN_LOAD_APPLICANT_EVALUATION mJSN_LOAD_APPLICANT_EVALUATION = new JSN_LOAD_APPLICANT_EVALUATION();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlApplicantEvaluation()
        {
            this.switchDisplayView(DisplayView.Card);
            ApplicantEvaluationLoad = new JSN_LOAD_APPLICANT_EVALUATION();
            ApplicantEvaluationList = new List<DAT_APPLICANT_EVALUATION>();
            Top3List = new List<DAT_APPLICANT_EVALUATION>();
            Top5List = new List<DAT_APPLICANT_EVALUATION>();
            OtherList = new List<DAT_APPLICANT_EVALUATION>();          
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
        public JSN_LOAD_APPLICANT_EVALUATION JSN_LOAD_APPLICANT_EVALUATION = new JSN_LOAD_APPLICANT_EVALUATION();
        public JSN_LOAD_APPLICANT_EVALUATION ApplicantEvaluationLoad
        {
            get { return JSN_LOAD_APPLICANT_EVALUATION; }
            set { JSN_LOAD_APPLICANT_EVALUATION = value; NotifyPropertyChanged("ApplicantEvaluationLoad"); }
        }


        public DAT_APPLICANT_EVALUATION mDAT_APPLICANT_EVALUATION = new DAT_APPLICANT_EVALUATION();
        public DAT_APPLICANT_EVALUATION DAT_APPLICANT_EVALUATION
        {
            get { return mDAT_APPLICANT_EVALUATION; }
            set { mDAT_APPLICANT_EVALUATION = value; NotifyPropertyChanged("DAT_APPLICANT_EVALUATION"); }
        }

        public List<DAT_APPLICANT_EVALUATION> mApplicantEvaluationList;
        public List<DAT_APPLICANT_EVALUATION> ApplicantEvaluationList
        {
            get { return mApplicantEvaluationList; }
            set { mApplicantEvaluationList = value; NotifyPropertyChanged("ApplicantEvaluationList"); }
        }

        public List<DAT_APPLICANT_EVALUATION> mTop3List;
        public List<DAT_APPLICANT_EVALUATION> Top3List
        {
            get { return mTop3List; }
            set { mTop3List = value; NotifyPropertyChanged("Top3List"); }
        }

        public List<DAT_APPLICANT_EVALUATION> mTop5List;
        public List<DAT_APPLICANT_EVALUATION> Top5List
        {
            get { return mTop5List; }
            set { mTop5List = value; NotifyPropertyChanged("Top5List"); }
        }

        public List<DAT_APPLICANT_EVALUATION> mOtherList;
        public List<DAT_APPLICANT_EVALUATION> OtherList
        {
            get { return OtherList; }
            set { mOtherList = value; NotifyPropertyChanged("OtherList"); }
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
                    mRefreshCommand = new Command(() => this.getApplicantEvaluation());
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
        private void bindDataTab(List<DAT_APPLICANT_EVALUATION> argDAT_APPLICANT_EVALUATION_LST)
        {
            try
            {
                
                List<DAT_APPLICANT_EVALUATION> argDAT_EVALUATION= new List<DAT_APPLICANT_EVALUATION>();

                foreach (DAT_APPLICANT_EVALUATION argDAT_APPLICANT_EVALUATION in argDAT_APPLICANT_EVALUATION_LST)
                {

                    argDAT_EVALUATION.Add(argDAT_APPLICANT_EVALUATION);
                    
                }

                argDAT_EVALUATION = argDAT_EVALUATION.OrderByDescending(c => c.TotalRate).ToList();

                List<DAT_APPLICANT_EVALUATION> l_DAT_APPLICANT_EVALUATION_TOP3 = new List<DAT_APPLICANT_EVALUATION>();
                List<DAT_APPLICANT_EVALUATION> l_DAT_APPLICANT_EVALUATION_TOP5 = new List<DAT_APPLICANT_EVALUATION>();
                List<DAT_APPLICANT_EVALUATION> l_DAT_APPLICANT_EVALUATION_OTHER = new List<DAT_APPLICANT_EVALUATION>();

                if (argDAT_APPLICANT_EVALUATION_LST != null && argDAT_APPLICANT_EVALUATION_LST.Count > 0)
                {

                   
                    DAT_APPLICANT_EVALUATION l_DAT_APPLICANT_EVALUATION;

                        for (int i = 1; i < 4; i++)
                        {
                            l_DAT_APPLICANT_EVALUATION = argDAT_EVALUATION[i];
                        l_DAT_APPLICANT_EVALUATION.TotalRate = Utility.getAmountString(l_DAT_APPLICANT_EVALUATION.TotalRate);
                        l_DAT_APPLICANT_EVALUATION_TOP3.Add(l_DAT_APPLICANT_EVALUATION);
                        }

                       for (int i = 1; i <6 ; i++)
                       {
                        l_DAT_APPLICANT_EVALUATION = argDAT_EVALUATION[i];
                        l_DAT_APPLICANT_EVALUATION.TotalRate = Utility.getAmountString(l_DAT_APPLICANT_EVALUATION.TotalRate);

                        l_DAT_APPLICANT_EVALUATION_TOP5.Add(l_DAT_APPLICANT_EVALUATION);
                       }

                      
                       for (int i = 6; i < argDAT_APPLICANT_EVALUATION_LST.Count; i++)
                       {
                        l_DAT_APPLICANT_EVALUATION = argDAT_EVALUATION[i];
                        l_DAT_APPLICANT_EVALUATION.TotalRate = Utility.getAmountString(l_DAT_APPLICANT_EVALUATION.TotalRate);

                        l_DAT_APPLICANT_EVALUATION_OTHER.Add(l_DAT_APPLICANT_EVALUATION);
                       }

                  

                    DAT_APPLICANT_EVALUATION = argDAT_APPLICANT_EVALUATION_LST[0];
                    ApplicantEvaluationList = argDAT_APPLICANT_EVALUATION_LST;
                    Top3List = l_DAT_APPLICANT_EVALUATION_TOP3;
                    Top5List = l_DAT_APPLICANT_EVALUATION_TOP5;
                    OtherList = l_DAT_APPLICANT_EVALUATION_OTHER;
                }
                else
                {
                    ApplicantEvaluationList = new List<DAT_APPLICANT_EVALUATION>();
                    Top3List = new List<DAT_APPLICANT_EVALUATION>();
                    Top5List = new List<DAT_APPLICANT_EVALUATION>();
                    OtherList = new List<DAT_APPLICANT_EVALUATION>();
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
                List<DAT_APPLICANT_EVALUATION> l_DAT_APPLICANT_EVALUATION_lst = new List<DAT_APPLICANT_EVALUATION>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_APPLICANT_EVALUATION l_DAT_APPLICANT_EVALUATION in mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_APPLICANT_EVALUATION.ApplicantName.ToLower().Contains(argKeyword)
                            || l_DAT_APPLICANT_EVALUATION.DesignationName.ToLower().Contains(argKeyword)
                            || l_DAT_APPLICANT_EVALUATION.EvaluationFormName.ToLower().Contains(argKeyword)
                            || l_DAT_APPLICANT_EVALUATION.TotalRate.ToLower().Contains(argKeyword))
                        {
                            l_DAT_APPLICANT_EVALUATION_lst.Add(l_DAT_APPLICANT_EVALUATION);
                        }
                    }
                }
                else
                {
                    l_DAT_APPLICANT_EVALUATION_lst = mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION;// OriginalApplicantEvaluationList.GetRange(0, OriginalApplicantEvaluationList.Count);
                }
                bindDataTab(l_DAT_APPLICANT_EVALUATION_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getApplicantEvaluation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_APPLICANT_EVALUATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetApplicantEvaluationDtl);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_APPLICANT_EVALUATION = JsonConvert.DeserializeObject<JSN_RES_APPLICANT_EVALUATION>(mResponse);
                    if (mJSN_RES_APPLICANT_EVALUATION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION.Count > 0)
                        {
                            

                            bindDataTab(this.mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_APPLICANT_EVALUATION.Message.Message);
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

        public async void saveApplicantEvaluation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_APPLICANT_EVALUATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveApplicantEvaluation);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_APPLICANT_EVALUATION = JsonConvert.DeserializeObject<JSN_RES_APPLICANT_EVALUATION>(mResponse);
                    if (mJSN_RES_APPLICANT_EVALUATION.Message.Code == "7")
                    {
                        if (this.mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION.Count > 0)
                        {
                            DAT_APPLICANT_EVALUATION = this.mJSN_RES_APPLICANT_EVALUATION.DAT_APPLICANT_EVALUATION[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_APPLICANT_EVALUATION.Message.Message);
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

        public async void loadApplicantEvaluation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadApplicant);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_APPLICANT_EVALUATION = JsonConvert.DeserializeObject<JSN_LOAD_APPLICANT_EVALUATION>(mResponse);
                    if (mJSN_LOAD_APPLICANT_EVALUATION.Message.Code == "7")
                    {
                        this.ApplicantEvaluationLoad = mJSN_LOAD_APPLICANT_EVALUATION;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_APPLICANT_EVALUATION.Message.Message);
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
