using DMapp.Helpers;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DMapp.ViewModel
{
    class QualitiesSetupVM : BaseViewModel
    {
        INavigation navigation;

        public Command PlusButtonClickedCommand { get; set; }
        public Command ContinueButtonClickedCommand { get; set; }
        
        private string previousName;
        private bool isUpdating;


        public Command DeleteCommand { get; set; }
       
        public QualitiesSetupVM(INavigation navi)
        {
            navigation = navi;
            qualitiesList = new ObservableCollection<string>();

            PlusButtonClickedCommand = new Command(async () => await ExecutePlusButtonClickedCommand());
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            DeleteCommand = new Command<string>((x) => ExecuteDeleteCommand(x));
            ButtonColor = "LightGreen";
        }

        private void ExecuteDeleteCommand(string name)
        {
            int numOfAllQualities = qualitiesList.Count;
            for(int i = 0; i < numOfAllQualities; i++)
            {
                if (qualitiesList[i] == name)
                 { qualitiesList.RemoveAt(i);
                    TemporaryDb.qualityNames.RemoveAt(i);
                    numOfAllQualities -= 1;
                    isUpdating = false;
                    // reseting data connected with qualities - in the future do it this way not to reset data.
                    TemporaryDb.qualitiesImportance = new List<double>();
                    TemporaryDb.Qualities = new List<Models.Quality>();
                    SelectedItem = null;
                    int numOFQualities = qualitiesList.Count;
                    List<string> Sequence = DecisionSystem.GetQualitiesComparisonSequence(numOFQualities);
                    QualitiesChoiceSliderValuesHolder.SetArraySize(Sequence, i);
                    OptionsChoiceSliderValuesHolder.SetArraySize(TemporaryDb.optionNames.Count * TemporaryDb.qualityNames.Count, 0, 0, i+1);
                    CalculateNumberOfChoices();
                }
            }
        }

        private async Task ExecuteContinueButtonClickedCommand()
        {
                if (qualitiesList.Count != 0)
                {
                await navigation.PushAsync(new OptionsSetupPage(navigation));
                }
                else { await App.Current.MainPage.DisplayAlert("Not enough qualities", "Enter at least one quality, please", "Ok"); }
        }
        private Task ExecutePlusButtonClickedCommand()
        {
            if (!String.IsNullOrWhiteSpace(qualityName))
            {
                try
                {
                    if (qualityName.Length > 17) { throw new Exception("Quality's name is too long. 17 characters max."); }
                    foreach (var quality in qualitiesList)
                    {
                        if (quality == qualityName && qualityName != previousName) { throw new Exception("Quality is already on the list"); }
                    }
                    if(!isUpdating)
                    {
                        qualitiesList.Add(qualityName);
                        double qualitiesNum = qualitiesList.Count;
                        double numOfChoicesByQualities = (((Math.Pow(qualitiesNum, 2)) - qualitiesNum) / 2);
                        //QualitiesChoiceSliderValuesHolder.SetArraySize((int)numOfChoicesByQualities); 
                        TemporaryDb.qualityNames.Add(qualityName);
                        QualityName = "";
                        CalculateNumberOfChoices();
                        OptionsChoiceSliderValuesHolder.SetArraySize(TemporaryDb.optionNames.Count * TemporaryDb.qualityNames.Count, 0, 0, 0);
                        int numOFQualities = qualitiesList.Count;
                        List<string> Sequence = DecisionSystem.GetQualitiesComparisonSequence(numOFQualities);
                        QualitiesChoiceSliderValuesHolder.SetArraySize(Sequence, 0);
                    }
                    else
                    {
                        if(qualityName != previousName)
                        {
                            int numOfAllQualities = qualitiesList.Count;
                            for (int i = 0; i < numOfAllQualities; i++)
                            {
                                if (qualitiesList[i] == previousName) { qualitiesList[i] = qualityName; TemporaryDb.qualityNames[i] = qualityName; }
                            }
                            ButtonColor = "LightGreen";
                            QualityName = "";
                            isUpdating = false;
                            SelectedItem = null;
                        }
                        else
                        {
                            
                            QualityName = "";
                            isUpdating = false;
                            SelectedItem = null;

                        }
                        

                    }
                    

                }
                catch (Exception ex) { App.Current.MainPage.DisplayAlert("Something went wrong",ex.Message, "Ok"); }
            }
            return Task.CompletedTask;
        }

        public void CalculateNumberOfChoices()
        {
            double qualitiesNumber = QualitiesList.Count;
            double optionsNumber = TemporaryDb.optionNames.Count;
            double number = (((Math.Pow(qualitiesNumber, 2)) - qualitiesNumber) / 2) + (qualitiesNumber *  optionsNumber) ;
            NumOfChoices = $"{number} choices";
        }


        public void LoadCurrentOptions()
        {
           
            var names = TemporaryDb.qualityNames;
            ObservableCollection<string> newNames = new ObservableCollection<string>();
            foreach (var name in names) { newNames.Add(name); }
            QualitiesList = newNames;
        }

        #region BindableProperties

        private string buttonColor;

        public string ButtonColor
        {
            get { return buttonColor; }
            set
            {
                buttonColor = value;
                OnPropertyChanged();
            }
        }

        private string selectedItem;

        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                if (!String.IsNullOrEmpty(value))
                {
                    previousName = value;
                     QualityName = value;
                    isUpdating = true;
                    ButtonColor = "DodgerBlue";
                }
                else { ButtonColor = "LightGreen"; }

                OnPropertyChanged();
            }
        }


        private string qualityName;

        public string QualityName
        {
            get { return qualityName; }
            set { qualityName = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> qualitiesList;

        public ObservableCollection<string> QualitiesList
        {
            get { return qualitiesList; }
            set
            {
                qualitiesList = value;
                OnPropertyChanged();
            }
        }

        private string numOfChoices;

        public string NumOfChoices
        {
            get { return numOfChoices; }
            set { numOfChoices = value;
                OnPropertyChanged();
            }
        }

        #endregion


    }
}
