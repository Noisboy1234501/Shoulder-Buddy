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
    public partial class SessionSettingsPage : ContentPage
    {
        
        INavigation navigation;
        SessionSettingsVM viewModel;
        public SessionSettingsPage(INavigation navi)
        {
            InitializeComponent();
            navigation = navi;
            viewModel = new SessionSettingsVM(navi);
            BindingContext = viewModel;
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.CalculateTimeAndAccuracy();
            viewModel.LoadSettings();
        }
    }
}