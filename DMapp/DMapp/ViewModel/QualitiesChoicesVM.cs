using DMapp.Helpers;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DMapp.ViewModel
{
    public class QualitiesChoicesVM : BaseViewModel
    {
        INavigation navigation;
        List<string> Sequence;
        List<string> QualitiesNames;
        int maxIndex;
        int NumOfVisibleChoices;
        
        
        public Command ContinueButtonClickedCommand { get; set; }
        public Command BackButtonClickedCommand { get; set; }

        public QualitiesChoicesVM(INavigation navi)
        {
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            BackButtonClickedCommand = new Command( () => ExecuteBackButtonClickedCommand());
            navigation = navi;
            if(InitializationCounter.numOfQualitiesChoicesVMInitialized == 0)
            {
                CurrentIndexHolder.QualitiesCurrentIndex = 1;
                InitializationCounter.numOfQualitiesChoicesVMInitialized++;
            }
            

        }

        #region Commands
        private void ExecuteBackButtonClickedCommand()
        {
            int currentIndex = CurrentIndexHolder.QualitiesCurrentIndex;
            if (currentIndex != 1) { CurrentIndexHolder.QualitiesCurrentIndex--; UpdateIndexes(); }
            
        }

        private async Task ExecuteContinueButtonClickedCommand()
        {
            int currentIndex = CurrentIndexHolder.QualitiesCurrentIndex;
            if(currentIndex != maxIndex) { CurrentIndexHolder.QualitiesCurrentIndex++; UpdateIndexes(); }
            else
            {
                SaveResultsToTemporaryDb();
                await navigation.PushAsync(new OptionsChoicesPage(navigation));
            }
            
        }
        #endregion

        #region Flow control methods
        //method triggered by QualitiesChoicePage, when page is displayed
        public void SiteDisplayed()
        {
            
            
           
            
            GetQualitesNames();
            GetSequence();
            UpdateIndexes();
            

            //SetDefaultSliderValues();
            //InitializationCounter.numOfQualitiesChoicesVMInitialized++;
        }

        // triggered by SiteDisplayed and when continue button is clicked.
        private void UpdateIndexes()
        {
           
            

            int currentIndex = CurrentIndexHolder.QualitiesCurrentIndex;
            int numOfAllQualities = QualitiesNames.Count();
            int numOfChoices = ((int)(Math.Pow(numOfAllQualities, 2) - numOfAllQualities))/2;
            if(numOfChoices == 0) { maxIndex = 1;  }
            else if (numOfChoices % 6 == 0) { maxIndex = numOfChoices / 6; }
            else if (numOfChoices % 6 != 0) { maxIndex = (numOfChoices / 6) + 1; }
            if (currentIndex > maxIndex) { currentIndex = maxIndex; } //in case someone had index for example 7, then he go back, delete many qualities so max index will be 5. So 5 < 7 and we have index but no data there.
            if (CurrentIndexHolder.QualitiesCurrentIndex > maxIndex) { CurrentIndexHolder.QualitiesCurrentIndex = maxIndex; }
            IndexToDisplay = $"{currentIndex}/{maxIndex}";

            AssignNames();
        }

        

        private void UpdateVisibility()
        {
            int currentIndex = CurrentIndexHolder.QualitiesCurrentIndex;

            
            int numOfAllQualities = QualitiesNames.Count;
            int numOfAllChoices = ((int) (Math.Pow(numOfAllQualities, 2) - numOfAllQualities))/2;
            int numOfChoicesForIndex;

            if(currentIndex == 1 && maxIndex == 1) { numOfChoicesForIndex = numOfAllChoices; }
            else if(currentIndex == maxIndex) { numOfChoicesForIndex = numOfAllChoices - 6 * (currentIndex - 1); }
            else { numOfChoicesForIndex = 6; }

            NumOfVisibleChoices = numOfChoicesForIndex;
            if (NumOfVisibleChoices == 0) { IsVisibleNoChoice = true; }
            else
            {
                IsVisibleNoChoice = false;
            }



            //displays right controls depending on choices' visibility
            switch (NumOfVisibleChoices)
            {
                
                case 1:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = false;
                    IsVisibleFrame3 = false;
                    IsVisibleFrame4 = false;
                    IsVisibleFrame5 = false;
                    IsVisibleFrame6 = false;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = false;
                    IsVisibleScoreLeft3 = false;
                    IsVisibleScoreLeft4 = false;
                    IsVisibleScoreLeft5 = false;
                    IsVisibleScoreLeft6 = false;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = false;
                    IsVisibleScoreRight3 = false;
                    IsVisibleScoreRight4 = false;
                    IsVisibleScoreRight5 = false;
                    IsVisibleScoreRight6 = false;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = false;
                    IsVisibleSlider3 = false;
                    IsVisibleSlider4 = false;
                    IsVisibleSlider5 = false;
                    IsVisibleSlider6 = false;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = false;
                    IsVisibleQualityNameLeft3 = false;
                    IsVisibleQualityNameLeft4 = false;
                    IsVisibleQualityNameLeft5 = false;
                    IsVisibleQualityNameLeft6 = false;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = false;
                    IsVisibleQualityNameRight3 = false;
                    IsVisibleQualityNameRight4 = false;
                    IsVisibleQualityNameRight5 = false;
                    IsVisibleQualityNameRight6 = false;
                    break;
                case 2:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = true;
                    IsVisibleFrame3 = false;
                    IsVisibleFrame4 = false;
                    IsVisibleFrame5 = false;
                    IsVisibleFrame6 = false;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = true;
                    IsVisibleScoreLeft3 = false;
                    IsVisibleScoreLeft4 = false;
                    IsVisibleScoreLeft5 = false;
                    IsVisibleScoreLeft6 = false;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = true;
                    IsVisibleScoreRight3 = false;
                    IsVisibleScoreRight4 = false;
                    IsVisibleScoreRight5 = false;
                    IsVisibleScoreRight6 = false;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = true;
                    IsVisibleSlider3 = false;
                    IsVisibleSlider4 = false;
                    IsVisibleSlider5 = false;
                    IsVisibleSlider6 = false;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = true;
                    IsVisibleQualityNameLeft3 = false;
                    IsVisibleQualityNameLeft4 = false;
                    IsVisibleQualityNameLeft5 = false;
                    IsVisibleQualityNameLeft6 = false;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = true;
                    IsVisibleQualityNameRight3 = false;
                    IsVisibleQualityNameRight4 = false;
                    IsVisibleQualityNameRight5 = false;
                    IsVisibleQualityNameRight6 = false;
                    break;
                case 3:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = true;
                    IsVisibleFrame3 = true;
                    IsVisibleFrame4 = false;
                    IsVisibleFrame5 = false;
                    IsVisibleFrame6 = false;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = true;
                    IsVisibleScoreLeft3 = true;
                    IsVisibleScoreLeft4 = false;
                    IsVisibleScoreLeft5 = false;
                    IsVisibleScoreLeft6 = false;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = true;
                    IsVisibleScoreRight3 = true;
                    IsVisibleScoreRight4 = false;
                    IsVisibleScoreRight5 = false;
                    IsVisibleScoreRight6 = false;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = true;
                    IsVisibleSlider3 = true;
                    IsVisibleSlider4 = false;
                    IsVisibleSlider5 = false;
                    IsVisibleSlider6 = false;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = true;
                    IsVisibleQualityNameLeft3 = true;
                    IsVisibleQualityNameLeft4 = false;
                    IsVisibleQualityNameLeft5 = false;
                    IsVisibleQualityNameLeft6 = false;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = true;
                    IsVisibleQualityNameRight3 = true;
                    IsVisibleQualityNameRight4 = false;
                    IsVisibleQualityNameRight5 = false;
                    IsVisibleQualityNameRight6 = false;
                    break;
                case 4:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = true;
                    IsVisibleFrame3 = true;
                    IsVisibleFrame4 = true;
                    IsVisibleFrame5 = false;
                    IsVisibleFrame6 = false;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = true;
                    IsVisibleScoreLeft3 = true;
                    IsVisibleScoreLeft4 = true;
                    IsVisibleScoreLeft5 = false;
                    IsVisibleScoreLeft6 = false;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = true;
                    IsVisibleScoreRight3 = true;
                    IsVisibleScoreRight4 = true;
                    IsVisibleScoreRight5 = false;
                    IsVisibleScoreRight6 = false;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = true;
                    IsVisibleSlider3 = true;
                    IsVisibleSlider4 = true;
                    IsVisibleSlider5 = false;
                    IsVisibleSlider6 = false;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = true;
                    IsVisibleQualityNameLeft3 = true;
                    IsVisibleQualityNameLeft4 = true;
                    IsVisibleQualityNameLeft5 = false;
                    IsVisibleQualityNameLeft6 = false;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = true;
                    IsVisibleQualityNameRight3 = true;
                    IsVisibleQualityNameRight4 = true;
                    IsVisibleQualityNameRight5 = false;
                    IsVisibleQualityNameRight6 = false;
                    break;
                case 5:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = true;
                    IsVisibleFrame3 = true;
                    IsVisibleFrame4 = true;
                    IsVisibleFrame5 = true;
                    IsVisibleFrame6 = false;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = true;
                    IsVisibleScoreLeft3 = true;
                    IsVisibleScoreLeft4 = true;
                    IsVisibleScoreLeft5 = true;
                    IsVisibleScoreLeft6 = false;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = true;
                    IsVisibleScoreRight3 = true;
                    IsVisibleScoreRight4 = true;
                    IsVisibleScoreRight5 = true;
                    IsVisibleScoreRight6 = false;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = true;
                    IsVisibleSlider3 = true;
                    IsVisibleSlider4 = true;
                    IsVisibleSlider5 = true;
                    IsVisibleSlider6 = false;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = true;
                    IsVisibleQualityNameLeft3 = true;
                    IsVisibleQualityNameLeft4 = true;
                    IsVisibleQualityNameLeft5 = true;
                    IsVisibleQualityNameLeft6 = false;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = true;
                    IsVisibleQualityNameRight3 = true;
                    IsVisibleQualityNameRight4 = true;
                    IsVisibleQualityNameRight5 = true;
                    IsVisibleQualityNameRight6 = false;
                    break;
                case 6:
                    IsVisibleFrame1 = true;
                    IsVisibleFrame2 = true;
                    IsVisibleFrame3 = true;
                    IsVisibleFrame4 = true;
                    IsVisibleFrame5 = true;
                    IsVisibleFrame6 = true;
                    IsVisibleScoreLeft1 = true;
                    IsVisibleScoreLeft2 = true;
                    IsVisibleScoreLeft3 = true;
                    IsVisibleScoreLeft4 = true;
                    IsVisibleScoreLeft5 = true;
                    IsVisibleScoreLeft6 = true;
                    IsVisibleScoreRight1 = true;
                    IsVisibleScoreRight2 = true;
                    IsVisibleScoreRight3 = true;
                    IsVisibleScoreRight4 = true;
                    IsVisibleScoreRight5 = true;
                    IsVisibleScoreRight6 = true;
                    IsVisibleSlider1 = true;
                    IsVisibleSlider2 = true;
                    IsVisibleSlider3 = true;
                    IsVisibleSlider4 = true;
                    IsVisibleSlider5 = true;
                    IsVisibleSlider6 = true;
                    IsVisibleQualityNameLeft1 = true;
                    IsVisibleQualityNameLeft2 = true;
                    IsVisibleQualityNameLeft3 = true;
                    IsVisibleQualityNameLeft4 = true;
                    IsVisibleQualityNameLeft5 = true;
                    IsVisibleQualityNameLeft6 = true;
                    IsVisibleQualityNameRight1 = true;
                    IsVisibleQualityNameRight2 = true;
                    IsVisibleQualityNameRight3 = true;
                    IsVisibleQualityNameRight4 = true;
                    IsVisibleQualityNameRight5 = true;
                    IsVisibleQualityNameRight6 = true;
                    break;

                default:
                    break;
            }

        }

        private void SaveResultsToTemporaryDb()
        {
            double[] qualitiesImportance = DecisionSystem.CalculateQualitiesScores(Sequence, QualitiesChoiceSliderValuesHolder.SliderValues, QualitiesNames.Count);
            TemporaryDb.qualitiesImportance = qualitiesImportance.ToList();
            //var resultsCheck = TemporaryDb.qualitiesImportance;
        }

        private void AssignNames()
        {
            UpdateVisibility();


            
            // establish start and end point of traversion. 0 means nothing was traversed.
            int TraversionStartingPoint = (CurrentIndexHolder.QualitiesCurrentIndex - 1) * 6;
            int TraversionEndPoint = TraversionStartingPoint + NumOfVisibleChoices;
            //Using Decision Sequence from DecisionSystem, where every index on the list means element (choice for example choice betwwen quality with index 0 and the one with index 1) which can be traversed.
            //Traversing
            int rowNumber = 0;
            for(int i = TraversionStartingPoint; i < TraversionEndPoint; i++)
            {
                string choice = Sequence[i];
                string[] qualities = choice.Split(' ');
                int leftQualityIndex;
                int rightQualityIndex;
                Int32.TryParse(qualities[0], out leftQualityIndex);
                Int32.TryParse(qualities[1], out rightQualityIndex);

                // Action - put right values inside rows.
                switch(rowNumber)
                {
                    case 0:
                        QualityNameLeft1 = QualitiesNames[leftQualityIndex];
                        QualityNameRight1 = QualitiesNames[rightQualityIndex];
                        break;
                    case 1:
                        QualityNameLeft2 = QualitiesNames[leftQualityIndex];
                        QualityNameRight2 = QualitiesNames[rightQualityIndex];
                        break;
                    case 2:
                        QualityNameLeft3 = QualitiesNames[leftQualityIndex];
                        QualityNameRight3 = QualitiesNames[rightQualityIndex];
                        break;
                    case 3:
                        QualityNameLeft4 = QualitiesNames[leftQualityIndex];
                        QualityNameRight4 = QualitiesNames[rightQualityIndex];
                        break;
                    case 4:
                        QualityNameLeft5 = QualitiesNames[leftQualityIndex];
                        QualityNameRight5 = QualitiesNames[rightQualityIndex];
                        break;
                    case 5:
                        QualityNameLeft6 = QualitiesNames[leftQualityIndex];
                        QualityNameRight6 = QualitiesNames[rightQualityIndex];
                        break;
                    default:
                        break;

                }

                rowNumber++;
            }

            LoadLatestSliderValues();
        }

        private void SafeSliderValue(int numOfSlider, double leftValue)
        {
            //switch - we need to know from which slider we got a value and in which índex to decide what is the index of the choice
            int choiceIndex = 0;

            switch(numOfSlider)
            {
                case 1:
                    choiceIndex = numOfSlider;
                    break;
                case 2:
                    choiceIndex = numOfSlider;
                    break;
                case 3:
                    choiceIndex = numOfSlider;
                    break;
                case 4:
                    choiceIndex = numOfSlider;
                    break;
                case 5:
                    choiceIndex = numOfSlider;
                    break;
                case 6:
                    choiceIndex = numOfSlider;
                    break;
            }
            int indexMultiplier = CurrentIndexHolder.QualitiesCurrentIndex - 1;
            choiceIndex += indexMultiplier * 6;
            QualitiesChoiceSliderValuesHolder.SliderValues[choiceIndex - 1] = leftValue;
        }
        private void LoadLatestSliderValues()
        {
            
            int currentIndex = CurrentIndexHolder.QualitiesCurrentIndex;
            int startingIndex = (currentIndex -1) * 6 -1;
            

            switch (NumOfVisibleChoices)
            {
                case 1:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    break;
                case 2:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    SliderValue2 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 2];
                    break;
                case 3:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    SliderValue2 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 2];
                    SliderValue3 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 3];
                    break;
                case 4:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    SliderValue2 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 2];
                    SliderValue3 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 3];
                    SliderValue4 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 4];
                    break;
                case 5:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    SliderValue2 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 2];
                    SliderValue3 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 3];
                    SliderValue4 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 4];
                    SliderValue5 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 5];
                    break;
                case 6:
                    SliderValue1 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 1];
                    SliderValue2 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 2];
                    SliderValue3 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 3];
                    SliderValue4 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 4];
                    SliderValue5 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 5];
                    SliderValue6 = QualitiesChoiceSliderValuesHolder.SliderValues[startingIndex + 6];
                    break;
            }
            

        }


        private void GetSequence()
        {
            int numOFQualities = QualitiesNames.Count;
            Sequence = DecisionSystem.GetQualitiesComparisonSequence(numOFQualities);

        }

        private void GetQualitesNames()
        {
            QualitiesNames = TemporaryDb.qualityNames;
        }

        //private void SetDefaultSliderValues()
        //{
        //    switch(NumOfVisibleChoices)
        //    {
        //        case 1:
        //            SliderValue1 = 0.5;
        //            break;
        //        case 2:
        //            SliderValue1 = SliderValue2 = 0.5;
        //            break;
        //        case 3:
        //            SliderValue1 = SliderValue2 = SliderValue3 = 0.5;
        //            break;
        //        case 4:
        //            SliderValue1 = SliderValue2 = SliderValue3 = SliderValue4 = 0.5;
        //            break;
        //        case 5:
        //            SliderValue1 = SliderValue2 = SliderValue3 = SliderValue4 = SliderValue5 = 0.5;
        //            break;
        //        case 6:
        //            SliderValue1 = SliderValue2 = SliderValue3 = SliderValue4 = SliderValue5 = SliderValue6 = 0.5;
        //            break;
        //    }
            
        //}
        

        private void SliderValueChanged(int sliderNumber, double value)
        {
            if(sliderNumber == 1)
            {
                SafeSliderValue(1, value);
                ScoreLeft1 = 1- value;
                ScoreRight1 = value;
                
            }
            if (sliderNumber == 2)
            {
                SafeSliderValue(2, value);
                ScoreLeft2 = 1 - value;
                ScoreRight2 = value;

            }
            if (sliderNumber == 3)
            {
                SafeSliderValue(3, value);
                ScoreLeft3 = 1 - value;
                ScoreRight3 = value;

            }
            if (sliderNumber == 4)
            {
                SafeSliderValue(4, value);
                ScoreLeft4 = 1 - value;
                ScoreRight4 = value;

            }
            if (sliderNumber == 5)
            {
                SafeSliderValue(5, value);
                ScoreLeft5 = 1 - value;
                ScoreRight5 = value;

            }
            if (sliderNumber == 6)
            {
                SafeSliderValue(6, value);
                ScoreLeft6 = 1 - value;
                ScoreRight6 = value;

            }


        }
        #endregion



        #region Bindable properties

        

        //index
        private string indexToDisplay;

        public string IndexToDisplay
        {
            get { return indexToDisplay; }
            set { indexToDisplay = value;
                OnPropertyChanged();
            }
        }

        



        //choice 1
        private bool isVisibleFrame1;

        public bool IsVisibleFrame1
        {
            get { return isVisibleFrame1; }
            set { isVisibleFrame1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft1;

        public bool IsVisibleScoreLeft1
        {
            get { return isVisibleScoreLeft1; }
            set { isVisibleScoreLeft1 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft1;

        public double ScoreLeft1
        {
            get { return scoreLeft1; }
            set { scoreLeft1 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight1;

        public double ScoreRight1
        {
            get { return scoreRight1; }
            set { scoreRight1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight1;

        public bool IsVisibleScoreRight1
        {
            get { return isVisibleScoreRight1; }
            set { isVisibleScoreRight1 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue1;

        public double SliderValue1
        {
            get { return sliderValue1; }
            set { sliderValue1 = value;
                SliderValueChanged(1, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider1;

        public bool IsVisibleSlider1
        {
            get { return isVisibleSlider1; }
            set { isVisibleSlider1 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft1;

        public string QualityNameLeft1
        {
            get { return qualityNameLeft1; }
            set { qualityNameLeft1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft1;

        public bool IsVisibleQualityNameLeft1
        {
            get { return isVisibleQualityNameLeft1; }
            set { isVisibleQualityNameLeft1 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight1;

        public string QualityNameRight1
        {
            get { return qualityNameRight1; }
            set { qualityNameRight1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight1;

        public bool IsVisibleQualityNameRight1
        {
            get { return isVisibleQualityNameRight1; }
            set { isVisibleQualityNameRight1 = value;
                OnPropertyChanged();
            }
        }


        //choice 2

        private bool isVisibleFrame2;

        public bool IsVisibleFrame2
        {
            get { return isVisibleFrame2; }
            set { isVisibleFrame2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft2;

        public bool IsVisibleScoreLeft2
        {
            get { return isVisibleScoreLeft2; }
            set { isVisibleScoreLeft2 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft2;

        public double ScoreLeft2
        {
            get { return scoreLeft2; }
            set
            {
                scoreLeft2 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight2;

        public double ScoreRight2
        {
            get { return scoreRight2; }
            set
            {
                scoreRight2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight2;

        public bool IsVisibleScoreRight2
        {
            get { return isVisibleScoreRight2; }
            set { isVisibleScoreRight2 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue2;

        public double SliderValue2
        {
            get { return sliderValue2; }
            set
            {
                sliderValue2 = value;
                SliderValueChanged(2, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider2;

        public bool IsVisibleSlider2
        {
            get { return isVisibleSlider2; }
            set { isVisibleSlider2 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft2;

        public string QualityNameLeft2
        {
            get { return qualityNameLeft2; }
            set
            {
                qualityNameLeft2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft2;

        public bool IsVisibleQualityNameLeft2
        {
            get { return isVisibleQualityNameLeft2; }
            set { isVisibleQualityNameLeft2 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight2;

        public string QualityNameRight2
        {
            get { return qualityNameRight2; }
            set
            {
                qualityNameRight2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight2;

        public bool IsVisibleQualityNameRight2
        {
            get { return isVisibleQualityNameRight2; }
            set { isVisibleQualityNameRight2 = value;
                OnPropertyChanged();
            }
        }


        //choice 3

        private bool isVisibleFrame3;

        public bool IsVisibleFrame3
        {
            get { return isVisibleFrame3; }
            set { isVisibleFrame3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft3;

        public bool IsVisibleScoreLeft3
        {
            get { return isVisibleScoreLeft3; }
            set { isVisibleScoreLeft3 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft3;

        public double ScoreLeft3
        {
            get { return scoreLeft3; }
            set
            {
                scoreLeft3 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight3;

        public double ScoreRight3
        {
            get { return scoreRight3; }
            set
            {
                scoreRight3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight3;

        public bool IsVisibleScoreRight3
        {
            get { return isVisibleScoreRight3; }
            set { isVisibleScoreRight3 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue3;

        public double SliderValue3
        {
            get { return sliderValue3; }
            set
            {
                sliderValue3 = value;
                SliderValueChanged(3, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider3;

        public bool IsVisibleSlider3
        {
            get { return isVisibleSlider3; }
            set { isVisibleSlider3 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft3;

        public string QualityNameLeft3
        {
            get { return qualityNameLeft3; }
            set
            {
                qualityNameLeft3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft3;

        public bool IsVisibleQualityNameLeft3
        {
            get { return isVisibleQualityNameLeft3; }
            set { isVisibleQualityNameLeft3 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight3;

        public string QualityNameRight3
        {
            get { return qualityNameRight3; }
            set
            {
                qualityNameRight3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight3;

        public bool IsVisibleQualityNameRight3
        {
            get { return isVisibleQualityNameRight3; }
            set { isVisibleQualityNameRight3 = value;
                OnPropertyChanged();
            }
        }


        //choice 4
        private bool isVisibleFrame4;

        public bool IsVisibleFrame4
        {
            get { return isVisibleFrame4; }
            set { isVisibleFrame4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft4;

        public bool IsVisibleScoreLeft4
        {
            get { return isVisibleScoreLeft4; }
            set { isVisibleScoreLeft4 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft4;

        public double ScoreLeft4
        {
            get { return scoreLeft4; }
            set
            {
                scoreLeft4 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight4;

        public double ScoreRight4
        {
            get { return scoreRight4; }
            set
            {
                scoreRight4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight4;

        public bool IsVisibleScoreRight4
        {
            get { return isVisibleScoreRight4; }
            set { isVisibleScoreRight4 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue4;

        public double SliderValue4
        {
            get { return sliderValue4; }
            set
            {
                sliderValue4 = value;
                SliderValueChanged(4, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider4;

        public bool IsVisibleSlider4
        {
            get { return isVisibleSlider4; }
            set { isVisibleSlider4 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft4;

        public string QualityNameLeft4
        {
            get { return qualityNameLeft4; }
            set
            {
                qualityNameLeft4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft4;

        public bool IsVisibleQualityNameLeft4
        {
            get { return isVisibleQualityNameLeft4; }
            set { isVisibleQualityNameLeft4 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight4;

        public string QualityNameRight4
        {
            get { return qualityNameRight4; }
            set
            {
                qualityNameRight4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight4;

        public bool IsVisibleQualityNameRight4
        {
            get { return isVisibleQualityNameRight4; }
            set { isVisibleQualityNameRight4 = value;
                OnPropertyChanged();
            }
        }


        //choice 5
        private bool isVisibleFrame5;

        public bool IsVisibleFrame5
        {
            get { return isVisibleFrame5; }
            set { isVisibleFrame5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft5;

        public bool IsVisibleScoreLeft5
        {
            get { return isVisibleScoreLeft5; }
            set { isVisibleScoreLeft5 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft5;

        public double ScoreLeft5
        {
            get { return scoreLeft5; }
            set
            {
                scoreLeft5 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight5;

        public double ScoreRight5
        {
            get { return scoreRight5; }
            set
            {
                scoreRight5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight5;

        public bool IsVisibleScoreRight5
        {
            get { return isVisibleScoreRight5; }
            set { isVisibleScoreRight5 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue5;

        public double SliderValue5
        {
            get { return sliderValue5; }
            set
            {
                sliderValue5 = value;
                SliderValueChanged(5, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider5;

        public bool IsVisibleSlider5
        {
            get { return isVisibleSlider5; }
            set { isVisibleSlider5 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft5;

        public string QualityNameLeft5
        {
            get { return qualityNameLeft5; }
            set
            {
                qualityNameLeft5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft5;

        public bool IsVisibleQualityNameLeft5
        {
            get { return isVisibleQualityNameLeft5; }
            set { isVisibleQualityNameLeft5 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight5;

        public string QualityNameRight5
        {
            get { return qualityNameRight5; }
            set
            {
                qualityNameRight5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight5;

        public bool IsVisibleQualityNameRight5
        {
            get { return isVisibleQualityNameRight5; }
            set { isVisibleQualityNameRight5 = value;
                OnPropertyChanged();
            }
        }


        //choice 6

        private bool isVisibleFrame6;

        public bool IsVisibleFrame6
        {
            get { return isVisibleFrame6; }
            set { isVisibleFrame6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft6;

        public bool IsVisibleScoreLeft6
        {
            get { return isVisibleScoreLeft6; }
            set { isVisibleScoreLeft6 = value;
                OnPropertyChanged();
            }
        }


        private double scoreLeft6;

        public double ScoreLeft6
        {
            get { return scoreLeft6; }
            set
            {
                scoreLeft6 = value;
                OnPropertyChanged();
            }
        }

        private double scoreRight6;

        public double ScoreRight6
        {
            get { return scoreRight6; }
            set
            {
                scoreRight6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreRight6;

        public bool IsVisibleScoreRight6
        {
            get { return isVisibleScoreRight6; }
            set { isVisibleScoreRight6 = value;
                OnPropertyChanged();
            }
        }


        private double sliderValue6;

        public double SliderValue6
        {
            get { return sliderValue6; }
            set
            {
                sliderValue6 = value;
                SliderValueChanged(6, value);
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider6;

        public bool IsVisibleSlider6
        {
            get { return isVisibleSlider6; }
            set { isVisibleSlider6 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameLeft6;

        public string QualityNameLeft6
        {
            get { return qualityNameLeft6; }
            set
            {
                qualityNameLeft6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft6;

        public bool IsVisibleQualityNameLeft6
        {
            get { return isVisibleQualityNameLeft6; }
            set { isVisibleQualityNameLeft6 = value;
                OnPropertyChanged();
            }
        }


        private string qualityNameRight6;

        public string QualityNameRight6
        {
            get { return qualityNameRight6; }
            set
            {
                qualityNameRight6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameRight6;

        public bool IsVisibleQualityNameRight6
        {
            get { return isVisibleQualityNameRight6; }
            set { isVisibleQualityNameRight6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleNoChoice;

        public bool IsVisibleNoChoice
        {
            get { return isVisibleNoChoice; }
            set { isVisibleNoChoice = value;
                OnPropertyChanged();
            }
        }


        #endregion


    }
}