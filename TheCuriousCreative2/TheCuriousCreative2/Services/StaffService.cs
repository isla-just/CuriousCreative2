using System;
using SQLite;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.Services
{
    //all our database functions and connection
    public class StaffService : IStaffService
    {

        private SQLiteAsyncConnection _dbConnection;

        //setup using path
        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Staff.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<StaffModel>();
            }
        }



        public async Task<List<StaffModel>> GetStaffList()
        {
            await SetUpDb();
            var staffList = await _dbConnection.Table<StaffModel>().ToListAsync();
            return staffList;
        }

        public async Task<int> AddStaff(StaffModel staffModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(staffModel);
        }

        public async Task<int> DeleteStaff(StaffModel staffModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(staffModel);
        }

        public async Task<int> UpdateStaff(StaffModel staffModel)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(staffModel);
        }


    }
}

