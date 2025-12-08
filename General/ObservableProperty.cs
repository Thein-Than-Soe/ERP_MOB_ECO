using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CS.ERP.PL.SYS.DAT;

namespace CS.ERP_MOB.General
{
    public class ObservableProperty: ObservableCollection<RES_MENU>, INotifyPropertyChanged
    {
        private bool _expanded;

        public string Text { get; set; }

        public List<RES_CONTROL> button { get; set; }
        public bool Expanded
        {
            get { return _expanded; }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged("Expanded");
                    OnPropertyChanged("StateIcon");
                }
            }
        }

        private string _StateIcon;
        public string StateIcon
        {
            //get { return Expanded ? "\uf063" : "\uf061"; }
            get { return _StateIcon; }
            set
            {
                _StateIcon = value;
                OnPropertyChanged("StateIcon");
            }
        }

        private string _MenuUrl;
        public string MenuUrl
        {
            get { return _MenuUrl; }
            set
            {
                _MenuUrl = value;
                OnPropertyChanged("MenuUrl");
            }
        }
        private string _SubMenuWidth;
        public string SubMenuWidth
        {
            get { return _SubMenuWidth; }
            set
            {
                _SubMenuWidth = value;
                OnPropertyChanged("SubMenuWidth");
            }
        }

        public int MenuCount { get; set; }

        public ObservableProperty()
        {
            Text = "";
            Expanded = false;
            //right arrow state icon
            StateIcon = "\uf105";
            MenuUrl = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}