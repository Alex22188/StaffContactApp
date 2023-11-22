using System;

public class DatabaseService
{
    public class DatabaseService
    {


        private SQLiteAsyncConnection _database;



        public DatabaseService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Staff_Contact.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<People>().Wait();
        }

        #region C R U D Operations
        public async Task AddPeopleAsync(People people)
        {


            await _database.InsertAsync(people);
        }

        public async Task<List<People>> GetPeopleAsync()
        {
            return await _database.Table<People>().ToListAsync();
        }
        public async Task UpdatePeopleAsync(People people)
        {
            await _database.UpdateAsync(people);
        }

        public async Task DeletePeopleAsync(People people)
        {
            await _database.DeleteAsync(people);
        }
        #endregion

    }
}

	
	}
}
