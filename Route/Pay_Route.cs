using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.PAY;

namespace CS.ERP_MOB.Route
{
    public class Pay_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Pay_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("pay-employee-lst", typeof(FrmPayEmployeeSet));
          //  DicRouteList.Add("pay-employee-set", typeof(FrmPayEmployeeSet));
            DicRouteList.Add("pay-salary-lst", typeof(FrmPaySalaryLst));
            DicRouteList.Add("pay-schedule-lst", typeof(FrmPayScheduleLst));
            DicRouteList.Add("pay-data-lst", typeof(FrmPayDataLst));

            DicRouteList.Add("pay-employee-mm-lst", typeof(FrmPayEmployeeLst));
            DicRouteList.Add("pay-salary-mm-lst", typeof(FrmPaySalaryLst));
            DicRouteList.Add("pay-schedule-mm-lst", typeof(FrmPayScheduleLst));
            DicRouteList.Add("pay-data-mm-lst", typeof(FrmPayDataLst));

        }
    }
}
