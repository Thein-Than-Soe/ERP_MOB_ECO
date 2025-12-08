using System;
namespace CS.ERP_MOB.Services.PAY
{
    public class Pay_Name
    {
        public Pay_Name()
        {
        }

        #region "List"

        public static string wsgetPayData = "getPayData";
        public static string wsgetEmployeePayData = "getEmployeePayData";
        public static string wsgetPayDataSSB = "getPayDataSSB";
        public static string wsgetPayDataGPF = "getPayDataGPF";
        public static string wsgetPayDataIncomeTax = "getPayDataIncomeTax";
        public static string wsgetPayDataProcess = "PayDataProcess";
        public static string wsgetPayDataRevert = "PayDataRevert";
        public static string wsgetEmployee = "getEmployeePay";
        public static string wsgetEmployeeRO = "getEmployeeRO";
        public static string wsgetEmployeeSalary = "getEmployeeSalary";
        public static string wsgetPaySchedule = "getPaySchedule";
        public static string wsgetScheduleType = "getPayScheduleType";
        public static string wsgetScheduleMethod = "getPayScheduleMethod";
       

        #endregion
        #region "Save"
        public static string wssavePayData = "savePayData";
        public static string wssaveEmployee = "saveEmployeePay";
        public static string wsconfirmEmployee = "confirmEmployee";
        public static string wssaveEmployeeUser = "saveEmployeeUser";
        public static string wsconfirmSalary = "confirmSalary";
        public static string wssavePaySchedule = "savePaySchedule";
        public static string wssaveScheduleType = "savePayScheduleType";
        public static string wssaveScheduleMethod = "savePayScheduleMethod";       

        #endregion
        #region "Load"

        public static string wsloadPayData = "loadPayData";
        public static string wsloadEmployee = "loadEmployeePay";
        public static string wsloadPaySchedule = "loadPaySchedule";
        
        #endregion

    }
}
