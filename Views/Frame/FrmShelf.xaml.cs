using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB_ECO.Views.ECO;

namespace CS.ERP_MOB.Views.Frame
{
	public partial class FrmShelf : ContentView
	{
		public FrmShelf()
		{
			InitializeComponent();
		}

        //private async void cardView_ItemSelected(object sender, SelectionChangedEventArgs e)
        //{
        //    await Application.Current.MainPage.DisplayAlert("Test", "SelectionChanged fired", "OK");
        //    try
        //    {
        //        System.Diagnostics.Debug.WriteLine(Navigation == null);
        //        if (Navigation == null)
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Error", "Navigation is null", "OK");
        //            return;
        //        }
        //        if (e.CurrentSelection.Count == 0)
        //            return;

        //        var selectedItem = e.CurrentSelection.FirstOrDefault() as RES_STOCK;

        //        if (selectedItem != null)
        //        {
        //            await Navigation.PushAsync(
        //                new FrmEcoProductDtl(selectedItem));
        //            await Application.Current.MainPage.Navigation.PushAsync(new FrmEcoProductDtl(selectedItem));
        //        }

        //        ((CollectionView)sender).SelectedItem = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex);
        //    }
        //}
    
    }
}