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
    public partial class FrmPosStockTransferLst : ContentView
    {

        #region "Declaring"
        VmlStockTransfer mVmlStockTransfer;
        #endregion
        #region "Constructor"
        public FrmPosStockTransferLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlStockTransfer = new VmlStockTransfer();
                mVmlStockTransfer.mJSN_REQ_INVENTORY_TRANSFER.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlStockTransfer.mJSN_REQ_INVENTORY_TRANSFER.RES_INVENTRY_TRANSFER = new ERP.PL.POS.DAT.RES_INVENTORY_TRANSFER();
                mVmlStockTransfer.getStockTransfer();
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
                            TabActive.Text = "Active (" + mVmlStockTransfer.TransferActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockTransfer.TransferPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockTransfer.TransferClosedlist.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlStockTransfer.TransferActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီ (" + mVmlStockTransfer.TransferPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlStockTransfer.TransferClosedlist.Count + ")";

                            AcCode.Title = "ကုန်အရွေ့အပြောင်းနံပါတ်";
                            AcDate.Title = "ကုန်အရွေ့အပြောင်းရက်စွဲ";
                            AcFrom.Title = "မှ နေရာမှ";
                            AcTo.Title = "သို့ နေရာသို့";
                            AcType.Title = "ကုန်အရွေ့အပြောင်း အမျိုးအစား";

                            PCode.Title = "ကုန်အရွေ့အပြောင်းနံပါတ်";
                            PDate.Title = "ကုန်အရွေ့အပြောင်းရက်စွဲ";
                            PFrom.Title = "မှ နေရာမှ";
                            PTo.Title = "သို့ နေရာသို့";
                            PType.Title = "ကုန်အရွေ့အပြောင်း အမျိုးအစား";

                            CCode.Title = "ကုန်အရွေ့အပြောင်းနံပါတ်";
                            CDate.Title = "ကုန်အရွေ့အပြောင်းရက်စွဲ";
                            CFrom.Title = "မှ နေရာမှ";
                            CTo.Title = "သို့ နေရာသို့";
                            CType.Title = "ကုန်အရွေ့အပြောင်း အမျိုးအစား";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlStockTransfer.TransferActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockTransfer.TransferPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockTransfer.TransferClosedlist.Count + ")";
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
                    mVmlStockTransfer.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlStockTransfer.searchData("");
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
                var l_RES_INVENTRY_TRANSFER = (RES_INVENTORY_TRANSFER)e.SelectedItem;
                if (l_RES_INVENTRY_TRANSFER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTRY_TRANSFER.Ask) ;
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
                var l_RES_INVENTRY_TRANSFER = (RES_INVENTORY_TRANSFER)e.SelectedItem;
                if (l_RES_INVENTRY_TRANSFER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTRY_TRANSFER.Ask);
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
                mVmlStockTransfer.getStockTransfer();
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
                //mVmlStockTransfer.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion
    }

}