using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;

namespace CS.ERP_MOB.Views.Frame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopDiscussion : PopupPage
    {
        public PopDiscussion()
        {
            try
            {
                InitializeComponent();
                //BindingContext = new VmlDiscussion(this);
                //double screenWidth = Application.Current.MainPage.Width;
                //int paddingRight = (int)(screenWidth / 5);
                //paddingRight = paddingRight / 2;
                //DiscussionGrid.Padding = new Thickness(0, 0, paddingRight, 50);
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
        private async void DiscussionList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_DISCUSSION = (DAT_DISCUSSION_NOTI)DiscussionList.SelectedItem;
                if (l_RES_DISCUSSION != null)
                {
                    l_RES_DISCUSSION.DiscussionStatusAsk = "1";//1=send, 0=open
                    //OpenExternalApp(item?.AppName);
                    Common.mCommon.OpenExternalApp(l_RES_DISCUSSION.DiscussionNotiURL);
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