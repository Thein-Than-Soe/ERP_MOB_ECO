using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.CHT;
namespace CS.ERP_MOB.Route
{
    public class Cht_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Cht_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));
            DicRouteList.Add("acc-account-lst", typeof(FrmChtChatLst));
           


        }
    }
}