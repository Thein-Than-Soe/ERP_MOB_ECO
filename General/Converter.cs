using System.Globalization;
using CS.ERP_MOB.Services.SYS;
namespace CS.ERP_MOB.General
{
    public class ImageURLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string url)
                return Sys_Service.getUploadURL() + url;
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ProfileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() != parameter?.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }


    public class FriendlyDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateTime)
            {
                return ToFriendlyDate(dateTime);
            }
            // Handle null or invalid input (e.g., return a default string)
            return "Invalid date";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed for this use case (one-way binding)
            throw new NotImplementedException();
        }

        // Your original method, slightly adapted for clarity
        private static string ToFriendlyDate(string dateTimeString)
        {
            DateTime dateTime = DateTime.Parse(dateTimeString);
            var now = DateTime.Now;
            var diff = now - dateTime;
            bool isFuture = dateTime > now;
            if (Math.Abs(diff.TotalSeconds) < 60)
                return "just now";
            if (Math.Abs(diff.TotalMinutes) < 60)
                return isFuture
                    ? $"in {(int)Math.Abs(diff.TotalMinutes)} minute(s)"
                    : $"{(int)Math.Abs(diff.TotalMinutes)} minute(s) ago";
            if (Math.Abs(diff.TotalHours) < 24)
                return isFuture
                    ? $"in {(int)Math.Abs(diff.TotalHours)} hour(s)"
                    : $"{(int)Math.Abs(diff.TotalHours)} hour(s) ago";
            int daysDiff = (int)Math.Round(Math.Abs(diff.TotalDays));
            if (daysDiff == 1)
                return isFuture ? "tomorrow" : "yesterday";
            if (daysDiff <= 6)
            {
                string day = dateTime.ToString("dddd 'at' h:mm tt");
                return isFuture ? $"on {day}" : $"last {day}";
            }
            if (daysDiff <= 30)
                return isFuture
                    ? $"in {daysDiff} day(s)"
                    : $"{daysDiff} day(s) ago";
            // For dates older than a month or farther in future
            return dateTime.ToString(Common.mCommon.UserSetting.DateTimeFormatName_0_255);
        }
    }


    public class AmountCurrencyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var currency = values[0]?.ToString();
            var amount = values[1]?.ToString();
            amount = Utility.getGrandTotalDecimal(amount).ToString();
            return $"{currency} {amount}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class CustomerContactConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var customer = values[0]?.ToString();
            var contact = values[1]?.ToString();
            if(contact == "")
                return customer;
            return $"{customer} ({contact})";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class StatusPostingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var status = values[0]?.ToString();
            var postingStatus = values[1]?.ToString();
            if (postingStatus == "")
                return status;
            return $"{status}/{postingStatus}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() == "1";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "1" : "0";
        }
    }

    public class FontAwesomeUnicodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            // Expecting value like 0xf0fe
            if (int.TryParse(value.ToString(), System.Globalization.NumberStyles.HexNumber, null, out int codePoint))
            {
                return char.ConvertFromUtf32(codePoint);
            }

            // If already int
            if (value is int intValue)
                return char.ConvertFromUtf32(intValue);

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string html = value.ToString();  // example: "&#xf0fe;"

            // Extract hex number
            if (html.StartsWith("&#x") && html.EndsWith(";"))
            {
                string hex = html.Replace("&#x", "").Replace(";", "");

                if (int.TryParse(hex, NumberStyles.HexNumber, null, out int code))
                {
                    return char.ConvertFromUtf32(code); // return actual glyph
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
    public class ZeroToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // null → hide
            if (value == null)
                return false;

            // convert to int
            if (int.TryParse(value.ToString(), out int count))
            {
                return count > 0;   // show only if > 0
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class HighlightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            if (value is string statusAsk && statusAsk != "0")
            {
                return Colors.Transparent;
            }
            if (parameter is string colorString)
            {
                return Color.FromArgb(colorString);
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TabColorConverter : IValueConverter
    {
        // You can customize these colors in XAML when registering the converter
        public Color ActiveColor { get; set; } = Colors.LightBlue;
        public Color InactiveColor { get; set; } = Colors.Transparent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // value = current tab item
            // parameter = ActiveTab

            if (value == null || parameter == null)
                return InactiveColor;

            // same object -> active tab
            bool isActive = value.Equals(parameter);

            return isActive ? ActiveColor : InactiveColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}