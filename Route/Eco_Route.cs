using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.ERP_MOB.Views.ECO;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB_ECO.Views.ECO;
namespace CS.ERP_MOB.Route
{
    public class Eco_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Eco_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));
            DicRouteList.Add("signin", typeof(FrmSignIn));
            DicRouteList.Add("signup", typeof(FrmSignUp));
            DicRouteList.Add("change-password", typeof(ChangePasswordPage));
            DicRouteList.Add("search", typeof(FrmSearchPage));
            DicRouteList.Add("shelf", typeof(FrmShelf));

            DicRouteList.Add("eco-dashboard", typeof(FrmShelf));
            DicRouteList.Add("eco-product-lst", typeof(FrmShelf));
            DicRouteList.Add("eco-checkout", typeof(FrmEcoCheckOut));

            DicRouteList.Add("eco-my-wishlist", typeof(FrmWishlistLst));
            DicRouteList.Add("eco-my-shopping-cart", typeof(FrmShoppingCartLst));
            DicRouteList.Add("eco-my-order-lst", typeof(FrmMyOrderLst));



        }
    }
}