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
    public partial class FrmPosStockReceivedLst : ContentView
    {

        #region "Declaring"
        VmlStockReceived mVmlStockReceived;
        #endregion
        #region "Constructor"
        public FrmPosStockReceivedLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlStockReceived = new VmlStockReceived();
                mVmlStockReceived.mJSN_REQ_INVENTORY_RECEIVED_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlStockReceived.mJSN_REQ_INVENTORY_RECEIVED_JUN.RES_INVENTORY_RECEIVED = new RES_INVENTORY_RECEIVED();
                mVmlStockReceived.mJSN_REQ_INVENTORY_RECEIVED_JUN.RES_PURCHASE_BROWSE.Add(new RES_PURCHASE_BROWSE());
                mVmlStockReceived.getStockReceived();
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
                            TabActive.Text = "Active (" + mVmlStockReceived.ReceivedActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockReceived.ReceivedPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockReceived.ReceivedClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlStockReceived.ReceivedActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlStockReceived.ReceivedPartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlStockReceived.ReceivedClosedList.Count + ")";

                            AcCode.Title = "ကုန်၀င်နံပါတ်";
                            AcDate.Title = "ကုန်၀င်ရက်စွဲ";
                            AcSupplier.Title = "ကုန်တင်သွင်းသူ";
                            AcLocation.Title = "ကုန်ရှိနေရာ";
                            AcType.Title = "ကုန်၀င် အမျိုးအစား";

                            PCode.Title = "ကုန်၀င်နံပါတ်";
                            PDate.Title = "ကုန်၀င်ရက်စွဲ";
                            PSupplier.Title = "ကုန်တင်သွင်းသူ";
                            PLocation.Title = "ကုန်ရှိနေရာ";
                            PType.Title = "ကုန်၀င် အမျိုးအစား";

                            CCode.Title = "ကုန်၀င်နံပါတ်";
                            CDate.Title = "ကုန်၀င်ရက်စွဲ";
                            CSupplier.Title = "ကုန်တင်သွင်းသူ";
                            CLocation.Title = "ကုန်ရှိနေရာ";
                            CType.Title = "ကုန်၀င် အမျိုးအစား";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlStockReceived.ReceivedActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockReceived.ReceivedPartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockReceived.ReceivedClosedList.Count + ")";
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
                    mVmlStockReceived.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlStockReceived.searchData("");
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
                var l_RES_INVENTORY_RECEIVED = (RES_INVENTORY_RECEIVED)e.SelectedItem;
                if (l_RES_INVENTORY_RECEIVED != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_RECEIVED.Ask) ;
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
                var l_RES_INVENTORY_RECEIVED = (RES_INVENTORY_RECEIVED)e.SelectedItem;
                if (l_RES_INVENTORY_RECEIVED != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_RECEIVED.Ask);
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
                mVmlStockReceived.getStockReceived();
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
                //mVmlStockReceived.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}