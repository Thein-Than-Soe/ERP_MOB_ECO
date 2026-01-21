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
using CS.ERP_MOB.ViewsModel.Frame;
using static CS.ERP_MOB.General.Utility;
using RGPopup.Maui.Services;
using System.Threading.Tasks;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmSignIn : ContentView
    {
        #region "Declaration"
        VmlSignIn mVmlSignIn;
        SignInState mSignInState = SignInState.SignIn;
        //Account account;
        //AccountStore store;
        REQ_AUTHORIZATION mREQ_AUTHORIZATION = new REQ_AUTHORIZATION();
        DbUser mDbUser = new DbUser();
        REQ_RESET_PWD mREQ_RESET_PWD = new REQ_RESET_PWD();
        JSN_REQ_USER_RESETPWD mJSN_REQ_USER_RESETPWD = new JSN_REQ_USER_RESETPWD();
        JSN_USER mJSN_USER = new JSN_USER();
        JSN_RESET_PWD mJSN_RESET_PWD = new JSN_RESET_PWD();
        #endregion
        #region "Constructor"
        public FrmSignIn()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSignIn = new VmlSignIn();
                mVmlSignIn.IsSignInPage = true;

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
        private void showActivation()
        {
            try
            {
                switchSignInState(SignInState.Activate);
                entActivationCode.Text = mJSN_USER.RES_USER_LST.First().OTPCode_0_50;
                entActivationCode.Focus();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void showSignIn()
        {
            try
            {
                switchSignInState(SignInState.SignIn);
                entUserName.Focus();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private bool bindActivationData()
        {
            try
            {
                bool flag = true;
                //Name
                if (!string.IsNullOrEmpty(entUserName.Text))
                {
                    mJSN_RESET_PWD = new JSN_RESET_PWD();
                    mJSN_RESET_PWD.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                    REQ_RESET_PWD l_REQ_RESET_PWD = new REQ_RESET_PWD();
                    l_REQ_RESET_PWD.UserID = entUserName.Text;
                    l_REQ_RESET_PWD.LoginURL = AppInfo.Current.Name;
                    mJSN_RESET_PWD.REQ_RESET_PWD = l_REQ_RESET_PWD;
                }
                else
                {
                    flag = false;
                    entUserName.Focus();
                }
                return flag;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void resetPassword()
        {
            string l_Request = "";
            var l_Response = "";
            try
            {
                l_Request = JsonConvert.SerializeObject(mJSN_RESET_PWD);
                Utility.openLoader();
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsResetPwd);
                if (l_Response != null)
                {
                    mJSN_USER = JsonConvert.DeserializeObject<JSN_USER>(l_Response);
                    if (mJSN_USER.Message.Code == "7")
                    {
                       showActivation();
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(this.mJSN_USER?.Message?.Message);
                    }
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }

        }
        private void switchSignInState(SignInState argSignInState)
        {
            try
            {
                mVmlSignIn.IsSignInPage = false;
                mVmlSignIn.IsActivatePage = false;
                mSignInState = argSignInState;
                switch (mSignInState)
                {
                    case SignInState.SignIn:
                        mVmlSignIn.IsSignInPage = true;
                        break;
                    case SignInState.Activate:
                        mVmlSignIn.IsActivatePage = true;
                        break;
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


        private void TgrForgotPassword_Tapped(object sender, TappedEventArgs e)
        {
            try
            {
                if (bindActivationData())
                {
                    resetPassword();
                }
                else
                {
                    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgUIDRequired"));
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnResend_onClicked(object sender, EventArgs e)
        {
            try
            {
                if (bindActivationData())
                {
                    resetPassword();
                }
                else
                {
                    WeakReferenceMessenger.Default.Send("Server Err");
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void btnBack_onClicked(object sender, EventArgs e)
        {
            showSignIn();
        }
        private async void btnOk_onClicked(object sender, EventArgs e)
        {
            string verificationCode = entActivationCode.Text?.Trim();

            if (!string.IsNullOrEmpty(verificationCode))
            {
                RES_MESSAGE? response = await Common.mCommon.verifyEmailOTP(verificationCode);

                if (response != null && response.Code == "7")
                {

                    if (!Common.bindMenu("change-password"))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Change Password", MenuUrl = "change-password", logoImg = "" };
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);
                }
            }
        }
        #endregion

    }
}