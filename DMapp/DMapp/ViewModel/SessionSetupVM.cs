using DMapp.Models;
using DMapp.Services;
using DMapp.View;
using DMapp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DMapp.ViewModel
{
    class SessionSetupVM : BaseViewModel
    {
        private int plusButton_CycleIndex;
        private List<SessionCategory> sessionCategoires_fromDb;
        public Command PlusButtonClickedCommand { get; set; }
        public Command CancelButtonClickedCommand { get; set; }
        public Command ContinueButtonClickedCommand { get; set; }
        INavigation navigation;
        public SessionSetupVM(INavigation navi)
        {
            InitializeProperties(navi);
        }

        private void InitializeProperties(INavigation navi)
        {
            
            navigation = navi;
            IsCategoryEntryVisible = false;
            IsContinueButtonVisible = true;
            PlusButtonClickedCommand = new Command(async () => await ExecutePlusButtonClickedCommand());
            CancelButtonClickedCommand = new Command(async () => await ExecuteCancelButtonClickedCommand());
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            InitializationCounter.numOfQualitiesChoicesVMInitialized = InitializationCounter.numOfOptionsChoicesVMInitialized =
                InitializationCounter.numOfSessionSettingsVMInitialized = 0;  // we create decision session for the first time
            // so we are making sure that it will have set  default values on pages.
            LoadSessionCategories();
            
        }

       

        private void LoadSessionCategories()
        {
            sessionCategoires_fromDb = ManagerSQL.ReadSessionCategories();
            var sessionCategoriesNames = new List<string>();
            foreach(var sessionCategory in sessionCategoires_fromDb)
            {
                sessionCategoriesNames.Add(sessionCategory.CategoryName);
            }
            SessionCategoriesList = sessionCategoriesNames;

        }

        #region Commands

        private async Task ExecuteContinueButtonClickedCommand()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(sessionTitle) && sessionTitle != "" && goal != null && goal!="" && !String.IsNullOrWhiteSpace(goal) && choosenCategoryName != null && choosenCategoryName != "")
                {
                    var allSessionsTitles = ManagerSQL.ReadDecisionSessions().Select(x => x.Title).ToList();
                    
                    
                        foreach (var title in allSessionsTitles)
                        {
                            if (title == sessionTitle) { throw new Exception("Such Title already exists"); }
                        }
                        TemporaryDb.sessionTitle = sessionTitle;
                        TemporaryDb.SessionCategoryName = choosenCategoryName;
                        TemporaryDb.goal = Goal;
                         await navigation.PushAsync(new QualitiesSetupPage(navigation));
                }
                else
                {
                   await App.Current.MainPage.DisplayAlert("Empty field", "Title, goal or category is empty or whitespace", "Ok");
                }
            }
            catch(Exception ex) 
                 {await App.Current.MainPage.DisplayAlert("Wrong entry", ex.Message, "Ok"); }

            
        }
        private Task ExecutePlusButtonClickedCommand()
        {
            if(plusButton_CycleIndex == 0)
            {
                IsCategoryEntryVisible = true;
                IsContinueButtonVisible = false;
                IsCancelButtonVisible = true;
                plusButton_CycleIndex++;
            }
            else if(plusButton_CycleIndex == 1)
            {
                if (newCategoryName != null && newCategoryName != "")
                {
                    try
                    {
                        foreach (var sessionCategory in sessionCategoires_fromDb)
                        {
                            if (sessionCategory.CategoryName == newCategoryName) { throw new Exception("Category name already exists"); }
                        }

                        if(String.IsNullOrWhiteSpace(newCategoryName)) { throw new Exception( "Name can not be white space" ); }

                        SessionCategory newCategory = new SessionCategory();
                        newCategory.CategoryName = newCategoryName;
                        ManagerSQL.InsertSessionCategory(newCategory);
                        IsCategoryEntryVisible = false;
                        IsContinueButtonVisible = true;
                        IsCancelButtonVisible = false;
                        LoadSessionCategories();
                        NewCategoryName = "";
                        ChoosenCategoryName = newCategory.CategoryName;
                        plusButton_CycleIndex = 0;
                    }
                    catch(Exception ex)  {
                        App.Current.MainPage.DisplayAlert(ex.Message, "Please insert other name.", "Ok");
                    }
                    

                    
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Empty entry", "Category can't have empty name.", "Ok");
                }
            }
            
            return Task.CompletedTask;
        }

        private Task ExecuteCancelButtonClickedCommand()
        {
            plusButton_CycleIndex = 0;
            IsCategoryEntryVisible = false;
            IsContinueButtonVisible = true;
            IsCancelButtonVisible = false;
            NewCategoryName = "";
            return Task.CompletedTask;
        }
        #endregion
        #region bindableProperties

        private string choosenCategoryName;

        public string ChoosenCategoryName
        {
            get { return choosenCategoryName; }
            set {if(value !=null)
                {
                    choosenCategoryName = value;
                    OnPropertyChanged();
                }
                
            }
        }


        private string sessionTitle;

        public string SessionTitle
        {
            get { return sessionTitle; }
            set { sessionTitle = value;
                OnPropertyChanged();
            }
        }

        private string goal;

        public string Goal
        {
            get { return goal; }
            set { goal = value;
                OnPropertyChanged();
            }
        }

        private bool isContinueButtonVisible;

        public bool IsContinueButtonVisible
        {
            get { return isContinueButtonVisible; }
            set { isContinueButtonVisible = value;
                OnPropertyChanged();
            }
        }

        private bool  isCategoryEntryVisible;

        public bool  IsCategoryEntryVisible
        {
            get { return isCategoryEntryVisible; }
            set { isCategoryEntryVisible = value;
                OnPropertyChanged();
            }
        }

        private string newCategoryName;

        public string NewCategoryName
        {
            get { return newCategoryName; }
            set { newCategoryName = value;
                OnPropertyChanged();
            }
        }

        private List<string> sessionCategoriesList;

        public List<string> SessionCategoriesList
        {
            get { return sessionCategoriesList; }
            set { sessionCategoriesList = value;
                  OnPropertyChanged();
            }
        }

        private bool isCancelButtonVisible;

        public bool IsCancelButtonVisible
        {
            get { return isCancelButtonVisible; }
            set { isCancelButtonVisible = value;
                OnPropertyChanged();
            }
        }





        #endregion


    }
}
