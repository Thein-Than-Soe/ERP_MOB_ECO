using CS.ERP_MOB.Extensions;
using CS.ERP_MOB.General;
using Microsoft.Maui.Controls;
using System;
using System.ComponentModel;

namespace CS.ERP_MOB.Extensions
{
    public class LanguageExtension : BindableObject
    {
        public static readonly BindableProperty KeyProperty =
            BindableProperty.Create(
                nameof(Key),
                typeof(string),
                typeof(LanguageExtension),
                default(string),
                propertyChanged: OnKeyChanged);

        public string Key
        {
            get => (string)GetValue(KeyProperty);
            set => SetValue(KeyProperty, value);
        }

        private static void OnKeyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LanguageExtension extension)
            {
                extension.UpdateValue();
            }
        }

        private void UpdateValue()
        {
            if (string.IsNullOrWhiteSpace(Key) ||  Common.mCommon.LanguageData == null)
            {
                Value = string.Empty;
                return;
            }

            Value =  Common.mCommon.GetLanguageValueByKey(Key) ?? Key;
        }

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            private set => SetValue(ValueProperty, value);
        }

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(
                nameof(Value),
                typeof(string),
                typeof(LanguageExtension),
                default(string));

        public LanguageExtension()
        {
            Common.mCommon.LangaugePropertyChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof( Common.mCommon.LanguageData))
            {
                UpdateValue();
            }
        }
    }
}
