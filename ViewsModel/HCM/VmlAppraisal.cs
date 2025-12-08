using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.General;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.HCM
{
    public class VmlAppraisal : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_APPRAISAL mJSN_REQ_EMPLOYEE_APPRAISAL = new JSN_REQ_EMPLOYEE_APPRAISAL();
        public JSN_RES_EMPLOYEE_APPRAISAL mJSN_RES_EMPLOYEE_APPRAISAL = new JSN_RES_EMPLOYEE_APPRAISAL();
        public JSN_LOAD_EMPLOYEE_APPRAISAL_DTL mJSN_LOAD_EMPLOYEE_APPRAISAL_DTL = new JSN_LOAD_EMPLOYEE_APPRAISAL_DTL();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlAppraisal()
        {
            this.switchDisplayView(DisplayView.Card);
            AppraisalLoad = new JSN_LOAD_EMPLOYEE_APPRAISAL_DTL();
            AppraisalList = new List<DAT_EMPLOYEE_APPRAISAL>();
            AppraisalOpenList = new List<DAT_EMPLOYEE_APPRAISAL>();
            AppraisalSubmitList = new List<DAT_EMPLOYEE_APPRAISAL>();          
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
        public JSN_LOAD_EMPLOYEE_APPRAISAL_DTL JSN_LOAD_EMPLOYEE_APPRAISAL_DTL =new JSN_LOAD_EMPLOYEE_APPRAISAL_DTL();
        public JSN_LOAD_EMPLOYEE_APPRAISAL_DTL AppraisalLoad
        {
            get { return JSN_LOAD_EMPLOYEE_APPRAISAL_DTL; }
            set { JSN_LOAD_EMPLOYEE_APPRAISAL_DTL = value; NotifyPropertyChanged("AppraisalLoad"); }
        }


        public DAT_EMPLOYEE_APPRAISAL mDAT_EMPLOYEE_APPRAISAL = new DAT_EMPLOYEE_APPRAISAL();
        public DAT_EMPLOYEE_APPRAISAL DAT_EMPLOYEE_APPRAISAL
        {
            get { return mDAT_EMPLOYEE_APPRAISAL; }
            set { mDAT_EMPLOYEE_APPRAISAL = value; NotifyPropertyChanged("DAT_EMPLOYEE_APPRAISAL"); }
        }

        public List<DAT_EMPLOYEE_APPRAISAL> mAppraisalList;
        public List<DAT_EMPLOYEE_APPRAISAL> AppraisalList
        {
            get { return mAppraisalList; }
            set { mAppraisalList = value; NotifyPropertyChanged("AppraisalList"); }
        }

        public List<DAT_EMPLOYEE_APPRAISAL> mAppraisalOpenList;
        public List<DAT_EMPLOYEE_APPRAISAL> AppraisalOpenList
        {
            get { return mAppraisalOpenList; }
            set { mAppraisalOpenList = value; NotifyPropertyChanged("AppraisalOpenList"); }
        }

        public List<DAT_EMPLOYEE_APPRAISAL> mAppraisalSubmitList;
        public List<DAT_EMPLOYEE_APPRAISAL> AppraisalSubmitList
        {
            get { return mAppraisalSubmitList; }
            set { mAppraisalSubmitList = value; NotifyPropertyChanged("AppraisalSubmitList"); }
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
                    mRefreshCommand = new Command(() => this.getAppraisal());
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
        private void bindDataTab(List<DAT_EMPLOYEE_APPRAISAL> argDAT_EMPLOYEE_APPRAISAL_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_APPRAISAL> l_DAT_EMPLOYEE_APPRAISAL_OPEN = new List<DAT_EMPLOYEE_APPRAISAL>();
                List<DAT_EMPLOYEE_APPRAISAL> l_DAT_EMPLOYEE_APPRAISAL_SUBMIT = new List<DAT_EMPLOYEE_APPRAISAL>();
                if (argDAT_EMPLOYEE_APPRAISAL_LST != null && argDAT_EMPLOYEE_APPRAISAL_LST.Count > 0)
                {

                    foreach (DAT_EMPLOYEE_APPRAISAL l_DAT_EMPLOYEE_APPRAISAL in argDAT_EMPLOYEE_APPRAISAL_LST)
                    {
                        if (l_DAT_EMPLOYEE_APPRAISAL.StatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_APPRAISAL_OPEN.Add(l_DAT_EMPLOYEE_APPRAISAL);
                        }
                        else if (l_DAT_EMPLOYEE_APPRAISAL.StatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_APPRAISAL_SUBMIT.Add(l_DAT_EMPLOYEE_APPRAISAL);
                        }
                    }

                    DAT_EMPLOYEE_APPRAISAL = argDAT_EMPLOYEE_APPRAISAL_LST[0];
                    AppraisalList = argDAT_EMPLOYEE_APPRAISAL_LST;
                    AppraisalOpenList = l_DAT_EMPLOYEE_APPRAISAL_OPEN;
                    AppraisalSubmitList = l_DAT_EMPLOYEE_APPRAISAL_SUBMIT;
                }
                else
                {
                    AppraisalList = new List<DAT_EMPLOYEE_APPRAISAL>();
                    AppraisalOpenList = new List<DAT_EMPLOYEE_APPRAISAL>();
                    AppraisalSubmitList = new List<DAT_EMPLOYEE_APPRAISAL>();
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
                List<DAT_EMPLOYEE_APPRAISAL> l_DAT_EMPLOYEE_APPRAISAL_lst = new List<DAT_EMPLOYEE_APPRAISAL>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_APPRAISAL l_DAT_EMPLOYEE_APPRAISAL in mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_APPRAISAL.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_APPRAISAL.AppraisalFormName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_APPRAISAL.ROName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_APPRAISAL_lst.Add(l_DAT_EMPLOYEE_APPRAISAL);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_APPRAISAL_lst = mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL;// OriginalAppraisalList.GetRange(0, OriginalAppraisalList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_APPRAISAL_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getAppraisal()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_APPRAISAL);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeAppraisal);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_APPRAISAL = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_APPRAISAL>(mResponse);
                    if (mJSN_RES_EMPLOYEE_APPRAISAL.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_APPRAISAL.Message.Message);
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

        public async void saveAppraisal()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_APPRAISAL);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeAppraisal);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_EMPLOYEE_APPRAISAL = JsonConvert.DeserializeObject<JSN_RES_EMPLOYEE_APPRAISAL>(mResponse);
                    if (mJSN_RES_EMPLOYEE_APPRAISAL.Message.Code == "7")
                    {
                        if (this.mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL.Count > 0)
                        {
                            DAT_EMPLOYEE_APPRAISAL = this.mJSN_RES_EMPLOYEE_APPRAISAL.DAT_EMPLOYEE_APPRAISAL[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_EMPLOYEE_APPRAISAL.Message.Message);
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

        public async void loadAppraisal()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployeeAppraisal);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_APPRAISAL_DTL = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_APPRAISAL_DTL>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_APPRAISAL_DTL.Message.Code == "7")
                    {
                        this.AppraisalLoad = mJSN_LOAD_EMPLOYEE_APPRAISAL_DTL;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE_APPRAISAL_DTL.Message.Message);
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
