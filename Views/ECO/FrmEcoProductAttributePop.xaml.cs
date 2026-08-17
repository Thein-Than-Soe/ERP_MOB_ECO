using CS.ERP.PL.POS.DAT;
using CS.ERP_MOB.ViewsModel.ECO;
using System.Collections.ObjectModel;

namespace CS.ERP_MOB_ECO.Views.ECO;

public partial class FrmEcoProductAttributePop : ContentPage
{
    private readonly VmlProductDtlLst _detailViewModel;

    private readonly Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>
        _selectedAttributeTerms;

    public ObservableCollection<DAT_STOCK_ATTRIBUTE> Attributes { get; }

    public FrmEcoProductAttributePop(
        ObservableCollection<DAT_STOCK_ATTRIBUTE> attributes,
        Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM> selectedAttributeTerms,
        VmlProductDtlLst detailViewModel)
    {
        InitializeComponent();

        Attributes = attributes;

        // This is a COPY of the current selection.
        // So changing values in the popup doesn't immediately
        // change the main page.
        _selectedAttributeTerms =
            new Dictionary<string, DAT_STOCK_ATTRIBUTE_TERM>(
                selectedAttributeTerms);

        _detailViewModel = detailViewModel;

        BindingContext = this;
    }


    // =========================================================
    // SET CURRENT SELECTED VALUE IN EACH PICKER
    // =========================================================

    private void AttributePicker_Loaded(object sender, EventArgs e)
    {
        if (sender is not Picker picker)
            return;

        if (picker.BindingContext is not DAT_STOCK_ATTRIBUTE attribute)
            return;

        if (_selectedAttributeTerms.TryGetValue(
                attribute.AttributeName_0_255,
                out var selectedTerm))
        {
            picker.SelectedItem = selectedTerm;
        }
    }


    // =========================================================
    // USER CHANGED AN ATTRIBUTE
    // =========================================================

    private void AttributePicker_SelectedIndexChanged(
        object sender,
        EventArgs e)
    {
        if (sender is not Picker picker)
            return;

        if (picker.SelectedItem is not DAT_STOCK_ATTRIBUTE_TERM selectedTerm)
            return;

        if (picker.BindingContext is not DAT_STOCK_ATTRIBUTE attribute)
            return;

        // Save the selected value into temporary dictionary
        _selectedAttributeTerms[
            attribute.AttributeName_0_255] = selectedTerm;
    }


    // =========================================================
    // APPLY
    // =========================================================

    private async void Apply_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            // -------------------------------------------------
            // Copy popup selections back to main ViewModel
            // -------------------------------------------------

            _detailViewModel.SelectedAttributeTerms.Clear();

            foreach (var item in _selectedAttributeTerms)
            {
                _detailViewModel.SelectedAttributeTerms[
                    item.Key] = item.Value;
            }


            // -------------------------------------------------
            // Update selected attribute display
            // -------------------------------------------------

            _detailViewModel.SelectedAttributeDisplay.Clear();

            foreach (var term in
                     _detailViewModel.SelectedAttributeTerms.Values)
            {
                _detailViewModel.SelectedAttributeDisplay.Add(term);
            }


            // -------------------------------------------------
            // Find matching variation
            // -------------------------------------------------

            _detailViewModel.UpdateSelectedVariation();


            // -------------------------------------------------
            // Close popup
            // -------------------------------------------------

            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.ToString(),
                "OK");
        }
    }
}