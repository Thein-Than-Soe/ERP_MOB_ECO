using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlDiscussion : FreshBasePageModel
    {
        public List<RES_DISCUSSION> mDiscussionList;

        public List<RES_DISCUSSION> DiscussionList
        {
            get { return mDiscussionList; }
            set { mDiscussionList = value; RaisePropertyChanged("DiscussionList"); }
        }
        public VmlDiscussion(Page currentPage)
        {
            CoreMethods = new PageModelCoreMethods(currentPage, this);
            DiscussionList = Common.mCommon.DiscussionList;

        }
    }
}
