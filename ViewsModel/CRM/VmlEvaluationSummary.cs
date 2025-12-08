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
    public class VmlEvaluationSummary : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_TICKET_SUMMARY_DTL mJSN_REQ_TICKET_SUMMARY_DTL = new JSN_REQ_TICKET_SUMMARY_DTL();
        public JSN_RES_TICKET_SUMMARY_DTL mJSN_RES_TICKET_SUMMARY_DTL = new JSN_RES_TICKET_SUMMARY_DTL();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlEvaluationSummary()
        {
            this.switchDisplayView(DisplayView.Card);
            SummaryOpenList = new List<DAT_TICKET_SUMMARY>();
            SummaryClosedList = new List<DAT_TICKET_SUMMARY>();
            SummaryList = new List<DAT_TICKET_SUMMARY>();
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
        public DAT_TICKET_SUMMARY mDAT_TICKET_SUMMARY = new DAT_TICKET_SUMMARY();
        public DAT_TICKET_SUMMARY DAT_TICKET_SUMMARY
        {
            get { return mDAT_TICKET_SUMMARY; }
            set { mDAT_TICKET_SUMMARY = value; NotifyPropertyChanged("DAT_TICKET_SUMMARY"); }
        }

        public List<DAT_TICKET_SUMMARY> mSummaryList;
        public List<DAT_TICKET_SUMMARY> SummaryList
        {
            get { return mSummaryList; }
            set { mSummaryList = value; NotifyPropertyChanged("SummaryList"); }
        }

        public List<DAT_TICKET_SUMMARY> mSummaryOpenList;
        public List<DAT_TICKET_SUMMARY> SummaryOpenList
        {
            get { return mSummaryOpenList; }
            set { mSummaryOpenList = value; NotifyPropertyChanged("SummaryOpenList"); }
        }

        public List<DAT_TICKET_SUMMARY> mSummaryClosedList;
        public List<DAT_TICKET_SUMMARY> SummaryClosedList
        {
            get { return mSummaryClosedList; }
            set { mSummaryClosedList = value; NotifyPropertyChanged("SummaryClosedList"); }
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
                    mRefreshCommand = new Command(() => this.getEvaluationSummary());
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
        private void bindDataTab(List<DAT_TICKET_SUMMARY> argDAT_TICKET_SUMMARY_LST)
        {
            try
            {
                List<DAT_TICKET_SUMMARY> l_DAT_TICKET_SUMMARY_OPEN = new List<DAT_TICKET_SUMMARY>();
                List<DAT_TICKET_SUMMARY> l_DAT_TICKET_SUMMARY_CLOSED = new List<DAT_TICKET_SUMMARY>();


                if (argDAT_TICKET_SUMMARY_LST != null && argDAT_TICKET_SUMMARY_LST.Count > 0)
                {
                    foreach (DAT_TICKET_SUMMARY l_DAT_TICKET_SUMMARY in argDAT_TICKET_SUMMARY_LST)
                    {
                        if (l_DAT_TICKET_SUMMARY.StatusAsk.Equals("1"))
                        {                           
                            l_DAT_TICKET_SUMMARY_OPEN.Add(l_DAT_TICKET_SUMMARY);
                        }

                        else if (l_DAT_TICKET_SUMMARY.StatusAsk.Equals("5"))
                        {
                           
                            l_DAT_TICKET_SUMMARY_CLOSED.Add(l_DAT_TICKET_SUMMARY);
                        }
                    }

                    DAT_TICKET_SUMMARY = argDAT_TICKET_SUMMARY_LST[0];
                    SummaryList = argDAT_TICKET_SUMMARY_LST;
                    SummaryOpenList = l_DAT_TICKET_SUMMARY_OPEN;
                    SummaryClosedList = l_DAT_TICKET_SUMMARY_CLOSED;

                }
                else
                {
                    SummaryList = new List<DAT_TICKET_SUMMARY>();
                    SummaryOpenList = new List<DAT_TICKET_SUMMARY>();
                    SummaryClosedList = new List<DAT_TICKET_SUMMARY>();

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
                List<DAT_TICKET_SUMMARY> l_DAT_TICKET_SUMMARY_lst = new List<DAT_TICKET_SUMMARY>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_TICKET_SUMMARY l_DAT_TICKET_SUMMARY in mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_TICKET_SUMMARY.EvaluationPeriodName.ToLower().Contains(argKeyword)
                            || l_DAT_TICKET_SUMMARY.TotalTicket.ToLower().Contains(argKeyword)
                             || l_DAT_TICKET_SUMMARY.GradeName.ToLower().Contains(argKeyword)
                            || l_DAT_TICKET_SUMMARY.TotalPoint.ToLower().Contains(argKeyword))
                        {
                            l_DAT_TICKET_SUMMARY_lst.Add(l_DAT_TICKET_SUMMARY);
                        }
                    }
                }
                else
                {
                    l_DAT_TICKET_SUMMARY_lst = mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY;// OriginalSummaryList.GetRange(0, OriginalSummaryList.Count);
                }
                bindDataTab(l_DAT_TICKET_SUMMARY_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getEvaluationSummary()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_TICKET_SUMMARY_DTL);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetSummaryDetail);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_TICKET_SUMMARY_DTL = JsonConvert.DeserializeObject<JSN_RES_TICKET_SUMMARY_DTL>(mResponse);
                    if (mJSN_RES_TICKET_SUMMARY_DTL.Message.Code == "7")
                    {
                        if (this.mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_TICKET_SUMMARY_DTL.Message.Message);
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

        public async void saveEvaluationSummary()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_TICKET_SUMMARY_DTL);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetEvaluationPeriod);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_TICKET_SUMMARY_DTL = JsonConvert.DeserializeObject<JSN_RES_TICKET_SUMMARY_DTL>(mResponse);
                    if (mJSN_RES_TICKET_SUMMARY_DTL.Message.Code == "7")
                    {
                        if (this.mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY.Count > 0)
                        {
                            DAT_TICKET_SUMMARY = this.mJSN_RES_TICKET_SUMMARY_DTL.DAT_TICKET_SUMMARY[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_TICKET_SUMMARY_DTL.Message.Message);
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
