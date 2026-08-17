using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.ViewsModel.ECO;
using CS.ERP_MOB.ViewsModel.Frame;
using RGPopup.Maui.Extensions;
using Syncfusion.Maui.Core.Carousel;

namespace CS.ERP_MOB_ECO.Views.ECO;

public partial class FrmEcoCheckOut : ContentView
{
    private VmlCheckOut vm;
    private bool _isLoaded;

    public FrmEcoCheckOut()
    {
        InitializeComponent();

        vm = new VmlCheckOut();
        BindingContext = vm;

        Loaded += FrmEcoCheckOut_Loaded;
    }

    private async void FrmEcoCheckOut_Loaded(object sender, EventArgs e)
    {
        if (_isLoaded)
            return;

        _isLoaded = true;

        try
        {
            await vm.InitializeAsync();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"========== CHECKOUT LOAD ERROR ==========\n{ex}"
            );
        }
    }

    //item check box
    private void CheckBox_CheckedChanged(  object sender,CheckedChangedEventArgs e)
    {
        if (sender is not CheckBox checkBox)
            return;

        if (checkBox.BindingContext is not RES_SHOPPING_DETAIL item)
            return;

        if (BindingContext is not VmlCheckOut viewModel)
            return;

        viewModel.UpdateItemSelection(
            item,
            e.Value);
    
    }

    // Delivery contact
    private async void EditCustomerContact_Clicked(  object sender,  EventArgs e) {
        try
        {
            if (vm.SelectedCustomerContact == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Customer Contact",
                    "Please select a delivery address first.",
                    "OK");

                return;
            }

            await Navigation.PushAsync( new FrmCustomerContactSet(vm, vm.SelectedCustomerContact)); 
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
    private async void AddCustomerContact_Clicked(  object sender,  EventArgs e)
    {
        try
        {
            await Navigation.PushAsync( new FrmCustomerContactSet(  vm, null));
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private void DeliveryCheckBox_CheckedChanged( object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        if (sender is not CheckBox checkBox)
            return;

        if (checkBox.BindingContext is not RES_STOCK_DELIVERY delivery)
            return;

        vm.SelectDelivery(delivery);

    }

    private async void btn_Payment_Clicked(object sender, EventArgs e)
    {
        try
        {
            RES_PAYMENT_TYPE paymentType = vm.PaymentTypeList[0];

            if (sender is Button button)
            {
                vm.SelectedPaymentType = paymentType;

                await vm.saveCheckOut();

                if (paymentType.Ask == "15")
                {
                    await Clipboard.Default.SetTextAsync(vm.HitPayUrl);

                    await Application.Current.MainPage.DisplayAlert(
                        "HitPay URL",
                        $"{vm.HitPayUrl}\n\nThe URL has been copied to your clipboard.",
                        "OK");
                }
                else
                {
                    await Navigation.PushPopupAsync(new FrmSubscriptionPayment());
                }
            }
            
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
    private async void btn_OtherPayment_Clicked(object sender, EventArgs e)
    {
        try
        {

            if (sender is Button button &&
                button.BindingContext is RES_PAYMENT_TYPE paymentType)
            {
                vm.SelectedPaymentType = paymentType;
                await vm.saveCheckOut();

                if (paymentType.Ask == "15")
                {
                    await Clipboard.Default.SetTextAsync(vm.HitPayUrl);

                    await Application.Current.MainPage.DisplayAlert(
                        "HitPay URL",
                        $"{vm.HitPayUrl}\n\nThe URL has been copied to your clipboard.",
                        "OK");
                }
                else
                {
                    await Navigation.PushPopupAsync(new FrmSubscriptionPayment());
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    private async void ShoppingCard_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (sender is Frame frame &&
                frame.BindingContext is RES_SHOPPING_DETAIL shoppingDetail)
            {
                RES_STOCK mRES_STOCK = new RES_STOCK();
                mRES_STOCK.Ask = shoppingDetail.StockAsk;

                await Navigation.PushAsync( new FrmEcoProductDtl(mRES_STOCK));
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert( "Error",ex.ToString(),"OK");
        }
    }
}
