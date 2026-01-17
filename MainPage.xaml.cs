using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.ECO.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP.PL.SYS.REQ;
using CS.ERP.PL.SYS.RES;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using System.Diagnostics;


namespace CS.ERP_MOB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private bool _longPressTriggered = false;
        private bool isExpanded = true;
        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void TgrSignIn_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("signin"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void TgrSignUp_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (!Common.bindMenu("signup"))
                {
                    Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign Up", MenuUrl = "signup", logoImg = "" };
                }
                Common.routeMenu(Common.mCommon.SelectedMenu);
               
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void TgrCheckout_Tapped(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private async void FloatRight_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (_longPressTriggered)
                {
                    _longPressTriggered = false;
                    return; 
                }
                var frame = sender as Frame;
                if (frame == null) return;

                var tappedItem = frame.BindingContext as DAT_FLOAT;
                if (tappedItem == null) return;

                if (tappedItem.FloatDescription_0_500 == "openChat")
                {
                    await Common.mCommon.OpenExternalApp("", tappedItem.DefaultURL, tappedItem.UserURL);
                }
                else if (tappedItem.FloatDescription_0_500 == "scrollToTop")
                {
                    ScrollToTop();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void FloatLeft_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (_longPressTriggered)
                {
                    _longPressTriggered = false;
                    return;
                }
                var frame = sender as Frame;
                if (frame == null) return;

                var tappedItem = frame.BindingContext as DAT_FLOAT;
                if (tappedItem == null) return;
                switch (tappedItem.FloatTypeAsk)
                {
                    case "1"://1 for meta data
                        await Share.Default.RequestAsync(new ShareTextRequest
                        {
                            Uri = tappedItem.DefaultURL
                        });
                        break;
                    case "2"://2 for same app

                        if (Common.bindMenu(tappedItem.DefaultURL))
                        {
                            Common.routeMenu(Common.mCommon.SelectedMenu);
                        }
                        else
                        {
                            WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                        }
                         break;
                    case "3"://3 for different app
                        Common.mCommon.OpenExternalApp("", tappedItem.DefaultURL, tappedItem.UserURL);
                        break;
                    default:break;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void ToggleShareList()
        {
            ShareList.IsVisible = !ShareList.IsVisible;

            // Optional animation for smooth effect
            if (ShareList.IsVisible)
                ShareList.FadeTo(1, 200, Easing.CubicIn);
            else
                ShareList.FadeTo(0, 150, Easing.CubicOut);
        }

        private async void BtnMainShare_Tapped(object sender, EventArgs e)
        {
            if (!isExpanded)
            {
                // Make visible first
                ShareList.IsVisible = true;

                // Start slightly below (hidden behind button)
                ShareList.TranslationY = 20;
                ShareList.Opacity = 0;

                // Animate upward
                await Task.WhenAll(
                    ShareList.TranslateTo(0, -10, 250, Easing.CubicOut),
                    ShareList.FadeTo(1, 250, Easing.CubicIn)
                );
            }
            else
            {
                // Animate downward (hide)
                await Task.WhenAll(
                    ShareList.TranslateTo(0, 20, 200, Easing.CubicIn),
                    ShareList.FadeTo(0, 200)
                );

                ShareList.IsVisible = false;
            }

            isExpanded = !isExpanded;
        }
       
        private void ScrollToTop()
        {
            if (CView.Content is View content)
            {
                var scroll = FindScrollView(content);

                scroll?.ScrollToAsync(0, 0, true);
            }
        }

        private ScrollView FindScrollView(View view)
        {
            if (view is ScrollView sv)
                return sv;

            if (view is ContentView layout)
            {
                foreach (var child in layout.Children)
                {
                    var result = FindScrollView((View)child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
        
        private void FloatLeft_LongPressCompleted(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                _longPressTriggered = true;
                for (int i = 0; i < Common.mCommon.FloatLeftList.Count; i++)
                {
                    DAT_FLOAT item = Common.mCommon.FloatLeftList[i];
                    item.CostCenterCode_0_50 = "1";
                    Common.mCommon.FloatLeftList.RemoveAt(i);
                    Common.mCommon.FloatLeftList.Insert(i, item);
                }
            }
        }
        // Long-press handler
        private void FloatRight_LongPressCompleted(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                _longPressTriggered = true;
                for (int i = 0; i < Common.mCommon.FloatRightList.Count; i++)
                {
                    DAT_FLOAT item = Common.mCommon.FloatRightList[i];
                    item.CostCenterCode_0_50 = "1";
                    Common.mCommon.FloatRightList.RemoveAt(i);
                    Common.mCommon.FloatRightList.Insert(i, item);
                }
            }
        }
        private void Page_Tapped(object sender, EventArgs e)
        {
            ExitDeleteMode();
        }
        private void ExitDeleteMode()
        {        
            for (int i = 0; i < Common.mCommon.FloatLeftList.Count; i++)
            {
                DAT_FLOAT item = Common.mCommon.FloatLeftList[i];
                item.CostCenterCode_0_50 = "0";
                Common.mCommon.FloatLeftList.RemoveAt(i);
                Common.mCommon.FloatLeftList.Insert(i, item);
            }
            for (int i = 0; i < Common.mCommon.FloatRightList.Count; i++)
            {
                DAT_FLOAT item = Common.mCommon.FloatRightList[i];
                item.CostCenterCode_0_50 = "0";
                Common.mCommon.FloatRightList.RemoveAt(i);
                Common.mCommon.FloatRightList.Insert(i, item);
            }
        }

        private void CloseButton_FloatRight(object sender, EventArgs e)
        {
            if (sender is Label label && label.BindingContext is DAT_FLOAT item)
            {
                Common.mCommon.FloatRightList.Remove(item); // hide/delete item
            }
        }
        private void CloseButton_FloatLeft(object sender, TappedEventArgs e)
        {
            if (sender is Label label && label.BindingContext is DAT_FLOAT item)
            {
                Common.mCommon.FloatLeftList.Remove(item);
            }
        }
    }
}