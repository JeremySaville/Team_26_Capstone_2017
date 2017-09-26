using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database.Query;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiabetesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";

        //Constructor
        public LoginPage() {
            InitializeComponent();
        }

        //Handles a login request
        async void onClick_loginButton(object sender, EventArgs e) {
            loginButton.IsEnabled = false;

            string usernameEntered = usernameEntry.Text;
            string birthdayEntered = birthdayEntry.Date.ToString("yyyy-MM-dd");

            if (usernameEntered == null || usernameEntered == "") {
                await DisplayAlert("Login Failed", "Must enter your username", "OK");
                return;
            }

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDMHZSuyi3F5RqkhDBJnTOSNxwDaxzRSVQ"));

            //Attempt to log in
            FirebaseAuthLink auth = null;
            try {
                auth = await authProvider.SignInWithEmailAndPasswordAsync(usernameEntered + "@diabetesarp.com", birthdayEntered);
            } catch {
                //login failed
                await DisplayAlert("Login Failed", "Unable to login at this time", "OK");
                loginButton.IsEnabled = true;
                return;
            }
            //Check whether logged in user is gamified
            bool gamified = false;
            try {
                var firebase = new FirebaseClient(FirebaseURL);
                var items = await firebase
                    .Child("users")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceAsync<String>();

                foreach (var item in items) {
                    if (item.Key == "gamified" && item.Object == "2")
                        gamified = true;
                }
            } catch { 
                //user is not gamified
            }

            App.Current.MainPage = new TabbedContent(auth, gamified);
        }
    }
}