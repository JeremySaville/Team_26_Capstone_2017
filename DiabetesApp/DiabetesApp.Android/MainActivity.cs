using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DiabetesApp.Droid
{
	[Activity (Label = "Diabetes Logbook", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            var droidAccentColor = Android.Graphics.Color.DodgerBlue;

            // set Xamarin Color.Accent to match the theme's accent color
            var accentColorProp = typeof(Xamarin.Forms.Color).GetProperty(nameof(Xamarin.Forms.Color.Accent), System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            var xamarinAccentColor = new Xamarin.Forms.Color(droidAccentColor.R / 255.0, droidAccentColor.G / 255.0, droidAccentColor.B / 255.0, droidAccentColor.A / 255.0);
            accentColorProp.SetValue(null, xamarinAccentColor, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static, null, null, System.Globalization.CultureInfo.CurrentCulture);


            LoadApplication(new DiabetesApp.App());
		}
	}
}

