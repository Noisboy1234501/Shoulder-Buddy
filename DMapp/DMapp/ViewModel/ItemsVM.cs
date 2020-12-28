using DMapp.Helpers;
using DMapp.Models;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Diagnostics;
using System.Linq;

using System.Threading.Tasks;
using Xamarin.Forms;



using Xamarin.Forms.Internals;



namespace DMapp.ViewModel
{
    /// <summary>
    /// ViewModel for my wallet page.
    /// </summary>
    [Preserve(AllMembers = true)]
    class ItemsVM : BaseViewModel
    {
        INavigation navigation;

        

        // Titles of these decisions sessions will be displayed in list.
        public ObservableCollection<DecisionSession> Decisions { get; set; }
        public Command LoadDecisionsCommand { get; set; }

        public Command NewDecisionCommand { get; set; }
        public Command DeleteCommand { get; set; }
        public Command DetailsCommand { get; set; }

       
        

        private List<DecisionSession> notes;

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="MyWalletViewModel" /> class.
        /// </summary>

        public ItemsVM(INavigation nav)
        {
            Title = "Browse";
            navigation = nav;
            InitializeFields();
           
        }

        #endregion

        private void InitializeFields()
        {
            //var navipaths = navigation.NavigationStack;
            Decisions = new ObservableCollection<DecisionSession>();
            LoadDecisionsCommand = new Command( () => ExecuteLoadDecisionsCommand());
            NewDecisionCommand = new Command(async () => await ExecuteNewDecisionCommand());
            
            DeleteCommand = new Command<DecisionSession>((x) => ExecuteDeleteCommand(x));
            DetailsCommand = new Command<DecisionSession>((x) => ExecuteDetailsCommand(x));
          
            

        }

        private void ExecuteDetailsCommand(DecisionSession x)
        {
            navigation.PushAsync(new GeneralResultsPage(navigation, 1, x.SessionID));
        }

        private void ExecuteDeleteCommand(DecisionSession sessionObject)
        {
            var allSessions = ManagerSQL.ReadDecisionSessions();
            DecisionSession sessionToDelete = new DecisionSession();
            foreach(var session in allSessions)
            {
                if(session.Title == sessionObject.Title) { sessionToDelete = session; break; }
            }
            List<Option> optionsToDelete = ManagerSQL.ReadOptions().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();
            List<Quality> qualitiesToDelete = ManagerSQL.ReadQualities().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();
            List<Weight> weightsToDelete = ManagerSQL.ReadWeights().Where(x => x.SessionID == sessionToDelete.SessionID).ToList();

            ManagerSQL.DeleteDecisionSession(sessionToDelete);
            foreach(var option in optionsToDelete) { ManagerSQL.DeleteOption(option); }
            foreach (var quality in qualitiesToDelete) { ManagerSQL.DeleteQuality(quality); }
            foreach(var weight in weightsToDelete) { ManagerSQL.DeletetWeight(weight); }

            var test1 = ManagerSQL.ReadDecisionSessions();
            var test2 = ManagerSQL.ReadOptions();
            var test3 = ManagerSQL.ReadQualities();
            var test4 = ManagerSQL.ReadWeights();

            ExecuteLoadDecisionsCommand();
        }

        public void loadPickerOptions()
        {
            
            CategoriesToDisplay = new ObservableCollection<SessionCategory>();
            CategoriesToDisplay.Clear();
            CategoriesToDisplay.Add(new SessionCategory
            {
                CategoryName = "All"
            });


            var temp = ManagerSQL.ReadSessionCategories();
            foreach(var cat in temp) { CategoriesToDisplay.Add(cat); }
           
           
        }

        public void PageAppeared()
        {
             loadPickerOptions();
            
            OptionsChoiceSliderValuesHolder.SliderValues = null;
            QualitiesChoiceSliderValuesHolder.SliderValues = null;
            InitializationCounter.numOfQualitiesChoicesVMInitialized = 0;
            InitializationCounter.numOfOptionsChoicesVMInitialized = 0;
            QualitiesChoiceSliderValuesHolder.oldSequence = null;
            SelectedCategoryIndex = 0;
        }
    

        #region Commands
        private async void CallLoadDecisionSessionCommand()
        {
            await ExecuteLoadDecisionSessionCommand();
        }

        private  void CallLoadDecisionsCommand()
        {
             ExecuteLoadDecisionsCommand();
        }
        private void ExecuteLoadDecisionsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Decisions.Clear();

                // make reading this data async!


                //Mock_DB mockDB = new Mock_DB();
                notes = ManagerSQL.ReadDecisionSessions().ToList();

                string choosenCategory = CategoriesToDisplay[selectedCategoryIndex].CategoryName;

                if (choosenCategory != "All")
                {
                    int choosenCategoryID = CategoriesToDisplay.Where(x => x.CategoryName == choosenCategory).Select(x => x.SessionCategoryID).FirstOrDefault();
                   
                    notes = notes.Where(x => x.SessionCategoryID == choosenCategoryID).ToList();
                }
                

                foreach (var note in notes)
                {
                    Decisions.Add(note);
                }

                if(Decisions.Count != 0)
                {
                    selectedSession = Decisions[0];
                    PrepareChartData();
                }

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        private void PrepareChartData()
        {

            var options = ManagerSQL.ReadOptions().Where(x => x.SessionID == selectedSession.SessionID).ToList();
            var qualitiesImportance = ManagerSQL.ReadQualities().Where(x => x.SessionID == selectedSession.SessionID).Select(x => x.Importance).ToList();
            var weights = ManagerSQL.ReadWeights().Where(x => x.SessionID == selectedSession.SessionID).Select(x => x.Amount).ToList();
            List<List<double>> weightsToPass = new List<List<double>>();
            int cycleCounter = 1;
            int numOfQualities = qualitiesImportance.Count;
            foreach(var option in options)
            {
                List<double> weightsForOneOption = new List<double>();
                for(int i = (cycleCounter-1)*numOfQualities; i < numOfQualities*cycleCounter; i++)
                {
                    weightsForOneOption.Add(weights[i]);
                   
                }
                cycleCounter++;
                weightsToPass.Add(weightsForOneOption);
               
                
            }

            var optionsScore = DecisionSystem.ReturnResult(qualitiesImportance, weightsToPass).ToList();
            int counter = 0;
            var temp = new ObservableCollection<ChartDataModel>();
            foreach (var option in options)
            {
                temp.Add(new ChartDataModel
                {
                     OptionTitle = option.Name,
                    FinalScore = optionsScore[counter]
                });
                counter++;
            }

            double sumOfScore = 0;
            foreach(var tempItem in temp) { sumOfScore += tempItem.FinalScore; }
            foreach(var tempItem in temp)
            {
                tempItem.FinalScore /= sumOfScore;
                tempItem.FinalScore *= 100;
                tempItem.FinalScore = Math.Round(tempItem.FinalScore, 1);
                tempItem.FinalScoreString = tempItem.FinalScore.ToString();
                
            }

            double tester = 0;
            string tempbest = "";
            //Find best option
            foreach(var item in temp)
            {
                if(item.FinalScore > tester) { tester = item.FinalScore; tempbest = item.OptionTitle; }
            }

            BestOption = tempbest;
            ChartData = temp;
        }

        private async Task ExecuteNewDecisionCommand() 
        {
            TemporaryDb.CleanAllData();
            CurrentIndexHolder.QualitiesCurrentIndex = 1;
            QualitiesChoiceSliderValuesHolder.SliderValues = null;
            await navigation.PushAsync(new SessionSetupPage(navigation));

        }
        private async Task ExecuteLoadDecisionSessionCommand()
        {
            await navigation.PushAsync(new GeneralResultsPage(navigation, 1, selectedSession.SessionID));
        }

        #endregion

        #region bindable properties


        private string bestOption;

        public string BestOption
        {
            get { return bestOption; }
            set { bestOption = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ChartDataModel> chartData;

        public ObservableCollection<ChartDataModel> ChartData
        {
            get { return chartData; }
            set { chartData = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<SessionCategory> categoriesToDisplay;

        public ObservableCollection<SessionCategory> CategoriesToDisplay
        {
            get { return categoriesToDisplay; }
            set { categoriesToDisplay = value;
                OnPropertyChanged();
            }
        }



        private DecisionSession selectedSession;
        public DecisionSession SelectedSession
        {
            get { return selectedSession; }
            set
            {
                if(value != null)
                {
                    selectedSession = value;

                    int numOfDecisions = Decisions.Count();
                    for (int i = 0; i < numOfDecisions; i++) { if(Decisions[i] == value) { SelectedIndex = i; } }

                    PrepareChartData();
                    OnPropertyChanged();
                }
                

            }
        }

        private int selectedCategoryIndex;

        public int SelectedCategoryIndex
        {
            get { return selectedCategoryIndex; }
            set { selectedCategoryIndex = value;
                CallLoadDecisionsCommand();
                OnPropertyChanged();
            }
        }

        private int selectedIndex;

        public int SelectedIndex //index selected in decision list, used for chart explode
        {
            get { return selectedIndex; }
            set { selectedIndex = value;
                OnPropertyChanged();
            }
        }












        #endregion





    }
}
