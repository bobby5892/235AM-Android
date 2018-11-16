using SQLite;
using System;

namespace DataAccess_Library
{
    // This model uses attributes for SQLite.Net
    [Table("Stocks")]
    public class Stock
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(8)]
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public decimal Close { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal AdjClose { get; set; }
        public int Volume { get; set; }
        //Date,Open,High,Low,Close,Adj Close,Volume
    }
}
