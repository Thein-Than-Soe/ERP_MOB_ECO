using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;

using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP_MOB.Views.POS;
using CS.ERP.PL.POS.DAT;
using CommunityToolkit.Maui.Views;
using System.Windows.Input;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OvaListFormCartView : ContentView
    {
        public OvaListFormCartView()
        {
            InitializeComponent();
        }

        public OvaListFormCartView(
            bool isChecked,
            string header,
            string itemAsk,
            string codeLabel,
            string nameLabel,
            string descriptionLabel,
            string desLabel,
            string descLabel,
            string body4Label,
            string remarkLabel,
            string footerLabel)
        {
            InitializeComponent();
            //LongPressCommand = new Command(OnLongPressed);
            chkSelectItem.IsChecked = isChecked;
            Header.Text = header;
            ItemAsk = itemAsk;
            Code.Text =codeLabel;
            Name.Text =nameLabel;
            Description.Text =descriptionLabel;
            Remark.Text =remarkLabel;
            Des.Text =desLabel;
            Desc.Text =descLabel;
            Footer.Text =footerLabel;
        }

        #region chkSelectItem
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(propertyName: nameof(IsChecked),
                                                                        returnType: typeof(bool),
                                                                        declaringType: typeof(OvaListFormCartView),
                                                                        defaultValue: false,
                                                                        defaultBindingMode: BindingMode.TwoWay,
                                                                        propertyChanged: IsCheckedChanged);

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
        private static void IsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            bool NewValue = (bool)newValue;
            ThisControl.chkSelectItem.IsChecked = NewValue;
        }
        #endregion chkSelectItem


        #region HeaderLabel
        public static readonly BindableProperty HeaderLabelProperty =
            BindableProperty.Create(propertyName: nameof(HeaderLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: HeaderLabelChanged);

        public string HeaderLabel
        {
            get; set;
        }

        private static void HeaderLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Header.Text = NewValue;
        }

        #endregion HeaderLabel

        #region FooterLabel
        public static readonly BindableProperty FooterLabelProperty =
            BindableProperty.Create(propertyName: nameof(FooterLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: FooterLabelChanged);

        public string FooterLabel
        {
            get; set;
        }

        private static void FooterLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Footer.Text = NewValue;
        }

        #endregion FooterLabel

        #region ItemAsk
        public static readonly BindableProperty ItemAskProperty =
            BindableProperty.Create(propertyName: nameof(ItemAsk),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "0",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: ItemAskChanged);

        public string ItemAsk
        {
            get; set;
        }

        private static void ItemAskChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.ItemAsk = NewValue;
            //ThisControl.Code.Text =NewValue;
        }

        #endregion ItemAsk

        #region CodeLabel
        public static readonly BindableProperty CodeLabelProperty =
            BindableProperty.Create(propertyName: nameof(CodeLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: CodeLabelChanged);

        public string CodeLabel
        {
            get; set;
        }

        private static void CodeLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Code.Text =NewValue;
        }

        #endregion CodeLabel

        #region NameLabel
        public static readonly BindableProperty NameLabelProperty =
            BindableProperty.Create(propertyName: nameof(NameLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: NameLabelChanged);

        public string NameLabel
        {
            get; set;
        }

        private static void NameLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Name.Text =NewValue;
        }

        #endregion NameLabel

        #region DescriptionLabel
        public static readonly BindableProperty DescriptionLabelProperty =
            BindableProperty.Create(propertyName: nameof(DescriptionLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: DescriptionLabelChanged);

        public string DescriptionLabel
        {
            get; set;
        }

        private static void DescriptionLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Description.Text =NewValue;
        }

        #endregion DescriptionLabel


        #region RemarkLabel
        public static readonly BindableProperty RemarkLabelProperty =
            BindableProperty.Create(propertyName: nameof(RemarkLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: RemarkLabelChanged);

        public string RemarkLabel
        {
            get; set;
        }

        private static void RemarkLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Remark.Text =NewValue;
        }

        #endregion RemarkLabel


        #region DesLabel
        public static readonly BindableProperty DesLabelProperty =
            BindableProperty.Create(propertyName: nameof(DesLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: DesLabelChanged);

        public string DesLabel
        {
            get; set;
        }

        private static void DesLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Des.Text =NewValue;
        }

        #endregion DesLabel


        #region DescLabel
        public static readonly BindableProperty DescLabelProperty =
            BindableProperty.Create(propertyName: nameof(DescLabel),
                                    returnType: typeof(string),
                                    declaringType: typeof(OvaListFormCartView),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: DescLabelChanged);

        public string DescLabel
        {
            get; set;
        }

        private static void DescLabelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            OvaListFormCartView ThisControl = (OvaListFormCartView)bindable;
            string NewValue = (string)newValue;
            ThisControl.Desc.Text =NewValue;
        }

        #endregion DescLabel
        private void chkSelectItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (BindingContext is RES_SALE_INVOICE item)
            {
                item.IsChecked = e.Value ? "1" : "0";
            }
        }

        public event EventHandler<object> ItemDoubleTapped;
        public event EventHandler<object> ItemSingleTapped;
        private void OnDoubleTap(object sender, TappedEventArgs e)
        {
            chkSelectItem.IsChecked = true;
            ItemDoubleTapped?.Invoke(this, BindingContext);
        }
        private void OnSingleTap(object sender, TappedEventArgs e)
        {
            ItemSingleTapped?.Invoke(this, BindingContext);
        }

        private void OnLongPress(object sender, EventArgs e)
        {
            chkSelectItem.IsChecked = true;
            ItemDoubleTapped?.Invoke(this, BindingContext);
        }
        public Command OnLongPressCommand => new Command(async () =>
        {
            var popup = new OptionsPopup();
            await App.Current.MainPage.ShowPopupAsync(popup);
        });

        public static readonly BindableProperty LongPressCommandProperty =
            BindableProperty.Create(
                nameof(LongPressCommand),
                typeof(ICommand),
                typeof(OvaListFormCartView),
                null);

        public ICommand LongPressCommand
        {
            get => (ICommand)GetValue(LongPressCommandProperty);
            set => SetValue(LongPressCommandProperty, value);
        }

        private void OnLongPressed()
        {
            // 🔥 Your logic here — for example:
            Console.WriteLine("Long press detected!");
            // or call a method like:
            // HandleLongPress();
        }

    }
}