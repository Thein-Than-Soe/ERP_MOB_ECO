using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
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
    public class VmlStockDamage : BaseViewModel
    {
        #region "Declaring"
        public JSN_REQ_INVENTORY_DAMAGE mJSN_REQ_INVENTORY_DAMAGE = new JSN_REQ_INVENTORY_DAMAGE();
        public JSN_INVENTORY_DAMAGE mJSN_INVENTORY_DAMAGE = new JSN_INVENTORY_DAMAGE();
        public JSN_LOAD_INVENTRY_DAMAGE mJSN_LOAD_INVENTRY_DAMAGE = new JSN_LOAD_INVENTRY_DAMAGE();

        string mRequest = "";
        string mResponse = "";
        #endregion

        #region "Contructor"
        public VmlStockDamage()
        {
            this.switchDisplayView(DisplayView.Card);
            DamageLoad = new JSN_LOAD_INVENTRY_DAMAGE();
            DamageClosedList = new List<RES_INVENTORY_DAMAGE>();
            DamageActiveList = new List<RES_INVENTORY_DAMAGE>();
            DamagePartialList = new List<RES_INVENTORY_DAMAGE>();
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
        public JSN_LOAD_INVENTRY_DAMAGE JSN_LOAD_INVENTRY_DAMAGE = new JSN_LOAD_INVENTRY_DAMAGE();
        public JSN_LOAD_INVENTRY_DAMAGE DamageLoad
        {
            get { return JSN_LOAD_INVENTRY_DAMAGE; }
            set { JSN_LOAD_INVENTRY_DAMAGE = value; NotifyPropertyChanged("DamageLoad"); }
        }

        public RES_INVENTORY_DAMAGE mRES_INVENTORY_DAMAGE = new RES_INVENTORY_DAMAGE();

        public RES_COMPANY rES_COMPANY = new RES_COMPANY();
        public RES_COMPANY RES_COMPANY
        {
            get { return rES_COMPANY; }
            set { rES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        }

        public RES_INVENTORY_DAMAGE RES_INVENTORY_DAMAGE
        {
            get { return mRES_INVENTORY_DAMAGE; }
            set { mRES_INVENTORY_DAMAGE = value; NotifyPropertyChanged("RES_INVENTORY_DAMAGE"); }
        }



        public List<RES_INVENTORY_DAMAGE> mDamageClosedList;
        public List<RES_INVENTORY_DAMAGE> DamageClosedList
        {
            get { return mDamageClosedList; }
            set { mDamageClosedList = value; NotifyPropertyChanged("DamageClosedList"); }
        }

        public List<RES_INVENTORY_DAMAGE> mDamageActiveList;
        public List<RES_INVENTORY_DAMAGE> DamageActiveList
        {
            get { return mDamageActiveList; }
            set { mDamageActiveList = value; NotifyPropertyChanged("DamageActiveList"); }
        }

        public List<RES_INVENTORY_DAMAGE> mDamagePartialList;
        public List<RES_INVENTORY_DAMAGE> DamagePartialList
        {
            get { return mDamagePartialList; }
            set { mDamagePartialList = value; NotifyPropertyChanged("DamagePartialList"); }
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
                    mRefreshCommand = new Command(() => this.getStockDamage());
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
        private void bindDataTab(List<RES_INVENTORY_DAMAGE> argRES_INVENTORY_DAMAGE_LST)
        {
            try
            {
                List<RES_INVENTORY_DAMAGE> l_RES_INVENTORY_DAMAGE_ACTIVE = new List<RES_INVENTORY_DAMAGE>();
                List<RES_INVENTORY_DAMAGE> l_RES_INVENTORY_DAMAGE_PARTIAL = new List<RES_INVENTORY_DAMAGE>();
                List<RES_INVENTORY_DAMAGE> l_RES_INVENTORY_DAMAGE_CLOSED = new List<RES_INVENTORY_DAMAGE>();

                if (argRES_INVENTORY_DAMAGE_LST != null && argRES_INVENTORY_DAMAGE_LST.Count > 0)
                {
                    foreach (RES_INVENTORY_DAMAGE l_RES_INVENTORY_DAMAGE in argRES_INVENTORY_DAMAGE_LST)
                    {
                        l_RES_INVENTORY_DAMAGE.DamageDate = Utility.getDateTimeString(l_RES_INVENTORY_DAMAGE.DamageDate);


                        if (l_RES_INVENTORY_DAMAGE.StatusAsk == "1")
                        {
                            l_RES_INVENTORY_DAMAGE_ACTIVE.Add(l_RES_INVENTORY_DAMAGE);
                        }
                        else if (l_RES_INVENTORY_DAMAGE.StatusAsk == "8")
                        {
                            l_RES_INVENTORY_DAMAGE_PARTIAL.Add(l_RES_INVENTORY_DAMAGE);
                        }
                        else if (l_RES_INVENTORY_DAMAGE.StatusAsk == "3")
                        {
                            l_RES_INVENTORY_DAMAGE_CLOSED.Add(l_RES_INVENTORY_DAMAGE);
                        }
                    }
                    RES_INVENTORY_DAMAGE = argRES_INVENTORY_DAMAGE_LST[0];
                    DamageClosedList = l_RES_INVENTORY_DAMAGE_CLOSED;
                    DamageActiveList = l_RES_INVENTORY_DAMAGE_ACTIVE;
                    DamagePartialList = l_RES_INVENTORY_DAMAGE_PARTIAL;
                }
                else
                {
                    DamageClosedList = new List<RES_INVENTORY_DAMAGE>();
                    DamageActiveList = new List<RES_INVENTORY_DAMAGE>();
                    DamagePartialList = new List<RES_INVENTORY_DAMAGE>();
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
                List<RES_INVENTORY_DAMAGE> l_RES_INVENTORY_DAMAGE_lst = new List<RES_INVENTORY_DAMAGE>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_INVENTORY_DAMAGE l_RES_INVENTORY_DAMAGE in mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_INVENTORY_DAMAGE.DamageTypeName.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_DAMAGE.DamageCode.ToLower().Contains(argKeyword)
                            || l_RES_INVENTORY_DAMAGE.LocationName.ToLower().Contains(argKeyword))
                        {
                            l_RES_INVENTORY_DAMAGE_lst.Add(l_RES_INVENTORY_DAMAGE);
                        }
                    }
                }
                else
                {
                    l_RES_INVENTORY_DAMAGE_lst = mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE;// OriginalDamageClosedList.GetRange(0, OriginalDamageClosedList.Count);
                }
                bindDataTab(l_RES_INVENTORY_DAMAGE_lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getStockDamage()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_DAMAGE);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetInventryDamage);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_DAMAGE = JsonConvert.DeserializeObject<JSN_INVENTORY_DAMAGE>(mResponse);
                    if (mJSN_INVENTORY_DAMAGE.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE.Count > 0)
                        {
                            bindDataTab(this.mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE);
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.LoadSuccess);
                        }
                        else
                        {
                            MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.NoData);
                        }
                    }
                    else
                    {
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_DAMAGE.Message.Message);
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
        public async void saveStockDamage()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_DAMAGE);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveInventryDamage);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_INVENTORY_DAMAGE = JsonConvert.DeserializeObject<JSN_INVENTORY_DAMAGE>(mResponse);
                    if (mJSN_INVENTORY_DAMAGE.Message.Code == "7")
                    {
                        if (this.mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE.Count > 0)
                        {
                            RES_INVENTORY_DAMAGE = this.mJSN_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE[0];
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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_INVENTORY_DAMAGE.Message.Message);
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

        public async void loadStockDamage()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadInventryDamage);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_LOAD_INVENTRY_DAMAGE = JsonConvert.DeserializeObject<JSN_LOAD_INVENTRY_DAMAGE>(mResponse);
                    if (mJSN_LOAD_INVENTRY_DAMAGE.Message.Code == "7")
                    {
                        this.DamageLoad = mJSN_LOAD_INVENTRY_DAMAGE;

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
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, this.mJSN_LOAD_INVENTRY_DAMAGE.Message.Message);
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
