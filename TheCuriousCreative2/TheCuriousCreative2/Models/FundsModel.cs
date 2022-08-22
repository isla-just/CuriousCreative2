using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Models
{
	public class FundsModel
	{
        //check if that is right
        [PrimaryKey, AutoIncrement]
        public int FundsId { get; set; }
        [MaxLength(50)]
        public string FundsTotal { get; set; }
        [MaxLength(50)]
        public string Salaries { get; set; }
        [MaxLength(50)]
        public string ClientIncome { get; set; }
        [MaxLength(50)]
        public string Expenses { get; set; }

    }
}

