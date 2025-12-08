using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.General;
using System.Collections.Generic;
using System;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.HCM
{

    public class VmlClaim : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_EMPLOYEE_CLAIM mJSN_REQ_EMPLOYEE_CLAIM = new JSN_REQ_EMPLOYEE_CLAIM();
        public JSN_EMPLOYEE_CLAIM mJSN_EMPLOYEE_CLAIM = new JSN_EMPLOYEE_CLAIM();
        public JSN_LOAD_EMPLOYEE_CLAIM mJSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();
        public List<DAT_EMPLOYEE> mDAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        public List<DAT_CLAIMS_TYPE> mDAT_CLAIMS_TYPE = new List<DAT_CLAIMS_TYPE>();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlClaim()
        {
            this.switchDisplayView(DisplayView.Card);
            ClaimLoad = new JSN_LOAD_EMPLOYEE_CLAIM();
            EmployeeRo = new List<DAT_EMPLOYEE>();
            ClaimType = new List<DAT_CLAIMS_TYPE>();
            EmployeeClaimList = new List<DAT_EMPLOYEE_CLAIM>();
            EmployeeClaimSubmitList = new List<DAT_EMPLOYEE_CLAIM>();
            EmployeeClaimApprovedList = new List<DAT_EMPLOYEE_CLAIM>();
            EmployeeClaimRejectList = new List<DAT_EMPLOYEE_CLAIM>();
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
        public JSN_LOAD_EMPLOYEE_CLAIM JSN_LOAD_EMPLOYEE_CLAIM = new JSN_LOAD_EMPLOYEE_CLAIM();
        public JSN_LOAD_EMPLOYEE_CLAIM ClaimLoad
        {
            get { return JSN_LOAD_EMPLOYEE_CLAIM; }
            set { JSN_LOAD_EMPLOYEE_CLAIM = value; NotifyPropertyChanged("ClaimLoad"); }
        }

        public List<DAT_EMPLOYEE> DAT_EMPLOYEE = new List<DAT_EMPLOYEE>();
        public List<DAT_EMPLOYEE> EmployeeRo
        {
            get { return DAT_EMPLOYEE; }
            set { DAT_EMPLOYEE = value; NotifyPropertyChanged("EmployeeRo"); }
        }

        public List<DAT_CLAIMS_TYPE> DAT_CLAIMS_TYPE = new List<DAT_CLAIMS_TYPE>();
        public List<DAT_CLAIMS_TYPE> ClaimType
        {
            get { return DAT_CLAIMS_TYPE; }
            set { DAT_CLAIMS_TYPE = value; NotifyPropertyChanged("ClaimsType"); }
        }

        public DAT_EMPLOYEE_CLAIM mDAT_EMPLOYEE_CLAIM = new DAT_EMPLOYEE_CLAIM();
        public DAT_EMPLOYEE_CLAIM DAT_EMPLOYEE_CLAIM
        {
            get { return mDAT_EMPLOYEE_CLAIM; }
            set { mDAT_EMPLOYEE_CLAIM = value; NotifyPropertyChanged("DAT_EMPLOYEE_CLAIM"); }
        }

        public List<DAT_EMPLOYEE_CLAIM> mEmployeeClaimList;
        public List<DAT_EMPLOYEE_CLAIM> EmployeeClaimList
        {
            get { return mEmployeeClaimList; }
            set { mEmployeeClaimList = value; NotifyPropertyChanged("EmployeeClaimList"); }
        }

        public List<DAT_EMPLOYEE_CLAIM> mEmployeeClaimSubmitList;
        public List<DAT_EMPLOYEE_CLAIM> EmployeeClaimSubmitList
        {
            get { return mEmployeeClaimSubmitList; }
            set { mEmployeeClaimSubmitList = value; NotifyPropertyChanged("EmployeeClaimSubmitList"); }
        }

        public List<DAT_EMPLOYEE_CLAIM> mEmployeeClaimApprovedList;
        public List<DAT_EMPLOYEE_CLAIM> EmployeeClaimApprovedList
        {
            get { return mEmployeeClaimApprovedList; }
            set { mEmployeeClaimApprovedList = value; NotifyPropertyChanged("EmployeeClaimApprovedList"); }
        }

        public List<DAT_EMPLOYEE_CLAIM> mEmployeeClaimRejectList;
        public List<DAT_EMPLOYEE_CLAIM> EmployeeClaimRejectList
        {
            get { return mEmployeeClaimRejectList; }
            set { mEmployeeClaimRejectList = value; NotifyPropertyChanged("EmployeeClaimRejectList"); }
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
                    mRefreshCommand = new Command(() => this.getEmployeeClaim());
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
        private void bindDataTab(List<DAT_EMPLOYEE_CLAIM> argDAT_EMPLOYEE_CLAIM_LST)
        {
            try
            {
                List<DAT_EMPLOYEE_CLAIM> l_DAT_EMPLOYEE_CLAIM_SUBMIT = new List<DAT_EMPLOYEE_CLAIM>();
                List<DAT_EMPLOYEE_CLAIM> l_DAT_EMPLOYEE_CLAIM_APPROVE = new List<DAT_EMPLOYEE_CLAIM>();
                List<DAT_EMPLOYEE_CLAIM> l_DAT_EMPLOYEE_CLAIM_REJECT = new List<DAT_EMPLOYEE_CLAIM>();

                if (argDAT_EMPLOYEE_CLAIM_LST != null && argDAT_EMPLOYEE_CLAIM_LST.Count > 0)
                {
                    foreach (DAT_EMPLOYEE_CLAIM l_DAT_EMPLOYEE_CLAIM in argDAT_EMPLOYEE_CLAIM_LST)
                    {  
                        l_DAT_EMPLOYEE_CLAIM.ClaimDate = Utility.getDateTimeString(l_DAT_EMPLOYEE_CLAIM.ClaimDate);
                       // l_DAT_EMPLOYEE_CLAIM.ClaimAmount = Utility.getAmountString(l_DAT_EMPLOYEE_CLAIM.ClaimAmount);

                        if (l_DAT_EMPLOYEE_CLAIM.ApproveStatusAsk.Equals("1"))
                        {
                            l_DAT_EMPLOYEE_CLAIM_SUBMIT.Add(l_DAT_EMPLOYEE_CLAIM);
                        }
                        else if (l_DAT_EMPLOYEE_CLAIM.ApproveStatusAsk.Equals("2"))
                        {
                            l_DAT_EMPLOYEE_CLAIM_APPROVE.Add(l_DAT_EMPLOYEE_CLAIM);
                        }
                        else if (l_DAT_EMPLOYEE_CLAIM.ApproveStatusAsk.Equals("3"))
                        {
                            l_DAT_EMPLOYEE_CLAIM_REJECT.Add(l_DAT_EMPLOYEE_CLAIM);
                        }

                    }

                    DAT_EMPLOYEE_CLAIM = argDAT_EMPLOYEE_CLAIM_LST[0];
                    EmployeeClaimList = argDAT_EMPLOYEE_CLAIM_LST;
                    EmployeeClaimSubmitList = l_DAT_EMPLOYEE_CLAIM_SUBMIT;
                    EmployeeClaimApprovedList = l_DAT_EMPLOYEE_CLAIM_APPROVE;
                    EmployeeClaimRejectList = l_DAT_EMPLOYEE_CLAIM_REJECT;
                }
                else
                {
                    EmployeeClaimList = new List<DAT_EMPLOYEE_CLAIM>();
                    EmployeeClaimSubmitList = new List<DAT_EMPLOYEE_CLAIM>();
                    EmployeeClaimApprovedList = new List<DAT_EMPLOYEE_CLAIM>();
                    EmployeeClaimRejectList = new List<DAT_EMPLOYEE_CLAIM>();
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
                List<DAT_EMPLOYEE_CLAIM> l_DAT_EMPLOYEE_CLAIM_lst = new List<DAT_EMPLOYEE_CLAIM>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_EMPLOYEE_CLAIM l_DAT_EMPLOYEE_CLAIM in mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_EMPLOYEE_CLAIM.EmployeeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_CLAIM.ClaimsTypeName.ToLower().Contains(argKeyword)
                            || l_DAT_EMPLOYEE_CLAIM.ClaimAmount.ToLower().Contains(argKeyword))
                        {
                            l_DAT_EMPLOYEE_CLAIM_lst.Add(l_DAT_EMPLOYEE_CLAIM);
                        }
                    }
                }
                else
                {
                    l_DAT_EMPLOYEE_CLAIM_lst = mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM;// OriginalEmployeeClaimList.GetRange(0, OriginalEmployeeClaimList.Count);
                }
                bindDataTab(l_DAT_EMPLOYEE_CLAIM_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public  int getIndexByTypeAsk(List<DAT_EMPLOYEE> arrList)
        {
            
            int index = 0;
            for (int i = 0; i < arrList.Count; i++)  
            {
                   index = i;
                    break;
                
            }
            return index;
        }
        #endregion

        #region "Web Service Api"
        public async void getEmployeeClaim()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_CLAIM);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsgetEmployeeClaim);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_CLAIM = JsonConvert.DeserializeObject<JSN_EMPLOYEE_CLAIM>(mResponse);
                    if (mJSN_EMPLOYEE_CLAIM.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Count > 0)
                        {
                            bindDataTab(this.mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_CLAIM.Message.Message);
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

        public async void saveEmployeeClaim()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_EMPLOYEE_CLAIM);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wssaveEmployeeClaim);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_EMPLOYEE_CLAIM = JsonConvert.DeserializeObject<JSN_EMPLOYEE_CLAIM>(mResponse);
                    if (mJSN_EMPLOYEE_CLAIM.Message.Code == "7")
                    {
                        if (this.mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM.Count > 0)
                        {
                            DAT_EMPLOYEE_CLAIM = this.mJSN_EMPLOYEE_CLAIM.DAT_EMPLOYEE_CLAIM[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_EMPLOYEE_CLAIM.Message.Message);
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

        public async void loadClaim()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Hcm_Service.ApiCall(mRequest, Hcm_Name.wsloadEmployeeClaim);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_EMPLOYEE_CLAIM = JsonConvert.DeserializeObject<JSN_LOAD_EMPLOYEE_CLAIM>(mResponse);
                    if (mJSN_LOAD_EMPLOYEE_CLAIM.Message.Code == "7")
                    {                        
                        this.ClaimLoad = mJSN_LOAD_EMPLOYEE_CLAIM;
                        this.EmployeeRo = ClaimLoad.DAT_EMPLOYEE_RO;
                        this.ClaimType = ClaimLoad.DAT_CLAIMS_TYPE;                       
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_EMPLOYEE_CLAIM.Message.Message);
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
