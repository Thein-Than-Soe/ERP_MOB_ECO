using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Extensions;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopSetting : PopupPage
    {
        public PopSetting()
        {
            InitializeComponent();
        }

        public void SettingTapped(object sender, EventArgs e)
        {
            if (!Common.bindMenu("setting"))
            {
                Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
            }
            Common.routeMenu(Common.mCommon.SelectedMenu);
            Navigation.PopPopupAsync();

            //if (Common.bindMenu("setting"))
            //{
            //    Common.routeMenu("setting", "Settings");
            //}
            //else
            //{
            //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
            //}
            //Navigation.PopPopupAsync();
        }
    }
}