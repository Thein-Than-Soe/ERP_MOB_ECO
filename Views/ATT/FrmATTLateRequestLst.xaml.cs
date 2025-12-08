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
    public partial class FrmATTLateRequestLst : ContentView
    {
        #region "Declaring"
        VmlLateRequest mVmlLateRequest;
        #endregion
        #region "Constructor"
        public FrmATTLateRequestLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlLateRequest = new VmlLateRequest();
                mVmlLateRequest.mJSN_REQ_EMPLOYEE_LATE_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlLateRequest.mJSN_REQ_EMPLOYEE_LATE_REQUEST.DAT_EMPLOYEE_LATE_REQUEST.Add(new DAT_EMPLOYEE_LATE_REQUEST());
                mVmlLateRequest.getLateRequest();
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
                            TabSubmit.Text = "Submit (" + mVmlLateRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLateRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlLateRequest.RejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlLateRequest.SubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlLateRequest.ApprovedList.Count + ")";
                            TabReject.Text = "ငြင်းပယ်ချက် (" + mVmlLateRequest.RejectList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SLateReason.Title = "နောက်ကျရခြင်း အကြောင်းအရင်း";
                            SLateDate.Title = "နောက်ကျသောရက်စွဲ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            ALateReason.Title = "နောက်ကျရခြင်း အကြောင်းအရင်း";
                            ALateDate.Title = "နောက်ကျသောရက်စွဲ";

                            REmployee.Title = "ဝန်ထမ်း";
                            RLateReason.Title = "နောက်ကျရခြင်း အကြောင်းအရင်း";
                            RLateDate.Title = "နောက်ကျသောရက်စွဲ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlLateRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlLateRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlLateRequest.RejectList.Count + ")";
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
                if (!Common.bindMenu("att-late-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("att-late-request-set"))
                //{
                //    Common.routeMenu("att-late-request-set", "Access Entry");
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
                    mVmlLateRequest.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlLateRequest.searchData("");
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
                var l_DAT_EMPLOYEE_LATE_REQUEST = (DAT_EMPLOYEE_LATE_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_LATE_REQUEST != null)
                {
                    if (!Common.bindMenu("att-late-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTLateRequestSet", MenuUrl = "att-late-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_LATE_REQUEST.Ask);

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
                var l_DAT_EMPLOYEE_LATE_REQUEST = (DAT_EMPLOYEE_LATE_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_LATE_REQUEST != null)
                {
                    if (!Common.bindMenu("att-late-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTLateRequestSet", MenuUrl = "att-late-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_LATE_REQUEST.Ask);

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
                if (!Common.bindMenu("att-late-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("att-late-request-set"))
                //{
                //    Common.routeMenu("att-late-request-set", "Access Entry");
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
                mVmlLateRequest.getLateRequest();
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
                //mVmlLateRequest.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion

    }
}