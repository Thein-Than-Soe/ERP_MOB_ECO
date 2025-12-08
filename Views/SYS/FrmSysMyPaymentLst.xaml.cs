using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.SYS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSysMyPaymentLst : ContentView
    {
        #region "Declaring"
        VmlMyPayment mVmlMyPayment;
        #endregion
        #region "Constructor"
        public FrmSysMyPaymentLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMyPayment = new VmlMyPayment();
                mVmlMyPayment.mJSN_REQ_SALE_PAYMENT_LST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMyPayment.mJSN_REQ_SALE_PAYMENT_LST.RES_SALE_PAYMENT_HEADER.Add(new RES_SALE_PAYMENT_HEADER());
                mVmlMyPayment.getMyPayment();
                switchLanguage();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion
        #region "Private Mehtod"
        public void switchLanguage()
        {
            try
            {
                switch (Common.mCommon.UserSetting.LanguageAsk)
                {
                    case "1"://English
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.BadgeText ="Active (" + mVmlMyPayment.mMyPaymentActiveList.Count + ")";
                            TabPartial.BadgeText ="Partial (" + mVmlMyPayment.MyPaymentPartialList.Count + ")";
                            TabClosed.BadgeText ="Closed (" + mVmlMyPayment.MyPaymentClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.BadgeText ="အသုံးပြု (" + mVmlMyPayment.mMyPaymentActiveList.Count + ")";
                            TabPartial.BadgeText ="တစိတ်တပိုင်း (" + mVmlMyPayment.MyPaymentPartialList.Count + ")";
                            TabClosed.BadgeText ="ပိတ်သိမ်း (" + mVmlMyPayment.MyPaymentClosedList.Count + ")";

                            AcCode.HeaderText ="ကုဒ်";
                            AcDate.HeaderText ="ရက်စွဲ";
                            AcType.HeaderText ="ငွေပေးချေမှုအမျိုးအစား";
                            AcAmount.HeaderText ="ပမာဏ";

                            PCode.HeaderText ="ကုဒ်";
                            PDate.HeaderText ="ရက်စွဲ";
                            PType.HeaderText ="ငွေပေးချေမှုအမျိုးအစား";
                            PAmount.HeaderText ="ပမာဏ";

                            CCode.HeaderText ="ကုဒ်";
                            CDate.HeaderText ="ရက်စွဲ";
                            CType.HeaderText ="ငွေပေးချေမှုအမျိုးအစား";
                            CAmount.HeaderText ="ပမာဏ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.BadgeText ="Active (" + mVmlMyPayment.mMyPaymentActiveList.Count + ")";
                            TabPartial.BadgeText ="Partial (" + mVmlMyPayment.MyPaymentPartialList.Count + ")";
                            TabClosed.BadgeText ="Closed (" + mVmlMyPayment.MyPaymentClosedList.Count + ")";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Event"
        private void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("country-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("country-set"))
                //{
                //    Common.routeMenu("country-set", "Country Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void entSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (entSearch.Text != null && entSearch.Text != "")
                {
                    mVmlMyPayment.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlMyPayment.searchData("");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SALE_PAYMENT_HEADER = (RES_SALE_PAYMENT_HEADER)e.SelectedItem;
                if (l_RES_SALE_PAYMENT_HEADER != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_PAYMENT_HEADER.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    // }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_SALE_PAYMENT_HEADER = (RES_SALE_PAYMENT_HEADER)e.SelectedItem;
                if (l_RES_SALE_PAYMENT_HEADER != null)
                {
                    if (!Common.bindMenu("country-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("country-set"))
                    //{
                    //    Common.routeMenuStr("country-set", "Country Entry", l_RES_SALE_PAYMENT_HEADER.Ask);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrCardView_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("country-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("country-set"))
                //{
                //    Common.routeMenu("country-set", "Country Entry");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrRefresh_Tapped(object sender, EventArgs e)
        {
            try
            {
                entSearch.Text ="";
                mVmlMyPayment.getMyPayment();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrDisplayColumn_Tapped(object sender, EventArgs e)
        {
            try
            {
                //mVmlMyPayment.GetCountryData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}