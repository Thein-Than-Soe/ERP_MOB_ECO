using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.ECO.DAT;
using CS.ERP.PL.ECO.REQ;
using CS.ERP.PL.ECO.RES;
using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.ECO;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.Views.ECO;
using CS.ERP_MOB.ViewsModel.Frame;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using Stripe;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.ECO
{
    public class VmlProductDtlLst : BaseViewModel
    {
        #region "Declaring"
        string mRequest = "";
        string mResponse = "";

        //stock
        public ERP.PL.ECO.REQ.JSN_REQ_INVENTORY_STOCK mJSN_REQ_INVENTORY_STOCK_1 = new ERP.PL.ECO.REQ.JSN_REQ_INVENTORY_STOCK();//Auth, RES_stock
        public JSN_RES_INVENTORY_STOCK mJSN_RES_INVENTORY_STOCK_1 = new JSN_RES_INVENTORY_STOCK();//response data from get_inv_stock
        
        //stock dtl
        public ERP.PL.ECO.REQ.JSN_REQ_INVENTORY_STOCK mJSN_REQ_INVENTORY_STOCK = new ERP.PL.ECO.REQ.JSN_REQ_INVENTORY_STOCK();//Auth, RES_stock
        public JSN_RES_INVENTORY_STOCK mJSN_RES_INVENTORY_STOCK = new JSN_RES_INVENTORY_STOCK();//response data from get_inv_stock_dtl

        //stock sale
        public JSN_REQ_INVENTRY_STOCK_NEW mJSN_REQ_INVENTRY_STOCK_NEW = new JSN_REQ_INVENTRY_STOCK_NEW();
        public JSN_INVENTORY_STOCK mJSN_INVENTORY_STOCK = new JSN_INVENTORY_STOCK();

        //Stock detail
        // Used for selection/business logic
        public Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM> SelectedAttributeTerms { get; set; }
            = new Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>();

        // Used for displaying selected values in UI
        public ObservableCollection<DAT_STOCK_ATTRIBUTE_TERM> SelectedAttributeDisplay { get; set; }
    = new ObservableCollection<DAT_STOCK_ATTRIBUTE_TERM>();
        

        #endregion

        #region "Contructor"
        public VmlProductDtlLst()
        {
            // Main Product
            Product = new RES_STOCK();
            // Collections

            ProductPhotos = new ObservableCollection<RES_STOCK_PHOTO>();

            ProductReviews = new ObservableCollection<DAT_STOCK_RATING>();

            RelatedProducts = new ObservableCollection<DAT_STOCK_RELATION>();

            Attributes = new ObservableCollection<DAT_STOCK_ATTRIBUTE>();

            Variations = new ObservableCollection<DAT_STOCK_VARIATION>();

            SelectedAttributeTerms = new Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>();
        }
        public VmlProductDtlLst(RES_STOCK item)
        {
            // Main Product
            Product = item;
            // Collections
            ProductPhotos = new ObservableCollection<RES_STOCK_PHOTO>();

            ProductReviews = new ObservableCollection<DAT_STOCK_RATING>();

            RelatedProducts = new ObservableCollection<DAT_STOCK_RELATION>();

            Attributes = new ObservableCollection<DAT_STOCK_ATTRIBUTE>();

            Variations = new ObservableCollection<DAT_STOCK_VARIATION>();

            SelectedAttributeTerms = new Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>();

        }
        public async Task LoadProductAsync()
        {
            await LoadProduct(Product);
        }

        public async Task LoadProduct(RES_STOCK item)
        {
            try
            {
                Utility.openLoader();

                await getInventoryStock(item);

                if (Product == null)
                    return;
                else if (Product.TypeAsk == "3")
                {
                    await getInventoryStockSales(Product);
                }
                else
                {
                    await getInventoryStockDetail(Product);
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Utility.closeLoader();
            }
        }
        #endregion

        #region "Boolean Declaring"

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

        public bool HasDiscount
        {
            get
            {
                if (Product == null)
                    return false;

                decimal.TryParse(Product.DiscountAmount, out var discount);

                return discount > 0;
            }
        }

        public bool HasOldPrice
        {
            get
            {
                if (Product == null)
                    return false;

                decimal.TryParse(Product.OldPrice, out var oldPrice);
                decimal.TryParse(Product.NewPrice, out var newPrice);

                return oldPrice > newPrice;
            }
        }

        public bool HasCustomized => Product != null && Product.TypeAsk == "3";
        public bool HasNoCustomized => !HasCustomized;
        public bool HasDescription =>Product != null &&!string.IsNullOrWhiteSpace(Product.StockDescription_0_500);
        public bool HasReviews =>ProductReviewsDisplay != null &&ProductReviewsDisplay.Count > 0;
        public bool HasRelatedProducts =>RelatedProducts != null &&RelatedProducts.Count > 0;

        public bool HasPhotos => ProductPhotos != null && ProductPhotos.Count > 0;
        #endregion

        #region Product Detail display model


        public string CurrentPrice =>
            Product == null
                ? ""
                : $"{decimal.Parse(Product.NewPrice)}";

        public string OldPrice =>
            Product == null
                ? ""
                : $"{decimal.Parse(Product.OldPrice)}";

        public string DiscountAmount =>
            Product == null
                ? ""
                : $"{decimal.Parse(Product.DiscountAmount)}";

        public string RatingSummary
        {
            get
            {
                if (ProductReviews == null || ProductReviews.Count == 0)
                    return "No Reviews";

                if (!double.TryParse(Product?.Rate, out double rate))
                    return "No Reviews";

                return $"{rate:0.0} ({ProductReviews.Count} Reviews)";
            }
        }

        
        
        private int _quantity = 1;

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 1)
                    value = 1;

                _quantity = value;

                NotifyPropertyChanged(nameof(Quantity));
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }

        public string TotalPrice
        {
            get
            {
                decimal unitPrice = 0;

                if (HasCustomized)
                {
                    if (SelectedVariation != null)
                    {
                        decimal.TryParse(
                            SelectedVariation.RetailPrice,
                            out unitPrice);
                    }
                }
                else if (HasNoCustomized)
                {
                    decimal.TryParse(
                        Product?.NewPrice,
                        out unitPrice);
                }

                decimal total = unitPrice * Quantity;

                return total.ToString();
            }
        }

        //Review text card
        public class ProductReviewDisplay
        {
            public string UserName { get; set; }
            public string Rate { get; set; }
            public string Review { get; set; }
            public string UserProfile { get; set; }
        }
        #endregion

        #region "Get Set"
        private RES_STOCK _product;
        public RES_STOCK Product
        {
            get => _product;
            set
            {
                _product = value;
                NotifyPropertyChanged(nameof(Product));

                // Visibility
                NotifyPropertyChanged(nameof(HasCustomized));
                NotifyPropertyChanged(nameof(HasNoCustomized));
                NotifyPropertyChanged(nameof(HasDescription));
                NotifyPropertyChanged(nameof(HasDiscount));
                NotifyPropertyChanged(nameof(HasOldPrice));

                // Price
                NotifyPropertyChanged(nameof(CurrentPrice));
                NotifyPropertyChanged(nameof(OldPrice));
                NotifyPropertyChanged(nameof(DiscountAmount));
                NotifyPropertyChanged(nameof(TotalPrice));

            }
        }
        private ObservableCollection<RES_STOCK_PHOTO> _productPhotos =new ObservableCollection<RES_STOCK_PHOTO>();

        public ObservableCollection<RES_STOCK_PHOTO> ProductPhotos
        {
            get => _productPhotos;
            set
            {
                _productPhotos = value;
                NotifyPropertyChanged(nameof(ProductPhotos));
                NotifyPropertyChanged(nameof(HasPhotos));
            }
        }
        private ObservableCollection<DAT_STOCK_ATTRIBUTE> _attributes = new ObservableCollection<DAT_STOCK_ATTRIBUTE>();

        public ObservableCollection<DAT_STOCK_ATTRIBUTE> Attributes
        {
            get => _attributes;
            set
            {
                _attributes = value;
                NotifyPropertyChanged(nameof(Attributes));
            }
        }

        private ObservableCollection<DAT_STOCK_VARIATION> _variations =new ObservableCollection<DAT_STOCK_VARIATION>();

        public ObservableCollection<DAT_STOCK_VARIATION> Variations
        {
            get => _variations;
            set
            {
                _variations = value;
                NotifyPropertyChanged(nameof(Variations));
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }
        private DAT_STOCK_VARIATION _selectedVariation;

        public DAT_STOCK_VARIATION SelectedVariation
        {
            get => _selectedVariation;
            set
            {
                _selectedVariation = value;
                NotifyPropertyChanged(nameof(SelectedVariation));
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }


        private ObservableCollection<DAT_STOCK_RATING> _productReviews =new ObservableCollection<DAT_STOCK_RATING>();

        public ObservableCollection<DAT_STOCK_RATING> ProductReviews
        {
            get => _productReviews;
            set
            {
                _productReviews = value;
                if (_productReviews != null)
                {
                    foreach (var review in _productReviews)
                    {
                        if (double.TryParse(review.Rate, out double rate))
                        {
                            review.Rate = rate.ToString("0.0");
                        }
                    }
                }

                NotifyPropertyChanged(nameof(ProductReviews));
                NotifyPropertyChanged(nameof(HasReviews));
                NotifyPropertyChanged(nameof(RatingSummary));
                NotifyPropertyChanged(nameof(ProductReviewsDisplay));
            }
        }

        private ObservableCollection<ProductReviewDisplay> _productReviewsDisplay
    = new ObservableCollection<ProductReviewDisplay>();

        public ObservableCollection<ProductReviewDisplay> ProductReviewsDisplay
        {
            get => _productReviewsDisplay;
            set
            {
                _productReviewsDisplay = value;

                NotifyPropertyChanged(nameof(ProductReviewsDisplay));
                NotifyPropertyChanged(nameof(HasReviews));
            }
        }
        private ObservableCollection<DAT_STOCK_RELATION> _relatedProducts =new ObservableCollection<DAT_STOCK_RELATION>();

        public ObservableCollection<DAT_STOCK_RELATION> RelatedProducts
        {
            get => _relatedProducts;
            set
            {
                _relatedProducts = value;
                NotifyPropertyChanged(nameof(RelatedProducts));
                NotifyPropertyChanged(nameof(HasRelatedProducts));
            }
        }

        #endregion

        #region "Commands"

        #endregion

        #region "Task"

        #endregion

        #region "Method"
        private void InitializeSelectedAttributes()
        {
            SelectedAttributeTerms.Clear();
            SelectedAttributeDisplay.Clear();

            foreach (var attribute in Attributes)
            {
                var firstTerm = attribute.DAT_STOCK_ATTRIBUTE_TERM?
                    .FirstOrDefault();

                if (firstTerm == null)
                    continue;

                SelectedAttributeTerms[attribute.AttributeName_0_255] = firstTerm;

                SelectedAttributeDisplay.Add(firstTerm);
            }

            // Find price for the default selections
            UpdateSelectedVariation();
        }

        public void UpdateSelectedVariation()
        {
            if (SelectedAttributeTerms == null ||
                SelectedAttributeTerms.Count == 0 ||
                Variations == null ||
                Variations.Count == 0)
            {
                SelectedVariation = null;
                return;
            }

            //string permutationData = string.Join(";",
            //    SelectedAttributeTerms.Select(x =>
            //        $"{x.Key}:{x.Value.AttributeTermName_0_255}"));
            //SelectedVariation = Variations.FirstOrDefault(x =>
            //    string.Equals(
            //        x.PermutationData,
            //        permutationData,
            //        StringComparison.OrdinalIgnoreCase));

            SelectedVariation = Variations.FirstOrDefault(x =>
            {
                if (string.IsNullOrWhiteSpace(x.PermutationData))
                    return false;

                var variationParts = x.PermutationData
                    .Split(';', StringSplitOptions.RemoveEmptyEntries);

                var variationDictionary = variationParts
                    .Select(part => part.Split(':', 2))
                    .Where(parts => parts.Length == 2)
                    .ToDictionary(
                        parts => parts[0].Trim(),
                        parts => parts[1].Trim(),
                        StringComparer.OrdinalIgnoreCase);

                return SelectedAttributeTerms.All(selected =>
                    variationDictionary.TryGetValue(
                        selected.Key.Trim(),
                        out var value)
                    &&
                    string.Equals(
                        value,
                        selected.Value.AttributeTermName_0_255.Trim(),
                        StringComparison.OrdinalIgnoreCase));
            });
        }


        private void LoadProductReviews(IEnumerable<DAT_STOCK_RATING> ratings)
        {
            try
            {
                ProductReviewsDisplay =
                    new ObservableCollection<ProductReviewDisplay>();

                if (ratings == null)
                    return;

                foreach (var rating in ratings)
                {
                    var review = rating.DAT_SHOPPING_REVIEW?
                        .FirstOrDefault();

                    ProductReviewsDisplay.Add(new ProductReviewDisplay
                    {
                        UserName = rating.UserName_0_255,

                        Rate = double.TryParse(rating.Rate, out var rate)
                            ? rate.ToString("0.0")
                            : "0.0",

                        Review = review?.Review ?? "",

                        UserProfile = rating.UserProfile
                    });
                }

                NotifyPropertyChanged(nameof(ProductReviewsDisplay));
                NotifyPropertyChanged(nameof(HasReviews));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"LoadProductReviews Error: {ex}");
            }
        }

        #endregion

        #region "Web Service Api"
        public async Task getInventoryStock(RES_STOCK argRES_STOCK)
        {
            try
            {
                mJSN_REQ_INVENTORY_STOCK_1.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_INVENTORY_STOCK_1.RES_STOCK = new List<RES_STOCK>
                {
                    new RES_STOCK
                    {
                        Ask = argRES_STOCK.Ask
                    }
                };

                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_STOCK_1);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsgetInventoryStock);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_RES_INVENTORY_STOCK_1 = JsonConvert.DeserializeObject<JSN_RES_INVENTORY_STOCK>(mResponse);
                    if (this.mJSN_RES_INVENTORY_STOCK_1.Message.Code == "7")
                    {
                        //RES_STOCK
                        if (this.mJSN_RES_INVENTORY_STOCK_1.RES_STOCK.Count > 0)
                        {
                            Product = mJSN_RES_INVENTORY_STOCK_1.RES_STOCK.FirstOrDefault();
                            WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK_1.Message.Message);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK_1.Message.Message);
                        }
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK_1.Message.Message);
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
        public async Task getInventoryStockDetail(RES_STOCK argRES_STOCK)
        {
            try
            {
                mJSN_REQ_INVENTORY_STOCK.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_INVENTORY_STOCK.RES_STOCK = new List<RES_STOCK> { argRES_STOCK };

                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTORY_STOCK);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsgetInventoryStockDetail);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_RES_INVENTORY_STOCK = JsonConvert.DeserializeObject<JSN_RES_INVENTORY_STOCK>(mResponse);
                    if (this.mJSN_RES_INVENTORY_STOCK.Message.Code == "7")
                    {
                        //RES_STOCK
                        if (this.mJSN_RES_INVENTORY_STOCK.RES_STOCK.Count > 0)
                        {
                            ProductPhotos = new ObservableCollection<RES_STOCK_PHOTO>(mJSN_RES_INVENTORY_STOCK.RES_STOCK_PHOTO);
                            ProductReviews = new ObservableCollection<DAT_STOCK_RATING>(mJSN_RES_INVENTORY_STOCK.DAT_STOCK_RATING);
                            LoadProductReviews(mJSN_RES_INVENTORY_STOCK.DAT_STOCK_RATING);

                            RelatedProducts = new ObservableCollection<DAT_STOCK_RELATION>( mJSN_RES_INVENTORY_STOCK.DAT_STOCK_RELATION);
                            WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK.Message.Message);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK.Message.Message);
                        }
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_INVENTORY_STOCK.Message.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }

        public async Task getInventoryStockSales(RES_STOCK argRES_STOCK)
        {
            try
            {
                mJSN_REQ_INVENTRY_STOCK_NEW.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_INVENTRY_STOCK_NEW.RES_STOCK = new List<RES_STOCK> { argRES_STOCK };

                mRequest = JsonConvert.SerializeObject(mJSN_REQ_INVENTRY_STOCK_NEW);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsgetInventoryStockSales);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_INVENTORY_STOCK = JsonConvert.DeserializeObject<JSN_INVENTORY_STOCK>(mResponse);
                    if (this.mJSN_INVENTORY_STOCK.Message.Code == "7")
                    {
                        //RES_STOCK
                        if (this.mJSN_INVENTORY_STOCK.DAT_STOCK_ATTRIBUTE.Count > 0)
                        {

                            //DAT_STOCK_RATING
                            Attributes = new ObservableCollection<DAT_STOCK_ATTRIBUTE>(mJSN_INVENTORY_STOCK.DAT_STOCK_ATTRIBUTE);

                            Variations = new ObservableCollection<DAT_STOCK_VARIATION>(mJSN_INVENTORY_STOCK.DAT_STOCK_VARIATION);
                            InitializeSelectedAttributes();


                            ProductPhotos = new ObservableCollection<RES_STOCK_PHOTO>(mJSN_INVENTORY_STOCK.RES_STOCK_PHOTO);
                            ProductReviews = new ObservableCollection<DAT_STOCK_RATING>(mJSN_INVENTORY_STOCK.DAT_STOCK_RATING);
                            LoadProductReviews(mJSN_INVENTORY_STOCK.DAT_STOCK_RATING);

                            RelatedProducts = new ObservableCollection<DAT_STOCK_RELATION>(mJSN_INVENTORY_STOCK.DAT_STOCK_RELATION);

                            WeakReferenceMessenger.Default.Send(this.mJSN_INVENTORY_STOCK.Message.Message);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(this.mJSN_INVENTORY_STOCK.Message.Message);
                        }
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_INVENTORY_STOCK.Message.Message);
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

        #endregion
    }

}
