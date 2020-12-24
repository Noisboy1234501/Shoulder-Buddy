using DMapp.Models;
using DMapp.Services;
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
    public partial class DetailedResultsPage : ContentPage
    {
        DetailedResultsVM viewModel;
        private int Mode;
        public DetailedResultsPage(int SessionID, int mode)
        {
            InitializeComponent();
            viewModel = new DetailedResultsVM(Navigation, SessionID, mode);
            BindingContext = viewModel;
            Mode = mode;
            
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            base.OnAppearing();
            MessagingCenter.Send(this, "AllowLandscape");
            ListRow.Height = viewModel.ReturnHightOfRowWithList();
            viewModel.ChangeHightOfRowWithOptions();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Send(this, "PreventLandscape"); //during page close setting back to portrait 
        }


    }
}