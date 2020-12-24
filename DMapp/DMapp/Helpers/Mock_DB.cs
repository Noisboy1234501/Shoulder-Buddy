using System;
using System.Collections.Generic;
using System.Text;
using DMapp.Models;

namespace DMapp.Helpers
{
    class Mock_DB
    {

        // Thanks to this mock database we will see if sorting and finding right option
        // and qualities, options and weights connected with it, will work correctly.

        #region FirstSession
        //First session:
        DecisionSession mockSessionRight = new DecisionSession()
        {
            SessionID= 1,
            Title= "Right one",
           
        };
        // Options for first session
        Option rightOption_savePIS = new Option()
        {
            OptionID = 1,
            SessionID = 1,
            Name = "Save PIS"
        };

        Option rightOption_destroyPIS = new Option()
        {
            OptionID = 2,
            SessionID = 1,
            Name = "Destroy PIS"
        };

        // Qualities for first session

        Quality rightQuality_economy = new Quality()
        {
            QualityID = 1,
            SessionID=1,
            Name="Economy",
            Importance=5
        };

        Quality rightQuality_ideology = new Quality()
        {
            QualityID = 2,
            SessionID = 1,
            Name = "Ideology",
            Importance = 3
        };

        Weight rightWeight1 = new Weight()
        {
            OptionID = 1,
            WeightID = 1,
            Amount= 2,
            SessionID = 1

        };

        Weight rightWeight2 = new Weight()
        {
            OptionID = 1,
            WeightID = 2,
            Amount = 4,
            SessionID = 1
        };

        Weight rightWeight3 = new Weight()
        {
            OptionID = 2,
            WeightID = 3,
            Amount = 3,
            SessionID = 1
        };

        Weight rightWeight4 = new Weight()
        {
            OptionID = 2,
            WeightID = 4,
            Amount = 3,
            SessionID = 1
        };

        #endregion




        //Second session

        #region SecondSession
        DecisionSession mockSessionWrong = new DecisionSession()
        {
            SessionID = 2,
            Title = "Wrong one",
           
        };
        // Options for first session
        
       
        
        Option wrongOption_nonsense1 = new Option()
        {
            OptionID = 3,
            SessionID = 2,
            Name = "Wybor w zdaniu inaczej napisany test decyzjiiiiiii" //50
        };

        Option wrongOption_nonsense2 = new Option()
        {
            OptionID = 4,
            SessionID = 2,
            Name = "zdanie randomowe napisane teraz przerwy test"
        };

        Option wrongOption_nonsense3 = new Option()
        {
            OptionID = 5,
            SessionID = 2,
            Name = "zdanie dopisane jako ostatnie sprawdzam"
        };

        Option wrongOption_nonsense4 = new Option()
        {
            OptionID = 6,
            SessionID = 2,
            Name = "ALBERTCHUUUUUUUUUUJLBERTCHUUUUUUUUUALBERTCHUUUUUUUUUUUUUUUUUUUUUUAS" //67
        };

        Option wrongOption_nonsense5 = new Option()
        {
            OptionID = 7,
            SessionID = 2,
            Name = "ALBERTCHUUUUUUUUUUJLBERTCHUUUUUUUUUALBERTCHUUUUUUUUUUUUUUUUUUUUUUAS"
        };

        Option wrongOption_nonsense6 = new Option()
        {
            OptionID = 8,
            SessionID = 2,
            Name = "ALBERTCHUUUUUUUUUUJLBERTCHUUUUUUUUUALBERTCHUUUUUUUUUUUUUUUUUUUUUUAS"
        };

        Option wrongOption_nonsense7 = new Option()
        {
            OptionID = 9,
            SessionID = 2,
            Name = "ALBERTCHUUUUUUUUUJALBEFSAWFAWFAWF" //33
        };

        Option wrongOption_nonsense8 = new Option()
        {
            OptionID = 10,
            SessionID = 2,
            Name = "nonsesnseOption8"
        };

        Option wrongOption_nonsense9 = new Option()
        {
            OptionID = 11,
            SessionID = 2,
            Name = "nonsenseOption9"
        };

        Option wrongOption_nonsense10 = new Option()
        {
            OptionID = 12,
            SessionID = 2,
            Name = "nonsensOption10"
        };

        Option wrongOption_nonsense11 = new Option()
        {
            OptionID = 13,
            SessionID = 2,
            Name = "ALBERTCHUUUUUUFS" //16
        };

        Option wrongOption_nonsense12 = new Option()
        {
            OptionID = 14,
            SessionID = 2,
            Name = "nonsesnseOption12"
        };

        // Qualities for first session

        Quality wrongQuality_nonsense1 = new Quality()
        {
            QualityID = 3,
            SessionID = 2,
            Name = "AWFAWFAWFAWFAWFWAFAW",
            Importance = 5.43234
        };

        Quality wrongQuality_nonsense2 = new Quality()
        {
            QualityID = 4,
            SessionID = 2,
            Name = "stopien ",
            Importance = 3.2342
        };

        Quality wrongQuality_nonsense3 = new Quality()
        {
            QualityID = 5,
            SessionID = 2,
            Name = "masa",
            Importance = 1.4234
        };


        Weight wrongWeight1 = new Weight()
        {
            OptionID = 3,
            WeightID = 5,
            Amount = 4.3333,
            SessionID = 2

        };

        Weight wrongWeight2 = new Weight()  
        {
            OptionID = 3,
            WeightID = 6,
            Amount = 2.43,
            SessionID = 2
        };

        Weight wrongWeight3 = new Weight()
        {
            OptionID = 3,
            WeightID = 7,
            Amount = 2.12,
            SessionID = 2
        };

        Weight wrongWeight4 = new Weight()
        {
            OptionID = 4,
            WeightID = 8,
            Amount = 4.45,
            SessionID = 2

        };

        Weight wrongWeight5 = new Weight()
        {
            OptionID = 4,
            WeightID = 9,
            Amount = 2.123,
            SessionID = 2
        };

        Weight wrongWeight6 = new Weight()
        {
            OptionID = 4,
            WeightID = 10,
            Amount = 4.432,
            SessionID = 2
        };

        Weight wrongWeight7 = new Weight()
        {
            OptionID = 5,
            WeightID = 11,
            Amount = 1.234,
            SessionID = 2

        };

        Weight wrongWeight8 = new Weight()
        {
            OptionID = 5,
            WeightID = 12,
            Amount = 3.543,
            SessionID = 2
        };

        Weight wrongWeight9 = new Weight()
        {
            OptionID = 5,
            WeightID = 13,
            Amount = 5.21234,
            SessionID = 2
        };

        Weight wrongWeight10 = new Weight()
        {
            OptionID = 6,
            WeightID = 14,
            Amount = 3,
            SessionID = 2

        };

        Weight wrongWeight11 = new Weight()
        {
            OptionID = 6,
            WeightID = 15,
            Amount = 1,
            SessionID = 2
        };

        Weight wrongWeight12 = new Weight()
        {
            OptionID = 6,
            WeightID = 16,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight13 = new Weight()
        {
            OptionID = 7,
            WeightID = 17,
            Amount = 4,
            SessionID = 2

        };

        Weight wrongWeight14 = new Weight()
        {
            OptionID = 7,
            WeightID = 18,
            Amount = 4,
            SessionID = 2
        };

        Weight wrongWeight15 = new Weight()
        {
            OptionID = 7,
            WeightID = 19,
            Amount = 3,
            SessionID = 2
        };

        Weight wrongWeight16 = new Weight()
        {
            OptionID = 8,
            WeightID = 20,
            Amount = 2,
            SessionID = 2

        };

        Weight wrongWeight17 = new Weight()
        {
            OptionID = 8,
            WeightID = 21,
            Amount = 1,
            SessionID = 2
        };

        Weight wrongWeight18 = new Weight()
        {
            OptionID = 8,
            WeightID = 22,
            Amount = 4,
            SessionID = 2
        };

        Weight wrongWeight19 = new Weight()
        {
            OptionID = 9,
            WeightID = 23,
            Amount = 3,
            SessionID = 2

        };

        Weight wrongWeight20 = new Weight()
        {
            OptionID = 9,
            WeightID = 24,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight21 = new Weight()
        {
            OptionID = 9,
            WeightID = 25,
            Amount = 1,
            SessionID = 2
        };

        Weight wrongWeight22 = new Weight()
        {
            OptionID = 10,
            WeightID = 26,
            Amount = 4,
            SessionID = 2

        };

        Weight wrongWeight23 = new Weight()
        {
            OptionID = 10,
            WeightID = 27,
            Amount = 4,
            SessionID = 2
        };

        Weight wrongWeight24 = new Weight()
        {
            OptionID = 10,
            WeightID = 28,
            Amount = 4,
            SessionID = 2
        };

        Weight wrongWeight25 = new Weight()
        {
            OptionID = 11,
            WeightID = 29,
            Amount = 3,
            SessionID = 2

        };

        Weight wrongWeight26 = new Weight()
        {
            OptionID = 11,
            WeightID = 30,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight27 = new Weight()
        {
            OptionID = 11,
            WeightID = 31,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight28 = new Weight()
        {
            OptionID = 12,
            WeightID = 32,
            Amount = 2,
            SessionID = 2

        };

        Weight wrongWeight29 = new Weight()
        {
            OptionID = 12,
            WeightID = 33,
            Amount = 5,
            SessionID = 2
        };

        Weight wrongWeight30 = new Weight()
        {
            OptionID = 12,
            WeightID = 34,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight31 = new Weight()
        {
            OptionID = 13,
            WeightID = 35,
            Amount = 3,
            SessionID = 2

        };

        Weight wrongWeight32 = new Weight()
        {
            OptionID = 13,
            WeightID = 36,
            Amount = 2,
            SessionID = 2
        };

        Weight wrongWeight33 = new Weight()
        {
            OptionID = 13,
            WeightID = 37,
            Amount = 1,
            SessionID = 2
        };

        Weight wrongWeight34 = new Weight()
        {
            OptionID = 14,
            WeightID = 38,
            Amount = 4,
            SessionID = 2

        };

        Weight wrongWeight35 = new Weight()
        {
            OptionID = 14,
            WeightID = 39,
            Amount = 5,
            SessionID = 2
        };

        Weight wrongWeight36 = new Weight()
        {
            OptionID = 14,
            WeightID = 40,
            Amount = 2,
            SessionID = 2
        };

        #endregion


        // we add data to list, to simulate that it was return like this from data base table
        //Then we return the list, just like it works in case of real DB

        #region ReturningLists

        public List<DecisionSession> GetDecissionSessions()
        {
            List<DecisionSession> decisionSessionsList = new List<DecisionSession>();
            decisionSessionsList.Add(mockSessionRight);
            decisionSessionsList.Add(mockSessionWrong);
            return decisionSessionsList;
        }

        public List<Option> GetOptions()
        {
            List<Option> optionsList = new List<Option>();
            optionsList.Add(rightOption_savePIS);
            optionsList.Add(rightOption_destroyPIS);
            optionsList.Add(wrongOption_nonsense1);
            optionsList.Add(wrongOption_nonsense2);
            optionsList.Add(wrongOption_nonsense3);
            optionsList.Add(wrongOption_nonsense4);
            optionsList.Add(wrongOption_nonsense5);
            optionsList.Add(wrongOption_nonsense6);
            optionsList.Add(wrongOption_nonsense7);
            optionsList.Add(wrongOption_nonsense8);
            optionsList.Add(wrongOption_nonsense9);
            optionsList.Add(wrongOption_nonsense10);
            optionsList.Add(wrongOption_nonsense11);
            //optionsList.Add(wrongOption_nonsense12);

            return optionsList;
        }

        public List<Quality> GetQualities()
        {
            List<Quality> qualitiesList = new List<Quality>();
            qualitiesList.Add(rightQuality_economy);
            qualitiesList.Add(rightQuality_ideology);
            qualitiesList.Add(wrongQuality_nonsense1);
            qualitiesList.Add(wrongQuality_nonsense2);
            qualitiesList.Add(wrongQuality_nonsense3);
            return qualitiesList;
        }

        public List<Weight> GetWeights()
        {
            List<Weight> weightsList = new List<Weight>();
            weightsList.Add(rightWeight1);
            weightsList.Add(rightWeight2);
            weightsList.Add(rightWeight3);
            weightsList.Add(rightWeight4);

            weightsList.Add(wrongWeight1);
            weightsList.Add(wrongWeight2);
            weightsList.Add(wrongWeight3);
            weightsList.Add(wrongWeight4);
            weightsList.Add(wrongWeight5);
            weightsList.Add(wrongWeight6);
            weightsList.Add(wrongWeight7);
            weightsList.Add(wrongWeight8);
            weightsList.Add(wrongWeight9);
            weightsList.Add(wrongWeight10);
            weightsList.Add(wrongWeight11);
            weightsList.Add(wrongWeight12);
            weightsList.Add(wrongWeight13);
            weightsList.Add(wrongWeight14);
            weightsList.Add(wrongWeight15);
            weightsList.Add(wrongWeight16);
            weightsList.Add(wrongWeight17);
            weightsList.Add(wrongWeight18);
            weightsList.Add(wrongWeight19);
            weightsList.Add(wrongWeight20);
            weightsList.Add(wrongWeight21);
            weightsList.Add(wrongWeight22);
            weightsList.Add(wrongWeight23);
            weightsList.Add(wrongWeight24);
            weightsList.Add(wrongWeight25);
            weightsList.Add(wrongWeight26);
            weightsList.Add(wrongWeight27);
            weightsList.Add(wrongWeight28);
            weightsList.Add(wrongWeight29);
            weightsList.Add(wrongWeight30);
            weightsList.Add(wrongWeight31);
            weightsList.Add(wrongWeight32);
            weightsList.Add(wrongWeight33);
            weightsList.Add(wrongWeight34);
            weightsList.Add(wrongWeight35);
            weightsList.Add(wrongWeight36);





            return weightsList;
        }

        #endregion

    }
}
