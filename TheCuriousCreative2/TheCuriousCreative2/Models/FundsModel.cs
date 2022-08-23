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
        public int FundsTotal { get; set; }
        [MaxLength(50)]
        public int Salaries { get; set; }
        [MaxLength(50)]
        public int ClientIncome { get; set; }
        [MaxLength(50)]
        public int Expenses { get; set; }

    }
}

