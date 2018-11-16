using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DataAccess_Library
{
    [Table("Tides")]
    public class Tide
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Location { get; set; }
        public string Day { get; set; }
        public long DateTime { get; set; }
        public string Time { get; set; }
        public string PredictionFt { get; set; }
        /// <summary>
        ///  Either HIGH or LOW
        /// </summary>
        public string TideLevel { get; set; }
    }
}
