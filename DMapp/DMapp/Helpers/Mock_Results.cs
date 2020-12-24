using DMapp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DMapp.Helpers
{
    public static class Mock_Results
    {

        //This method is used to debug the slider, nothing to do with real options which are displayed
        public static List<Option> ReturnOptions()
        {
            
            return new List<Option>()
            {
                new Option{Name="option1" },
                new Option{Name="option2" },
                new Option{Name="option3" },

                new Option{Name="option1" },
                new Option{Name="option2" },
                new Option{Name="option3" },

                new Option{Name="option1" },
                new Option{Name="option2" },
                new Option{Name="option3" }



            };
        }
        public static List<string> ReturnQualitiesNames()
        {
            return new List<string>
            {
                
                "QUALITY1EGERGGGGGGGGGGGG",//max 24 characters for quality length
                "quality2gergergfaefeffse",
                "qWsaAsdSFSAasdFsasFFAsdf",
                "quality4ergerg",
                "quality5ergerg",
                "quality6erger",
                "quality7eger",
                "quality8erger",
                "quality9erger",
                "quality10erge",
                "quality11erge",
                "quality12erger",

                // in case of more qualities, the last one may be cutted a little bit. Easy to solve, every 12 qualities we can add for example 1 to our multuplier in ResultVM


            };
        }

        public static List<double> ReturnQualitiesImportance()
        {
            return new List<double>()
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };
        }

        public static List<string> ReturnOptionNamesDividableBy3()
        {
            return new List<string>()
            {
                "it is short",
                "normal option length", // max number of characters fo option is 60
                "this one is much longer but not max",
                
            };
        }

        

        public static List<List<double>> ReturnWeightsOptionsDividableBy3()
        {
            return new List<List<double>>()
            {
                new List<double>() {1,2,3,4,5,6,7,8,9,10,11,12},
                new List<double>() {1,2,3,4,5,6,7,8,9,10,11,12},
                new List<double>() {1,2,3,4,5,6,7,8,9,10,11,12},
            };


        }

        public static List<double> ReturnOptionsScoreDiv3()
        {
            return new List<double>
            {
                1,22,333
            };
        }

        


    }
}
