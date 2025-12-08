using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;

using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP_MOB.Views.POS;
using CS.ERP.PL.POS.DAT;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCardViewStyle1 : ContentView
    {
        public FrmCardViewStyle1()
        {
            InitializeComponent();
        }

        // Bindable Properties (Image, Name, Price)
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Image
        {
            get => (string)GetValue(ImageProperty);
            set { SetValue(ImageProperty, value); StockImage.Source = value; }
        }

        public static readonly BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set { SetValue(NameProperty, value); StockName.Text = value; }
        }

        public static readonly BindableProperty PriceProperty =
            BindableProperty.Create(nameof(Price), typeof(string), typeof(FrmCardViewStyle1), default(string));

        public string Price
        {
            get => (string)GetValue(PriceProperty);
            set { SetValue(PriceProperty, value); StockPrice.Text = value; }
        }

        private void WishlistButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Wishlist", $"{Name} added to wishlist", "OK");
        }

        private void CartButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Cart", $"{Name} added to cart", "OK");
        }
    }
}