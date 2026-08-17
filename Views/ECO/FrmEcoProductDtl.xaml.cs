namespace CS.ERP_MOB_ECO.Views.ECO;

using CS.ERP.PL.ECO.DAT;
using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.ViewsModel.ECO;
using Syncfusion.Maui.Core.Carousel;
using System.Linq;
using System.Xml.Linq;

public partial class FrmEcoProductDtl : ContentPage
{
    private readonly VmlProductDtlLst vm;
    private bool _isLoaded;

    //public FrmEcoProductDtl(RES_STOCK item)
    //{
    //    InitializeComponent();

    //    vm = new VmlProductDtlLst(item);

    //    BindingContext = vm;
    //}
    public FrmEcoProductDtl(RES_STOCK item)
    {
        try
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine(
                "FrmEcoProductDtl: InitializeComponent OK");

            vm = new VmlProductDtlLst(item);

            System.Diagnostics.Debug.WriteLine(
                "FrmEcoProductDtl: ViewModel created");

            BindingContext = vm;

            System.Diagnostics.Debug.WriteLine(
                "FrmEcoProductDtl: BindingContext OK");

            Loaded += FrmEcoProductDtl_Loaded;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"FrmEcoProductDtl ERROR: {ex}");

            throw;
        }
    }
    private async void FrmEcoProductDtl_Loaded(object sender, EventArgs e)
    {
        if (_isLoaded)
            return;

        _isLoaded = true;

        try
        {
            await vm.LoadProductAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"========== PRODUCT DETAIL LOAD ERROR ==========\n{ex}");
        }
    }
    private void NextImage_Clicked(object sender, EventArgs e)
    {
        int total = ProductCarousel.ItemsSource.Cast<object>().Count();

        if (ProductCarousel.Position < total - 1)
            ProductCarousel.Position++;
    }

    private void PreviousImage_Clicked(object sender, EventArgs e)
    {
        if (ProductCarousel.Position > 0)
            ProductCarousel.Position--;
    }

    
    private void Plus_Clicked(object sender, EventArgs e)
    {
        if (vm == null)
            return;

        vm.Quantity++;

        QtyEntry.Text = vm.Quantity.ToString();
    }
    private void Minus_Clicked(object sender, EventArgs e)
    {
        if (vm == null)
            return;

        if (vm.Quantity > 1)
            vm.Quantity--;

        QtyEntry.Text = vm.Quantity.ToString();
    }

    private void QtyEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (vm == null)
            return;

        if (int.TryParse(e.NewTextValue, out int quantity))
        {
            if (quantity < 1)
                quantity = 1;

            if (vm.Quantity != quantity)
                vm.Quantity = quantity;
        }
    }

    private async void WishlistButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            RES_STOCK mRES_STOCK = vm.Product;
            DAT_WISHLIST_DETAIL mDAT_WISHLIST_DETAIL = new DAT_WISHLIST_DETAIL();

            // Detail assign 
            mDAT_WISHLIST_DETAIL.StockAsk = mRES_STOCK.Ask;
            mDAT_WISHLIST_DETAIL.StockCode_0_50 = mRES_STOCK.StockCode_0_50;
            mDAT_WISHLIST_DETAIL.StockName_0_255 = mRES_STOCK.StockName_0_255;
            mDAT_WISHLIST_DETAIL.StockPhotoURL = mRES_STOCK.PhotoURL;

            mDAT_WISHLIST_DETAIL.Price = mRES_STOCK.NewPrice;
            mDAT_WISHLIST_DETAIL.QTY = vm.Quantity.ToString();
            mDAT_WISHLIST_DETAIL.TotalAmount = vm.TotalPrice;

            mDAT_WISHLIST_DETAIL.DiscountTypeAsk = mRES_STOCK.DiscountTypeAsk;
            mDAT_WISHLIST_DETAIL.DiscountAmount = mRES_STOCK.DiscountAmount;
            mDAT_WISHLIST_DETAIL.CurrencyAsk = mRES_STOCK.CurrencyAsk;
            mDAT_WISHLIST_DETAIL.Cost = mDAT_WISHLIST_DETAIL.Price;
            mDAT_WISHLIST_DETAIL.TotalCost = mDAT_WISHLIST_DETAIL.TotalAmount;

            if (vm.HasCustomized)
            {
                mDAT_WISHLIST_DETAIL.Price = vm.SelectedVariation.RetailPrice;
                mDAT_WISHLIST_DETAIL.StockName_0_255 = vm.SelectedVariation.PermutationData;
            }

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
            RES_STOCK mRES_STOCK = vm.Product;

            RES_SHOPPING_DETAIL mRES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();

            // Detail assign 
            mRES_SHOPPING_DETAIL.StockAsk = mRES_STOCK.Ask;
            mRES_SHOPPING_DETAIL.StockCode_0_50 = mRES_STOCK.StockCode_0_50;
            mRES_SHOPPING_DETAIL.StockName_0_255 = mRES_STOCK.StockName_0_255;
            mRES_SHOPPING_DETAIL.Price = mRES_STOCK.NewPrice;
            mRES_SHOPPING_DETAIL.QTY = vm.Quantity.ToString();
            mRES_SHOPPING_DETAIL.TotalAmount = vm.TotalPrice;

            mRES_SHOPPING_DETAIL.StockPhotoURL = mRES_STOCK.PhotoURL;
            mRES_SHOPPING_DETAIL.UOMAsk = mRES_STOCK.UOMAsk;
            mRES_SHOPPING_DETAIL.UOMName_0_255 = mRES_STOCK.UOMName_0_255;

            mRES_SHOPPING_DETAIL.DiscountTypeAsk = mRES_STOCK.DiscountTypeAsk;
            mRES_SHOPPING_DETAIL.DiscountAmount = mRES_STOCK.DiscountAmount;
            mRES_SHOPPING_DETAIL.CurrencyAsk = mRES_STOCK.CurrencyAsk;

            if (vm.HasCustomized)
            {
                mRES_SHOPPING_DETAIL.Price = vm.SelectedVariation.RetailPrice;
            }

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

    private async void Variation_Clicked( object sender, TappedEventArgs e)
    {
        try
        {
            var vm = BindingContext as VmlProductDtlLst;

            if (vm == null)
                return;

            var tempSelectedAttributes =
                new Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>(
                    vm.SelectedAttributeTerms);

            var popup = new FrmEcoProductAttributePop(
                vm.Attributes,
                tempSelectedAttributes,
                vm);

            await Navigation.PushModalAsync(popup);
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.ToString(),
                "OK");
        }
    }


}