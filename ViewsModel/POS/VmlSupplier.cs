using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.POS
{
    public class VmlSupplier : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_SUPPLIER mJSN_REQ_SUPPLIER = new JSN_REQ_SUPPLIER();
        public JSN_SUPPLIERNCONTACT mJSN_SUPPLIERNCONTACT = new JSN_SUPPLIERNCONTACT();
        public JSN_LOAD_SUPPLIER mJSN_LOAD_SUPPLIER = new JSN_LOAD_SUPPLIER();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlSupplier()
        {
            this.switchDisplayView(DisplayView.Card);
            SupplierLoad = new JSN_LOAD_SUPPLIER();
            //Supplier = new RES_SUPPLIER();
            SupplierList = new List<RES_SUPPLIER>();
            SupplierActiveList = new List<RES_SUPPLIER>();
            SupplierInActiveList = new List<RES_SUPPLIER>();
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
        public JSN_LOAD_SUPPLIER JSN_LOAD_SUPPLIER = new JSN_LOAD_SUPPLIER();
        public JSN_LOAD_SUPPLIER SupplierLoad
        {
            get { return JSN_LOAD_SUPPLIER; }
            set { JSN_LOAD_SUPPLIER = value; NotifyPropertyChanged("SupplierLoad"); }
        }

        public RES_SUPPLIER mRES_SUPPLIER = new RES_SUPPLIER();
        public RES_SUPPLIER RES_SUPPLIER
        {
            get { return mRES_SUPPLIER; }
            set { mRES_SUPPLIER = value; NotifyPropertyChanged("RES_SUPPLIER"); }
        }

        public List<RES_SUPPLIER> mSupplierList;
        public List<RES_SUPPLIER> SupplierList
        {
            get { return mSupplierList; }
            set { mSupplierList = value; NotifyPropertyChanged("SupplierList"); }
        }

        public List<RES_SUPPLIER> mSupplierActiveList;
        public List<RES_SUPPLIER> SupplierActiveList
        {
            get { return mSupplierActiveList; }
            set { mSupplierActiveList = value; NotifyPropertyChanged("SupplierActiveList"); }
        }

        public List<RES_SUPPLIER> mSupplierInActiveList;
        public List<RES_SUPPLIER> SupplierInActiveList
        {
            get { return mSupplierInActiveList; }
            set { mSupplierInActiveList = value; NotifyPropertyChanged("SupplierInActiveList"); }
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
                    mRefreshCommand = new Command(() => this.getSupplier());
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
        private void bindDataTab(List<RES_SUPPLIER> argRES_SUPPLIER_LST)
        {
            try
            {
                List<RES_SUPPLIER> l_RES_SUPPLIER_ACTIVE = new List<RES_SUPPLIER>();
                List<RES_SUPPLIER> l_RES_SUPPLIER_InACTIVE = new List<RES_SUPPLIER>();
                if (argRES_SUPPLIER_LST != null && argRES_SUPPLIER_LST.Count > 0)
                {

                    foreach (RES_SUPPLIER l_RES_SUPPLIER in argRES_SUPPLIER_LST)
                    {
                        if (l_RES_SUPPLIER.StatusAsk.Equals("1"))
                        {
                            l_RES_SUPPLIER_ACTIVE.Add(l_RES_SUPPLIER);
                        }
                        else if (l_RES_SUPPLIER.StatusAsk.Equals("8"))
                        {
                            l_RES_SUPPLIER_InACTIVE.Add(l_RES_SUPPLIER);
                        }
                    }

                    RES_SUPPLIER = argRES_SUPPLIER_LST[0];
                    SupplierList = argRES_SUPPLIER_LST;
                    SupplierActiveList = l_RES_SUPPLIER_ACTIVE;
                    SupplierInActiveList = l_RES_SUPPLIER_InACTIVE;
                }
                else
                {
                    SupplierList = new List<RES_SUPPLIER>();
                    SupplierActiveList = new List<RES_SUPPLIER>();
                    SupplierInActiveList = new List<RES_SUPPLIER>();
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
                List<RES_SUPPLIER> l_RES_SUPPLIER_lst = new List<RES_SUPPLIER>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SUPPLIER l_RES_SUPPLIER in mJSN_SUPPLIERNCONTACT.RES_SUPPLIER)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SUPPLIER.SupplierEmail.ToLower().Contains(argKeyword)
                            || l_RES_SUPPLIER.SupplierCode.ToLower().Contains(argKeyword)
                            || l_RES_SUPPLIER.SupplierMobilePhone.ToLower().Contains(argKeyword))
                        {
                            l_RES_SUPPLIER_lst.Add(l_RES_SUPPLIER);
                        }
                    }
                }
                else
                {
                    l_RES_SUPPLIER_lst = mJSN_SUPPLIERNCONTACT.RES_SUPPLIER;// OriginalSupplierList.GetRange(0, OriginalSupplierList.Count);
                }
                bindDataTab(l_RES_SUPPLIER_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getSupplier()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SUPPLIER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSupplierAndContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SUPPLIERNCONTACT = JsonConvert.DeserializeObject<JSN_SUPPLIERNCONTACT>(mResponse);
                    if (mJSN_SUPPLIERNCONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_SUPPLIERNCONTACT.RES_SUPPLIER.Count > 0)
                        {
                            bindDataTab(this.mJSN_SUPPLIERNCONTACT.RES_SUPPLIER);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SUPPLIERNCONTACT.Message.Message);
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

        public async void saveSupplier()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SUPPLIER);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveSupplierContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_SUPPLIERNCONTACT = JsonConvert.DeserializeObject<JSN_SUPPLIERNCONTACT>(mResponse);
                    if (mJSN_SUPPLIERNCONTACT.Message.Code == "7")
                    {
                        if (this.mJSN_SUPPLIERNCONTACT.RES_SUPPLIER.Count > 0)
                        {
                            RES_SUPPLIER = this.mJSN_SUPPLIERNCONTACT.RES_SUPPLIER[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_SUPPLIERNCONTACT.Message.Message);
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

        public async void loadSupplier()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSupplier);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_SUPPLIER = JsonConvert.DeserializeObject<JSN_LOAD_SUPPLIER>(mResponse);
                    if (mJSN_LOAD_SUPPLIER.Message.Code == "7")
                    {
                        this.SupplierLoad = mJSN_LOAD_SUPPLIER;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_SUPPLIER.Message.Message);
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
