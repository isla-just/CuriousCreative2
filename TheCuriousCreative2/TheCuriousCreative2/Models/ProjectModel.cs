﻿using System;
using SQLite;

namespace TheCuriousCreative2.Models
{

    public class ProjectModel
    {

        [PrimaryKey, AutoIncrement]
        public int ProjectID { get; set; }
        [MaxLength(250)]
        public string ProjectImage { get; set; }
        [MaxLength(250)]
        public string ProjectName { get; set; }
        [MaxLength(250)]
        public string Client { get; set; }
        [MaxLength(250)]
        public string Status { get; set; }
        [MaxLength(250)]
        public string DesignTeam { get; set; }
        [MaxLength(10)]
        public int Deposit { get; set; }
        [MaxLength(250)]
        public string DepositPaid { get; set; }
        [MaxLength(10)]
        public int PricePerMonth { get; set; }
        [MaxLength(10)]
        public int MaxEmployeeHours { get; set; }
        [MaxLength(10)]
        public int MaxClientHours { get; set; }
    }
}
