using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.ATT;

namespace CS.ERP_MOB.Route
{
    public class Att_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Att_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("att-employee-lst", typeof(FrmATTEmployeeLst));
            DicRouteList.Add("att-employee-schedule-lst", typeof(FrmATTEmployeeScheduleLst));
            DicRouteList.Add("att-employee-inout-lst", typeof(FrmATTEmployeeInOutLst));
            DicRouteList.Add("att-onduty-lst", typeof(FrmATTOndutyLst));
            DicRouteList.Add("att-employee-inout-set", typeof(FrmATTEmployeeInOutSet));
            DicRouteList.Add("att-onduty-set", typeof(FrmATTOndutySet));
            DicRouteList.Add("att-late-request-lst", typeof(FrmATTLateRequestLst));
            DicRouteList.Add("att-early-out-request-lst", typeof(FrmATTEarlyOutRequestLst));
            DicRouteList.Add("att-late-request-set", typeof(FrmATTLateRequestSet));
            DicRouteList.Add("att-early-out-request-set", typeof(FrmATTEarlyOutRequestSet));

            DicRouteList.Add("att-leave-request-lst", typeof(FrmATTLeaveRequestLst));
           DicRouteList.Add("att-absent-request-lst", typeof(FrmATTAbsentLst));
           DicRouteList.Add("att-ot-request-lst", typeof(FrmATTOTRequestLst));
            DicRouteList.Add("att-absent-request-set", typeof(FrmATTAbsentSet));
            DicRouteList.Add("att-ot-request-set", typeof(FrmATTOTRequestSet));


            DicRouteList.Add("att-employee-mm-lst", typeof(FrmATTEmployeeLst));
            DicRouteList.Add("att-employee-schedule-mm-lst", typeof(FrmATTEmployeeScheduleLst));
            DicRouteList.Add("att-employee-inout-mm-lst", typeof(FrmATTEmployeeInOutLst));
            DicRouteList.Add("att-onduty-mm-lst", typeof(FrmATTOndutyLst));

            DicRouteList.Add("att-late-request-mm-lst", typeof(FrmATTLateRequestLst));
            DicRouteList.Add("att-early-out-request-mm-lst", typeof(FrmATTEarlyOutRequestLst));
            DicRouteList.Add("att-leave-request-mm-lst", typeof(FrmATTLeaveRequestLst));
            DicRouteList.Add("att-absent-request-mm-lst", typeof(FrmATTAbsentLst));
            DicRouteList.Add("att-ot-request-mm-lst", typeof(FrmATTOTRequestLst));


        }
    }
}
