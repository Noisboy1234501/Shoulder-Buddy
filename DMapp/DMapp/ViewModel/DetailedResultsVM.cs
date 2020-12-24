using DMapp.Models;
using DMapp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using DMapp.Helpers;
using System.Collections.ObjectModel;

namespace DMapp.ViewModel
{
    class DetailedResultsVM :BaseViewModel
    {
        INavigation navigation;

        //Mock_DB mock_DB;
        public double SliderStep;
        public Command EndCommand { get; set; }
        public Command LeftButtonClickedCommand { get; set; }
        public Command RightButtonClickedCommand { get; set; }
        private int Mode;
        public DetailedResultsVM(INavigation nav, int passedSessionID,int mode)
        {
            InitializeProperties(nav, passedSessionID,mode);

            // used only to display first 3 options, test index, slider and criterias display
            //MockExtractData();

            sliderValue = 1;
            SliderStep = 1;
            ExtractData();
            SetSliderMinAndMax();
            
        }

        private void InitializeProperties(INavigation nav, int passedSessionID,int mode)
        {
            EndCommand = new Command(async () => await ExecuteEndCommand());
            LeftButtonClickedCommand = new Command(async () => await ExecuteLeftButtonClickedCommand());
            RightButtonClickedCommand = new Command(async () => await ExecuteRightButtonClickedCommand());
            //mock_DB = new Mock_DB();
            navigation = nav;
            sessionID = passedSessionID;  // ID of Desision session 
            optionNames = new List<string>();
            qualitiesNames = new List<string>();
            qualitiesImportance = new List<double>();
            weights = new List<List<double>>();
            weightsToDisplayColumn1 = new ObservableCollection<double>();
            weightsToDisplayColumn2 = new ObservableCollection<double>();
            weightsToDisplayColumn3 = new ObservableCollection<double>();
            Mode = mode;
        }

        // Readjusted the value so slider moves only by determined value (step by step)
        private double ReadjustSliderValue(double passedValue)
        {
            var newStep = Math.Round(passedValue/SliderStep);
            return newStep * SliderStep;
        }
        
        
        private void SetSliderMinAndMax()
        {
            SliderMin = 0;
            int optionsCount = options.Count();
            if(optionsCount % 3 !=0) { SliderMax = 1 + (int)optionsCount / 3; }
            else { SliderMax = (int)optionsCount / 3; }

        }

        // extracts data from database, so they can be accessed later.
        private void ExtractData() // change to private later
        {

            OptionNames.Clear();
            Weights.Clear();

            

            //Getting qualities names
            List<Quality> qualitiesForSession;
            if (Mode ==1)
            {
               qualitiesForSession = ManagerSQL.ReadQualities().Where(x => x.SessionID == SessionID).ToList();
            }
            else
            {
                 qualitiesForSession = TemporaryDb.Qualities;
            }
            
            QualitiesNames = qualitiesForSession.Select(x => x.Name).ToList();

            //Getting Qualities Importance
            QualitiesImportance = qualitiesForSession.Select(x => Math.Round(x.Importance,2)).ToList();

            //Getting option names and weights
            if(Mode == 1)
            {
                options = ManagerSQL.ReadOptions().Where(x => x.SessionID == SessionID).ToList();
            }
            else
            {
                options = TemporaryDb.Options;
            }

            List<Weight> WeightsForSession;
            if (Mode == 1)
            {
                WeightsForSession = ManagerSQL.ReadWeights().Where(x => x.SessionID == SessionID).ToList();
            }
            else
            {
                WeightsForSession = TemporaryDb.WeightClasses;
            }

            if (Mode == 0) { AdjustDataFromTempDB(WeightsForSession); }

            foreach (var option in options)
            {
                //Getting options names
                OptionNames.Add(option.Name);

                //Getting weights
                List<Weight> tempWeightList = WeightsForSession.Where(x => x.OptionID == option.OptionID).ToList();
                List<double> optionWeightsAmounts = new List<double>();
                foreach (var weight in tempWeightList)
                {
                    optionWeightsAmounts.Add(weight.Amount);
                }
                Weights.Add(optionWeightsAmounts);
            }
  
        }

        private void AdjustDataFromTempDB(List<Weight> WeightsForSession)
        {
            int numOfQualites = QualitiesNames.Count;
            int numOfAllWeights = WeightsForSession.Count;
            int numOfOptions = Options.Count;
            for(int i=0; i < numOfOptions; i++)
            {
                Options[i].OptionID = i + 1;
            }
            int counter = 0;
            int baseIndex = 1;
            int maxBase = numOfAllWeights / numOfOptions;
            for(int i = 0; i < numOfAllWeights; i++)
            {
                if(counter > maxBase-1) { baseIndex++; counter = 0; }
                WeightsForSession[i].OptionID = baseIndex;
                counter++;

            }
        }

        //Update display - depends on slider value
        private void UpdateDisplay()
        {
           
            Option1ToDisplay = "";
            Option2ToDisplay = "";
            Option3ToDisplay = "";
            OptionScore1ToDisplay = 0;
            OptionScore2ToDisplay = 0;
            OptionScore3ToDisplay = 0;
            WeightsToDisplayColumn1.Clear();
            WeightsToDisplayColumn2.Clear();
            WeightsToDisplayColumn3.Clear();
            int valueOfSlider = (int)sliderValue;
            //default values for frames and last row color
            IsVisibleScore2 = true;
            Column3FrameVisibility = true;
            Column3LastRowColor = "Beige";

            IsVisibleScore3 = true;
            Column4FrameVisibility = true;
            Column4LastRowColor = "Beige";


            int optionsDisplayStarterIndex = (3 * valueOfSlider) - 3;

            int indexAdjuster = 0;
            if (SliderValue == SliderMax)
            {
                if (Options.Count() % 3 == 1) { indexAdjuster = -2; }
                else if (Options.Count() % 3 == 2) { indexAdjuster = -1; }
            }

            int optionsDisplayFinishIndex = (3 * valueOfSlider) + indexAdjuster;
            // Adding first 3 option names to display list
            for (int i = optionsDisplayStarterIndex; i < optionsDisplayFinishIndex; i++)
            {

                if (i == optionsDisplayStarterIndex) { Option1ToDisplay = OptionNames[i]; }
                if (i == optionsDisplayStarterIndex + 1) { Option2ToDisplay = OptionNames[i]; }
                if (i == optionsDisplayStarterIndex + 2) { Option3ToDisplay = OptionNames[i]; }

            }


            // Adding first three list of weights (one for each option)

            int numOfOptionsToDisplay = 0;
            if (Option3ToDisplay.Equals("") && !Option2ToDisplay.Equals("")) { numOfOptionsToDisplay = 2; }
            else if (Option3ToDisplay.Equals("") && Option2ToDisplay.Equals("")) { numOfOptionsToDisplay = 1; }
            else { numOfOptionsToDisplay = 3; }

            int numOfQualities = QualitiesNames.Count();
            for (int i = 0; i < numOfOptionsToDisplay * numOfQualities; i++)
            {
                if (i >= 0 && i < numOfQualities) { weightsToDisplayColumn1.Add(Math.Round(Weights[optionsDisplayStarterIndex][i],2)); }
                else if (i >= numOfQualities && i < numOfQualities * 2) { weightsToDisplayColumn2.Add(Math.Round(Weights[optionsDisplayStarterIndex + 1][i - numOfQualities],2)); }
                else { weightsToDisplayColumn3.Add(Math.Round(Weights[optionsDisplayStarterIndex + 2][i - (numOfQualities * 2)],2)); }

            }

            List<List<double>> weightsToPass = new List<List<double>>();
            if (WeightsToDisplayColumn1.Count != 0) { weightsToPass.Add(WeightsToDisplayColumn1.ToList()); }
            if (WeightsToDisplayColumn2.Count != 0) { weightsToPass.Add(WeightsToDisplayColumn2.ToList()); }
            if (WeightsToDisplayColumn3.Count != 0) { weightsToPass.Add(WeightsToDisplayColumn3.ToList()); }

            List<double> optionsScore = new List<double>();

            //Calculating options scores by using DecisionSystem overloaded method
            optionsScore = DecisionSystem.ReturnResult(qualitiesImportance, weightsToPass);

            //Adding final score for each option, to display
            for (int i = 0; i < optionsScore.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        OptionScore1ToDisplay = Math.Round(optionsScore[i],2);
                        break;
                    case 1:
                        OptionScore2ToDisplay = Math.Round(optionsScore[i], 2);
                        break;
                    case 2:
                        OptionScore3ToDisplay = Math.Round(optionsScore[i], 2);
                        break;
                    default:
                        break;
                }
            }
            if (Option2ToDisplay == "") { IsVisibleScore2 = false; Column3FrameVisibility = false; Column3LastRowColor = "Transparent"; }
            if (Option3ToDisplay == "") { IsVisibleScore3 = false; Column4FrameVisibility = false; Column4LastRowColor = "Transparent"; }
        }

        //Mock data extraction method for debugging purposes:
        private void MockExtractData()
        {
            options = Mock_Results.ReturnOptions();
            OptionNames = Mock_Results.ReturnOptionNamesDividableBy3();
            QualitiesNames = Mock_Results.ReturnQualitiesNames();
            QualitiesImportance = Mock_Results.ReturnQualitiesImportance();
            Weights = Mock_Results.ReturnWeightsOptionsDividableBy3();
            List<double> optionsScore = new List<double>();
            optionsScore = Mock_Results.ReturnOptionsScoreDiv3();

            //Adding 3 first options to display
            for (int i=0; i<3;i++)
            {
                switch (i)
                {
                    case 0:
                        Option1ToDisplay = OptionNames[i];
                        break;
                    case 1:
                        Option2ToDisplay = OptionNames[i];
                        break;
                    case 2:
                        Option3ToDisplay = OptionNames[i];
                        break;
                    default:
                        break;
                }
            }

            //Adding final score for each option, to display
            for(int i=0; i<3;i++)
            {
                switch (i)
                {
                    case 0:
                        optionScore1ToDisplay = optionsScore[i];
                        break;
                    case 1:
                        optionScore2ToDisplay = optionsScore[i];
                        break;
                    case 2:
                        optionScore3ToDisplay = optionsScore[i];
                        break;
                    default:
                        break;
                }
            }
            //Adding appropriate weight for each option to display
            for(int i=0; i< Weights.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        foreach(var weight in Weights[i])
                        {
                            WeightsToDisplayColumn1.Add(weight);
                        }
                        break;
                    case 1:
                        foreach (var weight in Weights[i])
                        {
                            WeightsToDisplayColumn2.Add(weight);
                        }
                        break;
                        
                    case 2:
                        foreach (var weight in Weights[i])
                        {
                            WeightsToDisplayColumn3.Add(weight);
                        }
                        break;
                        
                    default:
                        break;
                }
            }
        }

        //This method adjust the hight of the row with qualities with respect to number of qualities
        public double ReturnHightOfRowWithList()
        {
            return (double)qualitiesNames.Count() * 44;
        }

        //This method adjust the hight of the row with options with respect of the lenght of options' names
        public void ChangeHightOfRowWithOptions()
        {
            int NumOfCharsOfTheLongestOption = Math.Max(Option1ToDisplay.Length, Option2ToDisplay.Length);
            NumOfCharsOfTheLongestOption = Math.Max(NumOfCharsOfTheLongestOption, Option3ToDisplay.Length);
            if(NumOfCharsOfTheLongestOption >= 1 && NumOfCharsOfTheLongestOption <= 17) { OptionsRowHeight= 43; }
            else if (NumOfCharsOfTheLongestOption > 17 && NumOfCharsOfTheLongestOption < 34) { OptionsRowHeight = 58; }
            else if (NumOfCharsOfTheLongestOption >= 34 && NumOfCharsOfTheLongestOption < 51) { OptionsRowHeight = 73; }
            else if (NumOfCharsOfTheLongestOption >= 51 && NumOfCharsOfTheLongestOption <= 68) { OptionsRowHeight = 88; }
            
        }
        // command methods
        #region Commands
        private Task ExecuteRightButtonClickedCommand()
        {
            if(SliderValue < SliderMax) { SliderValue++; }
            return Task.CompletedTask;
        }

        private Task ExecuteLeftButtonClickedCommand()
        {
            if (SliderValue > SliderMin +1) { SliderValue--; } 
            return Task.CompletedTask;
        }

        // It takes user back to the root page.
        private async Task ExecuteEndCommand()
        {
            await navigation.PopToRootAsync();
        }

        #endregion
        //Properties used behind xaml:

        #region backend properties

        private double optionsRowHeight;

        public double OptionsRowHeight
        {
            get { return optionsRowHeight; }
            set { optionsRowHeight = value;
                OnPropertyChanged();
            }
        }


        private List<Option> options;

        public List<Option> Options
        {
            get { return options; }
            set { options = value;  }
        }

        private List<string> optionNames;

        public List<string> OptionNames
        {
            get { return optionNames; }
            set { optionNames = value; }
        }

        private List<List<double>> weights;
        public List<List<double>> Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        private int sessionID;

        public int SessionID //Session ID, necessary to get options, qualities connected with this session.
        {
            get { return sessionID; }
            set { sessionID = value; }
        }
        #endregion

        //Properties to bind from xaml:

        #region BindableLists

        private List<string> qualitiesNames;

        public List<string> QualitiesNames
        {
            get { return qualitiesNames; }
            set { qualitiesNames = value; }
        }

        private List<double> qualitiesImportance;

        public List<double> QualitiesImportance
        {
            get { return qualitiesImportance; }
            set { qualitiesImportance = value; }
        }

        #endregion

        #region OptionsScoreToDisplay

        private double optionScore1ToDisplay;

        public double OptionScore1ToDisplay
        {
            get { return optionScore1ToDisplay; }
            set { optionScore1ToDisplay = value;
                OnPropertyChanged();
            }
        }

        private double optionScore2ToDisplay;

        public double OptionScore2ToDisplay
        {
            get { return optionScore2ToDisplay; }
            set { optionScore2ToDisplay = value;
                OnPropertyChanged();
            }
        }

        private double optionScore3ToDisplay;

        public double OptionScore3ToDisplay
        {
            get { return optionScore3ToDisplay; }
            set { optionScore3ToDisplay = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region OptionsToDisplay
        private string option1ToDisplay;

        public string Option1ToDisplay
        {
            get { return option1ToDisplay; }
            set { option1ToDisplay = value;
                OnPropertyChanged();
            }
        }

        private string option2ToDisplay;

        public string Option2ToDisplay
        {
            get { return option2ToDisplay; }
            set
            {
                option2ToDisplay = value;
                OnPropertyChanged();
            }
        }

        private string option3ToDisplay;

        public string Option3ToDisplay
        {
            get { return option3ToDisplay; }
            set
            {
                option3ToDisplay = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region WeightsToDisplayColumns
        private ObservableCollection<double> weightsToDisplayColumn1;

        public ObservableCollection<double> WeightsToDisplayColumn1
        {
            get { return weightsToDisplayColumn1; }
            set { weightsToDisplayColumn1 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> weightsToDisplayColumn2;

        public ObservableCollection<double> WeightsToDisplayColumn2
        {
            get { return weightsToDisplayColumn2; }
            set
            {
                weightsToDisplayColumn2 = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<double> weightsToDisplayColumn3;

        public ObservableCollection<double> WeightsToDisplayColumn3
        {
            get { return weightsToDisplayColumn3; }
            set
            {
                weightsToDisplayColumn3 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region SliderProperties
        private double sliderValue;

        public double SliderValue
        {
            get { return sliderValue; }
            set {
                var readjustedResult = ReadjustSliderValue(value);
                if(readjustedResult == 0) { sliderValue = 1; }
                else { sliderValue = readjustedResult; }

                Index = $"{sliderValue}/{SliderMax}";
                //MockExtractData();
                UpdateDisplay();
                ChangeHightOfRowWithOptions();
                OnPropertyChanged();
                  }
        }

        private double sliderMax;

        public double SliderMax
        {
            get { return sliderMax; }
            set { sliderMax = value; }
        }

        private double sliderMin;

        public double SliderMin
        {
            get { return sliderMin; }
            set { sliderMin = value; }
        }

        public string index;
        public string Index
        {
            get { return index; }
            set { index = value;
                  OnPropertyChanged();
            }
        }


        #endregion

        #region ScoreLabelProperties
        private bool isVisibleScore2;

        public bool IsVisibleScore2
        {
            get { return isVisibleScore2; }
            set { isVisibleScore2 = value;
                OnPropertyChanged();
            }
        }

        private bool isVisibleScore3;

        public bool IsVisibleScore3
        {
            get { return isVisibleScore3; }
            set { isVisibleScore3 = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region ColumnVisibilityProperties

        private bool column4FrameVisibility;

        public bool  Column4FrameVisibility
        {
            get { return column4FrameVisibility; }
            set { column4FrameVisibility = value;
                OnPropertyChanged();
            }
        }

        private bool column3FrameVisibility;

        public bool Column3FrameVisibility
        {
            get { return column3FrameVisibility; }
            set { column3FrameVisibility = value;
                OnPropertyChanged();
            }
        }

        private string column4LastRowColor;

        public string Column4LastRowColor
        {
            get { return column4LastRowColor; }
            set { column4LastRowColor = value;
                OnPropertyChanged();
            }
        }

        private string column3LastRowColor;

        public string Column3LastRowColor
        {
            get { return column3LastRowColor; }
            set { column3LastRowColor = value;
                OnPropertyChanged();
            }
        }

        #endregion













    }
}
