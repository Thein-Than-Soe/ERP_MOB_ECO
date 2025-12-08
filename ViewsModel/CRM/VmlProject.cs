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
    public class VmlProject : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_PROJECT_JUN mJSN_REQ_PROJECT_JUN = new JSN_REQ_PROJECT_JUN();
        public JSN_RES_PROJECT_JUN mJSN_RES_PROJECT_JUN = new JSN_RES_PROJECT_JUN();
        public JSN_LOAD_PROJECT_JUN mJSN_LOAD_PROJECT_JUN = new JSN_LOAD_PROJECT_JUN();
        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlProject()
        {
            this.switchDisplayView(DisplayView.Card);
            ProjectLoad = new JSN_LOAD_PROJECT_JUN();
            ProjectActiveList = new List<DAT_PROJECT>();
            ProjectInActiveList = new List<DAT_PROJECT>();
            ProjectList = new List<DAT_PROJECT>();
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
        public JSN_LOAD_PROJECT_JUN JSN_LOAD_PROJECT_JUN = new JSN_LOAD_PROJECT_JUN();
        public JSN_LOAD_PROJECT_JUN ProjectLoad
        {
            get { return JSN_LOAD_PROJECT_JUN; }
            set { JSN_LOAD_PROJECT_JUN = value; NotifyPropertyChanged("ProjectLoad"); }
        }


        public DAT_PROJECT mDAT_PROJECT = new DAT_PROJECT();
        public DAT_PROJECT DAT_PROJECT
        {
            get { return mDAT_PROJECT; }
            set { mDAT_PROJECT = value; NotifyPropertyChanged("DAT_PROJECT"); }
        }

        public List<DAT_PROJECT> mProjectList;
        public List<DAT_PROJECT> ProjectList
        {
            get { return mProjectList; }
            set { mProjectList = value; NotifyPropertyChanged("ProjectList"); }
        }

        public List<DAT_PROJECT> mProjectActiveList;
        public List<DAT_PROJECT> ProjectActiveList
        {
            get { return mProjectActiveList; }
            set { mProjectActiveList = value; NotifyPropertyChanged("ProjectActiveList"); }
        }

        public List<DAT_PROJECT> mProjectInActiveList;
        public List<DAT_PROJECT> ProjectInActiveList
        {
            get { return mProjectInActiveList; }
            set { mProjectInActiveList = value; NotifyPropertyChanged("ProjectInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getProject());
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
        private void bindDataTab(List<DAT_PROJECT> argDAT_PROJECT_LST)
        {
            try
            {
                List<DAT_PROJECT> l_DAT_PROJECT_ACTIVE = new List<DAT_PROJECT>();
                List<DAT_PROJECT> l_DAT_PROJECT_INACTIVE = new List<DAT_PROJECT>();

                if (argDAT_PROJECT_LST != null && argDAT_PROJECT_LST.Count > 0)
                {

                    foreach (DAT_PROJECT l_DAT_PROJECT in argDAT_PROJECT_LST)
                    {
                        l_DAT_PROJECT.SD = Utility.getDateTimeString(l_DAT_PROJECT.SD);
                        l_DAT_PROJECT.ED = Utility.getDateTimeString(l_DAT_PROJECT.ED);

                        if (l_DAT_PROJECT.StatusAsk.Equals("1"))
                        {
                            l_DAT_PROJECT_ACTIVE.Add(l_DAT_PROJECT);
                        }

                        else if (l_DAT_PROJECT.StatusAsk.Equals("8"))
                        {
                            l_DAT_PROJECT_INACTIVE.Add(l_DAT_PROJECT);
                        }
                    }

                    DAT_PROJECT = argDAT_PROJECT_LST[0];
                    ProjectList = argDAT_PROJECT_LST;
                    ProjectActiveList = l_DAT_PROJECT_ACTIVE;
                    ProjectInActiveList = l_DAT_PROJECT_INACTIVE;

                }
                else
                {
                    ProjectList = new List<DAT_PROJECT>();
                    ProjectActiveList = new List<DAT_PROJECT>();
                    ProjectInActiveList = new List<DAT_PROJECT>();

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
                List<DAT_PROJECT> l_DAT_PROJECT_lst = new List<DAT_PROJECT>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (DAT_PROJECT l_DAT_PROJECT in mJSN_RES_PROJECT_JUN.DAT_PROJECT)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_DAT_PROJECT.ProjectName.ToLower().Contains(argKeyword)
                            || l_DAT_PROJECT.CustomerName.ToLower().Contains(argKeyword)
                            || l_DAT_PROJECT.IndustrialTypeName.ToLower().Contains(argKeyword))
                        {
                            l_DAT_PROJECT_lst.Add(l_DAT_PROJECT);
                        }
                    }
                }
                else
                {
                    l_DAT_PROJECT_lst = mJSN_RES_PROJECT_JUN.DAT_PROJECT;// OriginalProjectList.GetRange(0, OriginalProjectList.Count);
                }
                bindDataTab(l_DAT_PROJECT_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getProject()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PROJECT_JUN);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsgetProjectJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_PROJECT_JUN = JsonConvert.DeserializeObject<JSN_RES_PROJECT_JUN>(mResponse);
                    if (mJSN_RES_PROJECT_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_RES_PROJECT_JUN.DAT_PROJECT.Count > 0)
                        {
                            bindDataTab(this.mJSN_RES_PROJECT_JUN.DAT_PROJECT);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_PROJECT_JUN.Message.Message);
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

        public async void saveProject()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_PROJECT_JUN);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wssaveProjectJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_RES_PROJECT_JUN = JsonConvert.DeserializeObject<JSN_RES_PROJECT_JUN>(mResponse);
                    if (mJSN_RES_PROJECT_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_RES_PROJECT_JUN.DAT_PROJECT.Count > 0)
                        {
                            DAT_PROJECT = this.mJSN_RES_PROJECT_JUN.DAT_PROJECT[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_RES_PROJECT_JUN.Message.Message);
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

        public async void LoadProject()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Crm_Service.ApiCall(mRequest, Crm_Name.wsloadProjectJun);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_PROJECT_JUN = JsonConvert.DeserializeObject<JSN_LOAD_PROJECT_JUN>(mResponse);
                    if (mJSN_LOAD_PROJECT_JUN.Message.Code == "7")
                    {
                        this.ProjectLoad = mJSN_LOAD_PROJECT_JUN;

                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_PROJECT_JUN.Message.Message);
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
