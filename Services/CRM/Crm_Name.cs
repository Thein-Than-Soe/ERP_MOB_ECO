using System;
namespace CS.ERP_MOB.Services.CRM
{
    public class Crm_Name
    {
        public Crm_Name()
        {
        }
        #region "List"

        public static string wsgetCustomerAndContact = "getCustomerNContact";

        public static string wsgetCustomerSupport = "getCustomerSupport";
        public static string wsgetEvaluationPeriod = "getEvaluationPeriod";
        public static string wsgetProjectJun = "getProjectJun";
       // public static string wsgetProject = "getProject";
        public static string wsgetTicketLog = "getTicketLog";        
        public static string wsgetTicketSummary = "getSummaryDetail";
        public static string wsgetManualContent = "getManualContent";
        public static string wsgetSummaryDetail = "getSummaryDetail";


        #endregion
        #region "Save"

        public static string wsSaveCustomer = "saveCustomerSupport";
        public static string wsSaveCustomerSupport = "saveAccess";
        public static string wssaveEvaluationPeriod = "saveEvaluationPeriod";
        public static string wssaveProjectJun = "saveProjectJun";
        //public static string wssaveProject = "saveProject";
        public static string wssaveTicketLog = "saveTicketLog";
        public static string wssaveTicketSummary = "saveTicketSummary";
        public static string wssaveManualContent = "saveManualContent";
       
        #endregion
        #region "Load"
        public static string wsLoadCustomer = "loadCustomer";
        public static string wsLoadCustomerSupport = "loadCustomerSupport";
        public static string wsloadEvaluationPeriod = "loadEvaluationPeriod";
        public static string wsloadProjectJun = "loadProjectJun";
      //  public static string wsloadProject = "loadProject";
        public static string wsloadTicketLog = "loadTicketLog";
        public static string wsloadTicketSummary = "loadTicketSummary";
        public static string wsloadManualContent = "loadManualContent";
       
        #endregion

    }
}
