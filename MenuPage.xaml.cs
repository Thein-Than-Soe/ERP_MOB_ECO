using CommunityToolkit.Mvvm.Messaging;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.Data;
using CS.ERP_MOB.General;
using RGPopup.Maui.Extensions;
using RGPopup.Maui.Pages;
using RGPopup.Maui.Services;
using System;
using System.Collections.ObjectModel;

namespace CS.ERP_MOB
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : PopupPage
    {
        private ObservableCollection<ObservableProperty> mRES_MENU_LST;
        private ObservableCollection<ObservableProperty> mRES_MENU_PRO = new ObservableCollection<ObservableProperty>();
        private ObservableCollection<Menugroup> _allMenuGroups;
        private ObservableCollection<Menugroup> _expandedMenuGroups;

        public MenuPage()
        {
            try
            {
                InitializeComponent();
                // Associate the Page with its PageModel manually - this is normally done as part of FreshMvvm navigation, but we're not using
                // FreshMvvm to display the Menu.
                BindingContext = new MenuPageModel(this);
                Common.mCommon.InitMenuGroup();
                UpdateListContent();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void lstProduct_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var l_RES_PRODUCT = (RES_PRODUCT)ProductList.SelectedItem;
                if (l_RES_PRODUCT != null)
                {
                    Common.mCommon.switchProduct(l_RES_PRODUCT, () => {
                        var l_viewModel = (MenuPageModel)BindingContext;
                        l_viewModel.ToggleDropdownCommand.Execute(null);
                        l_viewModel.SelectedProduct = Common.mCommon.SelectedProduct;
                        UpdateListContent();
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void UpdateListContent()
        {
            try
            {
                mRES_MENU_PRO.Clear();
                mRES_MENU_LST = new ObservableCollection<ObservableProperty>(Common.mMenuList);
                foreach (ObservableProperty l_RES_MENU in mRES_MENU_LST)
                {
                    if (l_RES_MENU.SubMenuWidth != "2")
                    {
                        //Create new MenuGroup so we do not alter original list
                        ObservableProperty newGroup = new ObservableProperty();
                        newGroup.Text = l_RES_MENU.Text;
                        if (l_RES_MENU.Count > 0)
                        {
                            if (l_RES_MENU.Expanded)
                            {
                                //down arrow state icon
                                newGroup.StateIcon = "&#xf107;";

                                foreach (RES_MENU menu in l_RES_MENU)
                                {
                                    newGroup.Add(menu);
                                }
                            }
                            else
                            {
                                //right arrow state icon
                                newGroup.StateIcon = "&#xf107;";
                            }
                        }
                        else
                        {
                            newGroup.StateIcon = "";
                            newGroup.MenuUrl = l_RES_MENU.MenuUrl;
                        }
                        mRES_MENU_PRO.Add(newGroup);
                    }
                }
            
                MenuListView.ItemsSource = mRES_MENU_PRO;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
        private void TgrMenuHeader_onTapped(object sender, EventArgs args)
        {
            try
            {
                var selectedIndex = mRES_MENU_PRO.IndexOf(((ObservableProperty)((StackLayout)sender).BindingContext));

                //if menu has sub meu
                if (mRES_MENU_PRO[selectedIndex].Count > 0)
                {
                    mRES_MENU_PRO[selectedIndex].Expanded = !mRES_MENU_PRO[selectedIndex].Expanded;
                    UpdateListContent();
                }
                else
                {
                    //there is no sub menu
                    RES_MENU l_RES_MENU = new RES_MENU();
                    // l_RES_MENU = new RES_MENU { ProductAsk = mRES_MENU_LST[selectedIndex].MenuUrl, Text = mRES_MENU_LST[selectedIndex].Text, MenuUrl = mRES_MENU_LST[selectedIndex].MenuUrl, logoImg = mRES_MENU_LST[selectedIndex]. };
                    l_RES_MENU.MenuUrl = mRES_MENU_PRO[selectedIndex].MenuUrl;
                    l_RES_MENU.button = mRES_MENU_PRO[selectedIndex].button;
                    l_RES_MENU.Text = mRES_MENU_PRO[selectedIndex].Text;

                    if (l_RES_MENU != null)
                    {
                        if (!Common.bindMenu(l_RES_MENU.MenuUrl))
                        {
                            Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Request Access", MenuUrl = "access-lst", logoImg = "" };
                            WeakReferenceMessenger.Default.Send("No access menu - " + l_RES_MENU.Text);
                        }

                        Common.routeMenu(Common.mCommon.SelectedMenu);
                        PopupNavigation.Instance.PopAllAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        private void MenuList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var selectedItem = (ObservableProperty)MenuListView.SelectedItem;
                if (selectedItem != null)
                {
                    RES_MENU l_RES_MENU = new RES_MENU();
                    l_RES_MENU.MenuUrl = selectedItem.MenuUrl;
                    l_RES_MENU.button = selectedItem.button;
                    l_RES_MENU.Text = selectedItem.Text;
                    if (!Common.bindMenu(l_RES_MENU.MenuUrl))
                    {
                        WeakReferenceMessenger.Default.Send("No access menu - " + l_RES_MENU.Text);
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);
                    PopupNavigation.Instance.PopAllAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }
        public void MenuTapped(object sender, EventArgs e)
        {
            try
            {
                //var selectedIndex = mRES_MENU_PRO.IndexOf(((ObservableProperty)((StackLayout)sender).BindingContext));
                //var selectedItem = mRES_MENU_LST[selectedIndex];

                var selectedItem = (ObservableProperty)MenuListView.SelectedItem;
                if (selectedItem != null)
                {
                    RES_MENU l_RES_MENU = new RES_MENU();
                    l_RES_MENU.MenuUrl = selectedItem.MenuUrl;
                    l_RES_MENU.button = selectedItem.button;
                    l_RES_MENU.Text = selectedItem.Text;

                    if (!Common.bindMenu(l_RES_MENU.MenuUrl))
                    {
                        Common.mCommon.SelectedMenu = new RES_MENU { ProductAsk = "1", Text = "Sign In", MenuUrl = "signin", logoImg = "" };
                    }
                    Common.routeMenu(Common.mCommon.SelectedMenu);
                    PopupNavigation.Instance.PopAllAsync();
                }

                //PopupNavigation.Instance.PopAllAsync();
                //ContentView contentView = new AccessListPage();
                //RoutingModel routemodel = new RoutingModel( contentView, "Access List");
                //MessagingCenter.Send<Application, RoutingModel>(Application.Current, "ViewChange", routemodel);


            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private async void FacebookTapped(object sender, EventArgs e)
        {
            try
            {
                if (Common.mCommon.SelectedProduct != null)
                {
                    await OpenBrowser(Common.mCommon.SelectedProduct.FBURL);
                }

                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void TwitterTapped(object sender, EventArgs e)
        {
            try
            {
                if (Common.mCommon.SelectedProduct != null)
                {
                    await OpenBrowser(Common.mCommon.SelectedProduct.TwitterURL);
                }

                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        private async void YoutubeTapped(object sender, EventArgs e)
        {
            try
            {
                if (Common.mCommon.SelectedProduct != null)
                {
                    await OpenBrowser(Common.mCommon.SelectedProduct.YoutubeURL);
                }

                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }

        public async Task OpenBrowser(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);

            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send("" + ex.InnerException?.ToString());
                //MessagingCenter.Send<Application, string>(Application.Current, ApplicationMessage.Message.Alert, ex.InnerException.ToString());
            }
        }

        private void MenuTapped(object sender, TappedEventArgs e)
        {

        }
    }
}