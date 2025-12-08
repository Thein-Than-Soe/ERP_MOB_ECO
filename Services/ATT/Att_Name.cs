using System;
namespace CS.ERP_MOB.Services.ATT
{
    public class Att_Name
    {
        public Att_Name()
        {
        }
        #region "List"

        public static string wsgetEmployeeInOut = "getEmployeeInOut";
        public static string wsgetEmployeeDtl = "getEmployeeDtl";
        public static string wsgetEmployeeRoaster = "getEmployeeRoasterJun";
        public static string wsgetRoaster = "getEmployeeRoaster";

        public static string wsgetOnDuty = "getOnDuty";
        public static string wsgetEmployee = "getEmployeeAttendance";

        public static string wsgetEmployeeLateRequest = "getEmployeeLateRequest";
        public static string wsgetEmployeeEarlyOutRequest = "getEmployeeEarlyOutRequest";
        public static string wsgetEmployeeOTRequest = "getEmployeeOTRequest";
        public static string wsgetEmployeeAbsentRequest = "getEmployeeAbsentRequest";
        public static string wsgetEmployeeLeaveTaken = "getEmployeeLeaveTaken";


        #endregion
        #region "Save"
        public static string wssaveEmployeeInOut = "saveEmployeeInOut";
        public static string wssaveEmployeeInOutTMF = "saveEmployeeInOutTMF";
        public static string wssaveEmployeeRoaster = "saveEmployeeRoasterJun";
        public static string wssaveEmployeeDtl = "saveEmployeeDtl";       
        public static string wssaveOnDuty = "saveOnDuty";
        public static string wssaveOT = "saveEmployeeOTRequest";
        public static string wssaveEmployee = "saveEmployeeAttendance";
        public static string wssaveEmployeeAbsentRequest = "saveEmployeeAbsentRequest";
        public static string wssaveEmployeeEarlyOutRequest = "saveEmployeeEarlyOutRequest";
        public static string wssaveEmployeeLateRequest = "saveEmployeeLateRequest";

        
        #endregion
        #region "Load"

        public static string wsLoadAttendanceInOut = "loadAttendanceInOut";
        public static string wsLoadOnDuty = "loadOnDuty";
        public static string wsloadEmployeeRoaster = "loadEmployeeRoasterJun";
        public static string wsloadEmployeeShift = "loadEmployeeShiftJun";
        public static string wsloadEmployee = "loadEmployee";
        public static string wsloadEmployeeRequest = "loadEmployeeRequest";
        public static string wsloadAbsentReq = "loadEmployeeRequest";
        //public static string wsloadEmployeeShift = "loadEmployeeShiftJun";
        //public static string wsloadEmployee = "loadEmployee";

        #endregion
    }
}
