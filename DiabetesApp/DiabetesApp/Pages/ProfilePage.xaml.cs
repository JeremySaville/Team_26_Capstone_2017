using Firebase.Xamarin.Auth;
using Firebase.Xamarin.Database;
using Firebase.Xamarin.Database.Query;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DiabetesApp.DataTypes;

namespace DiabetesApp {
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage {

        private const string FirebaseURL = "https://diabetesarp.firebaseio.com/";
        bool gamified;
        FirebaseAuthLink auth;
        GameStats gStats;
        int[] xpForLevel = new int[] { 200, 300, 500, 700, 900, 1100, 1300, 1400, 1500 };
        int xpForLaterLevels = 1500;

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
            xpNextLevel.Detail = getExpToNextLevel(gStats.level, gStats.xp).ToString();
        }
        
        

        //Helper method to calculate the experience required for the next level
        private int getExpToNextLevel(int currentLevel, int currentExp) {
            int totalRequired = 0;
            if(currentLevel > xpForLevel.Length) {
                totalRequired = SumArray(xpForLevel) + (currentLevel - xpForLevel.Length) * xpForLaterLevels;
            } else {
                for(int i=0; i<currentLevel; i++) {
                    totalRequired += xpForLevel[i];
                }
            }
            return totalRequired - currentExp;
        }

        //Helper method to add all the elements of a numeric array
        private int SumArray(int[] array) {
            int sum = 0;
            foreach(int i in array) {
                sum += i;
            }
            return sum;
        }

	}
}