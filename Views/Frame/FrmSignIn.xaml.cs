using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.DB;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
//using ERP_MOB.AppConstant;
//using ERP_MOB.AuthHelpers;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
//using Xamarin.Auth;
using Microsoft.Maui.Authentication;
using Microsoft.Maui.Storage;
using CommunityToolkit.Mvvm.Messaging;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSignIn : ContentView
    {
        #region "Declaration"
        //Account account;
        //AccountStore store;
        REQ_AUTHORIZATION mREQ_AUTHORIZATION = new REQ_AUTHORIZATION();
        DbUser mDbUser = new DbUser();
        #endregion
        #region "Constructor"
        public FrmSignIn()
        {
            try
            {
                InitializeComponent();
                //store = AccountStore.Create();
                mREQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                if (Common.mCommon.REQ_AUTHORIZATION.UserID.ToLower() != "guest" )
                {
                    showSignInInfo();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Property"
        #endregion
        #region "Private Method"
        private bool bindAuthorization()
        {
            bool flag = true;
            try
            {
                //Name
                if (!string.IsNullOrWhiteSpace(entUserName.Text))
                {
                    mREQ_AUTHORIZATION.UserID = entUserName.Text.Trim();
                }
                else
                {
                    flag = false;
                    entUserName.Focus();
                }
                //Password
                if (!string.IsNullOrWhiteSpace(entPassword.Text))
                {
                    mREQ_AUTHORIZATION.UserPassword = entPassword.Text.Trim();
                }
                else
                {
                    flag = false;
                    entPassword.Focus();
                }
                mREQ_AUTHORIZATION.TransactionName = "1";
                return flag;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void showSignInInfo()
        {
            try
            {
                //Name
                if (mREQ_AUTHORIZATION!=null)
                {
                    entUserName.Text= mREQ_AUTHORIZATION.UserID;
                    entPassword.Text =mREQ_AUTHORIZATION.UserPassword;
                }
                else
                {
                    entUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        #endregion
        #region "Property"
        #endregion
        #region "Public Method"
        #endregion
        #region "Event"
        private void entUserName_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
        {
            try
            {
                if (Common.mCommon.mDbUser_LST.Count > 0)
                {
                    foreach (DbUser l_DbUser in Common.mCommon.mDbUser_LST)
                    {
                        if (  (entUserName.Text.Trim() == l_DbUser.UserID
                            || entUserName.Text.Trim() == l_DbUser.UserEmail
                            || entUserName.Text.Trim() == l_DbUser.UserPhone)
                            && entUserName.Text.Trim().ToLower()!="guest")
                        {
                            entPassword.Text =l_DbUser.UserPassword;
                            entPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnSignUp_onClicked(object sender, EventArgs e)
        {
            try
            {
                //IntercomService.RouteMenu("signup", "Sign up");
                if (!Common.bindMenu("signup"))
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                    return;
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);

                //Common.mCommon.SelectedMenu = new RES_MENU();
                //Common.mCommon.SelectedMenu.MenuUrl = "signup";
                //Common.mCommon.SelectedMenu.Text ="Sign Up";
                //Common.mCommon.SelectedMenu.logoImg = "";
                ////Common.routeMenu("signin", "Sign In");
                //Common.routeMenu(Route.Sys_Route.DicRouteList, Common.mCommon.SelectedMenu);
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnSignIn_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindAuthorization())
                {
                    Common.mCommon.signIn(mREQ_AUTHORIZATION);
                    //if (chbRememberPassword.IsChecked && Common.mCommon.UserLoggedIn)
                    //{
                    //    Common.saveDbUser();
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnSignInFb_onClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void btnSignInGoogle_onClick(object sender, EventArgs e)
        {
            try
            {
                //string clientId = null;
                //string redirectUri = null;

                //switch (Device.RuntimePlatform)
                //{
                //    case Device.iOS:
                //        clientId = Constants.iOSClientId;
                //        redirectUri = Constants.iOSRedirectUrl;
                //        break;

                //    case Device.Android:
                //        clientId = Constants.AndroidClientId;
                //        redirectUri = Constants.AndroidRedirectUrl;
                //        break;
                //}

                //account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

                //var authenticator = new OAuth2Authenticator(
                //    clientId,
                //    null,
                //    Constants.Scope,
                //    new Uri(Constants.AuthorizeUrl),
                //    new Uri(redirectUri),
                //    new Uri(Constants.AccessTokenUrl),
                //    null,
                //    true);

                //authenticator.Completed += OnAuthCompleted;
                //authenticator.Error += OnAuthError;

                //AuthenticationState.Authenticator = authenticator;

                //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                //presenter.Login(authenticator); string clientId = null;
                //string redirectUri = null;

                //switch (Device.RuntimePlatform)
                //{
                //    case Device.iOS:
                //        clientId = Constants.iOSClientId;
                //        redirectUri = Constants.iOSRedirectUrl;
                //        break;

                //    case Device.Android:
                //        clientId = Constants.AndroidClientId;
                //        redirectUri = Constants.AndroidRedirectUrl;
                //        break;
                //}

                //account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
                //var authenticator = new OAuth2Authenticator(
                //    clientId,
                //    null,
                //    Constants.Scope,
                //    new Uri(Constants.AuthorizeUrl),
                //    new Uri(redirectUri),
                //    new Uri(Constants.AccessTokenUrl),
                //    null,
                //    true);

                //authenticator.Completed += OnAuthCompleted;
                //authenticator.Error += OnAuthError;

                //AuthenticationState.Authenticator = authenticator;

                //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                //presenter.Login(authenticator); string clientId = null;
                //string redirectUri = null;

                //switch (Device.RuntimePlatform)
                //{
                //    case Device.iOS:
                //        clientId = Constants.iOSClientId;
                //        redirectUri = Constants.iOSRedirectUrl;
                //        break;

                //    case Device.Android:
                //        clientId = Constants.AndroidClientId;
                //        redirectUri = Constants.AndroidRedirectUrl;
                //        break;
                //}

                //account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();

                //var authenticator = new OAuth2Authenticator(
                //    clientId,
                //    null,
                //    Constants.Scope,
                //    new Uri(Constants.AuthorizeUrl),
                //    new Uri(redirectUri),
                //    new Uri(Constants.AccessTokenUrl),
                //    null,
                //    true);

                //authenticator.Completed += OnAuthCompleted;
                //authenticator.Error += OnAuthError;

                //AuthenticationState.Authenticator = authenticator;

                //var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                //presenter.Login(authenticator);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }
        //async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        //{
        //    try
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= OnAuthCompleted;
        //            authenticator.Error -= OnAuthError;
        //        }

        //        User user = null;
        //        if (e.IsAuthenticated)
        //        {
        //            // If the user is authenticated, request their basic user data from Google
        //            // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
        //            var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
        //            var response = await request.GetResponseAsync();
        //            if (response != null)
        //            {
        //                // Deserialize the data and store it in the account store
        //                // The users email address will be used to identify data in SimpleDB
        //                string userJson = await response.GetResponseTextAsync();
        //                user = JsonConvert.DeserializeObject<User>(userJson);
        //            }

        //            if (user != null)
        //            {
        //                //App.Current.MainPage = new NavigationPage(new MainPage());
        //                Console.WriteLine("success");

        //            }

        //            //await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
        //            //await DisplayAlert("Email address", user.Email, "OK");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
           
        //}
        //void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        //{
        //    try
        //    {
        //        var authenticator = sender as OAuth2Authenticator;
        //        if (authenticator != null)
        //        {
        //            authenticator.Completed -= OnAuthCompleted;
        //            authenticator.Error -= OnAuthError;
        //        }

        //        Debug.WriteLine("Authentication error: " + e.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
            
        //}

        
        #endregion

    }
}