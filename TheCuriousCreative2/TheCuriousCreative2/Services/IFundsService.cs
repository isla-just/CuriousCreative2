using System;
using TheCuriousCreative2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Services
{
	public interface IFundsService
	{
        Task<List<FundsModel>> GetFundsList();
        Task<int> AddFunds(FundsModel fundsModel);
        Task<int> DeleteFunds(FundsModel fundsModel);
        Task<int> UpdateFunds(FundsModel fundsModel);
    
    }
}

