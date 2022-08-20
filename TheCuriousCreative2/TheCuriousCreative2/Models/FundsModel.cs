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
        [PrimaryKey]
        public static DateTime Now { get; }
        [MaxLength(250)]
        public double FundsTotal { get; set; }
        [MaxLength(250)]
        public double Salaries { get; set; }
        [MaxLength(250)]
        public double ClientIncome { get; set; }
        [MaxLength(250)]
        public double Expenses { get; set; }

    }
}

