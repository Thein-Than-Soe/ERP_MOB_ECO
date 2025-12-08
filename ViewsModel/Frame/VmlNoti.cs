using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using CS.ERP_MOB.Services.SYS;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Controls;
using FreshMvvm.Maui;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CS.ERP_MOB.ViewsModel.Frame
{
    public class VmlNoti : FreshBasePageModel
    {
        public ObservableCollection<NotiTypeModel> NotiTypeList { get; set; }
        public ObservableCollection<RES_NOTI_LST> FilteredNotifications { get; set; } =
       new ObservableCollection<RES_NOTI_LST>();

        private NotiTypeModel _activeTab;
        public NotiTypeModel ActiveTab
        {
            get => _activeTab;
            set
            {
                _activeTab = value;
                RaisePropertyChanged();
                FilterNotifications();
            }
        }
        public VmlNoti(Page currentPage)
        {
            CoreMethods = new PageModelCoreMethods(currentPage, this);
            NotiTypeList = new ObservableCollection<NotiTypeModel>();
            FilteredNotifications = new ObservableCollection<RES_NOTI_LST>();

            NotiTypeList = new ObservableCollection<NotiTypeModel>(Common.mCommon.NotiList
                      .GroupBy(n => n.NotiTypeAsk)
                      .Select(g =>
                      {
                          var first = g.First();
                          var unreadCount = g.Count(n => n.StatusAsk == "0");

                          return new NotiTypeModel
                          {
                              NotiTypeAsk = first.NotiTypeAsk,
                              NotiTypeName_0_255 = first.NotiTypeName_0_255,
                              StatusAsk = first.StatusAsk,
                              unreadCount = unreadCount
                          };
                      })
             );
            ActiveTab = NotiTypeList.FirstOrDefault();
        }

        public ICommand SelectTabCommand => new Command<NotiTypeModel>((item) =>
        {
            ActiveTab = item;
        }); 
        private void FilterNotifications()
        {
            if (ActiveTab == null) return;

            FilteredNotifications.Clear();

            var list = Common.mCommon.NotiList
                .Where(x => x.NotiTypeName_0_255 == ActiveTab.NotiTypeName_0_255)
                .ToList();

            foreach (var n in list)
                FilteredNotifications.Add(n);
        }
    }
    public class NotiTypeModel
    {
        public string NotiTypeAsk { get; set; }
        public string NotiTypeName_0_255 { get; set; }
        public string StatusAsk { get; set; }
        public int unreadCount { get; set; }
    }
}
