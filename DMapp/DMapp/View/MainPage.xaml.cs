using DMapp.Models;
using DMapp.View;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DMapp
{
    public partial class MainPage : MasterDetailPage
    {

        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

        }

        //public MainPage(INavigation navi)
        //{
        //    InitializeComponent();
        //    MasterBehavior = MasterBehavior.Popover;

        //    MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

        //    var existingPages = navi.NavigationStack.ToList();
        //    int existingPagesNum = existingPages.Count();
        //    int counter = 0;
        //    foreach (var page in existingPages)
        //    {
        //        if(counter != 0)
        //        {
        //            navi.RemovePage(page);
        //        }
        //        counter++;

        //    }
        //}

        public async Task NavigateFromMenu(int id)
        {

            if (!MenuPages.ContainsKey(id)) // we want to handle situation when menuPages do not have our ID
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    
                       
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }

        }

    }
}
