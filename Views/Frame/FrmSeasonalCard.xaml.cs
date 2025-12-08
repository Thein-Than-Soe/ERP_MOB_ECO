
using System;
using CS.ERP.PL.SYS.DAT;
using Microsoft.Maui.Controls;
using RGPopup.Maui.Extensions;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSeasonalCard : ContentView
    {
        public FrmSeasonalCard()
        {
            InitializeComponent();
        }
        public FrmSeasonalCard(RES_SEASONAL_STOCK argRES_SEASONAL_STOCK)
        {
            try
            {
                InitializeComponent();
                ImgSubscriberIcon.Source = SubscriberIconSource = argRES_SEASONAL_STOCK.SubscriberPhoto; ;
                LblSubscriberName.Text =SubscriberLabel = argRES_SEASONAL_STOCK.SubscriberName_0_255;
                ImgStockIcon.Source = StockIconSource = argRES_SEASONAL_STOCK.SeasonalPhotoURL;
                LblStockName.Text =StockNameLabel = argRES_SEASONAL_STOCK.SeasonalName_0_255;
                LblNewPrice.Text =NewPriceLabel = float.Parse(argRES_SEASONAL_STOCK.Price).ToString("0.00");
                LblOldPrice.Text =OldPriceLabel = float.Parse(argRES_SEASONAL_STOCK.OriginalPrice).ToString("0.00");


                if (float.Parse(argRES_SEASONAL_STOCK.OriginalPrice) <= 0)
                {
                    LblOldPrice.IsVisible = false;
                }
                else
                {
                    LblOldPrice.Text =OldPriceLabel = float.Parse(argRES_SEASONAL_STOCK.OriginalPrice).ToString("0.00");
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }


        #region SubscriberIconSource
        public static readonly BindableProperty SubscriberIconSourceProperty =
            BindableProperty.Create(propertyName: nameof(SubscriberIconSource),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "icon",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: SubscriberIconSourceChanged);

        public string SubscriberIconSource
        {
            get; set;
        }

        private static void SubscriberIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.ImgSubscriberIcon.Source = NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion StockIconSource

        #region SubscriberLabel
        public static readonly BindableProperty SubscriberLabelProperty =
            BindableProperty.Create(propertyName: nameof(SubscriberLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: SubscriberLabelChanged);

        public string SubscriberLabel
        {
            get; set;
        }

        private static void SubscriberLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.LblSubscriberName.Text =NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion SubscriberLabel

        #region StockIconSource
        public static readonly BindableProperty StockIconSourceProperty =
            BindableProperty.Create(propertyName: nameof(StockIconSource),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "icon",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: StockIconSourceChanged);

        public string StockIconSource
        {
            get; set;
        }

        private static void StockIconSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.ImgStockIcon.Source = NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion StockIconSource

        #region StockNameLabel
        public static readonly BindableProperty StockNameLabelProperty =
            BindableProperty.Create(propertyName: nameof(StockNameLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: StockNameLabelChanged);

        public string StockNameLabel
        {
            get; set;
        }

        private static void StockNameLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.LblStockName.Text =NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion StockNameLabel

        #region NewPriceLabel
        public static readonly BindableProperty NewPriceLabelProperty =
            BindableProperty.Create(propertyName: nameof(NewPriceLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: NewPriceLabelChanged);

        public string NewPriceLabel
        {
            get; set;
        }

        private static void NewPriceLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.LblNewPrice.Text =NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion NewPriceLabel

        #region OldPriceLabel
        public static readonly BindableProperty OldPriceLabelProperty =
            BindableProperty.Create(propertyName: nameof(OldPriceLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(FrmSeasonalCard),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: OldPriceLabelChanged);

        public string OldPriceLabel
        {
            get; set;
        }

        private static void OldPriceLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                FrmSeasonalCard ThisControl = (FrmSeasonalCard)bindable;
                string NewValue = (string)newValue;
                ThisControl.LblOldPrice.Text =NewValue;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion OldPriceLabel

        private void TggAddCart_Tapped(object sender, System.EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private async void TgrStockDetail_Tapped(object sender, System.EventArgs e)
        {
            try
            {
                //await Application.Current.MainPage.Navigation.PushPopupAsync(new PopStockDetail());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}