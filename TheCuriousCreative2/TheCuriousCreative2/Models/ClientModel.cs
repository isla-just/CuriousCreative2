using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheCuriousCreative2.Models
{
	public class ClientModel
	{
        [PrimaryKey, AutoIncrement]
        public int ClientID { get; set; }
        [MaxLength(250)]
        public string ClientName { get; set; }
        [MaxLength(250)]
        public string ClientNotes { get; set; }
        [MaxLength(250)]
        public bool Priority { get; set; } = false;

        //change to handle image upload
        //[MaxLength(250)]
        //public string ClientImage { get; set; }
    }

}


