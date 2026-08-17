using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.BOL.POS;
using CS.ERP.PL.ECO.DAT;
using CS.ERP.PL.ECO.REQ;
using CS.ERP.PL.ECO.RES;
using CS.ERP.PL.HCM.DAT;
using CS.ERP.PL.HCM.REQ;
using CS.ERP.PL.HCM.RES;
using CS.ERP.PL.HMS.REQ;
using CS.ERP.PL.HMS.RES;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.ECO;
using CS.ERP_MOB.Services.POS;
using CS.ERP_MOB.Services.SYS;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using RGPopup.Maui.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static CS.ERP_MOB.General.Utility;

namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlCheckOut : BaseViewModel
    {
        #region "Declaring"
        string mRequest = "";
        string mResponse = "";

        RES_SHOPPING ShoppingData;
        //get shopping
        public JSN_REQ_SHOPPING mJSN_REQ_SHOPPING = new JSN_REQ_SHOPPING();
        public JSN_SHOPPING mJSN_SHOPPING = new JSN_SHOPPING();

        //load checkout
        public JSN_REQ_LOAD_CHECKOUT mJSN_REQ_LOAD_CHECKOUT = new JSN_REQ_LOAD_CHECKOUT();
        public JSN_RES_LOAD_CHECKOUT mJSN_RES_LOAD_CHECKOUT = new JSN_RES_LOAD_CHECKOUT();

        //save checkout
        public JSN_REQ_CHECKOUT mJSN_REQ_CHECKOUT = new JSN_REQ_CHECKOUT();
        public JSN_RES_CHECKOUT mJSN_RES_CHECKOUT = new JSN_RES_CHECKOUT();

        //save new contact card
        public JSN_REQ_CUSTOMER_CONTACT mJSN_REQ_CUSTOMER_CONTACT = new JSN_REQ_CUSTOMER_CONTACT();
        public JSN_CUSTOMER_CONTACT mJSN_CUSTOMER_CONTACT = new JSN_CUSTOMER_CONTACT();

        #endregion

        #region "Contructor"
        public VmlCheckOut()
        {
        }

        public async Task InitializeAsync()
        {
            try
            {
                Utility.openLoader();

                await getShopping();

                await loadCheckOut();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"========== INITIALIZATION ERROR ==========\n{ex}"
                );
            }
            finally
            {
                Utility.closeLoader();
            }
        }

        #endregion

        #region "Boolean Declaring"
        public bool HasDiscount => DiscountAmount > 0;

        public bool IsShippingOptionSelected { get; set; }

        #endregion

        #region "Get Set"
        // Country, state, city
        private RES_COUNTRY_DTL _selectedCountry;
        public RES_COUNTRY_DTL SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    NotifyPropertyChanged(nameof(SelectedCountry));
                    // reset downstream when parent changes
                    SelectedState = null;
                    SelectedCity = null;
                }
            }
        }

        private RES_STATE_DTL _selectedState;
        public RES_STATE_DTL SelectedState
        {
            get => _selectedState;
            set
            {
                if (_selectedState != value)
                {
                    _selectedState = value;
                    NotifyPropertyChanged(nameof(SelectedState));
                    // reset city when state changes
                    SelectedCity = null;
                }
            }
        }

        private RES_CITY _selectedCity;
        public RES_CITY SelectedCity
        {
            get => _selectedCity;
            set { if (_selectedCity != value) { _selectedCity = value; NotifyPropertyChanged(nameof(SelectedCity)); } }
        }

        private List<RES_SHOPPING> mShopping;
        public List<RES_SHOPPING> Shopping
        {
            get
            {
                return mShopping;
            }
            set
            {
                mShopping = value;
                NotifyPropertyChanged("Shopping");
            }
        }

        private List<RES_SHOPPING_DETAIL> mShoppingList;
        public List<RES_SHOPPING_DETAIL> ShoppingList
        {
            get
            {
                return mShoppingList;
            }
            set
            {
                mShoppingList = value;
                NotifyPropertyChanged("ShoppingList");
            }
        }

       
        private List<RES_STOCK> mStockList;
        public List<RES_STOCK> StockList
        {
            get
            {
                return mStockList;
            }
            set
            {
                mStockList = value;
                NotifyPropertyChanged("StockList");
            }
        }

       
        // Keeps track of which API model objects are checked
        private readonly HashSet<RES_SHOPPING_DETAIL> _selectedItems
            = new HashSet<RES_SHOPPING_DETAIL>();
        public List<RES_SHOPPING_DETAIL> SelectedShoppingDetails { get; private set; }
    = new List<RES_SHOPPING_DETAIL>();


        //Discount
        private List<DAT_DISCOUNT_RULE> mDiscountRule;
        public List<DAT_DISCOUNT_RULE> DiscountRule
        {
            get
            {
                return mDiscountRule;
            }
            set
            {
                mDiscountRule = value;
                NotifyPropertyChanged("DiscountRule");
            }
        }
        private DAT_DISCOUNT_RULE mSelectedRule;
        public DAT_DISCOUNT_RULE SelectedRule
        {
            get
            {
                return mSelectedRule;
            }
            set
            {
                mSelectedRule = value;
                NotifyPropertyChanged("SelectedRule");
            }
        }

        //Tax Information
        private RES_GST mTaxInformation;
        public RES_GST TaxInformation
        {
            get
            {
                return mTaxInformation;
            }
            set
            {
                mTaxInformation = value;
                NotifyPropertyChanged("TaxInformation");
            }
        }

        // For delivery address and contact details
        private RES_CUSTOMER_DTL mCustomerDetails;
        public RES_CUSTOMER_DTL CustomerDetails
        {
            get
            {
                return mCustomerDetails;
            }
            set
            {
                mCustomerDetails = value;
                NotifyPropertyChanged("CustomerDetails");
            }
        }
        private List<RES_CUSTOMER_CONTACT> mCustomerContactList;
        public List<RES_CUSTOMER_CONTACT> CustomerContactList
        {
            get
            {
                return mCustomerContactList;
            }
            set
            {
                mCustomerContactList = value;
                NotifyPropertyChanged("CustomerContactList");
                NotifyPropertyChanged("HasDeliveryContact");
            }
        }
        private RES_CUSTOMER_CONTACT mSelectedCustomerContact;
        public RES_CUSTOMER_CONTACT SelectedCustomerContact
        {
            get
            {
                return mSelectedCustomerContact;
            }
            set
            {
                mSelectedCustomerContact = value;
                NotifyPropertyChanged("SelectedCustomerContact");
            }
        }

        public List<RES_COUNTRY_DTL> mCountryList;
        public List<RES_COUNTRY_DTL> CountryList
        {
            get { return mCountryList; }
            set { mCountryList = value; NotifyPropertyChanged("CountryList"); }
        }

        public List<RES_CONTACT_TYPE> mContactTypeList;
        public List<RES_CONTACT_TYPE> ContactTypeList
        {
            get { return mContactTypeList; }
            set { mContactTypeList = value; NotifyPropertyChanged("ContactTypeList"); }
        }


        //Shipping
        private List<DAT_SUBSCRIBER_SERVICE> _deliverySubscribers;

        public List<DAT_SUBSCRIBER_SERVICE> DeliverySubscribers
        {
            get => _deliverySubscribers;
            set
            {
                _deliverySubscribers = value;
                NotifyPropertyChanged(nameof(DeliverySubscribers));
            }
        }

        private RES_STOCK_DELIVERY? _selectedDelivery;

        public RES_STOCK_DELIVERY? SelectedDelivery
        {
            get => _selectedDelivery;
            set
            {
                if (_selectedDelivery == value)
                    return;

                _selectedDelivery = value;
                NotifyPropertyChanged("SelectedDelivery");
            }
        }

        //for save
        private DAT_SUBSCRIBER_SERVICE _selectedDeliverySubscriber;

        public DAT_SUBSCRIBER_SERVICE SelectedDeliverySubscriber
        {
            get => _selectedDeliverySubscriber;
            set
            {
                _selectedDeliverySubscriber = value;
                NotifyPropertyChanged(nameof(SelectedDeliverySubscriber));
            }
        }
        

        //Payment
        private RES_PAYMENT_TYPE mSelectedPaymentType;

        public RES_PAYMENT_TYPE SelectedPaymentType
        {
            get => mSelectedPaymentType;
            set
            {
                mSelectedPaymentType = value;
                NotifyPropertyChanged(nameof(SelectedPaymentType));
            }
        }
        private List<RES_PAYMENT_TYPE> mPaymentTypeList;
        public List<RES_PAYMENT_TYPE> PaymentTypeList
        {
            get
            {
                return mPaymentTypeList;
            }
            set
            {
                mPaymentTypeList = value;
                NotifyPropertyChanged("PaymentTypeList");
                NotifyPropertyChanged("FirstPaymentTypeName");
                NotifyPropertyChanged("OtherPaymentTypes");
            }
        }

        #endregion

        #region "Payment methods"
        private string _hitPayUrl;

        public string HitPayUrl
        {
            get => _hitPayUrl;
            set
            {
                if (_hitPayUrl != value)
                {
                    _hitPayUrl = value;
                    NotifyPropertyChanged(nameof(HitPayUrl));
                }
            }
        }

        //List price and subtotal and grand total calculation fields
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
                //NotifyPropertyChanged(nameof(TotalPrice));
                CalculateSubTotal();
            }
        }


        private decimal _subTotal;
        public decimal SubTotal
        {
            get => _subTotal;
            set
            {
                if (_subTotal != value)
                {
                    _subTotal = value;
                    NotifyPropertyChanged("SubTotal");
                    // Recalculate discount whenever subtotal changes
                    CalculateDiscount();
                }
            }
        }
        private decimal _discountAmount;
        public decimal DiscountAmount
        {
            get => _discountAmount;
            set
            {
                if (_discountAmount != value)
                {
                    _discountAmount = value;
                    NotifyPropertyChanged(nameof(DiscountAmount));
                    CalculateGrandTotal();
                }
            }
        }
        private decimal _taxAmount;
        public decimal TaxAmount
        {
            get => _taxAmount;
            set
            {
                if (_taxAmount != value)
                {
                    _taxAmount = value;
                    NotifyPropertyChanged(nameof(TaxAmount));
                }
            }
        }

        private decimal _deliveryFee;
        public decimal DeliveryFee
        {
            get => _deliveryFee;
            set
            {
                if (_deliveryFee != value)
                {
                    _deliveryFee = value;
                    NotifyPropertyChanged(nameof(DeliveryFee));
                    CalculateTax();
                    CalculateGrandTotal();
                }
            }
        }

        private decimal _grandTotal;
        public decimal GrandTotal
        {
            get => _grandTotal;
            set
            {
                if (_grandTotal != value)
                {
                    _grandTotal = value;
                    NotifyPropertyChanged(nameof(GrandTotal));
                }
            }
        }


        //payment
        public string FirstPaymentTypeName => PaymentTypeList?.FirstOrDefault()?.PaymentTypeName_0_255 ?? "";

        public List<RES_PAYMENT_TYPE> OtherPaymentTypes => PaymentTypeList?.Skip(1).ToList() ?? new List<RES_PAYMENT_TYPE>();

        public void UpdateItemSelection(RES_SHOPPING_DETAIL item,bool isSelected)
        {
            if (isSelected)
            {
                _selectedItems.Add(item);
            }
            else
            {
                _selectedItems.Remove(item);
            }
            // Synchronize the list with the HashSet
            SelectedShoppingDetails = _selectedItems.ToList();
            CalculateSubTotal();
        }
        private void CalculateSubTotal()
        {
            decimal subtotal = 0;

            foreach (var item in _selectedItems)
            {
                if (decimal.TryParse(
                        item.TotalAmount,
                        NumberStyles.Any,
                        CultureInfo.InvariantCulture,
                        out decimal amount))
                {
                    subtotal += amount;
                }
            }
            SubTotal = subtotal;

            CalculateDiscount();

            CalculateTax();

            CalculateGrandTotal();
        }
        private decimal ParseDecimal(string? value)
        {
            if (decimal.TryParse(
                value,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out decimal result))
            {
                return result;
            }

            return 0;
        }
        private void CalculateDiscount()
        {
            // Reset first
            DiscountAmount = 0;

            if (DiscountRule == null || DiscountRule.Count == 0)
                return;

            if (SubTotal <= 0)
                return;

            // FIND ALL RULES THAT MATCH CURRENT SUBTOTAL
            var matchedRules = DiscountRule
                .Where(rule => IsDiscountRuleMatched(rule, SubTotal))
                .OrderByDescending(rule =>
                    ParseDecimal(rule.DiscountCalculationAmount))
                .ToList();


            // No matching rule
            if (matchedRules.Count == 0)
                return;

            SelectedRule = matchedRules.First();

            decimal rate = ParseDecimal(SelectedRule.Rate);


            // DiscountTypeAsk:
            //
            // 1 = Percentage
            // 2 = Fixed amount
            // 3 = Coupon
            //
            // Based on your sample:
            //
            // R4 -> 1 -> %
            // R5 -> 2 -> $
            // R6 -> 3 -> Coupon
            //

            switch (SelectedRule.DiscountTypeAsk)
            {
                // Percentage
                case "1":
                    DiscountAmount = SubTotal * rate / 100m;
                    break;

                // Fixed amount
                case "2":
                    DiscountAmount = rate;
                    break;


                // Coupon
                case "3":
                    // Your API has "Cu" as the type.
                    // If this coupon is already a fixed amount:
                    DiscountAmount = rate;
                    break;


                default:

                    DiscountAmount = 0;

                    break;
            }
            NotifyPropertyChanged("HasDiscount");

            // Never allow discount greater than subtotal
            if (DiscountAmount > SubTotal)
            {
                DiscountAmount = SubTotal;
            }

            NotifyPropertyChanged(nameof(DiscountAmount));
            NotifyPropertyChanged(nameof(HasDiscount));
        }
        private bool IsDiscountRuleMatched( DAT_DISCOUNT_RULE rule,decimal subtotal)
        {
            decimal conditionAmount = ParseDecimal(rule.DiscountCalculationAmount);


            switch (rule.DiscountConditionTypeName_0_255?.Trim())
            {
                case ">=":
                    return subtotal >= conditionAmount;

                case ">":
                    return subtotal > conditionAmount;

                case "=":
                case "==":
                    return subtotal == conditionAmount;

                case "<=":
                    return subtotal <= conditionAmount;

                case "<":
                    return subtotal < conditionAmount;

                default:
                    return false;
            }
        }
        private void CalculateTax()
        {
            TaxAmount = 0;

            if (TaxInformation == null)
            {
                return;
            }

            // Get tax rate
            var taxInfo = TaxInformation;

            if (taxInfo == null)
            {
                return;
            }

            decimal taxRate = ParseDecimal(taxInfo.GSTRate);

            if (taxRate <= 0)
            {
                return;
            }

            decimal taxableAmount =SubTotal - DiscountAmount + DeliveryFee;


            if (taxableAmount <= 0)
            {
                TaxAmount = 0;
                return;
            }

            TaxAmount = taxableAmount * taxRate / 100m;
            CalculateGrandTotal();
        }
        private void CalculateGrandTotal()
        {
            decimal total = SubTotal - DiscountAmount + DeliveryFee + TaxAmount;

            if (total < 0)
            {
                total = 0;
            }

            GrandTotal = total;
        }

        //shipping method
        private void SetDefaultDelivery()
        {
            if (DeliverySubscribers == null || DeliverySubscribers.Count == 0)
                return;

            // Make everything unchecked first
            foreach (var subscriber in DeliverySubscribers)
            {
                if (subscriber.RES_STOCK_DELIVERY == null)
                    continue;

                foreach (var delivery in subscriber.RES_STOCK_DELIVERY)
                {
                    delivery.IsChecked = "0";
                }
            }

            // Find first available delivery
            foreach (var subscriber in DeliverySubscribers)
            {
                if (subscriber.RES_STOCK_DELIVERY == null)
                    continue;

                var firstDelivery = subscriber.RES_STOCK_DELIVERY.FirstOrDefault();

                if (firstDelivery == null)
                    continue;

                // Select it
                firstDelivery.IsChecked = "1";

                // Keep the selected delivery
                SelectedDelivery = firstDelivery;

                // Keep the selected subscriber + its information
                SelectedDeliverySubscriber = subscriber;

                // Delivery fee
                DeliveryFee = ParseDecimal(firstDelivery.DeliveryFee);

                break;
            }

            NotifyPropertyChanged(nameof(DeliverySubscribers));
        }
        public void SelectDelivery(RES_STOCK_DELIVERY selectedDelivery)
        {
            if (selectedDelivery == null)
                return;

            if (DeliverySubscribers == null || DeliverySubscribers.Count == 0)
                return;

            DAT_SUBSCRIBER_SERVICE selectedSubscriber = null;

            // Find the subscriber that owns this delivery
            foreach (var subscriber in DeliverySubscribers)
            {
                if (subscriber.RES_STOCK_DELIVERY == null)
                    continue;

                if (subscriber.RES_STOCK_DELIVERY.Contains(selectedDelivery))
                {
                    selectedSubscriber = subscriber;
                    break;
                }
            }

            if (selectedSubscriber == null)
                return;

            // Uncheck every delivery
            foreach (var subscriber in DeliverySubscribers)
            {
                if (subscriber.RES_STOCK_DELIVERY == null)
                    continue;

                foreach (var delivery in subscriber.RES_STOCK_DELIVERY)
                {
                    delivery.IsChecked = "0";
                }
            }

            // Check the newly selected delivery
            selectedDelivery.IsChecked = "1";

            // Keep selected delivery and subscriber separately
            SelectedDelivery = selectedDelivery;
            SelectedDeliverySubscriber = selectedSubscriber;

            // Update fee
            DeliveryFee = ParseDecimal(selectedDelivery.DeliveryFee);

            // Refresh UI
            NotifyPropertyChanged(nameof(DeliverySubscribers));
            NotifyPropertyChanged(nameof(SelectedDeliverySubscriber));
            NotifyPropertyChanged(nameof(SelectedDelivery));
        }

        #endregion

        #region "Commands"
        private ICommand mRefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                if (mRefreshCommand == null)
                {
                    mRefreshCommand = new Command(() => {
                        this.loadCheckOut();
                    });
                }
                return mRefreshCommand;
            }
        }
        #endregion

        #region "Task"

        #endregion

        #region "Databind Method"
        

        //data bind
        private void bindSaveCheckOutData()
        {
            try
            {
                //Header assign RES_SALE_ORDER
                RES_SALE_ORDER mRES_SALE_ORDER = new RES_SALE_ORDER();
                RES_SALE_INVOICE mRES_SALE_INVOICE = new RES_SALE_INVOICE();
                RES_SALE_PAYMENT mRES_SALE_PAYMENT = new RES_SALE_PAYMENT();

                mRES_SALE_ORDER.Subtotal = SubTotal.ToString();
                mRES_SALE_ORDER.DiscountTypeAsk = SelectedRule?.DiscountTypeAsk ?? "0";
                mRES_SALE_ORDER.DiscountAmount = DiscountAmount.ToString();
                mRES_SALE_ORDER.GSTAsk = TaxInformation.Ask;
                mRES_SALE_ORDER.GSTRate = TaxInformation.GSTRate;
                mRES_SALE_ORDER.GSTAmount = TaxAmount.ToString();
                mRES_SALE_ORDER.GrandTotal = GrandTotal.ToString();
                mRES_SALE_ORDER.CustomerAsk = CustomerDetails.Ask;
                mRES_SALE_ORDER.CompanyAsk = TaxInformation.CompanyAsk;
                mRES_SALE_ORDER.OutstandingAmount = GrandTotal.ToString();
                mRES_SALE_ORDER.SettleAmount = "0";

                mRES_SALE_INVOICE.Subtotal = SubTotal.ToString();
                mRES_SALE_INVOICE.DiscountTypeAsk = SelectedRule?.DiscountTypeAsk ?? "0";
                mRES_SALE_INVOICE.DiscountAmount = DiscountAmount.ToString();
                mRES_SALE_INVOICE.GSTAsk = TaxInformation.Ask;
                mRES_SALE_INVOICE.GSTRate = TaxInformation.GSTRate;
                mRES_SALE_INVOICE.GSTAmount = TaxAmount.ToString();
                mRES_SALE_INVOICE.GrandTotal = GrandTotal.ToString();
                mRES_SALE_INVOICE.CustomerAsk = CustomerDetails.Ask;
                mRES_SALE_INVOICE.CompanyAsk = TaxInformation.CompanyAsk;
                mRES_SALE_INVOICE.OutstandingAmount = GrandTotal.ToString();
                mRES_SALE_INVOICE.SettleAmount = "0";

                mRES_SALE_PAYMENT.Subtotal = SubTotal.ToString();
                mRES_SALE_PAYMENT.DiscountTypeAsk = SelectedRule?.DiscountTypeAsk ?? "0";
                mRES_SALE_PAYMENT.DiscountAmount = DiscountAmount.ToString();
                mRES_SALE_PAYMENT.GSTAsk = TaxInformation.Ask;
                mRES_SALE_PAYMENT.GSTRate = TaxInformation.GSTRate;
                mRES_SALE_PAYMENT.GSTAmount = TaxAmount.ToString();
                mRES_SALE_PAYMENT.GrandTotal = GrandTotal.ToString();
                mRES_SALE_PAYMENT.CustomerAsk = CustomerDetails.Ask;
                mRES_SALE_PAYMENT.CompanyAsk = TaxInformation.CompanyAsk;
                mRES_SALE_PAYMENT.OutstandingAmount = GrandTotal.ToString();
                mRES_SALE_PAYMENT.SettleAmount = "0";
                mRES_SALE_PAYMENT.PaymentTypeAsk = SelectedPaymentType.Ask;
                mRES_SALE_PAYMENT.DepositAmount = mRES_SALE_PAYMENT.GrandTotal;


                // Detail assign RES_SALE_ORDER_DETAIL and Sale invoice
                List<RES_SALE_ORDER_DETAIL> mRES_SALE_ORDER_DETAIL = new List<RES_SALE_ORDER_DETAIL>();
                List<RES_SALE_INVOICE_DETAIL> mRES_SALE_INVOICE_DETAIL = new List<RES_SALE_INVOICE_DETAIL>();

                for (int i = 0; i < SelectedShoppingDetails.Count; i++)
                {
                        var shoppingDetail = SelectedShoppingDetails[i];

                        // =========================
                        // Sale Order Detail
                        // =========================

                        var orderDetail = new RES_SALE_ORDER_DETAIL();

                        orderDetail.StockAsk = shoppingDetail.Ask;
                        orderDetail.StockCode_0_50 = shoppingDetail.StockCode_0_50;
                        orderDetail.StockName_0_255 = shoppingDetail.StockName_0_255;
                        orderDetail.Price = shoppingDetail.Price;
                        orderDetail.QTY = shoppingDetail.QTY;
                        orderDetail.TotalAmount = shoppingDetail.TotalAmount;
                        orderDetail.StockPhotoURL = shoppingDetail.StockPhotoURL;
                        orderDetail.DiscountTypeAsk = shoppingDetail.DiscountTypeAsk;
                        orderDetail.DiscountRate = shoppingDetail.DiscountRate;
                        orderDetail.DiscountAmount = shoppingDetail.DiscountAmount;
                        orderDetail.CurrencyAsk = shoppingDetail.CurrencyAsk;
                        orderDetail.Cost = shoppingDetail.Price;
                        orderDetail.TotalCost = shoppingDetail.TotalAmount;

                        mRES_SALE_ORDER_DETAIL.Add(orderDetail);


                        // =========================
                        // Sale Invoice Detail
                        // =========================

                        var invoiceDetail = new RES_SALE_INVOICE_DETAIL();

                        invoiceDetail.StockAsk = shoppingDetail.Ask;
                        invoiceDetail.StockCode_0_50 = shoppingDetail.StockCode_0_50;
                        invoiceDetail.StockName_0_255 = shoppingDetail.StockName_0_255;
                        invoiceDetail.Price = shoppingDetail.Price;
                        invoiceDetail.QTY = shoppingDetail.QTY;
                        invoiceDetail.TotalAmount = shoppingDetail.TotalAmount;
                        invoiceDetail.DiscountTypeAsk = shoppingDetail.DiscountTypeAsk;
                        invoiceDetail.DiscountRate = shoppingDetail.DiscountRate;
                        invoiceDetail.DiscountAmount = shoppingDetail.DiscountAmount;
                        invoiceDetail.CurrencyAsk = shoppingDetail.CurrencyAsk;
                        invoiceDetail.Cost = shoppingDetail.Price;
                        invoiceDetail.TotalCost = shoppingDetail.TotalAmount;

                        mRES_SALE_INVOICE_DETAIL.Add(invoiceDetail);
                    
                }

                RES_SERVICE_DETAIL mRES_SERVICE_DETAIL = new RES_SERVICE_DETAIL();
                for (int i = 0; i < StockList.Count; i++)
                {
                    if (StockList[i].TypeAsk == "8")
                    {
                        mRES_SERVICE_DETAIL.StockAsk = StockList[i].Ask;
                        mRES_SERVICE_DETAIL.StockCode_0_50 = StockList[i].StockCode_0_50;
                        mRES_SERVICE_DETAIL.StockName_0_255 = StockList[i].StockName_0_255;
                        mRES_SERVICE_DETAIL.Price = SelectedDelivery.DeliveryFee;
                        mRES_SERVICE_DETAIL.QTY = "1";
                        mRES_SERVICE_DETAIL.UOMAsk = StockList[i].UOMAsk;
                        mRES_SERVICE_DETAIL.TotalAmount = SelectedDelivery.DeliveryFee;
                        break;
                    }
                }

                RES_SERVICE mRES_SERVICE = new RES_SERVICE();
                mRES_SERVICE.SubTotal = SelectedDelivery.DeliveryFee;
                mRES_SERVICE.GrandTotal = SelectedDelivery.DeliveryFee;
                mRES_SERVICE.CustomerAsk = CustomerDetails.Ask;


                mJSN_REQ_CHECKOUT.RES_SALE_ORDER = mRES_SALE_ORDER;
                mJSN_REQ_CHECKOUT.RES_SALE_ORDER_DETAIL = mRES_SALE_ORDER_DETAIL;
                mJSN_REQ_CHECKOUT.RES_SALE_PAYMENT = new List<RES_SALE_PAYMENT> { mRES_SALE_PAYMENT };

                mJSN_REQ_CHECKOUT.RES_SALE_INVOICE = mRES_SALE_INVOICE;
                mJSN_REQ_CHECKOUT.RES_SALE_INVOICE_DETAIL = mRES_SALE_INVOICE_DETAIL;


                mJSN_REQ_CHECKOUT.RES_SERVICE = mRES_SERVICE;
                mJSN_REQ_CHECKOUT.RES_SERVICE_DETAIL = new List<RES_SERVICE_DETAIL> { mRES_SERVICE_DETAIL };
                mJSN_REQ_CHECKOUT.RES_CUSTOMER_DTL = CustomerDetails;
                mJSN_REQ_CHECKOUT.RES_CUSTOMER_DTL.RES_CUSTOMER_CONTACT = new List<RES_CUSTOMER_CONTACT> { SelectedCustomerContact };
                SelectedDeliverySubscriber.RES_STOCK_DELIVERY = new List<RES_STOCK_DELIVERY> { SelectedDelivery };
                mJSN_REQ_CHECKOUT.DAT_SUBSCRIBER_SERVICE = new List<DAT_SUBSCRIBER_SERVICE> { SelectedDeliverySubscriber };


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Web Service Api"
        public async Task getShopping()
        {
            try
            {
                mJSN_REQ_SHOPPING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_SHOPPING.RES_SHOPPING = new RES_SHOPPING();
                mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = new List<RES_SHOPPING_DETAIL>();
        
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_SHOPPING);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsgetShopping);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_SHOPPING = JsonConvert.DeserializeObject<JSN_SHOPPING>(mResponse);

                    if (this.mJSN_SHOPPING.Message.Code == "7")
                    {
                        if (this.mJSN_SHOPPING.RES_SHOPPING_DETAIL.Count > 0)
                        {

                            Shopping = mJSN_SHOPPING.RES_SHOPPING;
                            ShoppingList = mJSN_SHOPPING.RES_SHOPPING_DETAIL;
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
        public async Task loadCheckOut()
        {
            try
            {
                Utility.openLoader();
                mJSN_REQ_LOAD_CHECKOUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_LOAD_CHECKOUT.RES_SHOPPING = Shopping?.FirstOrDefault();
                mJSN_REQ_LOAD_CHECKOUT.RES_SHOPPING_DETAIL =
                    ShoppingList ?? new List<RES_SHOPPING_DETAIL>();
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_LOAD_CHECKOUT);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsloadCheckOut);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_RES_LOAD_CHECKOUT = JsonConvert.DeserializeObject<JSN_RES_LOAD_CHECKOUT>(mResponse);
                    if (this.mJSN_RES_LOAD_CHECKOUT.Message.Code == "7")
                    {
                        //ShoppingList = mJSN_RES_LOAD_CHECKOUT.RES_SHOPPING_DETAIL;
                        TaxInformation = mJSN_RES_LOAD_CHECKOUT.RES_GST.FirstOrDefault();
                        CustomerDetails = mJSN_RES_LOAD_CHECKOUT.RES_CUSTOMER_DTL;
                        StockList = mJSN_RES_LOAD_CHECKOUT.RES_STOCK;
                        CustomerContactList = mJSN_RES_LOAD_CHECKOUT.RES_CUSTOMER_DTL.RES_CUSTOMER_CONTACT;
                        SelectedCustomerContact = mJSN_RES_LOAD_CHECKOUT.RES_CUSTOMER_DTL.RES_CUSTOMER_CONTACT.FirstOrDefault();
                        PaymentTypeList = mJSN_RES_LOAD_CHECKOUT.RES_PAYMENT_TYPE;
                        //DeliverySubscribers = mJSN_RES_LOAD_CHECKOUT.DAT_SUBSCRIBER_SERVICE;

                        DeliverySubscribers = mJSN_RES_LOAD_CHECKOUT.RES_STOCK_DELIVERY;
                        SetDefaultDelivery();
                        NotifyPropertyChanged(nameof(DeliverySubscribers));

                        DiscountRule = mJSN_RES_LOAD_CHECKOUT.DAT_DISCOUNT_RULE;

                        CountryList = mJSN_RES_LOAD_CHECKOUT.RES_COUNTRY_DTL;
                        ContactTypeList = mJSN_RES_LOAD_CHECKOUT.RES_CONTACT_TYPE;

                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_LOAD_CHECKOUT.Message.Message);
                        
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_LOAD_CHECKOUT.Message.Message);
                    }

                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
                Utility.closeLoader();
            }
        }
        public async Task saveCheckOut()
        {
            try
            {
                Utility.openLoader();
                mJSN_REQ_CHECKOUT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_CHECKOUT.REQ_AUTHORIZATION.TranDateTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

                bindSaveCheckOutData();
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CHECKOUT);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wsloadCheckOut);
                if (mResponse != null && mResponse != "")
                {
                    this.mJSN_RES_CHECKOUT = JsonConvert.DeserializeObject<JSN_RES_CHECKOUT>(mResponse);
                    if (this.mJSN_RES_CHECKOUT.Message.Code == "7")
                    {
                        //ShoppingList = mJSN_RES_LOAD_CHECKOUT.RES_SHOPPING_DETAIL;

                        //HitPayUrl = mJSN_RES_CHECKOUT.RES_SALE_PAYMENT[0].HitPayURL;
                        HitPayUrl = mJSN_RES_CHECKOUT.RES_SALE_PAYMENT != null &&
                                     mJSN_RES_CHECKOUT.RES_SALE_PAYMENT.Count > 0
                                ? mJSN_RES_CHECKOUT.RES_SALE_PAYMENT[0].HitPayURL ?? ""
                                : "";

                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_CHECKOUT.Message.Message);
                        Utility.closeLoader();

                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_RES_CHECKOUT.Message.Message);
                        Utility.closeLoader();
                    }

                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                    Utility.closeLoader();
                }
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                WeakReferenceMessenger.Default.Send(this.mJSN_RES_CHECKOUT.Message.Message);
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task saveCustomerContact(RES_CUSTOMER_CONTACT argRES_CUSTOMER_CONTACT)
        {
            try
            {
                Utility.openLoader();
                mJSN_REQ_CUSTOMER_CONTACT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mJSN_REQ_CUSTOMER_CONTACT.RES_CUSTOMER_CONTACT = new List<RES_CUSTOMER_CONTACT> { argRES_CUSTOMER_CONTACT };
                mRequest = JsonConvert.SerializeObject(mJSN_REQ_CUSTOMER_CONTACT);
                mResponse = await Eco_Service.ApiCall(mRequest, Eco_Name.wssaveCustomerContact);
                if (mResponse != null || mResponse != "")
                {
                    this.mJSN_CUSTOMER_CONTACT = JsonConvert.DeserializeObject<JSN_CUSTOMER_CONTACT>(mResponse);
                    if (mJSN_CUSTOMER_CONTACT.Message.Code == "7")
                    {
                        if (argRES_CUSTOMER_CONTACT.StatusAsk == "1")
                        {
                            RES_CUSTOMER_CONTACT l_RES_CUSTOMER_CONTACT = mJSN_CUSTOMER_CONTACT.RES_CUSTOMER_CONTACT[0];

                            if (argRES_CUSTOMER_CONTACT.StatusAsk == "1" && argRES_CUSTOMER_CONTACT.Ask == "0")
                            {
                                CustomerContactList.Add(l_RES_CUSTOMER_CONTACT);
                                SelectedCustomerContact = l_RES_CUSTOMER_CONTACT;
                                NotifyPropertyChanged("SelectedCustomerContact");
                            }
                            else
                            {
                                for (int i = 0; i < CustomerContactList.Count; i++)
                                {
                                    if (CustomerContactList[i].Ask == l_RES_CUSTOMER_CONTACT.Ask)
                                    {
                                        CustomerContactList.RemoveAt(i);
                                        CustomerContactList.Insert(i, l_RES_CUSTOMER_CONTACT);
                                        SelectedCustomerContact = l_RES_CUSTOMER_CONTACT;
                                        NotifyPropertyChanged("SelectedCustomerContact");
                                        await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Success", "Saved successfully.", "OK");
                                        Utility.closeLoader();
                                        break;
                                    }
                                }
                            }
                        }
                        else if (argRES_CUSTOMER_CONTACT.StatusAsk == "6")
                        {
                            for (int i = 0; i < CustomerContactList.Count; i++)
                            {
                                if (CustomerContactList[i].Ask == argRES_CUSTOMER_CONTACT.Ask)
                                {
                                    CustomerContactList.RemoveAt(i); 
                                    NotifyPropertyChanged("SelectedCustomerContact");
                                    await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert("Success", "Delected successfully.", "OK");
                                    Utility.closeLoader();
                                    break;
                                }
                            }
                        }
                        
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("DAT.ErrWebService"));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

    }
}
