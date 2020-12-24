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
    public partial class QualitiesSetupPage : ContentPage
    {
        QualitiesSetupVM viewModel;
        public QualitiesSetupPage(INavigation navi)
        {
            InitializeComponent();
            viewModel = new QualitiesSetupVM(navi);
            viewModel.CalculateNumberOfChoices();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadCurrentOptions();
            viewModel.CalculateNumberOfChoices();
        }


    }
}