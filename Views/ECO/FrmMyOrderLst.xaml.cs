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
    public partial class FrmMyOrderLst : ContentView
    {
        #region "Declaring"
        VmlMyOrderLst mVmlMyOrderLst { get; set; }
        #endregion
        #region "Constructor"
        public FrmMyOrderLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlMyOrderLst = new VmlMyOrderLst();
                mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER = new RES_SALE_ORDER();
                //mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER.SD = Utility.getTLFormLoadSD();
                //mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER.ED = Utility.getTLFormLoadED();
                mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER_DETAIL.Add(new RES_SALE_ORDER_DETAIL());
                mVmlMyOrderLst.getMyOrder();
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

            if (mVmlMyOrderLst != null)
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
            if (mVmlMyOrderLst.SaleOrderLst == null || !mVmlMyOrderLst.SaleOrderLst.Any())
                return;

            IEnumerable<RES_SALE_ORDER> sorted;

            switch (sortBy)
            {
                case "OrderDate":
                    sorted = mVmlMyOrderLst.IsAscending
                        ? mVmlMyOrderLst.SaleOrderLst.OrderBy(x => x.OrderDate)
                        : mVmlMyOrderLst.SaleOrderLst.OrderByDescending(x => x.OrderDate);
                    break;

                case "OrderCode_0_50":
                    sorted = mVmlMyOrderLst.IsAscending
                        ? mVmlMyOrderLst.SaleOrderLst.OrderBy(x => x.OrderCode_0_50)
                        : mVmlMyOrderLst.SaleOrderLst.OrderByDescending(x => x.OrderCode_0_50);
                    break;

                case "SalePersonName_0_255":
                    sorted = mVmlMyOrderLst.IsAscending
                        ? mVmlMyOrderLst.SaleOrderLst.OrderBy(x => x.SalePersonName_0_255)
                        : mVmlMyOrderLst.SaleOrderLst.OrderByDescending(x => x.SalePersonName_0_255);
                    break;

                case "StatusName_0_255":
                    sorted = mVmlMyOrderLst.IsAscending
                        ? mVmlMyOrderLst.SaleOrderLst.OrderBy(x => x.StatusName_0_255)
                        : mVmlMyOrderLst.SaleOrderLst.OrderByDescending(x => x.StatusName_0_255);
                    break;

                case "GrandTotal":
                    sorted = mVmlMyOrderLst.IsAscending
                        ? mVmlMyOrderLst.SaleOrderLst.OrderBy(x => x.GrandTotal)
                        : mVmlMyOrderLst.SaleOrderLst.OrderByDescending(x => x.GrandTotal);
                    break;

                default:
                    return;
            }

            mVmlMyOrderLst.SaleOrderLst = new ObservableCollection<RES_SALE_ORDER>(sorted);
            if (mVmlMyOrderLst.IsCardView)
            {
                collectionView.ItemsSource = mVmlMyOrderLst.SaleOrderLst;
            }
            else if (mVmlMyOrderLst.IsListView)
            {
                lstView.ItemsSource = mVmlMyOrderLst.SaleOrderLst;
            }
            else
            {
                MyGrid.ItemsSource = mVmlMyOrderLst.SaleOrderLst;
            }
        }
        private void getCheckedData(List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST)
        {
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
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
            //    RES_SALE_ORDER l_RES_SALE_ORDER = (RES_SALE_ORDER)tappedItem;
            //    Common.mCommon.saveNoti(argRES_CONTROL, l_RES_SALE_ORDER.Ask);
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(l_RES_SALE_ORDER));
            //}
        }
        private async Task btnDelete_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            RES_SALE_ORDER l_RES_SALE_ORDER = (RES_SALE_ORDER)tappedItem;
            if (l_RES_SALE_ORDER.StatusAsk != "9" && l_RES_SALE_ORDER.PostingStatusAsk != "1")
            {
                string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete ") + l_RES_SALE_ORDER.OrderCode_0_50 + "?";

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
                    mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER = l_RES_SALE_ORDER;
                    mVmlMyOrderLst.mJSN_REQ_SALE_ORDER_JUN.RES_SALE_ORDER.StatusAsk = "6";
                    mVmlMyOrderLst.saveInvoice();
                    Common.mCommon.saveNoti(argRES_CONTROL, l_RES_SALE_ORDER.Ask);
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgDelete"));
            }
        }
        private async Task btnPrint_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            getCheckedData(l_RES_SALE_ORDER_LST);
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Print");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Post");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
            List<RES_SALE_ORDER> l_RES_SALE_ORDER_LST = new List<RES_SALE_ORDER>();
            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                if (mVmlMyOrderLst.SaleOrderLst[i].IsChecked == "1")
                {
                    l_RES_SALE_ORDER_LST.Add(mVmlMyOrderLst.SaleOrderLst[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            string notiInfo = "";
            foreach (RES_SALE_ORDER item in l_RES_SALE_ORDER_LST)
            {
                messageInfo += item.OrderCode_0_50 + ",";
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
                        mVmlMyOrderLst.searchData(text);
                    }
                    else
                    {
                        mVmlMyOrderLst.searchData("");
                    }
                }
                else
                {
                    mVmlMyOrderLst.searchDataApi(text);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void OnEditSwipeInvoked(object sender, EventArgs e)
        {
            //if (sender is SwipeItem swipeItem && swipeItem.BindingContext is RES_SALE_ORDER selectedItem)
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
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet((RES_SALE_ORDER)tappedItem));
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

            for (int i = 0; i < mVmlMyOrderLst.SaleOrderLst.Count; i++)
            {
                var item = mVmlMyOrderLst.SaleOrderLst[i];
                item.IsChecked = checkAll ? "1" : "0";

                mVmlMyOrderLst.SaleOrderLst.RemoveAt(i);
                mVmlMyOrderLst.SaleOrderLst.Insert(i, item);
            }
        }
        private void Sorting_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is SortingItem tappedItem)
            {
                // Hide all icons
                foreach (var item in mVmlMyOrderLst.sortingList)
                    item.ShowIcon = false;

                // Show only tapped item’s icon
                tappedItem.ShowIcon = true;
                sortSalesInvoiceList(tappedItem.value);
            }
        }
        private void Ascending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlMyOrderLst.IsDescending = false;
            mVmlMyOrderLst.IsAscending = true;
        }
        private void Descending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlMyOrderLst.IsDescending = true;
            mVmlMyOrderLst.IsAscending = false;
        }
        private void chkSelectItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RES_SALE_ORDER item)
            {
                item.IsChecked = e.Value ? "1" : "0";
            }
        }
        private async void OnListSingleTap(object sender, TappedEventArgs e)
        {
            //if (Utility.checkButtonAccess("Edit"))
            //{
            //    if (e.Parameter is RES_SALE_ORDER tappedItem)
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
            if (sender is Grid gridItem && gridItem.BindingContext is RES_SALE_ORDER selectedItem)
            {
                if (selectedItem == null)
                    return;
                int index = mVmlMyOrderLst.SaleOrderLst.IndexOf(selectedItem);
                selectedItem.IsChecked = "1";
                mVmlMyOrderLst.SaleOrderLst.RemoveAt(index);
                mVmlMyOrderLst.SaleOrderLst.Insert(index, selectedItem);
                OnItemDoubleTapped(sender, selectedItem);
            }
        }
        private void OnListDoubleTap(object sender, TappedEventArgs e)
        {
            var tappedItem = e.Parameter as RES_SALE_ORDER;

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

            //if (sender is VisualElement ve && ve.BindingContext is RES_SALE_ORDER tappedItem)
            //{
            //    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(tappedItem));
            //}
        }
        private void OnGridDoubleTap(object sender, TappedEventArgs e)
        {
            if (sender is VisualElement ve && ve.BindingContext is RES_SALE_ORDER tappedItem)
            {
                tappedItem.IsChecked = "1";
                OnItemDoubleTapped(sender, tappedItem);
            }
        }
        #endregion
    }
}