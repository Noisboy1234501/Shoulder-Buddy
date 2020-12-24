using DMapp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Services
{
    public static class ManagerSQL
    {

        public static void InsertDecisionSession(DecisionSession decisionSession)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<DecisionSession>();
                int rows = sqlConnection.Insert(decisionSession);

            }
        }

        public static List<DecisionSession> ReadDecisionSessions()
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<DecisionSession>();
                var list = sqlConnection.Table<DecisionSession>().ToList();
                return new List<DecisionSession> (list);
            }
        }

        public static void InsertSessionCategory(SessionCategory sessionCategory)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<SessionCategory>();
                int rows = sqlConnection.Insert(sessionCategory);
            }
        }

        public static List<SessionCategory> ReadSessionCategories()
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<SessionCategory>();
                var list = sqlConnection.Table<SessionCategory>().ToList();
                return new List<SessionCategory>(list);
            }
        }

        public static void InsertQuality(Quality quality)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Quality>();
                int rows = sqlConnection.Insert(quality);


            }
        }

        public static List<Quality> ReadQualities()
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Quality>();
                var list = sqlConnection.Table<Quality>().ToList();
                return new List<Quality> (list);


            }

        }




        public static void InsertWeight(Weight weight)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Weight>();
                int rows = sqlConnection.Insert(weight);


            }
        }

        public static List<Weight> ReadWeights()
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Weight>();
                var list = sqlConnection.Table<Weight>().ToList();
                return new List<Weight> (list);


            }

        }


        public static void InsertOption(Option option)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Option>();
                int rows = sqlConnection.Insert(option);


            }
        }


        public static List<Option> ReadOptions()
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Option>();
                var list = sqlConnection.Table<Option>().ToList();
                return new List<Option> (list);


            }

        }


        //Deleting



        public static void DeleteDecisionSession(DecisionSession decisionSession)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<DecisionSession>();
                int rows = sqlConnection.Delete(decisionSession);

            }
        }

        public static void DeleteQuality(Quality quality)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Quality>();
                int rows = sqlConnection.Delete(quality);


            }
        }

        public static void DeletetWeight(Weight weight)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Weight>();
                int rows = sqlConnection.Delete(weight);


            }
        }

        public static void DeleteOption(Option option)
        {
            using (SQLiteConnection sqlConnection = new SQLiteConnection(App.DataBase))
            {
                sqlConnection.CreateTable<Option>();
                int rows = sqlConnection.Delete(option);


            }
        }

    }
    }
