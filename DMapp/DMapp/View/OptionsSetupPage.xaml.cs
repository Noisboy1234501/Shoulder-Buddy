using DMapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DMapp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsSetupPage : ContentPage
    {
        OptionsSetupVM viewModel; 
        public OptionsSetupPage(INavigation navi)
        {
            InitializeComponent();
            viewModel = new OptionsSetupVM(navi);
            viewModel.CalculateNumberOfChoices();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCurrentOptions();
            viewModel.CalculateNumberOfChoices();
            listview.SelectedItem = String.Empty;
        }
    }
}