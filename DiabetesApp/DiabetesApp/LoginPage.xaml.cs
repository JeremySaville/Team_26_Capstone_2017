using DiabetesApp.DataTypes;
using Firebase.Xamarin.Database;
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

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        public LoginPage() {
            InitializeComponent();
        }

        async void onClick_loginButton(object sender, EventArgs e) {
            string usernameEntered = usernameEntry.Text;
            string birthdayEntered = birthdayEntry.Date.ToString("yyyy-MM-dd");

            bool loggedIn = false;

            if (usernameEntered == null || usernameEntered == "") {
                await DisplayAlert("Login Failed", "Must enter your username", "OK");
                return;
            }

            var firebase = new FirebaseClient(FirebaseURL);

            var items = await firebase
                .Child("users")
                .OnceAsync<User>();

            foreach (var item in items) {
                if (item.Key.ToLower().Equals(usernameEntered.ToLower())) {
                    loggedIn = item.Object.birthday.Equals(birthdayEntered);
                    usernameEntered = item.Key;
                }
            }

            if (loggedIn) {
                App.Current.MainPage = new TabbedContent(usernameEntered);
            } else {
                await DisplayAlert("Login Failed", "Unable to login at this time", "OK");
            }
        }
    }
}