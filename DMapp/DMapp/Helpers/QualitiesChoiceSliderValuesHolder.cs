using DMapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMapp.Helpers
{
    public static class QualitiesChoiceSliderValuesHolder
    {
        public static double[] SliderValues;  // storing only left value, right = 1 - left

       
        public static List<string> oldSequence;
        

        public static void SetArraySize(List<string> Newsequence, int deletedQualityIndex)
        {
            int numOfAllChoices = Newsequence.Count;
            if(SliderValues == null)
            {
                oldSequence = Newsequence;
                SliderValues = new double[oldSequence.Count];
                
            }
           

            else if (numOfAllChoices < SliderValues.Count()) {

                    List<double> tempSliderValuesList = new List<double>();
                    tempSliderValuesList = SliderValues.ToList();

                    List<int> indexesToDelete = new List<int>();

                    int elementsInOldSequence = oldSequence.Count;
                    for (int i = 0; i< elementsInOldSequence; i++)
                    {
                        string[] comparisonIndexes = oldSequence[i].Split(' ');
                        int leftComparisonIndex = 0;
                        int rightComparisonIndex = 0;
                        Int32.TryParse(comparisonIndexes[0], out leftComparisonIndex);
                        Int32.TryParse(comparisonIndexes[1], out rightComparisonIndex);
                        if(leftComparisonIndex == deletedQualityIndex || rightComparisonIndex == deletedQualityIndex)
                        {
                            indexesToDelete.Add(i);
                        }
                    }
                    int counter = 0;
                    foreach(int index in indexesToDelete)
                    {
                        tempSliderValuesList.RemoveAt(index - counter);
                        counter++;
                        
                    }
                    SliderValues = tempSliderValuesList.ToArray();

                    oldSequence = Newsequence;
                }
            
            else
            {
                List<int> indexesToAddZero = new List<int>();

                int indexAddedLast = TemporaryDb.qualityNames.Count - 1;
                int elementsInNewSequence = Newsequence.Count;
                if(elementsInNewSequence == 1)
                {
                    var temp = new List<double>();
                    temp.Add(0);
                    SliderValues = temp.ToArray();
                    oldSequence = Newsequence;
                }
                else if(elementsInNewSequence == 3)
                {
                    var temp = SliderValues.ToList();
                    temp.Add(0);
                    temp.Add(0);
                    SliderValues = temp.ToArray();
                    oldSequence = Newsequence;
                }
                else
                {
                    for (int i = 0; i < elementsInNewSequence; i++)
                    {
                        string[] comparisonIndexes = Newsequence[i].Split(' ');

                        int leftComparisonIndex = 0;
                        int rightComparisonIndex = 0;
                        Int32.TryParse(comparisonIndexes[0], out leftComparisonIndex);
                        Int32.TryParse(comparisonIndexes[1], out rightComparisonIndex);
                        if (leftComparisonIndex == indexAddedLast || rightComparisonIndex == indexAddedLast)
                        {
                            indexesToAddZero.Add(i);
                        }

                        
                    }

                    var tempList = SliderValues.ToList();

                    

                    foreach (var element in indexesToAddZero)
                    {
                        tempList.Insert(element, 0);
                    }

                    SliderValues = tempList.ToArray();

                    oldSequence = Newsequence;
                }
                


            }

        }

    }
}
