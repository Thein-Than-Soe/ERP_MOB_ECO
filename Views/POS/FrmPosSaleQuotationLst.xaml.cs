using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Maui.Views;
using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.ViewsModel.POS;
using System.Collections.ObjectModel;
using CS.ERP.PL.SYS.DAT;

namespace CS.ERP_MOB.Views.POS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrmPosSaleQuotationLst : ContentView
    {
        #region "Declaring"
        VmlSalesQuotation mVmlSalesQuotation { get; set; }
        #endregion
        #region "Constructor"
        public FrmPosSaleQuotationLst()
        {
            try
            {
                InitializeComponent();
                BindingContext = mVmlSalesQuotation = new VmlSalesQuotation();
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.REQ_AUTHORIZATION = Common.mCommon.REQ_AUTHORIZATION;
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = new RES_SALE_QUOTATION();
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.SD = Utility.getTLFormLoadSD();
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.ED = Utility.getTLFormLoadED();
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION_DETAIL.Add(new RES_SALE_QUOTATION_DETAIL());
                mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_BROWSE.Add(new RES_SALE_BROWSE());
                mVmlSalesQuotation.getQuotation();
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

            if (mVmlSalesQuotation != null)
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
        private void sortSalesQuotationList(string sortBy)
        {
            if (mVmlSalesQuotation.SalesQuotationList == null || !mVmlSalesQuotation.SalesQuotationList.Any())
                return;

            IEnumerable<RES_SALE_QUOTATION> sorted;

            switch (sortBy)
            {
                case "QuotationDate":
                    sorted = mVmlSalesQuotation.IsAscending
                        ? mVmlSalesQuotation.SalesQuotationList.OrderBy(x => x.QuotationDate)
                        : mVmlSalesQuotation.SalesQuotationList.OrderByDescending(x => x.QuotationDate);
                    break;

                case "QuotationCode_0_50":
                    sorted = mVmlSalesQuotation.IsAscending
                        ? mVmlSalesQuotation.SalesQuotationList.OrderBy(x => x.QuotationCode_0_50)
                        : mVmlSalesQuotation.SalesQuotationList.OrderByDescending(x => x.QuotationCode_0_50);
                    break;

                case "CustomerName_0_255":
                    sorted = mVmlSalesQuotation.IsAscending
                        ? mVmlSalesQuotation.SalesQuotationList.OrderBy(x => x.CustomerName_0_255)
                        : mVmlSalesQuotation.SalesQuotationList.OrderByDescending(x => x.CustomerName_0_255);
                    break;

                case "StatusName_0_255":
                    sorted = mVmlSalesQuotation.IsAscending
                        ? mVmlSalesQuotation.SalesQuotationList.OrderBy(x => x.StatusName_0_255)
                        : mVmlSalesQuotation.SalesQuotationList.OrderByDescending(x => x.StatusName_0_255);
                    break;

                case "GrandTotal":
                    sorted = mVmlSalesQuotation.IsAscending
                        ? mVmlSalesQuotation.SalesQuotationList.OrderBy(x => x.GrandTotal)
                        : mVmlSalesQuotation.SalesQuotationList.OrderByDescending(x => x.GrandTotal);
                    break;

                default:
                    return;
            }

            mVmlSalesQuotation.SalesQuotationList = new ObservableCollection<RES_SALE_QUOTATION>(sorted);
            if (mVmlSalesQuotation.IsCardView)
            {
                collectionView.ItemsSource = mVmlSalesQuotation.SalesQuotationList;
            }
            else if (mVmlSalesQuotation.IsListView)
            {
                lstView.ItemsSource = mVmlSalesQuotation.SalesQuotationList;
            }
            else
            {
                MyGrid.ItemsSource = mVmlSalesQuotation.SalesQuotationList;
            }
        }
        private void getCheckedData(List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST)
        {
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
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
                await Navigation.PushAsync(new FrmPosSaleQuotationSet());
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
                await Navigation.PushAsync(new FrmPosSaleQuotationSet((RES_SALE_QUOTATION)tappedItem));
            }
        }
        private async Task btnDelete_onClick(object tappedItem, RES_CONTROL argRES_CONTROL)
        {
            RES_SALE_QUOTATION l_RES_SALE_QUOTATION = (RES_SALE_QUOTATION)tappedItem;
            if (l_RES_SALE_QUOTATION.StatusAsk != "9" && l_RES_SALE_QUOTATION.PostingStatusAsk != "1")
            {
                string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Delete ") + l_RES_SALE_QUOTATION.QuotationCode_0_50 + "?";
                bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                    Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                    Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
                if (answer)
                {
                    mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION = l_RES_SALE_QUOTATION;
                    mVmlSalesQuotation.mJSN_REQ_SALE_QUOTATION_JUN.RES_SALE_QUOTATION.StatusAsk = "6";
                    mVmlSalesQuotation.saveQuotation();
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            getCheckedData(l_RES_SALE_QUOTATION_LST);
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Print");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
            }
            bool answer = await this.GetParentPage().DisplayAlert("Info", messageInfo,
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.Yes"),
                Common.mCommon.GetLanguageValueByKey("POS.Common.btnName.No"));
            if (answer)
            {
                Common.mCommon.saveNoti(argRES_CONTROL);
            }
        }
        private async Task btnSendMail_onClick(RES_CONTROL argRES_CONTROL)
        {
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Send");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Post");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
            List<RES_SALE_QUOTATION> l_RES_SALE_QUOTATION_LST = new List<RES_SALE_QUOTATION>();
            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                if (mVmlSalesQuotation.SalesQuotationList[i].IsChecked == "1")
                {
                    l_RES_SALE_QUOTATION_LST.Add(mVmlSalesQuotation.SalesQuotationList[i]);
                }
            }
            string messageInfo = Common.mCommon.GetLanguageValueByKey("POS.Common.confirm.Export");
            foreach (RES_SALE_QUOTATION item in l_RES_SALE_QUOTATION_LST)
            {
                messageInfo += item.QuotationCode_0_50 + ",";
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
                await Navigation.PushAsync(new FrmPosSaleQuotationSet());
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
                        mVmlSalesQuotation.searchData(text);
                    }
                    else
                    {
                        mVmlSalesQuotation.searchData("");
                    }
                }
                else
                {
                    mVmlSalesQuotation.searchDataApi(text);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private async void OnEditSwipeInvoked(object sender, EventArgs e)
        {
            if (sender is SwipeItem swipeItem && swipeItem.BindingContext is RES_SALE_QUOTATION selectedItem)
            {
                await Navigation.PushAsync(new FrmPosSaleQuotationSet(selectedItem));
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
                await Navigation.PushAsync(new FrmPosSaleQuotationSet((RES_SALE_QUOTATION)tappedItem));
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

            for (int i = 0; i < mVmlSalesQuotation.SalesQuotationList.Count; i++)
            {
                var item = mVmlSalesQuotation.SalesQuotationList[i];
                item.IsChecked = checkAll ? "1" : "0";

                mVmlSalesQuotation.SalesQuotationList.RemoveAt(i);
                mVmlSalesQuotation.SalesQuotationList.Insert(i, item);
            }
        }
        private void Sorting_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is SortingItem tappedItem)
            {
                // Hide all icons
                foreach (var item in mVmlSalesQuotation.sortingList)
                    item.ShowIcon = false;

                // Show only tapped item’s icon
                tappedItem.ShowIcon = true;
                sortSalesQuotationList(tappedItem.value);
            }
        }
        private void Ascending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlSalesQuotation.IsDescending = false;
            mVmlSalesQuotation.IsAscending = true;
        }
        private void Descending_Tapped(object sender, TappedEventArgs e)
        {
            mVmlSalesQuotation.IsDescending = true;
            mVmlSalesQuotation.IsAscending = false;
        }
        private void chkSelectItem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is RES_SALE_QUOTATION item)
            {
                item.IsChecked = e.Value ? "1" : "0";
            }
        }
        private async void OnListSingleTap(object sender, TappedEventArgs e)
        {
            if (Utility.checkButtonAccess("Edit"))
            {
                if (e.Parameter is RES_SALE_QUOTATION tappedItem)
                {
                    await Navigation.PushAsync(new FrmPosSaleQuotationSet(tappedItem));
                }
            }
            else
            {
                WeakReferenceMessenger.Default.Send(Common.mCommon.GetMessageValueByKey("MsgAccess"));
            }
        }
        private void OnListDoubleTap(object sender, TappedEventArgs e)
        {
            var tappedItem = e.Parameter as RES_SALE_QUOTATION;

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

            if (sender is VisualElement ve && ve.BindingContext is RES_SALE_QUOTATION tappedItem)
            {
                await Navigation.PushAsync(new FrmPosSaleQuotationSet(tappedItem));
            }
        }
        private void OnGridDoubleTap(object sender, TappedEventArgs e)
        {
            if (sender is VisualElement ve && ve.BindingContext is RES_SALE_QUOTATION tappedItem)
            {
                tappedItem.IsChecked = "1";
                OnItemDoubleTapped(sender, tappedItem);
            }
        }
        #endregion
    }
}