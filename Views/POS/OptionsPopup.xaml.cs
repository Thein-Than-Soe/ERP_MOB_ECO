using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Maui.Views;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.General;
using Microsoft.Maui.Controls;

namespace CS.ERP_MOB.Views.POS
{

    public partial class OptionsPopup : Popup
    {
        public ObservableCollection<RES_CONTROL> ControlList { get; set; }


        public Action<RES_CONTROL> OnDeleteClicked { get; set; }
        public Action<RES_CONTROL> OnEditClicked { get; set; }
        public Action<RES_CONTROL> OnNewClicked { get; set; }
        public Action<RES_CONTROL> OnPrintClicked { get; set; }
        public Action<RES_CONTROL> OnSendMailClicked { get; set; }
        public Action<RES_CONTROL> OnExpPDFClicked { get; set; }
        public Action<RES_CONTROL> OnExpExcelClicked { get; set; }
        public Action<RES_CONTROL> OnExpCSVClicked { get; set; }
        public Action<RES_CONTROL> OnSummaryClicked { get; set; }
        public Action<RES_CONTROL> OnPostClicked { get; set; }
        public OptionsPopup()
        {
            InitializeComponent();
        }
        public OptionsPopup(List<RES_CONTROL> arg_RES_CONTROL)
        {
            InitializeComponent();
            ControlList = new ObservableCollection<RES_CONTROL>(arg_RES_CONTROL);
            BindingContext = this;
        }
        private void onControl_Clicked(object sender, TappedEventArgs e)
        {
            if (sender is Border btn && btn.BindingContext is RES_CONTROL l_RES_CONTROL)
            {

                switch (l_RES_CONTROL.link)
                {
                    case "btnNew_onClick":
                        btnNew_onClick(l_RES_CONTROL);
                        break;
                    case "btnEdit_onClick":
                        btnEdit_onClick(l_RES_CONTROL);
                        break;
                    case "btnDelete_onClick":
                        btnDelete_onClick(l_RES_CONTROL);
                        break;
                    case "btnPrint_onClick":
                        btnPrint_onClick(l_RES_CONTROL);
                        break;
                    case "btnSendMail_onClick":
                        btnSendMail_onClick(l_RES_CONTROL);
                        break;
                    case "btnExpPDF_onClick":
                        btnExpPDF_onClick(l_RES_CONTROL);
                        break;
                    case "btnExpExcel_onClick":
                        btnExpExcel_onClick(l_RES_CONTROL);
                        break;
                    case "btnExpCSV_onClick":
                        btnExpCSV_onClick(l_RES_CONTROL);
                        break;
                    case "btnPost_onClick":
                        btnPost_onClick(l_RES_CONTROL);
                        break;
                    case "btnSummary_onClick":
                        btnSummary_onClick(l_RES_CONTROL);
                        break;
                    default: break;

                }
            }
        }

        private void btnNew_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnNewClicked?.Invoke(argRES_CONTROL);
        }
        private void btnEdit_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnEditClicked?.Invoke(argRES_CONTROL);
        }
        private void btnDelete_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnDeleteClicked?.Invoke(argRES_CONTROL);
        }
        private void btnPrint_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnPrintClicked?.Invoke(argRES_CONTROL);
        }
        private void btnSendMail_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnSendMailClicked?.Invoke(argRES_CONTROL);
        }
        private void btnExpPDF_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnExpPDFClicked?.Invoke(argRES_CONTROL);
        }
        private void btnExpExcel_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnExpExcelClicked?.Invoke(argRES_CONTROL);
        }
        private void btnExpCSV_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnExpCSVClicked?.Invoke(argRES_CONTROL);
        }
        private void btnSummary_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnSummaryClicked?.Invoke(argRES_CONTROL);
        }
        private void btnPost_onClick(RES_CONTROL argRES_CONTROL)
        {
            var page = this.GetParentPage();
            Close();
            OnPostClicked?.Invoke(argRES_CONTROL);
        }
 }
}
public static class VisualTreeHelperExtensions
{
    public static Page GetParentPage(this Element element)
    {
        Element parent = element;
        while (parent != null && parent is not Page)
            parent = parent.Parent;
        return parent as Page;
    }
}
public class SortingItem : INotifyPropertyChanged
{
    public string label { get; set; }
    public string value { get; set; }
    public string btnicon { get; set; }

    private bool _showIcon;
    public bool ShowIcon
    {
        get => _showIcon;
        set
        {
            if (_showIcon != value)
            {
                _showIcon = value;
                OnPropertyChanged(nameof(ShowIcon));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}