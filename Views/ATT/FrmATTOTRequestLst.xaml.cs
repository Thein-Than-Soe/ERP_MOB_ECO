
using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;

using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.ViewsModel.ATT;
using CS.ERP_MOB.ViewsModel.Frame;

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
    public partial class FrmATTOTRequestLst : ContentView
    {
        #region "Declaring"
        VmlOTRequest mVmlOTRequest;
        #endregion
        #region "Constructor"
        public FrmATTOTRequestLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlOTRequest = new VmlOTRequest();
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlOTRequest.mJSN_REQ_OT_REQUEST.DAT_EMPLOYEE_OT_REQUEST.Add(new DAT_EMPLOYEE_OT_REQUEST());
                mVmlOTRequest.getOTRequest();
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
                            TabSubmit.Text = "Submit (" + mVmlOTRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlOTRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlOTRequest.RejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlOTRequest.SubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlOTRequest.ApprovedList.Count + ")";
                            TabReject.Text = "ငြင်းပယ်ချက် (" + mVmlOTRequest.RejectList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SOTReason.Title = "အချိန်ပိုအကြောင်းပြချက်";
                            SOTDate.Title = "အချိန်ပိုရက်စွဲ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            AOTReason.Title = "အချိန်ပိုအကြောင်းပြချက်";
                            AOTDate.Title = "အချိန်ပိုရက်စွဲ";

                            REmployee.Title = "ဝန်ထမ်း";
                            ROTReason.Title = "အချိန်ပိုအကြောင်းပြချက်";
                            ROTDate.Title = "အချိန်ပိုရက်စွဲ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlOTRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlOTRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlOTRequest.RejectList.Count + ")";
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
                if (!Common.bindMenu("att-ot-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("att-ot-request-set"))
                //{
                //    Common.routeMenu("att-ot-request-set", "Access Entry");
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
                    mVmlOTRequest.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlOTRequest.searchData("");
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
                var l_DAT_EMPLOYEE_OT_REQUEST = (DAT_EMPLOYEE_OT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_OT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-ot-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTOTRequestSet", MenuUrl = "att-ot-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_OT_REQUEST.Ask);

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
                var l_DAT_EMPLOYEE_OT_REQUEST = (DAT_EMPLOYEE_OT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_OT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-ot-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmATTOTRequestSet", MenuUrl = "att-ot-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_OT_REQUEST.Ask);

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
                if (!Common.bindMenu("att-ot-request-set"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //if (Common.bindMenu("att-ot-request-set"))
                //{
                //    Common.routeMenu("att-ot-request-set", "Access Entry");
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
                mVmlOTRequest.getOTRequest();
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
                //mVmlOTRequest.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}