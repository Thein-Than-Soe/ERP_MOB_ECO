using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ATT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTOndutyLst : ContentView
    {
        #region "Declaring"
        VmlOnduty mVmlOnduty;
        #endregion
        #region "Constructor"
        public FrmATTOndutyLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOnduty = new VmlOnduty();
                mVmlOnduty.mJSN_REQ_ON_DUTY.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOnduty.mJSN_REQ_ON_DUTY.DAT_ON_DUTY.Add(new DAT_ON_DUTY());
                mVmlOnduty.getOnDuty();
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
                            TabOnservice.Text = "OnService (" + mVmlOnduty.OnSiteList.Count + ")";
                            TabOnsite.Text = "OnSite (" + mVmlOnduty.OnServiceList.Count + ")";
                            TabOnevent.Text = "OnEvent (" + mVmlOnduty.OnEventList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOnservice.Text = "OnService (" + mVmlOnduty.OnSiteList.Count + ")";
                            TabOnsite.Text = "OnSite (" + mVmlOnduty.OnServiceList.Count + ")";
                            TabOnevent.Text = "OnEvent (" + mVmlOnduty.OnEventList.Count + ")";

                            OEEmployee.Title = "ဝန်ထမ်း";
                            OEInOut.Title = "ဝင်/ထွက်ချိန်";
                            OESD.Title = "စတင်သည့်ရက်စွဲ";
                            OEED.Title = "ကုန်ဆုံးရက်";

                            OSEmployee.Title = "ဝန်ထမ်း";
                            OSInOut.Title = "ဝင်/ထွက်ချိန်";
                            OSSD.Title = "စတင်သည့်ရက်စွဲ";
                            OSED.Title = "ကုန်ဆုံးရက်";

                            OSiteEmployee.Title = "ဝန်ထမ်း";
                            OSiteInOut.Title = "ဝင်/ထွက်ချိန်";
                            OSiteSD.Title = "စတင်သည့်ရက်စွဲ";
                            OSiteED.Title = "ကုန်ဆုံးရက်";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOnservice.Text = "OnService (" + mVmlOnduty.OnSiteList.Count + ")";
                            TabOnsite.Text = "OnSite (" + mVmlOnduty.OnServiceList.Count + ")";
                            TabOnevent.Text = "OnEvent (" + mVmlOnduty.OnEventList.Count + ")";
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
                if (!Common.bindMenu("att-onduty-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("att-onduty-set"))
                //{
                //    Common.routeMenu("att-onduty-set", "Access Entry");
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
                    mVmlOnduty.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlOnduty.searchData("");
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
                var l_DAT_ON_DUTY = (DAT_ON_DUTY)e.SelectedItem;
                if (l_DAT_ON_DUTY != null)
                {
                    if (!Common.bindMenu("att-onduty-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTOndutySet", MenuUrl = "att-onduty-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_ON_DUTY.Ask);

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
                var l_DAT_ON_DUTY = (DAT_ON_DUTY)e.SelectedItem;
                if (l_DAT_ON_DUTY != null)
                {
                    if (!Common.bindMenu("att-onduty-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTOndutySet", MenuUrl = "att-onduty-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_ON_DUTY.Ask);

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
                if (!Common.bindMenu("att-onduty-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("att-onduty-set"))
                //{
                //    Common.routeMenu("att-onduty-set", "Access Entry");
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
                mVmlOnduty.getOnDuty();
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
                //mVmlOnduty.getOnDutyData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}