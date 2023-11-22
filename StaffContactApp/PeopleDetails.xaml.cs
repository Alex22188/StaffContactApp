namespace StaffContactApp;

public partial class PeopleDetails : ContentPage
{
	public PeopleDetails(People people)
	{
		InitializeComponent();
		BindingContext = people;
	}
}