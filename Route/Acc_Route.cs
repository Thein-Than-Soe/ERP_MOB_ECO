using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.ACC;

namespace CS.ERP_MOB.Route
{
    public class Acc_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Acc_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));
            DicRouteList.Add("acc-account-lst", typeof(FrmAccAccountLst));
            DicRouteList.Add("acc-account-period-lst", typeof(FrmAccPeriodLst));
            DicRouteList.Add("acc-general-ledger-lst", typeof(FrmAccGeneralLedgerLst));

            DicRouteList.Add("acc-account-set", typeof(FrmAccAccountLst));
            DicRouteList.Add("acc-account-period-set", typeof(FrmAccPeriodSet));
          
            DicRouteList.Add("acc-account-mm-lst", typeof(FrmAccAccountLst));
            DicRouteList.Add("acc-account-period-mm-lst", typeof(FrmAccPeriodLst));
            DicRouteList.Add("acc-general-ledger-mm-lst", typeof(FrmAccGeneralLedgerLst));

        }
    }
}
