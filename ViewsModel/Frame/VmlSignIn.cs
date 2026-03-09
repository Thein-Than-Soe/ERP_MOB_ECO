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
        private bool mIsEmailVerifyPage;
        public bool IsEmailVerifyPage
        {
            get
            {
                return mIsEmailVerifyPage;
            }
            set
            {
                mIsEmailVerifyPage = value;
                NotifyPropertyChanged("IsEmailActivatePage");
            }
        }
        private bool mIsPhoneVerifyPage;
        public bool IsPhoneVerifyPage
        {
            get
            {
                return mIsPhoneVerifyPage;
            }
            set
            {
                mIsPhoneVerifyPage = value;
                NotifyPropertyChanged("IsPhoneVerifyPage");
            }
        }
        #endregion

    }
}
