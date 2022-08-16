using System;
using SQLite;

namespace TheCuriousCreative2.Models
{

    [Table("projects")]
    public class ProjectModel
    {
        [PrimaryKey, AutoIncrement]
        public int ProjectID { get; set; }

        [MaxLength(100)]
        public string ProjectName { get; set; }

        [MaxLength(100)]
        public string Image { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        [MaxLength(100)]
        public string Client { get; set; }

        [MaxLength(10)]
        public int Deposit { get; set; }

        [MaxLength(10)]
        public int DepositPaid { get; set; }

        [MaxLength(10)]
        public int PricePerMonth { get; set; }

        [MaxLength(100)]
        public string TeamID { get; set; }

        [MaxLength(100)]
        public bool Priority { get; set; } = false;

    }
}
