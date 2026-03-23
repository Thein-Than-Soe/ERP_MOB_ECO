
namespace CS.ERP_MOB.Views.Frame
{
	public partial class FrmSearchPage : ContentView
	{
		public FrmSearchPage()
		{
			InitializeComponent();
		}
        async void SearchInput_SearchButtonPressed(object sender, EventArgs e)
        {
            string keyword = SearchInput.Text;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                await Shell.Current.GoToAsync($"{nameof(FrmShelf)}?keyword={keyword}");
            }
        }
    }
}