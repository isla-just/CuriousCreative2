using System;
using TheCuriousCreative2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Services
{
    //service to be used to create our database data

    public interface IClientService
    {
        Task<List<ClientModel>> GetClientList();
        Task<int> AddClient(ClientModel clientModel);
        Task<int> DeleteClient(ClientModel clientModel);
        Task<int> UpdateClient(ClientModel clientModel);
    }
}

