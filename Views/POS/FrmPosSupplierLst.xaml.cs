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
    public partial class FrmPosSupplierLst : ContentView
    {

        #region "Declaring"
        VmlSupplier mVmlSupplier;
       
        #endregion
        #region "Constructor"
        public FrmPosSupplierLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSupplier = new VmlSupplier();
                mVmlSupplier.mJSN_REQ_SUPPLIER.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSupplier.mJSN_REQ_SUPPLIER.RES_SUPPLIER.Add(new RES_SUPPLIER());
                mVmlSupplier.getSupplier();
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
                                TabActive.Text = "Active (" + mVmlSupplier.SupplierActiveList.Count + ")";
                                TabInActive.Text = "InActive (" + mVmlSupplier.SupplierInActiveList.Count + ")";
                                TabAll.Text = "All (" + mVmlSupplier.SupplierList.Count + ")";
                            }
                            break;
                        case "2"://Myanmar
                            {
                                entSearch.Placeholder = "ရှာဖွေရန်";
                                TabActive.Text = "အသုံးပြု (" + mVmlSupplier.SupplierActiveList.Count + ")";
                                TabInActive.Text = "အသုံးမပြု (" + mVmlSupplier.SupplierInActiveList.Count + ")";
                                TabAll.Text = "အားလုံး (" + mVmlSupplier.SupplierList.Count + ")";
                               
                                SName.Title = "အမည်";
                                SCode.Title = "ကုဒ်";
                                SEmail.Title = "အီးမေးလ်";
                                SPh.Title = "ဖုန်းနံပါတ်";

                            AName.Title = "အမည်";
                            ACode.Title = "ကုဒ်";
                            AEmail.Title = "အီးမေးလ်";
                            APh.Title = "ဖုန်းနံပါတ်";

                            IName.Title = "အမည်";
                            ICode.Title = "ကုဒ်";
                            IEmail.Title = "အီးမေးလ်";
                            IPh.Title = "ဖုန်းနံပါတ်";
                        }
                            break;
                        default:
                            {
                                entSearch.Placeholder = "Search";
                                TabActive.Text = "Active (" + mVmlSupplier.SupplierActiveList.Count + ")";
                                TabInActive.Text = "InActive (" + mVmlSupplier.SupplierInActiveList.Count + ")";
                                TabAll.Text = "All (" + mVmlSupplier.SupplierList.Count + ")";

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
                    mVmlSupplier.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlSupplier.searchData("");
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
                var l_RES_SUPPLIER = (RES_SUPPLIER)e.SelectedItem;
                if (l_RES_SUPPLIER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SUPPLIER.Ask) ;
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
                var l_RES_SUPPLIER = (RES_SUPPLIER)e.SelectedItem;
                if (l_RES_SUPPLIER != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_RES_SUPPLIER.Ask);
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
                mVmlSupplier.getSupplier();
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
                //mVmlSupplier.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }
}