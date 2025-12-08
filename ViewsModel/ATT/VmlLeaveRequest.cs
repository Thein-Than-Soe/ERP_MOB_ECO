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

namespace CS.ERP_MOB.ViewsModel.ATT
{
    public class VmlLeaveRequest : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_LEAVE_TAKEN mJSN_REQ_EMPLOYEE_LEAVE_TAKEN = new JSN_REQ_EMPLOYEE_LEAVE_TAKEN();
        public JSN_EMPLOYEE_LEAVE_TAKEN mJSN_EMPLOYEE_LEAVE_TAKEN = new JSN_EMPLOYEE_LEAVE_TAKEN();
        public JSN_LOAD_EMPLOYEE_LEAVE_TAKEN mJSN_LOAD_EMPLOYEE_LEAVE_TAKEN = new JSN_LOAD_EMPLOYEE_LEAVE_TAKEN();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlLeaveRequest()
        {
            this.switchDisplayView(DisplayView.Card);
            this.LeaveReqLoad = new JSN_LOAD_EMPLOYEE_LEAVE_TAKEN();
            LeaveTakenSubmitList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
            LeaveTakenApprovedList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
            LeaveTakenRejectList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
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
        public JSN_LOAD_EMPLOYEE_LEAVE_TAKEN JSN_LOAD_EMPLOYEE_LEAVE_TAKEN = new JSN_LOAD_EMPLOYEE_LEAVE_TAKEN();
        public JSN_LOAD_EMPLOYEE_LEAVE_TAKEN LeaveReqLoad
        {
            get { return JSN_LOAD_EMPLOYEE_LEAVE_TAKEN; }
            set { JSN_LOAD_EMPLOYEE_LEAVE_TAKEN = value; NotifyPropertyChanged("LeaveReqLoad"); }
        }


        public DAT_EMPLOYEE_LEAVE_TAKEN mDAT_EMPLOYEE_LEAVE_TAKEN = new DAT_EMPLOYEE_LEAVE_TAKEN();
        public DAT_EMPLOYEE_LEAVE_TAKEN DAT_EMPLOYEE_LEAVE_TAKEN
        {
            get { return mDAT_EMPLOYEE_LEAVE_TAKEN; }
            set { mDAT_EMPLOYEE_LEAVE_TAKEN = value; NotifyPropertyChanged("DAT_EMPLOYEE_LEAVE_TAKEN"); }
        }

        public List<DAT_EMPLOYEE_LEAVE_TAKEN> mLeaveTakenList;
        public List<DAT_EMPLOYEE_LEAVE_TAKEN> LeaveTakenList
        {
            get { return mLeaveTakenList; }
            set { mLeaveTakenList = value; NotifyPropertyChanged("LeaveTakenList"); }
        }

        public List<DAT_EMPLOYEE_LEAVE_TAKEN> mLeaveTakenSubmitList;
        public List<DAT_EMPLOYEE_LEAVE_TAKEN> LeaveTakenSubmitList
        {
            get { return mLeaveTakenSubmitList; }
            set { mLeaveTakenSubmitList = value; NotifyPropertyChanged("LeaveTakenSubmitList"); }
        }

        public List<DAT_EMPLOYEE_LEAVE_TAKEN> mLeaveTakenApprovedList;
        public List<DAT_EMPLOYEE_LEAVE_TAKEN> LeaveTakenApprovedList
        {
            get { return mLeaveTakenApprovedList; }
            set { mLeaveTakenApprovedList = value; NotifyPropertyChanged("LeaveTakenApprovedList"); }
        }

        public List<DAT_EMPLOYEE_LEAVE_TAKEN> mLeaveTakenRejectList;
        public List<DAT_EMPLOYEE_LEAVE_TAKEN> LeaveTakenRejectList
        {
            get { return mLeaveTakenRejectList; }
            set { mLeaveTakenRejectList = value; NotifyPropertyChanged("mLeaveTakenRejectList"); }
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
                    mRefreshCommand = new Command(() => this.getLeaveTaken());
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
        private void bindDataTab(List<DAT_EMPLOYEE_LEAVE_TAKEN> argDAT_EMPLOYEE_LEAVE_TAKEN_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_LEAVE_TAKEN> l_DAT_EMPLOYEE_LEAVE_TAKEN_SUBMIT = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                List<DAT_EMPLOYEE_LEAVE_TAKEN> l_DAT_EMPLOYEE_LEAVE_TAKEN_APPROVED = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                List<DAT_EMPLOYEE_LEAVE_TAKEN> l_DAT_EMPLOYEE_LEAVE_TAKEN_REJECTED = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();

                if (argDAT_EMPLOYEE_LEAVE_TAKEN_LST != null && argDAT_EMPLOYEE_LEAVE_TAKEN_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_LEAVE_TAKEN l_DAT_EMPLOYEE_LEAVE_TAKEN in argDAT_EMPLOYEE_LEAVE_TAKEN_LST)
                    {
                        if (l_DAT_EMPLOYEE_LEAVE_TAKEN.StatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_LEAVE_TAKEN_SUBMIT.Add(l_DAT_EMPLOYEE_LEAVE_TAKEN);
                        }
                        else if (l_DAT_EMPLOYEE_LEAVE_TAKEN.StatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_LEAVE_TAKEN_APPROVED.Add(l_DAT_EMPLOYEE_LEAVE_TAKEN);
                        }
                        else if (l_DAT_EMPLOYEE_LEAVE_TAKEN.StatusAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_LEAVE_TAKEN_REJECTED.Add(l_DAT_EMPLOYEE_LEAVE_TAKEN);
                        }
                    }

                    DAT_EMPLOYEE_LEAVE_TAKEN = argDAT_EMPLOYEE_LEAVE_TAKEN_LST[0];
                    LeaveTakenList = argDAT_EMPLOYEE_LEAVE_TAKEN_LST;
                    LeaveTakenSubmitList = l_DAT_EMPLOYEE_LEAVE_TAKEN_SUBMIT;
                    LeaveTakenApprovedList = l_DAT_EMPLOYEE_LEAVE_TAKEN_APPROVED;
                    LeaveTakenRejectList = l_DAT_EMPLOYEE_LEAVE_TAKEN_REJECTED;
                }
                else
                {
                    LeaveTakenList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                    LeaveTakenSubmitList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                    LeaveTakenApprovedList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                    LeaveTakenRejectList = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
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
                List<DAT_EMPLOYEE_LEAVE_TAKEN> l_DAT_EMPLOYEE_LEAVE_TAKEN_lst = new List<DAT_EMPLOYEE_LEAVE_TAKEN>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_LEAVE_TAKEN l_DAT_EMPLOYEE_LEAVE_TAKEN in mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_LEAVE_TAKEN.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_LEAVE_TAKEN.LeaveName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_LEAVE_TAKEN.LeaveTypeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_LEAVE_TAKEN_lst.Add(l_DAT_EMPLOYEE_LEAVE_TAKEN);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_LEAVE_TAKEN_lst = mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN;// OriginalLeaveTakenList.GetRange(0, OriginalLeaveTakenList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_LEAVE_TAKEN_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getLeaveTaken()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_LEAVE_TAKEN);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeLeaveTaken);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_LEAVE_TAKEN = JsonConvert.DeserializeObject<JSN_EMPLOYEE_LEAVE_TAKEN>(mResponse);
                    if (mJSN_EMPLOYEE_LEAVE_TAKEN.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN.Count > 0)
                        {
                            bindDataTab(this.mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_LEAVE_TAKEN.Message.Message);
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

        public async void saveLeaveTaken()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_LEAVE_TAKEN);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeLeaveTaken);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_LEAVE_TAKEN = JsonConvert.DeserializeObject<JSN_EMPLOYEE_LEAVE_TAKEN>(mResponse);
                    if (mJSN_EMPLOYEE_LEAVE_TAKEN.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN.Count > 0)
                        {
                            DAT_EMPLOYEE_LEAVE_TAKEN = this.mJSN_EMPLOYEE_LEAVE_TAKEN.DAT_EMPLOYEE_LEAVE_TAKEN[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_LEAVE_TAKEN.Message.Message);
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

        public async void loadLeaveReq()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployeeLeaveTaken);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_LEAVE_TAKEN = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_LEAVE_TAKEN>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_LEAVE_TAKEN.Message.Code == "7")
                    {
                        this.LeaveReqLoad = mJSN_LOAD_EMPLOYEE_LEAVE_TAKEN;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_LEAVE_TAKEN.Message.Message);
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
