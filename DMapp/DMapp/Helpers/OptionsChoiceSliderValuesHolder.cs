using DMapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMapp.Helpers
{
   public static class OptionsChoiceSliderValuesHolder
    {

            public static double[] SliderValues;


        public static void SetArraySize(int numOfAllChoices, int addingMode, int NumOfOptionDeleted, int NumOfQualityDeleted)
            {
            int mode = addingMode; // 0 means, quality was added, 1 means option was added.
            if (mode == 0)
            {

                if (SliderValues != null  && TemporaryDb.optionNames.Count > 0)
                {

                    if (numOfAllChoices > SliderValues.Length)
                    {
                        int numOfOptions = TemporaryDb.optionNames.Count;
                        double[] NewSliderValues = new double[numOfAllChoices];
                        int indexOfSliderValues = 0;
                        int IndexOfNewSliderValues = 0;
                        int numOfQualities = TemporaryDb.qualityNames.Count;
                        for (int i = 0; i < numOfOptions; i++)
                        {
                            for (int j = 0; j < numOfQualities; j++)
                            {
                                if (j != numOfQualities - 1)
                                {
                                    NewSliderValues[IndexOfNewSliderValues] = SliderValues[indexOfSliderValues];
                                    IndexOfNewSliderValues++;
                                    indexOfSliderValues++;
                                }
                                else
                                {
                                    NewSliderValues[IndexOfNewSliderValues] = 0;
                                    IndexOfNewSliderValues++;
                                }
                            }
                        }
                        SliderValues = NewSliderValues;
                    }
                    else if (numOfAllChoices < SliderValues.Length)
                    {
                        int numOfQualityDeleted = NumOfQualityDeleted;
                        double[] NewSliderValues = new double[numOfAllChoices];
                        List<double> tempValues = SliderValues.ToList();
                        int option = 0;
                        foreach (var optionName in TemporaryDb.optionNames)
                        {
                            int indexToDelete = 0;
                            if (option == 0)
                            {
                                indexToDelete = (numOfQualityDeleted - 1);
                            }
                            else
                            {
                                indexToDelete = (numOfQualityDeleted - 1) + TemporaryDb.qualityNames.Count * option;
                            }

                            tempValues.RemoveAt(indexToDelete);
                            option++;
                        }
                        for (int i = 0; i < numOfAllChoices; i++)
                        {
                            NewSliderValues[i] = tempValues[i];
                        }
                        SliderValues = NewSliderValues;
                    }
                }
                
                
                else if (SliderValues == null) { SliderValues = new double[numOfAllChoices]; }
                else if (numOfAllChoices < SliderValues.Length)
                {
                    int numOfQualityDeleted = NumOfQualityDeleted;  // first quality has number 1, 0 means no quality was deleted
                }

            }
            else if(mode == 1)
            {
                if(numOfAllChoices > SliderValues.Length)
                {
                    double[] NewSliderValues = new double[numOfAllChoices];
                    int numOfSliderValueElements = SliderValues.Length;
                    for (int i = 0; i < numOfSliderValueElements; i++)
                    {
                        NewSliderValues[i] = SliderValues[i];
                    }
                    SliderValues = NewSliderValues;
                }
                if(numOfAllChoices < SliderValues.Length)
                {
                    double[] NewSliderValues = new double[numOfAllChoices];
                    int numOfOptionDeleted = NumOfOptionDeleted;   // first option has number 1, 0 means no option was deleted
                    //interval to skip = <startingIndexToSkip, endingIndexToSkip>
                    int startingIndexToSkip = 0;
                    if (numOfOptionDeleted == 1) { startingIndexToSkip = 0; } 
                    else
                    {
                        startingIndexToSkip = ((numOfOptionDeleted - 1) * TemporaryDb.qualityNames.Count);
                    }
                    int endingIndexToSkip = (numOfOptionDeleted * TemporaryDb.qualityNames.Count) - 1;
                    // 1  0- 5
                    // 2 6- 11
                    // 3 12 - 17
                    int indexOfNewSliderValues = 0;
                    for(int i= 0; i < startingIndexToSkip; i++ )
                    {
                        NewSliderValues[indexOfNewSliderValues] = SliderValues[i];
                        indexOfNewSliderValues++;
                    }
                    for(int i = endingIndexToSkip +1; i < SliderValues.Length; i++)
                    {
                        NewSliderValues[indexOfNewSliderValues] = SliderValues[i];
                        indexOfNewSliderValues++;
                    }
                    SliderValues = NewSliderValues;
                }
                
            }


        }





        }
    }
