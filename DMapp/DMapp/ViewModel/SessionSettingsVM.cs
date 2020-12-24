using DMapp.Helpers;
using DMapp.Services;
using DMapp.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DMapp.ViewModel
{
    class SessionSettingsVM : BaseViewModel
    {
        INavigation navigation;
        
        public Command ContinueButtonClickedCommand { get; set; }
        public SessionSettingsVM(INavigation navi)
        {
            ContinueButtonClickedCommand = new Command(async () => await ExecuteContinueButtonClickedCommand());
            navigation = navi;
            if(InitializationCounter.numOfSessionSettingsVMInitialized == 0)
            {
                TemporaryDb.StopWatch = "True";
            }
            InitializationCounter.numOfSessionSettingsVMInitialized++;
            StopWatchOn = false; // In future version count time

        }
        private async Task ExecuteContinueButtonClickedCommand()
        {
            await navigation.PushAsync(new QualitiesChoicesPage(navigation));
        }


        // Add charts
        //tweak accuracy and time system to make it more or less reasonable, but better do it later when I can finally make some decisions in app
        // Display number of choices in qualities and option setup

        public void CalculateTimeAndAccuracy()
        {

            //Future:
            // In short, the more decisions you make with particular setting setup, the more precize is the prediction of time and accuracy. For first decision in case of choosen setting, approximated values for accuracy and time were inserted, with respect to number of qualities and options.. There are other factors which may affect accuracy which were not incorporated here. For example number of qualities and options. THe more options and qualities, the more accurate is the comparison.
            //Problem: accuracy can be highly affected by number of options and qualities. In the future create trend line function (from statistics) to know what will be estimated accuracy for particular number of options and qualities. Gather data for 4 situations: no system, turboMode on, accuracyMode on, turbo and accuracyModeOn. In each situation check how accuracy will depend on number of options and number of qualities. Then when user will choose situation 1, provide estimation for situation 1 (no system), when he will choose situation 2, provide estimation for situation 2. This way we will know that estimate accuracy in each situation really depend on num of options and qualities, not because of using the system. Then we will add to accuracy appropriate values depending on which system was/wasn't used, thanks to data collected in our dataBase. By measuring accuracy provided by switiching on a system we need to remember to compare accuracies (one accuracy with using system and one without a system) which have similar number of options and criterias. This way we will have reliable information how particular system affects accuracy.
            // in the future use averages from data base, not hard coded numbers, I shoud create helper for it.

            //Now
            // Solution for problem now (find better in the future) - you can increasy accuracy maximaly to 60% by increasing num of qualities and options. The rest can be increased by using particualr system. All is estimated from nowhere and not based on data.
            // We keep time and accuracy hardcoded, but I will take to to enable measuring time and success, so future improvements will be easier.


            int optionsNumber = TemporaryDb.optionNames.Count;
            int qualitiesNumber = TemporaryDb.qualityNames.Count;
            // In the future display numberOfChoices while adding options and categories so people can know.



            double numberOfChoices = (((Math.Pow(qualitiesNumber,2)) - qualitiesNumber) / 2) + (optionsNumber* qualitiesNumber);
            double accuracy = 0;
            double time = 0;
            double maximalAccuracyGainedOnlyBuyNumOfChoices = 0.8;
            double accuracyPerChoice = 0.022;


            
          
                double timePerChoice = 8; // (accuracy time is longer because we need to compare also information reliability), In the future: we calculate by taking number of all choices, doesn't matter that turbo will cancel some of them, and all time required
                 time = timePerChoice * numberOfChoices;


            if(numberOfChoices <= 12)
            {
                accuracy = accuracyPerChoice * numberOfChoices;
            }
            else if(numberOfChoices > 12 && numberOfChoices <= 25)
            {
                accuracy += accuracyPerChoice * 12;
                accuracy += accuracyPerChoice * 0.8 * (numberOfChoices - 12);
            }
            else if(numberOfChoices > 25 && numberOfChoices <= 33)
            {
                accuracy += accuracyPerChoice * 12;
                accuracy += accuracyPerChoice * 0.8 * 13;
                accuracy += accuracyPerChoice * 0.5 * (numberOfChoices - 25);
            }
            else if(numberOfChoices > 33 && numberOfChoices < 39)
            {
                accuracy += accuracyPerChoice * 12;
                accuracy += accuracyPerChoice * 0.8 * 13;
                accuracy += accuracyPerChoice *0.5 *6;
                accuracy += accuracyPerChoice *0.3* (numberOfChoices - 33);
            }
            else {
                accuracy += accuracyPerChoice * 12;
                accuracy += accuracyPerChoice * 0.8 * 13;
                accuracy += accuracyPerChoice * 0.5 * 6;
                accuracy += accuracyPerChoice * 0.3 * 6;
                accuracy += accuracyPerChoice * 0.2 * (numberOfChoices - 39);
            }


                
                if (accuracy > maximalAccuracyGainedOnlyBuyNumOfChoices) { accuracy = maximalAccuracyGainedOnlyBuyNumOfChoices; }
                
           
            

            
            // I need to figure out how to measure accuracy change and time change in case of each system.
            // maybe enable making decision for example with info reliability, only if 

            //changing number to appropriate string format.

            TimeSpan t = TimeSpan.FromSeconds(time);
            string timeToReturn;
            if (t.Hours == 0)
            {
                timeToReturn = string.Format("{0:D2}m {1:D2}s",
                t.Minutes,
                t.Seconds);
            }
            else
            {
                timeToReturn = string.Format("{0:D2}h {1:D2}m {2:D2}s",
               t.Hours,
               t.Minutes,
               t.Seconds);
            }

           
            
            TimeToDisplay = timeToReturn;
            AccuracyToDisplay = (accuracy*100).ToString() + "%" ;
        }

        public void LoadSettings()
        {
            string tempDbStopWatch = TemporaryDb.StopWatch;
            if(tempDbStopWatch == "True") { StopWatchOn = true; } else { StopWatchOn = false; }
        }

        #region BindableProperties

        private string timeToDisplay;
        public string TimeToDisplay
        {
            get { return timeToDisplay; }
            set { timeToDisplay = value;
                OnPropertyChanged();
            }
        }

        private string accuracyToDisplay;
        public string AccuracyToDisplay
        {
            get { return accuracyToDisplay; }
            set { accuracyToDisplay = value;
                OnPropertyChanged();
            }
        }

        
        private bool stopWatchOn;

        public bool StopWatchOn
        {
            get { return stopWatchOn; }
            set { stopWatchOn = value;
                TemporaryDb.StopWatch = value.ToString();
                OnPropertyChanged();
            }
        }


        #endregion

    }
}
