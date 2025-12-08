using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;

using System.Windows.Input;
using Microsoft.Maui.Controls;
using static CS.ERP_MOB.General.Utility;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP_MOB.Views.POS;
using System.Diagnostics;
using RGPopup.Maui.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CS.ERP_MOB.ViewsModel.POS
{
    public class VmlSalesQuotation : BaseViewModel
    {
        #region "Declaring"
        string mRequest = "";
        string mResponse = "";

        public JSN_REQ_SALE_QUOTATION_JUN mJSN_REQ_SALE_QUOTATION_JUN = new JSN_REQ_SALE_QUOTATION_JUN();
        public JSN_SALE_QUOTATION_JUN mJSN_SALE_QUOTATION_JUN = new JSN_SALE_QUOTATION_JUN();
        public JSN_LOAD_SALE_QUOTATION mJSN_LOAD_SALE_QUOTATION = new JSN_LOAD_SALE_QUOTATION();
        public List<RES_SALE_QUOTATION> mRES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
        public ObservableCollection<RES_SALE_QUOTATION> SalesQuotationList { get; set; }
        public ObservableCollection<SortingItem> sortingList { get; set; }
        SortingItem[] labelTexts = [
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("POS.SalesQuotationJunOva.lbl.QuotationDate"), value = "QuotationDate", ShowIcon = true },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("POS.SalesQuotationJunOva.lbl.QuotationNo"), value = "QuotationCode_0_50", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("POS.SalesQuotationJunOva.lbl.Customer"), value = "CustomerName_0_255", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("POS.SalesQuotationJunOva.lbl.Status"), value = "StatusName_0_255", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("POS.SalesQuotationJunOva.lbl.Price"), value = "GrandTotal", ShowIcon = false} 
            ];

        #endregion

        #region "Contructor"
        public VmlSalesQuotation()
        {
            this.switchDisplayView(DisplayView.Card);
            SalesQuotationLoad = new JSN_LOAD_SALE_QUOTATION();
            SalesQuotationList = new ObservableCollection<RES_SALE_QUOTATION>();
            LoadMoreCommand = new Command(async () => await LoadMoreItems());
            sortingList = new ObservableCollection<SortingItem>(labelTexts);
            IsAscending = true;
            IsDescending = false;
        }
        #endregion

        #region "Boolean Declaring"
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
                if (value) // Only when refreshing starts
                {
                    IsRefreshing = false;
                    
                }
            }
        }
        private bool mIsAscending;
        public bool IsAscending{
            get
            {
                return mIsAscending;
            }
            set
            {
                mIsAscending = value;
                NotifyPropertyChanged("IsAscending");
            }
        }
        private bool mIsDescending;
        public bool IsDescending
        {
            get
            {
                return mIsDescending;
            }
            set
            {
                mIsDescending = value;
                NotifyPropertyChanged("IsDescending");
            }
        }

        private bool isLoadingMore = false;
        public bool IsLoadingMore
        {
            get => isLoadingMore;
            set
            {
                isLoadingMore = value;
                NotifyPropertyChanged(nameof(IsLoadingMore));
            }
        }
        #endregion

        #region "Get Set"
        public JSN_LOAD_SALE_QUOTATION JSN_LOAD_SALE_QUOTATION = new JSN_LOAD_SALE_QUOTATION();
        public JSN_LOAD_SALE_QUOTATION SalesQuotationLoad
        {
            get { return JSN_LOAD_SALE_QUOTATION; }
            set { JSN_LOAD_SALE_QUOTATION = value; NotifyPropertyChanged("SalesQuotationLoad"); }
        }


        //public RES_SALE_BROWSE mRES_SALE_BROWSE = new RES_SALE_BROWSE();
        //public RES_SALE_BROWSE RES_SALE_BROWSE
        //{
        //    get { return mRES_SALE_BROWSE; }
        //    set { mRES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_BROWSE"); }
        //}

        //public RES_SALE_QUOTATION mRES_SALE_QUOTATION = new RES_SALE_QUOTATION();
        //public RES_SALE_QUOTATION RES_SALE_QUOTATION
        //{
        //    get { return mRES_SALE_QUOTATION; }
        //    set { mRES_SALE_QUOTATION = value; NotifyPropertyChanged("RES_SALE_QUOTATION"); }
        //}

        //public RES_SALE_QUOTATION_DETAIL mRES_SALE_QUOTATION_DETAIL = new RES_SALE_QUOTATION_DETAIL();
        //public RES_SALE_QUOTATION_DETAIL RES_SALE_QUOTATION_DETAIL
        //{
        //    get { return mRES_SALE_QUOTATION_DETAIL; }
        //    set { mRES_SALE_QUOTATION_DETAIL = value; NotifyPropertyChanged("RES_SALE_QUOTATION_DETAIL"); }
        //}

        //public RES_COMPANY mRES_COMPANY = new RES_COMPANY();
        //public RES_COMPANY RES_COMPANY
        //{
        //    get { return mRES_COMPANY; }
        //    set { mRES_COMPANY = value; NotifyPropertyChanged("RES_COMPANY"); }
        //}

        public List<RES_CUSTOMER_DTL> mCustomerDtlList;
        public List<RES_CUSTOMER_DTL> CustomerDtlList
        {
            get { return mCustomerDtlList; }
            set { mCustomerDtlList = value; NotifyPropertyChanged("CustomerDtlList"); }
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
                    mRefreshCommand = new Command(() => {
                        //if (Common.mCommon.UserSetting.TLSearchTypeAsk == "1")//1 for local search
                        //{

                        //}
                        //else
                        //{
                        //    this.getQuotation();
                        //}
                        mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = new RES_SALE_QUOTATION();
                        mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.Sequence = "0";
                        this.getQuotation();
                    });
                }
                return mRefreshCommand;
            }
        }
        private ICommand mEditItemCommand;
        public ICommand EditItemCommand
        {
            get
            {
                if (mEditItemCommand == null)
                {
                    mEditItemCommand = new Command<RES_SALE_QUOTATION>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Edit"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.QuotationCode_0_50}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                            } 
                        }
                    });
                        //mEditItemCommand = new Command(() => this.switchDisplayView(DisplayView.Grid));
                    //mRefreshCommand = new Command(() => this.getQuotation());
                }
                return mEditItemCommand;
            }
        }
        private ICommand mDeleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get
            {
                if (mDeleteItemCommand == null)
                {
                    mDeleteItemCommand = new Command<RES_SALE_QUOTATION>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Delete") && item.PostingStatusAsk != "1" && item.StatusAsk != "9")
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.QuotationCode_0_50}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                                mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = item;
                                mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.StatusAsk = "6";
                                saveQuotation();
                            }
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgDelete"));
                        }
                    });
                }
                return mDeleteItemCommand;
            }
        }
        private ICommand mSelectItemCommand;
        public ICommand SelectItemCommand
        {
            get
            {
                if (mSelectItemCommand == null)
                {
                    //mRefreshCommand = new Command(() => this.getQuotation());
                }
                return mSelectItemCommand;
            }
        }
        private ICommand mSendItemCommand;
        public ICommand SendItemCommand
        {
            get
            {
                if (mSendItemCommand == null)
                {
                    mSendItemCommand = new Command<RES_SALE_QUOTATION>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Send"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.QuotationCode_0_50}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                            }
                        }
                    });
                }
                return mSendItemCommand;
            }
        }
        private ICommand mActiveItemCommand;
        public ICommand ActiveItemCommand
        {
            get
            {               
                if (mActiveItemCommand == null)
                {
                    mActiveItemCommand = new Command<RES_SALE_QUOTATION>(async (item) =>
                    {
                        if (item.StatusAsk == "8" && Utility.checkButtonAccess("Active"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.QuotationCode_0_50}?",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Active")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                                item.StatusAsk = "1";//1 for active
                                mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = item;
                                await ExecuteActiveItem();
                            }
                        }
                        else if (item.StatusAsk != "8" && Utility.checkButtonAccess("Inactive"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.QuotationCode_0_50}?",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Inactive")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                               item.StatusAsk = "8";//8 for inactive
                               mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = item;
                               await ExecuteActiveItem();
                            }
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                        }
                    });
                }
                return mActiveItemCommand;
            }
        }
        public ICommand LongPressItemCommand { get; }

        private ICommand mCardItemTappedCommand;
        public  ICommand CardItemTappedCommand
        {
            get
            {
                if (mCardItemTappedCommand == null)
                {
                    mCardItemTappedCommand = new Command<RES_SALE_QUOTATION>(async (item) =>
                    {
                        bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.QuotationCode_0_50}?",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Active")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                        if (answer)
                        {
                            //await Navigation.PushAsync(new FrmPosSaleQuotationSet(item));

                        }
                    });
                }
                return mCardItemTappedCommand;
            }
        }
        private ICommand mMoreSearchCommand;
        public ICommand MoreSearchCommand
        {
            get
            {
                if (mMoreSearchCommand == null)
                {
                    mMoreSearchCommand = new Command(() => this.selectMoreSearch());
                }
                return mMoreSearchCommand;
            }
        }
        public ICommand LoadMoreCommand { get; }
        #endregion

        #region "Task"
        private async Task LoadMoreItems()
        {
            if (IsLoadingMore) return;
            IsLoadingMore = true;
            getQuotation();
            IsLoadingMore = false;
        }
        private Task ExecuteActiveItem()
        {
            saveQuotation();
            return Task.CompletedTask;
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
    
                var tmp = SalesQuotationList;
                SalesQuotationList = null;
                NotifyPropertyChanged(nameof(SalesQuotationList));

                SalesQuotationList = tmp;
                NotifyPropertyChanged(nameof(SalesQuotationList));
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void bindDataTab(List<RES_SALE_QUOTATION> argRES_SALE_QUOTATION_LST)
        {
            try
            {
                if (argRES_SALE_QUOTATION_LST != null && argRES_SALE_QUOTATION_LST.Count > 0)
                {
                    foreach (RES_SALE_QUOTATION l_RES_SALE_QUOTATION in argRES_SALE_QUOTATION_LST)
                    {
                        SalesQuotationList.Add(l_RES_SALE_QUOTATION);
                    }
                }
                else
                {
                    SalesQuotationList = new ObservableCollection<RES_SALE_QUOTATION>();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void searchDataApi(string argKeyword)
        {
            try
            {
                mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = new RES_SALE_QUOTATION();
                mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.Remark = argKeyword;
                getQuotation();
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
                List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_Lst = new List<RES_SALE_QUOTATION>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SALE_QUOTATION l_RES_SALE_QUOTATION in mJSN_SALE_QUOTATION_JUN.RES_SALE_QUOTATION)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SALE_QUOTATION.QuotationCode_0_50.ToLower().Contains(argKeyword)
                            || l_RES_SALE_QUOTATION.QuotationDate.ToLower().Contains(argKeyword)
                            || l_RES_SALE_QUOTATION.OutstandingAmount.ToLower().Contains(argKeyword)
                            || l_RES_SALE_QUOTATION.GrandTotal.ToLower().Contains(argKeyword))
                        {
                            l_RES_SALE_QUOTATION_Lst.Add(l_RES_SALE_QUOTATION);
                        }
                    }
                }
                else
                {
                    l_RES_SALE_QUOTATION_Lst = new List<RES_SALE_QUOTATION>(mJSN_SALE_QUOTATION_JUN.RES_SALE_QUOTATION);// OriginalQuotationClosedList.GetRange(0, OriginalQuotationClosedList.Count);
                }
                bindDataTab(l_RES_SALE_QUOTATION_Lst);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void selectMoreSearch()
        {
            try
            {
                loadQuotation();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void callSearchMorePopup()
        {
            try
            {
                var popup = new FrmPosSaleQuotationPop(this.SalesQuotationLoad);
                await PopupNavigation.Instance.PushAsync(popup);

                var result = await popup.PopupClosedTask;
                if (result is RES_SALE_QUOTATION selectedData)
                {
                    mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = selectedData;
                    if(Common.mCommon.UserSetting.TLSearchTypeAsk == "1")//1 for local search
                    {
                        SalesQuotationList = new ObservableCollection<RES_SALE_QUOTATION>(mRES_SALE_QUOTATION_LST.Where(data =>(data.CustomerAsk == selectedData.CustomerAsk)
                                                                               || (data.QuotationCode_0_50 == selectedData.QuotationCode_0_50)).ToList());
                    }
                    else
                    {
                        getQuotation();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void bindCustomer(List<RES_CUSTOMER_DTL> argRES_CUSTOMER_DTL_LST)
        {
            try
            {
                if (argRES_CUSTOMER_DTL_LST != null && argRES_CUSTOMER_DTL_LST.Count > 0)
                {
                    CustomerDtlList = argRES_CUSTOMER_DTL_LST;
                }
                else
                {
                    CustomerDtlList = new List<RES_CUSTOMER_DTL>();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async void getQuotation()
        {
            try
            {
                Utility.openLoader();
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_QUOTATION_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsgetSaleQuotationJun);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_SALE_QUOTATION_JUN = JsonConvert.DeserializeObject<JSN_SALE_QUOTATION_JUN>(mResponse);
                    if (this.mJSN_SALE_QUOTATION_JUN.Message.Code == "7")
                    {
                        if (this.mJSN_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.Count > 0)
                        {
                            mRES_SALE_QUOTATION_LST = this.mJSN_SALE_QUOTATION_JUN.RES_SALE_QUOTATION;
                            bindDataTab(this.mJSN_SALE_QUOTATION_JUN.RES_SALE_QUOTATION);
                            WeakReferenceMessenger.Default.Send(this.mJSN_SALE_QUOTATION_JUN.Message.Message);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(this.mJSN_SALE_QUOTATION_JUN.Message.Message);
                        }
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_SALE_QUOTATION_JUN.Message.Message);
                    }

                    Utility.closeLoader();
                }
                else
                {
                    Utility.closeLoader();
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }

        public async void saveQuotation()
        {
            try
            {
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SALE_QUOTATION_JUN);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wssaveSaleQuotation);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_SALE_QUOTATION_JUN = JsonConvert.DeserializeObject<JSN_SALE_QUOTATION_JUN>(mResponse);
                    if (mJSN_SALE_QUOTATION_JUN.Message.Code == "7")
                    {
                        mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = new RES_SALE_QUOTATION();
                        getQuotation();
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_SALE_QUOTATION_JUN.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void loadQuotation()
        {
            try
            {
                Utility.openLoader();
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleQuotation);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_LOAD_SALE_QUOTATION = JsonConvert.DeserializeObject<JSN_LOAD_SALE_QUOTATION>(mResponse);
                    if (mJSN_LOAD_SALE_QUOTATION.Message.Code == "7")
                    {
                        Utility.closeLoader();
                        this.SalesQuotationLoad = mJSN_LOAD_SALE_QUOTATION;
                        callSearchMorePopup();
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_LOAD_SALE_QUOTATION.Message.Message);
                    }
                }
                else
                {
                    Utility.closeLoader();
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
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
