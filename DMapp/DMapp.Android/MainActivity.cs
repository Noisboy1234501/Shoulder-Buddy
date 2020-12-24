using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using DMapp.View;
using Lottie.Forms.Droid;

namespace DMapp.Droid
{



    [Activity(Label = "DMapp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //allowing the device to change the screen orientation based on the rotation 
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            AnimationViewRenderer.Init();


            string dbName = "5233assdfsdawf3gawfafdfsdfdwas23ccrgd4wa.sqlite"; //name of the file
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); //folder where the file will be created
            string fullPath = System.IO.Path.Combine(folderPath, dbName);

            MessagingCenter.Subscribe<DetailedResultsPage>(this, "AllowLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            });

            MessagingCenter.Subscribe<DetailedResultsPage>(this, "PreventLandscape", sender =>
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            });

            LoadApplication(new App(fullPath));
        }




        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}