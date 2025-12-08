using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.ViewsModel.Frame;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopProduct : PopupPage
    {
        #region "Declaring"
        #endregion
        #region "Constructor"
        public PopProduct()
        {
            try
            {
                InitializeComponent();
                BindingContext = new VmlProduct(this);
                if (Application.Current.MainPage != null)
                {
                    double screenWidth = Application.Current.MainPage.Width;
                    int paddingLeft = (int)(screenWidth / 5);
                    paddingLeft = paddingLeft / 2;
                    grdProduct.Padding = new Thickness(paddingLeft, 0, 0, 50);
                }
                else
                {
                    // Handle null case
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Private"
        #endregion
        #region "Public"
        #endregion
        #region "Event"

/* Unmerged change from project 'CS.ERP_MOB (net8.0-ios)'
Before:
        public void stlProduct_Tapped(object sender, EventArgs e)
        {
After:
        public async void stlProduct_TappedAsync(object sender, EventArgs e)
        {
*/
        public async void stlProduct_TappedAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.RemovePageAsync(this);

        }
        private async void lstProduct_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_PRODUCT = (RES_PRODUCT)ProductList.SelectedItem;
                if (l_RES_PRODUCT != null)
                {
                    Common.mCommon.switchProduct(l_RES_PRODUCT);
                    await PopupNavigation.Instance.PopAllAsync();
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