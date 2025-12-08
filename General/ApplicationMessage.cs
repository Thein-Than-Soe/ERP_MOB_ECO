using System;
namespace CS.ERP_MOB.General
{
    public  class ApplicationMessage
    {
        public static ApplicationMessage Message = new ApplicationMessage();
        public string Alert = "Alert";


        private string mWelcome = "You are welcome! ";
        public string Welcome { get { return mWelcome; } set { mWelcome = value; } }

        private string mLoadSuccess = "Successfully Loaded! ";
        public string LoadSuccess { get { return mLoadSuccess; } set { mLoadSuccess = value; } }

        private string mSaveSuccess = "Successfully Saved! ";
        public string SaveSuccess { get { return mSaveSuccess; } set { mSaveSuccess = value; } }

        private string mDeleteSuccess = "Successfully Deleted! ";
        public string DeleteSuccess { get { return mDeleteSuccess; } set { mDeleteSuccess = value; } }

        private string mNoData = "There is no data to display! ";
        public string NoData { get { return mNoData; } set { mNoData = value; } }

        private string mWebServiceErr = "Web Service Err ";
        public string WebServiceErr { get { return mWebServiceErr; } set { mWebServiceErr = value; } }

        private string mMenuAccessRight="No Access Right";
        public string MenuAccessRight { get { return mMenuAccessRight; } set { mMenuAccessRight = value; } }

        private string mMandatoryField = "(*) Fields can't be blaked";
        public string MandatoryField { get { return mMandatoryField; } set { mMandatoryField = value; } }
    }
}
