using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.ViewsModel.ATT;
using CS.ERP_MOB.ViewsModel.Frame;
using CS.ERP_MOB.ViewsModel.SYS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTEarlyOutRequestLst : ContentView
    {
        #region "Declaring"
        VmlEarlyOutRequest mVmlEarlyOutRequest;
        #endregion
        #region "Constructor"
        public FrmATTEarlyOutRequestLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlEarlyOutRequest = new VmlEarlyOutRequest();
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlEarlyOutRequest.mJSN_REQ_EMPLOYEE_EARLY_OUT_REQUEST.DAT_EMPLOYEE_EARLY_OUT_REQUEST.Add(new DAT_EMPLOYEE_EARLY_OUT_REQUEST());
                mVmlEarlyOutRequest.getEarlyOut();
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
                            TabSubmit.Text = "Submit (" + mVmlEarlyOutRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlEarlyOutRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlEarlyOutRequest.RejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlEarlyOutRequest.SubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlEarlyOutRequest.ApprovedList.Count + ")";
                            TabReject.Text = "ငြင်းပယ်ချက် (" + mVmlEarlyOutRequest.RejectList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SEarlyOutReason.Title = "စောထွက်ရခြင်းအကြောင်း";
                            SEarlyOutDate.Title = "အစောပိုင်းထွက်ရက်စွဲ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            AEarlyOutReason.Title = "စောထွက်ရခြင်းအကြောင်း";
                            AEarlyOutDate.Title = "အစောပိုင်းထွက်ရက်စွဲ";

                            REmployee.Title = "ဝန်ထမ်း";
                            REarlyOutReason.Title = "စောထွက်ရခြင်းအကြောင်း";
                            REarlyOutDate.Title = "အစောပိုင်းထွက်ရက်စွဲ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlEarlyOutRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlEarlyOutRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlEarlyOutRequest.RejectList.Count + ")";
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
                if (!Common.bindMenu("att-early-out-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("att-early-out-request-set"))
                //{
                //    Common.routeMenu("att-early-out-request-set", "Access Entry");
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
                    mVmlEarlyOutRequest.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlEarlyOutRequest.searchData("");
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
                var l_DAT_EMPLOYEE_EARLY_OUT_REQUEST = (DAT_EMPLOYEE_EARLY_OUT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-early-out-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTEarlyOutRequestSet", MenuUrl = "att-early-out-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.Ask);

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
                var l_DAT_EMPLOYEE_EARLY_OUT_REQUEST = (DAT_EMPLOYEE_EARLY_OUT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_EARLY_OUT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-early-out-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTEarlyOutRequestSet", MenuUrl = "att-early-out-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_EARLY_OUT_REQUEST.Ask);

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
                if (!Common.bindMenu("att-early-out-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

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
                mVmlEarlyOutRequest.getEarlyOut();
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
                //mVmlEarlyOutRequest.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}