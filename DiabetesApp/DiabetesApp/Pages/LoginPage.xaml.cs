using Firebase.Xamarin.Database;
using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database.Query;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesApp.DataTypes;
using DiabetesApp.Models;

namespace DiabetesApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        GameStats gStats;

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
            var firebase = new FirebaseClient(FirebaseURL);
            bool gamified = false;

            try {
                var item = await firebase
                    .Child("users")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<DataTypes.User>();

                gamified = item.gamified == 2;

            } catch { 
                //user is not gamified
            }

            //if gamified, check for num logins, increment if necessary
            if (gamified) {
                try {
                    var item = await firebase
                        .Child("gameStats")
                        .Child(auth.User.LocalId)
                        .WithAuth(auth.FirebaseToken)
                        .OnceSingleAsync<GameStats>();
                    gStats = item;
                } catch {
                    gStats = GamificationTools.initStats(auth);
                }
                DateTime loginTime = DateTime.Now;
                DateTime lastLogin = DateTime.Parse(gStats.lastLogin);
                if (loginTime.Year == lastLogin.Year && loginTime.Month == lastLogin.Month && loginTime.Day == lastLogin.Day) {
                    //Nothing happens already logged in today
                } else if (loginTime.Year == lastLogin.Year && loginTime.Month == lastLogin.Month && loginTime.Day == lastLogin.Day + 1) {
                    gStats.numLogins += 1;
                    gStats.lastLogin = loginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    if(gStats.numLogins % 7 == 0) {
                        //Seven consecutive logins, bonus XP
                        gStats.xp += GamificationTools.loginBonusXp;
                        GamificationTools.updateGStatsDB(auth, gStats);
                        await DisplayAlert("Login Bonus", "Received 50xp for seven consecutive logins", "OK");
                    } else {
                        //Normal XP
                        gStats.xp += GamificationTools.loginXP;
                        GamificationTools.updateGStatsDB(auth, gStats);
                        await DisplayAlert("Login Reward", "Received 10xp for logging in", "OK");
                    }
                } else {
                    gStats.numLogins = 1;
                    gStats.lastLogin = loginTime.ToString("yyyy-MM-dd HH:mm:ss");
                    gStats.xp += GamificationTools.loginXP;
                    GamificationTools.updateGStatsDB(auth, gStats);
                    await DisplayAlert("Login Reward", "Received 10xp for logging in", "OK");
                }
            }

            Application.Current.MainPage = new TabbedContent(auth, gamified);
        }

        
    }
}