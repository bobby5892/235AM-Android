using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace DataAccess_Library
{
    [Table("Cities")]
    public class City
    {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }
            public string Location { get; set; }
            public double Longitude { get; set; }
            public double Latitude { get; set; }
    }
}
