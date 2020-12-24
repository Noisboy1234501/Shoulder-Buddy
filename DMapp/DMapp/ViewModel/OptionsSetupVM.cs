using DMapp.Helpers;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DMapp.ViewModel
{
    class OptionsSetupVM : BaseViewModel
    {
        public Command PlusButtonClickedCommand { get; set; }
        public Command ContinueButtonClickedCommand { get; set; }
        public Command DeleteCommand { get; set; }
        INavigation navigation;
        private bool isUpdating;
        private string previousName;

        public OptionsSetupVM(INavigation navi)
        {
            optionsList = new ObservableCollection<string>();
            PlusButtonClickedCommand = new Command(async () => await ExecutePlusButtonClickedCommand());
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            DeleteCommand = new Command<string>((x) => ExecuteDeleteCommand(x));
            navigation = navi;
            ButtonColor = "LightGreen";
        }

        private void ExecuteDeleteCommand(string name)
        {
            int numOfAllOptions = optionsList.Count;
            for (int i = 0; i < numOfAllOptions; i++)
            {
                if (optionsList[i] == name)
                {
                    optionsList.RemoveAt(i);
                    TemporaryDb.optionNames.RemoveAt(i);
                    numOfAllOptions -= 1;
                    isUpdating = false;
                    // changing saved data appropriately

                    // slider values
                    SelectedItem = null;
                    OptionsChoiceSliderValuesHolder.SetArraySize(TemporaryDb.optionNames.Count * TemporaryDb.qualityNames.Count, 1, i+1, 0);
                    CalculateNumberOfChoices();
                }
            }
        }

        public void LoadCurrentOptions()
        {
            var names = TemporaryDb.optionNames;
            ObservableCollection<string> newNames = new ObservableCollection<string>();
            foreach (var name in names) { newNames.Add(name); }
            OptionsList = newNames;
        }

        private async Task ExecuteContinueButtonClickedCommand()
        {
                if (optionsList.Count >= 2)
                {
                // consider push modal async here - going back and change qualities will make a mess with qualities choices
                    await navigation.PushAsync(new SessionSettingsPage(navigation));
                }

                else { await App.Current.MainPage.DisplayAlert("Not enough options", "Enter at least two options, please", "Ok"); } 
        }

        

        private Task ExecutePlusButtonClickedCommand()
        {
            //check if options has appropriate num of characters, the same for qualities!
            // add number of microdecisions which will be required. Do sth with number of qualities and option and accuracy relationship! moze slider z probability of good decision and time efficiency - jak wzrasta liczba opcji to spada time efficiency...
            if(!String.IsNullOrWhiteSpace(optionName))
            {
                try
                {
                    if(optionName.Length > 60) { throw new Exception("Option's name is too long. 60 characters max."); }
                    if(String.IsNullOrWhiteSpace(optionName)) { throw new Exception("Name consists of white space only."); }
                    foreach (var option in OptionsList)
                    {
                        if (option == optionName && optionName != previousName ) { throw new Exception("Option is already on the list"); }
                    }
                    if(!isUpdating)
                    {
                        OptionsList.Add(optionName);
                        TemporaryDb.optionNames.Add(optionName);
                        OptionName = "";
                        CalculateNumberOfChoices();
                        OptionsChoiceSliderValuesHolder.SetArraySize(TemporaryDb.optionNames.Count * TemporaryDb.qualityNames.Count, 1, 0, 0);
                    }
                    else
                    {
                        if(optionName == previousName) {
                            OptionName = "";
                            isUpdating = false;
                            ButtonColor = "LightGreen";
                            SelectedItem = null;
                        }
                        else
                        {
                            int numOfAllOptions = optionsList.Count;
                            for (int i = 0; i < numOfAllOptions; i++)
                            {
                                if (optionsList[i] == previousName) { OptionsList[i] = optionName; TemporaryDb.optionNames[i] = optionName; }
                            }

                            OptionName = "";
                            isUpdating = false;
                            ButtonColor = "LightGreen";
                            SelectedItem = null;
                        }
                        
                    }
                    
                }
                catch(Exception ex) { App.Current.MainPage.DisplayAlert("Something went wrong", ex.Message, "Ok"); }
            }
            else {
                OptionName = "";
                isUpdating = false;
                ButtonColor = "LightGreen";
                SelectedItem = null;
                LoadCurrentOptions(); // it would be unncecessary but unfortunately for some reason selectedItem do not want to be displayed as null. In line 82 it just worked.
                  }
            return Task.CompletedTask;
            
        }

        public void CalculateNumberOfChoices()
        {
            double optionsNumber = OptionsList.Count;
            double qualitiesNumber = TemporaryDb.qualityNames.Count;
            double number = (((Math.Pow(qualitiesNumber, 2)) - qualitiesNumber) / 2) + (qualitiesNumber *optionsNumber) ;
            NumOfChoices = $"{number} choices";
        }

        #region BindableProperties

        private string buttonColor;

        public string ButtonColor
        {
            get { return buttonColor; }
            set { buttonColor = value;
                OnPropertyChanged();
            }
        }


        private string optionName;

        public string OptionName
        {
            get { return optionName; }
            set
            {
                optionName = value;
                OnPropertyChanged();
            }
        }


        private string selectedItem;

        public string SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value;
                if(!String.IsNullOrEmpty(value))
                {
                    previousName = value;
                    OptionName = value;
                    isUpdating = true;
                    ButtonColor = "DodgerBlue";
                }
                
                OnPropertyChanged();
            }
        }


        private ObservableCollection<string> optionsList;

        public ObservableCollection<string>  OptionsList
        {
            get { return optionsList; }
            set { optionsList = value;
                OnPropertyChanged();
            }
        }


        private string numOfChoices;

        public string NumOfChoices
        {
            get { return numOfChoices; }
            set
            {
                numOfChoices = value;
                OnPropertyChanged();
            }
        }

        #endregion



    }
}
