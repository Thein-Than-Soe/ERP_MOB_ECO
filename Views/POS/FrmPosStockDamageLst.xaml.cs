using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;

namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosStockDamageLst : ContentView
    {

        #region "Declaring"
        VmlStockDamage mVmlStockDamage;
        #endregion
        #region "Constructor"
        public FrmPosStockDamageLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlStockDamage = new VmlStockDamage();
                mVmlStockDamage.mJSN_REQ_INVENTORY_DAMAGE.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlStockDamage.mJSN_REQ_INVENTORY_DAMAGE.RES_INVENTRY_DAMAGE = new RES_INVENTORY_DAMAGE();
                mVmlStockDamage.getStockDamage();
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
                            TabActive.Text = "Active (" + mVmlStockDamage.DamageActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockDamage.DamagePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockDamage.DamageClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlStockDamage.DamageActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlStockDamage.DamagePartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlStockDamage.DamageClosedList.Count + ")";

                            AcCode.Title = "ကုန်ပျက်စီးနံပါတ်";
                            AcDate.Title = "ကုန်ပျက်စီးကုန်ပျက်စီးရက်စွဲ";
                            AcType.Title = "ကုန်ပျက်စီးအမျိုးအစား";
                            AcLocation.Title = "ကုန်ရှိနေရာ";
                          

                            PCode.Title = "ကုန်ပျက်စီးနံပါတ်";
                            PDate.Title = "ကုန်ပျက်စီးရက်စွဲ";
                            AcType.Title = "ကုန်ပျက်စီးအမျိုးအစား";
                            AcLocation.Title = "ကုန်ရှိနေရာ";

                            CCode.Title = "ကုန်ပျက်စီးနံပါတ်";
                            CDate.Title = "ကုန်ပျက်စီးရက်စွဲ";
                            AcType.Title = "ကုန်ပျက်စီးအမျိုးအစား";
                            AcLocation.Title = "ကုန်ရှိနေရာ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlStockDamage.DamageActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockDamage.DamagePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockDamage.DamageClosedList.Count + ")";
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
                    mVmlStockDamage.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlStockDamage.searchData("");
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
                var l_RES_INVENTORY_DAMAGE = (RES_INVENTORY_DAMAGE)e.SelectedItem;
                if (l_RES_INVENTORY_DAMAGE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_DAMAGE.Ask) ;
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
                var l_RES_INVENTORY_DAMAGE = (RES_INVENTORY_DAMAGE)e.SelectedItem;
                if (l_RES_INVENTORY_DAMAGE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_DAMAGE.Ask);
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
                mVmlStockDamage.getStockDamage();
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
                //mVmlStockDamage.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}