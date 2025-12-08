using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System.Collections.ObjectModel;
using CS.ERP_MOB.Services.NTF;
using CS.ERP.PL.SYS.DAT;

namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosSaleInvoiceLst : ContentView
    {
        #region "Declaring"
        VmlSalesInvoice mVmlSalesInvoice { get; set; }
        Ntf_Service_WebSocket ntfSocketService = new Ntf_Service_WebSocket();
        #endregion
        #region "Constructor"
        public FrmPosSaleInvoiceLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesInvoice = new VmlSalesInvoice();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE = new RES_SALE_INVOICE();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE.SD = Utility.getTLFormLoadSD();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE.ED = Utility.getTLFormLoadED();
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE_DETAIL.Add(new RES_SALE_INVOICE_DETAIL());
                mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesInvoice.getInvoice();
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

            if (mVmlSalesInvoice != null)
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
            if (mVmlSalesInvoice.SalesInvoiceList == null || !mVmlSalesInvoice.SalesInvoiceList.Any())
                return;

            IEnumerable<RES_SALE_INVOICE> sorted;

            switch (sortBy)
            {
                case "InvoiceDate":
                    sorted = mVmlSalesInvoice.IsAscending
                        ? mVmlSalesInvoice.SalesInvoiceList.OrderBy(x => x.InvoiceDate)
                        : mVmlSalesInvoice.SalesInvoiceList.OrderByDescending(x => x.InvoiceDate);
                    break;

                case "InvoiceCode_0_50":
                    sorted = mVmlSalesInvoice.IsAscending
                        ? mVmlSalesInvoice.SalesInvoiceList.OrderBy(x => x.InvoiceCode_0_50)
                        : mVmlSalesInvoice.SalesInvoiceList.OrderByDescending(x => x.InvoiceCode_0_50);
                    break;

                case "CustomerName_0_255":
                    sorted = mVmlSalesInvoice.IsAscending
                        ? mVmlSalesInvoice.SalesInvoiceList.OrderBy(x => x.CustomerName_0_255)
                        : mVmlSalesInvoice.SalesInvoiceList.OrderByDescending(x => x.CustomerName_0_255);
                    break;

                case "StatusName_0_255":
                    sorted = mVmlSalesInvoice.IsAscending
                        ? mVmlSalesInvoice.SalesInvoiceList.OrderBy(x => x.StatusName_0_255)
                        : mVmlSalesInvoice.SalesInvoiceList.OrderByDescending(x => x.StatusName_0_255);
                    break;

                case "GrandTotal":
                    sorted = mVmlSalesInvoice.IsAscending
                        ? mVmlSalesInvoice.SalesInvoiceList.OrderBy(x => x.GrandTotal)
                        : mVmlSalesInvoice.SalesInvoiceList.OrderByDescending(x => x.GrandTotal);
                    break;

                default:
                    return;
            }

            mVmlSalesInvoice.SalesInvoiceList = new ObservableCollection<RES_SALE_INVOICE>(sorted);
            if (mVmlSalesInvoice.IsCardView)
            {
                collectionView.ItemsSource = mVmlSalesInvoice.SalesInvoiceList;
            }
            else if (mVmlSalesInvoice.IsListView)
            {
                lstView.ItemsSource = mVmlSalesInvoice.SalesInvoiceList;
            }
            else
            {
                MyGrid.ItemsSource = mVmlSalesInvoice.SalesInvoiceList;
            }
        }
        private void getCheckedData(List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST)
        {
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
        }
        #endregion

        #region "Task"
        private async Task btnNew_onClick(RES_CONTROL argRES_CONTROL)
        {
            bool answer = await this.GetParentPage().DisplayAlert("Info", Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.AddNew"),
                                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet());
            }
        }
        private async Task btnEdit_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            bool answer = await this.GetParentPage().DisplayAlert("Info", Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Edit"),
                                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet((RES_SALE_INVOICE)tappedItem));
            }
        }
        private async Task btnDelete_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            RES_SALE_INVOICE l_RES_SALE_INVOICE = (RES_SALE_INVOICE)tappedItem;
            if (l_RES_SALE_INVOICE.StatusAsk != "9" && l_RES_SALE_INVOICE.PostingStatusAsk != "1")
            {
                string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete ") + l_RES_SALE_INVOICE.InvoiceCode_0_50 + "?";
                bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                    Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                    Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
                if (answer)
                {
                    mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE = l_RES_SALE_INVOICE;
                    mVmlSalesInvoice.mJSN_REQ_SALE_INVOICE_JUN.RES_SALE_INVOICE.StatusAsk = "6";
                    mVmlSalesInvoice.saveInvoice();
                    Common.mCommon.saveNoti(argRES_CONTROL);
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgDelete"));
            }
        }
        private async Task btnPrint_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            getCheckedData(l_RES_SALE_INVOICE_LST);
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Print");
            string notiInfo = "";
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
                notiInfo += item.InvoiceCode_0_50 + ",";
            }
            if (messageInfo.Length > 0)
            {
                messageInfo = messageInfo.TrimEnd(',');
                notiInfo = notiInfo.TrimEnd(',');
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                argRES_CONTROL.link = notiInfo;
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnSendMail_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnExpPDF_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnExpExcel_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnExpCSV_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnPost_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Post");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnSummary_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_INVOICE> l_RES_SALE_INVOICE_LST = new List<RES_SALE_INVOICE>();
            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                if (mVmlSalesInvoice.SalesInvoiceList[i].IsChecked == "1")
                {
                    l_RES_SALE_INVOICE_LST.Add(mVmlSalesInvoice.SalesInvoiceList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_INVOICE item in l_RES_SALE_INVOICE_LST)
            {
                messageInfo += item.InvoiceCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        #endregion

        #region "Event"
        private async void TgrNew_Tapped(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void OnEntryCompleted(object sender, EventArgs e)
        {
            try
            {
                string text = ((Entry)sender).Text;
                if (Common.mCommon.UserSetting.TLSearchTypeAsk == "1")//1 for local search
                {

                    if (text != null && text != "")
                    {
                        mVmlSalesInvoice.searchData(text);
                    }
                    else
                    {
                        mVmlSalesInvoice.searchData("");
                    }
                }
                else
                {
                    mVmlSalesInvoice.searchDataApi(text);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void OnEditSwipeInvoked(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.BindingContext is RES_SALE_INVOICE selectedItem)
            {
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet(selectedItem));
            }
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
            if (Utility.checkButtonAccess("Edit"))
            {
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet((RES_SALE_INVOICE)tappedItem));
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            }
        }
        private async void OnItemDoubleTapped(object sender, object tappedItem)
        {
            var popup = new OptionsPopup(Common.mCommon.SelectedMenu.button); 
            popup.OnNewClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnNew_onClick(argRES_CONTROL);
            };
            popup.OnEditClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnEdit_onClick(tappedItem, argRES_CONTROL);
            };
            popup.OnDeleteClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnDelete_onClick(tappedItem, argRES_CONTROL);
            };
            popup.OnPrintClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnPrint_onClick(argRES_CONTROL);
            };
            popup.OnSendMailClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnSendMail_onClick(argRES_CONTROL);
            };
            popup.OnExpPDFClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnExpPDF_onClick(argRES_CONTROL);
            };
            popup.OnExpExcelClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnExpExcel_onClick(argRES_CONTROL);
            };
            popup.OnExpCSVClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnExpCSV_onClick(argRES_CONTROL);
            };
            popup.OnPostClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnPost_onClick(argRES_CONTROL);
            };
            popup.OnSummaryClicked = async (RES_CONTROL argRES_CONTROL) =>
            {
                await btnSummary_onClick(argRES_CONTROL);
            };

            await this.GetParentPage().ShowPopupAsync(popup);          

        }
        private void OnCheckAllCheckChanged(object sender, CheckedChangedEventArgs e)
        {
            bool checkAll = chkSelectAll.IsChecked;

            for (int i = 0; i < mVmlSalesInvoice.SalesInvoiceList.Count; i++)
            {
                var item = mVmlSalesInvoice.SalesInvoiceList[i];
                item.IsChecked = checkAll ? "1" : "0";

                mVmlSalesInvoice.SalesInvoiceList.RemoveAt(i);
                mVmlSalesInvoice.SalesInvoiceList.Insert(i, item);
            }
        }
        private void Sorting_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is SortingItem tappedItem)
            {
                // Hide all icons
                foreach (var item in mVmlSalesInvoice.sortingList)
                    item.ShowIcon = false;

                // Show only tapped item’s icon
                tappedItem.ShowIcon = true;
                sortSalesInvoiceList(tappedItem.value);
            }
        }
        private void Ascending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlSalesInvoice.IsDescending = false;
            mVmlSalesInvoice.IsAscending = true;
        }
        private void Descending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlSalesInvoice.IsDescending = true;
            mVmlSalesInvoice.IsAscending = false;
        }
        private void chkSelectItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RES_SALE_INVOICE item)
            {
                item.IsChecked = e.Value ? "1" : "0";
            }
        }
        private async void OnListSingleTap(object sender, TappedEventArgs e)
        {
            if (Utility.checkButtonAccess("Edit"))
            {
                if (e.Parameter is RES_SALE_INVOICE tappedItem)
                {
                    await Navigation.PushAsync(new FrmPosSaleInvoiceSet(tappedItem));
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            }
        }
        private void OnListDoubleTap(object sender, TappedEventArgs e)
        {
            var tappedItem = e.Parameter as RES_SALE_INVOICE;

            if (tappedItem == null)
                return;
            tappedItem.IsChecked = "1";
            OnItemDoubleTapped(sender, tappedItem);
        }
        private async void OnGridSingleTap(object sender, TappedEventArgs e)
        {
            if (!Utility.checkButtonAccess("Edit"))
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
                return;
            }

            if (sender is VisualElement ve && ve.BindingContext is RES_SALE_INVOICE tappedItem)
            {
                await Navigation.PushAsync(new FrmPosSaleInvoiceSet(tappedItem));
            }
        }
        private void OnGridDoubleTap(object sender, TappedEventArgs e)
        {
            if (sender is VisualElement ve && ve.BindingContext is RES_SALE_INVOICE tappedItem)
            {
                tappedItem.IsChecked = "1";
                OnItemDoubleTapped(sender, tappedItem);
            }
        }
        #endregion
    }
}