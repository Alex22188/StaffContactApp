namespace StaffContactApp;

public partial class UpdatePeople : ContentPage
{
    private People _selectedPeople;

    private DatabaseServiceCSV _databaseServiceCSV;

    public UpdatePeople(People selectedPeople, DatabaseServiceCSV databaseServiceCSV)
    {
        InitializeComponent();

        _selectedPeople = selectedPeople;

        IdEntry.Text = _selectedPeople.Id.ToString();
        NameEntry.Text = _selectedPeople.Name;
        PhoneEntry.Text = _selectedPeople.Phone.ToString();
        DepartmentEntry.Text = _selectedPeople.Department.ToString();
        AddressStreetEntry.Text = _selectedPeople.AddressStreet;
        AddressCityEntry.Text = _selectedPeople.AddressCity;
        AddressStateEntry.Text = _selectedPeople.AddressState;
        AddressZIPEntry.Text = _selectedPeople.AddressZIP.ToString();


        _databaseServiceCSV = databaseServiceCSV;


    }

    private async void UpdatePeople_Clicked(object sender, EventArgs e)
    {
        _selectedPeople.Id = Convert.ToInt32(IdEntry.Text);
        _selectedPeople.Name = NameEntry.Text;
        _selectedPeople.Phone = Convert.ToInt32(PhoneEntry);
        _selectedPeople.AddressStreet = AddressCityEntry.Text;
        _selectedPeople.AddressCity = AddressStreetEntry.Text;
        _selectedPeople.AddressState = AddressStateEntry.Text;
        _selectedPeople.AddressZIP = Convert.ToInt32(AddressZIPEntry);
        _selectedPeople.AddressCountry = AddressCountryEntry.Text;



        await _databaseServiceCSV.UpdatePeopleAsync(_selectedPeople);



        await Navigation.PopAsync();
    }
}

    


