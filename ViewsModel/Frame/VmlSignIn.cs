using System;
using CS.ERP.PL.SYS.REQ;
namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlSignIn : BaseViewModel
    {
        #region "Declaration"
        private bool mIsSignInPage;
        public bool IsSignInPage
        {
            get
            {
                return mIsSignInPage;
            }
            set
            {
                mIsSignInPage = value;
                NotifyPropertyChanged("IsSignInPage");
            }
        }
        private bool mIsActivatePage;
        public bool IsActivatePage
        {
            get
            {
                return mIsActivatePage;
            }
            set
            {
                mIsActivatePage = value;
                NotifyPropertyChanged("IsActivatePage");
            }
        }
        #endregion
        
    }
}
