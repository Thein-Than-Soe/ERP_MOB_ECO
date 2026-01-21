using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;


namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentView
    {
        RES_USER_LST mRES_USER_LST = new RES_USER_LST();
        JSN_REQ_USER_LST mJSN_REQ_USER_LST = new JSN_REQ_USER_LST();
        public ChangePasswordPage()
        {
            InitializeComponent();
        }
        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            string oldPassword = entOldPassword.Text;
            string newPassword = entNewPassword.Text;
            string confirmPassword = entConfirmPassword.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(oldPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Please fill all fields.", "OK");
                return;
            }

            if (newPassword != confirmPassword)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "New password and confirm password do not match.", "OK");
                return;
            }

            // TODO: Call API / service to change password
            // Example:
            mRES_USER_LST = new RES_USER_LST();
            mRES_USER_LST.Ask = Common.mCommon.User.UserAsk;
            mRES_USER_LST.UserPassword = newPassword;
            mJSN_REQ_USER_LST = new JSN_REQ_USER_LST();
            mJSN_REQ_USER_LST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
            mJSN_REQ_USER_LST.RES_USER_LST.Add(mRES_USER_LST);
            await changePassword(mJSN_REQ_USER_LST);


            // Optional: clear fields
            //entOldPassword.Text = string.Empty;
            //entNewPassword.Text = string.Empty;
            //entConfirmPassword.Text = string.Empty;
        }
        private void OnReloginClicked(object sender, EventArgs e)
        {
            try
            {
                Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                Common.routeMenu(Common.mCommon.SelectedMenu);
            }
            catch (Exception)
            {

            }
        }

        public async Task changePassword(JSN_REQ_USER_LST argJSN_REQ_USER_LST)
        {
            string l_Request = "";
            var l_Response = "";
            RES_MESSAGE mRES_MESSAGE = new RES_MESSAGE();
            try
            {
                Utility.openLoader();
                l_Request = JsonConvert.SerializeObject(argJSN_REQ_USER_LST);
                l_Response = await Sys_Service.ApiCall(l_Request, Sys_Name.wsSavePassword);
                if (l_Response != null || l_Response != "")
                {
                    mRES_MESSAGE = JsonConvert.DeserializeObject<RES_MESSAGE>(l_Response);
                    if (mRES_MESSAGE?.Code == "7")
                    {
                        entOldPassword.Text = string.Empty;
                        entNewPassword.Text = string.Empty;
                        entConfirmPassword.Text = string.Empty;
                        WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgPasswdChanged"));
                    }
                    else
                    {
                        WeakReferenceMessenger.Default.Send(mRES_MESSAGE?.Message);
                    }
                }
                else
                {
                   WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("ErrWebService"));
                }
                Utility.closeLoader();
            }
            catch (Exception ex)
            {
                Utility.closeLoader();
                throw ex.InnerException;
            }
        }
    }
}