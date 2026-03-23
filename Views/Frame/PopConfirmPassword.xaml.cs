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
    public partial class PopConfirmPassword : PopupPage
    {
        private TaskCompletionSource<string?> _taskCompletionSource;

        public PopConfirmPassword()
        {
            try
            {
                InitializeComponent();
                BindingContext = new VmlNoti(this);
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

        public async void NotiTappedAsync(object sender, EventArgs e)
        {
            _taskCompletionSource?.TrySetResult(null);
            await PopupNavigation.Instance.PopAsync();

        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.TrySetResult(null);
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text?.Trim();

            if (!string.IsNullOrEmpty(email))
            {
                RES_MESSAGE? response = await Common.mCommon.verifyPassword(email);

                if (response != null && response.Code == "7")
                {
                    _taskCompletionSource?.TrySetResult(response.Code);

                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await PopupNavigation.Instance.PopAsync();
                    });
                }
            }           
        }
    }
}