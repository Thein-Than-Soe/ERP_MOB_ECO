using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using FreshMvvm.Maui;

namespace CS.ERP_MOB.ViewsModel.Frame
{
    class VmlProduct : FreshBasePageModel
    {

        public List<RES_PRODUCT> mRES_PRODUCT;
        public List<RES_PRODUCT> ProductList
        {
            get { return mRES_PRODUCT; }
            set { mRES_PRODUCT = value; RaisePropertyChanged("ProductList"); }
        }
        public VmlProduct(Page currentPage)
        {
            CoreMethods = new PageModelCoreMethods(currentPage, this);
            ProductList = Common.mCommon.RES_PRODUCT_LST;
        }
    }
}
