using CS.ERP.PL.ACC.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.ACC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.ACC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmAccGeneralLedgerLst : ContentView
    {
        #region "Declaring"
        VmlAccountTransaction mVmlAccountTransaction;
        #endregion
        #region "Constructor"
        public FrmAccGeneralLedgerLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlAccountTransaction = new VmlAccountTransaction();
                mVmlAccountTransaction.mJSN_REQ_ACCOUNT_TRANSACTION.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlAccountTransaction.mJSN_REQ_ACCOUNT_TRANSACTION.DAT_ACCOUNT_TRANSACTION.Add(new DAT_ACCOUNT_TRANSACTION());
                mVmlAccountTransaction.getAccountTransaction();
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
                            TabDebit.BadgeText = "Debit (" + mVmlAccountTransaction.AccountTransactionDebitList.Count + ")";
                            TabCredit.BadgeText = "Credit (" + mVmlAccountTransaction.AccountTransactionCreditList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccountTransaction.AccountTransactionList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabDebit.BadgeText = "ငွေကြိုရှင်း (" + mVmlAccountTransaction.AccountTransactionDebitList.Count + ")";
                            TabCredit.BadgeText = "အကြွေ (" + mVmlAccountTransaction.AccountTransactionCreditList.Count + ")";
                            TabAll.BadgeText = "အားလုံး (" + mVmlAccountTransaction.AccountTransactionList.Count + ")";

                            ACode.MappingName = "ကုဒ်";
                            AName.MappingName = "အမည်";
                            ACurrency.MappingName = "အကောင့်အမျိုးအစား";
                            AAmount.MappingName = "အခြေအနေ";

                            CCode.MappingName = "ကုဒ်";
                            CName.MappingName = "အမည်";
                            CCurrency.MappingName = "အကောင့်အမျိုးအစား";
                            CAmout.MappingName = "အခြေအနေ";

                            DCode.MappingName = "ကုဒ်";
                            DName.MappingName = "အမည်";
                            DCurrency.MappingName = "အကောင့်အမျိုးအစား";
                            DAmount.MappingName = "အခြေအနေ";

                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabDebit.BadgeText = "Debit (" + mVmlAccountTransaction.AccountTransactionDebitList.Count + ")";
                            TabCredit.BadgeText = "Credit (" + mVmlAccountTransaction.AccountTransactionCreditList.Count + ")";
                            TabAll.BadgeText = "All (" + mVmlAccountTransaction.AccountTransactionList.Count + ")";
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
                    mVmlAccountTransaction.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlAccountTransaction.searchData("");
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
                var l_DAT_ACCOUNT_TRANSACTION = (DAT_ACCOUNT_TRANSACTION)e.SelectedItem;
                if (l_DAT_ACCOUNT_TRANSACTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT_TRANSACTION.Ask) ;
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
                var l_DAT_ACCOUNT_TRANSACTION = (DAT_ACCOUNT_TRANSACTION)e.SelectedItem;
                if (l_DAT_ACCOUNT_TRANSACTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_ACCOUNT_TRANSACTION.Ask);
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
                mVmlAccountTransaction.getAccountTransaction();
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
                //mVmlAccountTransaction.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}