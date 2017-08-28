using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        public LoginPage() {
            InitializeComponent();
        }

        async void onClick_loginButton(object sender, EventArgs e) {
            loginButton.IsEnabled = false;

            string usernameEntered = usernameEntry.Text;
            string birthdayEntered = birthdayEntry.Date.ToString("yyyy-MM-dd");

            if (usernameEntered == null || usernameEntered == "") {
                await DisplayAlert("Login Failed", "Must enter your username", "OK");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDMHZSuyi3F5RqkhDBJnTOSNxwDaxzRSVQ"));
            
            try {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(usernameEntered + "@diabetesarp.com", birthdayEntered);
                App.Current.MainPage = new TabbedContent(auth);
            } catch (Exception ex) {
                await DisplayAlert("Login Failed", "Unable to login at this time", "OK");
            }

            loginButton.IsEnabled = true;
        }
    }
}