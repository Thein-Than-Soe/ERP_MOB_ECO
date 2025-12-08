using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.RES;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.Frame;
using CS.ERP_MOB.ViewsModel.POS;
using Microsoft.Maui.Devices;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosSaleInvoicePop : PopupPage
    {
        #region "Declaring"
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7).AddHours(8);
        public DateTime EndDate { get; set; } = DateTime.Today.AddHours(8);

        RES_SALE_INVOICE mRES_SALE_INVOICE = new RES_SALE_INVOICE();
        VmlSalesInvoice mVmlSalesInvoice;
        private TaskCompletionSource<object> _taskCompletionSource;
        public Task<object> PopupClosedTask => _taskCompletionSource.Task;
        #endregion
        public FrmPosSaleInvoicePop()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesInvoice = new VmlSalesInvoice();


                var display = DeviceDisplay.MainDisplayInfo;
                double height = display.Height / display.Density;
                double width = display.Width / display.Density;

                // Set 2/3 height and full width
                PopupFrame.HeightRequest = height * 2 / 3;
                PopupFrame.WidthRequest = width;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public FrmPosSaleInvoicePop(JSN_LOAD_SALE_INVOICE argJSN_LOAD_SALE_INVOICE)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesInvoice = new VmlSalesInvoice();
                mVmlSalesInvoice.bindCustomer(argJSN_LOAD_SALE_INVOICE.RES_CUSTOMER_DTL);

                _taskCompletionSource = new TaskCompletionSource<object>();

                var display = DeviceDisplay.MainDisplayInfo;
                double height = display.Height / display.Density;
                double width = display.Width / display.Density;

                // Set 2/3 height and full width
                PopupFrame.HeightRequest = height * 2 / 3;
                PopupFrame.WidthRequest = width;
            
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async void SearchTappedAsync(object sender, EventArgs e)
        {
            //await PopupNavigation.Instance.PopAsync();

        }

        private async void OnBackgroundTapped(object sender, TappedEventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OkButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                mRES_SALE_INVOICE = new RES_SALE_INVOICE();
                var l_SelectedCustomer = pkrCustomer.SelectedItem as RES_CUSTOMER_DTL;
                if (l_SelectedCustomer != null)
                {
                    mRES_SALE_INVOICE.CustomerAsk = l_SelectedCustomer.Ask;
                }
                if (entCode.Text != null)
                {
                    mRES_SALE_INVOICE.InvoiceCode_0_50 = entCode.Text;
                }

                if (mRES_SALE_INVOICE != null)
                {
                    _taskCompletionSource.SetResult(mRES_SALE_INVOICE);
                }
                else
                {
                    _taskCompletionSource.SetResult(null);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        private async void OnSwipeDown(object sender, SwipedEventArgs e)
        {
            await PopupNavigation.Instance.PopAsync(); 
        }
    }
}