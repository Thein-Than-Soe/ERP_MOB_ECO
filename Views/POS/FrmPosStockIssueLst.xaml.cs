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
    public partial class FrmPosStockIssueLst : ContentView
    {

        #region "Declaring"
        VmlStockIssue mVmlStockIssue;
        #endregion
        #region "Constructor"
        public FrmPosStockIssueLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlStockIssue = new VmlStockIssue();
                mVmlStockIssue.mJSN_REQ_INVENTORY_ISSUE_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlStockIssue.mJSN_REQ_INVENTORY_ISSUE_JUN.RES_INVENTORY_ISSUE = new RES_INVENTORY_ISSUE();
                mVmlStockIssue.mJSN_REQ_INVENTORY_ISSUE_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlStockIssue.getStockIssue();
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
                            TabActive.Text = "Active (" + mVmlStockIssue.IssueActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockIssue.IssuePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockIssue.IssueClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabActive.Text = "လုပ်ဆောင်ရန် (" + mVmlStockIssue.IssueActiveList.Count + ")";
                            TabPartial.Text = "တပိုင်းတစပြီးစီး (" + mVmlStockIssue.IssuePartialList.Count + ")";
                            TabClosed.Text = "ပြီးစီး (" + mVmlStockIssue.IssueClosedList.Count + ")";

                            AcCode.Title = "ကုန်ထွက်နံပါတ်";
                            AcDate.Title = "ကုန်ထုတ်ရက်စွဲ";
                            AcCustomer.Title = "၀ယ်ယူသူ";
                            AcLocation.Title = "ကုန်ရှိနေရာ";
                            AcIssueType.Title = "ကုန်ထွက်အမျိုးအစား";

                            PCode.Title = "ကုန်ထွက်နံပါတ်";
                            PDate.Title = "ကုန်ထုတ်ရက်စွဲ";
                            PCustomer.Title = "၀ယ်ယူသူ";
                            PLocation.Title = "ကုန်ရှိနေရာ";
                            PIssueType.Title = "ကုန်ထွက်အမျိုးအစား";

                            CCode.Title = "ကုန်ထွက်နံပါတ်";
                            CDate.Title = "ကုန်ထုတ်ရက်စွဲ";
                            CCustomer.Title = "၀ယ်ယူသူ";
                            CLocation.Title = "ကုန်ရှိနေရာ";
                            CIssueType.Title = "ကုန်ထွက်အမျိုးအစား";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabActive.Text = "Active (" + mVmlStockIssue.IssueActiveList.Count + ")";
                            TabPartial.Text = "Partial (" + mVmlStockIssue.IssuePartialList.Count + ")";
                            TabClosed.Text = "Closed (" + mVmlStockIssue.IssueClosedList.Count + ")";
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
                    mVmlStockIssue.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlStockIssue.searchData("");
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
                var l_RES_INVENTORY_ISSUE = (RES_INVENTORY_ISSUE)e.SelectedItem;
                if (l_RES_INVENTORY_ISSUE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_ISSUE.Ask) ;
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
                var l_RES_INVENTORY_ISSUE = (RES_INVENTORY_ISSUE)e.SelectedItem;
                if (l_RES_INVENTORY_ISSUE != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_INVENTORY_ISSUE.Ask);
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
                mVmlStockIssue.getStockIssue();
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
                //mVmlStockIssue.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}