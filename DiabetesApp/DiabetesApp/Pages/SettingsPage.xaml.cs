using Firebase.Xamarin.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp { 
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage { 

        FirebaseAuthLink auth;

		public SettingsPage (FirebaseAuthLink auth) {
			InitializeComponent ();
            this.auth = auth;
		}

        private async void onClick_logoutButton(object sender, EventArgs e) {
            bool logout = await DisplayAlert("Log Out?", "Log out of the app?", "Yes", "No");
            if(logout) Application.Current.MainPage = new LoginPage();
        }
    }
}