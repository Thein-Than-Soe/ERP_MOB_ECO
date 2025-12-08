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
    public partial class FrmPosSaleQuotationPop : PopupPage
    {
        #region "Declaring"
        public DateTime StartDate { get; set; } = DateTime.Today.AddDays(-7).AddHours(8);
        public DateTime EndDate { get; set; } = DateTime.Today.AddHours(8);

        RES_SALE_QUOTATION mRES_SALE_QUOTATION = new RES_SALE_QUOTATION();
        VmlSalesQuotation mVmlSalesQuotation;
        private TaskCompletionSource<object> _taskCompletionSource;
        public Task<object> PopupClosedTask => _taskCompletionSource.Task;
        #endregion
        public FrmPosSaleQuotationPop()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesQuotation = new VmlSalesQuotation();


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
        public FrmPosSaleQuotationPop(JSN_LOAD_SALE_QUOTATION argJSN_LOAD_SALE_QUOTATION)
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesQuotation = new VmlSalesQuotation();
                mVmlSalesQuotation.bindCustomer(argJSN_LOAD_SALE_QUOTATION.RES_CUSTOMER_DTL);

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
                mRES_SALE_QUOTATION = new RES_SALE_QUOTATION();
                var l_SelectedCustomer = pkrCustomer.SelectedItem as RES_CUSTOMER_DTL;
                if (l_SelectedCustomer != null)
                {
                    mRES_SALE_QUOTATION.CustomerAsk = l_SelectedCustomer.Ask;
                }
                if (entCode.Text != null)
                {
                    mRES_SALE_QUOTATION.QuotationCode_0_50 = entCode.Text;
                }

                if (mRES_SALE_QUOTATION != null)
                {
                    _taskCompletionSource.SetResult(mRES_SALE_QUOTATION);
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