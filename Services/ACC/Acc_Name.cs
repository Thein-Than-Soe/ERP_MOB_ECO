using System;
namespace CS.ERP_MOB.Services.ACC
{
    public class Acc_Name
    {
        public Acc_Name()
        {
        }

        #region "List"

        public static string wsgetAccount = "getAccount";
        public static string wsgetAccountPeriod = "getAccountPeriod";
        public static string wsgetAccountTransaction = "getAccountTransaction";


        #endregion
        #region "Save"
        public static string wssaveAccount = "saveAccount";
        public static string wssaveAccountPeriod = "saveAccountPeriod";
        public static string wssaveAccountTransaction = "saveAccountTransaction";
        #endregion

        #region "Load"
        public static string wsloadAccount = "loadAccount";
        public static string wsloadAccountPeriod = "loadAccountPeriod";
        public static string wsloadAccountTransaction = "loadAccountTransaction";
        #endregion
    }
}
