using CS.ERP_MOB.General;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms.Xaml;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopLiveChat : PopupPage
    {
        public PopLiveChat()
        {
            InitializeComponent();
        }

        private async void lblClose_Tapped(object sender, EventArgs e)
        {
            //IntercomService.Current.LiveChatSwitch = false;
            Common.mCommon.LiveChat = false;
            await PopupNavigation.Instance.PopAllAsync();
        }

        private async void lblMinized__Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}