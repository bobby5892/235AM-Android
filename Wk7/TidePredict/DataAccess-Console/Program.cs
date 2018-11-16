﻿// Demo of using SQLite-net ORM
// Brian Bird, 5/20/13
// Converted to an exercise starter and completed
// By Brian Bird 5/12/16

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
            // Console.WriteLine(currentDir);
            // Was string dbPath = currentDir + @"/../DataAccess-Android/Assets/stocks.db3";
            string dbPath = currentDir + @"/../../../../ListView/Assets/tide.db3";
            var db = new SQLiteConnection(dbPath);

            // Create a Stocks table
            db.DropTable<Tide>();
            if (db.CreateTable<Tide>() == 0)
            {
                // A table already exixts, delete any data it contains
                db.DeleteAll<Tide>();
            }
            AddStocksToDbFromXML(db, "Charleston", "9432780_annual.xml");
            AddStocksToDbFromXML(db, "Reedsport", "9433501_annual.xml");
            AddStocksToDbFromXML(db, "Florence", "9434032_annual.xml");
            
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
                        DateTime = Convert.ToDateTime(entry["date"]),
                        Time = entry["time"].ToString(),
                        //PredictionFt = entry["pred_in_ft"].ToString(),
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