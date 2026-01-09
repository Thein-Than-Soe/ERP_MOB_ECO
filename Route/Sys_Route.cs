using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.POS;
using CS.ERP_MOB.Views.SYS;

namespace CS.ERP_MOB.Route
{
    public class Sys_Route
    {
        //public static Dictionary<string, ContentView> RouteModels { get; private set; }

        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Sys_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("signin", typeof(FrmSignIn));
            DicRouteList.Add("signup", typeof(FrmSignUp));
            DicRouteList.Add("FrmAdmin", typeof(FrmAdmin));
            DicRouteList.Add("home", typeof(HomePage));

            //DicRouteList.Add("signin", typeof(LoginPage));
            //DicRouteList.Add("signup", typeof(SignupPage));
            //DicRouteList.Add("setting", typeof(SettingPage));
            DicRouteList.Add("change-password", typeof(ChangePasswordPage));
            //RouteList.Add("profile", typeof(ProfilePage));
            //DicRouteList.Add("Transaction", typeof(MyTransactionPage));
            //DicRouteList.Add("myorder-lst", typeof(MyOrderPage));
            //DicRouteList.Add("billing", typeof(MyBillingPage));
            //DicRouteList.Add("ad", typeof(ADPage));
            //DicRouteList.Add("user-access", typeof(UserAccessPage));

            DicRouteList.Add("sys-myorder-lst", typeof(FrmSysMyOrderLst));
            DicRouteList.Add("sys-mybilling-lst", typeof(FrmSysMyBillingLst));
            DicRouteList.Add("sys-mypayment-lst", typeof(FrmSysMyPaymentLst));
            DicRouteList.Add("sys-mytransaction-lst", typeof(FrmSysMyTransactionLst));
            DicRouteList.Add("sys-apiconfig-lst", typeof(FrmSysApiConfigLst));
            DicRouteList.Add("sys-apiconfig-set", typeof(FrmApiConfigSet));


            //test temporary
            //DicRouteList.Add("profile", typeof(AccessSetPage));
            
            DicRouteList.Add("access-lst", typeof(FrmAccessLst));
            DicRouteList.Add("sys-country-lst", typeof(FrmCountryLst));
            DicRouteList.Add("sys-country-set", typeof(FrmCountrySet));
           


            DicRouteList.Add("sys-myorder-mm-lst", typeof(FrmSysMyOrderLst));
            DicRouteList.Add("sys-mybilling-mm-lst", typeof(FrmSysMyBillingLst));
            DicRouteList.Add("sys-mypayment-mm-lst", typeof(FrmSysMyPaymentLst));
            DicRouteList.Add("sys-mytransaction-mm-lst", typeof(FrmSysMyTransactionLst));
            DicRouteList.Add("sys-apiconfig-mm-lst", typeof(FrmApiConfigLst));
            //  DicRouteList.Add("sys-apiconfig-set", typeof(FrmApiConfigSet));
            //test temporary
            //DicRouteList.Add("profile", typeof(AccessSetPage));
            DicRouteList.Add("sys-apiconfig-mm-set", typeof(FrmAccessSet));
        }
    }
}
