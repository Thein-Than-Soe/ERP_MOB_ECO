using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System.Collections.ObjectModel;
using CS.ERP.PL.SYS.DAT;
using RGPopup.Maui.Services;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.ViewsModel.ECO;
using CS.ERP.PL.ECO.DAT;

namespace CS.ERP_MOB.Views.ECO
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmWishlistLst : ContentView
    {
        #region "Declaring"
        VmlWishlist mVmlWishlist { get; set; }
        #endregion
        #region "Constructor"
        public FrmWishlistLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlWishlist = new VmlWishlist();
                mVmlWishlist.mJSN_REQ_WISHLIST.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST = new DAT_WISHLIST();
                //mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST.SD = Utility.getTLFormLoadSD();
                //mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST.ED = Utility.getTLFormLoadED();
                mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST_DETAIL.Add(new DAT_WISHLIST_DETAIL());
                mVmlWishlist.getWishlist();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        #endregion

        #region "Private Mehtod"
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (mVmlWishlist != null)
            {
                int newColumns = width switch
                {
                    < 400 => 2,
                    < 600 => 2,
                    < 800 => 4,
                    < 1000 => 5,
                    < 1200 => 6,
                    < 1400 => 7,
                    < 1600 => 8,
                    < 1800 => 9,
                    _ => 10
                };

                if (collectionView.ItemsLayout is not GridItemsLayout currentLayout ||
                    currentLayout.Span != newColumns)
                {
                    var layout = new GridItemsLayout(newColumns, ItemsLayoutOrientation.Vertical)
                    {
                        VerticalItemSpacing = 10,
                        HorizontalItemSpacing = 10
                    };
                    collectionView.ItemsLayout = layout;
                }
            }
        }
        private void sortSalesInvoiceList(string sortBy)
        {
            if (mVmlWishlist.WishlistLst == null || !mVmlWishlist.WishlistLst.Any())
                return;

            IEnumerable<DAT_WISHLIST> sorted;

            switch (sortBy)
            {
                case "WishlistDate":
                    sorted = mVmlWishlist.IsAscending
                        ? mVmlWishlist.WishlistLst.OrderBy(x => x.WishlistDate)
                        : mVmlWishlist.WishlistLst.OrderByDescending(x => x.WishlistDate);
                    break;

                case "WishlistCode_0_50":
                    sorted = mVmlWishlist.IsAscending
                        ? mVmlWishlist.WishlistLst.OrderBy(x => x.WishlistCode_0_50)
                        : mVmlWishlist.WishlistLst.OrderByDescending(x => x.WishlistCode_0_50);
                    break;

                case "SalePersonName_0_255":
                    sorted = mVmlWishlist.IsAscending
                        ? mVmlWishlist.WishlistLst.OrderBy(x => x.SalePersonName_0_255)
                        : mVmlWishlist.WishlistLst.OrderByDescending(x => x.SalePersonName_0_255);
                    break;

                case "StatusName_0_255":
                    sorted = mVmlWishlist.IsAscending
                        ? mVmlWishlist.WishlistLst.OrderBy(x => x.StatusName_0_255)
                        : mVmlWishlist.WishlistLst.OrderByDescending(x => x.StatusName_0_255);
                    break;

                case "GrandTotal":
                    sorted = mVmlWishlist.IsAscending
                        ? mVmlWishlist.WishlistLst.OrderBy(x => x.GrandTotal)
                        : mVmlWishlist.WishlistLst.OrderByDescending(x => x.GrandTotal);
                    break;

                default:
                    return;
            }

            mVmlWishlist.WishlistLst = new ObservableCollection<DAT_WISHLIST>(sorted);
            if (mVmlWishlist.IsCardView)
            {
                collectionView.ItemsSource = mVmlWishlist.WishlistLst;
            }
            else if (mVmlWishlist.IsListView)
            {
                lstView.ItemsSource = mVmlWishlist.WishlistLst;
            }
            else
            {
                MyGrid.ItemsSource = mVmlWishlist.WishlistLst;
            }
        }
        private void getCheckedData(List<DAT_WISHLIST> l_DAT_WISHLIST_LST)
        {
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
        }
        #endregion

        #region "Task"
        private async Task btnNew_onClick(RES_CONTROL argRES_CONTROL)
        {
            //string result = "";
            //Common.mCommon.getConfirmation(argRES_CONTROL);
            //if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            //{
            //    result = "7";
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            //{
            //    var popup = new PopConfirmYesNo();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            //{
            //    var popup = new PopConfirmEmailOTP();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            //{
            //    var popup = new PopConfirmSMSOTP();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            //{
            //    var popup = new PopConfirmPassword();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            //{
            //    var popup = new PopConfirmSignature();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //if (result != null && result == "7")
            //{
            //    Common.mCommon.saveNoti(argRES_CONTROL);
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet());
            //}
        }
        private async Task btnEdit_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            //string result = "";
            //Common.mCommon.getConfirmation(argRES_CONTROL);
            //if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            //{
            //    result = "7";
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            //{
            //    var popup = new PopConfirmYesNo();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            //{
            //    var popup = new PopConfirmEmailOTP();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            //{
            //    var popup = new PopConfirmSMSOTP();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            //{
            //    var popup = new PopConfirmPassword();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            //{
            //    var popup = new PopConfirmSignature();
            //    await PopupNavigation.Instance.PushAsync(popup);
            //    result = await popup.ShowAsync();
            //}
            //if (result != null && result == "7")
            //{
            //    DAT_WISHLIST l_DAT_WISHLIST = (DAT_WISHLIST)tappedItem;
            //    Common.mCommon.saveNoti(argRES_CONTROL, l_DAT_WISHLIST.Ask);
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(l_DAT_WISHLIST));
            //}
        }
        private async Task btnDelete_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            DAT_WISHLIST l_DAT_WISHLIST = (DAT_WISHLIST)tappedItem;
            if (l_DAT_WISHLIST.StatusAsk != "9" && l_DAT_WISHLIST.PostingStatusAsk != "1")
            {
                string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete ") + l_DAT_WISHLIST.WishlistCode_0_50 + "?";

                string result = "";
                Common.mCommon.getConfirmation(argRES_CONTROL);
                if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
                {
                    result = "7";
                }
                else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
                {
                    var popup = new PopConfirmYesNo();
                    await PopupNavigation.Instance.PushAsync(popup);
                    result = await popup.ShowAsync();
                }
                else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
                {
                    var popup = new PopConfirmEmailOTP();
                    await PopupNavigation.Instance.PushAsync(popup);
                    result = await popup.ShowAsync();
                }
                else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
                {
                    var popup = new PopConfirmSMSOTP();
                    await PopupNavigation.Instance.PushAsync(popup);
                    result = await popup.ShowAsync();
                }
                else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
                {
                    var popup = new PopConfirmPassword();
                    await PopupNavigation.Instance.PushAsync(popup);
                    result = await popup.ShowAsync();
                }
                else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
                {
                    var popup = new PopConfirmSignature();
                    await PopupNavigation.Instance.PushAsync(popup);
                    result = await popup.ShowAsync();
                }
                if (result != null && result == "7")
                {
                    mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST = l_DAT_WISHLIST;
                    mVmlWishlist.mJSN_REQ_WISHLIST.DAT_WISHLIST.StatusAsk = "6";
                    mVmlWishlist.saveInvoice();
                    Common.mCommon.saveNoti(argRES_CONTROL, l_DAT_WISHLIST.Ask);
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgDelete"));
            }
        }
        private async Task btnPrint_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            getCheckedData(l_DAT_WISHLIST_LST);
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Print");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }

            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        private async Task btnSendMail_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }

            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        
        }
        private async Task btnExpPDF_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }

            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        private async Task btnExpExcel_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }
            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        private async Task btnExpCSV_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }
            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        private async Task btnPost_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Post");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }

            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        private async Task btnSummary_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<DAT_WISHLIST> l_DAT_WISHLIST_LST = new List<DAT_WISHLIST>();
            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                if (mVmlWishlist.WishlistLst[i].IsChecked == "1")
                {
                    l_DAT_WISHLIST_LST.Add(mVmlWishlist.WishlistLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (DAT_WISHLIST item in l_DAT_WISHLIST_LST)
            {
                messageInfo += item.WishlistCode_0_50 + ",";
                notiInfo += item.Ask + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }
            string result = "";
            Common.mCommon.getConfirmation(argRES_CONTROL);
            if (Common.mCommon.ConfirmationUserJun.ConfirmationStatus == "0")
            {
                result = "7";
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "1")//1 for Yes/No
            {
                var popup = new PopConfirmYesNo();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "2")//2 for EmailOTP
            {
                var popup = new PopConfirmEmailOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "3")//3 for SMSOTP
            {
                var popup = new PopConfirmSMSOTP();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "4")//4 for Password
            {
                var popup = new PopConfirmPassword();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            else if (Common.mCommon.ConfirmationUserJun.ConfirmationTypeAsk == "5")//5 for Signature
            {
                var popup = new PopConfirmSignature();
                await PopupNavigation.Instance.PushAsync(popup);
                result = await popup.ShowAsync();
            }
            if (result != null && result == "7")
            {
                Common.mCommon.saveNoti(argRES_CONTROL, notiInfo);
            }
        }
        #endregion

        #region "Event"
       
        private void OnEntryCompleted(object sender, EventArgs e)
        {
            try
            {
                string text = ((Entry)sender).Text;
                if (Common.mCommon.UserSetting.TLSearchTypeAsk == "1")//1 for local search
                {

                    if (text != null && text != "")
                    {
                        mVmlWishlist.searchData(text);
                    }
                    else
                    {
                        mVmlWishlist.searchData("");
                    }
                }
                else
                {
                    mVmlWishlist.searchDataApi(text);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void OnEditSwipeInvoked(object sender, EventArgs e)
        {
            //if (sender is SwipeItem swipeItem && swipeItem.BindingContext is DAT_WISHLIST selectedItem)
            //{
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(selectedItem));
            //}
        }
        private void OnMenuTapped(object sender, TappedEventArgs e)
        {
            Overlay.IsVisible = true;
            MenuBox.IsVisible = true;
        }
        private void OnOverlayTapped(object sender, EventArgs e)
        {
            MenuBox.IsVisible = false;
            Overlay.IsVisible = false;
        }
        private async void OnItemSingleTapped(object sender, object tappedItem)
        {
            //if (Utility.checkButtonAccess("Edit"))
            //{
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet((DAT_WISHLIST)tappedItem));
            //}
            //else
            //{
            //    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            //}
        }
        private async void OnItemDoubleTapped(object sender, object tappedItem)
        {
            //var popup = new OptionsPopup(Common.mCommon.SelectedMenu.button);
            //popup.OnNewClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnNew_onClick(argRES_CONTROL);
            //};
            //popup.OnEditClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnEdit_onClick(tappedItem, argRES_CONTROL);
            //};
            //popup.OnDeleteClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnDelete_onClick(tappedItem, argRES_CONTROL);
            //};
            //popup.OnPrintClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnPrint_onClick(argRES_CONTROL);
            //};
            //popup.OnSendMailClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnSendMail_onClick(argRES_CONTROL);
            //};
            //popup.OnExpPDFClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnExpPDF_onClick(argRES_CONTROL);
            //};
            //popup.OnExpExcelClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnExpExcel_onClick(argRES_CONTROL);
            //};
            //popup.OnExpCSVClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnExpCSV_onClick(argRES_CONTROL);
            //};
            //popup.OnPostClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnPost_onClick(argRES_CONTROL);
            //};
            //popup.OnSummaryClicked = async (RES_CONTROL argRES_CONTROL) =>
            //{
            //    await btnSummary_onClick(argRES_CONTROL);
            //};

            //await this.GetParentPage().ShowPopupAsync(popup);

        }
        private void OnCheckAllCheckChanged(object sender, CheckedChangedEventArgs e)
        {
            bool checkAll = chkSelectAll.IsChecked;

            for (int i = 0; i < mVmlWishlist.WishlistLst.Count; i++)
            {
                var item = mVmlWishlist.WishlistLst[i];
                item.IsChecked = checkAll ? "1" : "0";

                mVmlWishlist.WishlistLst.RemoveAt(i);
                mVmlWishlist.WishlistLst.Insert(i, item);
            }
        }
        private void Sorting_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is SortingItem tappedItem)
            {
                // Hide all icons
                foreach (var item in mVmlWishlist.sortingList)
                    item.ShowIcon = false;

                // Show only tapped item’s icon
                tappedItem.ShowIcon = true;
                sortSalesInvoiceList(tappedItem.value);
            }
        }
        private void Ascending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlWishlist.IsDescending = false;
            mVmlWishlist.IsAscending = true;
        }
        private void Descending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlWishlist.IsDescending = true;
            mVmlWishlist.IsAscending = false;
        }
        private void chkSelectItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is DAT_WISHLIST item)
            {
                item.IsChecked = e.Value ? "1" : "0";
            }
        }
        private async void OnListSingleTap(object sender, TappedEventArgs e)
        {
            //if (Utility.checkButtonAccess("Edit"))
            //{
            //    if (e.Parameter is DAT_WISHLIST tappedItem)
            //    {
            //        await Navigation.PushAsync(new FrmPosSaleInvoiceSet(tappedItem));
            //    }
            //}
            //else
            //{
            //    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            //}
        }
        private void OnListNGridLongPress(object sender, EventArgs e)
        {
            if (sender is Grid gridItem && gridItem.BindingContext is DAT_WISHLIST selectedItem)
            {
                if (selectedItem == null)
                    return;
                int index = mVmlWishlist.WishlistLst.IndexOf(selectedItem);
                selectedItem.IsChecked = "1";
                mVmlWishlist.WishlistLst.RemoveAt(index);
                mVmlWishlist.WishlistLst.Insert(index, selectedItem);
                OnItemDoubleTapped(sender, selectedItem);
            }
        }
        private void OnListDoubleTap(object sender, TappedEventArgs e)
        {
            var tappedItem = e.Parameter as DAT_WISHLIST;

            if (tappedItem == null)
                return;
            tappedItem.IsChecked = "1";
            OnItemDoubleTapped(sender, tappedItem);
        }
        private async void OnGridSingleTap(object sender, TappedEventArgs e)
        {
            //if (!Utility.checkButtonAccess("Edit"))
            //{
            //    WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            //    return;
            //}

            //if (sender is VisualElement ve && ve.BindingContext is DAT_WISHLIST tappedItem)
            //{
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(tappedItem));
            //}
        }
        private void OnGridDoubleTap(object sender, TappedEventArgs e)
        {
            if (sender is VisualElement ve && ve.BindingContext is DAT_WISHLIST tappedItem)
            {
                tappedItem.IsChecked = "1";
                OnItemDoubleTapped(sender, tappedItem);
            }
        }
        #endregion
    }
}