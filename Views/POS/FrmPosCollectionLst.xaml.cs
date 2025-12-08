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
    public partial class FrmPosCollectionLst : ContentView
    {

        #region "Declaring"
        VmlCollection mVmlCollection;
        #endregion
        #region "Constructor"
        public FrmPosCollectionLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlCollection = new VmlCollection();
                mVmlCollection.mJSN_REQ_COLLECTION.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlCollection.mJSN_REQ_COLLECTION.DAT_COLLECTION_DETAIL.Add(new DAT_COLLECTION_DETAIL());
                mVmlCollection.getCollection();
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
                            TabOpen.Text = "Open (" + mVmlCollection.CollectionOpenList.Count + ")";
                            TabReady.Text = "Ready (" + mVmlCollection.CollectionReadyList.Count + ")";
                            TabClosed.Text = "Collected (" + mVmlCollection.CollectionClosedList.Count + ")";
                        }
                        break;
                    case "2"://Myanmar
                        {
                            entSearch.Placeholder = "ရှာဖွေရန်";
                            TabOpen.Text = "Open  (" + mVmlCollection.CollectionOpenList.Count + ")";
                            TabReady.Text = "Ready (" + mVmlCollection.CollectionReadyList.Count + ")";
                            TabClosed.Text = "Collected (" + mVmlCollection.CollectionClosedList.Count + ")";
                         
                            OCode.Title = "ကုန်စုဆောင်းမှု နံပါတ်";
                            OCustomer.Title = "၀ယ်ယူသူ";
                            ODate.Title = "စုဆောင်းသည့်ရက်စွဲ";
                            OParent.Title = "မူလနံပါတ်";
                            OAmount.Title = "ပေးရန်ကျန်ငွေ";

                            OCode.Title = "ကုန်စုဆောင်းမှု နံပါတ်";
                            OCustomer.Title = "၀ယ်ယူသူ";
                            ODate.Title = "စုဆောင်းသည့်ရက်စွဲ";
                            OParent.Title = "မူလနံပါတ်";
                            OAmount.Title = "ပေးရန်ကျန်ငွေ";

                            CCode.Title = "ကုန်စုဆောင်းမှု နံပါတ်";
                            CCustomer.Title = "၀ယ်ယူသူ";
                            CDate.Title = "စုဆောင်းသည့်ရက်စွဲ";
                            CParent.Title = "မူလနံပါတ်";
                            CAmount.Title = "ပေးရန်ကျန်ငွေ";
                        }
                        break;
                    default:
                        {
                            entSearch.Placeholder = "Search";
                            TabOpen.Text = "Open (" + mVmlCollection.CollectionOpenList.Count + ")";
                            TabReady.Text = "Ready (" + mVmlCollection.CollectionReadyList.Count + ")";
                            TabClosed.Text = "Collected (" + mVmlCollection.CollectionClosedList.Count + ")";
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
                    mVmlCollection.searchData(e.NewTextValue);
                }
                else
                {
                    mVmlCollection.searchData("");
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
                var l_DAT_COLLECTION = (DAT_COLLECTION)e.SelectedItem;
                if (l_DAT_COLLECTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);

                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_COLLECTION.Ask) ;
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
                var l_DAT_COLLECTION = (DAT_COLLECTION)e.SelectedItem;
                if (l_DAT_COLLECTION != null)
                {
                    if (!Common.bindMenu("access-set"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                        MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);


                    //if (Common.bindMenu("access-set"))
                    //{
                    //    Common.routeMenuStr("access-set", "Access Entry", l_DAT_COLLECTION.Ask);
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
                mVmlCollection.getCollection();
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
                //mVmlCollection.GetAccessData();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        #endregion
    }

}