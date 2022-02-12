using nguyenXuanHinh_lap3.Entity;
using SQLitePCL;
using System;
using System.Collections.Generic;

namespace nguyenXuanHinh_lap3.DB
{
    class DatabaseInitialize
    {
        public Double totalMoney;
        private static SQLiteConnection conn = new SQLiteConnection("lab3.db");
        public static bool CreateTable()
        {
            var sql = @"CREATE TABLE IF NOT EXISTS PersonalTransactions(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Name VARCHAR(255), Description TEXT, Money DOUBLE, CreatedDate DATE, Category INTEGER);";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public bool InsertData(PersonalTransaction request)
        {
            var sql = $"INSERT INTO PersonalTransactions (Name, Description, Money, CreatedDate, Category) VALUES ('{request.Name}', '{request.Description}', {request.Money}, '{request.CreatedDate}', {request.Category})";
            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
            return true;
        }

        public List<PersonalTransaction> ListData()
        {
            totalMoney = 0;
            var personalTransactions = new List<PersonalTransaction>();
            var sql = "SELECT * FROM PersonalTransactions";
            using (var statement = conn.Prepare(sql))
            {
                while (statement.Step() == SQLiteResult.ROW)
                {
                    var name = (string)statement["Name"];
                    var description = (string)statement["Description"];
                    var money = (double)statement["Money"];
                    var createDate = Convert.ToDateTime(statement["CreatedDate"]);
                    var category = Convert.ToInt32(statement["Category"]);
                    var obj = new PersonalTransaction(name, description, money, createDate, category);
                    personalTransactions.Add(obj);
                    totalMoney += Convert.ToDouble(statement["Money"]);
                }
            }
            return personalTransactions;
        }

        public List<PersonalTransaction> FindByCategory(int Category)
        {
            totalMoney = 0;
            var list = new List<PersonalTransaction>();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("lab3.db");
                using (var stt = cnn.Prepare($"select * from PersonalTransactions where Category = {Category}"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new PersonalTransaction()
                        {
                            Name = (string)stt["Name"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToDouble(stt["Money"]);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PersonalTransaction> FindByStartDateAndEndDate(string startDate, string endDate)
        {
            totalMoney = 0;
            var list = new List<PersonalTransaction>();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection("lab3.db");
                using (var stt = cnn.Prepare($"select * from PersonalTransactions where CreatedDate between '{startDate}' and '{endDate}'"))
                {
                    while (stt.Step() == SQLiteResult.ROW)
                    {
                        var personal = new PersonalTransaction()
                        {
                            Name = (string)stt["Name"],
                            Description = (string)stt["Description"],
                            Money = Convert.ToDouble(stt["Money"]),
                            CreatedDate = Convert.ToDateTime(stt["CreatedDate"]),
                            Category = Convert.ToInt32(stt["Category"]),
                        };
                        list.Add(personal);
                        totalMoney += Convert.ToInt32(stt["Money"]);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {              
                return null;
            }
        }
    }
}
