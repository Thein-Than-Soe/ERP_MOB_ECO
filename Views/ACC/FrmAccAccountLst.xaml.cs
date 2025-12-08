using CS.ERP.PL.ACC.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.ACC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAccAccountLst : ContentView
    {
        #region "Declaring"
        VmlAccount mVmlAccount;
        #endregion
        #region "Constructor"
        public FrmAccAccountLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAccount = new VmlAccount();
                mVmlAccount.mJSN_REQ_ACCOUNT.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccount.mJSN_REQ_ACCOUNT.DAT_ACCOUNT.Add(new DAT_ACCOUNT());
                mVmlAccount.getAccount();
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
                            TabDebit.BadgeText = "Debit (" + mVmlAccount.AccountDebitList.Count + ")";
                            TabCredit.BadgeText = "Credit (" + mVmlAccount.AccountCreditList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccount.AccountList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabDebit.BadgeText = "ငွေကြိုရှင်း (" + mVmlAccount.AccountDebitList.Count + ")";
                            TabCredit.BadgeText = "အကြွေ (" + mVmlAccount.AccountCreditList.Count + ")";
                            TabAll.BadgeText = "အားလုံး (" + mVmlAccount.AccountList.Count + ")";
                      
                            SCode.HeaderText = "ကုဒ်";
                            SName.HeaderText = "အမည်";
                            SType.HeaderText = "အကောင့်အမျိုးအစား";
                            StatusName.HeaderText = "အခြေအနေ";
                          
                            CCode.HeaderText = "ကုဒ်";
                            CName.HeaderText = "အမည်";
                            CType.HeaderText = "အကောင့်အမျိုးအစား";
                            CStatus.HeaderText = "အခြေအနေ";
                            
                            DCode.HeaderText = "ကုဒ်";
                            DName.HeaderText = "အမည်";
                            DType.HeaderText = "အကောင့်အမျိုးအစား";
                            DStatus.HeaderText = "အခြေအနေ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabDebit.BadgeText = "Debit (" + mVmlAccount.AccountDebitList.Count + ")";
                            TabCredit.BadgeText = "Credit (" + mVmlAccount.AccountCreditList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccount.AccountList.Count + ")";
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
                    mVmlAccount.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlAccount.searchData("");
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
                var l_DAT_ACCOUNT = (DAT_ACCOUNT)e.SelectedItem;
                if (l_DAT_ACCOUNT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT.Ask) ;
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
                var l_DAT_ACCOUNT = (DAT_ACCOUNT)e.SelectedItem;
                if (l_DAT_ACCOUNT != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT.Ask);
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
                entSearch.Text ="";
                mVmlAccount.getAccount();
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
                //mVmlAccount.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}