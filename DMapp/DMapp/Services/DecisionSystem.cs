using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMapp.Services
{
    public static class DecisionSystem
    {
        // Stores comparisonsResults. We assume first item in list (with index 0), stores the importance of first quality, second item (index 1), stores the importance of second quality and so on..
        private static List<double> ComparisonResults = new List<double>();


         
        // It returns the sequence by which comparisons will be compared. For example first we should compare qualities 0 with 1, then 0 with 2 and so on.
        //index of first list represents index of option which will be displayed on the left. Value for indexes of second list represents index of option which will be displayed on the right.
        // In update, it is presented as strings for examples comparison of quality with index 0 (first quality) with quality of index 1 is written as 01.
        public static List<string> GetQualitiesComparisonSequence(int numberOfQualities)
        {
            List<List<int>> DecisionSequence = new List<List<int>>();
            int passedMaxNumOfQualities = numberOfQualities;
            for ( int starter = 0; starter < passedMaxNumOfQualities -1 ; starter++)
            {
                DecisionSequence.Add(new List<int>());
                for(int challanger = starter + 1; challanger < passedMaxNumOfQualities ; challanger ++)
                {
                    DecisionSequence[starter].Add(challanger) ;   
                }
            }
            // jezeli sie okaze ze nie potrzebuje w formie listy w liscie, tylko wystarczy string, to usunac stara wersje i zrobic od razu do listy string.
            return TransformQualitiesSequenceToListOfStrings(DecisionSequence);
        }

        // index of first list is index of quality, index of second list is the index of first options which will be displayed on the left
        //values of last list represents index of options which will be displayed on the right.
        public static List<List<List<int>>> GetOptionsComparisonSequence (int numberOfQualities, int numberOfOptions)
        {
            // change it
            List<List<List<int>>> finalList = new List<List<List<int>>>();
            for(int i = 0; i < numberOfQualities; i++)
            {
                    List<List<int>> OptionsSequence = new List<List<int>>();
                    int passedMaxNumOfOptions = numberOfOptions;
                    for (int starter = 0; starter < passedMaxNumOfOptions - 1; starter++)
                    {
                        OptionsSequence.Add(new List<int>());
                        for (int challanger = starter + 1; challanger < passedMaxNumOfOptions; challanger++)
                        {
                            OptionsSequence[starter].Add(challanger);
                        }
                    }
                    finalList.Add(OptionsSequence);
            }
            return finalList;
                                

        }

        private static List<string> TransformQualitiesSequenceToListOfStrings(List<List<int>> qualitesSequence)
        {
            List<string> sequence = new List<string>();

            int rightCounter = 0;
            int leftCounter = 0;
            foreach (var main in qualitesSequence )
            {
                foreach(int leftIndex in main)
                {
                    string comparison =  $"{leftCounter.ToString()} {(rightCounter + 1 + leftCounter).ToString()}";
                    
                    sequence.Add(comparison);
                    rightCounter++;
                }
                rightCounter = 0;
                leftCounter++;
            }
            return sequence;
        }

        //Obsolete method used in previous version of the app.

        //public static void CompareQualities(List<double> winningIndexes, int numberOfQualities)
        //{
        //    ComparisonResults.Clear();
        //    for (int i = 0; i < numberOfQualities; i++)
        //    {
        //        int qualityWonTimes = winningIndexes.Count(x => x == i);
        //        ComparisonResults.Add(qualityWonTimes);

        //    }

        //}


        //It returns final result, it means that qualities' importance is multiplied by respected option weights
        
        //first version of method, used by 

        public static double[] CalculateQualitiesScores(List<string> sequence, double [] leftSideValues, int numOfQualities) {

            double[] qualitiesScores = new double[numOfQualities];
            int counter = 0;
            foreach(string comparison in sequence)
            {
                string[] elements = comparison.Split(' ');
                int leftQualityIndex;
                int rightQualityIndex;
                Int32.TryParse(elements[0], out leftQualityIndex);
                Int32.TryParse(elements[1], out rightQualityIndex);
                qualitiesScores[leftQualityIndex] += leftSideValues[counter];
                qualitiesScores[rightQualityIndex] += 1 - leftSideValues[counter]; 
                counter++;
            }
            return qualitiesScores;
        }
        public static List<double> ReturnResult(List<double> qualitesScores, List<double> weights)
        {
            int numOfQualities = weights.Count();
            ComparisonResults = qualitesScores;
            double RightCount = ComparisonResults.Count();
            
            try 
            {
                double expectedSum = ((Math.Sqrt(numOfQualities) - numOfQualities)) / 2;
                if (RightCount != expectedSum) { throw new System.Exception($"Sum of quality importance is not equal to expected sum {expectedSum}. Algorithm doesn't work as expected" ); }
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Algorithm failed (Decision System)", ex.Message, "Ok");
            }

            for (int i = 0; i < numOfQualities; i++)
            {
                ComparisonResults[i] *= weights[i];
            }
 
            return new List<double>(ComparisonResults); //we create new list, otherwise we have only reference to list stored in decisionSession, when this list will be changed our result will change!
        }
        // Overload used for DetailedResultVM
        public static List<double> ReturnResult(List<double> qualitiesImportance, List<List<double>> optionWeights)
        {
            List<double> scores = new List<double>();
            double score = 0;
            for(int i = 0; i< optionWeights.Count; i++)
            {
               for(int j=0; j< qualitiesImportance.Count; j++)
                {
                    score += qualitiesImportance[j]*optionWeights[i][j];
                }
                scores.Add(score);
                score = 0;
            }
            
            return scores;
        }
        
        //Obsolete method

        //public static List<double> ReturnQualitiesImportance()
        //{
        //    try
        //    {
        //        if (ComparisonResults.Count() == 0)
        //        {
        //            throw new System.Exception("Qualities were not compared yet");
        //        }
        //    }
            
        //    catch(Exception ex)
        //    {
        //        App.Current.MainPage.DisplayAlert("Wrong sequence of executed methods", ex.Message, "Ok");
        //    }
                
        //    return new List<double>(ComparisonResults);
        //}

    }
}
