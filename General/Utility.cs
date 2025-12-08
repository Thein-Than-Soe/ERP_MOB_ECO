using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CS.ERP_MOB.Views.Frame;
using Newtonsoft.Json;
//using Plugin.Connectivity;
//using Rg.Plugins.Popup.Services;
using Microsoft.Maui.Networking;
using RGPopup.Maui.Services;
using RGPopup.Maui;
using System.Diagnostics;
using CS.ERP.PL.POS.DAT;
namespace CS.ERP_MOB.General
{
    public class Utility
    {
        #region "Enum"
        public enum SignUpState
        {
            SignUp,
            Activate,
            Subscribe,
            SubscriptionType,
            SubscriptionPlan
        }
        public enum DisplayView
        {
            Card,
            List,
            Grid
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
                //throw ex.InnerException;
            }

        }
        public static async void closeLoader()
        {
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
                    //return getStartPeriod(Common.mCommon.UserSetting.TLPeriodTypeAsk, (int)double.Parse(Common.mCommon.UserSetting.TLDiplayPeriod));
                    return getStartPeriod("4", 6);
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
                    return DateTime.UtcNow.ToString("o");
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

            return date.ToString("o"); // ISO 8601 format (e.g., 2025-07-10T11:35:00.0000000Z)
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

        public static DateTime getDateTime(string argUTCDate)
        {
            try
            {
                if (argUTCDate != null && argUTCDate != "")
                {
                    return DateTime.ParseExact(DateTime.Parse(argUTCDate).ToLocalTime().ToString(), Common.mCommon.UserSetting.DateTimeFormatName_0_255, CultureInfo.InvariantCulture);
                }
                else
                {
                    return DateTime.ParseExact((DateTime.Now).ToString(), Common.mCommon.UserSetting.DateTimeFormatName_0_255, CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
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
