
using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.RES;
using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.POS
{
    public partial class FrmPosSaleInvoiceSet : ContentPage
    {
        private RES_SALE_INVOICE invoice;
        public FrmPosSaleInvoiceSet()
        {
            InitializeComponent();
            invoice = new RES_SALE_INVOICE();

            // Optionally bind to the UI
            BindingContext = invoice;
        }
        public FrmPosSaleInvoiceSet(RES_SALE_INVOICE selectedInvoice)
        {
            InitializeComponent();
            invoice = selectedInvoice;

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
