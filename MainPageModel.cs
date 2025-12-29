using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Models;
using CS.ERP_MOB.Models.Frame;
using CS.ERP_MOB.Services.ACC;
using CS.ERP_MOB.Services.ATT;
using CS.ERP_MOB.Services.CRM;
using CS.ERP_MOB.Services.HCM;
using CS.ERP_MOB.Services.PAY;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.Services.SYS;
//using CS.ERP_MOB.Services.WMS;
using CS.ERP_MOB.Views.Frame;
using System.Windows.Input;
using FreshMvvm.Maui;
using CommunityToolkit.Mvvm.Messaging;
using RGPopup.Maui.Extensions;
using RGPopup.Maui.Services;
namespace CS.ERP_MOB
{
    public class MainPageModel: FreshBasePageModel
    {
        #region "Constructor"
        public MainPageModel()
        {
            try
            {
                this.getApiConfig();
                Title = "Home";
                HomeSelected = true;
                PublicAccess = true;

                Common.mCommon.FormAlert = false;
                Common.mCommon.ApplicationAlert = false;

                Common.mCommon.UserLoggedOut = true;
                Common.mCommon.LiveChat = true;
                Common.mCommon.UserLoggedIn = false;

                Common.mCommon.SelectedProduct = new RES_PRODUCT();
                
                //listen toast message change
                WeakReferenceMessenger.Default.Register<Application, string>(Application.Current, (recipient, message) =>
                {
                    startToastTimer(message, 3);

                });

                //listen route change
                WeakReferenceMessenger.Default.Register<Application, ModelRoute>(Application.Current, (recipient, routemodel) =>
                {
                    // Handle the received message and update the UI
                    this.changeContentView(routemodel.Page, routemodel.Title);
                });

                //listen login , logout
                MessagingCenter.Subscribe<Application, bool>(Application.Current, "AccessChange", (sender, islogedin) =>
                {
                    if (islogedin)
                    {
                        this.PublicAccess = false;
                        this.PrivateAccess = true;
                    }
                    else
                    {
                        this.PublicAccess = true;
                        this.PrivateAccess = false;
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region Private Properties
        private ContentView? mContentView;
        private string mTitle = "";
        private bool mPublicAccess = false;
        private bool mPrivateAccess = false;
        private bool mHomeSelected = false;
        private bool mProductSelected = false;
        private bool mNotiSelected = false;
        private bool mDiscussionSelected = false;
        private bool mSettingselected = false;

        private ICommand? mHomeCommand;
        private ICommand? mProductCommand;
        private ICommand? mNotiCommand;
        private ICommand? mDiscussionCommand;
        private ICommand? mSettingCommand;
        private ICommand? mMenuCommand;
        private ICommand? mCheckoutCommand;
        private ICommand? mUserProfileCommand;
        private ICommand? mCloseToastCommand;
        #endregion

        #region Public Properties
        public ContentView CvWorkingArea
        {
            get { return mContentView; }
            set
            {
                mContentView = value;
                RaisePropertyChanged("CvWorkingArea");
            }
        }
        public string Title
        {
            get { return mTitle; }
            set
            {
                mTitle = value;
                RaisePropertyChanged("Title");
            }
        }
        public bool PublicAccess
        {
            get { return mPublicAccess; }
            set
            {
                mPublicAccess = value;
                RaisePropertyChanged("IsPublicAccess");
            }
        }
        public bool PrivateAccess
        {
            get { return mPrivateAccess; }
            set
            {
                mPrivateAccess = value;
                RaisePropertyChanged("IsPrivateAccess");
            }
        }
        public bool HomeSelected
        {
            get { return mHomeSelected; }
            set
            {
                mHomeSelected = value;
                RaisePropertyChanged("HomeSelected");
            }
        }
        public bool ProductSelected
        {
            get { return mProductSelected; }
            set
            {
                mProductSelected = value;
                RaisePropertyChanged("ProductSelected");
            }
        }
        public bool NotiSelected
        {
            get { return mNotiSelected; }
            set
            {
                mNotiSelected = value;
                RaisePropertyChanged("NotiSelected");
            }
        }
        public bool DiscussionSelected
        {
            get { return mDiscussionSelected; }
            set
            {
                mDiscussionSelected = value;
                RaisePropertyChanged("DiscussionSelected");
            }
        }
        public bool Settingselected
        {
            get { return mSettingselected; }
            set
            {
                mSettingselected = value;
                RaisePropertyChanged("Settingselected");
            }
        }
        #endregion
        #region Commands
        public ICommand HomeCommand
        {
            get
            {
                if (mHomeCommand == null)
               {
                    //mHomeCommand = new Command(() => this.SelectTab("home"));
                    //mHomeCommand = new Command(() => changeContentView(new FrmSignUp(), "Sign up"));
                    mHomeCommand = new Command(() => this.selectApplicationTab("home"));
                }
                return mHomeCommand;
            }
        }
        public ICommand ProductCommand
        {
            get
            {
                if (mProductCommand == null)
                {
                    mProductCommand = new Command(() => this.selectApplicationTab("product"));
                }
                return mProductCommand;
            }
        }
        public ICommand NotiCommand
        {
            get
            {
                if (mNotiCommand == null)
                {
                    mNotiCommand = new Command(() => this.selectApplicationTab("noti"));
                }
                return mNotiCommand;
            }
        }
        public ICommand DiscussionCommand
        {
            get
            {
                if (mDiscussionCommand == null)
                {
                    mDiscussionCommand = new Command(() => this.selectApplicationTab("discussion"));
                }
                return mDiscussionCommand;
            }
        }
        public ICommand SettingCommand
        {
            get
            {
                if (mSettingCommand == null)
                {
                    mSettingCommand = new Command(() => this.selectApplicationTab("setting"));
                }
                return mSettingCommand;
            }
        }




        public ICommand MenuCommand
        {
            get
            {
                if (mMenuCommand == null)
                {
                    mMenuCommand = new Command(async () => await PopupNavigation.Instance.PushAsync(new MenuPage()));
                }
                return mMenuCommand;
            }
        }
        public ICommand CheckoutCommand
        {
            get
            {
                if (mCheckoutCommand == null)
                {
                    mCheckoutCommand = new Command(async () => await this.showCheckout());
                }
                return mCheckoutCommand;
            }
        }
        public ICommand UserProfileCommand
        {
            get
            {
                if (mUserProfileCommand == null)
                {
                    mUserProfileCommand = new Command(() => this.showUserProfile());
                }
                return mUserProfileCommand;
            }
        }
        public ICommand CloseToastCommand
        {
            get
            {
                if (mCloseToastCommand == null)
                {
                    mCloseToastCommand = new Command(() => this.closeToastMessage());
                }
                return mCloseToastCommand;
            }
        }

        #endregion Commands
        #region Private Methods

        private async void startToastTimer(string message, int seconds)
        {
            Common.mCommon.FormAlertMessage = message;
            Common.mCommon.FormAlert = true;
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            Common.mCommon.FormAlert = false;
        }

        //private Task showMenu()
        //{
        //    try
        //    {
        //        //await Application.Current.MainPage.Navigation.PushPopupAsync(new MenuPage());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }

        //    return Task.CompletedTask;
        //}

        private Task showCheckout()
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushPopupAsync(new PopCheckOut());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return Task.CompletedTask;
        }

        private async void showUserProfile()
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushPopupAsync(new PopUserProfile());
                await PopupNavigation.Instance.PushAsync(new PopUserProfile());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void closeToastMessage()
        {
            try
            {
                Common.mCommon.FormAlert = false;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void changeContentView(ContentView argContentView, string argTitle)
        {
            try
            {
                this.Title =argTitle;
                this.CvWorkingArea = argContentView;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private async void selectApplicationTab(string tabName)
        {
            try
            {
                string lowercaseName = tabName.ToLower();

                HomeSelected = lowercaseName == "home";
                ProductSelected = lowercaseName == "product";
                NotiSelected = lowercaseName == "noti";
                DiscussionSelected = lowercaseName == "discussion";
                Settingselected = lowercaseName == "setting";

                if (ProductSelected)
                {
                    //await this.showProductList();
                    await PopupNavigation.Instance.PushAsync(new PopProduct());
                }
                else if (HomeSelected)
                {
                    if (Common.mCommon.CompanyUserList.Count == 1)
                    {
                        this.changeContentView(new HomePage(), "Home");
                    }
                    else
                    {
                        await PopupNavigation.Instance.PushAsync(new PopCompany());
                    }
                }
                else if (NotiSelected)
                {
                    //await this.showNotiList();
                    await PopupNavigation.Instance.PushAsync(new PopNoti());
                }
                else if (DiscussionSelected)
                {
                    //await this.showDiscussionList();
                    await PopupNavigation.Instance.PushAsync(new PopDiscussion());
                }
                else
                {
                    //this.showSettingList();
                    await PopupNavigation.Instance.PushAsync(new PopSetting());
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }



           
        }
        public async void getApiConfig()
        {
            try
            {
                List<ApiConfig> l_ApiConfig_Lst =  await App.Database.getApiConfigAsync();
                if (l_ApiConfig_Lst.Count>0)
                {
                   
                    foreach (ApiConfig l_apiConfig in l_ApiConfig_Lst)
                    {
                      await App.Database.deleteApiConfigAsync(l_apiConfig);
                        switch (l_apiConfig.ProductCode)
                        {
                            case "SYS":
                                Sys_Service.mApiConfig = l_apiConfig;
                                break;
                            case "POS":
                                Pos_Service.mApiConfig = l_apiConfig;
                                break;
                            case "HCM":
                                Hcm_Service.mApiConfig = l_apiConfig;
                                break;
                            case "ATT":
                                Att_Service.mApiConfig = l_apiConfig;
                                break;
                            case "PAY":
                                Pay_Service.mApiConfig = l_apiConfig;
                                break;
                            case "ACC":
                                Acc_Service.mApiConfig = l_apiConfig;
                                break;
                            case "CRM":
                                Crm_Service.mApiConfig = l_apiConfig;
                                break;
                            //case "WMS":
                            //    Wms_Service.mApiConfig = l_apiConfig;
                            //    break;
                        }
                    }
                }
                else
                {
                    if(Data_ApiConfig.mApiConfig_Lst.Count>0)
                    {
                        foreach (ApiConfig l_ApiConfig in Data_ApiConfig.mApiConfig_Lst)
                        {
                           await App.Database.saveApiConfigAsync(l_ApiConfig);
                        }
                    }

                    //go to apiconfig setup
                    if (!Common.bindMenu("FrmApiConfigLst"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Server Configuration", MenuUrl = "FrmApiConfigLst", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("FrmApiConfigLst"))
                    //{
                    //    Common.routeMenu(Route.Sys_Route.DicRouteList, Common.mCommon.SelectedMenu);
                    //}
                    //else
                    //{
                    //    //remove it after add in menu access for sign in and sign up
                    //    Common.mCommon.SelectedMenu = new RES_MENU();
                    //    Common.mCommon.SelectedMenu.MenuUrl = "FrmApiConfigLst";
                    //    Common.mCommon.SelectedMenu.BadgeText ="Api Config";
                    //    Common.mCommon.SelectedMenu.logoImg = "";
                    //    //Common.routeMenu("signin", "Sign In");
                    //    Common.routeMenu(Route.Sys_Route.DicRouteList, Common.mCommon.SelectedMenu);
                    //}
                }
            }
            catch (Exception ex)
            {
                //await Utility.closeLoader();
                throw ex.InnerException;
            }
        }

        public static implicit operator Page(MainPageModel v)
        {
            throw new NotImplementedException();
        }
        #endregion







        // old



        #region Private member
        //private ContentView _DynamicView;

        //private bool _HomeSelected;

        //private bool _ProductSelected;

        //private bool _NotiSelected;

        //private bool _DiscussionSelected;

        //private bool _MoreSelected;
        #endregion

        #region Bound Properties

        //private string _title;
        //public string Title { 
        //    get { return _title; }
        //    set { 
        //        _title = value;
        //        RaisePropertyChanged("Title"); 
        //    }
        //}


        //private bool _isPublicAccess;
        //public bool IsPublicAccess
        //{
        //    get { return _isPublicAccess; }
        //    set
        //    {
        //        _isPublicAccess = value;
        //        RaisePropertyChanged("IsPublicAccess");
        //    }
        //}

        //private bool _isPrivateAccess;
        //public bool IsPrivateAccess
        //{
        //    get { return _isPrivateAccess; }
        //    set
        //    {
        //        _isPrivateAccess = value;
        //        RaisePropertyChanged("IsPrivateAccess");
        //    }
        //}

        //public ContentView DynamicView { 
        //    get { return _DynamicView; }
        //    set
        //    {
        //        _DynamicView = value;
        //        RaisePropertyChanged("DynamicView");
        //    } 
        //}

        //public bool HomeSelected { 
        //    get { return _HomeSelected; }
        //    set { 
        //        _HomeSelected = value;
        //        RaisePropertyChanged("HomeSelected"); 
        //    } 
        //}
        //public bool ProductSelected { 
        //    get { return _ProductSelected; }
        //    set
        //    {
        //        _ProductSelected = value;
        //        RaisePropertyChanged("ProductSelected");
        //    }
        //}
        //public bool NotiSelected { 
        //    get { return _NotiSelected; }
        //    set
        //    {
        //        _NotiSelected = value;
        //        RaisePropertyChanged("NotiSelected");
        //    }
        //}
        //public bool DiscussionSelected { 
        //    get { return _DiscussionSelected; }
        //    set
        //    {
        //        _DiscussionSelected = value;
        //        RaisePropertyChanged("DiscussionSelected");
        //    }
        //}
        //public bool MoreSelected { 
        //    get { return _MoreSelected; }
        //    set
        //    {
        //        _MoreSelected = value;
        //        RaisePropertyChanged("MoreSelected");
        //    }
        //}


        #endregion Bound Properties

        #region Commands

        //private ICommand _HomeCommand;
        //public ICommand HomeCommand
        //{
        //    get
        //    {
        //        if (_HomeCommand == null)
        //        {
        //            _HomeCommand = new Command(() => this.SelectTab("home"));
        //        }
        //        return _HomeCommand;
        //    }
        //}

        //private ICommand _ProductCommand;
        //public ICommand ProductCommand
        //{
        //    get
        //    {
        //        if (_ProductCommand == null)
        //        {
        //            _ProductCommand = new Command(() => this.SelectTab("product"));
        //        }
        //        return _ProductCommand;
        //    }
        //}

        //private ICommand _NotiCommand;
        //public ICommand NotiCommand
        //{
        //    get
        //    {
        //        if (_NotiCommand == null)
        //        {
        //            _NotiCommand = new Command(() => this.SelectTab("noti"));
        //        }
        //        return _NotiCommand;
        //    }
        //}

        //private ICommand _DiscussionCommand;
        //public ICommand DiscussionCommand
        //{
        //    get
        //    {
        //        if (_DiscussionCommand == null)
        //        {
        //            _DiscussionCommand = new Command(() => this.SelectTab("discussion"));
        //        }
        //        return _DiscussionCommand;
        //    }
        //}

        //private ICommand _MoreCommand;
        //public ICommand MoreCommand
        //{
        //    get
        //    {
        //        if (_MoreCommand == null)
        //        {
        //            _MoreCommand = new Command(() => this.SelectTab("more"));
        //        }
        //        return _MoreCommand;
        //    }
        //}


        //private ICommand _MenuCommand;
        //public ICommand MenuCommand
        //{
        //    get
        //    {
        //        if (_MenuCommand == null)
        //        {
        //            _MenuCommand = new Command(async () => await this.ShowMenu());
        //        }
        //        return _MenuCommand;
        //    }
        //}

        //private ICommand _CheckoutCommand;
        //public ICommand CheckoutCommand
        //{
        //    get
        //    {
        //        if (_CheckoutCommand == null)
        //        {
        //            _CheckoutCommand = new Command(async () => await this.ShowCheckout());
        //        }
        //        return _CheckoutCommand;
        //    }
        //}

        //private ICommand _UserProfileCommand;
        //public ICommand UserProfileCommand
        //{
        //    get
        //    {
        //        if (_UserProfileCommand == null)
        //        {
        //            _UserProfileCommand = new Command(async () => await this.ShowUserProfile());
        //        }
        //        return _UserProfileCommand;
        //    }
        //}

        //private ICommand _CloseToastCommand;
        //public ICommand CloseToastCommand
        //{
        //    get
        //    {
        //        if (_CloseToastCommand == null)
        //        {
        //            _CloseToastCommand = new Command(() => this.CloseToastMessage());
        //        }
        //        return _CloseToastCommand;
        //    }
        //}

        #endregion Commands

        #region Private Methods

        //private async void SelectTab(string tabName)
        //{
        //    string lowercaseName = tabName.ToLower();

        //    HomeSelected = lowercaseName == "home";
        //    ProductSelected = lowercaseName == "product";
        //    NotiSelected = lowercaseName == "noti";
        //    DiscussionSelected = lowercaseName == "discussion";
        //    MoreSelected = lowercaseName == "more";

        //    if (ProductSelected)
        //    {
        //        await this.ShowProductList();
        //    }else if(HomeSelected)
        //    {
        //        this.ChangeView(new HomePage(),"Home");
        //    }else if(NotiSelected)
        //    {
        //        await this.ShowNotiList();
        //    }else if(DiscussionSelected)
        //    {
        //        await this.ShowDiscussionList();
        //    }else
        //    {
        //        await this.ShowSettingList();
        //    }

        //}

        //private async Task ShowMenu()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new MenuPage());
        //}

        //private async Task ShowCheckout()
        //{
        //    //await Application.Current.MainPage.Navigation.PushPopupAsync(new CheckoutPage());
        //}

        //private async Task ShowUserProfile()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new UserProfilePopup());
        //}

        //private async Task ShowProductList()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new ProductListPage());
        //}

        //private async Task ShowNotiList()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new NotiPageList());
        //}

        //private async Task ShowDiscussionList()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new DiscussionPageList());
        //}

        //private async Task ShowSettingList()
        //{
        //    await Application.Current.MainPage.Navigation.PushPopupAsync(new SettingPageList());
        //}

        //private void CloseToastMessage()
        //{
        //    IntercomService.Current.IsToastAlertMessage = false;
        //}


        //private void ChangeView(ContentView view,string title)
        //{
        //    Title = title;
        //    DynamicView = view;
        //}

        #endregion Private Methods
    }
}
