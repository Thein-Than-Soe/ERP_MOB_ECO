using System;
namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlSignUp : BaseViewModel
    {

        private bool mIsSignupPage;
        public bool IsSignupPage
        {
            get
            {
                return mIsSignupPage;
            }
            set
            {
                mIsSignupPage = value;
                NotifyPropertyChanged("IsSignupPage");
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

        private bool mIsOtherServicePage;
        public bool IsOtherServicePage
        {
            get
            {
                return mIsOtherServicePage;
            }
            set
            {
                mIsOtherServicePage = value;
                NotifyPropertyChanged("IsOtherServicePage");
            }
        }

        private bool mIsChooseTypePage;
        public bool IsChooseTypePage
        {
            get
            {
                return mIsChooseTypePage;
            }
            set
            {
                mIsChooseTypePage = value;
                NotifyPropertyChanged("IsChooseTypePage");
            }
        }

        private bool mIsChoosePlanPage;
        public bool IsChoosePlanPage
        {
            get
            {
                return mIsChoosePlanPage;
            }
            set
            {
                mIsChoosePlanPage = value;
                NotifyPropertyChanged("IsChoosePlanPage");
            }
        }
    }
}
