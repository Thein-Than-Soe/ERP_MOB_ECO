using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Views.Frame;
//using Plugin.Connectivity;
//using Rg.Plugins.Popup.Services;
using Microsoft.Maui.Networking;
using Newtonsoft.Json;
using RGPopup.Maui;
using RGPopup.Maui.Services;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
namespace CS.ERP_MOB.General
{
    public class Utility
    {
        #region "Enum"
        public enum SignUpState
        {
            SignUp,
            EmailActivate,
            PhoneActivate,
            Subscribe,
            SubscriptionType,
            SubscriptionPlan
        }
        public enum DisplayView
        {
            Card,
            List,
            Grid,
            Schedule
        }
        public enum SignInState
        {
            SignIn,
            VerifyEmail,
            VerifyPhone,
        }



        #endregion





        public Utility()
        {
        }
        public static async void openLoader()
        {
            try
            {
                var popup = new FrmLoader();  // Using the PopupPage that wraps FrmLoader
                await PopupNavigation.Instance.PushAsync(popup);  // Show the popup
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public static async void closeLoader()
        {
            // Existing code that actually hides loader
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("loader Already closed");
            }
        }
        public static void checkInternetCon()
        {
            try
            {
                if (!(Connectivity.NetworkAccess == NetworkAccess.Internet))
                { Common.mCommon.ApplicationAlert = true; }
                else { Common.mCommon.ApplicationAlert = false; }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #region "Date time"
        public static string getFormLoadSD()
        {
            try
            {
                //1 for top of the rows
                //2 for botton of the rows
                if (Common.mCommon.UserSetting.LFPeriodTypeAsk == "1" || Common.mCommon.UserSetting.LFPeriodTypeAsk == "2")
                {
                    return Common.mCommon.UserSetting.LFPeriodTypeName_0_255;
                }
                else
                {
                    return getStartPeriod(Common.mCommon.UserSetting.LFPeriodTypeAsk, int.Parse(Common.mCommon.UserSetting.LFDiplayPeriod));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        public static string getFormLoadED()
        {
            try
            {
                //1 for top of the rows
                //2 for botton of the rows
                if (Common.mCommon.UserSetting.LFPeriodTypeAsk == "1" || Common.mCommon.UserSetting.LFPeriodTypeAsk == "2")
                {
                    return Common.mCommon.UserSetting.LFDiplayPeriod;
                }
                else
                {
                    return DateTime.UtcNow.ToString("o");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public static string getTLFormLoadSD()
        {
            try
            {
                //1 for top of the rows
                //2 for botton of the rows
                if (Common.mCommon.UserSetting.TLPeriodTypeAsk == "1" || Common.mCommon.UserSetting.TLPeriodTypeAsk == "2")
                {
                    return Common.mCommon.UserSetting.TLPeriodTypeName_0_255;
                }
                else
                {
                    //int displayPeriod = (int)double.Parse(Common.mCommon.UserSetting.TLDiplayPeriod, CultureInfo.InvariantCulture);
                    //return getStartPeriod(Common.mCommon.UserSetting.TLPeriodTypeAsk, displayPeriod);
                    return getStartPeriod(Common.mCommon.UserSetting.TLPeriodTypeAsk, (int)double.Parse(Common.mCommon.UserSetting.TLDiplayPeriod));
                    //return getStartPeriod("4", 6);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        public static string getTLFormLoadED()
        {
            try
            {
                //1 for top of the rows
                //2 for botton of the rows
                if (Common.mCommon.UserSetting.TLPeriodTypeAsk == "1" || Common.mCommon.UserSetting.TLPeriodTypeAsk == "2")
                {
                    return Common.mCommon.UserSetting.TLDiplayPeriod;
                }
                else
                {
                    //return DateTime.UtcNow.ToString("o");
                    return DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        public static string getStartPeriod(string periodType, int displayPeriod)
        {
            DateTime date = DateTime.UtcNow; // Use UTC like in JS

            switch (periodType)
            {
                case "3": // Days
                    date = date.AddDays(-displayPeriod);
                    break;

                case "4": // Months
                    date = date.AddMonths(-displayPeriod);
                    break;

                case "5": // Years
                    date = date.AddYears(-displayPeriod);
                    break;

                case "6": // Hours
                    date = date.AddHours(-displayPeriod);
                    break;
            }

            //return date.ToString("o"); // ISO 8601 format (e.g., 2025-07-10T11:35:00.0000000Z)
            return date.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
        }

        public static string getDateTimeString(string argUTCDate)
        {
            try
            {
                if (argUTCDate != null && argUTCDate != "")
                {
                    return DateTime.Parse(argUTCDate).ToLocalTime().ToString(Common.mCommon.UserSetting.DateTimeFormatName_0_255);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //public static DateTime getDateTime(string argUTCDate)
        //{
        //    try
        //    {
        //        if (argUTCDate != null && argUTCDate != "")
        //        {
        //            return DateTime.ParseExact(DateTime.Parse(argUTCDate).ToLocalTime().ToString(), Common.mCommon.UserSetting.DateTimeFormatName_0_255, CultureInfo.InvariantCulture);
        //        }
        //        else
        //        {
        //            return DateTime.ParseExact((DateTime.Now).ToString(), Common.mCommon.UserSetting.DateTimeFormatName_0_255, CultureInfo.InvariantCulture);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}
        public static DateTime getDateTime(string argUTCDate)
        {
            if (string.IsNullOrWhiteSpace(argUTCDate))
            {
                return DateTime.Now;
            }

            if (DateTime.TryParse(
                argUTCDate,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal |
                DateTimeStyles.AdjustToUniversal,
                out DateTime utcDateTime))
            {
                return utcDateTime.ToLocalTime();
            }

            Debug.WriteLine(
                $"getDateTime() invalid date: '{argUTCDate}'");

            return DateTime.Now;
        }

        //Filter range

        public class DateRange
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public static DateRange CalendarFormat(DAT_FILTER_RANGE filterRangeData)
        {
            DateTime today = DateTime.Today;
            double count = double.Parse(filterRangeData.Count);

            DateTime startDate;
            DateTime endDate;

            switch (filterRangeData.PeriodTypeAsk)
            {
                // ===== Day =====
                case "3":
                    startDate = today.AddDays(-(count - 1)).Date;
                    endDate = today.Date.AddDays(1).AddTicks(-1);
                    break;

                // ===== Week (Mon–Sun) =====
                case "8":
                    int dayOfWeek = (int)today.DayOfWeek;

                    // Sunday = 0, Monday = 1
                    int diffToMonday = dayOfWeek == 0
                        ? -6
                        : 1 - dayOfWeek;

                    DateTime currentWeekStart = today.AddDays(diffToMonday);

                    startDate = currentWeekStart.Date;

                    endDate = currentWeekStart
                        .AddDays(6)
                        .Date
                        .AddDays(1)
                        .AddTicks(-1);

                    break;

                // ===== Month =====
                case "4":
                    startDate = new DateTime(
                        today.Year,
                        today.Month,
                        1
                    );

                    endDate = new DateTime(
                        today.Year,
                        today.Month,
                        DateTime.DaysInMonth(today.Year, today.Month)
                    )
                    .Date
                    .AddDays(1)
                    .AddTicks(-1);

                    break;

                // ===== Quarter =====
                case "7":
                    int currentQuarter = (today.Month - 1) / 3;
                    int quarterStartMonth = currentQuarter * 3 + 1;

                    startDate = new DateTime(
                        today.Year,
                        quarterStartMonth,
                        1
                    ).AddMonths(-(int)((count - 1) * 3));

                    endDate = new DateTime(
                        today.Year,
                        quarterStartMonth,
                        1
                    )
                    .AddMonths(3)
                    .AddTicks(-1);

                    break;

                // ===== Year =====
                case "5":
                    startDate = new DateTime(
                        today.Year - (int)(count - 1),
                        1,
                        1
                    );

                    endDate = new DateTime(
                        today.Year,
                        12,
                        31
                    )
                    .Date
                    .AddDays(1)
                    .AddTicks(-1);

                    break;

                default:
                    return null;
            }

            return new DateRange
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }

        //public static DateRange CalendarFormat(DAT_FILTER_RANGE filterRangeData)
        //{
        //    DateTime today = DateTime.Today;
        //    double count = double.Parse(filterRangeData.Count);

        //    DateTime startDate;
        //    DateTime endDate;

        //    switch (filterRangeData.PeriodTypeAsk)
        //    {
        //        // Day
        //        case "3":
        //            // Count 0 = today
        //            // Count 1 = yesterday + today
        //            startDate = today.AddDays(-count);
        //            endDate = today.AddDays(1).AddTicks(-1);
        //            break;

        //        // Week (Monday - Sunday)
        //        case "8":
        //            int dayOfWeek = (int)today.DayOfWeek;
        //            int diffToMonday = dayOfWeek == 0 ? -6 : 1 - dayOfWeek;

        //            DateTime currentWeekStart =
        //                today.AddDays(diffToMonday).Date;

        //            // Count 0 = current week
        //            // Count 1 = previous week + current week
        //            startDate = currentWeekStart.AddDays(-(count * 7));
        //            endDate = currentWeekStart.AddDays(7).AddTicks(-1);
        //            break;

        //        // Month
        //        case "4":
        //            DateTime currentMonthStart =
        //                new DateTime(today.Year, today.Month, 1);

        //            // Count 0 = current month
        //            // Count 1 = previous month + current month
        //            startDate = currentMonthStart.AddMonths(-count);
        //            endDate = currentMonthStart.AddMonths(1).AddTicks(-1);
        //            break;

        //        // Quarter
        //        case "7":
        //            int currentQuarter =
        //                (today.Month - 1) / 3;

        //            int quarterStartMonth =
        //                currentQuarter * 3 + 1;

        //            DateTime currentQuarterStart =
        //                new DateTime(today.Year, quarterStartMonth, 1);

        //            // Count 0 = current quarter
        //            // Count 1 = previous quarter + current quarter
        //            startDate =
        //                currentQuarterStart.AddMonths(-(count * 3));

        //            endDate =
        //                currentQuarterStart.AddMonths(3).AddTicks(-1);
        //            break;

        //        // Year
        //        case "5":
        //            DateTime currentYearStart =
        //                new DateTime(today.Year, 1, 1);

        //            // Count 0 = current year
        //            // Count 1 = previous year + current year
        //            startDate =
        //                currentYearStart.AddYears(-count);

        //            endDate =
        //                currentYearStart.AddYears(1).AddTicks(-1);
        //            break;

        //        default:
        //            return null;
        //    }

        //    return new DateRange
        //    {
        //        StartDate = startDate,
        //        EndDate = endDate
        //    };
        //}

        public static DateRange CurrentFormat(DAT_FILTER_RANGE filterRangeData)
        {
            DateTime today = DateTime.Now;
            double count = double.Parse(filterRangeData.Count);

            DateTime startDate;
            DateTime endDate;

            switch (filterRangeData.PeriodTypeAsk)
            {
                // ===== Day (rolling) =====
                case "3":
                    startDate = today.Date.AddDays(-count);
                    endDate = today;
                    break;

                // ===== Week (rolling) =====
                case "8":
                    startDate = today.AddDays(-count * 7);
                    endDate = today;
                    break;

                // ===== Month (rolling) =====
                case "4":
                    startDate = today.AddMonths(-(int)count);
                    endDate = today;
                    break;

                // ===== Quarter (rolling) =====
                case "7":
                    startDate = today.AddMonths(-(int)(count * 3));
                    endDate = today;
                    break;

                // ===== Year (rolling) =====
                case "5":
                    startDate = today.AddYears(-(int)count);
                    endDate = today;
                    break;

                default:
                    return null;
            }

            return new DateRange
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }

        public static DateRange OnFilterRangeChanged(DAT_FILTER_RANGE filterRangeData)
        {
            if (filterRangeData == null)
                return null;

            DateRange range = null;

            // 1 = Calendar Format
            if (filterRangeData.FilterRangeTypeAsk == "1")
            {
                range = CalendarFormat(filterRangeData);
            }
            // 2 = Current Format
            else if (filterRangeData.FilterRangeTypeAsk == "2")
            {
                range = CurrentFormat(filterRangeData);
            }

            return range;
        }

        //User setting's Selected Filter range 
        public static DAT_FILTER_RANGE GetUserSettingFilterRange(List<DAT_FILTER_RANGE> RangeList)
        {
            DAT_FILTER_RANGE mDAT_FILTER_RANGE = new DAT_FILTER_RANGE();

            string periodType = Common.mCommon.UserSetting.LFPeriodTypeAsk;
            mDAT_FILTER_RANGE = RangeList.FirstOrDefault(x => x.PeriodTypeAsk == periodType);
            return mDAT_FILTER_RANGE;
        }


        // date range of initial user setting before range list
        public static DateRange GetInitialScheduleDateRange()
        {
            try
            {
                DateTime today = DateTime.Today;

                string periodTypeAsk =
                    Common.mCommon.UserSetting.LFPeriodTypeAsk;

                DateTime startDate;
                DateTime endDate;

                switch (periodTypeAsk)
                {
                    // ===== Day =====
                    case "3":
                        startDate = today.Date;
                        endDate = today.Date
                            .AddDays(1)
                            .AddTicks(-1);
                        break;

                    // ===== Week (Monday - Sunday) =====
                    case "8":
                        int dayOfWeek = (int)today.DayOfWeek;

                        // Sunday = 0, Monday = 1
                        int diffToMonday = dayOfWeek == 0
                            ? -6
                            : 1 - dayOfWeek;

                        startDate = today
                            .AddDays(diffToMonday)
                            .Date;

                        endDate = startDate
                            .AddDays(7)
                            .AddTicks(-1);
                        break;

                    // ===== Month =====
                    case "4":
                        startDate = new DateTime(
                            today.Year,
                            today.Month,
                            1);

                        endDate = startDate
                            .AddMonths(1)
                            .AddTicks(-1);
                        break;

                    // ===== Quarter =====
                    case "7":
                        int currentQuarter =
                            (today.Month - 1) / 3;

                        int quarterStartMonth =
                            currentQuarter * 3 + 1;

                        startDate = new DateTime(
                            today.Year,
                            quarterStartMonth,
                            1);

                        endDate = startDate
                            .AddMonths(3)
                            .AddTicks(-1);
                        break;

                    // ===== Year =====
                    case "5":
                        startDate = new DateTime(
                            today.Year,
                            1,
                            1);

                        endDate = startDate
                            .AddYears(1)
                            .AddTicks(-1);
                        break;

                    default:
                        startDate = today.Date;

                        endDate = today.Date
                            .AddDays(1)
                            .AddTicks(-1);
                        break;
                }

                return new DateRange
                {
                    StartDate = startDate,
                    EndDate = endDate
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                DateTime today = DateTime.Today;

                return new DateRange
                {
                    StartDate = today,
                    EndDate = today.AddDays(1).AddTicks(-1)
                };
            }
        }
        #endregion
        public static Boolean checkButtonAccess(string menuName)
        {
            try
            {
                if (menuName != "")
                {
                    foreach (var l_Button in Common.mCommon.SelectedMenu.button)
                    {
                        menuName = menuName.ToLower();
                        if (l_Button.text.ToLower().Contains(menuName))
                        {
                            return true;
                        }
                    }
                    return false;

                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #region "DecimalFormat"
        //roundoffamount, usersetting decimal place, amount after dis
        public static string getDecimalFormatString(string argDecimal, string argDecimalPlace, string argDecimaRounding)
        {
            try
            {
                if (argDecimal != null && argDecimal != "")
                {

                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argDecimal), Convert.ToInt32(Convert.ToDecimal(argDecimaRounding))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argDecimaRounding))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getDecimalFormatString(string argDecimal)
        {
            try
            {
                if (argDecimal != null && argDecimal != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argDecimal), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getDecimalFormatDecimal(string argDecimal, string argDecimalPlace, string argRounding)
        {
            try
            {
                if (argDecimal != null && argDecimal != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argDecimal), Convert.ToInt32(Convert.ToDecimal(argRounding)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argRounding)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getDecimalFormatDecimal(string argDecimal)
        {
            try
            {
                if (argDecimal != null && argDecimal != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argDecimal), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Price"
        public static string getPriceString(string argPrice, string argPriceDecimalPlace, string argPriceRoundAsking)
        {
            try
            {
                if (argPrice != null && argPrice != "")
                {
                    //Decimal.Round(Convert.ToDecimal(argPrice), Convert.ToInt32(argRounding));
                    //string.Format($"{{0:F{argDecimalPlace}}}", argPrice);
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPriceDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(Convert.ToDecimal(argPrice)), Convert.ToInt32(Convert.ToDecimal(argPriceRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPriceDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argPriceRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getPriceString(string argPrice)
        {
            try
            {
                if (argPrice != null && argPrice != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPrice), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getPriceDecimal(string argPrice, string argPriceDecimalPlace, string argPriceRoundAsking)
        {
            try
            {
                if (argPrice != null && argPrice != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPriceDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPrice), Convert.ToInt32(Convert.ToDecimal(argPriceRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToDecimal(Convert.ToInt32(argPriceDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argPriceRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getPriceDecimal(string argPrice)
        {
            try
            {
                if (argPrice != null && argPrice != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPrice), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PriceRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "QTY"
        public static string getQTYString(string argQTY, string argQTYDecimalPlace, string argQTYRoundAsking)
        {
            try
            {
                if (argQTY != null && argQTY != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argQTYDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argQTY), Convert.ToInt32(Convert.ToDecimal(argQTYRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argQTYDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argQTYRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getQTYString(string argQTY)
        {
            try
            {
                if (argQTY != null && argQTY != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argQTY), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getQTYDecimal(string argQTY, string argQTYDecimalPlace, string argQTYRoundAsking)
        {
            try
            {
                if (argQTY != null && argQTY != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argQTYDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argQTY), Convert.ToInt32(Convert.ToDecimal(argQTYRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argQTYDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argQTYRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getQTYDecimal(string argQTY)
        {
            try
            {
                if (argQTY != null && argQTY != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argQTY), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.QTYRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Amount"
        public static string getAmountString(string argAmount, string argAmountDecimalPlace, string argAmountRoundAsking)
        {
            try
            {
                if (argAmount != null && argAmount != "")
                {
                    //Decimal.Round(Convert.ToDecimal(argPrice), Convert.ToInt32(argRounding));
                    //string.Format($"{{0:F{argDecimalPlace}}}", argPrice);
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argAmountDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argAmount), Convert.ToInt32(Convert.ToDecimal(argAmountRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argAmountDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argAmountRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getAmountString(string argAmount)
        {
            try
            {
                if (argAmount != null && argAmount != "")
                {
                    //int ad = Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace));
                    ////decimal aef = new decimal()
                    //decimal aef = new decimal(51.2345679807);
                    //decimal ae = Decimal.Round(new decimal(51.2345679807), 1);
                    ////decimal a = Decimal.Round(Convert.ToDecimal(argAmount), Convert.ToInt32(Common.mCommon.UserSetting.AmountRoundAsk));
                    /////string ss = string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace))}}}", aef);

                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argAmount), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getAmountDecimal(string argAmount, string argAmountDecimalPlace, string argAmountRoundAsking)
        {
            try
            {
                if (argAmount != null && argAmount != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argAmountDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argAmount), Convert.ToInt32(Convert.ToDecimal(argAmountRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argAmountDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argAmountRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getAmountDecimal(string argAmount)
        {
            try
            {
                if (argAmount != null && argAmount != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argAmount), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.AmountRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Percentage"
        public static string getPercentageString(string argPercentage, string argPercentageDecimalPlace, string argPercentageRoundAsking)
        {
            try
            {
                if (argPercentage != null && argPercentage != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPercentageDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPercentage), Convert.ToInt32(Convert.ToDecimal(argPercentageRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPercentageDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argPercentageRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getPercentageString(string argPercentage)
        {
            try
            {
                if (argPercentage != null && argPercentage != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPercentage), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getPercentageDecimal(string argPercentage, string argPercentageDecimalPlace, string argPercentageRoundAsking)
        {
            try
            {
                if (argPercentage != null && argPercentage != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPercentageDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPercentage), Convert.ToInt32(Convert.ToDecimal(argPercentageRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argPercentageDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argPercentageRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getPercentageDecimal(string argPercentage)
        {
            try
            {
                if (argPercentage != null && argPercentage != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argPercentage), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.PercentageRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Tax"
        public static string getTaxString(string argTax, string argTaxDecimalPlace, string argTaxRoundAsking)
        {
            try
            {
                if (argTax != null && argTax != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argTaxDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argTax), Convert.ToInt32(Convert.ToDecimal(argTaxRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argTaxDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argTaxRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getTaxString(string argTax)
        {
            try
            {
                if (argTax != null && argTax != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argTax), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getTaxDecimal(string argTax, string argTaxDecimalPlace, string argTaxRoundAsking)
        {
            try
            {
                if (argTax != null && argTax != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argTaxDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argTax), Convert.ToInt32(Convert.ToDecimal(argTaxRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argTaxDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argTaxRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getTaxDecimal(string argTax)
        {
            try
            {
                if (argTax != null && argTax != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argTax), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.TaxRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion

        #region "Discount"
        //call for finding maching discount rule
        public static DAT_DISCOUNT_RULE FindMatchingDiscountRule(List<DAT_DISCOUNT_RULE> DiscountRules, string discountCalculationFigureAsk, decimal calculationValue, DateTime discountDate, string currencyAsk)
        {
            if (DiscountRules == null || DiscountRules.Count == 0)
                return null;

            var matchedRules = DiscountRules
                .Where(rule =>
                    rule.DiscountCalculationFigureAsk == discountCalculationFigureAsk &&
                    IsDiscountDateMatched(
                        discountDate,
                        DateTime.Parse(rule.SD),
                        DateTime.Parse(rule.ED)) &&
                    IsDiscountCurrencyMatched(rule, currencyAsk) &&
                    IsDiscountConditionMatched(rule, calculationValue))
                .OrderByDescending(rule =>
                    decimal.Parse(rule.DiscountCalculationAmount))
                .ToList();

            return matchedRules.FirstOrDefault();
        }
        private static bool IsDiscountDateMatched(DateTime discountDate, DateTime startDate, DateTime endDate)
        {
            if (startDate.Kind == DateTimeKind.Utc)
                startDate = startDate.ToLocalTime();

            if (endDate.Kind == DateTimeKind.Utc)
                endDate = endDate.ToLocalTime();

            if (discountDate.Kind == DateTimeKind.Utc)
                discountDate = discountDate.ToLocalTime();

            discountDate = discountDate.Date;
            startDate = startDate.Date;
            endDate = endDate.Date;

            return discountDate >= startDate &&
                   discountDate <= endDate;
        }
        private static bool IsDiscountCurrencyMatched(DAT_DISCOUNT_RULE rule, string currencyAsk)
        {
            if (rule == null)
                return false;

            // Only fixed amount needs currency matching.
            // Percentage does not need it.
            if (rule.DiscountTypeAsk != "2")
                return true;

            return rule.CurrencyAsk == currencyAsk;
        }
        private static bool IsDiscountConditionMatched(DAT_DISCOUNT_RULE rule, decimal calculationValue)
        {
            decimal conditionAmount =
                decimal.Parse(rule.DiscountCalculationAmount);

            switch (rule.DiscountConditionTypeAsk)
            {
                // >=
                case "1":
                    return calculationValue >= conditionAmount;
                // <=
                case "2":
                    return calculationValue <= conditionAmount;
                // >
                case "3":
                    return calculationValue > conditionAmount;
                // Between
                case "4":

                    // Between requires a minimum and maximum value.
                    // DAT_DISCOUNT_RULE currently only shows
                    // DiscountCalculationAmount.
                    //
                    // So this needs the actual upper-bound field
                    // from your API/model before implementing.
                    return false;


                default:

                    return false;
            }
        }

        //call for discount after selected rule
        public static decimal CalculateDiscount(DAT_DISCOUNT_RULE SelectedRule, decimal calculationValue)
        {
            if (calculationValue <= 0)
                return 0;

            // At this point:
            // SelectedDiscountTypeAsk can come from the matched rule
            // OR from user selection.
            //
            // DiscountRate can come from the matched rule
            // OR from user input.

            string discountTypeAsk = SelectedRule.DiscountTypeAsk;
            decimal rate = decimal.Parse(SelectedRule.Rate);

            decimal discount = 0;

            switch (discountTypeAsk)
            {
                case "0": // NA
                    discount = 0;
                    break;
                case "1": // %
                    discount = calculationValue * rate / 100m;
                    break;

                case "2": // $
                    discount = rate;
                    break;

                case "3": // Coupon
                    discount = CalculateCouponDiscount(
                        SelectedRule,
                        rate,
                        calculationValue);
                    break;

                case "4": // By X get X
                case "5": // By X get Y
                          // Handle later when item/tier logic is added
                    discount = 0;
                    break;

                default:
                    discount = 0;
                    break;
            }

            if (discount < 0)
                discount = 0;

            if (discount > calculationValue)
                discount = calculationValue;

            return discount;
        }
        private static decimal CalculateCouponDiscount(DAT_DISCOUNT_RULE rule, decimal rate, decimal calculationValue)
        {
            if (rule == null || rate <= 0)
                return 0;

            switch (rule.ValueTypeAsk)
            {
                case "1": // Fixed
                    return rate;

                case "2": // Percentage
                    return calculationValue * rate / 100m;

                default:
                    return 0;
            }
        }

        #endregion

        #region "GrandTotal"
        public static string getGrandTotalString(string argGrandTotal, string argGrandTotalDecimalPlace, string argGrandTotalRoundAsking)
        {
            try
            {
                if (argGrandTotal != null && argGrandTotal != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argGrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argGrandTotal), Convert.ToInt32(Convert.ToDecimal(argGrandTotalRoundAsking))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argGrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argGrandTotalRoundAsking))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static string getGrandTotalString(string argGrandTotal)
        {
            try
            {
                if (argGrandTotal != null && argGrandTotal != "")
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argGrandTotal), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk))));
                }
                else
                {
                    return string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getGrandTotalDecimal(string argGrandTotal, string argGrandTotalDecimalPlace, string argGrandTotalRoundAsking)
        {
            try
            {
                if (argGrandTotal != null && argGrandTotal != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argGrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argGrandTotal), Convert.ToInt32(Convert.ToDecimal(argGrandTotalRoundAsking)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(argGrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(argGrandTotalRoundAsking)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public static decimal getGrandTotalDecimal(string argGrandTotal)
        {
            try
            {
                if (argGrandTotal != null && argGrandTotal != "")
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(Convert.ToDecimal(argGrandTotal), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk)))));
                }
                else
                {
                    return Convert.ToDecimal(string.Format($"{{0:F{Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalDecimalPlace))}}}", Decimal.Round(new decimal(0), Convert.ToInt32(Convert.ToDecimal(Common.mCommon.UserSetting.GrandTotalRoundAsk)))));
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion


        #region 
        public static byte[] GetImageBytes(Stream stream)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
        }

        #endregion




        public static T Clone<T>(T source)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(source);
                return JsonConvert.DeserializeObject<T>(serialized);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

    }
}
