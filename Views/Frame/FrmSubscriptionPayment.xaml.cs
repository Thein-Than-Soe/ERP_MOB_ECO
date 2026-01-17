using CS.ERP.PL.SYS.DAT;
using RGPopup.Maui.Pages;
using Stripe;
using Application = Microsoft.Maui.Controls.Application;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSubscriptionPayment : PopupPage
    {
        #region "Declaration"
        private RES_SUB_PAYMENT mRES_SUB_PAYMENT = new RES_SUB_PAYMENT();
        private RES_SUB_PLAN mRES_SUB_PLAN = new RES_SUB_PLAN();
        public PaymentIntent mPaymentIntent;

        public Token StripeToken;
        public TokenService TokenService;
        //public string StripeApiPublicKey = "pk_test_gWlQQePi5MiS7YIk4ZdTuSSi00UteQcJP5";

        
        #endregion
        #region "Constructor"
        public FrmSubscriptionPayment()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmSubscriptionPayment(RES_SUB_PAYMENT argRES_SUB_PAYMENT, RES_SUB_PLAN argRES_SUB_PLAN)
        {
            try
            {
                InitializeComponent();
                mRES_SUB_PAYMENT = argRES_SUB_PAYMENT;
                mRES_SUB_PLAN = argRES_SUB_PLAN;
                if (mRES_SUB_PAYMENT != null)
                {
                    btnPay.Text = "Pay " + mRES_SUB_PAYMENT.CurrencyCode_0_50 + mRES_SUB_PAYMENT.DepositAmount;
                    lblCustomerName.Text = mRES_SUB_PAYMENT.CustomerName_0_255;
                    lblEmail.Text = mRES_SUB_PAYMENT.ContactEmail;
                }

                if (mRES_SUB_PLAN != null)
                {
                    lblPlanName.Text = mRES_SUB_PLAN.SubscriberPlanName_0_255;
                    lblDurationName.Text = mRES_SUB_PLAN.DurationName_0_255;
                    lblPriceName.Text = mRES_SUB_PLAN.PriceName_0_255;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Private"
        public bool paySubscriptionFee()
        {
            try
            {
                StripeConfiguration.ApiKey = mRES_SUB_PAYMENT.ReferenceDetail;//StripeApiPublicKey
                string secretKey = mRES_SUB_PAYMENT.ReferenceNo;
                string customerName = mRES_SUB_PAYMENT.CustomerName_0_255;
                string customerEmail = mRES_SUB_PAYMENT.ContactEmail;
                string paymentIntentId = mRES_SUB_PAYMENT.TransactionNo;
                string paymentMethodId = getPaymentMethodId();
                //StripeConfiguration.ApiKey = StripeApiPublicKey;
                // To create a PaymentIntent for confirmation, see our guide at: https://stripe.com/docs/payments/payment-intents/creating-payment-intents#creating-for-automatic
                var options = new PaymentIntentConfirmOptions
                {
                    ClientSecret = mPaymentIntent.ClientSecret,
                    PaymentMethod = paymentMethodId,
                };
                var service = new PaymentIntentService();
                //PaymentIntent pmintent = service.Confirm(paymentIntentId, options);
                PaymentIntent pmintent = service.Confirm(mRES_SUB_PAYMENT.TransactionNo, options);
                return true;
              
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public string getPaymentMethodId()
        {
            try
            {
                //StripeConfiguration.ApiKey = StripeApiPublicKey;
                var options = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    //Card = new PaymentMethodCardOptions
                    //{
                    //    Number = entCreditCardNumber.Text,
                    //    ExpMonth = long.Parse(entExpiryMonth.Text),
                    //    ExpYear = long.Parse(entExpiryYear.Text),
                    //    Cvc = entCVV.Text,
                    //},
                    //BillingDetails = new PaymentMethodBillingDetailsOptions
                    //{
                    //    Name = mRES_SUB_PAYMENT.CustomerName_0_255,
                    //    Email = mRES_SUB_PAYMENT.ContactEmail
                    //}
                };
                var service = new PaymentMethodService();
                PaymentMethod pm = service.Create(options);
                return pm.Id;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Event"
        private async void btnPay_onClicked(object sender, EventArgs e)
        {
            try
            {
                bool IsSuccess = paySubscriptionFee();
                if (IsSuccess)
                {
                    MessagingCenter.Send<Application, bool>(Application.Current, "PaymentDialog", true);
                    await PopupNavigation.Instance.PopAllAsync();
                }
                else
                {
                    MessagingCenter.Send<Application, bool>(Application.Current, "PaymentDialog", false);
                    await PopupNavigation.Instance.PopAllAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void TgrClose_Tapped(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<Application, bool>(Application.Current, "PaymentDialog", false);
                await PopupNavigation.Instance.PopAllAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

       
        #endregion
        //public string GetPaymentIntentId()
        //{
        //    StripeConfiguration.ApiKey = "sk_test_egLMSnyJFREy9fjmctDshcYT00B5YaZ7MM";

        //    try
        //    {
        //        var options = new PaymentIntentCreateOptions
        //        {
        //            Amount = 2000,
        //            Currency = "usd",
        //            PaymentMethodTypes = new System.Collections.Generic.List<string>
        //        {
        //        "card",
        //        },
        //        };
        //        var service = new PaymentIntentService();
        //        paymentIntent = service.Create(options);
        //        return paymentIntent.Id;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //}

        //public async Task PaymentAsync()
        //{
        //    bool IsTransactionSuccess = false;
        //    PayButton.Text = "Processing...";
        //    CancellationTokenSource tokenSource = new CancellationTokenSource();
        //    try
        //    {
        //        await Task.Run(async () =>
        //        {
        //            var Token = CreateToken();
        //            if (Token != null)
        //            {
        //                //long amount = long.Parse(SubPayment.DepositAmount);
        //                long amount = (long)Convert.ToDouble(SubPayment.DepositAmount);
        //                string privateApikey = "sk_test_egLMSnyJFREy9fjmctDshcYT00B5YaZ7MM";
        //                string customerName = SubPayment.CustomerName;
        //                string customerEmail = SubPayment.ContactEmail;
        //                IsTransactionSuccess = MakePayment(privateApikey, amount, customerName, customerEmail);
        //                if (IsTransactionSuccess)
        //                {
        //                    MessagingCenter.Send<Application, bool>(Application.Current, "PaymentDialog", true);
        //                }

        //                //await PopupNavigation.Instance.PopAllAsync();
        //            }
        //        });
        //    }
        //    catch (Exception e)
        //    {
        //        MessagingCenter.Send<Application, bool>(Application.Current, "PaymentDialog", false);
        //        //await PopupNavigation.Instance.PopAllAsync();
        //    }

        //}

        //public string GetToken()
        //{
        //    StripeConfiguration.ApiKey = StripeApiPublicKey;

        //    var options = new PaymentMethodCreateOptions
        //    {
        //        Type = "card",
        //        Card = new PaymentMethodCardOptions
        //        {
        //            Number = "4242424242424242",
        //            ExpMonth = 7,
        //            ExpYear = 2022,
        //            Cvc = "314",
        //        },
        //    };
        //    var service = new PaymentMethodService();
        //    PaymentMethod paymentMethod =  service.Create(options);
        //    return paymentMethod.Id;
        //}

        //public bool MakePayment(string privateKey, long amount, string customer, string email)
        //{
        //    try
        //    {
        //        StripeConfiguration.SetApiKey(privateKey);
        //        var Option = new ChargeCreateOptions
        //        {
        //            Amount = amount,
        //            Currency = "inr",
        //            ReceiptEmail = "cnk@ovaspace.com",
        //            Capture = true,
        //            Source = StripeToken.Id

        //        };
        //        //Make payment
        //        var service = new ChargeService();
        //        Charge charge = service.Create(Option);
        //        return true;

        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        //public string CreateToken()
        //{
        //    try
        //    {

        //        StripeConfiguration.SetApiKey(StripeApiPublicKey);

        //        var tokenOptions = new TokenCreateOptions()
        //        {
        //            Card = new TokenCardOptions()
        //            {
        //                Number = CreditCardNumberInput.Text,
        //                ExpYear = long.Parse(ExpiryYearInput.Text),
        //                ExpMonth = long.Parse(ExpiryMonthInput.Text),
        //                Cvc = CVVInput.Text
        //            }
        //        };

        //        TokenService = new TokenService();
        //        StripeToken = TokenService.Create(tokenOptions);

        //        return StripeToken.Id; // This is the token

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}