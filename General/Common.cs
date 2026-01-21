using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.Models;
using CS.ERP_MOB.Models.Frame;
using CS.ERP_MOB.Route;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.Services.SYS;
using Newtonsoft.Json;
//using Plugin.Connectivity;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using static SQLite.SQLite3;
using CS.ERP_MOB.Extensions;
using System.Text.Json.Nodes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CS.ERP.PL.ECO.DAT;
using CS.ERP_MOB.Services.ECO;
using CS.ERP.PL.ECO.RES;
using CS.ERP_MOB.Services.NTF;
using CS.ERP.PL.NTF.DAT;
using CS.ERP_MOB.Services.CHT;
using CS.ERP.PL.WSS.DAT;
using System.Diagnostics;
namespace CS.ERP_MOB.General
{
    public class Common : ObservableProperty, INotifyPropertyChanged
    {
        //public Common()
        //{            

        //}

        #region"Private Property"
        // Singleton
        public static Common mCommon = new Common();
        public List<DbUser> mDbUser_LST = new List<DbUser>();
        private RES_USER mRES_USER = new RES_USER();
        private DAT_USER_SETTING mDAT_USER_SETTING = new DAT_USER_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_ALERT = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_MENU = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_TASKBAR = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_CONTENT = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BUTTON = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_SWIPE = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_CARDVIEW = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_CARDVIEW_HEADER = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_CARDVIEW_BODY = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_CARDVIEW_FOOTER = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_HIGHLIGHT = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_SLIDER = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_SHELF = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_PROFILE = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_NOTI = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_SEARCH = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_AI = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_ORDER = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_SHOPPING_CART = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_BANNER_WISHLIST = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_COMPANY = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_MESSAGE = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_FLOAT_LEFT = new DAT_THEME_SETTING();
        private DAT_THEME_SETTING mDAT_THEME_SETTING_FLOAT_RIGHT = new DAT_THEME_SETTING();

        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_ALERT = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_MENU = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_TASKBAR = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_CONTENT = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BUTTON = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_SWIPE = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_CARDVIEW = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_CARDVIEW_HEADER = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_CARDVIEW_BODY = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_CARDVIEW_FOOTER = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_HIGHLIGHT = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_SLIDER = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_SHELF = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_PROFILE = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_NOTI = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_SEARCH = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_AI = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_ORDER = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_SHOPPING_CART = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_BANNER_WISHLIST = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_COMPANY = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_MESSAGE = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_FLOAT_LEFT = new DAT_REGION_SETTING();
        private DAT_REGION_SETTING mDAT_REGION_SETTING_FLOAT_RIGHT = new DAT_REGION_SETTING();

        //public static ObservableCollection<MenuGroup> mMenuList = new ObservableCollection<MenuGroup>();
        //private static  ObservableCollection<MenuGroup> menuGroups = new ObservableCollection<MenuGroup>();
        //public static ObservableCollection<MenuGroup> mMenuList = menuGroups;
        //public static ObservableCollection<MenuGroup> mMenuList = new ObservableCollection<MenuGroup>();
        public static ObservableCollection<ObservableProperty> mMenuList = new ObservableCollection<ObservableProperty>();
        public static ObservableCollection<RES_MENU> RES_MENU_LST = new ObservableCollection<RES_MENU>();
        private RES_PRODUCT mRES_PRODUCT = new RES_PRODUCT();
        private RES_MENU mRES_MENU = new RES_MENU();
        public REQ_AUTHORIZATION mREQ_AUTHORIZATION = new REQ_AUTHORIZATION();
        public RES_MESSAGE mRES_MESSAGE = new RES_MESSAGE();
        //public static JSN_PROFILE mJSN_PROFILE = new JSN_PROFILE();
        public static JSN_RES_ECOMANCE mJSN_RES_ECOMANCE = new JSN_RES_ECOMANCE();
        private JSN_SHOPPING mJSN_SHOPPING = new JSN_SHOPPING();
        private RES_SHOPPING mRES_SHOPPING = new RES_SHOPPING();
        private List<RES_SHOPPING_DETAIL> mRES_SHOPPING_DETAIL = new List<RES_SHOPPING_DETAIL>();

        private List<RES_COMPANY_USER> mRES_COMPANY_USER = new List<RES_COMPANY_USER>();
        private List<RES_COUNTRY> mRES_COUNTRY = new List<RES_COUNTRY>();
        private ObservableCollection<RES_NOTI_LST> mRES_NOTI_LST = new ObservableCollection<RES_NOTI_LST>();
        private ObservableCollection<DAT_DISCUSSION_NOTI> mRES_DISCUSSION_LST = new ObservableCollection<DAT_DISCUSSION_NOTI>();
        private ObservableCollection<DAT_FLOAT> mDAT_FLOAT_LEFT = new ObservableCollection<DAT_FLOAT>();
        private ObservableCollection<DAT_FLOAT> mDAT_FLOAT_RIGHT = new ObservableCollection<DAT_FLOAT>();
        private List<RES_PRODUCT> mRES_PRODUCT_LST = new List<RES_PRODUCT>();
        private List<RES_AD> mRES_AD_LST = new List<RES_AD>();
        private List<RES_PROMOTION> mRES_PROMOTION_LST = new List<RES_PROMOTION>();
        private List<RES_SEASONAL_STOCK> mRES_SEASONAL_STOCK = new List<RES_SEASONAL_STOCK>();
        private List<RES_STOCK_HOME> mRES_STOCK_HOME = new List<RES_STOCK_HOME>();
        private Thickness mCheckoutThickness = new Thickness(0, 10, 0, 0);

        JSN_REQ_COMPANY_USER mJSN_REQ_COMPANY_USER = new JSN_REQ_COMPANY_USER();
        JSN_COMPANY_USER mJSN_COMPANY_USER = new JSN_COMPANY_USER();

        private bool mApplicationAlert;
        private bool mLiveChat;
        private bool mFormAlert;
        private string mFormAlertMessage = "";
        private bool mUserLoggedIn;
        private bool mUserLoggedOut;
        private int count = 8;
        #endregion

        #region"Public Property"
        Cht_Service_WebSocket chtSocketService;
        Ntf_Service_WebSocket ntfSocketService;
        DAT_RES_NOTI mDAT_RES_NOTI = new DAT_RES_NOTI();
        DAT_RES_CHAT_NOTI mDAT_RES_CHAT_NOTI = new DAT_RES_CHAT_NOTI();
        RES_NOTI_LST mRES_NOTI_LST_DATA = new RES_NOTI_LST();
        JSN_REQ_USER mJSN_REQ_USER = new JSN_REQ_USER();
        public ObservableCollection<DAT_SHELF_PRODUCT> Items { get; set; }
        public ObservableCollection<DAT_SHELF> ShelfList { get; set; }
        public ObservableCollection<DAT_SLIDER> SliderList { get; set; }

        public JsonNode mLanguageData;

        public event PropertyChangedEventHandler? LangaugePropertyChanged;

        protected virtual void OnLanguagePropertyChanged([CallerMemberName] string propertyName = null)
        {
            LangaugePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public JsonNode LanguageData
        {
            get { return mLanguageData; }
            set
            {
                if (mLanguageData != value)
                {
                    mLanguageData = value;
                    OnLanguagePropertyChanged(nameof(LanguageData));
                }
            }
        }

        public void UpdateLanguage(string l_languageCode)
        {
            LoadLanguageAsync(l_languageCode);
            OnLanguagePropertyChanged(nameof(LanguageData));
        }

        public async void LoadLanguageAsync(string l_languageCode)
        {
            string l_fileName = $"Language_{l_languageCode}.json";

            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync(l_fileName);
                using var reader = new StreamReader(stream);
                string l_jsonContent = await reader.ReadToEndAsync();

                LanguageData = JsonNode.Parse(l_jsonContent);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetLanguageValueByKey(string l_jsonPath)
        {
            if (mLanguageData == null)
            {
                throw new Exception("Language data is not loaded. Call LoadLanguageAsync first.");
            }

            try
            {
                JsonNode current = mLanguageData;
                foreach (var segment in l_jsonPath.Split('.'))
                {
                    current = current?[segment];
                }

                return current?.ToString();
            }
            catch
            {
                return null;
            }
        }

        public JsonNode mMessageData;

        public void UpdateMessageManager(string l_languageCode)
        {
            LoadMessagesAsync(l_languageCode);
        }

        public async void LoadMessagesAsync(string l_languageCode)
        {
            // Build the file name based on the language code
            string l_fileName = $"Message_{l_languageCode}.json";

            try
            {
                // Open the asset file from the package
                using var stream = await FileSystem.OpenAppPackageFileAsync(l_fileName);
                using var reader = new StreamReader(stream);
                string l_jsonContent = reader.ReadToEnd();

                // Parse the JSON content into a JsonNode for dynamic access
                mMessageData = JsonNode.Parse(l_jsonContent);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetMessageValueByKey(string l_jsonPath)
        {
            try
            {
                if (mCommon.mMessageData == null)
                    throw new Exception("Message data is not loaded. Call LoadMessagesAsync first.");

                var current = mCommon.mMessageData;
                foreach (var segment in l_jsonPath.Split('.'))
                {
                    current = current?[segment];
                }

                return current?.ToString();
            }
            catch
            {
                return null; // Return null for missing paths
            }
        }

        public RES_USER User
        {
            get { return mRES_USER; }
            set
            {
                mRES_USER = value;
                OnPropertyChanged("User");
            }
        }

        public DAT_USER_SETTING UserSetting
        {
            get { return mDAT_USER_SETTING; }
            set
            {
                mDAT_USER_SETTING = value;
                OnPropertyChanged("User");
            }
        }
        #region "Theme Property"
        public DAT_THEME_SETTING ThemeSettingBanner
        {
            get { return mDAT_THEME_SETTING_BANNER; }
            set
            {
                mDAT_THEME_SETTING_BANNER = value;
                OnPropertyChanged("ThemeSettingBanner");
            }
        }
        public DAT_THEME_SETTING ThemeSettingAlert
        {
            get { return mDAT_THEME_SETTING_ALERT; }
            set
            {
                mDAT_THEME_SETTING_ALERT = value;
                OnPropertyChanged("ThemeSettingAlert");
            }
        }
        public DAT_THEME_SETTING ThemeSettingMenu
        {
            get { return mDAT_THEME_SETTING_MENU; }
            set
            {
                mDAT_THEME_SETTING_MENU = value;
                OnPropertyChanged("ThemeSettingMenu");
            }
        }
        public DAT_THEME_SETTING ThemeSettingTaskbar
        {
            get { return mDAT_THEME_SETTING_TASKBAR; }
            set
            {
                mDAT_THEME_SETTING_TASKBAR = value;
                OnPropertyChanged("ThemeSettingTaskbar");
            }
        }
        public DAT_THEME_SETTING ThemeSettingContent
        {
            get { return mDAT_THEME_SETTING_CONTENT; }
            set
            {
                mDAT_THEME_SETTING_CONTENT = value;
                OnPropertyChanged("ThemeSettingContent");
            }
        }

        public DAT_THEME_SETTING ThemeSettingButton
        {
            get { return mDAT_THEME_SETTING_BUTTON; }
            set
            {
                mDAT_THEME_SETTING_BUTTON = value;
                OnPropertyChanged("ThemeSettingButton");
            }
        }

        public DAT_THEME_SETTING ThemeSettingSwipe
        {
            get { return mDAT_THEME_SETTING_SWIPE; }
            set
            {
                mDAT_THEME_SETTING_SWIPE = value;
                OnPropertyChanged("ThemeSettingSwipe");
            }
        }

        public DAT_THEME_SETTING ThemeSettingCardView
        {
            get { return mDAT_THEME_SETTING_CARDVIEW; }
            set
            {
                mDAT_THEME_SETTING_CARDVIEW = value;
                OnPropertyChanged("ThemeSettingCardView");
            }
        }
        public DAT_THEME_SETTING ThemeSettingCardViewHeader
        {
            get { return mDAT_THEME_SETTING_CARDVIEW_HEADER; }
            set
            {
                mDAT_THEME_SETTING_CARDVIEW_HEADER = value;
                OnPropertyChanged("ThemeSettingCardViewHeader");
            }
        }
        public DAT_THEME_SETTING ThemeSettingCardViewBody
        {
            get { return mDAT_THEME_SETTING_CARDVIEW_BODY; }
            set
            {
                mDAT_THEME_SETTING_CARDVIEW_BODY = value;
                OnPropertyChanged("ThemeSettingCardViewBody");
            }
        }
        public DAT_THEME_SETTING ThemeSettingCardViewFooter
        {
            get { return mDAT_THEME_SETTING_CARDVIEW_FOOTER; }
            set
            {
                mDAT_THEME_SETTING_CARDVIEW_FOOTER = value;
                OnPropertyChanged("ThemeSettingCardViewFooter");
            }
        }
        public DAT_THEME_SETTING ThemeSettingHighlight
        {
            get { return mDAT_THEME_SETTING_HIGHLIGHT; }
            set
            {
                mDAT_THEME_SETTING_HIGHLIGHT = value;
                OnPropertyChanged("ThemeSettingHighlight");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerSearch
        {
            get { return mDAT_THEME_SETTING_BANNER_SEARCH; }
            set
            {
                mDAT_THEME_SETTING_BANNER_SEARCH = value;
                OnPropertyChanged("ThemeSettingBannerSearch");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerAI
        {
            get { return mDAT_THEME_SETTING_BANNER_AI; }
            set
            {
                mDAT_THEME_SETTING_BANNER_AI = value;
                OnPropertyChanged("ThemeSettingBannerAI");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerOrder
        {
            get { return mDAT_THEME_SETTING_BANNER_ORDER; }
            set
            {
                mDAT_THEME_SETTING_BANNER_ORDER = value;
                OnPropertyChanged("ThemeSettingBannerOrder");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerShoppingCart
        {
            get { return mDAT_THEME_SETTING_BANNER_SHOPPING_CART; }
            set
            {
                mDAT_THEME_SETTING_BANNER_SHOPPING_CART = value;
                OnPropertyChanged("ThemeSettingBannerShoppingCart");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerWishlist
        {
            get { return mDAT_THEME_SETTING_BANNER_WISHLIST; }
            set
            {
                mDAT_THEME_SETTING_BANNER_WISHLIST = value;
                OnPropertyChanged("ThemeSettingBannerWishlist");
            }
        }
        public DAT_THEME_SETTING ThemeSettingBannerProfile
        {
            get { return mDAT_THEME_SETTING_BANNER_PROFILE; }
            set
            {
                mDAT_THEME_SETTING_BANNER_PROFILE = value;
                OnPropertyChanged("ThemeSettingBannerProfile");
            }
        }
        public DAT_THEME_SETTING ThemeSettingCompany
        {
            get { return mDAT_THEME_SETTING_COMPANY; }
            set
            {
                mDAT_THEME_SETTING_COMPANY = value;
                OnPropertyChanged("ThemeSettingCompany");
            }
        }
        public DAT_THEME_SETTING ThemeSettingNoti
        {
            get { return mDAT_THEME_SETTING_NOTI; }
            set
            {
                mDAT_THEME_SETTING_NOTI = value;
                OnPropertyChanged("ThemeSettingNoti");
            }
        }
        public DAT_THEME_SETTING ThemeSettingMessage
        {
            get { return mDAT_THEME_SETTING_MESSAGE; }
            set
            {
                mDAT_THEME_SETTING_MESSAGE = value;
                OnPropertyChanged("ThemeSettingMessage");
            }
        }
        public DAT_THEME_SETTING ThemeSettingSlider
        {
            get { return mDAT_THEME_SETTING_SLIDER; }
            set
            {
                mDAT_THEME_SETTING_SLIDER = value;
                OnPropertyChanged("ThemeSettingSlider");
            }
        }
        public DAT_THEME_SETTING ThemeSettingShelf
        {
            get { return mDAT_THEME_SETTING_SLIDER; }
            set
            {
                mDAT_THEME_SETTING_SLIDER = value;
                OnPropertyChanged("ThemeSettingShelf");
            }
        }
        public DAT_THEME_SETTING ThemeSettingFloatLeft
        {
            get { return mDAT_THEME_SETTING_FLOAT_LEFT; }
            set
            {
                mDAT_THEME_SETTING_FLOAT_LEFT = value;
                OnPropertyChanged("ThemeSettingFloatLeft");
            }
        }
        public DAT_THEME_SETTING ThemeSettingFloatRight
        {
            get { return mDAT_THEME_SETTING_FLOAT_RIGHT; }
            set
            {
                mDAT_THEME_SETTING_FLOAT_RIGHT = value;
                OnPropertyChanged("ThemeSettingFloatRight");
            }
        }
        #endregion

        #region "Region Property"
        public DAT_REGION_SETTING RegionSettingBanner
        {
            get { return mDAT_REGION_SETTING_BANNER; }
            set
            {
                mDAT_REGION_SETTING_BANNER = value;
                OnPropertyChanged("RegionSettingBanner");
            }
        }
        public DAT_REGION_SETTING RegionSettingAlert
        {
            get { return mDAT_REGION_SETTING_ALERT; }
            set
            {
                mDAT_REGION_SETTING_ALERT = value;
                OnPropertyChanged("RegionSettingAlert");
            }
        }
        public DAT_REGION_SETTING RegionSettingMenu
        {
            get { return mDAT_REGION_SETTING_MENU; }
            set
            {
                mDAT_REGION_SETTING_MENU = value;
                OnPropertyChanged("RegionSettingMenu");
            }
        }
        public DAT_REGION_SETTING RegionSettingTaskbar
        {
            get { return mDAT_REGION_SETTING_TASKBAR; }
            set
            {
                mDAT_REGION_SETTING_TASKBAR = value;
                OnPropertyChanged("RegionSettingTaskbar");
            }
        }
        public DAT_REGION_SETTING RegionSettingContent
        {
            get { return mDAT_REGION_SETTING_CONTENT; }
            set
            {
                mDAT_REGION_SETTING_CONTENT = value;
                OnPropertyChanged("RegionSettingContent");
            }
        }

        public DAT_REGION_SETTING RegionSettingButton
        {
            get { return mDAT_REGION_SETTING_BUTTON; }
            set
            {
                mDAT_REGION_SETTING_BUTTON = value;
                OnPropertyChanged("RegionSettingButton");
            }
        }

        public DAT_REGION_SETTING RegionSettingSwipe
        {
            get { return mDAT_REGION_SETTING_SWIPE; }
            set
            {
                mDAT_REGION_SETTING_SWIPE = value;
                OnPropertyChanged("RegionSettingSwipe");
            }
        }

        public DAT_REGION_SETTING RegionSettingCardView
        {
            get { return mDAT_REGION_SETTING_CARDVIEW; }
            set
            {
                mDAT_REGION_SETTING_CARDVIEW = value;
                OnPropertyChanged("RegionSettingCardView");
            }
        }
        public DAT_REGION_SETTING RegionSettingCardViewHeader
        {
            get { return mDAT_REGION_SETTING_CARDVIEW_HEADER; }
            set
            {
                mDAT_REGION_SETTING_CARDVIEW_HEADER = value;
                OnPropertyChanged("RegionSettingCardViewHeader");
            }
        }
        public DAT_REGION_SETTING RegionSettingCardViewBody
        {
            get { return mDAT_REGION_SETTING_CARDVIEW_BODY; }
            set
            {
                mDAT_REGION_SETTING_CARDVIEW_BODY = value;
                OnPropertyChanged("RegionSettingCardViewBody");
            }
        }
        public DAT_REGION_SETTING RegionSettingCardViewFooter
        {
            get { return mDAT_REGION_SETTING_CARDVIEW_FOOTER; }
            set
            {
                mDAT_REGION_SETTING_CARDVIEW_FOOTER = value;
                OnPropertyChanged("RegionSettingCardViewFooter");
            }
        }
        public DAT_REGION_SETTING RegionSettingHighlight
        {
            get { return mDAT_REGION_SETTING_HIGHLIGHT; }
            set
            {
                mDAT_REGION_SETTING_HIGHLIGHT = value;
                OnPropertyChanged("RegionSettingHighlight");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerSearch
        {
            get { return mDAT_REGION_SETTING_BANNER_SEARCH; }
            set
            {
                mDAT_REGION_SETTING_BANNER_SEARCH = value;
                OnPropertyChanged("RegionSettingBannerSearch");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerAI
        {
            get { return mDAT_REGION_SETTING_BANNER_AI; }
            set
            {
                mDAT_REGION_SETTING_BANNER_AI = value;
                OnPropertyChanged("RegionSettingBannerAI");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerOrder
        {
            get { return mDAT_REGION_SETTING_BANNER_ORDER; }
            set
            {
                mDAT_REGION_SETTING_BANNER_ORDER = value;
                OnPropertyChanged("RegionSettingBannerOrder");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerShoppingCart
        {
            get { return mDAT_REGION_SETTING_BANNER_SHOPPING_CART; }
            set
            {
                mDAT_REGION_SETTING_BANNER_SHOPPING_CART = value;
                OnPropertyChanged("RegionSettingBannerShoppingCart");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerWishlist
        {
            get { return mDAT_REGION_SETTING_BANNER_WISHLIST; }
            set
            {
                mDAT_REGION_SETTING_BANNER_WISHLIST = value;
                OnPropertyChanged("RegionSettingBannerWishlist");
            }
        }
        public DAT_REGION_SETTING RegionSettingBannerProfile
        {
            get { return mDAT_REGION_SETTING_BANNER_PROFILE; }
            set
            {
                mDAT_REGION_SETTING_BANNER_PROFILE = value;
                OnPropertyChanged("RegionSettingBannerProfile");
            }
        }
        public DAT_REGION_SETTING RegionSettingCompany
        {
            get { return mDAT_REGION_SETTING_COMPANY; }
            set
            {
                mDAT_REGION_SETTING_COMPANY = value;
                OnPropertyChanged("RegionSettingCompany");
            }
        }
        public DAT_REGION_SETTING RegionSettingNoti
        {
            get { return mDAT_REGION_SETTING_NOTI; }
            set
            {
                mDAT_REGION_SETTING_NOTI = value;
                OnPropertyChanged("RegionSettingNoti");
            }
        }
        public DAT_REGION_SETTING RegionSettingMessage
        {
            get { return mDAT_REGION_SETTING_MESSAGE; }
            set
            {
                mDAT_REGION_SETTING_MESSAGE = value;
                OnPropertyChanged("RegionSettingMessage");
            }
        }
        public DAT_REGION_SETTING RegionSettingSlider
        {
            get { return mDAT_REGION_SETTING_SLIDER; }
            set
            {
                mDAT_REGION_SETTING_SLIDER = value;
                OnPropertyChanged("RegionSettingSlider");
            }
        }
        public DAT_REGION_SETTING RegionSettingShelf
        {
            get { return mDAT_REGION_SETTING_SHELF; }
            set
            {
                mDAT_REGION_SETTING_SHELF = value;
                OnPropertyChanged("RegionSettingShelf");
            }
        }
        public DAT_REGION_SETTING RegionSettingFloatLeft
        {
            get { return mDAT_REGION_SETTING_FLOAT_LEFT; }
            set
            {
                mDAT_REGION_SETTING_FLOAT_LEFT = value;
                OnPropertyChanged("RegionSettingFloatLeft");
            }
        }
        public DAT_REGION_SETTING RegionSettingFloatRight
        {
            get { return mDAT_REGION_SETTING_FLOAT_RIGHT; }
            set
            {
                mDAT_REGION_SETTING_FLOAT_RIGHT = value;
                OnPropertyChanged("RegionSettingFloatRight");
            }
        }
        #endregion

        public RES_PRODUCT SelectedProduct
        {
            get { return mRES_PRODUCT; }
            set
            {
                mRES_PRODUCT = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public RES_MENU SelectedMenu
        {
            get { return mRES_MENU; }
            set
            {
                mRES_MENU = value;
                OnPropertyChanged("SelectedMenu");
            }
        }

        public REQ_AUTHORIZATION REQ_AUTHORIZATION
        {
            get { return mREQ_AUTHORIZATION; }
            set
            {
                mREQ_AUTHORIZATION = value;
                OnPropertyChanged("SelectedMenu");
            }
        }
        public RES_MESSAGE ResMessage
        {
            get { return mRES_MESSAGE; }
            set
            {
                mRES_MESSAGE = value;
                OnPropertyChanged("ResMessage");
            }
        }
        public JSN_RES_ECOMANCE JSN_RES_ECOMANCE
        {
            get { return mJSN_RES_ECOMANCE; }
            set
            {
                mJSN_RES_ECOMANCE = value;
                OnPropertyChanged("SelectedMenu");
            }
        }

        public JSN_SHOPPING ShoppingCart
        {
            get { return mJSN_SHOPPING; }
            set
            {
                mJSN_SHOPPING = value;
                OnPropertyChanged("Shopping");
            }
        }
        public RES_SHOPPING Shopping
        {
            get { return mRES_SHOPPING; }
            set
            {
                mRES_SHOPPING = value;
                OnPropertyChanged("Shopping");
            }
        }
        public List<RES_SHOPPING_DETAIL> ShoppingCartList
        {
            get { return mRES_SHOPPING_DETAIL; }
            set
            {
                mRES_SHOPPING_DETAIL = value;
                OnPropertyChanged("ShoppingCartList");
            }
        }
        public List<RES_COMPANY_USER> CompanyUserList
        {
            get { return mRES_COMPANY_USER; }
            set
            {
                mRES_COMPANY_USER = value; 
                OnPropertyChanged(nameof(CompanyUserList));
                OnPropertyChanged(nameof(CompanyUserData));
            }
        }
        public RES_COMPANY_USER CompanyUserData
        {
            get
            {
                if (Common.mCommon.CompanyUserList == null || Common.mCommon.CompanyUserList.Count == 0)
                {

                    return new RES_COMPANY_USER(); // fallback text
                }

                return Common.mCommon.CompanyUserList[0];
            }
        }
        public List<RES_COUNTRY> CountryList
        {
            get { return mRES_COUNTRY; }
            set
            {
                mRES_COUNTRY = value;
                OnPropertyChanged(nameof(CountryList));
            }
        }
        public ObservableCollection<RES_NOTI_LST> NotiList
        {
            get { return mRES_NOTI_LST; }
            set
            {
                mRES_NOTI_LST = value;
                OnPropertyChanged("NotiList");
            }
        }
        public ObservableCollection<DAT_DISCUSSION_NOTI> DiscussionList
        {
            get { return mRES_DISCUSSION_LST; }
            set
            {
                mRES_DISCUSSION_LST = value;
                OnPropertyChanged("DiscussionList");
            }
        }
        public List<RES_PRODUCT> RES_PRODUCT_LST
        {
            get { return mRES_PRODUCT_LST; }
            set
            {
                mRES_PRODUCT_LST = value;
                OnPropertyChanged("DiscussionList");
            }
        }
        public List<RES_AD> RES_AD_LST
        {
            get { return mRES_AD_LST; }
            set
            {
                mRES_AD_LST = value;
                OnPropertyChanged("ADList");
            }
        }
        public List<RES_PROMOTION> RES_PROMOTION_LST
        {
            get { return mRES_PROMOTION_LST; }
            set
            {
                mRES_PROMOTION_LST = value;
                OnPropertyChanged("PromotionList");
            }
        }
        public List<RES_SEASONAL_STOCK> RES_SEASONAL_STOCK
        {
            get { return mRES_SEASONAL_STOCK; }
            set
            {
                mRES_SEASONAL_STOCK = value;
                OnPropertyChanged("SeasonalList");
            }
        }
        public List<RES_STOCK_HOME> RES_STOCK_HOME
        {
            get { return mRES_STOCK_HOME; }
            set
            {
                mRES_STOCK_HOME = value;
                OnPropertyChanged("StockHomeList");
            }
        }

        public ObservableCollection<DAT_FLOAT> FloatLeftList
        {
            get { return mDAT_FLOAT_LEFT; }
            set
            {
                mDAT_FLOAT_LEFT = value;
                OnPropertyChanged("FloatLeftList");
            }
        }
        public ObservableCollection<DAT_FLOAT> FloatRightList
        {
            get { return mDAT_FLOAT_RIGHT; }
            set
            {
                mDAT_FLOAT_RIGHT = value;
                OnPropertyChanged("FloatRightList");
            }
        }


        public Thickness CheckoutThickness
        {
            get { return mCheckoutThickness; }
            set
            {
                mCheckoutThickness = value;
                OnPropertyChanged("CheckoutThickness");
                //OnPropertyChanged("BadgeViewCheckoutThickness");

            }
        }
        public bool ApplicationAlert
        {
            get { return mApplicationAlert; }
            set
            {
                mApplicationAlert = value;
                OnPropertyChanged("ApplicationAlert");
            }
        }
        public bool LiveChat
        {
            get { return mLiveChat; }
            set
            {
                mLiveChat = value;
                OnPropertyChanged("LiveChat");
            }
        }
        public bool FormAlert
        {
            get { return mFormAlert; }
            set
            {
                mFormAlert = value;
                OnPropertyChanged("FormAlert");
            }
        }
        public string FormAlertMessage
        {
            get { return mFormAlertMessage; }
            set
            {
                mFormAlertMessage = value;
                OnPropertyChanged("FormAlertMessage");
            }
        }
        public bool UserLoggedIn
        {
            get { return mUserLoggedIn; }
            set
            {
                mUserLoggedIn = value;
                OnPropertyChanged("UserLoggedIn");
            }
        }
        public bool UserLoggedOut
        {
            get { return mUserLoggedOut; }
            set
            {
                mUserLoggedOut = value;
                OnPropertyChanged("UserLoggedOut");
            }
        }

        #endregion


        #region"Private Method"
        private void bindRegionData()
        {
            SliderList = new ObservableCollection<DAT_SLIDER>();
            foreach (DAT_SLIDER l_DAT_SLIDER in this.JSN_RES_ECOMANCE.DAT_SLIDER)
            {
                SliderList.Add(l_DAT_SLIDER);
            }
        }
        public void bindSwitchProductData()
        {

            for (int i = 0; i < Common.mCommon.RES_PRODUCT_LST.Count; i++)
            {
                if (Common.mCommon.RES_PRODUCT_LST[i].ProductAsk == mCommon.REQ_AUTHORIZATION.ProductAsk)
                {
                    mCommon.SelectedProduct = Common.mCommon.RES_PRODUCT_LST[i];
                }
            }
            this.SelectedMenu = this.JSN_RES_ECOMANCE.menu[0];
        }
        public async void bindLoginData()
        {
            this.RES_PRODUCT_LST = this.JSN_RES_ECOMANCE.products;
            this.CompanyUserList = this.JSN_RES_ECOMANCE.RES_COMPANY_USER;
            this.CountryList = this.JSN_RES_ECOMANCE.RES_COUNTRY;

            if (this.JSN_RES_ECOMANCE.user != null)
            {
                this.User = this.JSN_RES_ECOMANCE.user;
            }
            if (this.JSN_RES_ECOMANCE.DAT_USER_SETTING.Count > 0)
            {
                this.UserSetting = this.JSN_RES_ECOMANCE.DAT_USER_SETTING[0];
                string l_userPreferredLanguage = this.UserSetting.LanguageCode_0_50;
                mCommon.UpdateLanguage(l_userPreferredLanguage);
                mCommon.UpdateMessageManager(l_userPreferredLanguage);
            }
            else
            {
                this.UserSetting = new DAT_USER_SETTING();
            }
            if (this.JSN_RES_ECOMANCE.RES_SHOPPING_CART.Detail.Count > 0)
            {
                this.CheckoutThickness = new Thickness(0, 0, 0, 0);
                this.ShoppingCartList = this.JSN_RES_ECOMANCE.RES_SHOPPING_CART.Detail;
                this.Shopping = this.JSN_RES_ECOMANCE.RES_SHOPPING_CART.Header[0];
                //need to set paddint to change checkout icon
                this.CheckoutThickness = new Thickness(0, 0, 0, 0);
            }
            else
            {
                //need to set paddint to change checkout icon
                this.CheckoutThickness = new Thickness(0, 10, 0, 0);
            }
            if (mCommon.User.UserAsk == "2")//Guest
            {
                this.UserLoggedIn = false;
                this.UserLoggedOut = true;
            }
            else //login
            {
                this.UserLoggedIn = true;
                this.UserLoggedOut = false;
            }

            NotiList = new ObservableCollection<RES_NOTI_LST>();
            if (this.JSN_RES_ECOMANCE.noti.Count > 0)
            {
                foreach (RES_NOTI_LST l_RES_NOTI_LST in JSN_RES_ECOMANCE.noti)
                {
                    this.NotiList.Add(l_RES_NOTI_LST);
                }
            }
            DiscussionList = new ObservableCollection<DAT_DISCUSSION_NOTI>();
            if (this.JSN_RES_ECOMANCE.discussion.Count > 0)
            {
                foreach (DAT_DISCUSSION_NOTI l_DAT_DISCUSSION_NOTI in JSN_RES_ECOMANCE.discussion)
                {
                    this.DiscussionList.Add(l_DAT_DISCUSSION_NOTI);
                }
            }
            FloatLeftList = new ObservableCollection<DAT_FLOAT>();
            FloatRightList = new ObservableCollection<DAT_FLOAT>();
            if (this.JSN_RES_ECOMANCE.DAT_FLOAT.Count > 0)
            {
                foreach (DAT_FLOAT l_DAT_FLOAT in JSN_RES_ECOMANCE.DAT_FLOAT)
                {
                    if (l_DAT_FLOAT.FloatAlignmentAsk == "1")//1 for left
                    {
                        FloatLeftList.Add(l_DAT_FLOAT);
                    }
                    else
                    if (l_DAT_FLOAT.FloatAlignmentAsk == "2")//2 for right
                    {
                        FloatRightList.Add(l_DAT_FLOAT);
                    }
                }
            }
        }
        public void bindShelfData()
        {
          
            ShelfList = new ObservableCollection<DAT_SHELF>();
            foreach (DAT_SHELF l_DAT_SHELF in this.JSN_RES_ECOMANCE.DAT_SHELF)
            {
                ShelfList.Add(l_DAT_SHELF);
            }
        }
        public void combileImageURL()
        {
            try
            {
                if (this.JSN_RES_ECOMANCE.user != null)
                {
                    this.JSN_RES_ECOMANCE.user.ProfilePicture = Sys_Service.getUploadURL() + this.JSN_RES_ECOMANCE.user.ProfilePicture;
                }
                if (this.JSN_RES_ECOMANCE.products != null && this.JSN_RES_ECOMANCE.products.Count > 0)
                {
                    foreach (RES_PRODUCT l_RES_PRODUCT in this.JSN_RES_ECOMANCE.products)
                    {
                        l_RES_PRODUCT.logoImg = Sys_Service.getUploadURL() + l_RES_PRODUCT.logoImg;
                    }
                }
                if (this.JSN_RES_ECOMANCE.discussion != null && this.JSN_RES_ECOMANCE.discussion.Count > 0)
                {
                    foreach (DAT_DISCUSSION_NOTI l_RES_DISCUSSION in this.JSN_RES_ECOMANCE.discussion)
                    {
                        l_RES_DISCUSSION.UserProfileURL = Sys_Service.getUploadURL() + l_RES_DISCUSSION.UserProfileURL;
                    }
                }
                if (this.JSN_RES_ECOMANCE.RES_AD != null)
                {
                    foreach (RES_AD l_RES_AD in this.JSN_RES_ECOMANCE.RES_AD)
                    {
                        l_RES_AD.ADPhotoURL = Sys_Service.getUploadURL() + l_RES_AD.ADPhotoURL;
                    }
                }
                if (this.JSN_RES_ECOMANCE.RES_PROMOTION != null)
                {
                    foreach (RES_PROMOTION l_RES_PROMOTION in this.JSN_RES_ECOMANCE.RES_PROMOTION)
                    {
                        l_RES_PROMOTION.SubscriberPhoto = Sys_Service.getUploadURL() + l_RES_PROMOTION.SubscriberPhoto;
                        l_RES_PROMOTION.PromotionPhotoURL = Sys_Service.getUploadURL() + l_RES_PROMOTION.PromotionPhotoURL;
                    }
                }
                if (this.JSN_RES_ECOMANCE.RES_SEASONAL_STOCK != null)
                {
                    foreach (RES_SEASONAL_STOCK l_RES_SEASONAL_STOCK in this.JSN_RES_ECOMANCE.RES_SEASONAL_STOCK)
                    {
                        l_RES_SEASONAL_STOCK.SubscriberPhoto = Sys_Service.getUploadURL() + l_RES_SEASONAL_STOCK.SubscriberPhoto;
                        l_RES_SEASONAL_STOCK.SeasonalPhotoURL = Sys_Service.getUploadURL() + l_RES_SEASONAL_STOCK.SeasonalPhotoURL;
                    }
                }
                if (this.JSN_RES_ECOMANCE.RES_COMPANY_USER != null)
                {
                    foreach (RES_COMPANY_USER l_RES_COMPANY_USER in this.JSN_RES_ECOMANCE.RES_COMPANY_USER)
                    {
                        l_RES_COMPANY_USER.CompanyLogo = Sys_Service.getUploadURL() + l_RES_COMPANY_USER.CompanyLogo;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void bindMenuLst()
        {
            try
            {
                RES_MENU_LST = new ObservableCollection<RES_MENU>();
                if (this.JSN_RES_ECOMANCE.menu != null && this.JSN_RES_ECOMANCE.menu.Count > 0 && this.SelectedProduct != null)
                {
                    foreach (RES_MENU l_RES_MENU in this.JSN_RES_ECOMANCE.menu)
                    {
                        if (l_RES_MENU.ProductAsk == this.SelectedProduct.ProductAsk)
                        {
                            RES_MENU_LST.Add(l_RES_MENU);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void InitMenuGroup()
        {
            //ObservableCollection<MenuGroup> menuGroups = new ObservableCollection<MenuGroup>();
            mMenuList = new ObservableCollection<ObservableProperty>();
            if (this.JSN_RES_ECOMANCE.menu != null && this.JSN_RES_ECOMANCE.menu.Count > 0 && this.SelectedProduct != null)
            {
                foreach (RES_MENU menu in this.JSN_RES_ECOMANCE.menu)
                {
                    if (menu.ProductAsk == this.SelectedProduct.ProductAsk)
                    {
                        ObservableProperty l_ObservableProperty = new ObservableProperty();
                        if (menu.subMenuList != null && menu.subMenuList.Count > 0)
                        {
                            foreach (RES_MENU subMenu in menu.subMenuList)
                            {
                                //MenuGroup submenuGroup = new MenuGroup();
                                //submenuGroup.Text =subMenu.Text;
                                //submenuGroup.MenuUrl = subMenu.MenuUrl;

                                l_ObservableProperty.Add(subMenu);
                            }
                            l_ObservableProperty.MenuCount = menu.subMenuList.Count;
                        }
                        else
                        {
                            l_ObservableProperty.SubMenuWidth = menu.SubMenuWidth;
                            l_ObservableProperty.MenuUrl = menu.MenuUrl;
                            l_ObservableProperty.button = menu.button;
                            l_ObservableProperty.StateIcon = "";
                        }
                        l_ObservableProperty.Text = menu.Text;
                        mMenuList.Add(l_ObservableProperty);
                    }
                }
            }
        }
        public DbUser getNewDbUser()
        {
            try
            {
                DbUser l_DbUser = new DbUser();
                l_DbUser.UserAsk = mCommon.User.UserAsk;
                l_DbUser.UserTS = mCommon.User.UserTS;
                l_DbUser.UserUD = mCommon.User.UserUD;
                l_DbUser.UserTD = mCommon.User.UserTD;
                l_DbUser.UserID = mCommon.REQ_AUTHORIZATION.UserID; //mCommon.User.UserEmail;
                l_DbUser.UserDescription = mCommon.User.UserDescription;
                l_DbUser.UserPassword = mCommon.REQ_AUTHORIZATION.UserPassword; //Common.mCommon.User.UserPassword;
                l_DbUser.UserEmail = mCommon.User.UserEmail;
                l_DbUser.UserPhone = mCommon.User.UserPhone;
                l_DbUser.ProfilePicture = mCommon.User.ProfilePicture;
                l_DbUser.SubscriberAsk = mCommon.User.SubscriberAsk;
                l_DbUser.AccessAsk = mCommon.User.AccessAsk;
                l_DbUser.UserTypeAsk = mCommon.User.UserTypeAsk;
                l_DbUser.UserStatus = 1; //Active login user
                l_DbUser.UserSequence = mCommon.User.UserSequence;
                l_DbUser.UserRemark = mCommon.User.UserRemark;
                l_DbUser.Remove = mCommon.User.Remove;
                return l_DbUser;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Public Method"
        public async void signInAuto()
        {
            try
            {
                mCommon.mDbUser_LST = await App.Database.getUserAsync();
                if (mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_DbUser in mCommon.mDbUser_LST)
                    {
                        if (l_DbUser.UserStatus == 1)
                        {
                            mCommon.REQ_AUTHORIZATION.UserID = l_DbUser.UserID;
                            mCommon.REQ_AUTHORIZATION.UserPassword = l_DbUser.UserPassword;
                            mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                            mCommon.REQ_AUTHORIZATION.ProductAsk = "2";
                        }
                    }
                }
                else
                {
                    DbUser l_DbUser = new DbUser();
                    //await App.Database.saveUserAsync(l_DbUser);
                    mCommon.REQ_AUTHORIZATION.UserID = l_DbUser.UserID;
                    mCommon.REQ_AUTHORIZATION.UserPassword = l_DbUser.UserPassword;
                    mCommon.REQ_AUTHORIZATION.ProductAsk = "2";
                    mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                }
                await mCommon.signIn(mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void switchProduct(RES_PRODUCT argRES_PRODUCT,string argMenuURL= "")
        {
            try
            {
                if (argRES_PRODUCT != null)
                {
                    mCommon.SelectedProduct = argRES_PRODUCT;
                    mCommon.REQ_AUTHORIZATION.ProductAsk = mCommon.SelectedProduct.ProductAsk;
                }
                mCommon.switchProduct(mCommon.REQ_AUTHORIZATION, null,argMenuURL);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void switchProduct(RES_PRODUCT argRES_PRODUCT, Action? l_MenuCallback = null)
        {
            try
            {
                if (argRES_PRODUCT != null)
                {
                    mCommon.SelectedProduct = argRES_PRODUCT;
                    mCommon.REQ_AUTHORIZATION.ProductAsk = mCommon.SelectedProduct.ProductAsk;
                }
                mCommon.switchProduct(mCommon.REQ_AUTHORIZATION, l_MenuCallback);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void updateNoti(RES_NOTI_LST argRES_NOTI_LST)
        {
            try
            {
                JSN_NOTI l_JSN_NOTI = new JSN_NOTI();
                List<RES_NOTI_LST> l_RES_NOTI_LST = new List<RES_NOTI_LST>();
                l_RES_NOTI_LST.Add(argRES_NOTI_LST);
                if (l_RES_NOTI_LST.Count > 0)
                {
                    l_JSN_NOTI.REQ_AUTHORIZATION = mCommon.REQ_AUTHORIZATION;
                    l_JSN_NOTI.RES_NOTI_LST = l_RES_NOTI_LST;
                    mCommon.updateNoti(l_JSN_NOTI);
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void updateDiscussion(DAT_DISCUSSION_NOTI argRES_DISCUSSION)
        {
            try
            {
                JSN_DISCUSSION_STATUS l_JSN_DISCUSSION_STATUS = new JSN_DISCUSSION_STATUS();
                List<DAT_DISCUSSION_NOTI> l_RES_DISCUSSION_LST = new List<DAT_DISCUSSION_NOTI>();
                l_RES_DISCUSSION_LST.Add(argRES_DISCUSSION);
                if (l_RES_DISCUSSION_LST.Count > 0)
                {
                    l_JSN_DISCUSSION_STATUS.REQ_AUTHORIZATION = mCommon.REQ_AUTHORIZATION;
                    //l_JSN_DISCUSSION_STATUS.REQ_DISCUSSION = l_RES_DISCUSSION_LST;
                    mCommon.updateDiscussion(l_JSN_DISCUSSION_STATUS);
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void signOut()
        {
            try
            {
                if (Common.mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_user in mCommon.mDbUser_LST)
                    {
                        if (l_user.UserAsk == mCommon.User.UserAsk)
                        {
                            await App.Database.deleteUserAsync(l_user);
                        }
                    }
                }
                DbUser l_DbUser = new DbUser();
                // await App.Database.saveUserAsync(l_DbUser);
                mCommon.REQ_AUTHORIZATION.UserID = l_DbUser.UserID;
                mCommon.REQ_AUTHORIZATION.UserPassword = l_DbUser.UserPassword;
                mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                mCommon.signIn(mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void signOut(DbUser argDbUser)
        {
            try
            {
                if (Common.mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_user in mCommon.mDbUser_LST)
                    {
                        if (l_user.UserAsk == argDbUser.UserAsk)
                        {
                            await App.Database.deleteUserAsync(l_user);
                        }
                    }
                }
                mCommon.mDbUser_LST = await App.Database.getUserAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void signOutAll()
        {
            try
            {
                if (mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_user in mCommon.mDbUser_LST)
                    {
                        l_user.UserStatus = 0;
                        await App.Database.deleteUserAsync(l_user);
                        //await App.Database.saveUserAsync(l_user);
                    }
                }
                mCommon.mDbUser_LST.Clear();
                DbUser l_DbUser = new DbUser();
                //await App.Database.saveUserAsync(l_DbUser);
                mCommon.REQ_AUTHORIZATION.UserID = l_DbUser.UserID;
                mCommon.REQ_AUTHORIZATION.UserPassword = l_DbUser.UserPassword;
                mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                mCommon.signIn(mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        //private void signIn()
        //{
        //    throw new NotImplementedException();
        //}

        public async static void saveDbUser()
        {
            try
            {
                bool isNew = true;
                if (mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_DbUser in mCommon.mDbUser_LST)
                    {
                        if (l_DbUser.UserAsk == mCommon.User.UserAsk)
                        {
                            l_DbUser.UserStatus = 1;
                            l_DbUser.UserID = mCommon.REQ_AUTHORIZATION.UserID;
                            l_DbUser.UserPassword = mCommon.REQ_AUTHORIZATION.UserPassword;
                            isNew = false;
                            await App.Database.saveUserAsync(l_DbUser);
                        }
                        else
                        {
                            l_DbUser.UserStatus = 0;
                            await App.Database.saveUserAsync(l_DbUser);
                        }
                    }
                    if (isNew)
                    {
                        await App.Database.saveUserAsync(mCommon.getNewDbUser());
                    }
                }
                else
                {
                    DbUser l_DbUser = new DbUser();
                    await App.Database.saveUserAsync(mCommon.getNewDbUser());
                    mCommon.REQ_AUTHORIZATION.UserID = l_DbUser.UserID;
                    mCommon.REQ_AUTHORIZATION.UserPassword = l_DbUser.UserPassword;
                }
                mCommon.mDbUser_LST = await App.Database.getUserAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public void checkInternetCon()
        {
            if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
            { ApplicationAlert = true; }
            else { ApplicationAlert = false; }

        }
        public async Task signIn(REQ_AUTHORIZATION argREQ_AUTHORIZATION)
        {
            string l_Request = "";
            var l_Response = "";

            try
            {
                Utility.openLoader();
                argREQ_AUTHORIZATION.ProductAsk = "10";//10 for eco
                l_Request = JsonConvert.SerializeObject(argREQ_AUTHORIZATION);
                l_Response = await Eco_Service.ApiCall(l_Request, Eco_Name.wsLogIn);
                if (!string.IsNullOrEmpty(l_Response))
                {
                    //Debug.WriteLine("MAUI:l_Response " + l_Response);
                    JSN_RES_ECOMANCE = JsonConvert.DeserializeObject<JSN_RES_ECOMANCE>(l_Response);
                    if (mCommon.JSN_RES_ECOMANCE?.Message?.Code == "7")
                    {
                        mCommon.REQ_AUTHORIZATION = argREQ_AUTHORIZATION;
                        this.combileImageURL();
                        this.InitMenuGroup();
                        this.bindShelfData();
                        this.connectChtSocket();
                        this.connectNtfSocket();
                        // Hook up callback to receive messages from service
                        chtSocketService.OnMessageReceived = handleChtMessage;
                        ntfSocketService.OnMessageReceived = handleNtfMessage;

                        listenWebSocket();
                        listenNtfSocket();
                        bindRegionData();
                        bindLoginData();
                        if (this.JSN_RES_ECOMANCE?.products?.Count > 0)
                        {
                            for (int i = 0; i < this.JSN_RES_ECOMANCE.products.Count; i++)
                            {
                                if (this.JSN_RES_ECOMANCE.products[i].ProductAsk == argREQ_AUTHORIZATION.ProductAsk)
                                {
                                    this.SelectedProduct = this.JSN_RES_ECOMANCE.products[i];
                                }
                            }
                            if (this.SelectedProduct?.ProductAsk == "0")
                            {
                                this.SelectedProduct = this.JSN_RES_ECOMANCE.products.First();
                            }
                        }
                        bindThemeSetting();
                        bindRegionSetting();

                        saveDbUser();
                        if (!Common.bindMenu("home"))
                        {
                            Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Home", MenuUrl = "home", logoImg = "" };
                            //WeakReferenceMessenger.Default.Send(mCommon.GetMessageValueByKey("MsgSaveSuccess"));
                        }
                        Common.routeMenu(Common.mCommon.SelectedMenu);
                        //WeakReferenceMessenger.Default.Send(mCommon.JSN_RES_ECOMANCE?.Message?.Message);
                    }
                    else
                    {
                        this.UserLoggedIn = false;
                        this.UserLoggedOut = true;
                        mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                        Common.routeMenu(Common.mCommon.SelectedMenu);
                        WeakReferenceMessenger.Default.Send(mCommon.JSN_RES_ECOMANCE?.Message?.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception)
            {
                Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                Common.routeMenu(Common.mCommon.SelectedMenu);
                Utility.closeLoader();
            }
        }

        public async Task<RES_MESSAGE?> getEmailOTP()
        {
            try
            {
                //Utility.openLoader();
                string l_Request = JsonConvert.SerializeObject(mCommon.REQ_AUTHORIZATION);
                string l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsgetEmailOTP);

                if (!string.IsNullOrEmpty(l_Response))
                {
                    ResMessage = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (ResMessage != null && ResMessage.Code == "7")
                    {
                        return ResMessage;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(ResMessage.Message);
                        return null;
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                //Utility.closeLoader();
            }
        }
        public async Task<RES_MESSAGE?> verifyEmailOTP(string argOTPCode)
        {
            try
            {
                mJSN_REQ_USER = new JSN_REQ_USER();
                mJSN_REQ_USER.REQ_AUTHORIZATION = mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_USER.RES_USER_LST.UserID = mCommon.User.UserAsk;
                mJSN_REQ_USER.RES_USER_LST.OTPCode_0_50 = argOTPCode;
                string l_Request = JsonConvert.SerializeObject(mJSN_REQ_USER);
                string l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsVerifyEmailOTP);

                if (!string.IsNullOrEmpty(l_Response))
                {
                    ResMessage = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (ResMessage != null && ResMessage.Code == "7")
                    {
                        return ResMessage;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(ResMessage.Message);
                        return null;
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<RES_MESSAGE?> getSMSOTP()
        {
            try
            {
                string l_Request = JsonConvert.SerializeObject(mCommon.REQ_AUTHORIZATION);
                string l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsgetSMSOTP);

                if (!string.IsNullOrEmpty(l_Response))
                {
                    ResMessage = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (ResMessage != null && ResMessage.Code == "7")
                    {
                        return ResMessage;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(ResMessage.Message);
                        return null;
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<RES_MESSAGE?> verifySMSOTP(string argOTPCode)
        {
            try
            {
                mJSN_REQ_USER = new JSN_REQ_USER();
                mJSN_REQ_USER.REQ_AUTHORIZATION = mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_USER.RES_USER_LST.UserID = mCommon.User.UserAsk;
                mJSN_REQ_USER.RES_USER_LST.OTPCode_0_50 = argOTPCode;
                string l_Request = JsonConvert.SerializeObject(mJSN_REQ_USER);
                string l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsverifySMSOTP);

                if (!string.IsNullOrEmpty(l_Response))
                {
                    ResMessage = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (ResMessage != null && ResMessage.Code == "7")
                    {
                        return ResMessage;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(ResMessage.Message);
                        return null;
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<RES_MESSAGE?> verifyPassword(string argPassword)
        {
            try
            {
                mCommon.REQ_AUTHORIZATION.UserPassword = argPassword;
                string l_Request = JsonConvert.SerializeObject(mCommon.REQ_AUTHORIZATION);
                string l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsverifyPassword);

                if (!string.IsNullOrEmpty(l_Response))
                {
                    ResMessage = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (ResMessage != null && ResMessage.Code == "7")
                    {
                        return ResMessage;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(ResMessage.Message);
                        return null;
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void bindRegionSetting()
        {
            try
            {
                if (this.JSN_RES_ECOMANCE.DAT_REGION_SETTING.Count > 0)
                {
                    foreach (DAT_REGION_SETTING l_DAT_REGION_SETTING in JSN_RES_ECOMANCE.DAT_REGION_SETTING)
                    {
                        if (l_DAT_REGION_SETTING.RegionTypeAsk == "1")
                        {
                            RegionSettingBanner = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "2")
                        {
                            RegionSettingAlert = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "3")
                        {
                            RegionSettingMenu = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "4")
                        {
                            RegionSettingTaskbar = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "5")
                        {
                            RegionSettingContent = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "7")
                        {
                            RegionSettingSlider = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "12")
                        {
                            RegionSettingShelf = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "18")
                        {
                            RegionSettingFloatLeft = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "19")
                        {
                            RegionSettingFloatRight = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "21")
                        {
                            RegionSettingButton = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "22")
                        {
                            RegionSettingSwipe = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "23")
                        {
                            RegionSettingCardView = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "24")
                        {
                            RegionSettingCardViewHeader = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "25")
                        {
                            RegionSettingCardViewBody = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "26")
                        {
                            RegionSettingCardViewFooter = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "27")
                        {
                            RegionSettingHighlight = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "32")
                        {
                            RegionSettingCompany = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "35")
                        {
                            RegionSettingBannerSearch = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "38")
                        {
                            RegionSettingMessage = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "39")
                        {
                            RegionSettingNoti = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "40")
                        {
                            RegionSettingBannerWishlist = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "41")
                        {
                            RegionSettingBannerOrder = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "42")
                        {
                            RegionSettingBannerShoppingCart = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "43")
                        {
                            RegionSettingBannerProfile = l_DAT_REGION_SETTING;
                        }
                        else if (l_DAT_REGION_SETTING.RegionTypeAsk == "45")
                        {
                            RegionSettingBannerAI = l_DAT_REGION_SETTING;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void bindThemeSetting()
        {
            try
            {
                if (this.JSN_RES_ECOMANCE.DAT_THEME_SETTING.Count > 0)
                {
                    foreach (DAT_THEME_SETTING l_DAT_THEME_SETTING in JSN_RES_ECOMANCE.DAT_THEME_SETTING)
                    {
                        if (l_DAT_THEME_SETTING.RegionTypeAsk == "1")
                        {
                            ThemeSettingBanner = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "2")
                        {
                            ThemeSettingAlert = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "3")
                        {
                            ThemeSettingMenu = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "4")
                        {
                            ThemeSettingTaskbar = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "5")
                        {
                            ThemeSettingContent = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "7")
                        {
                            ThemeSettingSlider = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "12")
                        {
                            ThemeSettingShelf = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "18")
                        {
                            ThemeSettingFloatLeft = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "19")
                        {
                            ThemeSettingFloatRight = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "21")
                        {
                            ThemeSettingButton = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "22")
                        {
                            ThemeSettingSwipe = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "23")
                        {
                            ThemeSettingCardView = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "24")
                        {
                            ThemeSettingCardViewHeader = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "25")
                        {
                            ThemeSettingCardViewBody = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "26")
                        {
                            ThemeSettingCardViewFooter = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "27")
                        {
                            ThemeSettingHighlight = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "32")
                        {
                            ThemeSettingCompany = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "35")
                        {
                            ThemeSettingBannerSearch = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "38")
                        {
                            ThemeSettingMessage = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "39")
                        {
                            ThemeSettingNoti = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "40")
                        {
                            ThemeSettingBannerWishlist = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "41")
                        {
                            ThemeSettingBannerOrder = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "42")
                        {
                            ThemeSettingBannerShoppingCart = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "43")
                        {
                            ThemeSettingBannerProfile = l_DAT_THEME_SETTING;
                        }
                        else if (l_DAT_THEME_SETTING.RegionTypeAsk == "45")
                        {
                            ThemeSettingBannerAI = l_DAT_THEME_SETTING;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void switchProduct(REQ_AUTHORIZATION argREQ_AUTHORIZATION, Action? l_MenuCallback = null,string argMenuURL = "")
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(argREQ_AUTHORIZATION);
                Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsSwitchProduct);
                if (l_Response != null || l_Response != "")
                {
                    this.JSN_RES_ECOMANCE = JsonConvert.DeserializeObject<JSN_RES_ECOMANCE>(l_Response);
                    if (mCommon.JSN_RES_ECOMANCE.Message.Code == "7")
                    {
                        mCommon.REQ_AUTHORIZATION.ProductAsk = argREQ_AUTHORIZATION.ProductAsk;
                        this.combileImageURL();
                        this.InitMenuGroup();
                        l_MenuCallback?.Invoke();
                        bindLoginData();
                        bindSwitchProductData();
                        saveDbUser();

                        if (Common.bindMenu(argMenuURL))
                        {

                        }
                        else if (!Common.bindMenu("home"))
                        {
                            Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Home", MenuUrl = "home", logoImg = "" };
                           WeakReferenceMessenger.Default.Send(ApplicationMessage.Message.Welcome + this.User.UserDescription);
                        }
                        Common.routeMenu(Common.mCommon.SelectedMenu);
                    }
                    else
                    {
                        this.UserLoggedIn = true;
                        this.UserLoggedOut = false;

                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                        Common.routeMenu(Common.mCommon.SelectedMenu);
                        //MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, mCommon.JSN_RES_ECOMANCE.Message.Message);
                        WeakReferenceMessenger.Default.Send(mCommon.JSN_RES_ECOMANCE.Message.Message);
                    }
                }
                else
                {
                    //MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, "Server Err");
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        public async void updateNoti(JSN_NOTI argJSN_NOTI)
        {
            string l_Request = "";
            var l_Response = "";
            RES_MESSAGE l_RES_MESSAGE = new RES_MESSAGE();
            try
            {
                Utility.openLoader();
                l_Request = JsonConvert.SerializeObject(argJSN_NOTI);
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsUpdateNoti);
                if (l_Response != null || l_Response != "")
                {
                    l_RES_MESSAGE = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (l_RES_MESSAGE.Code == "7")
                    {
                        //this.NotiList.Remove(argJSN_NOTI.RES_NOTI_LST[0]);
                    }
                    else
                    {
                        //MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, l_RES_MESSAGE.Message);
                        WeakReferenceMessenger.Default.Send(
                                         l_RES_MESSAGE.Message);
                    }
                }
                else
                {
                    //MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, "Server Err");
                    WeakReferenceMessenger.Default.Send(
                                             "Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        public async void updateDiscussion(JSN_DISCUSSION_STATUS argJSN_DISCUSSION_STATUS)
        {
            string l_Request = "";
            var l_Response = "";
            RES_MESSAGE l_RES_MESSAGE = new RES_MESSAGE();
            try
            {
                Utility.openLoader();
                l_Request = JsonConvert.SerializeObject(argJSN_DISCUSSION_STATUS);
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsupdateDiscussionStatus);
                if (l_Response != null || l_Response != "")
                {
                    l_RES_MESSAGE = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (l_RES_MESSAGE.Code == "7")
                    {
                        //this.NotiList.Remove(argJSN_NOTI.RES_NOTI_LST[0]);
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(l_RES_MESSAGE.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        public async void saveShoppingCart(JSN_REQ_SHOPPING argJSN_REQ_SHOPPING)
        {
            string l_Request = "";
            var l_Response = "";

            try
            {
                Utility.openLoader();
                l_Request = JsonConvert.SerializeObject(argJSN_REQ_SHOPPING);
                l_Response = await Pos_Service.ApiCall(l_Request, Pos_Name.wssaveShopping);
                if (l_Response != null || l_Response != "")
                {

                    this.ShoppingCart = JsonConvert.DeserializeObject<JSN_SHOPPING>(l_Response);
                    if (mCommon.ShoppingCart.Message.Code == "7")
                    {
                        if (mCommon.ShoppingCart.RES_SHOPPING_DETAIL.Count > 0)
                        {
                            mCommon.ShoppingCartList = this.JSN_RES_ECOMANCE.RES_SHOPPING_CART.Detail;
                            mCommon.Shopping = this.JSN_RES_ECOMANCE.RES_SHOPPING_CART.Header[0];
                        }
                        else
                        {

                        }
                        WeakReferenceMessenger.Default.Send("Successfully updated ");

                    }
                    else
                    {

                        WeakReferenceMessenger.Default.Send(mCommon.JSN_RES_ECOMANCE.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        public static bool bindMenu(string argMenuUrl)
        {
            try
            {
                if (mCommon.JSN_RES_ECOMANCE.menu != null && mCommon.JSN_RES_ECOMANCE.menu.Count > 0)
                {
                    foreach (RES_MENU L_RES_MENU in mCommon.JSN_RES_ECOMANCE.menu)
                    {
                        if (L_RES_MENU.MenuUrl.Equals(argMenuUrl))
                        {
                            //mCommon.mRES_MENU = Utility.Clone(l_menu);
                            mCommon.SelectedMenu = L_RES_MENU;
                            return true;
                        }

                        if (L_RES_MENU.subMenuList != null && L_RES_MENU.subMenuList.Count > 0)
                        {
                            foreach (RES_MENU L_RES_MENU_SUB in L_RES_MENU.subMenuList)
                            {
                                if (L_RES_MENU_SUB.Equals(argMenuUrl))
                                {
                                    //mCommon.RES_MENU = Utility.Clone(L_RES_MENU_SUB);
                                    mCommon.SelectedMenu = L_RES_MENU;
                                    return true;
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public static void routeMenu(RES_MENU argRES_MENU, object Arg1 = null, string Arg2 = null, string Arg3 = null)
        {
            try
            {
                Dictionary<string, Type> L_RouteList;
                switch (argRES_MENU.ProductAsk)
                {
                    case "1"://SYS
                        L_RouteList = Sys_Route.DicRouteList;
                        break;
                    case "2"://POS
                        L_RouteList = Pos_Route.DicRouteList;
                        break;
                    //case "3"://HCM
                    //    L_RouteList = Hcm_Route.DicRouteList;
                    //    break;
                    //case "4"://ATT
                    //    L_RouteList = Att_Route.DicRouteList;
                    //    break;
                    //case "5"://PAY
                    //    L_RouteList = Pay_Route.DicRouteList;
                    //break;
                    case "7"://ACC
                        L_RouteList = Acc_Route.DicRouteList;
                        break;
                    //case "6"://CRM
                    //    L_RouteList = Crm_Route.DicRouteList;
                    //    break;
                    //case "8"://WMS
                    //    L_RouteList = Wms_Route.DicRouteList;
                    //break;
                    default:
                        L_RouteList = Sys_Route.DicRouteList;
                        break;
                }
                Type l_type;
                var l_obj = new Object();
                if (!L_RouteList.TryGetValue(argRES_MENU.MenuUrl, out l_type))
                {
                    // the key isn't in the dictionary.
                    return;
                }

                // get public constructors
                var l_constructor = l_type.GetConstructors();

                if (Arg1 != null && Arg2 != null && Arg3 != null)
                {
                    // invoke the third public constructor with two parameters.
                    l_obj = l_constructor[2].Invoke(new object[] { Arg1, Arg2, Arg3 });

                }
                if (Arg1 != null && Arg2 != null)
                {
                    // invoke the third public constructor with two parameters.
                    l_obj = l_constructor[2].Invoke(new object[] { Arg1, Arg2, });

                }
                else if (Arg1 != null)
                {
                    // invoke the second public constructor with one parameters.
                    l_obj = l_constructor[1].Invoke(new object[] { Arg1 });
                }
                else
                {
                    // invoke the first public constructor with no parameters.
                    l_obj = l_constructor[0].Invoke(new object[] { });
                }
                if (argRES_MENU.MenuUrl == "pos-sales-invoice-jun-ova-set")
                {
                    ContentPage l_ContentPage = (ContentPage)l_obj;
                    ModelRoute l_ModelRoute = new ModelRoute(l_ContentPage, argRES_MENU.Text, "");
                    WeakReferenceMessenger.Default.Send(l_ModelRoute);
                }
                else
                {
                    ContentView l_ContentView = (ContentView)l_obj;
                    ModelRoute l_ModelRoute = new ModelRoute(l_ContentView, argRES_MENU.Text, "");
                    WeakReferenceMessenger.Default.Send(l_ModelRoute);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public async void connectChtSocket()
        {
            try
            {
                chtSocketService = new Cht_Service_WebSocket();
                await chtSocketService.connectSocket();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public async void connectNtfSocket()
        {
            try
            {
                ntfSocketService = new Ntf_Service_WebSocket();
                await ntfSocketService.connectSocket();

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void listenWebSocket()
        {
            await chtSocketService.ReceiveMessages();
        }

        private async void listenNtfSocket()
        {
            await ntfSocketService.ReceiveMessages();
        }

        private void handleChtMessage(string msgJson)
        {
            if (string.IsNullOrEmpty(msgJson)) return;
            mDAT_RES_CHAT_NOTI = JsonConvert.DeserializeObject<DAT_RES_CHAT_NOTI>(msgJson);
            if (mDAT_RES_CHAT_NOTI != null && mDAT_RES_CHAT_NOTI.Message.Code == "7")
            {
                if (mDAT_RES_CHAT_NOTI.Res == "getChatNoti")
                {
                    if (mDAT_RES_CHAT_NOTI.DAT_DISCUSSION_NOTI.Count > 0 && mDAT_RES_CHAT_NOTI.DAT_DISCUSSION_NOTI[0].Ask != Common.mCommon.User.UserAsk)
                    {
                        Common.mCommon.DiscussionList.Insert(0, mDAT_RES_CHAT_NOTI.DAT_DISCUSSION_NOTI[0]);
                    }                    
                }
            }
        }
        private void handleNtfMessage(string msgJson)
        {
            if (string.IsNullOrEmpty(msgJson)) return;
            mDAT_RES_NOTI = JsonConvert.DeserializeObject<DAT_RES_NOTI>(msgJson);
            if (mDAT_RES_NOTI != null && mDAT_RES_NOTI.Message.Code == "7")
            {
                if (mDAT_RES_NOTI.Res == "SaveNoti")
                {
                    if (this.mDAT_RES_NOTI.UserAsk != Common.mCommon.User.UserAsk)
                    {
                        if (mDAT_RES_NOTI.RES_USER_LST.Count > 0 && mDAT_RES_NOTI.RES_USER_LST[0].Ask == Common.mCommon.User.UserAsk)
                        {
                            Common.mCommon.NotiList.Insert(0, mDAT_RES_NOTI.RES_NOTI_LST);
                        }
                    }
                }
            }
        }
        public async Task OpenExternalApp(string appName, string argAppURL = "", string argPlaystoreURL = "")
        {
            try
            {
                await Launcher.OpenAsync(argAppURL);
            }
            catch
            {
                // App not installed → open Play Store
                await Launcher.OpenAsync(argPlaystoreURL);
            }
        }

        public async void saveNoti(RES_CONTROL argRES_CONTROL)
        {
            try
            {
                if (argRES_CONTROL.btncode == "0") return;
                if (argRES_CONTROL.btncode == "1")
                {
                    mRES_NOTI_LST_DATA = new RES_NOTI_LST();
                    mRES_NOTI_LST_DATA.ControlAsk = argRES_CONTROL.ID;
                    mRES_NOTI_LST_DATA.MenuAsk = Common.mCommon.SelectedMenu.Id;
                    mRES_NOTI_LST_DATA.LinkAsk1 = argRES_CONTROL.link;
                    await ntfSocketService.saveNoti(mRES_NOTI_LST_DATA);
                }
            }
            catch (Exception ex){
            }
        }
        public async void updateDefaultCompanyUser(RES_COMPANY_USER argRES_COMPANY_USER)
        {
            string l_Request = "";
            var l_Response = "";
            mJSN_REQ_COMPANY_USER = new JSN_REQ_COMPANY_USER();
            mJSN_REQ_COMPANY_USER.REQ_AUTHORIZATION = mCommon.REQ_AUTHORIZATION;
            mJSN_REQ_COMPANY_USER.RES_COMPANY_USER.Add(argRES_COMPANY_USER);
            try
            {
                Utility.openLoader();
                l_Request = JsonConvert.SerializeObject(mJSN_REQ_COMPANY_USER);
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsupdateDefaultCompanyUser);
                if (l_Response != null || l_Response != "")
                {
                    mJSN_COMPANY_USER = JsonConvert.DeserializeObject<JSN_COMPANY_USER>(l_Response);
                    if (mJSN_COMPANY_USER.Message.Code == "7")
                    {
                        CompanyUserList = mJSN_COMPANY_USER.RES_COMPANY_USER;
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(mJSN_COMPANY_USER.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion


    }
}
