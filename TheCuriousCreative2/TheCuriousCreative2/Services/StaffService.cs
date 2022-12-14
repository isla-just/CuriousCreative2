using System;
using System.Diagnostics;
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
                var options = new SQLiteConnectionString(dbPath, true, "password", postKeyAction: c =>
                {
                    c.Execute("PRAGMA cipher_compatability = 3");
                });
                _dbConnection = new SQLiteAsyncConnection(options);
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


        //Authentivation and verification for login
        public async Task<bool> AdminStaffLoginAuth(string userName, string password)
        {
            try
            {
                await SetUpDb();
                var staffList = await _dbConnection.Table<StaffModel>().ToListAsync();
                var successFind = staffList.Where(y => y.StaffName == userName && y.StaffPassword == password && y.Role == "Admin").FirstOrDefault();

                if (successFind != null)
                {
                    Debug.WriteLine("Staff member Found");
                    return true;
                }
                else
                {
                    Debug.WriteLine("Staff member not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }


    }
}
