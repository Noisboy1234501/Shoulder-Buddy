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
    public partial class GeneralResultsPage : ContentPage
    {
        GeneralResultsVM viewModel;
        public GeneralResultsPage(INavigation navi,int mode, int sessionID)
        {
            InitializeComponent();
            viewModel = new GeneralResultsVM(navi,mode,sessionID);
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.PageDisplayed();
        }
    }
}