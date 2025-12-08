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
    public partial class PopNoti : PopupPage
    {
        public PopNoti()
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

        protected override void OnAppearing()
        {
            base.OnAppearing();

            double deviceHeight = DeviceDisplay.MainDisplayInfo.Height / DeviceDisplay.MainDisplayInfo.Density;

            PopupFrame.HeightRequest = deviceHeight * 2 / 3;
        }
        //private async void NotiList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    try
        //    {
        //        var l_RES_NOTI = (RES_NOTI_LST)NotiList.SelectedItem;
        //        if (l_RES_NOTI != null)
        //        {
        //            l_RES_NOTI.StatusAsk = "1";//1=send, 0=open
        //            Common.mCommon.NotiList.Remove(l_RES_NOTI);
        //            //Common.mCommon.NotiList = Clone(Common.mCommon.NotiList);
        //            Common.mCommon.updateNoti(l_RES_NOTI);
        //            if (Common.bindMenu(l_RES_NOTI.SystemNotiURL))
        //            {
        //                if (!Common.bindMenu(l_RES_NOTI.SystemNotiURL))
        //                {
        //                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "FrmAdmin", MenuUrl = "FrmAdmin", logoImg = "" };
        //                    MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", ApplicationMessage.Message.MenuAccessRight);
        //                }
        //                Common.routeMenu(Common.mCommon.SelectedMenu);

        //                //Common.routeMenu(l_RES_NOTI.SystemNotiURL, l_RES_NOTI.MenuName);
        //                //Common.routeMenu(l_RES_NOTI.SystemNotiURL, l_RES_NOTI.MenuName, l_RES_NOTI.LinkAsk1);
        //            }
        //            else
        //            {
        //                MessagingCenter.Send<Application, string>(Application.Current, "ToastMessage", "no access right");
        //            }
        //            await PopupNavigation.Instance.PopAllAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}

        //private List<RES_NOTI_LST> Clone(object notiList)
        //{
        //    throw new NotImplementedException();
        //}

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
        public void NotiTapped(object sender, EventArgs e)
        {
After:
        public async void NotiTappedAsync(object sender, EventArgs e)
        {
*/
        public async void NotiTappedAsync(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
           
        }
        //private void SwipeItem_Invoked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var l_RES_NOTI = (RES_NOTI_LST)NotiList.SelectedItem;
        //        if (l_RES_NOTI != null)
        //        {
        //            //Call
        //            Common.mCommon.updateNoti(l_RES_NOTI);
        //            //await PopupNavigation.Instance.PopAllAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }

        //}
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

        private async void OnSwipeDown(object sender, SwipedEventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

      

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                var selectedItem = e.CurrentSelection.FirstOrDefault();
                if (selectedItem == null)
                    return;

              
                RES_NOTI_LST l_RES_NOTI_LST = selectedItem as RES_NOTI_LST;
                

              
                DisplayAlert("Selected", l_RES_NOTI_LST.SystemNotiName_0_255, "OK");

                
                ((CollectionView)sender).SelectedItem = null;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}