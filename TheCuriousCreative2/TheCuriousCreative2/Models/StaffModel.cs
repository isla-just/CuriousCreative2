using System;
using SQLite;

namespace TheCuriousCreative2.Models
{
    public class StaffModel
    {
        [PrimaryKey, AutoIncrement]
        public int StaffID { get; set; }
        //[MaxLength(250)]
        //public string StaffImage { get; set; }
        [MaxLength(250)]
        public string StaffPassword { get; set; }
        [MaxLength(250)]
        public string StaffName { get; set; }
        [MaxLength(250)]
        public string Role { get; set; }
        [MaxLength(250)]
        public string Nickname { get; set; }
        [MaxLength(250)]
        public string DesignTeam { get; set; }
        [MaxLength(10)]
        public int Salary { get; set; }
        [MaxLength(250)]
        public int HoursWorked { get; set; }
        [MaxLength(10)]
        public int MaxHours { get; set; }
        [MaxLength(250)]
        public string Birthday { get; set; }
        [MaxLength(250)]
        public string CurrentProject { get; set; }
    }
}

