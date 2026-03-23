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
using CS.ERP_MOB.Services.ECO;
using CS.ERP_MOB.Views.ECO;
using CS.ERP.PL.ECO.RES;
using CS.ERP.PL.ECO.REQ;
using CS.ERP.PL.ECO.DAT;

namespace CS.ERP_MOB.ViewsModel.ECO
{
    public class VmlShoppingCart : BaseViewModel
    {
        #region "Declaring"
        string mRequest = "";
        string mResponse = "";
        
        public JSN_REQ_SHOPPING mJSN_REQ_SHOPPING = new JSN_REQ_SHOPPING();
        public JSN_SHOPPING mJSN_SHOPPING = new JSN_SHOPPING();
        public JSN_LOAD_SALE_INVOICE mJSN_LOAD_SALE_INVOICE = new JSN_LOAD_SALE_INVOICE();
        public List<RES_SHOPPING> mRES_SHOPPING_LST = new List<RES_SHOPPING>();
        public ObservableCollection<RES_SHOPPING> ShoppingLst { get; set; }
        public ObservableCollection<SortingItem> sortingList { get; set; }
        SortingItem[] labelTexts = [
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("ECO.MyShopping.lbl.Code"), value = "ShoppingCode_0_50", ShowIcon = true },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("ECO.MyShopping.lbl.Date"), value = "ShoppingDate", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("ECO.MyShopping.lbl.SalesPerson"), value = "SalePersonName_0_255", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("ECO.MyShopping.lbl.Status"), value = "StatusName_0_255", ShowIcon = false },
            new SortingItem{ label = Common.mCommon.GetLanguageValueByKey("ECO.MyShopping.lbl.Total"), value = "GrandTotal", ShowIcon = false} 
            ];

        #endregion

        #region "Contructor"
        public VmlShoppingCart()
        {
            this.switchDisplayView(DisplayView.Card);
            SalesInvoiceLoad = new JSN_LOAD_SALE_INVOICE();
            ShoppingLst = new ObservableCollection<RES_SHOPPING>();
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
        public JSN_LOAD_SALE_INVOICE JSN_LOAD_SALE_INVOICE = new JSN_LOAD_SALE_INVOICE();
        public JSN_LOAD_SALE_INVOICE SalesInvoiceLoad
        {
            get { return JSN_LOAD_SALE_INVOICE; }
            set { JSN_LOAD_SALE_INVOICE = value; NotifyPropertyChanged("SalesInvoiceLoad"); }
        }


        //public RES_SALE_BROWSE mRES_SALE_BROWSE = new RES_SALE_BROWSE();
        //public RES_SALE_BROWSE RES_SALE_BROWSE
        //{
        //    get { return mRES_SALE_BROWSE; }
        //    set { mRES_SALE_BROWSE = value; NotifyPropertyChanged("RES_SALE_BROWSE"); }
        //}

        //public RES_SALE_INVOICE mRES_SALE_INVOICE = new RES_SALE_INVOICE();
        //public RES_SALE_INVOICE RES_SALE_INVOICE
        //{
        //    get { return mRES_SALE_INVOICE; }
        //    set { mRES_SALE_INVOICE = value; NotifyPropertyChanged("RES_SALE_INVOICE"); }
        //}

        //public RES_SALE_INVOICE_DETAIL mRES_SALE_INVOICE_DETAIL = new RES_SALE_INVOICE_DETAIL();
        //public RES_SALE_INVOICE_DETAIL RES_SALE_INVOICE_DETAIL
        //{
        //    get { return mRES_SALE_INVOICE_DETAIL; }
        //    set { mRES_SALE_INVOICE_DETAIL = value; NotifyPropertyChanged("RES_SALE_INVOICE_DETAIL"); }
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
                        //    this.getInvoice();
                        //}
                        mJSN_REQ_SHOPPING.RES_SHOPPING = new RES_SHOPPING();
                        mJSN_REQ_SHOPPING.RES_SHOPPING.Sequence = "0";
                        this.getShoppingCart();
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
                    mEditItemCommand = new Command<RES_SALE_INVOICE>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Edit"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.InvoiceCode_0_50}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                            } 
                        }
                    });
                        //mEditItemCommand = new Command(() => this.switchDisplayView(DisplayView.Grid));
                    //mRefreshCommand = new Command(() => this.getInvoice());
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
                    mDeleteItemCommand = new Command<RES_SHOPPING>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Delete") && item.StatusAsk != "9")
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.ShoppingCode_0_50}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                                mJSN_REQ_SHOPPING.RES_SHOPPING = item;
                                mJSN_REQ_SHOPPING.RES_SHOPPING.StatusAsk = "6";
                                saveShoppingCart();
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
                    //mRefreshCommand = new Command(() => this.getInvoice());
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
                    mSendItemCommand = new Command<RES_SALE_INVOICE>(async (item) =>
                    {
                        if (Utility.checkButtonAccess("Send"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.InvoiceCode_0_50}",
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
                    mActiveItemCommand = new Command<RES_SHOPPING>(async (item) =>
                    {
                        if (item.StatusAsk == "8" && Utility.checkButtonAccess("Active"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.ShoppingCode_0_50}?",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Active")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                                item.StatusAsk = "1";//1 for active
                                mJSN_REQ_SHOPPING.RES_SHOPPING = item;
                                await ExecuteActiveItem();
                            }
                        }
                        else if (item.StatusAsk != "8" && Utility.checkButtonAccess("Inactive"))
                        {
                            bool answer = await Application.Current.MainPage.DisplayAlert(
                                $"{item.ShoppingCode_0_50}?",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Inactive")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                                $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                            if (answer)
                            {
                               item.StatusAsk = "8";//8 for inactive
                               mJSN_REQ_SHOPPING.RES_SHOPPING = item;
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
                    mCardItemTappedCommand = new Command<RES_SALE_INVOICE>(async (item) =>
                    {
                        bool answer = await Application.Current.MainPage.DisplayAlert(
                               $"{item.InvoiceCode_0_50}?",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Active")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes")}",
                               $"{Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No")}");

                        if (answer)
                        {
                            //await Navigation.PushAsync(new FrmPosSaleInvoiceSet(item));

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
            getShoppingCart();
            IsLoadingMore = false;
        }
        private Task ExecuteActiveItem()
        {
            saveShoppingCart();
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
    
                var tmp = ShoppingLst;
                ShoppingLst = null;
                NotifyPropertyChanged(nameof(ShoppingLst));

                ShoppingLst = tmp;
                NotifyPropertyChanged(nameof(ShoppingLst));
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void bindDataTab(List<RES_SHOPPING> argRES_SHOPPING)
        {
            try
            {
                if (argRES_SHOPPING != null && argRES_SHOPPING.Count > 0)
                {
                    foreach (RES_SHOPPING l_RES_SHOPPING in argRES_SHOPPING)
                    {
                        ShoppingLst.Add(l_RES_SHOPPING);
                    }
                }
                else
                {
                    ShoppingLst = new ObservableCollection<RES_SHOPPING>();
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
                mJSN_REQ_SHOPPING.RES_SHOPPING = new RES_SHOPPING();
                mJSN_REQ_SHOPPING.RES_SHOPPING.Remark = argKeyword;
                getShoppingCart();
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
                List<RES_SHOPPING> l_RES_SHOPPING_Lst = new List<RES_SHOPPING>();
                if (argKeyword != null && !argKeyword.Equals(""))
                {
                    foreach (RES_SHOPPING l_RES_SHOPPING in mJSN_SHOPPING.RES_SHOPPING)
                    {
                        argKeyword = argKeyword.ToLower();
                        if (l_RES_SHOPPING.ShoppingCode_0_50.ToLower().Contains(argKeyword)
                            || l_RES_SHOPPING.ShoppingDate.ToLower().Contains(argKeyword)
                            || l_RES_SHOPPING.Subtotal.ToLower().Contains(argKeyword)
                            || l_RES_SHOPPING.GrandTotal.ToLower().Contains(argKeyword))
                        {
                            l_RES_SHOPPING_Lst.Add(l_RES_SHOPPING);
                        }
                    }
                }
                else
                {
                    l_RES_SHOPPING_Lst = new List<RES_SHOPPING>(mJSN_SHOPPING.RES_SHOPPING);// OriginalInvoiceClosedList.GetRange(0, OriginalInvoiceClosedList.Count);
                }
                bindDataTab(l_RES_SHOPPING_Lst);
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
                loadInvoice();
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
                var popup = new FrmShoppingCartPop(this.SalesInvoiceLoad);
                await PopupNavigation.Instance.PushAsync(popup);

                var result = await popup.PopupClosedTask;
                if (result is RES_SHOPPING selectedData)
                {
                    mJSN_REQ_SHOPPING.RES_SHOPPING = selectedData;
                    if(Common.mCommon.UserSetting.TLSearchTypeAsk == "1")//1 for local search
                    {
                        ShoppingLst = new ObservableCollection<RES_SHOPPING>(mRES_SHOPPING_LST.Where(data =>(data.CustomerAsk == selectedData.CustomerAsk)
                                                                               || (data.ShoppingCode_0_50 == selectedData.ShoppingCode_0_50)).ToList());
                    }
                    else
                    {
                        getShoppingCart();
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
        public async void getShoppingCart()
        {
            try
            {
                Utility.openLoader();
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SHOPPING);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsgetShopping);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_SHOPPING = JsonConvert.DeserializeObject<JSN_SHOPPING>(mResponse);
                    if (this.mJSN_SHOPPING.Message.Code == "7")
                    {
                        if (this.mJSN_SHOPPING.RES_SHOPPING.Count > 0)
                        {
                            mRES_SHOPPING_LST = this.mJSN_SHOPPING.RES_SHOPPING;
                            bindDataTab(this.mJSN_SHOPPING.RES_SHOPPING);
                            WeakReferenceMessenger.Default.Send(this.mJSN_SHOPPING.Message.Message);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(this.mJSN_SHOPPING.Message.Message);
                        }
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_SHOPPING.Message.Message);
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

        public async void saveShoppingCart()
        {
            try
            {
                //mRequest = JsonConvert.SerializeObject(mJSN_REQ_WISHLIST);
                //mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wssaveSaleInvoice);
                //if (mResponse != null && mResponse != "")
                //{
                //    this.mJSN_RES_WISHLIST = JsonConvert.DeserializeObject<JSN_RES_WISHLIST>(mResponse);
                //    if (mJSN_RES_WISHLIST.Message.Code == "7")
                //    {
                //        mJSN_REQ_WISHLIST.DAT_WISHLIST = new DAT_WISHLIST();
                //        getWishlist();
                //    }
                //    else
                //    {
                //        WeakReferenceMessenger.Default.Send(this.mJSN_RES_WISHLIST.Message.Message);
                //    }
                //}
                //else
                //{
                //    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void loadInvoice()
        {
            try
            {
                Utility.openLoader();
                mRequest = JsonConvert.SerializeObject(Common.mCommon.REQ_AUTHORIZATION);
                mResponse = await Pos_Service.ApiCall(mRequest, Pos_Name.wsLoadSaleInvoice);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_LOAD_SALE_INVOICE = JsonConvert.DeserializeObject<JSN_LOAD_SALE_INVOICE>(mResponse);
                    if (mJSN_LOAD_SALE_INVOICE.Message.Code == "7")
                    {
                        Utility.closeLoader();
                        this.SalesInvoiceLoad = mJSN_LOAD_SALE_INVOICE;
                        callSearchMorePopup();
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_LOAD_SALE_INVOICE.Message.Message);
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
