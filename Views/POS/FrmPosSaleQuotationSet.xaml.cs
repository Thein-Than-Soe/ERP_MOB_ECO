
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.RES;
using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.POS
{
    public partial class FrmPosSaleQuotationSet : ContentPage
    {
        private RES_SALE_QUOTATION invoice;
        public FrmPosSaleQuotationSet()
        {
            InitializeComponent();
            invoice = new RES_SALE_QUOTATION();

            // Optionally bind to the UI
            BindingContext = invoice;
        }
        public FrmPosSaleQuotationSet(RES_SALE_QUOTATION selectedQuotation)
        {
            InitializeComponent();
            invoice = selectedQuotation;

            // Optionally bind to the UI
            BindingContext = invoice;
        }
        private async void OnSwipeRight(object sender, SwipedEventArgs e)
        {
            await this.TranslateTo(100, 0, 150);
            await Navigation.PopAsync(); // Go back to previous page
        }
    }
}
