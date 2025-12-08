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
    public partial class FrmPosPurchaseRequestionLst : ContentView
    {

        #region "Declaring"
        VmlPurchaseRequestion mVmlPurchaseRequestion;
        #endregion
        #region "Constructor"
        public FrmPosPurchaseRequestionLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlPurchaseRequestion = new VmlPurchaseRequestion();
                mVmlPurchaseRequestion.mJSN_REQ_PURCHASE_REQUESTION.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlPurchaseRequestion.mJSN_REQ_PURCHASE_REQUESTION.RES_PURCHASE_REQUESTION= new RES_PURCHASE_REQUESTION();
                mVmlPurchaseRequestion.getRequestion();
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
                            TabActive.Text = "Active (" + mVmlPurchaseRequestion.RequestionActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseRequestion.RequestionPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseRequestion.RequestionClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlPurchaseRequestion.RequestionActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlPurchaseRequestion.RequestionPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlPurchaseRequestion.RequestionClosedList.Count + ")";

                            CCode.Title = "ကုန်လျှောက်တင်အမှတ်";
                            CDate.Title = "ကုန်လျှောက်တင် နေ့စွဲ";
                            CRequester.Title = "ကုန်လျှောက်တင်သူ";

                            PCode.Title = "ကုန်လျှောက်တင်အမှတ်";
                            PDate.Title = "ကုန်လျှောက်တင် နေ့စွဲ";
                            PRequester.Title = "ကုန်လျှောက်တင်သူ";

                            ACode.Title = "ကုန်လျှောက်တင်အမှတ်";
                            ADate.Title = "ကုန်လျှောက်တင် နေ့စွဲ";
                            ARequester.Title = "ကုန်လျှောက်တင်သူ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlPurchaseRequestion.RequestionActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlPurchaseRequestion.RequestionPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlPurchaseRequestion.RequestionClosedList.Count + ")";
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
                    mVmlPurchaseRequestion.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlPurchaseRequestion.searchData("");
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
                var l_RES_PURCHASE_REQUESTION = (RES_PURCHASE_REQUESTION)e.SelectedItem;
                if (l_RES_PURCHASE_REQUESTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_REQUESTION.Ask) ;
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
                var l_RES_PURCHASE_REQUESTION = (RES_PURCHASE_REQUESTION)e.SelectedItem;
                if (l_RES_PURCHASE_REQUESTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_PURCHASE_REQUESTION.Ask);
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
                mVmlPurchaseRequestion.getRequestion();
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
                //mVmlPurchaseRequestion.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}