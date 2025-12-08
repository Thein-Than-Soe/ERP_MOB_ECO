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
    public partial class FrmPosSaleReturnLst : ContentView
    {

        #region "Declaring"
        VmlSalesReturn mVmlSalesReturn;
        #endregion
        #region "Constructor"
        public FrmPosSaleReturnLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesReturn = new VmlSalesReturn();
                mVmlSalesReturn.mJSN_REQ_SALE_RETURN_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesReturn.mJSN_REQ_SALE_RETURN_JUN.RES_SALE_RETURN = new RES_SALE_RETURN();
                mVmlSalesReturn.mJSN_REQ_SALE_RETURN_JUN.RES_RETURN_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesReturn.mJSN_REQ_SALE_RETURN_JUN.RES_PAYMENT_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesReturn.mJSN_REQ_SALE_RETURN_JUN.RES_SALE_PAYMENT = new RES_SALE_PAYMENT();
                mVmlSalesReturn.getReturn();
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
                            TabActive.Text = "Active (" + mVmlSalesReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesReturn.ReturnClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "စာရင်းအဖွင့် (" + mVmlSalesReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "လုပ်ဆောင်ဆဲ (" + mVmlSalesReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "စာရင်းအပိတ် (" + mVmlSalesReturn.ReturnClosedList.Count + ")";

                            CCode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            CDate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            CParent.Title = "မူရင်း ကုဒ်";                           
                            CTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            CCustomer.Title = "၀ယ်ယူသူ";

                            AcCode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            AcDate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            AcParent.Title = "မူရင်း ကုဒ်";
                            AcTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            AcCustomer.Title = "၀ယ်ယူသူ";

                            PCode.Title = "ကုန်ပြန်ပို့ အမှတ်";
                            PDate.Title = "ကုန်ပြန်ပို့နေ့စွဲ";
                            PParent.Title = "မူရင်း ကုဒ်";
                            PTotal.Title = "စုစုပေါင်း သင့်ငွေ";
                            PCustomer.Title = "၀ယ်ယူသူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlSalesReturn.ReturnActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlSalesReturn.ReturnPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlSalesReturn.ReturnClosedList.Count + ")";
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
                    mVmlSalesReturn.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSalesReturn.searchData("");
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
                mVmlSalesReturn.getReturn();
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
                //mVmlSalesReturn.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }


}