
using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using DataAccess_Library;

namespace DataAccess_Console
{
    class MainClass
    {
        static string currentDir;
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello Tide Data!");

            // We're using a db file in the Android project's Assets folder
            currentDir = Directory.GetCurrentDirectory ();
           
            string dbPath = currentDir + @"/../../../../Android/Assets/tide.db3";
            var db = new SQLiteConnection(dbPath);

            // Create a Stocks table
            db.DropTable<Tide>();
            db.DropTable<City>();
            if(db.CreateTable<City>() == 0)
            {
                db.DeleteAll<City>();
            }
            if (db.CreateTable<Tide>() == 0)
            {
                // A table already exixts, delete any data it contains
                db.DeleteAll<Tide>();
            }
           // AddStocksToDbFromXML(db, "Charleston", "9432780_annual.xml");
          //  AddStocksToDbFromXML(db, "Reedsport", "9433501_annual.xml");
          //  AddStocksToDbFromXML(db, "Florence", "9434032_annual.xml");
            AddCityToDb(db, "Charleston",  43.3401, 124.3301,0);
            AddCityToDb(db, "Reedsport", 43.7023, 124.0968,1);
            AddCityToDb(db, "Florence", 43.9826, 124.0998,2);
        }
        private static void AddCityToDb(SQLiteConnection db, string city, double longitude,double latitude,int key)
        {
            var thing = db.Insert(new City()
            {
                ID = key,
                Location = city,
                Longitude = longitude,
                Latitude = latitude
            });
        }
        private static void AddStocksToDbFromXML(SQLiteConnection db, string name, string file)
        {
            // Create Stream
            using( FileStream fs = File.OpenRead("../../../XMLFiles/" + file)){
                // Pass Stream to Reader
                var reader = new XmlTideFileParser(fs);
                /// Get Data from Stream
                var dataList = reader.TideList;
                // Primary Key - Auto Increment
                int pk = 0;
                // Map Data to class
                foreach(IDictionary<string,object> entry in dataList)
                {
                    pk += db.Insert(new Tide()
                    {
                       // ID = pk,
                        Location = name,
                        Day = entry["day"].ToString(),
                        DateTime = Convert.ToDateTime(entry["date"]).Ticks,
                        Time = entry["time"].ToString(),
                        PredictionFt = entry["pred_in_ft"].ToString(),
                        TideLevel = entry["highlow"].ToString()
       
                    });
                    if (pk % 100 == 0)
                        Console.WriteLine("{0} {1} rows inserted", pk, name);
                }
                // Show the final count of rows inserted
                Console.WriteLine("{0} {1} rows inserted", pk, name);
            }
           

        }
     
    }
}
