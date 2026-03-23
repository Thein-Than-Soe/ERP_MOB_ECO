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
    public partial class PopConfirmYesNo : PopupPage
    {
        private TaskCompletionSource<string?> _taskCompletionSource;

        public PopConfirmYesNo()
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
        public PopConfirmYesNo(string argMessage)
        {
            try
            {
                InitializeComponent();
                BindingContext = new VmlNoti(this);
                lblMessage.Text = argMessage;
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


        private async void OnCancelClicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.TrySetResult(null);
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnOkClicked(object sender, EventArgs e)
        {
            _taskCompletionSource?.TrySetResult("7");
            await PopupNavigation.Instance.PopAsync();
        }
    }
}