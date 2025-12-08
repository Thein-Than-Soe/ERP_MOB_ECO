using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosPurchaseReturnLst : ContentView
    {

        #region "Declaring"
        VmlPurchaseReturn mVmlPurchaseReturn;
        #endregion
        #region "Constructor"
        public FrmPosPurchaseReturnLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchaseReturn = new VmlPurchaseReturn();
                mVmlPurchaseReturn.mJSN_REQ_PURCHASE_RETURN_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchaseReturn.mJSN_REQ_PURCHASE_RETURN_JUN.RES_PURCHASE_RETURN = new RES_PURCHASE_RETURN();
                mVmlPurchaseReturn.mJSN_REQ_PURCHASE_RETURN_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchaseReturn.mJSN_REQ_PURCHASE_RETURN_JUN.RES_PAYMENT_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlPurchaseReturn.getReturn();
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
                            TabActive.Text = "Active (" + mVmlPurchaseReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseReturn.ReturnClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchaseReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီ (" + mVmlPurchaseReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchaseReturn.ReturnClosedList.Count + ")";

                            CCode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            CDate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            CParent.Title = "မူရင်း ကုဒ်";
                            CTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            CSupplier.Title = "ကုန်တင်သွင်းသူ";

                            ACode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            ADate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            AParent.Title = "မူရင်း ကုဒ်";
                            ATotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            ASupplier.Title = "ကုန်တင်သွင်းသူ";

                            PCode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            PDate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            PParent.Title = "မူရင်း ကုဒ်";
                            PTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            PSupplier.Title = "ကုန်တင်သွင်းသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchaseReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseReturn.ReturnClosedList.Count + ")";
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
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
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
                    mVmlPurchaseReturn.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchaseReturn.searchData("");
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
                var l_RES_PURCHASE_RETURN = (RES_PURCHASE_RETURN)e.SelectedItem;
                if (l_RES_PURCHASE_RETURN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_RETURN.Ask) ;
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
        private void grdView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_PURCHASE_RETURN = (RES_PURCHASE_RETURN)e.SelectedItem;
                if (l_RES_PURCHASE_RETURN != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_RETURN.Ask);
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
                if (!Common.bindMenu("access-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("access-set"))
                //{
                //    Common.routeMenu("access-set", "Access Entry");
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
                entSearch.Text = "";
                mVmlPurchaseReturn.getReturn();
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
                //mVmlPurchaseReturn.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}