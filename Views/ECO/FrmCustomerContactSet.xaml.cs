using CS.ERP.PL.POS.DAT;
using CS.ERP.PL.SYS.DAT;
using CS.ERP_MOB.ViewsModel.Frame;
namespace CS.ERP_MOB_ECO.Views.ECO;

public partial class FrmCustomerContactSet : ContentPage
{
    private readonly VmlCheckOut _viewModel;
    private readonly RES_CUSTOMER_CONTACT _contact;


    public FrmCustomerContactSet(VmlCheckOut viewModel, RES_CUSTOMER_CONTACT contact)
	{
		InitializeComponent();
        _viewModel = viewModel;
        _contact = contact;

        BindingContext = _viewModel;

        // Edit mode
        if (_contact != null)
        {
            LoadContactData();
        }
    }
    private void LoadContactData()
    {
        Ent_ContactName.Text =  _contact.ContactName_0_255;

        var contactType = _viewModel.ContactTypeList?.FirstOrDefault(x => x.ContactTypeName_0_255 == _contact.ContactTypeName_0_255);

        Picker_ContactType.SelectedItem = contactType;

        var mobileCode = _viewModel.CountryList? .FirstOrDefault(x => x.Ask == _contact.CountryMobileCode_0_50);

        Picker_CountryMobileCode.SelectedItem = mobileCode;


        Ent_ContactMobilePhone.Text =  _contact.ContactMobilePhone;

        Ent_ContactAddress.Text = _contact.ContactAddress;

        var country = _viewModel.CountryList?
                .FirstOrDefault(c => c.CountryName_0_255 == _contact.ContactCountryName_0_255);

        _viewModel.SelectedCountry = country;
        var state = country?.RES_STATE_DTL?
            .FirstOrDefault(s => s.StateName_0_255 == _contact.ContactStateName_0_255);

        _viewModel.SelectedState = state;
        var city = state?.RES_CITY?
            .FirstOrDefault(ci => ci.CityName_0_255 == _contact.ContactCityName_0_255);

        _viewModel.SelectedCity = city;

    }
    private async void SaveContact_Clicked(  object sender,  EventArgs e)
         {
        try
        {
            List<string> errors = new();

            if (Picker_ContactType.SelectedItem == null)
                errors.Add("• Contact Type");

            if (string.IsNullOrWhiteSpace(Ent_ContactName.Text))
                errors.Add("• Contact Name");

            if (Picker_CountryMobileCode.SelectedItem == null)
                errors.Add("• Country Mobile Code");

            if (string.IsNullOrWhiteSpace(Ent_ContactMobilePhone.Text))
                errors.Add("• Contact Mobile Phone");

            if (PickerCountry.SelectedItem == null)
                errors.Add("• Country");

            if (PickerState.SelectedItem == null)
                errors.Add("• State");

            if (PickerCity.SelectedItem == null)
                errors.Add("• City");

            if (string.IsNullOrWhiteSpace(Ent_ContactAddress.Text))
                errors.Add("• Address");

            if (errors.Count > 0)
            {
                await DisplayAlert(
                    "Missing Information",
                    "Please complete the following fields:\n\n" +
                    string.Join("\n", errors),
                    "OK");

                return;
            }


            // Assign values from UI back into the model
            _contact.ContactAddress = Ent_ContactAddress.Text;
            _contact.ContactMobilePhone = Ent_ContactMobilePhone.Text;
            var l_SelectedContact = Picker_ContactType.SelectedItem as RES_CONTACT_TYPE;
            _contact.ContactTypeAsk = l_SelectedContact.Ask;
            var l_SelectedMobileCode = Picker_CountryMobileCode.SelectedItem as RES_COUNTRY_DTL;
            _contact.CountryMobileAsk = l_SelectedMobileCode.Ask;


            var l_SelectedCountry = PickerCountry.SelectedItem as RES_COUNTRY_DTL;
            _contact.ContactCountryAsk = l_SelectedCountry.Ask;
            _contact.ContactCountryName_0_255 = l_SelectedCountry.CountryName_0_255;

            var l_SelectedState = PickerState.SelectedItem as RES_STATE_DTL;
            _contact.ContactStateAsk = l_SelectedState.Ask;
            _contact.ContactStateName_0_255 = l_SelectedState.StateName_0_255;


            var l_SelectedCity = PickerCity.SelectedItem as RES_CITY;
            _contact.ContactCityAsk = l_SelectedCity.Ask;
            _contact.ContactCityName_0_255 = l_SelectedCity.CityName_0_255;


            // Save api
            _contact.StatusAsk = "1"; // Save key
            await _viewModel.saveCustomerContact(_contact);
        }
        catch (Exception ex)
        {
            await DisplayAlert( "Error", ex.Message,  "OK");
        }
    }

    private async void OnDeleteBtn_Clicked(object sender, EventArgs e)
    {
        _contact.StatusAsk = "6"; //delete key
        await _viewModel.saveCustomerContact(_contact);
        await Navigation.PopAsync();
    }
}