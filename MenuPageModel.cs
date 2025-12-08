using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using FreshMvvm.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CS.ERP_MOB
{
    class MenuPageModel : FreshBasePageModel
    {

        private bool mProductDropdownVisible;
        private string mDropDownIcon = "\uf0d7";

        public bool ProductDropdownVisible
        {
            get => mProductDropdownVisible;
            set
            {
                if (mProductDropdownVisible != value)
                {
                    mProductDropdownVisible = value;
                    RaisePropertyChanged(nameof(ProductDropdownVisible));
                }
            }
        }

        public string DropDownIcon
        {
            get => mDropDownIcon;
            set {

                if (mDropDownIcon != value)
                {
                    mDropDownIcon = value;
                    RaisePropertyChanged(nameof(DropDownIcon));
                }
            }
        }

        public ICommand ToggleDropdownCommand => new Command(() =>
        {
            ProductDropdownVisible = !ProductDropdownVisible;
            if (ProductDropdownVisible)
            {
                DropDownIcon = "\uf0d8"; //fontawesome caret-down
            }
            else
            {
                DropDownIcon = "\uf0d7"; //fontawesome caret-up
            }
        });

        private List<RES_PRODUCT> mRES_PRODUCT;
        public List<RES_PRODUCT> ProductList
        {
            get { return mRES_PRODUCT; }
            set { mRES_PRODUCT = value; RaisePropertyChanged("ProductList"); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BottomTabBarDemo.MenuPageModel"/> class.
        /// </summary>
        /// <param name="currentPage">The page with which the PageModel should be linked</param>
        /// <remarks>
        /// Because we're not navigating to this page via the normal FreshMvvm navigation, we need to
        /// create a CoreMethods instance manually.
        /// </remarks>

        public RES_PRODUCT _SelectedProduct;
        public RES_PRODUCT SelectedProduct
        {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value;
                RaisePropertyChanged("SelectedProduct"); }
        }

        public MenuPageModel(Page currentPage)
        {
            try
            {
                CoreMethods = new PageModelCoreMethods(currentPage, this);
                //mMenuList = Common.mJSN_RES_MOBILE_LOGIN.menu; // IntercomService.mProfile.menu;
                // MenuList = Common.mJSN_RES_MOBILE_LOGIN.menu;
                SelectedProduct = Common.mCommon.SelectedProduct; //IntercomService.Current.SelectedProduct;
                ProductList = Common.mCommon.RES_PRODUCT_LST;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            
        }

    }
}
