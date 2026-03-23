using CS.ERP.PL.NTF.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.NTF;
using CS.ERP_MOB.ViewsModel.Frame;
using Newtonsoft.Json;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopConfirmEmailOTP : PopupPage
    {
        private TaskCompletionSource<string?> _taskCompletionSource;

        public PopConfirmEmailOTP()
        {
            try
            {
                InitializeComponent();
                BindingContext = new VmlNoti(this);
                getEmailOTP();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public Task<string?> ShowAsync()
        {
            _taskCompletionSource = new TaskCompletionSource<string?>();
            return _taskCompletionSource.Task;
        }

        private void SwipeItemView_Invoked(object sender, EventArgs e)
        {
            try
            {
                SwipeItemView item = sender as SwipeItemView;
                RES_NOTI_LST l_RES_NOTI_LST = item.BindingContext as RES_NOTI_LST;

                // var l_RES_NOTI = (RES_NOTI_LST)NotiList.SelectedItem;
                if (l_RES_NOTI_LST != null)
                {
                    //Call
                    Common.mCommon.updateNoti(l_RES_NOTI_LST);
                    //await PopupNavigation.Instance.PopAllAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void OnResendClicked(object sender, EventArgs e)
        {
            getEmailOTP();
        }
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.TrySetResult(null);
            await PopupNavigation.Instance.PopAsync();
        }
        private async void getEmailOTP()
        {
            RES_MESSAGE? response = await Common.mCommon.getEmailOTP();

            if (response != null && response.Code == "7")
            {
                string result = "";
                var parts = response.Message.Split(',');

                if (parts.Length > 1)
                {
                    result = parts[1];
                }
                lblPrefix.Text = result;
            }
        }
        private async void OnOkClicked(object sender, EventArgs e)
        {
            string verificationCode = lblPrefix.Text + EntCode.Text?.Trim();

            if (!string.IsNullOrEmpty(verificationCode))
            {
                RES_MESSAGE? response = await Common.mCommon.verifyEmailOTP(verificationCode);

                if (response != null && response.Code == "7")
                {
                    _taskCompletionSource?.TrySetResult(verificationCode);
                    await PopupNavigation.Instance.PopAsync();
                }
            }           
        }
    }
}