using DMapp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Services
{
    public static class TemporaryDb
    {
        public static string sessionTitle = "";
        public static int sessionCategoryID = 0;
        
        public static List<string> optionNames = new List<string>();
        public static List<string> qualityNames = new List<string>();
        public static List<double> qualitiesImportance = new List<double>();
        public static List<double> weights = new List<double>();
        public static int SessionID;
        public static string goal;
        private static List<int> OptionsID = new List<int>();

        public static DecisionSession DecisionSession = new DecisionSession();
        public static List<Option> Options = new List<Option>();
        public static List<Quality> Qualities = new List<Quality>();
        public static List<Weight> WeightClasses = new List<Weight>();

        public static double TimeForChoices;  // then incorporate time counting system
      
        //DecisionSession systems on/off
      
        
        public static string StopWatch;

        public static void CleanAllData()
        {
            sessionTitle = "";
            sessionCategoryName = "";
            sessionCategoryID = 0;
            TimeForChoices = 0;
            StopWatch = "";
            optionNames = new List<string>();
            weights = new List<double>();
            qualityNames = new List<string>();
            qualitiesImportance = new List<double>();
            
            
            goal = "";


            //option name max chars = 68
            //quality name max chars = 20

            // set limit for lenght of option name and quality name, I have wrote what limit it should be probably
            //in mock db
        }
        
        private static void SetSessionCategoryID(string sessionCategoryName)
        {
            var allCategories = ManagerSQL.ReadSessionCategories();
            if(allCategories.Count != 0)
            {
                foreach (var category in allCategories)
                {
                    if (category.CategoryName == sessionCategoryName) { sessionCategoryID = category.SessionCategoryID; }
                }

                if (sessionCategoryID == 0)
                {
                    App.Current.MainPage.DisplayAlert("Error", "Category was not choosen", "Ok");
                }
            }
            else { sessionCategoryID = 1; }
            
        }
        #region MethodsToInsertData
        public static void PrepareDataBeforeInsertion()
        {
            FindSessionID();
            ClearPreviousData();
            SetSessionCategoryID(sessionCategoryName);
            CreateDecisionSession();
            CreateOptions();
            CreateQualities();
            CreateWeights();

        }

        private static void ClearPreviousData()
        {
            DecisionSession = new DecisionSession();
            Options = new List<Option>();
            Qualities = new List<Quality>();
            WeightClasses = new List<Weight>();
            OptionsID = new List<int>();
        }

        public static void InsertDataToSQLiteDB()
        {
            ManagerSQL.InsertDecisionSession(DecisionSession);
            foreach(Option option in Options)
            {
                ManagerSQL.InsertOption(option);
            }
            foreach(Quality quality in Qualities)
            {
                ManagerSQL.InsertQuality(quality);
            }
            foreach(Weight weight in WeightClasses)
            {
                ManagerSQL.InsertWeight(weight);
            }
            //var check1 = ManagerSQL.ReadDecisionSessions();
            //var check2 = ManagerSQL.ReadOptions();
            //var check3 = ManagerSQL.ReadQualities();
            //var check4 = ManagerSQL.ReadSessionCategories();
            //var check5 = ManagerSQL.ReadWeights();
                        
        }

        private static void CreateWeights()
        {
            // we do not need weights id
            int weightIndex = 0;

            foreach (var optionID in OptionsID)
                {
                
                int temp = 0;
                for(int i = weightIndex; i < weightIndex + weights.Count/optionNames.Count; i++)
                {
                    Weight newWeight = new Weight();
                    newWeight.OptionID = optionID;
                    newWeight.SessionID = SessionID;
                    newWeight.Amount = weights[i];
                    WeightClasses.Add(newWeight);
                    temp++;
                }
                weightIndex += temp;
                    
                }
            
        }

        private static void CreateQualities()
        {
            // for now we do not need qualities ID
            int importanceIndex = 0;
            foreach(string name in qualityNames)
            {
                Quality quality = new Quality();
                quality.Name = name;
                quality.SessionID = SessionID;
                quality.Importance = qualitiesImportance[importanceIndex];
                importanceIndex++;
                Qualities.Add(quality);
            }
        }

        private static void CreateOptions()
        {
            
            FindOptionsID();
            foreach(string name in optionNames)
            {
                Option option = new Option();
                option.Name = name;
                option.SessionID = SessionID;
                Options.Add(option);
            }
        }

        private static void FindOptionsID()
        {
            var allOptions = ManagerSQL.ReadOptions();
            int numOfAllOptions = allOptions.Count;
            int numberOfOptionsToAdd = optionNames.Count;
            int lastIDToAdd = numOfAllOptions + numberOfOptionsToAdd;
            for(int i = numOfAllOptions+1; i < lastIDToAdd+1; i++)
            {
                OptionsID.Add(i);
            }

        }

        public static void FindSessionID()
        {
            var allsessions = ManagerSQL.ReadDecisionSessions();
            int numOfAllSessions = allsessions.Count;
            SessionID = numOfAllSessions + 1;
        }

        private static void CreateDecisionSession()
        {
            DecisionSession session = new DecisionSession();
            session.SessionCategoryID = sessionCategoryID;
            session.Title = sessionTitle;
            session.Goal = goal;
            session.DateOfDecisionMaking = DateTime.Now.ToString();
            session.StopWatch = StopWatch;
            DecisionSession = session;
        }

        #endregion


        private static string sessionCategoryName;

        public static string SessionCategoryName
        {
            get { return sessionCategoryName; }
            set { sessionCategoryName = value;
                SetSessionCategoryID(sessionCategoryName);
             }
        }

        

    }
}
