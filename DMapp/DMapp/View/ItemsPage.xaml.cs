using DMapp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Internals;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DMapp.Models;

namespace DMapp.View
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyWalletPage"/> class.
        /// </summary>
        /// 

       

        ItemsVM viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            viewModel = new ItemsVM(Navigation);
            BindingContext = viewModel;
        }

       
        protected override void OnAppearing()
        {

            viewModel.PageAppeared();
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            viewModel.SelectedSession = e.SelectedItem as DecisionSession;
            itemList.SelectedItem = null;
        }

       
    }
}