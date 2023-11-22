using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;


namespace StaffContactApp
{


    public partial class MainPage : ContentPage
    {

        private DatabaseServiceCSV _databaseServiceCSV;

        private List<People> _people;

        public MainPage()
        {


            InitializeComponent();

            _databaseServiceCSV = new DatabaseServiceCSV();



            LoadPeopleAsync();
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load students from the database every time the page appears
            LoadPeopleAsync();
        }

        private async void UpdatePeople_Clicked(object sender, EventArgs e)
        {
            var selectedPeople = (People)((Button)sender).BindingContext;
            //Added CSV -> sends both database services 
            //await Navigation.PushAsync(new UpdateStudent(selectedStudent, _databaseService, _databaseServiceCSV));

            await Navigation.PushAsync(new UpdatePeople(selectedPeople, _databaseServiceCSV));
        }

        private async void DeletePeople_Clicked(object sender, EventArgs e)
        {
            var selectedPeople = (People)((Button)sender).BindingContext;
            bool result = await DisplayAlert("Delete Student", "Are you sure you want to delete this student?", "Yes", "No");

            if (result)
            {


                await _databaseServiceCSV.DeletePeopleAsync(selectedPeople);


                LoadPeopleAsync();
            }
        }

        private async void ViewPeopleDetails_Clicked(object sender, EventArgs e)
        {
            var selectedPeople = (People)((Button)sender).BindingContext;
            await Navigation.PushAsync(new PeopleDetails(selectedPeople));
        }

        private async void LoadPeopleAsync()
        {
            try
            {

                _people = await _databaseServiceCSV.GetPeopleAsync();

                PeopleListView.ItemsSource = _people;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        // Event handler for adding a new student
        //Visual bug, Consider moving to new Add Student page.
        private async void AddPeople_Clicked(object sender, EventArgs e)
        {
            var newPeople = new People
            {
                Id = int.Parse(IdEntry.Text),
                Name = NameEntry.Text,
                Phone = int.Parse(PhoneEntry.Text),
                Department = int.Parse(DepartmentEntry.Text),
                AddressStreet = AddressStreetEntry.Text,
                AddressCity = AddressCityEntry.Text,
                AddressState = AddressStateEntry.Text,
                AddressZIP = int.Parse(AddressZIPEntry.Text),
                AddressCountry = AddressCountryEntry.Text


            };

        
             

            await _databaseServiceCSV.AddPeopleAsync(newPeople);

           

            IdEntry.Text = NameEntry.Text = PhoneEntry.Text = DepartmentEntry.Text = AddressStreetEntry.Text = AddressCityEntry.Text = AddressStateEntry.Text = AddressZIPEntry.Text = AddressCountryEntry.Text = string.Empty;
            LoadPeopleAsync();
            //Add UI refresh command here
        
            
            
        
        
        
        
        
        }
    }
}




