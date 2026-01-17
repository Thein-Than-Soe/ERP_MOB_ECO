using System.Xml.Xsl;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUserProfile : PopupPage
    {
        #region "Declaring"
        List<DbUser> mDbUser = new List<DbUser>();
        int index = 0;
        #endregion
        #region "Constructor"
        public PopUserProfile()
        {
            try
            {
                InitializeComponent();
                //double screenWidth = Application.Current.MainPage.Width;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                if (Common.mCommon.UserLoggedIn)
                {
                    
                    if (Common.mCommon.mDbUser_LST.Count > 0)
                    {
                        foreach (DbUser l_user in Common.mCommon.mDbUser_LST)
                        {
                            if (l_user.UserAsk.ToLower() != Common.mCommon.User.UserAsk && l_user.UserID.ToLower()!="guest")
                            {
                                mDbUser.Add(l_user);
                            }
                        }
                    }
                    BindableLayout.SetItemsSource(sltOtherUser, mDbUser);
                    if (mDbUser.Count <= 0)
                    {
                        sltContent.IsVisible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Private Method"
        private async void OnSwipeRight(object sender, SwipedEventArgs e)
        {
            await this.TranslateTo(100, 0, 150);
            await Navigation.PopAsync(); // Go back to previous page
        }
        #endregion
        #region "Public Method"
        #endregion
        #region "Property"
        #endregion

        #region "Event"
        private async void btnChangePassword_onClicked(object sender, EventArgs e)
        {
            try
            {
                //if (Common.mCommon.mDbUser_LST.Count > 0)
                //{
                //    foreach (DbUser l_user in Common.mCommon.mDbUser_LST)
                //    {
                //        l_user.UserStatus = 0;
                //        await App.Database.deleteUserAsync(l_user);
                //        //await App.Database.saveUserAsync(l_user);
                //    }
                //}
                if (!Common.bindMenu("change-password"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
                await PopupNavigation.Instance.PopAsync();
                //Navigation.PopPopupAsync();



            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnSignOut_onClicked(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PopPopupAsync();
                PopupNavigation.Instance.PopAsync();
                Common.mCommon.signOut();
                //Common.mCommon.REQ_AUTHORIZATION = new ERP.PL.SYS.REQ.REQ_AUTHORIZATION();
                //Common.mCommon.REQ_AUTHORIZATION.UserID = "guest";
                //Common.mCommon.REQ_AUTHORIZATION.UserPassword = "123";
                //Common.mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                //Common.mCommon.signIn(Common.mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrUserProfile_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("profile"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
                //Common.routeMenu("profile", "Profile");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrAddAccount_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PopPopupAsync();
                PopupNavigation.Instance.PopAsync();
                if (!Common.bindMenu("signin"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //Navigation.PopPopupAsync();
                //Common.routeMenu("signin", "Sign in");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void TgrSignoutAllAccount_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PopPopupAsync();
                PopupNavigation.Instance.PopAsync();
                Common.mCommon.signOutAll();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void ProfileTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("profile"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);


                //if (Common.bindMenu("profile"))
                //{
                //    Common.routeMenu(Route.Sys_Route.DicRouteList, Common.mCommon.SelectedMenu);
                //}
                //else
                //{
                //    //remove it after add in menu access for sign in and sign up
                //    Common.mCommon.SelectedMenu = new RES_MENU();
                //    Common.mCommon.SelectedMenu.MenuUrl = "profile";
                //    Common.mCommon.SelectedMenu.Text = "Profile";
                //    Common.mCommon.SelectedMenu.logoImg = "";
                //    //Common.routeMenu("signin", "Sign In");
                //    Common.routeMenu(Route.Sys_Route.DicRouteList, Common.mCommon.SelectedMenu);
                //}
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public void UserProfileTapped(object sender, EventArgs e)
        {
            try
            {
                // Dismiss the Menu.
                //Navigation.PopPopupAsync();
                PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            
        }

        

        private void TgrOtherUser_Tapped(object sender, EventArgs e)
        {
            try
            {
                //Navigation.PopPopupAsync();
                PopupNavigation.Instance.PopAsync();
                Common.mCommon.REQ_AUTHORIZATION.UserID= mDbUser[index].UserID;
                Common.mCommon.REQ_AUTHORIZATION.UserPassword = mDbUser[index].UserPassword;
                Common.mCommon.REQ_AUTHORIZATION.TransactionName = "1";
                Common.mCommon.signIn(Common.mCommon.REQ_AUTHORIZATION);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void simSignOut_Invoked(object sender, EventArgs e)
        {
            try
            { 
                if (index>=0)
                {
                    Common.mCommon.signOut(mDbUser[index]);
                    mDbUser.RemoveAt(index);
                    this.OnAppearing();
                    //this.sltOtherUser.ResetSwipe();


                    //// sltOtherUser.Remove
                    //if (itemIndex >= 0)
                    //    viewModel.InboxInfo.RemoveAt(itemIndex);
                    //this.listView.ResetSwipe();


                    //if (itemIndex >= 0)
                    //{
                    //    var item = viewModel.InboxInfo[itemIndex];
                    //    item.IsFavorite = !item.IsFavorite;
                    //}
                    //this.listView.ResetSwipe();

                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private void simSwitch_Invoked(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        void simSignOut_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        void SwipeView_SwipeEnded(System.Object sender, SwipeEndedEventArgs e)
        {
            //index =sender.
        }
        #endregion
    }
}