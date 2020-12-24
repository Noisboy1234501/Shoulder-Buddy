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
    class OptionsChoicesVM : BaseViewModel
    {
        INavigation navigation;
        int maxIndex;
        public Command ContinueButtonClickedCommand { get; set; }
        public Command BackButtonClickedCommand { get; set; }
        private List<string> QualitiesNames;
        private List<string> OptionNames;


        private int PlaceInABatch;
        private int IndexesPerOption;
        private int NumOfCurrentOption;
        private int NumOfVisibleElements;
        
        public OptionsChoicesVM(INavigation navi)
        {
            navigation = navi;
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            BackButtonClickedCommand = new Command(() => ExecuteBackButtonClickedCommand());
            if (InitializationCounter.numOfOptionsChoicesVMInitialized == 0) // when continue clicked, set default once more.
            {
                
                CurrentIndexHolder.OptionsCurrentIndex = 1;
                InitializationCounter.numOfOptionsChoicesVMInitialized++;
            }
            

        }

        #region Methods

        public void PageDisplayed()
        {
            
            
            IsContinueButtonEnabled = false;
            QualitiesNames = TemporaryDb.qualityNames;
            OptionNames = TemporaryDb.optionNames;
            UpdateIndexes();
            LoadSliderValues();

        }
        private void UpdateIndexes()
        {
            IsContinueButtonEnabled = false;
            int qualitiesNamesCount = TemporaryDb.qualityNames.Count;
            int optionNamesCount = TemporaryDb.optionNames.Count;
            int indexesPerOption;
            if(qualitiesNamesCount % 6 == 0) { indexesPerOption = qualitiesNamesCount / 6; }
            else { indexesPerOption = (qualitiesNamesCount / 6) + 1; }
            maxIndex = indexesPerOption * optionNamesCount;
            if (CurrentIndexHolder.OptionsCurrentIndex > maxIndex) { CurrentIndexHolder.OptionsCurrentIndex = maxIndex; }
                IndexToDisplay = $"{CurrentIndexHolder.OptionsCurrentIndex}/{maxIndex}";
            UpdateOptionNames(indexesPerOption);
        }

        private void UpdateOptionNames(int indexesPerOption)
        {
            // maybe I am able to optimize it
            int NumberOfCurrentOption = 0;
            for(int i = 1; i < OptionNames.Count + 1; i++)
            {
                for(int j = 1 + indexesPerOption*(i-1); j < 1+ indexesPerOption*i; j++)
                {
                    if(CurrentIndexHolder.OptionsCurrentIndex == j) { OptionName = OptionNames[i -1];
                        NumberOfCurrentOption = i;
                    }
                }
            }
           

            UpdateVisibility(indexesPerOption, NumberOfCurrentOption);
        }

        private void UpdateVisibility(int indexesPerOption, int NumberOfCurrentOption)
        {
            // it can be optimized for sure
            NumOfCurrentOption = NumberOfCurrentOption;
            int visibleElements;
            int currentIndex = CurrentIndexHolder.OptionsCurrentIndex;
            int numOfQualities = QualitiesNames.Count;

            if( numOfQualities < 7) { visibleElements = numOfQualities; }
            else if (numOfQualities % 6 == 0) { visibleElements = 6; }
            else
            {
                int numOfIndexesWith6Qualities = numOfQualities / 6; // in one option I mean
                int numberOfQualitiesInLastIndexForOption = numOfQualities - numOfIndexesWith6Qualities * 6;
                int numOfPreviousOption = NumberOfCurrentOption - 1;

                if( currentIndex <= numOfIndexesWith6Qualities + numOfPreviousOption * indexesPerOption ) { visibleElements = 6; }
                else { visibleElements = numberOfQualitiesInLastIndexForOption; }

            }
            switch (visibleElements)
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
                   
                    break;

                default:
                    break;
            }
            UpdateQualitiesNames(indexesPerOption, NumberOfCurrentOption);
            NumOfVisibleElements = visibleElements;
            
        }

        private void UpdateQualitiesNames(int indexesPerOption, int numOfCurrentOption )
        {
            //rather optimized 

            int placeInBatch = indexesPerOption - (numOfCurrentOption * indexesPerOption - CurrentIndexHolder.OptionsCurrentIndex);
            PlaceInABatch = placeInBatch;
            IndexesPerOption = indexesPerOption;
            if(placeInBatch != indexesPerOption)
            {
                int qualityNameStartingIndex = (placeInBatch-1)*6;
                //int qualityNameEndingIndex = (placeInBatch*6) -1;
                QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex+1];
                QualityNameLeft3 = QualitiesNames[qualityNameStartingIndex+2];
                QualityNameLeft4 = QualitiesNames[qualityNameStartingIndex+3];
                QualityNameLeft5 = QualitiesNames[qualityNameStartingIndex+4];
                QualityNameLeft6 = QualitiesNames[qualityNameStartingIndex+5];

            }
            else
            {
                int leftQualities = QualitiesNames.Count - (placeInBatch-1) * 6;
                int qualityNameStartingIndex = (placeInBatch - 1) * 6;

                switch (leftQualities)
                {
                    case 1:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                      
                        break;
                    case 2:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                        QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex + 1];
                        
                        break;
                    case 3:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                        QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex + 1];
                        QualityNameLeft3 = QualitiesNames[qualityNameStartingIndex + 2];
                       
                        break;
                    case 4:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                        QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex + 1];
                        QualityNameLeft3 = QualitiesNames[qualityNameStartingIndex + 2];
                        QualityNameLeft4 = QualitiesNames[qualityNameStartingIndex + 3];
                        
                        break;
                    case 5:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                        QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex + 1];
                        QualityNameLeft3 = QualitiesNames[qualityNameStartingIndex + 2];
                        QualityNameLeft4 = QualitiesNames[qualityNameStartingIndex + 3];
                        QualityNameLeft5 = QualitiesNames[qualityNameStartingIndex + 4];
                        
                        break;
                    case 6:
                        QualityNameLeft1 = QualitiesNames[qualityNameStartingIndex];
                        QualityNameLeft2 = QualitiesNames[qualityNameStartingIndex + 1];
                        QualityNameLeft3 = QualitiesNames[qualityNameStartingIndex + 2];
                        QualityNameLeft4 = QualitiesNames[qualityNameStartingIndex + 3];
                        QualityNameLeft5 = QualitiesNames[qualityNameStartingIndex + 4];
                        QualityNameLeft6 = QualitiesNames[qualityNameStartingIndex + 5];
                        break;
                    default:
                        break;
                }
            }
            

            
        }

        private void SafeSliderValue(int numOfSlider, double value)
        {
            int numOfQualities = TemporaryDb.qualityNames.Count;
            int optionModifier;
            if(NumOfCurrentOption != 1) { optionModifier = ((NumOfCurrentOption - 1) * numOfQualities) ; }
            else { optionModifier = 0; }

            int indexToSave;
            
            
                indexToSave = optionModifier + ((PlaceInABatch - 1) * 6) - 1 + numOfSlider;
            
            
               

            OptionsChoiceSliderValuesHolder.SliderValues[indexToSave] = value;
        }

        private void SliderValueChanged(int numOfSlider) 
        {
            switch (numOfSlider)
            {
                case 1:
                    ScoreLeft1 = sliderScore1;
                    SafeSliderValue(1, ScoreLeft1);
                    break;
                case 2:
                    ScoreLeft2 = sliderScore2;
                    SafeSliderValue(2, ScoreLeft2);
                    break;
                case 3:
                    ScoreLeft3 = sliderScore3;
                    SafeSliderValue(3, ScoreLeft3);
                    break;
                case 4:
                    ScoreLeft4 = sliderScore4;
                    SafeSliderValue(4, ScoreLeft4);
                    break;
                case 5:
                    ScoreLeft5 = sliderScore5;
                    SafeSliderValue(5, ScoreLeft5);
                    break;
                case 6:
                    ScoreLeft6 = sliderScore6;
                    SafeSliderValue(6, ScoreLeft6);
                    break;
            }
        }
        private void LoadSliderValues()
        {

            int numOfQualities = TemporaryDb.qualityNames.Count;
            int optionModifier;
            if (NumOfCurrentOption != 1) { optionModifier = ((NumOfCurrentOption - 1) * numOfQualities); }
            else { optionModifier = 0; }

            int indexToLoad = optionModifier + ((PlaceInABatch - 1) * 6) - 1; ;


            switch(NumOfVisibleElements)
            {
                case 1:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    break;
                case 2:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    SliderScore2 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 2];
                    
                    break;
                case 3:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    SliderScore2 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 2];
                    SliderScore3 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 3];
                    
                    break;
                case 4:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    SliderScore2 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 2];
                    SliderScore3 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 3];
                    SliderScore4 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 4];
                    break;
                case 5:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    SliderScore2 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 2];
                    SliderScore3 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 3];
                    SliderScore4 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 4];
                    SliderScore5 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 5];
                    break;
                case 6:
                    SliderScore1 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 1];
                    SliderScore2 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 2];
                    SliderScore3 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 3];
                    SliderScore4 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 4];
                    SliderScore5 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 5];
                    SliderScore6 = OptionsChoiceSliderValuesHolder.SliderValues[indexToLoad + 6];
                    break;
            }

            IsContinueButtonEnabled = true;
        }

        #endregion

        #region Commands
        private void ExecuteBackButtonClickedCommand()
        {

            int currentIndex = CurrentIndexHolder.OptionsCurrentIndex;
            if (currentIndex != 1) { CurrentIndexHolder.OptionsCurrentIndex--; UpdateIndexes(); LoadSliderValues(); }
        }

        private async Task ExecuteContinueButtonClickedCommand()
        {
            IsContinueButtonEnabled = false;

            int currentIndex = CurrentIndexHolder.OptionsCurrentIndex;
            if (currentIndex != maxIndex) {
                CurrentIndexHolder.OptionsCurrentIndex++;
                UpdateIndexes();
                LoadSliderValues(); }
            else
            {
                TemporaryDb.weights = OptionsChoiceSliderValuesHolder.SliderValues.ToList();
                TemporaryDb.FindSessionID();
                await navigation.PushAsync(new GeneralResultsPage(navigation,0, TemporaryDb.SessionID));
                
            }


        }
        #endregion

        #region Bindable properties

        private bool isContinueButtonEnabled;

        public bool IsContinueButtonEnabled
        {
            get { return isContinueButtonEnabled; }
            set { isContinueButtonEnabled = value;
                OnPropertyChanged();
            }
        }


        private string optionName;

        public string OptionName
        {
            get { return optionName; }
            set { optionName = value;
                OnPropertyChanged();
            }
        }


        private string indexToDisplay;

        public string IndexToDisplay
        {
            get { return indexToDisplay; }
            set { indexToDisplay = value;
                OnPropertyChanged();
            }
        }


        private bool isVisibleFrame1;

        public bool IsVisibleFrame1
        {
            get { return isVisibleFrame1; }
            set { isVisibleFrame1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleFrame2;

        public bool IsVisibleFrame2
        {
            get { return isVisibleFrame2; }
            set
            {
                isVisibleFrame2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleFrame3;

        public bool IsVisibleFrame3
        {
            get { return isVisibleFrame3; }
            set
            {
                isVisibleFrame3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleFrame4;

        public bool IsVisibleFrame4
        {
            get { return isVisibleFrame4; }
            set
            {
                isVisibleFrame4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleFrame5;

        public bool IsVisibleFrame5
        {
            get { return isVisibleFrame5; }
            set
            {
                isVisibleFrame5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleFrame6;

        public bool IsVisibleFrame6
        {
            get { return isVisibleFrame6; }
            set
            {
                isVisibleFrame6 = value;
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

        private bool isVisibleSlider2;

        public bool IsVisibleSlider2
        {
            get { return isVisibleSlider2; }
            set
            {
                isVisibleSlider2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider3;

        public bool IsVisibleSlider3
        {
            get { return isVisibleSlider3; }
            set
            {
                isVisibleSlider3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider4;

        public bool IsVisibleSlider4
        {
            get { return isVisibleSlider4; }
            set
            {
                isVisibleSlider4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider5;

        public bool IsVisibleSlider5
        {
            get { return isVisibleSlider5; }
            set
            {
                isVisibleSlider5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleSlider6;

        public bool IsVisibleSlider6
        {
            get { return isVisibleSlider6; }
            set
            {
                isVisibleSlider6 = value;
                OnPropertyChanged();
            }
        }

        private double sliderScore1;

        public double SliderScore1
        {
            get { return sliderScore1; }
            set { sliderScore1 = value;
                OnPropertyChanged();
                SliderValueChanged(1);
            }
        }

        private double sliderScore2;

        public double SliderScore2
        {
            get { return sliderScore2; }
            set { sliderScore2 = value;
                OnPropertyChanged();
                SliderValueChanged(2);
            }
        }

        private double sliderScore3;

        public double SliderScore3
        {
            get { return sliderScore3; }
            set { sliderScore3 = value;
                OnPropertyChanged();
                SliderValueChanged(3);
            }
        }

        private double sliderScore4;

        public double SliderScore4
        {
            get { return sliderScore4; }
            set { sliderScore4 = value;
                OnPropertyChanged();
                SliderValueChanged(4);
            }
        }

        private double sliderScore5;

        public double SliderScore5
        {
            get { return sliderScore5; }
            set { sliderScore5 = value;
                OnPropertyChanged();
                SliderValueChanged(5);
            }
        }

        private double sliderScore6;

        public double SliderScore6
        {
            get { return sliderScore6; }
            set { sliderScore6 = value;
                OnPropertyChanged();
                SliderValueChanged(6);
            }
        }

        private bool isVisibleScoreLeft1;

        public bool IsVisibleScoreLeft1
        {
            get { return isVisibleScoreLeft1; }
            set
            {
                isVisibleScoreLeft1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft1;

        public bool IsVisibleQualityNameLeft1
        {
            get { return isVisibleQualityNameLeft1; }
            set
            {
                isVisibleQualityNameLeft1 = value;
                OnPropertyChanged();
            }
        }

        private double scoreLeft1;

        public double ScoreLeft1
        {
            get { return scoreLeft1; }
            set
            {
                scoreLeft1 = value;
                OnPropertyChanged();
            }
        }

        private string qualityNameLeft1;

        public string QualityNameLeft1
        {
            get { return qualityNameLeft1; }
            set
            {
                qualityNameLeft1 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScoreLeft2;

        public bool IsVisibleScoreLeft2
        {
            get { return isVisibleScoreLeft2; }
            set
            {
                isVisibleScoreLeft2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft2;

        public bool IsVisibleQualityNameLeft2
        {
            get { return isVisibleQualityNameLeft2; }
            set
            {
                isVisibleQualityNameLeft2 = value;
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

        
             private bool isVisibleScoreLeft3;

        public bool IsVisibleScoreLeft3
        {
            get { return isVisibleScoreLeft3; }
            set
            {
                isVisibleScoreLeft3 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft3;

        public bool IsVisibleQualityNameLeft3
        {
            get { return isVisibleQualityNameLeft3; }
            set
            {
                isVisibleQualityNameLeft3 = value;
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

        

             private bool isVisibleScoreLeft4;

        public bool IsVisibleScoreLeft4
        {
            get { return isVisibleScoreLeft4; }
            set
            {
                isVisibleScoreLeft4 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft4;

        public bool IsVisibleQualityNameLeft4
        {
            get { return isVisibleQualityNameLeft4; }
            set
            {
                isVisibleQualityNameLeft4 = value;
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

        

        private bool isVisibleScoreLeft5;

        public bool IsVisibleScoreLeft5
        {
            get { return isVisibleScoreLeft5; }
            set
            {
                isVisibleScoreLeft5 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft5;

        public bool IsVisibleQualityNameLeft5
        {
            get { return isVisibleQualityNameLeft5; }
            set
            {
                isVisibleQualityNameLeft5 = value;
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

        private bool isVisibleScoreLeft6;

        public bool IsVisibleScoreLeft6
        {
            get { return isVisibleScoreLeft6; }
            set
            {
                isVisibleScoreLeft6 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleQualityNameLeft6;

        public bool IsVisibleQualityNameLeft6
        {
            get { return isVisibleQualityNameLeft6; }
            set
            {
                isVisibleQualityNameLeft6 = value;
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

        #endregion


    }
}
