using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.SYS;

namespace CS.ERP_MOB.Route
{
    public class Wms_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Wms_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));

            //DicRouteList.Add("access-set", typeof(AccessSetPage));
            //DicRouteList.Add("access-lst", typeof(AccessListPage));
        }
    }
}
