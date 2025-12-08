using CS.ERP.PL.CRM.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;


namespace CS.ERP_MOB.Views.CRM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmCrmTicketLogLst : ContentView
    {

        #region "Declaring"
        VmlTicketLog mVmlTicketLog;
        #endregion
        #region "Constructor"
        public FrmCrmTicketLogLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlTicketLog = new VmlTicketLog();
                mVmlTicketLog.mJSN_REQ_TICKET_LOG.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlTicketLog.mJSN_REQ_TICKET_LOG.DAT_TICKET_LOG.Add(new DAT_TICKET_LOG());
                mVmlTicketLog.getTicketLog();
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
                            TabNewReq.Text = "New Request (" + mVmlTicketLog.NewRequestList.Count + ")";
                            TabChangeReq.Text = "Change Request (" + mVmlTicketLog.ChangeRequestList.Count + ")";
                            TabError.Text = "Error (" + mVmlTicketLog.ErrorRequestList.Count + ")";
                            TabAll.Text = "All (" + mVmlTicketLog.TicketLogList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabNewReq.Text = "တောင်းဆိုချက်အသစ် (" + mVmlTicketLog.NewRequestList.Count + ")";
                            TabChangeReq.Text = "ပြောင်းလဲရန် တောင်းဆိုချက် (" + mVmlTicketLog.ChangeRequestList.Count + ")";
                            TabError.Text = "အမှားတောင်းဆိုချက် (" + mVmlTicketLog.ErrorRequestList.Count + ")";
                            TabAll.Text = "အားလုံး (" + mVmlTicketLog.TicketLogList.Count + ")";

                            NCode.Title = "ကုဒ်";
                            NName.Title = "အမည်";
                            NStatus.Title = "အခြေအနေအမည်";
                            NType.Title = "Assign To";
                          

                            CCode.Title = "ကုဒ်";
                            CName.Title = "အမည်";
                            CStatus.Title = "အခြေအနေအမည်";
                            CType.Title = "Assign To";
                           
                            ECode.Title = "ကုဒ်";
                            EName.Title = "အမည်";
                            EStatus.Title = "အခြေအနေအမည်";
                            EType.Title = "Assign To";
                          
                            ACode.Title = "ကုဒ်";
                            AName.Title = "အမည်";
                            AStatus.Title = "အခြေအနေအမည်";
                            AType.Title = "Assign To";                         
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabNewReq.Text = "New Request (" + mVmlTicketLog.NewRequestList.Count + ")";
                            TabChangeReq.Text = "Change Request (" + mVmlTicketLog.ChangeRequestList.Count + ")";
                            TabError.Text = "Error (" + mVmlTicketLog.ErrorRequestList.Count + ")";
                            TabAll.Text = "All (" + mVmlTicketLog.TicketLogList.Count + ")";
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
                    mVmlTicketLog.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlTicketLog.searchData("");
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
                var l_DAT_TICKET_LOG = (DAT_TICKET_LOG)e.SelectedItem;
                if (l_DAT_TICKET_LOG != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_TICKET_LOG.Ask) ;
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
                var l_DAT_TICKET_LOG = (DAT_TICKET_LOG)e.SelectedItem;
                if (l_DAT_TICKET_LOG != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_TICKET_LOG.Ask);
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
                mVmlTicketLog.getTicketLog();
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
                //mVmlTicketLog.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}