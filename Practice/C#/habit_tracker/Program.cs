﻿using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Globalization;

namespace habit_tracker
{
    class Program
    {
        static string connectionString = @"Data Source=habit-Tracker.db";
        static void Main (string[] args)
        {
            

            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = 
                @"CREATE TABLE IF NOT EXISTS drinking_water (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    Date TEXT,
                    Quantity INTEGER
                    )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }

            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine ("\n\nMAIN MENU");
                Console.WriteLine ("\nWhat would you like to do?");
                Console.WriteLine ("\nType 0 to Close Application.");
                Console.WriteLine ("Type 1 to View All Records");
                Console.WriteLine ("Type 2 to Insert Record");
                Console.WriteLine ("Type 3 to Delete Record");
                Console.WriteLine ("Type 4 to Update Record");
                Console.WriteLine ("---------------------------------------\n");

                string commandInput = Console.ReadLine();

                switch (commandInput)
                {   
                    case "0":
                        Console.WriteLine("\nAdios!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        GetAllRecords();
                        break; 
                    case "2":
                        Insert();
                        break;
                    case "3":
                        Delete();
                        break;
                    case "4":
                        Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break; 
                }
            }
        }


        private static void GetAllRecords(){
            Console.Clear();
            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = 
                $"SELECT * FROM drinking_water";

                List<DrinkingWater> tableData =new();
                
                SqliteDataReader reader = tableCmd.ExecuteReader();

                //read code from database
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        tableData.Add(
                            new DrinkingWater
                            {
                                Id = reader.GetInt32(0),
                                Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-Us")),
                                Quantity = reader.GetInt32(2)
                            }
                        ); ;
                    }
                }else{
                    Console.WriteLine("No rows found");
                }

                 connection.Close();
                //looping over the table to create separation for entry
                 Console.WriteLine("--------------------\n");
                 foreach (var dw in tableData)
                 {
                    Console.WriteLine($"{dw.Id} - {dw.Date.ToString("dd-MM-yyyy")} - Quantity: {dw.Quantity}");
                 }
                 Console.WriteLine("--------------------\n");
            }
        }

        private static void Insert(){
            string date = GetDateInput();

            int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice.\n\n");

            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"INSERT INTO drinking_water(date, quantity) VALUES('{date}', {quantity})";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal static void Update(){
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\nPlease type Id of the record would like to update. Type 0 to return to main menu.\n\n");

            using (var connection = new SqliteConnection(connectionString)){
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText =  $"SELECT EXISTS(SELECT 1 FROM drinking_water WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if(checkQuery == 0){
                    Console.WriteLine($"\n\nRecord withId {recordId} doesn't exist.\n\n");
                    connection.Close();
                    Update();
                }

                string date = GetDateInput(); 

                int quantity = GetNumberInput("\n\nPlease insert number of glasses or other measure of your choice.\n\n");
                
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE drinking_water SET date = '{date}', quantity = {quantity} WHERE Id = {recordId}";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }}


        internal static string GetDateInput(){
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-nn-yy). Type 0 return to to main menu.\n\n");

            string dateInput = Console.ReadLine();

            if (dateInput == "0") GetUserInput();

            while(!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _)){
                Console.WriteLine("\n\nInvalid date. (Format: dd-mm-yy). Type 0 to return to main menu or try gain:\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }
        
        private static void Delete(){
            Console.Clear();
            GetAllRecords();

            var recordId = GetNumberInput("\n\nPlease type the Id of the record you want to delete or type 0 to go go back to Main Menu\n\n");

            using (var connection = new SqliteConnection(connectionString)) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from drinking_water WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if(rowCount == 0){
                    Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                    Delete();
                }
            }

            Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");
        }

        internal static int GetNumberInput(string message){
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0") GetUserInput();
            
            while(!Int16.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0){
                Console.WriteLine("\n\nInvalid number. Try again\n\n");
                numberInput = Console.ReadLine();
            }

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }
    }
     public class DrinkingWater 
     {
        public int Id { get; set;}
        
        public DateTime Date {get; set;}

        public int Quantity { get; set;} 
    }
}









