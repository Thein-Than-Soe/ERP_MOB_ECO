using CS.ERP.PL.ATT.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ATT;
using CS.ERP_MOB.ViewsModel.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.ATT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmATTAbsentLst : ContentView
    {
        #region "Declaring"
        VmlAbsentRequest mVmlAbsentRequest;
        #endregion
        #region "Constructor"
        public FrmATTAbsentLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAbsentRequest = new VmlAbsentRequest();
                mVmlAbsentRequest.mJSN_REQ_EMPLOYEE_ABSENT_REQUEST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAbsentRequest.mJSN_REQ_EMPLOYEE_ABSENT_REQUEST.DAT_EMPLOYEE_ABSENT_REQUEST.Add(new DAT_EMPLOYEE_ABSENT_REQUEST());
                mVmlAbsentRequest.getAbsentRequest();
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
                            TabSubmit.Text = "Submit (" + mVmlAbsentRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlAbsentRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlAbsentRequest.RejectList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabSubmit.Text = "တင်ပြချက် (" + mVmlAbsentRequest.SubmitList.Count + ")";
                            TabApproved.Text = "အတည်ပြုချက် (" + mVmlAbsentRequest.ApprovedList.Count + ")";
                            TabReject.Text = "ငြင်းပယ်ချက် (" + mVmlAbsentRequest.RejectList.Count + ")";

                            SEmployee.Title = "ဝန်ထမ်း";
                            SAbsentReason.Title = "ပျက်ရခြင်းအကြောင်း";
                            SAbsendDate.Title = "ပျက်ကွက်ရက်စွဲ";

                            AEmployee.Title = "ဝန်ထမ်း";
                            AAbsentReason.Title = "ပျက်ရခြင်းအကြောင်း";
                            AAbsendDate.Title = "ပျက်ကွက်ရက်စွဲ";

                            REmployee.Title = "ဝန်ထမ်း";
                            RAbsentReason.Title = "ပျက်ရခြင်းအကြောင်း";
                            RAbsendDate.Title = "ပျက်ကွက်ရက်စွဲ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabSubmit.Text = "Submit (" + mVmlAbsentRequest.SubmitList.Count + ")";
                            TabApproved.Text = "Approved (" + mVmlAbsentRequest.ApprovedList.Count + ")";
                            TabReject.Text = "Reject (" + mVmlAbsentRequest.RejectList.Count + ")";
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
                    mVmlAbsentRequest.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlAbsentRequest.searchData("");
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
                var l_DAT_EMPLOYEE_ABSENT_REQUEST = (DAT_EMPLOYEE_ABSENT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_ABSENT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-absent-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "att-absent-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);
                   
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
                var l_DAT_EMPLOYEE_ABSENT_REQUEST = (DAT_EMPLOYEE_ABSENT_REQUEST)e.SelectedItem;
                if (l_DAT_EMPLOYEE_ABSENT_REQUEST != null)
                {
                    if (!Common.bindMenu("att-absent-request-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAbsentRequestSet", MenuUrl = "att-absent-request-set", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu, l_DAT_EMPLOYEE_ABSENT_REQUEST.Ask);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_EMPLOYEE_ABSENT_REQUEST.Ask);
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
                mVmlAbsentRequest.getAbsentRequest();
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
                //mVmlAbsentRequest.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}