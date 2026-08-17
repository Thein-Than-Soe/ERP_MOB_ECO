using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.ECO.DAT;
using CS.ERP.PL.ECO.REQ;
using CS.ERP.PL.ECO.RES;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Views.POS;
using CS.ERP_MOB.ViewsModel.ECO;
using CS.ERP_MOB_ECO.Views.ECO;
using Microsoft.Maui.Controls;
using System.Windows.Input;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCardViewStyle1 : ContentView
    {
        public FrmCardViewStyle1()
        {
            InitializeComponent();
        }

        // Bindable Properties (Image, Name, Price)
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set { SetValue(ImageProperty, value); StockImage.Source = value; }
        }

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set { SetValue(NameProperty, value); StockName.Text = value; }
        }

        public static readonly BindableProperty PriceProperty =
            BindableProperty.Create(nameof(Price), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Price
        {
            get => (string)GetValue(PriceProperty);
            set { SetValue(PriceProperty, value); StockPrice.Text = value; }
        }

        #region "Method"
        private async void WishlistButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                RES_STOCK item = null;

                if (BindingContext is DAT_SHELF_PRODUCT shelfProduct)
                {
                    item = new RES_STOCK
                    {
                        Ask = shelfProduct.Ask
                    };
                }
                else if (BindingContext is DAT_STOCK_RELATION stockRelation)
                {
                    item = new RES_STOCK
                    {
                        Ask = stockRelation.StockAsk
                    };
                }

                if (item == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        $"Unsupported BindingContext: {BindingContext?.GetType().FullName}",
                        "OK");
                    return;
                }

                var vmProductDtl = new VmlProductDtlLst();
                await vmProductDtl.getInventoryStock(item);

                RES_STOCK mRES_STOCK = vmProductDtl.Product;
                DAT_WISHLIST_DETAIL mDAT_WISHLIST_DETAIL = new DAT_WISHLIST_DETAIL();

                // Detail assign 
                mDAT_WISHLIST_DETAIL.StockAsk = mRES_STOCK.Ask;
                mDAT_WISHLIST_DETAIL.StockCode_0_50 = mRES_STOCK.StockCode_0_50;
                mDAT_WISHLIST_DETAIL.StockName_0_255 = mRES_STOCK.StockName_0_255;
                mDAT_WISHLIST_DETAIL.StockPhotoURL = mRES_STOCK.PhotoURL;

                mDAT_WISHLIST_DETAIL.Price = mRES_STOCK.StockRetailPrice;
                mDAT_WISHLIST_DETAIL.QTY = "1";
                mDAT_WISHLIST_DETAIL.TotalAmount = ((decimal.TryParse(mDAT_WISHLIST_DETAIL.Price, out var p) ? p : 0) *
                                            (decimal.TryParse(mDAT_WISHLIST_DETAIL.QTY, out var q) ? q : 0)
                                         ).ToString();

                mDAT_WISHLIST_DETAIL.DiscountTypeAsk = mRES_STOCK.DiscountTypeAsk;
                //mDAT_WISHLIST_DETAIL.DiscountRate = mRES_STOCK.DiscountRate;
                mDAT_WISHLIST_DETAIL.DiscountAmount = mRES_STOCK.DiscountAmount;
                mDAT_WISHLIST_DETAIL.CurrencyAsk = mRES_STOCK.CurrencyAsk;
                mDAT_WISHLIST_DETAIL.Cost = mDAT_WISHLIST_DETAIL.Price;
                mDAT_WISHLIST_DETAIL.TotalCost = mDAT_WISHLIST_DETAIL.TotalAmount;



                var wishlistVM = new VmlWishlist();

                wishlistVM.mJSN_REQ_WISHLIST.DAT_WISHLIST = new DAT_WISHLIST();
                wishlistVM.mJSN_REQ_WISHLIST.DAT_WISHLIST_DETAIL = new List<DAT_WISHLIST_DETAIL> { mDAT_WISHLIST_DETAIL };
                await wishlistVM.saveWishlist();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
        private async void CartButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                RES_STOCK item = null;

                if (BindingContext is DAT_SHELF_PRODUCT shelfProduct)
                {
                    item = new RES_STOCK
                    {
                        Ask = shelfProduct.Ask
                    };
                }
                else if (BindingContext is DAT_STOCK_RELATION stockRelation)
                {
                    item = new RES_STOCK
                    {
                        Ask = stockRelation.StockAsk
                    };
                }

                if (item == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        $"Unsupported BindingContext: {BindingContext?.GetType().FullName}",
                        "OK");
                    return;
                }

                var vmProductDtl = new VmlProductDtlLst();
                await vmProductDtl.getInventoryStock(item);
                RES_STOCK mRES_STOCK = vmProductDtl.Product;

                RES_SHOPPING_DETAIL mRES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();

                // Detail assign 
                mRES_SHOPPING_DETAIL.StockAsk = mRES_STOCK.Ask;
                mRES_SHOPPING_DETAIL.StockCode_0_50 = mRES_STOCK.StockCode_0_50;
                mRES_SHOPPING_DETAIL.StockName_0_255 = mRES_STOCK.StockName_0_255;
                mRES_SHOPPING_DETAIL.Price = mRES_STOCK.StockRetailPrice;
                mRES_SHOPPING_DETAIL.QTY = "1";
                mRES_SHOPPING_DETAIL.TotalAmount = (
                                                    (decimal.TryParse(mRES_SHOPPING_DETAIL.Price, out var price) ? price : 0) *
                                                    (decimal.TryParse(mRES_SHOPPING_DETAIL.QTY, out var qty) ? qty : 0)
                                                   ).ToString();

                mRES_SHOPPING_DETAIL.StockPhotoURL = mRES_STOCK.PhotoURL;
                mRES_SHOPPING_DETAIL.UOMAsk = mRES_STOCK.DimensionUOMAsk;
                mRES_SHOPPING_DETAIL.UOMName_0_255 = mRES_STOCK.DimensionUOMName_0_255;

                mRES_SHOPPING_DETAIL.DiscountTypeAsk = mRES_STOCK.DiscountTypeAsk;
                mRES_SHOPPING_DETAIL.DiscountAmount = mRES_STOCK.DiscountAmount;
                mRES_SHOPPING_DETAIL.CurrencyAsk = mRES_STOCK.CurrencyAsk;



                var shoppingVM = new VmlShoppingCart();

                shoppingVM.mJSN_REQ_SHOPPING.RES_SHOPPING = new RES_SHOPPING();
                shoppingVM.mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = new List<RES_SHOPPING_DETAIL> { mRES_SHOPPING_DETAIL };
                await shoppingVM.saveShoppingCart();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }
        }
        private async void Product_Card_Tapped(object sender, TappedEventArgs e)
        {
            try
            {
                RES_STOCK item = null;

                if (BindingContext is DAT_SHELF_PRODUCT shelfProduct)
                {
                    item = new RES_STOCK
                    {
                        Ask = shelfProduct.Ask
                    };
                }
                else if (BindingContext is DAT_STOCK_RELATION stockRelation)
                {
                    item = new RES_STOCK
                    {
                        Ask = stockRelation.StockAsk
                    };
                }

                if (item == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        $"Unsupported BindingContext: {BindingContext?.GetType().FullName}",
                        "OK");
                    return;
                }

                await Application.Current.MainPage.Navigation.PushAsync(
                    new FrmEcoProductDtl(item));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    ex.ToString(),
                    "OK");
            }
        }

        #endregion

    }
}