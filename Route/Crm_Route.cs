using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.CRM;

namespace CS.ERP_MOB.Route
{
    public class Crm_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Crm_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));


            DicRouteList.Add("crm-customer-lst", typeof(FrmCrmCustomerLst));
            DicRouteList.Add("crm-project-lst", typeof(FrmCrmProjectLst));
            DicRouteList.Add("crm-ticket-log-lst", typeof(FrmCrmTicketLogLst));
            DicRouteList.Add("crm-ticket-log-set", typeof(FrmCrmTicketLogSet));
            DicRouteList.Add("crm-evaluation-period-lst", typeof(FrmCrmEvaluationPeriodLst));
            DicRouteList.Add("crm-evaluation-period-set", typeof(FrmCrmEvaluationPeriodSet));
            DicRouteList.Add("crm-evaluation-summary-lst", typeof(FrmCrmEvaluationSummaryLst));
            DicRouteList.Add("crm-customer-support-lst", typeof(FrmCrmCustomerSupportLst));
            DicRouteList.Add("crm-user-guide-lst", typeof(FrmCrmUserGuideLst));
            DicRouteList.Add("crm-release-lst", typeof(FrmCrmReleaseLst));


            DicRouteList.Add("crm-customer-mm-lst", typeof(FrmCrmCustomerLst));
            DicRouteList.Add("crm-project-mm-lst", typeof(FrmCrmProjectLst));
            DicRouteList.Add("crm-ticket-log-mm-lst", typeof(FrmCrmTicketLogLst));
            DicRouteList.Add("crm-evaluation-period-mm-lst", typeof(FrmCrmEvaluationPeriodLst));
            DicRouteList.Add("crm-evaluation-summary-mm-lst", typeof(FrmCrmEvaluationSummaryLst));
            DicRouteList.Add("crm-customer-support-mm-lst", typeof(FrmCrmCustomerSupportLst));
            DicRouteList.Add("crm-user-guide-mm-lst", typeof(FrmCrmUserGuideLst));
            DicRouteList.Add("crm-release-mm-lst", typeof(FrmCrmReleaseLst));
        }
    }
}
