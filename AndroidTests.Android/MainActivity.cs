using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;

using AndroidTests.Droid;
using AndroidTests;
using System.Collections.Generic;

namespace AndroidTests.Droid
{
    [Activity(Label = "Android Example Tests", Icon = "@drawable/light", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string basename = "MyBaseName.mbn";
        /// <summary>
        /// ЭТОТ ФАЙЛ ЛЕЖИТ В ASSETS!
        /// </summary>
        readonly string XMLname = "EB_3_(2021)743.xml";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //string xmlString = 
          
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            string xml;
            using (StreamReader stream = new StreamReader(Assets.Open(XMLname)))
            {
             xml = stream.ReadToEnd();
            }
            LoadApplication(new App(basename, xml));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
             
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        /// <summary>
        /// Загрузить вопросы из XML
        /// </summary>
        /// <returns></returns>
        private string LoadFromXML()
        {
            string f = "";
            using (StreamReader sr = new StreamReader(Assets.Open(XMLname)))
            {
                f = sr.ReadToEnd();
            }
            return f;
        }
    }
}