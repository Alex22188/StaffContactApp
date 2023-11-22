using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffContactApp
{
    public class DatabaseServiceCSV
    {
        string csvFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PeopleDB.csv");



        public DatabaseServiceCSV()
        {
            SetCSVFileAsync().Wait();

        }

        private async Task SetCSVFileAsync()
        {



            try
            {


                if (!File.Exists(csvFilePath))
                {
                    using (var writer = new StreamWriter(csvFilePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteField("Id");
                        csv.WriteField("Name");
                        csv.WriteField("Phone");
                        csv.WriteField("Department");
                        csv.WriteField("AddressStreet");
                        csv.WriteField("AddressCity");
                        csv.WriteField("AddressState");
                        csv.WriteField("AddressZIP");
                        csv.WriteField("AddressCountry");
                        csv.NextRecord();
                    }
                }

            }


            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up CSV file: {ex.Message}");
            }
        }

        #region C R U D Operations on CSV
        public async Task<List<People>> GetPeopleAsync()
        {
            List<People> peoples = new List<People>();

            try
            {
                if (File.Exists(csvFilePath))
                {
                    await Task.Run(() =>
                    {
                        using (var reader = new StreamReader(csvFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            peoples = csv.GetRecords<People>().ToList();
                        }
                    });
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }

            return peoples;

        }
        public async Task AddPeopleAsync(People people)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var writer = new StreamWriter(csvFilePath, true))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecord(people);
                        csv.NextRecord();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student to CSV file: {ex.Message}");
            }

        }
        public async Task UpdatePeopleAsync(People people)
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    var peoples = await GetPeopleAsync();
                    var existingPeople = peoples.FirstOrDefault(s => s.Id == people.Id);

                    if (existingPeople != null)
                    {
                        peoples.Remove(existingPeople);
                        peoples.Add(people);

                        using (var writer = new StreamWriter(csvFilePath, false))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(peoples);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student in CSV file: {ex.Message}");
            }
        }

        public async Task DeletePeopleAsync(People people)
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    var peoples = await GetPeopleAsync();
                    var existingPeople = peoples.FirstOrDefault(s => s.Id == people.Id);

                    if (existingPeople != null)
                    {
                        peoples.Remove(existingPeople);

                        using (var writer = new StreamWriter(csvFilePath, false))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(peoples);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student from CSV file: {ex.Message}");
            }
        }
        #endregion
    }
}





