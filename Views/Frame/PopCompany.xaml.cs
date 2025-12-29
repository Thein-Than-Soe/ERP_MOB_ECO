using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopCompany : PopupPage
    {
        public PopCompany()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            double deviceHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            PopupFrame.HeightRequest = deviceHeight * 2 / 3;
        }
        private async void CompanyUserList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_COMPANY_USER = (RES_COMPANY_USER)CompanyUserList.SelectedItem;
                if (l_RES_COMPANY_USER != null)
                {
                    //OpenExternalApp(item?.AppName);
                    Common.mCommon.updateDefaultCompanyUser(l_RES_COMPANY_USER);
                    //Common.mCommon.DiscussionList.Remove(l_RES_DISCUSSION);
                    //Common.mCommon.DiscussionList = Clone(Common.mCommon.DiscussionList);
                    //Common.mCommon.updateDiscussion(l_RES_DISCUSSION);

                    //if (!Common.bindMenu(l_RES_DISCUSSION.UserProfileURL))
                    //{
                    //    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
                    //}
                    //Common.routeMenu(Common.mCommon.SelectedMenu);
                    //await PopupNavigation.Instance.PopAllAsync();

                    //if (Common.bindMenu(l_RES_DISCUSSION.Link))
                    //{
                    //    Common.routeMenu(l_RES_DISCUSSION.Link, l_RES_DISCUSSION.DiscussionDataType);
                    //}
                    //else
                    //{
                    //    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
                    //}
                    //await PopupNavigation.Instance.PopAllAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        List<T> Clone<T>(IEnumerable<T> oldList)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return new List<T>(oldList);
        }

        /* Unmerged change from project 'CS.ERP_MOB (net8.0-ios)'
        Before:
                public void DiscussionTapped(object sender, EventArgs e)
                {
        After:
                public void DiscussionTappedAsync(object sender, EventArgs e)
                {
        */
        public async void DiscussionTappedAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();

        }
        private async void OnSwipeDown(object sender, SwipedEventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

    }
}