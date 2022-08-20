using System;
using SQLite;
using TheCuriousCreative2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Services
{
	public class FundsService : IFundsService
	{
        private SQLiteAsyncConnection _dbConnection;

        //setup using path
        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Funds.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<FundsModel>();
            }
        }


        public async Task<int> AddFunds(FundsModel fundsModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(fundsModel);
        }

        public async Task<int> DeleteFunds(FundsModel fundsModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(fundsModel);
        }

        public async Task<List<FundsModel>> GetFundsList()
        {
            await SetUpDb();
            var fundsList = await _dbConnection.Table<FundsModel>().ToListAsync();
            return fundsList;
        }

        public async Task<int> UpdateFunds(FundsModel fundsModel)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(fundsModel);
        }
    }
}

