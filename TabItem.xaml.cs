
using System;
using System.Windows.Input;
using CS.ERP_MOB.General;
using Microsoft.Maui.Controls;


namespace CS.ERP_MOB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabItem : ContentView
    {
        // Retrieve the colours so that we're only doing this once, not when the item is selected/unselected
        private static Color SelectedItemBackgroundColor = (Color)Application.Current.Resources["ToolbarItemSelectedBackground"];
        //private static Color SelectedItemBackgroundColor = (Color)Common.mCommon.ThemeSettingTaskbar.HighlightColor;
        private static Color UnselecteItemBackgroundcolor = (Color)Application.Current.Resources["ToolbarItemUnselectedBackground"];

        public TabItem()
        {
            InitializeComponent();
        }

        #region Caption
        public static readonly BindableProperty CaptionProperty =
            BindableProperty.Create(propertyName: nameof(Caption),
                                    returnType: typeof(string),
                                    declaringType: typeof(TabItem),
                                    defaultValue: "Home",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: CaptionChanged);

        public string Caption
        {
            get; set;
        }

        private static void CaptionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            string NewValue = (string)newValue;
            ThisControl.CaptionLabel.Text =NewValue;
        }
        #endregion Caption

        #region IsFontAwesome
        public static readonly BindableProperty IsFontAwesomeProperty =
            BindableProperty.Create(propertyName: nameof(IsFontAwesome),
                                    returnType: typeof(bool),
                                    declaringType: typeof(TabItem),
                                    defaultValue: false,
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: IsFontAwesomeChanged);

        public bool IsFontAwesome
        {
            get; set;
        }

        private static void IsFontAwesomeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            bool NewValue = (bool)newValue;
            ThisControl.FontAwesomeIconLabel.IsVisible = NewValue;
            ThisControl.IconFrame.IsVisible = !NewValue;
        }
        #endregion IsFontAwesome

        #region FontAwesomeIcon
        public static readonly BindableProperty FontAwesomeIconProperty =
            BindableProperty.Create(propertyName: nameof(FontAwesomeIcon),
                                    returnType: typeof(string),
                                    declaringType: typeof(TabItem),
                                    defaultValue: "",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: FontAwesomeIconChanged);

        public string FontAwesomeIcon
        {
            get; set;
        }

        private static void FontAwesomeIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            string NewValue = (string)newValue;
            ThisControl.FontAwesomeIconLabel.Text = NewValue;
        }
        #endregion FontAwesomeIcon

        #region IconSource
        public static readonly BindableProperty IconSourceProperty =
            BindableProperty.Create(nameof(IconSource),typeof(string),typeof(TabItem),default(string), propertyChanged: IconSourceChanged);

        public string IconSource
        {
            get => (string)GetValue(IconSourceProperty);
            set => SetValue(IconSourceProperty, value);
        }

        private static void IconSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TabItem)bindable;
            control.IconImage.Source = (string)newValue;
        }

        #endregion IconSource

        #region Selected
        public static readonly BindableProperty SelectedProperty =
            BindableProperty.Create(propertyName: nameof(Selected),
                            returnType: typeof(bool),
                            declaringType: typeof(TabItem),
                            defaultValue: false,
                            defaultBindingMode: BindingMode.OneWay,
                            propertyChanged: SelectedChanged);


        public bool Selected
        {
            get; set;
        }

        private static void SelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            bool NewValue = (bool)newValue;
            ThisControl.BackgroundColor = NewValue ? SelectedItemBackgroundColor : UnselecteItemBackgroundcolor;
        }
        #endregion Selected

        #region BadgeCount
        public static readonly BindableProperty BadgeCountProperty =
            BindableProperty.Create(propertyName: nameof(BadgeCount),
                                    returnType: typeof(string),
                                    declaringType: typeof(TabItem),
                                    defaultValue: "0",
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: BadgeCountChanged);

        public string BadgeCount
        {
            get; set;
        }

        private static void BadgeCountChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            string NewValue = (string)newValue;

            // Update the badge text
            ThisControl.BadgeCountName.Text = NewValue;

            // Check if the new value is zero
            if (int.TryParse(NewValue, out int count) && count == 0)
            {
                // Hide the badge if the count is zero
                ThisControl.BadgeFrame.IsVisible = false;
            }
            else
            {
                // Show the badge if the count is not zero
                ThisControl.BadgeFrame.IsVisible = true;
            }
        }

        #endregion BadgeCount

        #region TapCommand
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty.Create(propertyName: nameof(TapCommand),
                                    returnType: typeof(ICommand),
                                    declaringType: typeof(TabItem),
                                    defaultValue: null,
                                    defaultBindingMode: BindingMode.OneWay,
                                    propertyChanged: TapCommandChanged);

        public ICommand TapCommand
        {
            get; set;
        }

        private static void TapCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            TabItem ThisControl = (TabItem)bindable;
            ThisControl.TapCommand = (Command)newValue;
        }

        #endregion TapCommand

        #region Event Handlers

        private void Handle_Tapped(object sender, EventArgs args)
        {
            TapCommand?.Execute(this);
        }

        #endregion Event Handlers
    }
}