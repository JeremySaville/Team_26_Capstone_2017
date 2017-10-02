using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesApp.DataTypes;
using DiabetesApp.Models;

namespace DiabetesApp {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        bool gamified;
        FirebaseAuthLink auth;
        GameStats gStats;
        
        //Constructor
		public ProfilePage (FirebaseAuthLink auth, bool gamified) {
			InitializeComponent ();
            this.gamified = gamified;
            this.auth = auth;

            updateStats();

            if (!gamified) {
                profileTable.Remove(gameStats);
                profileTable.Remove(badges);
            } else {
                updateGamifiedStats();
            }
		}

        //update the table with number of logs made, etc.
        private async void updateStats() {
            DateTime latest = DateTime.MinValue;
            int count = 0;

            //Get the required information from the database
            var firebase = new FirebaseClient(FirebaseURL);
            try {
                var items = await firebase
                    .Child("logbooks")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceAsync<LogbookEntry>();

                foreach (var item in items) {
                    DateTime newVal = DateTime.Parse(item.Key);
                    if (latest < newVal) {
                        latest = newVal;
                    }
                }

                count = items.Count;
            } catch {

            }

            //Update count
            nLogs.Detail = count.ToString();

            //Update latest entry
            if (count == 0)
                lastLogTime.Detail = "No Logs Found";
            else
                lastLogTime.Detail = latest.ToString("dddd d MMMM yyyy, h:mm tt");
        }

        //update the bits relevant to gamified users
        private async void updateGamifiedStats() {
            var firebase = new FirebaseClient(FirebaseURL);
            try {
                var item = await firebase
                    .Child("gameStats")
                    .Child(auth.User.LocalId)
                    .WithAuth(auth.FirebaseToken)
                    .OnceSingleAsync<GameStats>();

                gStats = item;
            } catch {
                //no gamestats entered for the user, for some reason
            }
            updateGameStatsTable();
        }

        //Update the view with the relevant information gained
        private void updateGameStatsTable() {
            level.Detail = gStats.level.ToString();
            xp.Detail = gStats.xp.ToString();
            coins.Detail = gStats.coins.ToString();
            logins.Detail = gStats.numLogins.ToString();

            //Calculate the experience required for the next level
            xpNextLevel.Detail = GamificationTools.getExpToNextLevel(gStats.level, gStats.xp).ToString();
        }
	}
}