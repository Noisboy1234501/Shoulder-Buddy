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
    public partial class SessionSetupPage : ContentPage
    {
        SessionSetupVM viewModel;
        // make sure typed category name hasn' exist at database yet!!!!
        public SessionSetupPage(INavigation navi)
        {
            InitializeComponent();
            viewModel = new SessionSetupVM(navi);
            BindingContext = viewModel;
            
        }

      
    }
}