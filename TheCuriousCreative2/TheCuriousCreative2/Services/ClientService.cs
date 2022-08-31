using System;
using SQLite;
using TheCuriousCreative2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Services
{
    //all our database functions and connection
    public class ClientService : IClientService
    {

        private SQLiteAsyncConnection _dbConnection;

        //setup using path
        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Clients.db3");
                var options = new SQLiteConnectionString(dbPath, true, "password", postKeyAction: c =>
                {
                    c.Execute("PRAGMA cipher_compatability = 3");
                });
                _dbConnection = new SQLiteAsyncConnection(options);
                await _dbConnection.CreateTableAsync<ClientModel>();
            }
        }


        public async Task<int> AddClient(ClientModel clientModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(clientModel);
        }

        public async Task<int> DeleteClient(ClientModel clientModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(clientModel);
        }

        public async Task<List<ClientModel>> GetClientList()
        {
            await SetUpDb();
            var clientList = await _dbConnection.Table<ClientModel>().ToListAsync();
            return clientList;
        }

        public async Task<int> UpdateClient(ClientModel clientModel)
        {
            await SetUpDb();
            return await _dbConnection.UpdateAsync(clientModel);
        }

        //public async Task<int> GetCountClients()
        //{
        //    await SetUpDb();
        //    var clientCount = await _dbConnection.ExecuteScalarAsync<int>("select count(*) from Clients");
        //    return clientCount;
        //}

    }
}

