
using DMapp.View;

using Xamarin.Forms;


namespace DMapp
{
    public partial class App : Application
    {

        public static string DataBase = string.Empty;

       
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcxMjI4QDMxMzgyZTM0MmUzMFloRTZySVdtL2x0eUVHVGNRT0lteU1Pc3lwQXVJUk13Yk85VHlQWGxTQnc9");
            InitializeComponent();
            MainPage = new NavigationPage(new ItemsPage());
        }


        public App(string DataBaseLocation)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzcxMjI4QDMxMzgyZTM0MmUzMFloRTZySVdtL2x0eUVHVGNRT0lteU1Pc3lwQXVJUk13Yk85VHlQWGxTQnc9");
            InitializeComponent();
            MainPage = new NavigationPage(new ItemsPage());

            DataBase = DataBaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static string BaseImageUrl { get; } = "https://cdn.syncfusion.com/essential-ui-kit-for-xamarin.forms/common/uikitimages/";
    }
}
