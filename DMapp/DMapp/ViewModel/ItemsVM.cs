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
          
            BestScore = 15;

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
            SelectedIndex = 0;
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

                string choosenCategory = CategoriesToDisplay[selectedIndex].CategoryName;

                if (choosenCategory != "All")
                {
                    int choosenCategoryID = CategoriesToDisplay.Where(x => x.CategoryName == choosenCategory).Select(x => x.SessionCategoryID).FirstOrDefault();
                   
                    notes = notes.Where(x => x.SessionCategoryID == choosenCategoryID).ToList();
                }
                

                foreach (var note in notes)
                {
                    Decisions.Add(note);
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


        private double bestScore;

        public double BestScore
        {
            get { return bestScore; }
            set { bestScore = value; }
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
                selectedSession = value;

                //if (selectedSession != null)
                //{
                //    CallLoadDecisionSessionCommand(); //in the future add specific page which exist and we want to edit it.
                //}


                OnPropertyChanged();

            }
        }

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value;
                CallLoadDecisionsCommand();
                OnPropertyChanged();
            }
        }







        

      

        #endregion


        #region Methods


       

        /// <summary>
        /// Method for update the chart data.
        /// </summary>
        //private void UpdateChartData()
        //{
        //    ChartData.Clear();
        //    TotalBalance = 0;

        //    var incomeCollection = ListItems.Where(item => item.IsCredited).ToList();
        //    var expenseCollection = ListItems.Where(item => !item.IsCredited).ToList();

        //    for (int i = 0; i < incomeCollection.Count; i++)
        //    {
        //        TotalBalance += incomeCollection[i].Amount;
        //        TotalBalance -= expenseCollection[i].Amount;
        //        ChartData.Add(new TransactionChartData(xValues[i], incomeCollection[i].Amount, expenseCollection[i].Amount, 4));
        //    }
        //}

        /// <summary>
        /// Invoked when an item is selected from the my wallet page.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private void NavigateToNextPage(object selectedItem)
        {
            // Do something
        }
        #endregion


    }
}
