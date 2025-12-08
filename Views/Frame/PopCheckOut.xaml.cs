using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.POS.REQ;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using Xamarin.Forms.Xaml;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopCheckOut : PopupPage
    {
        #region "Declaring"
        JSN_REQ_SHOPPING mJSN_REQ_SHOPPING = new JSN_REQ_SHOPPING();
        #endregion
        #region "Constructor"
        public PopCheckOut()
        {
            try
            {
                InitializeComponent();

                lblTotalAmount.Text = Common.mCommon.Shopping.GrandTotal;
                lblCurrency.Text = Common.mCommon.Shopping.CurrencyCode;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion
        #region "Private"
        
        #endregion
        #region "Public"
        #endregion
        #region "Event"
        private async void TgrDeleteStock_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Utility.openLoader();
                RES_SHOPPING_DETAIL l_RES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();
                var selectedIndex = Common.mCommon.ShoppingCartList.IndexOf(
                   ((RES_SHOPPING_DETAIL)((Label)sender).BindingContext));
                if (selectedIndex > -1)
                {
                    l_RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList[selectedIndex];
                    l_RES_SHOPPING_DETAIL.StatusAsk = "6";
                    Common.mCommon.ShoppingCartList[selectedIndex] = l_RES_SHOPPING_DETAIL;

                    mJSN_REQ_SHOPPING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                    mJSN_REQ_SHOPPING.RES_SHOPPING = Common.mCommon.Shopping;
                    mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList;
                    Common.mCommon.saveShoppingCart(mJSN_REQ_SHOPPING);
                    Common.mCommon.ShoppingCartList.RemoveAt(selectedIndex);
                    Common.mCommon.ShoppingCartList = Clone(Common.mCommon.ShoppingCartList);
                }
                lblTotalAmount.Text = Common.mCommon.Shopping.GrandTotal;
               // Common.mCommon.saveShoppingCart(mJSN_REQ_SHOPPING);
                //await Utility.closeLoader();
            }
            catch (Exception ex)
            {
                //await Utility.closeLoader();
                throw ex.InnerException;
            }
        }
        private void TgrIncrease_Tapped(object sender, EventArgs e)
        {
            try
            {
                decimal NewValue = 0;
                RES_SHOPPING_DETAIL l_RES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();
                l_RES_SHOPPING_DETAIL = ((RES_SHOPPING_DETAIL)((Label)sender).BindingContext);

                var selectedIndex = Common.mCommon.ShoppingCartList.IndexOf(
                   ((RES_SHOPPING_DETAIL)((Label)sender).BindingContext));
                if (selectedIndex > -1)
                {
                    NewValue = Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY);
                    l_RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList[selectedIndex];
                    //stpQTy.Value = Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY);
                    if (NewValue >= 0)
                    {
                        NewValue = NewValue + 1;

                        l_RES_SHOPPING_DETAIL.QTY = String.Format("{0:F1}", NewValue);
                        l_RES_SHOPPING_DETAIL.TotalAmount = (Convert.ToDecimal(l_RES_SHOPPING_DETAIL.Price)
                            * Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY)).ToString();
                        Common.mCommon.ShoppingCartList[selectedIndex] = l_RES_SHOPPING_DETAIL;

                        mJSN_REQ_SHOPPING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                        mJSN_REQ_SHOPPING.RES_SHOPPING = Common.mCommon.Shopping;
                        mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList;

                        //Common.mCommon.ShoppingCartList.RemoveAt(selectedIndex);
                        Common.mCommon.ShoppingCartList = Clone(Common.mCommon.ShoppingCartList);
                        Common.mCommon.saveShoppingCart(mJSN_REQ_SHOPPING);

                        lblTotalAmount.Text = Common.mCommon.Shopping.GrandTotal;
                    }
                    else 
                    {
                        TgrDeleteStock_Tapped(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrDecrease_Tapped(object sender, EventArgs e)
        {
            try
            {
                decimal NewValue = 0;
                RES_SHOPPING_DETAIL l_RES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();
                var selectedIndex = Common.mCommon.ShoppingCartList.IndexOf(
                   ((RES_SHOPPING_DETAIL)((Label)sender).BindingContext));
                if (selectedIndex > -1)
                {
                    l_RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList[selectedIndex];
                    NewValue = Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY);
                    //stpQTy.Value = Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY);
                    if (NewValue > 1)
                    {
                        NewValue = NewValue - 1;
                        l_RES_SHOPPING_DETAIL.QTY = String.Format("{0:F1}", NewValue);
                        l_RES_SHOPPING_DETAIL.TotalAmount = (Convert.ToDecimal(l_RES_SHOPPING_DETAIL.Price)
                            * Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY)).ToString();
                        Common.mCommon.ShoppingCartList[selectedIndex] = l_RES_SHOPPING_DETAIL;

                        mJSN_REQ_SHOPPING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                        mJSN_REQ_SHOPPING.RES_SHOPPING = Common.mCommon.Shopping;
                        mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList;

                       //Common.mCommon.ShoppingCartList.RemoveAt(selectedIndex);
                        Common.mCommon.ShoppingCartList = Clone(Common.mCommon.ShoppingCartList);
                        Common.mCommon.saveShoppingCart(mJSN_REQ_SHOPPING);

                        lblTotalAmount.Text = Common.mCommon.Shopping.GrandTotal;
                    }
                    else
                    {
                        TgrDeleteStock_Tapped(sender, e);
                    }
                
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        void stpQTy_ValueChanged(System.Object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            try
            {
                RES_SHOPPING_DETAIL l_RES_SHOPPING_DETAIL = new RES_SHOPPING_DETAIL();
                l_RES_SHOPPING_DETAIL =((RES_SHOPPING_DETAIL)((Label)sender).BindingContext);


               
                var selectedIndex = Common.mCommon.ShoppingCartList.IndexOf(
                ((RES_SHOPPING_DETAIL)((Label)sender).BindingContext));
                if (selectedIndex > -1)
                {
                    l_RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList[selectedIndex];
                    //stpQTy.Value = Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY);
                    if (e.NewValue > 0)
                    {
                        
                        l_RES_SHOPPING_DETAIL.QTY = String.Format("{0:F1}", e.NewValue);
                        l_RES_SHOPPING_DETAIL.TotalAmount = (Convert.ToDecimal(l_RES_SHOPPING_DETAIL.Price)
                            * Convert.ToDecimal(l_RES_SHOPPING_DETAIL.QTY)).ToString();
                        Common.mCommon.ShoppingCartList[selectedIndex] = l_RES_SHOPPING_DETAIL;
                    }
                    else if (e.NewValue == 0)
                    {
                        TgrDeleteStock_Tapped(sender, new EventArgs());
                    }
                    else
                    {

                    }
                    mJSN_REQ_SHOPPING.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                    mJSN_REQ_SHOPPING.RES_SHOPPING = Common.mCommon.Shopping;
                    mJSN_REQ_SHOPPING.RES_SHOPPING_DETAIL = Common.mCommon.ShoppingCartList;
                    Common.mCommon.saveShoppingCart(mJSN_REQ_SHOPPING);
                    //Common.mCommon.ShoppingCartList = Clone(Common.mCommon.ShoppingCartList);
                }
                lblTotalAmount.Text = Common.mCommon.Shopping.GrandTotal;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        #endregion





        
        
        private void TgrCheckOut_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("check-out"))
                {
                    //if no access direct err
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
                Navigation.PopPopupAsync();



                //if (Common.bindMenu("signin"))
                //{
                //    Common.routeMenu("signin", "Sign in");
                //}
                //else
                //{
                //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                //}
                //Common.routeMenu("check-out", "Check Out");
                //Navigation.PopPopupAsync();

                //var selectedIndex = IntercomService.Current.ShoppingCartList.IndexOf(
                //((RES_SHOPPING_DETAIL)((Label)sender).BindingContext));
                //if (selectedIndex > -1)
                //{
                //    IntercomService.Current.ShoppingCartList.RemoveAt(selectedIndex);
                //    IntercomService.Current.ShoppingCartList = Clone(IntercomService.Current.ShoppingCartList);

                //    if (IntercomService.Current.ShoppingCartList.Count == 0)
                //    {
                //        //need to set paddint to change checkout icon
                //        IntercomService.Current.BadgeViewCheckoutThickness = new Thickness(0, 10, 0, 0);
                //    }
                //    else
                //    {
                //        //need to set paddint to change checkout icon
                //        IntercomService.Current.BadgeViewCheckoutThickness = new Thickness(0, 0, 0, 0);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }

        List<T> Clone<T>(IEnumerable<T> oldList)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return new List<T>(oldList);
        }

        public void CheckoutTapped(object sender, EventArgs e)
        {
            try
            {
                // Dismiss the Menu.
                Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        
    }
}